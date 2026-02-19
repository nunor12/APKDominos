using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

// Token: 0x02000005 RID: 5
[GeneratedCode("xsd", "4.0.30319.1")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01")]
[Serializable]
public class References
{
	// Token: 0x1700001C RID: 28
	// (get) Token: 0x0600003A RID: 58 RVA: 0x00002424 File Offset: 0x00000624
	// (set) Token: 0x0600003B RID: 59 RVA: 0x0000243C File Offset: 0x0000063C
	public string Reference
	{
		get
		{
			return this.referenceField;
		}
		set
		{
			this.referenceField = value;
		}
	}

	// Token: 0x0400001C RID: 28
	private string referenceField;
}
