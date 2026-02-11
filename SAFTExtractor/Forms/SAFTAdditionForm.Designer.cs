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
        this.grpSAFT = new System.Windows.Forms.GroupBox();
        this.lblStartDate = new System.Windows.Forms.Label();
        this.lblEndDate = new System.Windows.Forms.Label();
        this.startDatePicker = new System.Windows.Forms.DateTimePicker();
        this.endDatePicker = new System.Windows.Forms.DateTimePicker();
        this.lblFiscalYear = new System.Windows.Forms.Label();
        this.txtFiscalYear = new System.Windows.Forms.TextBox();
        this.btnGenerate = new System.Windows.Forms.Button();
        this.lblStatus = new System.Windows.Forms.Label();
        this.grpSAFT.SuspendLayout();
        this.SuspendLayout();
        // 
        // grpSAFT
        // 
        this.grpSAFT.Controls.Add(this.btnGenerate);
        this.grpSAFT.Controls.Add(this.txtFiscalYear);
        this.grpSAFT.Controls.Add(this.lblFiscalYear);
        this.grpSAFT.Controls.Add(this.endDatePicker);
        this.grpSAFT.Controls.Add(this.startDatePicker);
        this.grpSAFT.Controls.Add(this.lblEndDate);
        this.grpSAFT.Controls.Add(this.lblStartDate);
        this.grpSAFT.Location = new System.Drawing.Point(12, 12);
        this.grpSAFT.Name = "grpSAFT";
        this.grpSAFT.Size = new System.Drawing.Size(560, 180);
        this.grpSAFT.TabIndex = 0;
        this.grpSAFT.TabStop = false;
        this.grpSAFT.Text = "Geração de Ficheiro SAFT - PULSE DOMINOS";
        // 
        // lblStartDate
        // 
        this.lblStartDate.AutoSize = true;
        this.lblStartDate.Location = new System.Drawing.Point(20, 40);
        this.lblStartDate.Name = "lblStartDate";
        this.lblStartDate.Size = new System.Drawing.Size(83, 20);
        this.lblStartDate.TabIndex = 0;
        this.lblStartDate.Text = "Data Início:";
        // 
        // lblEndDate
        // 
        this.lblEndDate.AutoSize = true;
        this.lblEndDate.Location = new System.Drawing.Point(20, 75);
        this.lblEndDate.Name = "lblEndDate";
        this.lblEndDate.Size = new System.Drawing.Size(71, 20);
        this.lblEndDate.TabIndex = 0;
        this.lblEndDate.Text = "Data Fim:";
        // 
        // startDatePicker
        // 
        this.startDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
        this.startDatePicker.Location = new System.Drawing.Point(140, 35);
        this.startDatePicker.Name = "startDatePicker";
        this.startDatePicker.Size = new System.Drawing.Size(200, 27);
        this.startDatePicker.TabIndex = 0;
        this.startDatePicker.ValueChanged += new System.EventHandler(this.StartDatePicker_ValueChanged);
        // 
        // endDatePicker
        // 
        this.endDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
        this.endDatePicker.Location = new System.Drawing.Point(140, 70);
        this.endDatePicker.Name = "endDatePicker";
        this.endDatePicker.Size = new System.Drawing.Size(200, 27);
        this.endDatePicker.TabIndex = 1;
        this.endDatePicker.ValueChanged += new System.EventHandler(this.EndDatePicker_ValueChanged);
        // 
        // lblFiscalYear
        // 
        this.lblFiscalYear.AutoSize = true;
        this.lblFiscalYear.Location = new System.Drawing.Point(20, 110);
        this.lblFiscalYear.Name = "lblFiscalYear";
        this.lblFiscalYear.Size = new System.Drawing.Size(78, 20);
        this.lblFiscalYear.TabIndex = 0;
        this.lblFiscalYear.Text = "Ano Fiscal:";
        // 
        // txtFiscalYear
        // 
        this.txtFiscalYear.Location = new System.Drawing.Point(140, 105);
        this.txtFiscalYear.Name = "txtFiscalYear";
        this.txtFiscalYear.ReadOnly = true;
        this.txtFiscalYear.Size = new System.Drawing.Size(100, 27);
        this.txtFiscalYear.TabIndex = 2;
        this.txtFiscalYear.TabStop = false;
        // 
        // btnGenerate
        // 
        this.btnGenerate.Location = new System.Drawing.Point(140, 140);
        this.btnGenerate.Name = "btnGenerate";
        this.btnGenerate.Size = new System.Drawing.Size(200, 35);
        this.btnGenerate.TabIndex = 3;
        this.btnGenerate.Text = "Gerar Ficheiro SAFT";
        this.btnGenerate.UseVisualStyleBackColor = true;
        this.btnGenerate.Click += new System.EventHandler(this.BtnGenerate_Click);
        // 
        // lblStatus
        // 
        this.lblStatus.AutoSize = true;
        this.lblStatus.Location = new System.Drawing.Point(22, 205);
        this.lblStatus.Name = "lblStatus";
        this.lblStatus.Size = new System.Drawing.Size(59, 20);
        this.lblStatus.TabIndex = 1;
        this.lblStatus.Text = "Pronto.";
        // 
        // SAFTAdditionForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(584, 241);
        this.Controls.Add(this.lblStatus);
        this.Controls.Add(this.grpSAFT);
        this.Name = "SAFTAdditionForm";
        this.Text = "SAFTExtractor - PULSE DOMINOS";
        this.grpSAFT.ResumeLayout(false);
        this.grpSAFT.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private System.Windows.Forms.GroupBox grpSAFT;
    private System.Windows.Forms.Label lblStartDate;
    private System.Windows.Forms.Label lblEndDate;
    private System.Windows.Forms.DateTimePicker startDatePicker;
    private System.Windows.Forms.DateTimePicker endDatePicker;
    private System.Windows.Forms.Label lblFiscalYear;
    private System.Windows.Forms.TextBox txtFiscalYear;
    private System.Windows.Forms.Button btnGenerate;
    private System.Windows.Forms.Label lblStatus;
}
