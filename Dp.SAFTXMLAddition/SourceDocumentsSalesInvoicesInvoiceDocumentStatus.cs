using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

// Token: 0x02000013 RID: 19
[GeneratedCode("xsd", "4.0.30319.1")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01")]
[Serializable]
public class SourceDocumentsSalesInvoicesInvoiceDocumentStatus
{
	// Token: 0x1700005D RID: 93
	// (get) Token: 0x060000C8 RID: 200 RVA: 0x00002D5C File Offset: 0x00000F5C
	// (set) Token: 0x060000C9 RID: 201 RVA: 0x00002D74 File Offset: 0x00000F74
	public string InvoiceStatus
	{
		get
		{
			return this.invoiceStatusField;
		}
		set
		{
			this.invoiceStatusField = value;
		}
	}

	// Token: 0x1700005E RID: 94
	// (get) Token: 0x060000CA RID: 202 RVA: 0x00002D80 File Offset: 0x00000F80
	// (set) Token: 0x060000CB RID: 203 RVA: 0x00002D98 File Offset: 0x00000F98
	public DateTime InvoiceStatusDate
	{
		get
		{
			return this.invoiceStatusDateField;
		}
		set
		{
			this.invoiceStatusDateField = value;
		}
	}

	// Token: 0x1700005F RID: 95
	// (get) Token: 0x060000CC RID: 204 RVA: 0x00002DA4 File Offset: 0x00000FA4
	// (set) Token: 0x060000CD RID: 205 RVA: 0x00002DBC File Offset: 0x00000FBC
	public string SourceID
	{
		get
		{
			return this.sourceIDField;
		}
		set
		{
			this.sourceIDField = value;
		}
	}

	// Token: 0x17000060 RID: 96
	// (get) Token: 0x060000CE RID: 206 RVA: 0x00002DC8 File Offset: 0x00000FC8
	// (set) Token: 0x060000CF RID: 207 RVA: 0x00002DE0 File Offset: 0x00000FE0
	public string SourceBilling
	{
		get
		{
			return this.sourceBillingField;
		}
		set
		{
			this.sourceBillingField = value;
		}
	}

	// Token: 0x0400006E RID: 110
	private string invoiceStatusField;

	// Token: 0x0400006F RID: 111
	private DateTime invoiceStatusDateField;

	// Token: 0x04000070 RID: 112
	private string sourceIDField;

	// Token: 0x04000071 RID: 113
	private string sourceBillingField;
}
