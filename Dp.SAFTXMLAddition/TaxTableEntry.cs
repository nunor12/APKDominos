using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

// Token: 0x0200000F RID: 15
[GeneratedCode("xsd", "4.0.30319.1")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01")]
[XmlRoot(Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01", IsNullable = false)]
[Serializable]
public class TaxTableEntry
{
	// Token: 0x1700003E RID: 62
	// (get) Token: 0x06000086 RID: 134 RVA: 0x00002900 File Offset: 0x00000B00
	// (set) Token: 0x06000087 RID: 135 RVA: 0x00002918 File Offset: 0x00000B18
	public string TaxType
	{
		get
		{
			return this.taxTypeField;
		}
		set
		{
			this.taxTypeField = value;
		}
	}

	// Token: 0x1700003F RID: 63
	// (get) Token: 0x06000088 RID: 136 RVA: 0x00002924 File Offset: 0x00000B24
	// (set) Token: 0x06000089 RID: 137 RVA: 0x0000293C File Offset: 0x00000B3C
	public string TaxCountryRegion
	{
		get
		{
			return this.taxCountryRegionField;
		}
		set
		{
			this.taxCountryRegionField = value;
		}
	}

	// Token: 0x17000040 RID: 64
	// (get) Token: 0x0600008A RID: 138 RVA: 0x00002948 File Offset: 0x00000B48
	// (set) Token: 0x0600008B RID: 139 RVA: 0x00002960 File Offset: 0x00000B60
	public string TaxCode
	{
		get
		{
			return this.taxCodeField;
		}
		set
		{
			this.taxCodeField = value;
		}
	}

	// Token: 0x17000041 RID: 65
	// (get) Token: 0x0600008C RID: 140 RVA: 0x0000296C File Offset: 0x00000B6C
	// (set) Token: 0x0600008D RID: 141 RVA: 0x00002984 File Offset: 0x00000B84
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

	// Token: 0x17000042 RID: 66
	// (get) Token: 0x0600008E RID: 142 RVA: 0x00002990 File Offset: 0x00000B90
	// (set) Token: 0x0600008F RID: 143 RVA: 0x000029A8 File Offset: 0x00000BA8
	[XmlElement(DataType = "date")]
	public DateTime TaxExpirationDate
	{
		get
		{
			return this.taxExpirationDateField;
		}
		set
		{
			this.taxExpirationDateField = value;
		}
	}

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x06000090 RID: 144 RVA: 0x000029B4 File Offset: 0x00000BB4
	// (set) Token: 0x06000091 RID: 145 RVA: 0x000029CC File Offset: 0x00000BCC
	[XmlIgnore]
	public bool TaxExpirationDateSpecified
	{
		get
		{
			return this.taxExpirationDateFieldSpecified;
		}
		set
		{
			this.taxExpirationDateFieldSpecified = value;
		}
	}

	// Token: 0x17000044 RID: 68
	// (get) Token: 0x06000092 RID: 146 RVA: 0x000029D8 File Offset: 0x00000BD8
	// (set) Token: 0x06000093 RID: 147 RVA: 0x000029F0 File Offset: 0x00000BF0
	[XmlElement("TaxAmount", typeof(decimal))]
	[XmlElement("TaxPercentage", typeof(decimal))]
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

	// Token: 0x17000045 RID: 69
	// (get) Token: 0x06000094 RID: 148 RVA: 0x000029FC File Offset: 0x00000BFC
	// (set) Token: 0x06000095 RID: 149 RVA: 0x00002A14 File Offset: 0x00000C14
	[XmlIgnore]
	public ItemChoiceType ItemElementName
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

	// Token: 0x0400004F RID: 79
	private string taxTypeField;

	// Token: 0x04000050 RID: 80
	private string taxCountryRegionField;

	// Token: 0x04000051 RID: 81
	private string taxCodeField;

	// Token: 0x04000052 RID: 82
	private string descriptionField;

	// Token: 0x04000053 RID: 83
	private DateTime taxExpirationDateField;

	// Token: 0x04000054 RID: 84
	private bool taxExpirationDateFieldSpecified;

	// Token: 0x04000055 RID: 85
	private decimal itemField;

	// Token: 0x04000056 RID: 86
	private ItemChoiceType itemElementNameField;
}
