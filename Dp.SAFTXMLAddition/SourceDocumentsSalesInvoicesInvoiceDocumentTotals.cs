using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

// Token: 0x02000017 RID: 23
[GeneratedCode("xsd", "4.0.30319.1")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01")]
[Serializable]
public class SourceDocumentsSalesInvoicesInvoiceDocumentTotals
{
	// Token: 0x17000070 RID: 112
	// (get) Token: 0x060000F1 RID: 241 RVA: 0x00003008 File Offset: 0x00001208
	// (set) Token: 0x060000F2 RID: 242 RVA: 0x00003020 File Offset: 0x00001220
	public decimal TaxPayable
	{
		get
		{
			return this.taxPayableField;
		}
		set
		{
			this.taxPayableField = value;
		}
	}

	// Token: 0x17000071 RID: 113
	// (get) Token: 0x060000F3 RID: 243 RVA: 0x0000302C File Offset: 0x0000122C
	// (set) Token: 0x060000F4 RID: 244 RVA: 0x00003044 File Offset: 0x00001244
	public decimal NetTotal
	{
		get
		{
			return this.netTotalField;
		}
		set
		{
			this.netTotalField = value;
		}
	}

	// Token: 0x17000072 RID: 114
	// (get) Token: 0x060000F5 RID: 245 RVA: 0x00003050 File Offset: 0x00001250
	// (set) Token: 0x060000F6 RID: 246 RVA: 0x00003068 File Offset: 0x00001268
	public decimal GrossTotal
	{
		get
		{
			return this.grossTotalField;
		}
		set
		{
			this.grossTotalField = value;
		}
	}

	// Token: 0x17000073 RID: 115
	// (get) Token: 0x060000F7 RID: 247 RVA: 0x00003074 File Offset: 0x00001274
	// (set) Token: 0x060000F8 RID: 248 RVA: 0x0000308C File Offset: 0x0000128C
	[XmlElement("Payment")]
	public PaymentMethod[] Payment
	{
		get
		{
			return this.paymentField;
		}
		set
		{
			this.paymentField = value;
		}
	}

	// Token: 0x04000084 RID: 132
	private decimal taxPayableField;

	// Token: 0x04000085 RID: 133
	private decimal netTotalField;

	// Token: 0x04000086 RID: 134
	private decimal grossTotalField;

	// Token: 0x04000087 RID: 135
	private PaymentMethod[] paymentField;
}
