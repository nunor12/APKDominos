using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

// Token: 0x0200000E RID: 14
[GeneratedCode("xsd", "4.0.30319.1")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01")]
[XmlRoot(Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01", IsNullable = false)]
[Serializable]
public class Product
{
	// Token: 0x17000039 RID: 57
	// (get) Token: 0x0600007B RID: 123 RVA: 0x0000284C File Offset: 0x00000A4C
	// (set) Token: 0x0600007C RID: 124 RVA: 0x00002864 File Offset: 0x00000A64
	public string ProductType
	{
		get
		{
			return this.productTypeField;
		}
		set
		{
			this.productTypeField = value;
		}
	}

	// Token: 0x1700003A RID: 58
	// (get) Token: 0x0600007D RID: 125 RVA: 0x00002870 File Offset: 0x00000A70
	// (set) Token: 0x0600007E RID: 126 RVA: 0x00002888 File Offset: 0x00000A88
	public string ProductCode
	{
		get
		{
			return this.productCodeField;
		}
		set
		{
			this.productCodeField = value;
		}
	}

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x0600007F RID: 127 RVA: 0x00002894 File Offset: 0x00000A94
	// (set) Token: 0x06000080 RID: 128 RVA: 0x000028AC File Offset: 0x00000AAC
	public string ProductGroup
	{
		get
		{
			return this.productGroupField;
		}
		set
		{
			this.productGroupField = value;
		}
	}

	// Token: 0x1700003C RID: 60
	// (get) Token: 0x06000081 RID: 129 RVA: 0x000028B8 File Offset: 0x00000AB8
	// (set) Token: 0x06000082 RID: 130 RVA: 0x000028D0 File Offset: 0x00000AD0
	public string ProductDescription
	{
		get
		{
			return this.productDescriptionField;
		}
		set
		{
			this.productDescriptionField = value;
		}
	}

	// Token: 0x1700003D RID: 61
	// (get) Token: 0x06000083 RID: 131 RVA: 0x000028DC File Offset: 0x00000ADC
	// (set) Token: 0x06000084 RID: 132 RVA: 0x000028F4 File Offset: 0x00000AF4
	public string ProductNumberCode
	{
		get
		{
			return this.productNumberCodeField;
		}
		set
		{
			this.productNumberCodeField = value;
		}
	}

	// Token: 0x0400004A RID: 74
	private string productTypeField;

	// Token: 0x0400004B RID: 75
	private string productCodeField;

	// Token: 0x0400004C RID: 76
	private string productGroupField;

	// Token: 0x0400004D RID: 77
	private string productDescriptionField;

	// Token: 0x0400004E RID: 78
	private string productNumberCodeField;
}
