using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

// Token: 0x02000004 RID: 4
[GeneratedCode("xsd", "4.0.30319.1")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01")]
[XmlRoot("CompanyAddress", Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01", IsNullable = false)]
[Serializable]
public class AddressStructurePT
{
	// Token: 0x17000015 RID: 21
	// (get) Token: 0x0600002B RID: 43 RVA: 0x00002328 File Offset: 0x00000528
	// (set) Token: 0x0600002C RID: 44 RVA: 0x00002340 File Offset: 0x00000540
	public string BuildingNumber
	{
		get
		{
			return this.buildingNumberField;
		}
		set
		{
			this.buildingNumberField = value;
		}
	}

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x0600002D RID: 45 RVA: 0x0000234C File Offset: 0x0000054C
	// (set) Token: 0x0600002E RID: 46 RVA: 0x00002364 File Offset: 0x00000564
	public string StreetName
	{
		get
		{
			return this.streetNameField;
		}
		set
		{
			this.streetNameField = value;
		}
	}

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x0600002F RID: 47 RVA: 0x00002370 File Offset: 0x00000570
	// (set) Token: 0x06000030 RID: 48 RVA: 0x00002388 File Offset: 0x00000588
	public string AddressDetail
	{
		get
		{
			return this.addressDetailField;
		}
		set
		{
			this.addressDetailField = value;
		}
	}

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x06000031 RID: 49 RVA: 0x00002394 File Offset: 0x00000594
	// (set) Token: 0x06000032 RID: 50 RVA: 0x000023AC File Offset: 0x000005AC
	public string City
	{
		get
		{
			return this.cityField;
		}
		set
		{
			this.cityField = value;
		}
	}

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x06000033 RID: 51 RVA: 0x000023B8 File Offset: 0x000005B8
	// (set) Token: 0x06000034 RID: 52 RVA: 0x000023D0 File Offset: 0x000005D0
	public string PostalCode
	{
		get
		{
			return this.postalCodeField;
		}
		set
		{
			this.postalCodeField = value;
		}
	}

	// Token: 0x1700001A RID: 26
	// (get) Token: 0x06000035 RID: 53 RVA: 0x000023DC File Offset: 0x000005DC
	// (set) Token: 0x06000036 RID: 54 RVA: 0x000023F4 File Offset: 0x000005F4
	public string Region
	{
		get
		{
			return this.regionField;
		}
		set
		{
			this.regionField = value;
		}
	}

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x06000037 RID: 55 RVA: 0x00002400 File Offset: 0x00000600
	// (set) Token: 0x06000038 RID: 56 RVA: 0x00002418 File Offset: 0x00000618
	public string Country
	{
		get
		{
			return this.countryField;
		}
		set
		{
			this.countryField = value;
		}
	}

	// Token: 0x04000015 RID: 21
	private string buildingNumberField;

	// Token: 0x04000016 RID: 22
	private string streetNameField;

	// Token: 0x04000017 RID: 23
	private string addressDetailField;

	// Token: 0x04000018 RID: 24
	private string cityField;

	// Token: 0x04000019 RID: 25
	private string postalCodeField;

	// Token: 0x0400001A RID: 26
	private string regionField;

	// Token: 0x0400001B RID: 27
	private string countryField;
}
