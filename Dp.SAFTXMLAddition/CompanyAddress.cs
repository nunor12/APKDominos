using System;

namespace Dp.SAFTXMLAddition
{
	// Token: 0x0200001A RID: 26
	public class CompanyAddress
	{
		// Token: 0x060000FD RID: 253 RVA: 0x000030BA File Offset: 0x000012BA
		public CompanyAddress()
		{
			this._isValid = true;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000030CC File Offset: 0x000012CC
		// (set) Token: 0x060000FF RID: 255 RVA: 0x000030E4 File Offset: 0x000012E4
		public string AddressDetail
		{
			get
			{
				return this._addressDetail;
			}
			set
			{
				this.ValidateData(value);
				this._addressDetail = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000100 RID: 256 RVA: 0x000030F8 File Offset: 0x000012F8
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00003110 File Offset: 0x00001310
		public string City
		{
			get
			{
				return this._city;
			}
			set
			{
				this.ValidateData(value);
				this._city = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00003124 File Offset: 0x00001324
		// (set) Token: 0x06000103 RID: 259 RVA: 0x0000313C File Offset: 0x0000133C
		public string Country
		{
			get
			{
				return this._country;
			}
			set
			{
				this.ValidateData(value);
				this._country = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00003150 File Offset: 0x00001350
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00003168 File Offset: 0x00001368
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

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00003174 File Offset: 0x00001374
		// (set) Token: 0x06000107 RID: 263 RVA: 0x0000318C File Offset: 0x0000138C
		public string PostalCode
		{
			get
			{
				return this._postalCode;
			}
			set
			{
				this.ValidateData(value);
				this._postalCode = value;
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000031A0 File Offset: 0x000013A0
		private void ValidateData(string data)
		{
			bool flag = string.IsNullOrEmpty(data);
			if (flag)
			{
				this._isValid = false;
			}
		}

		// Token: 0x0400008D RID: 141
		private string _addressDetail;

		// Token: 0x0400008E RID: 142
		private string _city;

		// Token: 0x0400008F RID: 143
		private string _country;

		// Token: 0x04000090 RID: 144
		private bool _isValid;

		// Token: 0x04000091 RID: 145
		private string _postalCode;
	}
}
