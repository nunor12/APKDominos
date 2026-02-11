using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

// Token: 0x02000011 RID: 17
[GeneratedCode("xsd", "4.0.30319.1")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01")]
[Serializable]
public class SourceDocumentsSalesInvoices
{
	// Token: 0x17000047 RID: 71
	// (get) Token: 0x0600009A RID: 154 RVA: 0x00002A44 File Offset: 0x00000C44
	// (set) Token: 0x0600009B RID: 155 RVA: 0x00002A5C File Offset: 0x00000C5C
	[XmlElement(DataType = "nonNegativeInteger")]
	public string NumberOfEntries
	{
		get
		{
			return this.numberOfEntriesField;
		}
		set
		{
			this.numberOfEntriesField = value;
		}
	}

	// Token: 0x17000048 RID: 72
	// (get) Token: 0x0600009C RID: 156 RVA: 0x00002A68 File Offset: 0x00000C68
	// (set) Token: 0x0600009D RID: 157 RVA: 0x00002A80 File Offset: 0x00000C80
	public decimal TotalDebit
	{
		get
		{
			return this.totalDebitField;
		}
		set
		{
			this.totalDebitField = value;
		}
	}

	// Token: 0x17000049 RID: 73
	// (get) Token: 0x0600009E RID: 158 RVA: 0x00002A8C File Offset: 0x00000C8C
	// (set) Token: 0x0600009F RID: 159 RVA: 0x00002AA4 File Offset: 0x00000CA4
	public decimal TotalCredit
	{
		get
		{
			return this.totalCreditField;
		}
		set
		{
			this.totalCreditField = value;
		}
	}

	// Token: 0x1700004A RID: 74
	// (get) Token: 0x060000A0 RID: 160 RVA: 0x00002AB0 File Offset: 0x00000CB0
	// (set) Token: 0x060000A1 RID: 161 RVA: 0x00002AC8 File Offset: 0x00000CC8
	[XmlElement("Invoice")]
	public SourceDocumentsSalesInvoicesInvoice[] Invoice
	{
		get
		{
			return this.invoiceField;
		}
		set
		{
			this.invoiceField = value;
		}
	}

	// Token: 0x04000058 RID: 88
	private string numberOfEntriesField;

	// Token: 0x04000059 RID: 89
	private decimal totalDebitField;

	// Token: 0x0400005A RID: 90
	private decimal totalCreditField;

	// Token: 0x0400005B RID: 91
	private SourceDocumentsSalesInvoicesInvoice[] invoiceField;
}
