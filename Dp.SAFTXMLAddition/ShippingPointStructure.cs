using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

// Token: 0x02000014 RID: 20
[GeneratedCode("xsd", "4.0.30319.1")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01")]
[XmlRoot("ShipFrom", Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01", IsNullable = false)]
[Serializable]
public class ShippingPointStructure
{
	// Token: 0x17000061 RID: 97
	// (get) Token: 0x060000D1 RID: 209 RVA: 0x00002DEC File Offset: 0x00000FEC
	// (set) Token: 0x060000D2 RID: 210 RVA: 0x00002E04 File Offset: 0x00001004
	public AddressStructure Address
	{
		get
		{
			return this.addressField;
		}
		set
		{
			this.addressField = value;
		}
	}

	// Token: 0x04000072 RID: 114
	private AddressStructure addressField;
}
