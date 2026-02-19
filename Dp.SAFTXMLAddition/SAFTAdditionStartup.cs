using System;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;
using Dominos.Core;
using Dominos.LogForPulse;
using Dominos.Pulse;
using Dominos.PulseUI;

namespace Dp.SAFTXMLAddition
{
	// Token: 0x0200001C RID: 28
	public class SAFTAdditionStartup
	{
		// Token: 0x0600011B RID: 283 RVA: 0x00004404 File Offset: 0x00002604
		public static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			SAFTAdditionStartup._pulseContext.Language.GetText(SAFTAdditionStartup.UnhandledExceptionInSaftDetailsMessage);
			MessageBox.Show(e.Exception.Message, SAFTAdditionStartup._pulseContext.Language.GetText(SAFTAdditionStartup.ApplicationFailureMessage), MessageBoxButtons.OK, MessageBoxIcon.Hand);
			Logger.Error(SAFTAdditionStartup._pulseContext.Language.GetText(SAFTAdditionStartup.ApplicationFailureInSaftStartupMessage), e.Exception);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004470 File Offset: 0x00002670
		public static bool Login()
		{
			bool flag = SAFTAdditionStartup.PulseUIContext().Login();
			if (flag)
			{
				bool flag2 = SAFTAdditionStartup.PulseContext().User.IsAuthorized(SAFTAdditionStartup.SaftApplicationKey);
				if (flag2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000044B0 File Offset: 0x000026B0
		[STAThread]
		public static void Main()
		{
			ApplicationActivate applicationActivate = new ApplicationActivate();
			Application.ThreadException += SAFTAdditionStartup.Application_ThreadException;
			try
			{
				SqlConnection sqlConnection = PulseConnection.CreateSQLConnection();
				sqlConnection.Close();
			}
			catch (ApplicationException ex)
			{
				Logger.Error(SAFTAdditionStartup._pulseContext.Language.GetText(SAFTAdditionStartup.ApplicationExceptionInSaftStartupMessage), ex);
				MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace, SAFTAdditionStartup._pulseContext.Language.GetText(SAFTAdditionStartup.FailedMessage), MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			try
			{
				SystemSettings.Initialize(SAFTAdditionStartup.PulseContext());
				SAFTAdditionStartup.PulseUIContext().ShowSplashScreen();
				bool flag = applicationActivate.ContinueOrClose(ApplicationActivate.ContinuationRequest.SwitchToAnExistingProcess) == ApplicationActivate.ContinuationResponse.Continue;
				if (flag)
				{
					bool flag2 = SAFTAdditionStartup.Login();
					if (flag2)
					{
						string empty = string.Empty;
						bool flag3 = empty.Length > 0;
						if (flag3)
						{
							MessageBox.Show(string.Format(SystemSettings.PulseContext.Language.GetText(SAFTAdditionStartup.FailedMessage), "\r\n", "\t", empty), SystemSettings.PulseContext.Language.GetText(SAFTAdditionStartup.SaftFileGeneratorMessage), MessageBoxButtons.OK, MessageBoxIcon.Hand);
							bool messageLoop = Application.MessageLoop;
							if (messageLoop)
							{
								Application.Exit();
							}
							else
							{
								Environment.Exit(0);
							}
						}
						SAFTAdditionStartup.Startup();
					}
					else
					{
						Application.Exit();
					}
				}
				else
				{
					Application.Exit();
					Application.ExitThread();
				}
			}
			catch (Exception ex2)
			{
				Logger.Error(SAFTAdditionStartup._pulseContext.Language.GetText(SAFTAdditionStartup.UnhandledExceptionInSaftStartupMessage), ex2);
				MessageBox.Show(ex2.Message + "\n\r" + ex2.StackTrace, SAFTAdditionStartup._pulseContext.Language.GetText(SAFTAdditionStartup.StartupFailureMessage), MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004684 File Offset: 0x00002884
		public static PulseContext PulseContext()
		{
			bool flag = SAFTAdditionStartup._pulseContext == null;
			if (flag)
			{
				SAFTAdditionStartup._pulseContext = PulseStartUp.StartUp();
			}
			Logger.Writing += SAFTAdditionStartup.HandleLog;
			return SAFTAdditionStartup._pulseContext;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000046C8 File Offset: 0x000028C8
		public static PulseUiContext PulseUIContext()
		{
			bool flag = SAFTAdditionStartup._pulseUiContext == null;
			if (flag)
			{
				SAFTAdditionStartup._pulseUiContext = new PulseUiContext(SAFTAdditionStartup.PulseContext(), Application.ProductName);
			}
			return SAFTAdditionStartup._pulseUiContext;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004704 File Offset: 0x00002904
		public static void HandleLog(object sender, EventArgs e)
		{
			string text = "Failed to log to errors database in SAFT.";
			try
			{
				LoggerEventArgs loggerEventArgs = (LoggerEventArgs)e;
				bool flag = loggerEventArgs.LogEntry.Text.Contains(text);
				if (!flag)
				{
					bool flag2 = loggerEventArgs.LogEntry.EntryType == LogEntryType.Error;
					if (flag2)
					{
						Log.Error(loggerEventArgs.LogEntry.Text, loggerEventArgs.ErrorException);
					}
				}
			}
			catch (Exception e2)
			{
				Logger.Error(text, e2);
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004788 File Offset: 0x00002988
		private static void Startup()
		{
			try
			{
				Application.EnableVisualStyles();
				SAFTAdditionForm saftadditionForm = new SAFTAdditionForm(SAFTAdditionStartup.PulseContext(), SAFTAdditionStartup.PulseUIContext());
				SAFTAdditionStartup.PulseUIContext().HideSplashScreen();
				saftadditionForm.Show();
				Application.Run(saftadditionForm);
			}
			catch (Exception ex)
			{
				Logger.Error(SAFTAdditionStartup._pulseContext.Language.GetText(SAFTAdditionStartup.UnhandledExceptionInSaftStartupMessage), ex);
				MessageBox.Show(ex.Message + "\n\r" + ex.StackTrace, SAFTAdditionStartup._pulseContext.Language.GetText(SAFTAdditionStartup.StartupFailureMessage), MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x040000BB RID: 187
		private static readonly int ApplicationExceptionInSaftStartupMessage = 51874;

		// Token: 0x040000BC RID: 188
		private static readonly int ApplicationFailureInSaftStartupMessage = 51878;

		// Token: 0x040000BD RID: 189
		private static readonly int ApplicationFailureMessage = 51877;

		// Token: 0x040000BE RID: 190
		private static readonly int FailedMessage = 1126;

		// Token: 0x040000BF RID: 191
		private static readonly int SaftFileGeneratorMessage = 51891;

		// Token: 0x040000C0 RID: 192
		private static readonly int SaftApplicationKey = 60010;

		// Token: 0x040000C1 RID: 193
		private static readonly int StartupFailureMessage = 51876;

		// Token: 0x040000C2 RID: 194
		private static readonly int UnhandledExceptionInSaftDetailsMessage = 51879;

		// Token: 0x040000C3 RID: 195
		private static readonly int UnhandledExceptionInSaftStartupMessage = 51875;

		// Token: 0x040000C4 RID: 196
		private static PulseContext _pulseContext;

		// Token: 0x040000C5 RID: 197
		private static PulseUiContext _pulseUiContext;
	}
}
