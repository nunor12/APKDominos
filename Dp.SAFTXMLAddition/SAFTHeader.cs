using System;

namespace Dp.SAFTXMLAddition
{
	// Token: 0x0200001E RID: 30
	public class SAFTHeader
	{
		// Token: 0x0600012B RID: 299 RVA: 0x0000490A File Offset: 0x00002B0A
		public SAFTHeader()
		{
			this._isValid = true;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600012C RID: 300 RVA: 0x0000491C File Offset: 0x00002B1C
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00004934 File Offset: 0x00002B34
		public string AuditFileVersion
		{
			get
			{
				return this._auditFileVersion;
			}
			set
			{
				this.ValidateData(value);
				this._auditFileVersion = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00004948 File Offset: 0x00002B48
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00004960 File Offset: 0x00002B60
		public CompanyAddress CompanyAddress
		{
			get
			{
				return this._companyAddress;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					this.IsValid = false;
				}
				this._companyAddress = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00004988 File Offset: 0x00002B88
		// (set) Token: 0x06000131 RID: 305 RVA: 0x000049A0 File Offset: 0x00002BA0
		public string CompanyID
		{
			get
			{
				return this._companyID;
			}
			set
			{
				this.ValidateData(value);
				this._companyID = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000049B4 File Offset: 0x00002BB4
		// (set) Token: 0x06000133 RID: 307 RVA: 0x000049CC File Offset: 0x00002BCC
		public string CompanyName
		{
			get
			{
				return this._companyName;
			}
			set
			{
				this.ValidateData(value);
				this._companyName = value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000049E0 File Offset: 0x00002BE0
		// (set) Token: 0x06000135 RID: 309 RVA: 0x000049F8 File Offset: 0x00002BF8
		public string CurrencyCode
		{
			get
			{
				return this._currencyCode;
			}
			set
			{
				this.ValidateData(value);
				this._currencyCode = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00004A0C File Offset: 0x00002C0C
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00004A24 File Offset: 0x00002C24
		public DateTime DateCreated
		{
			get
			{
				return this._dateCreated;
			}
			set
			{
				this.ValidateData(value.ToString());
				this._dateCreated = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00004A3C File Offset: 0x00002C3C
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00004A54 File Offset: 0x00002C54
		public bool IsValid
		{
			get
			{
				return this._isValid;
			}
			set
			{
				this._isValid = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00004A60 File Offset: 0x00002C60
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00004A78 File Offset: 0x00002C78
		public string ProductCompanyTaxID
		{
			get
			{
				return this._productCompanyTaxID;
			}
			set
			{
				this.ValidateData(value);
				this._productCompanyTaxID = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00004A8C File Offset: 0x00002C8C
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00004AA4 File Offset: 0x00002CA4
		public string ProductID
		{
			get
			{
				return this._productID;
			}
			set
			{
				this.ValidateData(value);
				this._productID = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00004AB8 File Offset: 0x00002CB8
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00004AD0 File Offset: 0x00002CD0
		public string ProductVersion
		{
			get
			{
				return this._productVersion;
			}
			set
			{
				this.ValidateData(value);
				this._productVersion = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00004AE4 File Offset: 0x00002CE4
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00004AFC File Offset: 0x00002CFC
		public string SoftwareCertificateNumber
		{
			get
			{
				return this._softwareCertificateNumber;
			}
			set
			{
				this.ValidateData(value);
				this._softwareCertificateNumber = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00004B10 File Offset: 0x00002D10
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00004B28 File Offset: 0x00002D28
		public string TaxAccountingBasis
		{
			get
			{
				return this._taxAccountingBasis;
			}
			set
			{
				this.ValidateData(value);
				this._taxAccountingBasis = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00004B3C File Offset: 0x00002D3C
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00004B54 File Offset: 0x00002D54
		public string TaxEntity
		{
			get
			{
				return this._taxEntity;
			}
			set
			{
				this.ValidateData(value);
				this._taxEntity = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00004B68 File Offset: 0x00002D68
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00004B80 File Offset: 0x00002D80
		public string TaxRegistrationNumber
		{
			get
			{
				return this._taxRegistrationNumber;
			}
			set
			{
				this.ValidateData(value);
				this._taxRegistrationNumber = value;
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004B94 File Offset: 0x00002D94
		private void ValidateData(string data)
		{
			bool flag = string.IsNullOrEmpty(data);
			if (flag)
			{
				this._isValid = false;
			}
		}

		// Token: 0x040000C9 RID: 201
		private string _auditFileVersion;

		// Token: 0x040000CA RID: 202
		private CompanyAddress _companyAddress;

		// Token: 0x040000CB RID: 203
		private string _companyID;

		// Token: 0x040000CC RID: 204
		private string _companyName;

		// Token: 0x040000CD RID: 205
		private string _currencyCode;

		// Token: 0x040000CE RID: 206
		private DateTime _dateCreated;

		// Token: 0x040000CF RID: 207
		private bool _isValid;

		// Token: 0x040000D0 RID: 208
		private string _productCompanyTaxID;

		// Token: 0x040000D1 RID: 209
		private string _productID;

		// Token: 0x040000D2 RID: 210
		private string _productVersion;

		// Token: 0x040000D3 RID: 211
		private string _softwareCertificateNumber;

		// Token: 0x040000D4 RID: 212
		private string _taxAccountingBasis;

		// Token: 0x040000D5 RID: 213
		private string _taxEntity;

		// Token: 0x040000D6 RID: 214
		private string _taxRegistrationNumber;
	}
}
