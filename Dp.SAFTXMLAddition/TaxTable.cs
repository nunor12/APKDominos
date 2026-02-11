using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

// Token: 0x02000018 RID: 24
[GeneratedCode("xsd", "4.0.30319.1")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01")]
[XmlRoot(Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01", IsNullable = false)]
[Serializable]
public class TaxTable
{
	// Token: 0x17000074 RID: 116
	// (get) Token: 0x060000FA RID: 250 RVA: 0x00003098 File Offset: 0x00001298
	// (set) Token: 0x060000FB RID: 251 RVA: 0x000030B0 File Offset: 0x000012B0
	[XmlElement("TaxTableEntry")]
	public TaxTableEntry[] TaxTableEntry
	{
		get
		{
			return this.taxTableEntryField;
		}
		set
		{
			this.taxTableEntryField = value;
		}
	}

	// Token: 0x04000088 RID: 136
	private TaxTableEntry[] taxTableEntryField;
}
