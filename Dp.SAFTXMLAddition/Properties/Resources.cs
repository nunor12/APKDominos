using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Dp.SAFTXMLAddition.Properties
{
	// Token: 0x02000023 RID: 35
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000167 RID: 359 RVA: 0x00004893 File Offset: 0x00002A93
		internal Resources()
		{
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000871C File Offset: 0x0000691C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				bool flag = Resources.resourceMan == null;
				if (flag)
				{
					ResourceManager resourceManager = new ResourceManager("Dp.SAFTXMLAddition.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00008764 File Offset: 0x00006964
		// (set) Token: 0x0600016A RID: 362 RVA: 0x0000877B File Offset: 0x0000697B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x040000FF RID: 255
		private static ResourceManager resourceMan;

		// Token: 0x04000100 RID: 256
		private static CultureInfo resourceCulture;
	}
}
