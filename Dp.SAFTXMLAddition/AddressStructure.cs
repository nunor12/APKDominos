using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Security;
using System.Xml.Serialization;

// Token: 0x0200000D RID: 13
[GeneratedCode("xsd", "4.0.30319.1")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01")]
[XmlRoot("BillingAddress", Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01", IsNullable = false)]
[Serializable]
public class AddressStructure
{
	// Token: 0x17000032 RID: 50
	// (get) Token: 0x0600006C RID: 108 RVA: 0x00002744 File Offset: 0x00000944
	// (set) Token: 0x0600006D RID: 109 RVA: 0x0000275C File Offset: 0x0000095C
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

	// Token: 0x17000033 RID: 51
	// (get) Token: 0x0600006E RID: 110 RVA: 0x00002768 File Offset: 0x00000968
	// (set) Token: 0x0600006F RID: 111 RVA: 0x00002780 File Offset: 0x00000980
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

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x06000070 RID: 112 RVA: 0x0000278C File Offset: 0x0000098C
	// (set) Token: 0x06000071 RID: 113 RVA: 0x000027A9 File Offset: 0x000009A9
	public string AddressDetail
	{
		get
		{
			return SecurityElement.Escape(this.addressDetailField);
		}
		set
		{
			this.addressDetailField = value;
		}
	}

	// Token: 0x17000035 RID: 53
	// (get) Token: 0x06000072 RID: 114 RVA: 0x000027B4 File Offset: 0x000009B4
	// (set) Token: 0x06000073 RID: 115 RVA: 0x000027D1 File Offset: 0x000009D1
	public string City
	{
		get
		{
			return SecurityElement.Escape(this.cityField);
		}
		set
		{
			this.cityField = value;
		}
	}

	// Token: 0x17000036 RID: 54
	// (get) Token: 0x06000074 RID: 116 RVA: 0x000027DC File Offset: 0x000009DC
	// (set) Token: 0x06000075 RID: 117 RVA: 0x000027F9 File Offset: 0x000009F9
	public string PostalCode
	{
		get
		{
			return SecurityElement.Escape(this.postalCodeField);
		}
		set
		{
			this.postalCodeField = value;
		}
	}

	// Token: 0x17000037 RID: 55
	// (get) Token: 0x06000076 RID: 118 RVA: 0x00002804 File Offset: 0x00000A04
	// (set) Token: 0x06000077 RID: 119 RVA: 0x0000281C File Offset: 0x00000A1C
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

	// Token: 0x17000038 RID: 56
	// (get) Token: 0x06000078 RID: 120 RVA: 0x00002828 File Offset: 0x00000A28
	// (set) Token: 0x06000079 RID: 121 RVA: 0x00002840 File Offset: 0x00000A40
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

	// Token: 0x04000043 RID: 67
	private string buildingNumberField;

	// Token: 0x04000044 RID: 68
	private string streetNameField;

	// Token: 0x04000045 RID: 69
	private string addressDetailField;

	// Token: 0x04000046 RID: 70
	private string cityField;

	// Token: 0x04000047 RID: 71
	private string postalCodeField;

	// Token: 0x04000048 RID: 72
	private string regionField;

	// Token: 0x04000049 RID: 73
	private string countryField;
}
