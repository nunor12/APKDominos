using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

// Token: 0x02000019 RID: 25
[GeneratedCode("xsd", "4.0.30319.1")]
[XmlType(Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01")]
[XmlRoot("SourceBilling", Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01", IsNullable = false)]
[Serializable]
public enum SAFTPTSourceBilling
{
	// Token: 0x0400008A RID: 138
	P,
	// Token: 0x0400008B RID: 139
	I,
	// Token: 0x0400008C RID: 140
	M
}
