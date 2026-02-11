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
        /// Testa a conexão e retorna detalhes
        /// </summary>
        public static (bool success, string message) TestConnectionWithDetails()
        {
            return PulseConnection.TestConnectionWithDetails();
        }
        
        /// <summary>
        /// Inicializa a aplicação com as configurações necessárias
        /// </summary>
        public static void Initialize()
        {
            // Inicializar configurações do sistema
            SystemSettings.Initialize();
            
            Console.WriteLine("=== SAFTExtractor - PULSE DOMINOS ===");
            Console.WriteLine($"LocationCode configurado: '{SystemSettings.LocationCode}'");
            Console.WriteLine($"Connection String: {PulseConnection.GetConnectionStringSafe()}");
            Console.WriteLine();
            
            // Testar conexão
            var (success, message) = TestConnectionWithDetails();
            
            if (success)
            {
                Console.WriteLine("✓ " + message);
            }
            else
            {
                Console.WriteLine("✗ AVISO: Não foi possível conectar à base de dados PULSE.");
                Console.WriteLine(message);
                Console.WriteLine();
                Console.WriteLine("A aplicação pode não funcionar corretamente.");
                Console.WriteLine("Verifique a connection string no App.config.");
            }
            
            Console.WriteLine();
        }
    }
}
