using System.Data.SqlClient;
using SAFTExtractor.Utils;

namespace SAFTExtractor.Services
{
    /// <summary>
    /// Lógica de arranque e autenticação da aplicação
    /// </summary>
    public class SAFTAdditionStartup
    {
        /// <summary>
        /// Testa a conexão à base de dados PULSE
        /// </summary>
        public static bool TestConnection()
        {
            try
            {
                return PulseConnection.TestConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao testar conexão: {ex.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// Inicializa a aplicação com as configurações necessárias
        /// </summary>
        public static void Initialize()
        {
            // Inicializar configurações do sistema
            SystemSettings.Initialize();
            
            // Testar conexão
            if (!TestConnection())
            {
                Console.WriteLine("AVISO: Não foi possível conectar à base de dados PULSE.");
                Console.WriteLine("Verifique a connection string no app.config ou configure manualmente.");
            }
            else
            {
                Console.WriteLine($"Aplicação SAFTExtractor inicializada. LocationCode: {SystemSettings.LocationCode}");
            }
        }
    }
}
