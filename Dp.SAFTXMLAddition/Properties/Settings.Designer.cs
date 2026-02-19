using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace Dp.SAFTXMLAddition.Properties
{
	// Token: 0x02000024 RID: 36
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00008784 File Offset: 0x00006984
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x04000101 RID: 257
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
