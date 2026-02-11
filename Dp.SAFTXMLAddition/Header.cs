using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

// Token: 0x02000003 RID: 3
[GeneratedCode("xsd", "4.0.30319.1")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01")]
[XmlRoot(Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01", IsNullable = false)]
[Serializable]
public class Header
{
	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000008 RID: 8 RVA: 0x000020C4 File Offset: 0x000002C4
	// (set) Token: 0x06000009 RID: 9 RVA: 0x000020DC File Offset: 0x000002DC
	public string AuditFileVersion
	{
		get
		{
			return this.auditFileVersionField;
		}
		set
		{
			this.auditFileVersionField = value;
		}
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x0600000A RID: 10 RVA: 0x000020E8 File Offset: 0x000002E8
	// (set) Token: 0x0600000B RID: 11 RVA: 0x00002100 File Offset: 0x00000300
	public string CompanyID
	{
		get
		{
			return this.companyIDField;
		}
		set
		{
			this.companyIDField = value;
		}
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x0600000C RID: 12 RVA: 0x0000210C File Offset: 0x0000030C
	// (set) Token: 0x0600000D RID: 13 RVA: 0x00002124 File Offset: 0x00000324
	[XmlElement(DataType = "integer")]
	public string TaxRegistrationNumber
	{
		get
		{
			return this.taxRegistrationNumberField;
		}
		set
		{
			this.taxRegistrationNumberField = value;
		}
	}

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x0600000E RID: 14 RVA: 0x00002130 File Offset: 0x00000330
	// (set) Token: 0x0600000F RID: 15 RVA: 0x00002148 File Offset: 0x00000348
	public string TaxAccountingBasis
	{
		get
		{
			return this.taxAccountingBasisField;
		}
		set
		{
			this.taxAccountingBasisField = value;
		}
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x06000010 RID: 16 RVA: 0x00002154 File Offset: 0x00000354
	// (set) Token: 0x06000011 RID: 17 RVA: 0x0000216C File Offset: 0x0000036C
	public string CompanyName
	{
		get
		{
			return this.companyNameField;
		}
		set
		{
			this.companyNameField = value;
		}
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x06000012 RID: 18 RVA: 0x00002178 File Offset: 0x00000378
	// (set) Token: 0x06000013 RID: 19 RVA: 0x00002190 File Offset: 0x00000390
	public string BusinessName
	{
		get
		{
			return this.businessNameField;
		}
		set
		{
			this.businessNameField = value;
		}
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x06000014 RID: 20 RVA: 0x0000219C File Offset: 0x0000039C
	// (set) Token: 0x06000015 RID: 21 RVA: 0x000021B4 File Offset: 0x000003B4
	public AddressStructurePT CompanyAddress
	{
		get
		{
			return this.companyAddressField;
		}
		set
		{
			this.companyAddressField = value;
		}
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x06000016 RID: 22 RVA: 0x000021C0 File Offset: 0x000003C0
	// (set) Token: 0x06000017 RID: 23 RVA: 0x000021D8 File Offset: 0x000003D8
	[XmlElement(DataType = "integer")]
	public string FiscalYear
	{
		get
		{
			return this.fiscalYearField;
		}
		set
		{
			this.fiscalYearField = value;
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x06000018 RID: 24 RVA: 0x000021E4 File Offset: 0x000003E4
	// (set) Token: 0x06000019 RID: 25 RVA: 0x000021FC File Offset: 0x000003FC
	[XmlElement(DataType = "date")]
	public DateTime StartDate
	{
		get
		{
			return this.startDateField;
		}
		set
		{
			this.startDateField = value;
		}
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x0600001A RID: 26 RVA: 0x00002208 File Offset: 0x00000408
	// (set) Token: 0x0600001B RID: 27 RVA: 0x00002220 File Offset: 0x00000420
	[XmlElement(DataType = "date")]
	public DateTime EndDate
	{
		get
		{
			return this.endDateField;
		}
		set
		{
			this.endDateField = value;
		}
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x0600001C RID: 28 RVA: 0x0000222C File Offset: 0x0000042C
	// (set) Token: 0x0600001D RID: 29 RVA: 0x00002244 File Offset: 0x00000444
	public string CurrencyCode
	{
		get
		{
			return this.currencyCodeField;
		}
		set
		{
			this.currencyCodeField = value;
		}
	}

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x0600001E RID: 30 RVA: 0x00002250 File Offset: 0x00000450
	// (set) Token: 0x0600001F RID: 31 RVA: 0x00002268 File Offset: 0x00000468
	[XmlElement(DataType = "date")]
	public DateTime DateCreated
	{
		get
		{
			return this.dateCreatedField;
		}
		set
		{
			this.dateCreatedField = value;
		}
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x06000020 RID: 32 RVA: 0x00002274 File Offset: 0x00000474
	// (set) Token: 0x06000021 RID: 33 RVA: 0x0000228C File Offset: 0x0000048C
	public string TaxEntity
	{
		get
		{
			return this.taxEntityField;
		}
		set
		{
			this.taxEntityField = value;
		}
	}

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x06000022 RID: 34 RVA: 0x00002298 File Offset: 0x00000498
	// (set) Token: 0x06000023 RID: 35 RVA: 0x000022B0 File Offset: 0x000004B0
	public string ProductCompanyTaxID
	{
		get
		{
			return this.productCompanyTaxIDField;
		}
		set
		{
			this.productCompanyTaxIDField = value;
		}
	}

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x06000024 RID: 36 RVA: 0x000022BC File Offset: 0x000004BC
	// (set) Token: 0x06000025 RID: 37 RVA: 0x000022D4 File Offset: 0x000004D4
	[XmlElement(DataType = "nonNegativeInteger")]
	public string SoftwareCertificateNumber
	{
		get
		{
			return this.softwareCertificateNumberField;
		}
		set
		{
			this.softwareCertificateNumberField = value;
		}
	}

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x06000026 RID: 38 RVA: 0x000022E0 File Offset: 0x000004E0
	// (set) Token: 0x06000027 RID: 39 RVA: 0x000022F8 File Offset: 0x000004F8
	public string ProductID
	{
		get
		{
			return this.productIDField;
		}
		set
		{
			this.productIDField = value;
		}
	}

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x06000028 RID: 40 RVA: 0x00002304 File Offset: 0x00000504
	// (set) Token: 0x06000029 RID: 41 RVA: 0x0000231C File Offset: 0x0000051C
	public string ProductVersion
	{
		get
		{
			return this.productVersionField;
		}
		set
		{
			this.productVersionField = value;
		}
	}

	// Token: 0x04000004 RID: 4
	private string auditFileVersionField;

	// Token: 0x04000005 RID: 5
	private string companyIDField;

	// Token: 0x04000006 RID: 6
	private string taxRegistrationNumberField;

	// Token: 0x04000007 RID: 7
	private string taxAccountingBasisField;

	// Token: 0x04000008 RID: 8
	private string companyNameField;

	// Token: 0x04000009 RID: 9
	private string businessNameField;

	// Token: 0x0400000A RID: 10
	private AddressStructurePT companyAddressField;

	// Token: 0x0400000B RID: 11
	private string fiscalYearField;

	// Token: 0x0400000C RID: 12
	private DateTime startDateField;

	// Token: 0x0400000D RID: 13
	private DateTime endDateField;

	// Token: 0x0400000E RID: 14
	private string currencyCodeField;

	// Token: 0x0400000F RID: 15
	private DateTime dateCreatedField;

	// Token: 0x04000010 RID: 16
	private string taxEntityField;

	// Token: 0x04000011 RID: 17
	private string productCompanyTaxIDField;

	// Token: 0x04000012 RID: 18
	private string softwareCertificateNumberField;

	// Token: 0x04000013 RID: 19
	private string productIDField;

	// Token: 0x04000014 RID: 20
	private string productVersionField;
}
