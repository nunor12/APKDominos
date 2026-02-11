using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

// Token: 0x02000010 RID: 16
[GeneratedCode("xsd", "4.0.30319.1")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01")]
[XmlRoot(Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01", IsNullable = false)]
[Serializable]
public class SourceDocuments
{
	// Token: 0x17000046 RID: 70
	// (get) Token: 0x06000097 RID: 151 RVA: 0x00002A20 File Offset: 0x00000C20
	// (set) Token: 0x06000098 RID: 152 RVA: 0x00002A38 File Offset: 0x00000C38
	public SourceDocumentsSalesInvoices SalesInvoices
	{
		get
		{
			return this.salesInvoicesField;
		}
		set
		{
			this.salesInvoicesField = value;
		}
	}

	// Token: 0x04000057 RID: 87
	private SourceDocumentsSalesInvoices salesInvoicesField;
}
