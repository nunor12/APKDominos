using SAFTExtractor.Services;
using SAFTExtractor.Utils;

namespace SAFTExtractor.Forms;

/// <summary>
/// Formulário principal da aplicação SAFTExtractor
/// Interface para configuração e extração de dados SAFT do PULSE DOMINOS
/// </summary>
public partial class SAFTAdditionForm : Form
{
    private DatabaseConfig _dbConfig;
    
    public SAFTAdditionForm()
    {
        InitializeComponent();
        _dbConfig = new DatabaseConfig();
        InitializeForm();
    }
    
    private void InitializeForm()
    {
        // Configurar título e tamanho
        this.Text = "SAFTExtractor - PULSE DOMINOS";
        this.Size = new Size(800, 600);
        this.StartPosition = FormStartPosition.CenterScreen;
        
        // Inicializar aplicação
        SAFTAdditionStartup.Initialize();
    }
    
    private void BtnTestConnection_Click(object sender, EventArgs e)
    {
        // Obter dados da UI
        _dbConfig.Server = txtServer.Text;
        _dbConfig.Database = txtDatabase.Text;
        _dbConfig.Username = txtUsername.Text;
        _dbConfig.Password = txtPassword.Text;
        _dbConfig.IntegratedSecurity = chkIntegratedSecurity.Checked;
        
        if (SAFTAdditionStartup.TestConnection(_dbConfig))
        {
            MessageBox.Show("Conexão testada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            MessageBox.Show("Erro ao conectar à base de dados. Verifique as configurações.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    
    private void BtnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            // Validar configuração
            if (!_dbConfig.IsValid())
            {
                MessageBox.Show("Configure e teste a conexão antes de gerar o ficheiro SAFT.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // Obter ano fiscal
            int fiscalYear = (int)numFiscalYear.Value;
            
            // Selecionar destino do ficheiro
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
                saveDialog.DefaultExt = "xml";
                saveDialog.FileName = $"SAFT_{fiscalYear}.xml";
                
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // Mostrar progresso
                    lblStatus.Text = "A gerar ficheiro SAFT...";
                    Application.DoEvents();
                    
                    // Gerar ficheiro
                    var saftGenerator = new SAFTXMLObject(_dbConfig);
                    saftGenerator.GenerateSAFTFile(saveDialog.FileName, fiscalYear);
                    
                    lblStatus.Text = "Ficheiro SAFT gerado com sucesso!";
                    MessageBox.Show($"Ficheiro SAFT gerado com sucesso!\n\n{saveDialog.FileName}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        catch (Exception ex)
        {
            lblStatus.Text = "Erro ao gerar ficheiro SAFT.";
            MessageBox.Show($"Erro ao gerar ficheiro SAFT:\n\n{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    
    private void ChkIntegratedSecurity_CheckedChanged(object sender, EventArgs e)
    {
        // Habilitar/desabilitar campos de usuário e senha
        txtUsername.Enabled = !chkIntegratedSecurity.Checked;
        txtPassword.Enabled = !chkIntegratedSecurity.Checked;
    }
}
