"""
Gestão de conexões SQL Server para PULSE DOMINOS
"""
import pyodbc
import configparser
from typing import Optional

class PulseConnection:
    """Gerencia conexões SQL Server baseadas no config.ini"""
    
    _config: Optional[configparser.ConfigParser] = None
    
    @classmethod
    def load_config(cls, config_path: str = 'config.ini') -> None:
        """Carrega configuração do arquivo config.ini"""
        cls._config = configparser.ConfigParser()
        cls._config.read(config_path)
    
    @classmethod
    def get_connection_string(cls) -> str:
        """Constrói a connection string a partir do config.ini"""
        if cls._config is None:
            cls.load_config()
        
        db = cls._config['Database']
        server = db.get('Server', '.')
        database = db.get('Database', 'POS')
        driver = db.get('Driver', 'ODBC Driver 17 for SQL Server')
        trusted = db.getboolean('TrustedConnection', True)
        
        if trusted:
            conn_str = (
                f"Driver={{{driver}}};"
                f"Server={server};"
                f"Database={database};"
                f"Trusted_Connection=yes;"
            )
        else:
            username = db.get('Username', '')
            password = db.get('Password', '')
            conn_str = (
                f"Driver={{{driver}}};"
                f"Server={server};"
                f"Database={database};"
                f"UID={username};"
                f"PWD={password};"
            )
        
        return conn_str
    
    @classmethod
    def create_connection(cls) -> pyodbc.Connection:
        """Cria uma nova conexão SQL Server"""
        conn_str = cls.get_connection_string()
        return pyodbc.connect(conn_str)
    
    @classmethod
    def test_connection(cls) -> tuple[bool, str]:
        """
        Testa a conexão com a base de dados
        Returns: (sucesso: bool, mensagem: str)
        """
        try:
            conn = cls.create_connection()
            cursor = conn.cursor()
            cursor.execute("SELECT @@VERSION")
            version = cursor.fetchone()[0]
            conn.close()
            
            return (True, f"Conexão estabelecida com sucesso!\n{version}")
        except Exception as e:
            return (False, f"Erro ao conectar: {str(e)}")
    
    @classmethod
    def get_location_code(cls) -> str:
        """Obtém o LocationCode do config.ini"""
        if cls._config is None:
            cls.load_config()
        
        return cls._config['Settings'].get('LocationCode', '01')
    
    @classmethod
    def get_command_timeout(cls) -> int:
        """Obtém o timeout de comandos do config.ini"""
        if cls._config is None:
            cls.load_config()
        
        return cls._config['Settings'].getint('CommandTimeout', 0)
