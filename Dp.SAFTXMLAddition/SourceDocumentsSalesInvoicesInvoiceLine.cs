using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

// Token: 0x02000015 RID: 21
[GeneratedCode("xsd", "4.0.30319.1")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01")]
[Serializable]
public class SourceDocumentsSalesInvoicesInvoiceLine
{
	// Token: 0x17000062 RID: 98
	// (get) Token: 0x060000D4 RID: 212 RVA: 0x00002E10 File Offset: 0x00001010
	// (set) Token: 0x060000D5 RID: 213 RVA: 0x00002E28 File Offset: 0x00001028
	[XmlElement(DataType = "nonNegativeInteger")]
	public string LineNumber
	{
		get
		{
			return this.lineNumberField;
		}
		set
		{
			this.lineNumberField = value;
		}
	}

	// Token: 0x17000063 RID: 99
	// (get) Token: 0x060000D6 RID: 214 RVA: 0x00002E34 File Offset: 0x00001034
	// (set) Token: 0x060000D7 RID: 215 RVA: 0x00002E4C File Offset: 0x0000104C
	public string ProductCode
	{
		get
		{
			return this.productCodeField;
		}
		set
		{
			this.productCodeField = value;
		}
	}

	// Token: 0x17000064 RID: 100
	// (get) Token: 0x060000D8 RID: 216 RVA: 0x00002E58 File Offset: 0x00001058
	// (set) Token: 0x060000D9 RID: 217 RVA: 0x00002E70 File Offset: 0x00001070
	public string ProductDescription
	{
		get
		{
			return this.productDescriptionField;
		}
		set
		{
			this.productDescriptionField = value;
		}
	}

	// Token: 0x17000065 RID: 101
	// (get) Token: 0x060000DA RID: 218 RVA: 0x00002E7C File Offset: 0x0000107C
	// (set) Token: 0x060000DB RID: 219 RVA: 0x00002E94 File Offset: 0x00001094
	public decimal Quantity
	{
		get
		{
			return this.quantityField;
		}
		set
		{
			this.quantityField = value;
		}
	}

	// Token: 0x17000066 RID: 102
	// (get) Token: 0x060000DC RID: 220 RVA: 0x00002EA0 File Offset: 0x000010A0
	// (set) Token: 0x060000DD RID: 221 RVA: 0x00002EB8 File Offset: 0x000010B8
	public string UnitOfMeasure
	{
		get
		{
			return this.unitOfMeasureField;
		}
		set
		{
			this.unitOfMeasureField = value;
		}
	}

	// Token: 0x17000067 RID: 103
	// (get) Token: 0x060000DE RID: 222 RVA: 0x00002EC4 File Offset: 0x000010C4
	// (set) Token: 0x060000DF RID: 223 RVA: 0x00002EDC File Offset: 0x000010DC
	public decimal UnitPrice
	{
		get
		{
			return this.unitPriceField;
		}
		set
		{
			this.unitPriceField = value;
		}
	}

	// Token: 0x17000068 RID: 104
	// (get) Token: 0x060000E0 RID: 224 RVA: 0x00002EE8 File Offset: 0x000010E8
	// (set) Token: 0x060000E1 RID: 225 RVA: 0x00002F00 File Offset: 0x00001100
	[XmlElement(DataType = "date")]
	public DateTime TaxPointDate
	{
		get
		{
			return this.taxPointDateField;
		}
		set
		{
			this.taxPointDateField = value;
		}
	}

	// Token: 0x17000069 RID: 105
	// (get) Token: 0x060000E2 RID: 226 RVA: 0x00002F0C File Offset: 0x0000110C
	// (set) Token: 0x060000E3 RID: 227 RVA: 0x00002F24 File Offset: 0x00001124
	[XmlElement("References")]
	public References[] References
	{
		get
		{
			return this.referencesField;
		}
		set
		{
			this.referencesField = value;
		}
	}

	// Token: 0x1700006A RID: 106
	// (get) Token: 0x060000E4 RID: 228 RVA: 0x00002F30 File Offset: 0x00001130
	// (set) Token: 0x060000E5 RID: 229 RVA: 0x00002F48 File Offset: 0x00001148
	public string Description
	{
		get
		{
			return this.descriptionField;
		}
		set
		{
			this.descriptionField = value;
		}
	}

	// Token: 0x1700006B RID: 107
	// (get) Token: 0x060000E6 RID: 230 RVA: 0x00002F54 File Offset: 0x00001154
	// (set) Token: 0x060000E7 RID: 231 RVA: 0x00002F6C File Offset: 0x0000116C
	[XmlElement("CreditAmount", typeof(decimal))]
	[XmlElement("DebitAmount", typeof(decimal))]
	[XmlChoiceIdentifier("ItemElementName")]
	public decimal Item
	{
		get
		{
			return this.itemField;
		}
		set
		{
			this.itemField = value;
		}
	}

	// Token: 0x1700006C RID: 108
	// (get) Token: 0x060000E8 RID: 232 RVA: 0x00002F78 File Offset: 0x00001178
	// (set) Token: 0x060000E9 RID: 233 RVA: 0x00002F90 File Offset: 0x00001190
	[XmlIgnore]
	public ItemChoiceType1 ItemElementName
	{
		get
		{
			return this.itemElementNameField;
		}
		set
		{
			this.itemElementNameField = value;
		}
	}

	// Token: 0x1700006D RID: 109
	// (get) Token: 0x060000EA RID: 234 RVA: 0x00002F9C File Offset: 0x0000119C
	// (set) Token: 0x060000EB RID: 235 RVA: 0x00002FB4 File Offset: 0x000011B4
	public Tax Tax
	{
		get
		{
			return this.taxField;
		}
		set
		{
			this.taxField = value;
		}
	}

	// Token: 0x1700006E RID: 110
	// (get) Token: 0x060000EC RID: 236 RVA: 0x00002FC0 File Offset: 0x000011C0
	// (set) Token: 0x060000ED RID: 237 RVA: 0x00002FD8 File Offset: 0x000011D8
	public decimal SettlementAmount
	{
		get
		{
			return this.settlementAmountField;
		}
		set
		{
			this.settlementAmountField = value;
		}
	}

	// Token: 0x1700006F RID: 111
	// (get) Token: 0x060000EE RID: 238 RVA: 0x00002FE4 File Offset: 0x000011E4
	// (set) Token: 0x060000EF RID: 239 RVA: 0x00002FFC File Offset: 0x000011FC
	[XmlIgnore]
	public bool SettlementAmountSpecified
	{
		get
		{
			return this.settlementAmountFieldSpecified;
		}
		set
		{
			this.settlementAmountFieldSpecified = value;
		}
	}

	// Token: 0x04000073 RID: 115
	private string lineNumberField;

	// Token: 0x04000074 RID: 116
	private string productCodeField;

	// Token: 0x04000075 RID: 117
	private string productDescriptionField;

	// Token: 0x04000076 RID: 118
	private decimal quantityField;

	// Token: 0x04000077 RID: 119
	private string unitOfMeasureField;

	// Token: 0x04000078 RID: 120
	private decimal unitPriceField;

	// Token: 0x04000079 RID: 121
	private DateTime taxPointDateField;

	// Token: 0x0400007A RID: 122
	private References[] referencesField;

	// Token: 0x0400007B RID: 123
	private string descriptionField;

	// Token: 0x0400007C RID: 124
	private decimal itemField;

	// Token: 0x0400007D RID: 125
	private ItemChoiceType1 itemElementNameField;

	// Token: 0x0400007E RID: 126
	private Tax taxField;

	// Token: 0x0400007F RID: 127
	private decimal settlementAmountField;

	// Token: 0x04000080 RID: 128
	private bool settlementAmountFieldSpecified;
}
