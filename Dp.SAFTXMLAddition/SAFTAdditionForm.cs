using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Dominos.LogForPulse;
using Dominos.Pulse;
using Dominos.PulseUI;

namespace Dp.SAFTXMLAddition
{
	// Token: 0x0200001B RID: 27
	public partial class SAFTAdditionForm : Form
	{
		// Token: 0x06000109 RID: 265 RVA: 0x000031C4 File Offset: 0x000013C4
		public SAFTAdditionForm(PulseContext pulseContext, PulseUiContext pulseUiContext)
		{
			Control.CheckForIllegalCrossThreadCalls = false;
			Application.EnableVisualStyles();
			this._pulseContext = pulseContext;
			this._pulseUiContext = pulseUiContext;
			this.InitializeComponent();
			this.btnCancel.Text = this._pulseContext.Language.GetText(SAFTAdditionForm.CancelButtonText);
			this.btnBrowse.Text = this._pulseContext.Language.GetText(SAFTAdditionForm.BrowseButtonText);
			this.btnGenerateSaft.Text = this._pulseContext.Language.GetText(SAFTAdditionForm.GenerateButtonText);
			this.lblEndDate.Text = this._pulseContext.Language.GetText(SAFTAdditionForm.LabelEndDate);
			this.lblFiscalYear.Text = this._pulseContext.Language.GetText(SAFTAdditionForm.LabelFiscalYear);
			this.lblHeader.Text = this._pulseContext.Language.GetText(SAFTAdditionForm.LabelHeader);
			this.lblStartDate.Text = this._pulseContext.Language.GetText(SAFTAdditionForm.LabelStartDate);
			this.lblDestinationFolder.Text = this._pulseContext.Language.GetText(SAFTAdditionForm.LabelDestinationFolder);
			this.Text = this._pulseContext.Language.GetText(SAFTAdditionForm.SaftApplicationTitle);
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00003328 File Offset: 0x00001528
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00003340 File Offset: 0x00001540
		public bool IsValid
		{
			get
			{
				return this._isValid;
			}
			set
			{
				this._isValid = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600010C RID: 268 RVA: 0x0000334C File Offset: 0x0000154C
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00003364 File Offset: 0x00001564
		public DateTime MaxEndDate
		{
			get
			{
				return this._maxEndDate;
			}
			set
			{
				this._maxEndDate = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00003370 File Offset: 0x00001570
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00003388 File Offset: 0x00001588
		public DateTime MinEndDate
		{
			get
			{
				return this._minEndDate;
			}
			set
			{
				this._minEndDate = value;
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00003394 File Offset: 0x00001594
		private void btnBrowse_Click(object sender, EventArgs e)
		{
			this._folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
			DialogResult dialogResult = this._folderBrowserDialog.ShowDialog();
			bool flag = dialogResult == DialogResult.OK;
			if (flag)
			{
				this.txtDestinationFolder.Text = this._folderBrowserDialog.SelectedPath;
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000033DD File Offset: 0x000015DD
		private void btnCancel_Click(object sender, EventArgs e)
		{
			Application.Exit();
			Application.ExitThread();
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000033EC File Offset: 0x000015EC
		private void btnGenerateSaft_Click(object sender, EventArgs e)
		{
			try
			{
				SAFTXMLObject saftxmlobject = new SAFTXMLObject();
				DateTime value = this.startDatePicker.Value;
				DateTime value2 = this.endDatePicker.Value;
				bool flag = this.startDatePicker.Format == DateTimePickerFormat.Custom;
				if (flag)
				{
					MessageBox.Show(this._pulseContext.Language.GetText(SAFTAdditionForm.StartDateEmptyMessage), SystemSettings.PulseContext.Language.GetText(SAFTAdditionForm.DialogWindowInformationTitle));
				}
				else
				{
					bool flag2 = this.endDatePicker.Format == DateTimePickerFormat.Custom;
					if (flag2)
					{
						MessageBox.Show(this._pulseContext.Language.GetText(SAFTAdditionForm.EndDateEmptyMessage), SystemSettings.PulseContext.Language.GetText(SAFTAdditionForm.DialogWindowInformationTitle));
					}
					else
					{
						bool flag3 = string.IsNullOrEmpty(this.txtDestinationFolder.Text);
						if (flag3)
						{
							MessageBox.Show(this._pulseContext.Language.GetText(SAFTAdditionForm.DestinationFolderEmptyMessage), SystemSettings.PulseContext.Language.GetText(SAFTAdditionForm.DialogWindowInformationTitle));
						}
						else
						{
							DateTime value3 = this.startDatePicker.Value;
							DateTime value4 = this.endDatePicker.Value;
							bool flag4 = this.endDatePicker.Value < this.startDatePicker.Value;
							if (flag4)
							{
								MessageBox.Show(this._pulseContext.Language.GetText(SAFTAdditionForm.EndDateLesserThanStartDateMessage), SystemSettings.PulseContext.Language.GetText(SAFTAdditionForm.DialogWindowInformationTitle));
								this.endDatePicker.Value = this.startDatePicker.Value;
							}
							else
							{
								bool flag5 = this.endDatePicker.Value.Year > this.MaxEndDate.Year || this.endDatePicker.Value.Year < this.MinEndDate.Year || this.endDatePicker.Value < this.MinEndDate || this.endDatePicker.Value.Date > this.MaxEndDate.Date;
								if (flag5)
								{
									MessageBox.Show(this._pulseContext.Language.GetText(SAFTAdditionForm.DifferentFiscalYearMessage), SystemSettings.PulseContext.Language.GetText(SAFTAdditionForm.DialogWindowInformationTitle));
									this.endDatePicker.Value = this.startDatePicker.Value;
								}
								else
								{
									Cursor.Current = Cursors.WaitCursor;
									saftxmlobject.CreateSaftXmlFile(value.Date, value2.Date, this.txtDestinationFolder.Text + "\\");
									Cursor.Current = Cursors.Default;
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(this._pulseContext.Language.GetText(SAFTAdditionForm.UnhandledExceptionInSaftCreationMessage), ex);
				MessageBox.Show(ex.Message + "\n\r" + ex.StackTrace, this._pulseContext.Language.GetText(SAFTAdditionForm.SaftGenerationFailureMessage), MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00003714 File Offset: 0x00001914
		private void EndDatePicker_ValueChanged(object sender, EventArgs e)
		{
			this.endDatePicker.Format = DateTimePickerFormat.Short;
			bool flag = string.IsNullOrEmpty(this.txtFiscalYear.Text);
			if (flag)
			{
				this.endDatePicker.Format = DateTimePickerFormat.Custom;
				MessageBox.Show(this._pulseContext.Language.GetText(SAFTAdditionForm.StartDateRequiredMessage), SystemSettings.PulseContext.Language.GetText(SAFTAdditionForm.DialogWindowInformationTitle));
			}
			else
			{
				DateTime value = this.endDatePicker.Value;
				bool flag2 = this.endDatePicker.Value > DateTime.Now;
				if (flag2)
				{
					MessageBox.Show(this._pulseContext.Language.GetText(SAFTAdditionForm.EndDateGreaterThanCurrentDateMessage), SystemSettings.PulseContext.Language.GetText(SAFTAdditionForm.DialogWindowInformationTitle));
					this.endDatePicker.Value = DateTime.Today;
				}
				else
				{
					bool flag3 = this.endDatePicker.Value.Year > this.MaxEndDate.Year || this.endDatePicker.Value < this.MinEndDate || this.endDatePicker.Value.Date > this.MaxEndDate.Date;
					if (flag3)
					{
						MessageBox.Show(this._pulseContext.Language.GetText(SAFTAdditionForm.DifferentFiscalYearMessage), SystemSettings.PulseContext.Language.GetText(SAFTAdditionForm.DialogWindowInformationTitle));
						this.endDatePicker.Value = this.startDatePicker.Value;
					}
				}
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000038A2 File Offset: 0x00001AA2
		private void SAFTAdditionForm_Shown(object sender, EventArgs e)
		{
			this.ThreadWatcherGetHeaderDetails();
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000038AC File Offset: 0x00001AAC
		private void StartDatePicker_ValueChanged(object sender, EventArgs e)
		{
			this.startDatePicker.Format = DateTimePickerFormat.Short;
			SAFTXMLHeader saftxmlheader = new SAFTXMLHeader();
			DateTime value = this.startDatePicker.Value;
			bool flag = this.startDatePicker.Value > DateTime.Now;
			if (flag)
			{
				MessageBox.Show(this._pulseContext.Language.GetText(SAFTAdditionForm.StartDateGreaterThanCurrentDateMessage), SystemSettings.PulseContext.Language.GetText(SAFTAdditionForm.DialogWindowInformationTitle));
				this.startDatePicker.Value = DateTime.Today;
			}
			else
			{
				SAFTDate fiscalYear = saftxmlheader.GetFiscalYear(this.startDatePicker.Value.Date);
				this.txtFiscalYear.Text = fiscalYear.FiscalYear;
				this.MaxEndDate = fiscalYear.MaxEndDate;
				this.MinEndDate = fiscalYear.MinEndDate;
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00003980 File Offset: 0x00001B80
		private void ThreadWatcherGetHeaderDetails()
		{
			this._workerThread = new Thread(new ThreadStart(this.ThreadWorkerGetHeaderDetails));
			this._workerThread.Start();
			while (this._workerThread.IsAlive)
			{
				Thread.Sleep(50);
				Application.DoEvents();
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000039D4 File Offset: 0x00001BD4
		private void ThreadWorkerGetHeaderDetails()
		{
			SAFTXMLHeaderObject saftxmlheaderObject = new SAFTXMLHeaderObject();
			string text = string.Empty;
			text = saftxmlheaderObject.GetSAFTXMLHeaderDetails();
			this.headerData.Text = text;
		}

		// Token: 0x04000092 RID: 146
		private static readonly int BrowseButtonText = 51890;

		// Token: 0x04000093 RID: 147
		private static readonly int CancelButtonText = 51889;

		// Token: 0x04000094 RID: 148
		private static readonly int DestinationFolderEmptyMessage = 51865;

		// Token: 0x04000095 RID: 149
		private static readonly int DialogWindowInformationTitle = 310;

		// Token: 0x04000096 RID: 150
		private static readonly int DifferentFiscalYearMessage = 51867;

		// Token: 0x04000097 RID: 151
		private static readonly int EndDateEmptyMessage = 51864;

		// Token: 0x04000098 RID: 152
		private static readonly int EndDateGreaterThanCurrentDateMessage = 51872;

		// Token: 0x04000099 RID: 153
		private static readonly int EndDateLesserThanStartDateMessage = 51866;

		// Token: 0x0400009A RID: 154
		private static readonly int GenerateButtonText = 51888;

		// Token: 0x0400009B RID: 155
		private static readonly int LabelDestinationFolder = 51886;

		// Token: 0x0400009C RID: 156
		private static readonly int LabelEndDate = 51884;

		// Token: 0x0400009D RID: 157
		private static readonly int LabelFiscalYear = 51885;

		// Token: 0x0400009E RID: 158
		private static readonly int LabelHeader = 51887;

		// Token: 0x0400009F RID: 159
		private static readonly int LabelStartDate = 51883;

		// Token: 0x040000A0 RID: 160
		private static readonly int SaftApplicationTitle = 51891;

		// Token: 0x040000A1 RID: 161
		private static readonly int SaftGenerationFailureMessage = 51870;

		// Token: 0x040000A2 RID: 162
		private static readonly int StartDateEmptyMessage = 51863;

		// Token: 0x040000A3 RID: 163
		private static readonly int StartDateGreaterThanCurrentDateMessage = 51871;

		// Token: 0x040000A4 RID: 164
		private static readonly int StartDateRequiredMessage = 51873;

		// Token: 0x040000A5 RID: 165
		private static readonly int UnhandledExceptionInSaftCreationMessage = 51869;

		// Token: 0x040000A7 RID: 167
		private bool _isValid;

		// Token: 0x040000A8 RID: 168
		private DateTime _maxEndDate;

		// Token: 0x040000A9 RID: 169
		private DateTime _minEndDate;

		// Token: 0x040000AA RID: 170
		private PulseContext _pulseContext;

		// Token: 0x040000AB RID: 171
		private PulseUiContext _pulseUiContext;

		// Token: 0x040000AC RID: 172
		private Thread _workerThread;
	}
}
