using System.Data.SqlClient;
using System.Configuration;

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
    }
}
