using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

// Token: 0x02000006 RID: 6
[GeneratedCode("xsd", "4.0.30319.1")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01")]
[Serializable]
public class PaymentMethod
{
	// Token: 0x1700001D RID: 29
	// (get) Token: 0x0600003D RID: 61 RVA: 0x00002448 File Offset: 0x00000648
	// (set) Token: 0x0600003E RID: 62 RVA: 0x00002460 File Offset: 0x00000660
	public PaymentMechanism PaymentMechanism
	{
		get
		{
			return this.paymentMechanismField;
		}
		set
		{
			this.paymentMechanismField = value;
		}
	}

	// Token: 0x1700001E RID: 30
	// (get) Token: 0x0600003F RID: 63 RVA: 0x0000246C File Offset: 0x0000066C
	// (set) Token: 0x06000040 RID: 64 RVA: 0x00002484 File Offset: 0x00000684
	public decimal PaymentAmount
	{
		get
		{
			return this.paymentAmountField;
		}
		set
		{
			this.paymentAmountField = value;
		}
	}

	// Token: 0x1700001F RID: 31
	// (get) Token: 0x06000041 RID: 65 RVA: 0x00002490 File Offset: 0x00000690
	// (set) Token: 0x06000042 RID: 66 RVA: 0x000024A8 File Offset: 0x000006A8
	[XmlElement(DataType = "date")]
	public DateTime PaymentDate
	{
		get
		{
			return this.paymentDateField;
		}
		set
		{
			this.paymentDateField = value;
		}
	}

	// Token: 0x0400001D RID: 29
	private PaymentMechanism paymentMechanismField;

	// Token: 0x0400001E RID: 30
	private decimal paymentAmountField;

	// Token: 0x0400001F RID: 31
	private DateTime paymentDateField;
}
