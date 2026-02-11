namespace SAFTExtractor.Utils
{
    /// <summary>
    /// Configuração de conexão à base de dados PULSE DOMINOS
    /// </summary>
    public class DatabaseConfig
    {
        public string Server { get; set; } = string.Empty;
        public string Database { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IntegratedSecurity { get; set; } = false;
        
        /// <summary>
        /// Obtém a string de conexão
        /// </summary>
        public string GetConnectionString()
        {
            if (IntegratedSecurity)
            {
                return $"Server={Server};Database={Database};Integrated Security=True;TrustServerCertificate=True;";
            }
            else
            {
                return $"Server={Server};Database={Database};User Id={Username};Password={Password};TrustServerCertificate=True;";
            }
        }
        
        /// <summary>
        /// Valida se a configuração está completa
        /// </summary>
        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Server) || string.IsNullOrWhiteSpace(Database))
                return false;
                
            if (!IntegratedSecurity && (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password)))
                return false;
                
            return true;
        }
    }
}
