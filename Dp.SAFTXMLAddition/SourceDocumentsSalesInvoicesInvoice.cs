using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

// Token: 0x02000012 RID: 18
[GeneratedCode("xsd", "4.0.30319.1")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01")]
[Serializable]
public class SourceDocumentsSalesInvoicesInvoice
{
	// Token: 0x1700004B RID: 75
	// (get) Token: 0x060000A3 RID: 163 RVA: 0x00002AD4 File Offset: 0x00000CD4
	// (set) Token: 0x060000A4 RID: 164 RVA: 0x00002AEC File Offset: 0x00000CEC
	public string InvoiceNo
	{
		get
		{
			return this.invoiceNoField;
		}
		set
		{
			this.invoiceNoField = value;
		}
	}

	// Token: 0x1700004C RID: 76
	// (get) Token: 0x060000A5 RID: 165 RVA: 0x00002AF8 File Offset: 0x00000CF8
	// (set) Token: 0x060000A6 RID: 166 RVA: 0x00002B10 File Offset: 0x00000D10
	public string ATCUD
	{
		get
		{
			return this.ATCUDField;
		}
		set
		{
			this.ATCUDField = value;
		}
	}

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x060000A7 RID: 167 RVA: 0x00002B1C File Offset: 0x00000D1C
	// (set) Token: 0x060000A8 RID: 168 RVA: 0x00002B34 File Offset: 0x00000D34
	public SourceDocumentsSalesInvoicesInvoiceDocumentStatus DocumentStatus
	{
		get
		{
			return this.documentStatusField;
		}
		set
		{
			this.documentStatusField = value;
		}
	}

	// Token: 0x1700004E RID: 78
	// (get) Token: 0x060000A9 RID: 169 RVA: 0x00002B40 File Offset: 0x00000D40
	// (set) Token: 0x060000AA RID: 170 RVA: 0x00002B58 File Offset: 0x00000D58
	public string Hash
	{
		get
		{
			return this.hashField;
		}
		set
		{
			this.hashField = value;
		}
	}

	// Token: 0x1700004F RID: 79
	// (get) Token: 0x060000AB RID: 171 RVA: 0x00002B64 File Offset: 0x00000D64
	// (set) Token: 0x060000AC RID: 172 RVA: 0x00002B7C File Offset: 0x00000D7C
	public string HashControl
	{
		get
		{
			return this.hashControlField;
		}
		set
		{
			this.hashControlField = value;
		}
	}

	// Token: 0x17000050 RID: 80
	// (get) Token: 0x060000AD RID: 173 RVA: 0x00002B88 File Offset: 0x00000D88
	// (set) Token: 0x060000AE RID: 174 RVA: 0x00002BA0 File Offset: 0x00000DA0
	[XmlElement(DataType = "integer")]
	public string Period
	{
		get
		{
			return this.periodField;
		}
		set
		{
			this.periodField = value;
		}
	}

	// Token: 0x17000051 RID: 81
	// (get) Token: 0x060000AF RID: 175 RVA: 0x00002BAC File Offset: 0x00000DAC
	// (set) Token: 0x060000B0 RID: 176 RVA: 0x00002BC4 File Offset: 0x00000DC4
	[XmlElement(DataType = "date")]
	public DateTime InvoiceDate
	{
		get
		{
			return this.invoiceDateField;
		}
		set
		{
			this.invoiceDateField = value;
		}
	}

	// Token: 0x17000052 RID: 82
	// (get) Token: 0x060000B1 RID: 177 RVA: 0x00002BD0 File Offset: 0x00000DD0
	// (set) Token: 0x060000B2 RID: 178 RVA: 0x00002BE8 File Offset: 0x00000DE8
	public string InvoiceType
	{
		get
		{
			return this.invoiceTypeField;
		}
		set
		{
			this.invoiceTypeField = value;
		}
	}

	// Token: 0x17000053 RID: 83
	// (get) Token: 0x060000B3 RID: 179 RVA: 0x00002BF4 File Offset: 0x00000DF4
	// (set) Token: 0x060000B4 RID: 180 RVA: 0x00002C0C File Offset: 0x00000E0C
	public SpecialRegimes SpecialRegimes
	{
		get
		{
			return this.specialRegimesField;
		}
		set
		{
			this.specialRegimesField = value;
		}
	}

	// Token: 0x17000054 RID: 84
	// (get) Token: 0x060000B5 RID: 181 RVA: 0x00002C18 File Offset: 0x00000E18
	// (set) Token: 0x060000B6 RID: 182 RVA: 0x00002C30 File Offset: 0x00000E30
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

	// Token: 0x17000055 RID: 85
	// (get) Token: 0x060000B7 RID: 183 RVA: 0x00002C3C File Offset: 0x00000E3C
	// (set) Token: 0x060000B8 RID: 184 RVA: 0x00002C54 File Offset: 0x00000E54
	public DateTime SystemEntryDate
	{
		get
		{
			return this.systemEntryDateField;
		}
		set
		{
			this.systemEntryDateField = value;
		}
	}

	// Token: 0x17000056 RID: 86
	// (get) Token: 0x060000B9 RID: 185 RVA: 0x00002C60 File Offset: 0x00000E60
	// (set) Token: 0x060000BA RID: 186 RVA: 0x00002C78 File Offset: 0x00000E78
	public string TransactionID
	{
		get
		{
			return this.transactionIDField;
		}
		set
		{
			this.transactionIDField = value;
		}
	}

	// Token: 0x17000057 RID: 87
	// (get) Token: 0x060000BB RID: 187 RVA: 0x00002C84 File Offset: 0x00000E84
	// (set) Token: 0x060000BC RID: 188 RVA: 0x00002C9C File Offset: 0x00000E9C
	public string CustomerID
	{
		get
		{
			return this.customerIDField;
		}
		set
		{
			this.customerIDField = value;
		}
	}

	// Token: 0x17000058 RID: 88
	// (get) Token: 0x060000BD RID: 189 RVA: 0x00002CA8 File Offset: 0x00000EA8
	// (set) Token: 0x060000BE RID: 190 RVA: 0x00002CC0 File Offset: 0x00000EC0
	public ShippingPointStructure ShipTo
	{
		get
		{
			return this.shipToField;
		}
		set
		{
			this.shipToField = value;
		}
	}

	// Token: 0x17000059 RID: 89
	// (get) Token: 0x060000BF RID: 191 RVA: 0x00002CCC File Offset: 0x00000ECC
	// (set) Token: 0x060000C0 RID: 192 RVA: 0x00002CE4 File Offset: 0x00000EE4
	public ShippingPointStructure ShipFrom
	{
		get
		{
			return this.shipFromField;
		}
		set
		{
			this.shipFromField = value;
		}
	}

	// Token: 0x1700005A RID: 90
	// (get) Token: 0x060000C1 RID: 193 RVA: 0x00002CF0 File Offset: 0x00000EF0
	// (set) Token: 0x060000C2 RID: 194 RVA: 0x00002D08 File Offset: 0x00000F08
	public string MovementStartTime
	{
		get
		{
			return this.movementStartTimeField;
		}
		set
		{
			this.movementStartTimeField = value;
		}
	}

	// Token: 0x1700005B RID: 91
	// (get) Token: 0x060000C3 RID: 195 RVA: 0x00002D14 File Offset: 0x00000F14
	// (set) Token: 0x060000C4 RID: 196 RVA: 0x00002D2C File Offset: 0x00000F2C
	[XmlElement("Line")]
	public SourceDocumentsSalesInvoicesInvoiceLine[] Line
	{
		get
		{
			return this.lineField;
		}
		set
		{
			this.lineField = value;
		}
	}

	// Token: 0x1700005C RID: 92
	// (get) Token: 0x060000C5 RID: 197 RVA: 0x00002D38 File Offset: 0x00000F38
	// (set) Token: 0x060000C6 RID: 198 RVA: 0x00002D50 File Offset: 0x00000F50
	public SourceDocumentsSalesInvoicesInvoiceDocumentTotals DocumentTotals
	{
		get
		{
			return this.documentTotalsField;
		}
		set
		{
			this.documentTotalsField = value;
		}
	}

	// Token: 0x0400005C RID: 92
	private string invoiceNoField;

	// Token: 0x0400005D RID: 93
	private string ATCUDField;

	// Token: 0x0400005E RID: 94
	private SourceDocumentsSalesInvoicesInvoiceDocumentStatus documentStatusField;

	// Token: 0x0400005F RID: 95
	private string hashField;

	// Token: 0x04000060 RID: 96
	private string hashControlField;

	// Token: 0x04000061 RID: 97
	private string periodField;

	// Token: 0x04000062 RID: 98
	private DateTime invoiceDateField;

	// Token: 0x04000063 RID: 99
	private string invoiceTypeField;

	// Token: 0x04000064 RID: 100
	private SpecialRegimes specialRegimesField;

	// Token: 0x04000065 RID: 101
	private string sourceIDField;

	// Token: 0x04000066 RID: 102
	private DateTime systemEntryDateField;

	// Token: 0x04000067 RID: 103
	private string transactionIDField;

	// Token: 0x04000068 RID: 104
	private string customerIDField;

	// Token: 0x04000069 RID: 105
	private ShippingPointStructure shipToField;

	// Token: 0x0400006A RID: 106
	private ShippingPointStructure shipFromField;

	// Token: 0x0400006B RID: 107
	private SourceDocumentsSalesInvoicesInvoiceLine[] lineField;

	// Token: 0x0400006C RID: 108
	private SourceDocumentsSalesInvoicesInvoiceDocumentTotals documentTotalsField;

	// Token: 0x0400006D RID: 109
	private string movementStartTimeField;
}
