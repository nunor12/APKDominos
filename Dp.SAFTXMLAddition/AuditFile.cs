using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

// Token: 0x02000002 RID: 2
[GeneratedCode("xsd", "4.0.30319.1")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01")]
[XmlRoot(Namespace = "urn:OECD:StandardAuditFile-Tax:PT_1.03_01", IsNullable = false)]
[Serializable]
public class AuditFile
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	// (set) Token: 0x06000002 RID: 2 RVA: 0x00002068 File Offset: 0x00000268
	public Header Header
	{
		get
		{
			return this.headerField;
		}
		set
		{
			this.headerField = value;
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000003 RID: 3 RVA: 0x00002074 File Offset: 0x00000274
	// (set) Token: 0x06000004 RID: 4 RVA: 0x0000208C File Offset: 0x0000028C
	public AuditFileMasterFiles MasterFiles
	{
		get
		{
			return this.masterFilesField;
		}
		set
		{
			this.masterFilesField = value;
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000005 RID: 5 RVA: 0x00002098 File Offset: 0x00000298
	// (set) Token: 0x06000006 RID: 6 RVA: 0x000020B0 File Offset: 0x000002B0
	public SourceDocuments SourceDocuments
	{
		get
		{
			return this.sourceDocumentsField;
		}
		set
		{
			this.sourceDocumentsField = value;
		}
	}

	// Token: 0x04000001 RID: 1
	private Header headerField;

	// Token: 0x04000002 RID: 2
	private AuditFileMasterFiles masterFilesField;

	// Token: 0x04000003 RID: 3
	private SourceDocuments sourceDocumentsField;
}
