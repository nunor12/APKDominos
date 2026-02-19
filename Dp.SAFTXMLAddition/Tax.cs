using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

// Token: 0x02000008 RID: 8
[GeneratedCode("xsd", "4.0.30319.1")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01")]
[Serializable]
public class Tax
{
	// Token: 0x17000020 RID: 32
	// (get) Token: 0x06000044 RID: 68 RVA: 0x000024B4 File Offset: 0x000006B4
	// (set) Token: 0x06000045 RID: 69 RVA: 0x000024CC File Offset: 0x000006CC
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

	// Token: 0x17000021 RID: 33
	// (get) Token: 0x06000046 RID: 70 RVA: 0x000024D8 File Offset: 0x000006D8
	// (set) Token: 0x06000047 RID: 71 RVA: 0x000024F0 File Offset: 0x000006F0
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

	// Token: 0x17000022 RID: 34
	// (get) Token: 0x06000048 RID: 72 RVA: 0x000024FC File Offset: 0x000006FC
	// (set) Token: 0x06000049 RID: 73 RVA: 0x00002514 File Offset: 0x00000714
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

	// Token: 0x17000023 RID: 35
	// (get) Token: 0x0600004A RID: 74 RVA: 0x00002520 File Offset: 0x00000720
	// (set) Token: 0x0600004B RID: 75 RVA: 0x00002538 File Offset: 0x00000738
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

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x0600004C RID: 76 RVA: 0x00002544 File Offset: 0x00000744
	// (set) Token: 0x0600004D RID: 77 RVA: 0x0000255C File Offset: 0x0000075C
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

	// Token: 0x0400002E RID: 46
	private string taxTypeField;

	// Token: 0x0400002F RID: 47
	private string taxCountryRegionField;

	// Token: 0x04000030 RID: 48
	private string taxCodeField;

	// Token: 0x04000031 RID: 49
	private decimal itemField;

	// Token: 0x04000032 RID: 50
	private ItemChoiceType itemElementNameField;
}
