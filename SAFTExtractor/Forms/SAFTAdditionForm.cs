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
    private SAFTDate _fiscalYearInfo;
    
    public SAFTAdditionForm()
    {
        InitializeComponent();
        _dbConfig = new DatabaseConfig();
        _fiscalYearInfo = new SAFTDate();
        InitializeForm();
    }
    
    private void InitializeForm()
    {
        // Configurar título e tamanho
        this.Text = "SAFTExtractor - PULSE DOMINOS";
        this.Size = new Size(800, 650);
        this.StartPosition = FormStartPosition.CenterScreen;
        
        // Inicializar DatePickers
        startDatePicker.Format = DateTimePickerFormat.Short;
        endDatePicker.Format = DateTimePickerFormat.Short;
        startDatePicker.Value = new DateTime(DateTime.Now.Year, 1, 1);
        endDatePicker.Value = DateTime.Today;
        
        // Calcular ano fiscal inicial
        UpdateFiscalYear();
        
        // Inicializar aplicação
        SAFTAdditionStartup.Initialize();
    }
    
    private void UpdateFiscalYear()
    {
        _fiscalYearInfo = SAFTDate.GetFiscalYearInfo(startDatePicker.Value);
        txtFiscalYear.Text = _fiscalYearInfo.FiscalYear;
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
            
            // Validar datas
            if (!ValidateDates())
            {
                return;
            }
            
            // Selecionar destino do ficheiro
            using (var saveDialog = new SaveFileDialog())
            {
                DateTime startDate = startDatePicker.Value;
                DateTime endDate = endDatePicker.Value;
                
                saveDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
                saveDialog.DefaultExt = "xml";
                saveDialog.FileName = $"SAFT_{startDate:yyyyMMdd}_{endDate:yyyyMMdd}.xml";
                
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // Mostrar progresso
                    lblStatus.Text = "A gerar ficheiro SAFT...";
                    this.Cursor = Cursors.WaitCursor;
                    Application.DoEvents();
                    
                    // Gerar ficheiro
                    var saftGenerator = new SAFTXMLObject(_dbConfig);
                    saftGenerator.GenerateSAFTFile(saveDialog.FileName, startDate, endDate);
                    
                    this.Cursor = Cursors.Default;
                    lblStatus.Text = "Ficheiro SAFT gerado com sucesso!";
                    MessageBox.Show($"Ficheiro SAFT gerado com sucesso!\n\nPeríodo: {startDate:dd/MM/yyyy} a {endDate:dd/MM/yyyy}\nFicheiro: {saveDialog.FileName}", 
                        "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        catch (Exception ex)
        {
            this.Cursor = Cursors.Default;
            lblStatus.Text = "Erro ao gerar ficheiro SAFT.";
            MessageBox.Show($"Erro ao gerar ficheiro SAFT:\n\n{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    
    private bool ValidateDates()
    {
        DateTime startDate = startDatePicker.Value.Date;
        DateTime endDate = endDatePicker.Value.Date;
        
        // Validar data início não é futura
        if (startDate > DateTime.Today)
        {
            MessageBox.Show("A data de início não pode ser superior à data atual.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            startDatePicker.Value = DateTime.Today;
            return false;
        }
        
        // Validar data fim não é futura
        if (endDate > DateTime.Today)
        {
            MessageBox.Show("A data de fim não pode ser superior à data atual.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            endDatePicker.Value = DateTime.Today;
            return false;
        }
        
        // Validar data fim >= data início
        if (endDate < startDate)
        {
            MessageBox.Show("A data de fim não pode ser anterior à data de início.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            endDatePicker.Value = startDate;
            return false;
        }
        
        // Validar mesmo ano fiscal
        if (!SAFTDate.AreDatesInSameFiscalYear(startDate, endDate))
        {
            MessageBox.Show("As datas de início e fim devem estar no mesmo ano fiscal.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            endDatePicker.Value = startDate;
            return false;
        }
        
        // Validar data fim dentro do intervalo fiscal
        if (!SAFTDate.IsDateInFiscalRange(endDate, _fiscalYearInfo))
        {
            MessageBox.Show($"A data de fim deve estar dentro do ano fiscal {_fiscalYearInfo.FiscalYear}.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            endDatePicker.Value = startDate;
            return false;
        }
        
        return true;
    }
    
    private void ChkIntegratedSecurity_CheckedChanged(object sender, EventArgs e)
    {
        // Habilitar/desabilitar campos de usuário e senha
        txtUsername.Enabled = !chkIntegratedSecurity.Checked;
        txtPassword.Enabled = !chkIntegratedSecurity.Checked;
    }
    
    private void StartDatePicker_ValueChanged(object sender, EventArgs e)
    {
        // Atualizar ano fiscal quando data início muda
        UpdateFiscalYear();
        
        // Validar data início
        if (startDatePicker.Value > DateTime.Today)
        {
            MessageBox.Show("A data de início não pode ser superior à data atual.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            startDatePicker.Value = DateTime.Today;
        }
    }
    
    private void EndDatePicker_ValueChanged(object sender, EventArgs e)
    {
        // Validar data fim
        if (endDatePicker.Value > DateTime.Today)
        {
            MessageBox.Show("A data de fim não pode ser superior à data atual.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            endDatePicker.Value = DateTime.Today;
            return;
        }
        
        // Validar se está no mesmo ano fiscal
        if (endDatePicker.Value.Year != startDatePicker.Value.Year)
        {
            MessageBox.Show("A data de fim deve estar no mesmo ano fiscal que a data de início.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            endDatePicker.Value = startDatePicker.Value;
            return;
        }
        
        // Validar se está dentro do intervalo fiscal
        if (!SAFTDate.IsDateInFiscalRange(endDatePicker.Value, _fiscalYearInfo))
        {
            MessageBox.Show($"A data de fim deve estar dentro do ano fiscal {_fiscalYearInfo.FiscalYear}.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            endDatePicker.Value = startDatePicker.Value;
        }
    }
}
