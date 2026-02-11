using System.Configuration;

namespace SAFTExtractor.Utils
{
    /// <summary>
    /// Configurações do sistema PULSE DOMINOS
    /// </summary>
    public static class SystemSettings
    {
        private static string? _locationCode;
        
        /// <summary>
        /// Código da localização/loja (usado em todas as SPs)
        /// </summary>
        public static string LocationCode
        {
            get
            {
                if (string.IsNullOrEmpty(_locationCode))
                {
                    // Tentar obter do app.config
                    _locationCode = ConfigurationManager.AppSettings["LocationCode"];
                    
                    // Se não encontrar, usar valor padrão
                    if (string.IsNullOrEmpty(_locationCode))
                    {
                        _locationCode = "01"; // Código padrão
                    }
                }
                
                return _locationCode;
            }
            set
            {
                _locationCode = value;
            }
        }
        
        /// <summary>
        /// Inicializa as configurações do sistema
        /// </summary>
        public static void Initialize()
        {
            // Garantir que as configurações estão carregadas
            _ = LocationCode;
        }
    }
}
