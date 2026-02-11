namespace SAFTExtractor.Forms;

partial class SAFTAdditionForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.grpConnection = new System.Windows.Forms.GroupBox();
        this.chkIntegratedSecurity = new System.Windows.Forms.CheckBox();
        this.txtPassword = new System.Windows.Forms.TextBox();
        this.txtUsername = new System.Windows.Forms.TextBox();
        this.txtDatabase = new System.Windows.Forms.TextBox();
        this.txtServer = new System.Windows.Forms.TextBox();
        this.lblPassword = new System.Windows.Forms.Label();
        this.lblUsername = new System.Windows.Forms.Label();
        this.lblDatabase = new System.Windows.Forms.Label();
        this.lblServer = new System.Windows.Forms.Label();
        this.btnTestConnection = new System.Windows.Forms.Button();
        this.grpSAFT = new System.Windows.Forms.GroupBox();
        this.numFiscalYear = new System.Windows.Forms.NumericUpDown();
        this.lblFiscalYear = new System.Windows.Forms.Label();
        this.btnGenerate = new System.Windows.Forms.Button();
        this.lblStatus = new System.Windows.Forms.Label();
        this.grpConnection.SuspendLayout();
        this.grpSAFT.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.numFiscalYear)).BeginInit();
        this.SuspendLayout();
        // 
        // grpConnection
        // 
        this.grpConnection.Controls.Add(this.btnTestConnection);
        this.grpConnection.Controls.Add(this.chkIntegratedSecurity);
        this.grpConnection.Controls.Add(this.txtPassword);
        this.grpConnection.Controls.Add(this.txtUsername);
        this.grpConnection.Controls.Add(this.txtDatabase);
        this.grpConnection.Controls.Add(this.txtServer);
        this.grpConnection.Controls.Add(this.lblPassword);
        this.grpConnection.Controls.Add(this.lblUsername);
        this.grpConnection.Controls.Add(this.lblDatabase);
        this.grpConnection.Controls.Add(this.lblServer);
        this.grpConnection.Location = new System.Drawing.Point(12, 12);
        this.grpConnection.Name = "grpConnection";
        this.grpConnection.Size = new System.Drawing.Size(760, 200);
        this.grpConnection.TabIndex = 0;
        this.grpConnection.TabStop = false;
        this.grpConnection.Text = "Conexão à Base de Dados PULSE DOMINOS";
        // 
        // chkIntegratedSecurity
        // 
        this.chkIntegratedSecurity.AutoSize = true;
        this.chkIntegratedSecurity.Location = new System.Drawing.Point(120, 130);
        this.chkIntegratedSecurity.Name = "chkIntegratedSecurity";
        this.chkIntegratedSecurity.Size = new System.Drawing.Size(180, 24);
        this.chkIntegratedSecurity.TabIndex = 4;
        this.chkIntegratedSecurity.Text = "Autenticação Windows";
        this.chkIntegratedSecurity.UseVisualStyleBackColor = true;
        this.chkIntegratedSecurity.CheckedChanged += new System.EventHandler(this.ChkIntegratedSecurity_CheckedChanged);
        // 
        // txtPassword
        // 
        this.txtPassword.Location = new System.Drawing.Point(120, 100);
        this.txtPassword.Name = "txtPassword";
        this.txtPassword.PasswordChar = '*';
        this.txtPassword.Size = new System.Drawing.Size(620, 27);
        this.txtPassword.TabIndex = 3;
        // 
        // txtUsername
        // 
        this.txtUsername.Location = new System.Drawing.Point(120, 70);
        this.txtUsername.Name = "txtUsername";
        this.txtUsername.Size = new System.Drawing.Size(620, 27);
        this.txtUsername.TabIndex = 2;
        // 
        // txtDatabase
        // 
        this.txtDatabase.Location = new System.Drawing.Point(120, 40);
        this.txtDatabase.Name = "txtDatabase";
        this.txtDatabase.Size = new System.Drawing.Size(620, 27);
        this.txtDatabase.TabIndex = 1;
        // 
        // txtServer
        // 
        this.txtServer.Location = new System.Drawing.Point(120, 10);
        this.txtServer.Name = "txtServer";
        this.txtServer.Size = new System.Drawing.Size(620, 27);
        this.txtServer.TabIndex = 0;
        // 
        // lblPassword
        // 
        this.lblPassword.AutoSize = true;
        this.lblPassword.Location = new System.Drawing.Point(10, 103);
        this.lblPassword.Name = "lblPassword";
        this.lblPassword.Size = new System.Drawing.Size(70, 20);
        this.lblPassword.TabIndex = 0;
        this.lblPassword.Text = "Password:";
        // 
        // lblUsername
        // 
        this.lblUsername.AutoSize = true;
        this.lblUsername.Location = new System.Drawing.Point(10, 73);
        this.lblUsername.Name = "lblUsername";
        this.lblUsername.Size = new System.Drawing.Size(73, 20);
        this.lblUsername.TabIndex = 0;
        this.lblUsername.Text = "Utilizador:";
        // 
        // lblDatabase
        // 
        this.lblDatabase.AutoSize = true;
        this.lblDatabase.Location = new System.Drawing.Point(10, 43);
        this.lblDatabase.Name = "lblDatabase";
        this.lblDatabase.Size = new System.Drawing.Size(101, 20);
        this.lblDatabase.TabIndex = 0;
        this.lblDatabase.Text = "Base de Dados:";
        // 
        // lblServer
        // 
        this.lblServer.AutoSize = true;
        this.lblServer.Location = new System.Drawing.Point(10, 13);
        this.lblServer.Name = "lblServer";
        this.lblServer.Size = new System.Drawing.Size(65, 20);
        this.lblServer.TabIndex = 0;
        this.lblServer.Text = "Servidor:";
        // 
        // btnTestConnection
        // 
        this.btnTestConnection.Location = new System.Drawing.Point(120, 160);
        this.btnTestConnection.Name = "btnTestConnection";
        this.btnTestConnection.Size = new System.Drawing.Size(200, 30);
        this.btnTestConnection.TabIndex = 5;
        this.btnTestConnection.Text = "Testar Conexão";
        this.btnTestConnection.UseVisualStyleBackColor = true;
        this.btnTestConnection.Click += new System.EventHandler(this.BtnTestConnection_Click);
        // 
        // grpSAFT
        // 
        this.grpSAFT.Controls.Add(this.btnGenerate);
        this.grpSAFT.Controls.Add(this.lblFiscalYear);
        this.grpSAFT.Controls.Add(this.numFiscalYear);
        this.grpSAFT.Location = new System.Drawing.Point(12, 220);
        this.grpSAFT.Name = "grpSAFT";
        this.grpSAFT.Size = new System.Drawing.Size(760, 100);
        this.grpSAFT.TabIndex = 1;
        this.grpSAFT.TabStop = false;
        this.grpSAFT.Text = "Geração de Ficheiro SAFT";
        // 
        // numFiscalYear
        // 
        this.numFiscalYear.Location = new System.Drawing.Point(120, 30);
        this.numFiscalYear.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
        this.numFiscalYear.Minimum = new decimal(new int[] { 2000, 0, 0, 0 });
        this.numFiscalYear.Name = "numFiscalYear";
        this.numFiscalYear.Size = new System.Drawing.Size(200, 27);
        this.numFiscalYear.TabIndex = 0;
        this.numFiscalYear.Value = new decimal(new int[] { 2024, 0, 0, 0 });
        // 
        // lblFiscalYear
        // 
        this.lblFiscalYear.AutoSize = true;
        this.lblFiscalYear.Location = new System.Drawing.Point(10, 32);
        this.lblFiscalYear.Name = "lblFiscalYear";
        this.lblFiscalYear.Size = new System.Drawing.Size(78, 20);
        this.lblFiscalYear.TabIndex = 0;
        this.lblFiscalYear.Text = "Ano Fiscal:";
        // 
        // btnGenerate
        // 
        this.btnGenerate.Location = new System.Drawing.Point(120, 60);
        this.btnGenerate.Name = "btnGenerate";
        this.btnGenerate.Size = new System.Drawing.Size(200, 30);
        this.btnGenerate.TabIndex = 1;
        this.btnGenerate.Text = "Gerar Ficheiro SAFT";
        this.btnGenerate.UseVisualStyleBackColor = true;
        this.btnGenerate.Click += new System.EventHandler(this.BtnGenerate_Click);
        // 
        // lblStatus
        // 
        this.lblStatus.AutoSize = true;
        this.lblStatus.Location = new System.Drawing.Point(22, 330);
        this.lblStatus.Name = "lblStatus";
        this.lblStatus.Size = new System.Drawing.Size(59, 20);
        this.lblStatus.TabIndex = 2;
        this.lblStatus.Text = "Pronto.";
        // 
        // SAFTAdditionForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(784, 361);
        this.Controls.Add(this.lblStatus);
        this.Controls.Add(this.grpSAFT);
        this.Controls.Add(this.grpConnection);
        this.Name = "SAFTAdditionForm";
        this.Text = "SAFTExtractor";
        this.grpConnection.ResumeLayout(false);
        this.grpConnection.PerformLayout();
        this.grpSAFT.ResumeLayout(false);
        this.grpSAFT.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.numFiscalYear)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private System.Windows.Forms.GroupBox grpConnection;
    private System.Windows.Forms.TextBox txtServer;
    private System.Windows.Forms.TextBox txtDatabase;
    private System.Windows.Forms.TextBox txtUsername;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.Label lblServer;
    private System.Windows.Forms.Label lblDatabase;
    private System.Windows.Forms.Label lblUsername;
    private System.Windows.Forms.Label lblPassword;
    private System.Windows.Forms.CheckBox chkIntegratedSecurity;
    private System.Windows.Forms.Button btnTestConnection;
    private System.Windows.Forms.GroupBox grpSAFT;
    private System.Windows.Forms.NumericUpDown numFiscalYear;
    private System.Windows.Forms.Label lblFiscalYear;
    private System.Windows.Forms.Button btnGenerate;
    private System.Windows.Forms.Label lblStatus;
}
