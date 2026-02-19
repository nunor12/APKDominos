using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

// Token: 0x0200000A RID: 10
[GeneratedCode("xsd", "4.0.30319.1")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01")]
[Serializable]
public class SpecialRegimes
{
	// Token: 0x17000025 RID: 37
	// (get) Token: 0x0600004F RID: 79 RVA: 0x00002568 File Offset: 0x00000768
	// (set) Token: 0x06000050 RID: 80 RVA: 0x00002580 File Offset: 0x00000780
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

	// Token: 0x17000026 RID: 38
	// (get) Token: 0x06000051 RID: 81 RVA: 0x0000258C File Offset: 0x0000078C
	// (set) Token: 0x06000052 RID: 82 RVA: 0x000025A4 File Offset: 0x000007A4
	[XmlElement(DataType = "integer")]
	public string CashVATSchemeIndicator
	{
		get
		{
			return this.cashVATSchemeIndicatorField;
		}
		set
		{
			this.cashVATSchemeIndicatorField = value;
		}
	}

	// Token: 0x17000027 RID: 39
	// (get) Token: 0x06000053 RID: 83 RVA: 0x000025B0 File Offset: 0x000007B0
	// (set) Token: 0x06000054 RID: 84 RVA: 0x000025C8 File Offset: 0x000007C8
	[XmlElement(DataType = "integer")]
	public string ThirdPartiesBillingIndicator
	{
		get
		{
			return this.thirdPartiesBillingIndicatorField;
		}
		set
		{
			this.thirdPartiesBillingIndicatorField = value;
		}
	}

	// Token: 0x04000036 RID: 54
	private string selfBillingIndicatorField;

	// Token: 0x04000037 RID: 55
	private string cashVATSchemeIndicatorField;

	// Token: 0x04000038 RID: 56
	private string thirdPartiesBillingIndicatorField;
}
