namespace Dp.SAFTXMLAddition
{
	// Token: 0x0200001B RID: 27
	public partial class SAFTAdditionForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000118 RID: 280 RVA: 0x00003A04 File Offset: 0x00001C04
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00003A3C File Offset: 0x00001C3C
		private void InitializeComponent()
		{
			this.btnBrowse = new global::System.Windows.Forms.Button();
			this._folderBrowserDialog = new global::System.Windows.Forms.FolderBrowserDialog();
			this.txtDestinationFolder = new global::System.Windows.Forms.TextBox();
			this.lblDestinationFolder = new global::System.Windows.Forms.Label();
			this.headerData = new global::System.Windows.Forms.RichTextBox();
			this.lblHeader = new global::System.Windows.Forms.Label();
			this.lblStartDate = new global::System.Windows.Forms.Label();
			this.lblEndDate = new global::System.Windows.Forms.Label();
			this.lblFiscalYear = new global::System.Windows.Forms.Label();
			this.txtFiscalYear = new global::System.Windows.Forms.TextBox();
			this.endDatePicker = new global::System.Windows.Forms.DateTimePicker();
			this.startDatePicker = new global::System.Windows.Forms.DateTimePicker();
			this.btnGenerateSaft = new global::System.Windows.Forms.Button();
			this.btnCancel = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.btnBrowse.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btnBrowse.Location = new global::System.Drawing.Point(680, 138);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new global::System.Drawing.Size(102, 25);
			this.btnBrowse.TabIndex = 3;
			this.btnBrowse.Text = "Browse";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new global::System.EventHandler(this.btnBrowse_Click);
			this._folderBrowserDialog.ShowNewFolderButton = false;
			this.txtDestinationFolder.Location = new global::System.Drawing.Point(405, 143);
			this.txtDestinationFolder.Name = "txtDestinationFolder";
			this.txtDestinationFolder.ReadOnly = true;
			this.txtDestinationFolder.Size = new global::System.Drawing.Size(263, 20);
			this.txtDestinationFolder.TabIndex = 13;
			this.lblDestinationFolder.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblDestinationFolder.Location = new global::System.Drawing.Point(288, 146);
			this.lblDestinationFolder.Name = "lblDestinationFolder";
			this.lblDestinationFolder.Size = new global::System.Drawing.Size(110, 35);
			this.lblDestinationFolder.TabIndex = 10;
			this.lblDestinationFolder.Text = "Destination Folder";
			this.headerData.Location = new global::System.Drawing.Point(14, 37);
			this.headerData.Name = "headerData";
			this.headerData.ReadOnly = true;
			this.headerData.Size = new global::System.Drawing.Size(240, 290);
			this.headerData.TabIndex = 11;
			this.headerData.Text = "";
			this.headerData.WordWrap = false;
			this.lblHeader.AutoSize = true;
			this.lblHeader.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblHeader.Location = new global::System.Drawing.Point(12, 17);
			this.lblHeader.Name = "lblHeader";
			this.lblHeader.Size = new global::System.Drawing.Size(48, 13);
			this.lblHeader.TabIndex = 6;
			this.lblHeader.Text = "Header";
			this.lblStartDate.AutoSize = true;
			this.lblStartDate.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblStartDate.Location = new global::System.Drawing.Point(288, 64);
			this.lblStartDate.Name = "lblStartDate";
			this.lblStartDate.Size = new global::System.Drawing.Size(61, 13);
			this.lblStartDate.TabIndex = 7;
			this.lblStartDate.Text = "StartDate";
			this.lblEndDate.AutoSize = true;
			this.lblEndDate.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblEndDate.Location = new global::System.Drawing.Point(559, 64);
			this.lblEndDate.Name = "lblEndDate";
			this.lblEndDate.Size = new global::System.Drawing.Size(56, 13);
			this.lblEndDate.TabIndex = 8;
			this.lblEndDate.Text = "EndDate";
			this.lblFiscalYear.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblFiscalYear.Location = new global::System.Drawing.Point(288, 107);
			this.lblFiscalYear.Name = "lblFiscalYear";
			this.lblFiscalYear.Size = new global::System.Drawing.Size(98, 27);
			this.lblFiscalYear.TabIndex = 9;
			this.lblFiscalYear.Text = "FiscalYear";
			this.txtFiscalYear.Location = new global::System.Drawing.Point(405, 104);
			this.txtFiscalYear.Name = "txtFiscalYear";
			this.txtFiscalYear.ReadOnly = true;
			this.txtFiscalYear.Size = new global::System.Drawing.Size(107, 20);
			this.txtFiscalYear.TabIndex = 12;
			this.txtFiscalYear.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.endDatePicker.CustomFormat = "''";
			this.endDatePicker.Format = global::System.Windows.Forms.DateTimePickerFormat.Custom;
			this.endDatePicker.Location = new global::System.Drawing.Point(668, 57);
			this.endDatePicker.Name = "endDatePicker";
			this.endDatePicker.Size = new global::System.Drawing.Size(108, 20);
			this.endDatePicker.TabIndex = 2;
			this.endDatePicker.CloseUp += new global::System.EventHandler(this.EndDatePicker_ValueChanged);
			this.startDatePicker.CustomFormat = "''";
			this.startDatePicker.Format = global::System.Windows.Forms.DateTimePickerFormat.Custom;
			this.startDatePicker.Location = new global::System.Drawing.Point(405, 57);
			this.startDatePicker.Name = "startDatePicker";
			this.startDatePicker.Size = new global::System.Drawing.Size(107, 20);
			this.startDatePicker.TabIndex = 1;
			this.startDatePicker.CloseUp += new global::System.EventHandler(this.StartDatePicker_ValueChanged);
			this.btnGenerateSaft.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btnGenerateSaft.Location = new global::System.Drawing.Point(394, 223);
			this.btnGenerateSaft.Name = "btnGenerateSaft";
			this.btnGenerateSaft.Size = new global::System.Drawing.Size(139, 23);
			this.btnGenerateSaft.TabIndex = 4;
			this.btnGenerateSaft.Text = "Generate SAFT";
			this.btnGenerateSaft.UseVisualStyleBackColor = true;
			this.btnGenerateSaft.Click += new global::System.EventHandler(this.btnGenerateSaft_Click);
			this.btnCancel.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btnCancel.Location = new global::System.Drawing.Point(562, 223);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new global::System.Drawing.Size(136, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new global::System.EventHandler(this.btnCancel_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(794, 339);
			base.Controls.Add(this.lblEndDate);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnGenerateSaft);
			base.Controls.Add(this.startDatePicker);
			base.Controls.Add(this.endDatePicker);
			base.Controls.Add(this.txtFiscalYear);
			base.Controls.Add(this.lblFiscalYear);
			base.Controls.Add(this.lblStartDate);
			base.Controls.Add(this.lblHeader);
			base.Controls.Add(this.headerData);
			base.Controls.Add(this.lblDestinationFolder);
			base.Controls.Add(this.txtDestinationFolder);
			base.Controls.Add(this.btnBrowse);
			this.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Name = "SAFTAdditionForm";
			this.Text = "SAF-TFileGenerator";
			base.Shown += new global::System.EventHandler(this.SAFTAdditionForm_Shown);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000A6 RID: 166
		private global::System.Windows.Forms.FolderBrowserDialog _folderBrowserDialog;

		// Token: 0x040000AD RID: 173
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x040000AE RID: 174
		private global::System.Windows.Forms.Button btnBrowse;

		// Token: 0x040000AF RID: 175
		private global::System.Windows.Forms.TextBox txtDestinationFolder;

		// Token: 0x040000B0 RID: 176
		private global::System.Windows.Forms.Label lblDestinationFolder;

		// Token: 0x040000B1 RID: 177
		private global::System.Windows.Forms.RichTextBox headerData;

		// Token: 0x040000B2 RID: 178
		private global::System.Windows.Forms.Label lblHeader;

		// Token: 0x040000B3 RID: 179
		private global::System.Windows.Forms.Label lblStartDate;

		// Token: 0x040000B4 RID: 180
		private global::System.Windows.Forms.Label lblEndDate;

		// Token: 0x040000B5 RID: 181
		private global::System.Windows.Forms.Label lblFiscalYear;

		// Token: 0x040000B6 RID: 182
		private global::System.Windows.Forms.TextBox txtFiscalYear;

		// Token: 0x040000B7 RID: 183
		private global::System.Windows.Forms.DateTimePicker endDatePicker;

		// Token: 0x040000B8 RID: 184
		private global::System.Windows.Forms.DateTimePicker startDatePicker;

		// Token: 0x040000B9 RID: 185
		private global::System.Windows.Forms.Button btnGenerateSaft;

		// Token: 0x040000BA RID: 186
		private global::System.Windows.Forms.Button btnCancel;
	}
}
