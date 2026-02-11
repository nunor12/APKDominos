using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

// Token: 0x0200000B RID: 11
[GeneratedCode("xsd", "4.0.30319.1")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01")]
[Serializable]
public class AuditFileMasterFiles
{
	// Token: 0x17000028 RID: 40
	// (get) Token: 0x06000056 RID: 86 RVA: 0x000025D4 File Offset: 0x000007D4
	// (set) Token: 0x06000057 RID: 87 RVA: 0x000025EC File Offset: 0x000007EC
	[XmlElement("Customer")]
	public Customer[] Customer
	{
		get
		{
			return this.customerField;
		}
		set
		{
			this.customerField = value;
		}
	}

	// Token: 0x17000029 RID: 41
	// (get) Token: 0x06000058 RID: 88 RVA: 0x000025F8 File Offset: 0x000007F8
	// (set) Token: 0x06000059 RID: 89 RVA: 0x00002610 File Offset: 0x00000810
	[XmlElement("Product")]
	public Product[] Product
	{
		get
		{
			return this.productField;
		}
		set
		{
			this.productField = value;
		}
	}

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x0600005A RID: 90 RVA: 0x0000261C File Offset: 0x0000081C
	// (set) Token: 0x0600005B RID: 91 RVA: 0x00002634 File Offset: 0x00000834
	[XmlArrayItem("TaxTableEntry", IsNullable = false)]
	public TaxTableEntry[] TaxTable
	{
		get
		{
			return this.taxTableField;
		}
		set
		{
			this.taxTableField = value;
		}
	}

	// Token: 0x04000039 RID: 57
	private Customer[] customerField;

	// Token: 0x0400003A RID: 58
	private Product[] productField;

	// Token: 0x0400003B RID: 59
	private TaxTableEntry[] taxTableField;
}
