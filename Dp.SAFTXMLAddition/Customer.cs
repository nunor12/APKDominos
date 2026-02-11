using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Security;
using System.Xml.Serialization;

// Token: 0x0200000C RID: 12
[GeneratedCode("xsd", "4.0.30319.1")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01")]
[XmlRoot(Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01", IsNullable = false)]
[Serializable]
public class Customer
{
	// Token: 0x1700002B RID: 43
	// (get) Token: 0x0600005D RID: 93 RVA: 0x00002640 File Offset: 0x00000840
	// (set) Token: 0x0600005E RID: 94 RVA: 0x00002658 File Offset: 0x00000858
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

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x0600005F RID: 95 RVA: 0x00002664 File Offset: 0x00000864
	// (set) Token: 0x06000060 RID: 96 RVA: 0x0000267C File Offset: 0x0000087C
	public string AccountID
	{
		get
		{
			return this.accountIDField;
		}
		set
		{
			this.accountIDField = value;
		}
	}

	// Token: 0x1700002D RID: 45
	// (get) Token: 0x06000061 RID: 97 RVA: 0x00002688 File Offset: 0x00000888
	// (set) Token: 0x06000062 RID: 98 RVA: 0x000026A5 File Offset: 0x000008A5
	public string CustomerTaxID
	{
		get
		{
			return SecurityElement.Escape(this.customerTaxIDField);
		}
		set
		{
			this.customerTaxIDField = value;
		}
	}

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x06000063 RID: 99 RVA: 0x000026B0 File Offset: 0x000008B0
	// (set) Token: 0x06000064 RID: 100 RVA: 0x000026CD File Offset: 0x000008CD
	public string CompanyName
	{
		get
		{
			return SecurityElement.Escape(this.companyNameField);
		}
		set
		{
			this.companyNameField = value;
		}
	}

	// Token: 0x1700002F RID: 47
	// (get) Token: 0x06000065 RID: 101 RVA: 0x000026D8 File Offset: 0x000008D8
	// (set) Token: 0x06000066 RID: 102 RVA: 0x000026F0 File Offset: 0x000008F0
	public AddressStructure BillingAddress
	{
		get
		{
			return this.billingAddressField;
		}
		set
		{
			this.billingAddressField = value;
		}
	}

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x06000067 RID: 103 RVA: 0x000026FC File Offset: 0x000008FC
	// (set) Token: 0x06000068 RID: 104 RVA: 0x00002714 File Offset: 0x00000914
	[XmlElement("ShipToAddress")]
	public Collection<AddressStructure> ShipToAddress
	{
		get
		{
			return this.shipToAddressField;
		}
		set
		{
			this.shipToAddressField = value;
		}
	}

	// Token: 0x17000031 RID: 49
	// (get) Token: 0x06000069 RID: 105 RVA: 0x00002720 File Offset: 0x00000920
	// (set) Token: 0x0600006A RID: 106 RVA: 0x00002738 File Offset: 0x00000938
	[XmlElement(DataType = "integer")]
	public string SelfBillingIndicator
	{
		get
		{
			return this.selfBillingIndicatorField;
		}
		set
		{
			this.selfBillingIndicatorField = value;
		}
	}

	// Token: 0x0400003C RID: 60
	private string customerIDField;

	// Token: 0x0400003D RID: 61
	private string accountIDField;

	// Token: 0x0400003E RID: 62
	private string customerTaxIDField;

	// Token: 0x0400003F RID: 63
	private string companyNameField;

	// Token: 0x04000040 RID: 64
	private AddressStructure billingAddressField;

	// Token: 0x04000041 RID: 65
	private Collection<AddressStructure> shipToAddressField;

	// Token: 0x04000042 RID: 66
	private string selfBillingIndicatorField;
}
