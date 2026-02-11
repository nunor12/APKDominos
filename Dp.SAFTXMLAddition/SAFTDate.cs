using System;

namespace Dp.SAFTXMLAddition
{
	// Token: 0x0200001D RID: 29
	public class SAFTDate
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000125 RID: 293 RVA: 0x000048A0 File Offset: 0x00002AA0
		// (set) Token: 0x06000126 RID: 294 RVA: 0x000048B8 File Offset: 0x00002AB8
		public string FiscalYear
		{
			get
			{
				return this._fiscalYear;
			}
			set
			{
				this._fiscalYear = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000127 RID: 295 RVA: 0x000048C4 File Offset: 0x00002AC4
		// (set) Token: 0x06000128 RID: 296 RVA: 0x000048DC File Offset: 0x00002ADC
		public DateTime MaxEndDate
		{
			get
			{
				return this._maxEndDate;
			}
			set
			{
				this._maxEndDate = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000048E8 File Offset: 0x00002AE8
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00004900 File Offset: 0x00002B00
		public DateTime MinEndDate
		{
			get
			{
				return this._minEndDate;
			}
			set
			{
				this._minEndDate = value;
			}
		}

		// Token: 0x040000C6 RID: 198
		private string _fiscalYear;

		// Token: 0x040000C7 RID: 199
		private DateTime _maxEndDate;

		// Token: 0x040000C8 RID: 200
		private DateTime _minEndDate;
	}
}
