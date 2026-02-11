using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

namespace SAFTExtractor.Utils
{
    /// <summary>
    /// Classe para gerenciar conexões com o banco de dados PULSE DOMINOS
    /// Simula o comportamento do PulseConnection original
    /// </summary>
    public static class PulseConnection
    {
        private static string? _connectionString;
        
        /// <summary>
        /// Obtém a connection string da configuração
        /// </summary>
        public static string GetConnectionString()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                // Tentar obter do app.config
                _connectionString = ConfigurationManager.ConnectionStrings["PulseDB"]?.ConnectionString;
                
                // Se não encontrar, usar connection string padrão para PULSE DOMINOS
                if (string.IsNullOrEmpty(_connectionString))
                {
                    // Connection string padrão com Integrated Security
                    _connectionString = "Data Source=.;Initial Catalog=POS;Integrated Security=True;TrustServerCertificate=True";
                }
            }
            
            return _connectionString;
        }
        
        /// <summary>
        /// Obtém a connection string sem a password (para exibir em mensagens de erro)
        /// </summary>
        public static string GetConnectionStringSafe()
        {
            var connStr = GetConnectionString();
            
            // Remover password da connection string
            var safe = Regex.Replace(connStr, @"Password\s*=\s*[^;]*;?", "Password=****;", RegexOptions.IgnoreCase);
            safe = Regex.Replace(safe, @"Pwd\s*=\s*[^;]*;?", "Pwd=****;", RegexOptions.IgnoreCase);
            
            return safe;
        }
        
        /// <summary>
        /// Define a connection string manualmente (para testes ou configuração externa)
        /// </summary>
        public static void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        /// <summary>
        /// Cria uma nova conexão SQL usando a connection string configurada
        /// </summary>
        public static SqlConnection CreateSQLConnection()
        {
            var connectionString = GetConnectionString();
            return new SqlConnection(connectionString);
        }
        
        /// <summary>
        /// Testa se a conexão está funcionando
        /// </summary>
        public static bool TestConnection()
        {
            try
            {
                using (var connection = CreateSQLConnection())
                {
                    connection.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        
        /// <summary>
        /// Testa a conexão e retorna mensagem de erro se falhar
        /// </summary>
        public static (bool success, string message) TestConnectionWithDetails()
        {
            try
            {
                using (var connection = CreateSQLConnection())
                {
                    connection.Open();
                    return (true, $"Conexão estabelecida com sucesso!\nServidor: {connection.DataSource}\nBase de dados: {connection.Database}");
                }
            }
            catch (SqlException sqlEx)
            {
                return (false, $"Erro SQL: {sqlEx.Message}\nConnection String: {GetConnectionStringSafe()}");
            }
            catch (Exception ex)
            {
                return (false, $"Erro: {ex.Message}\nConnection String: {GetConnectionStringSafe()}");
            }
        }
    }
}
