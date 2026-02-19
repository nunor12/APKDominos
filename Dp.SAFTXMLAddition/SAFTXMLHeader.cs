using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dominos.LogForPulse;
using Dominos.Pulse;

namespace Dp.SAFTXMLAddition
{
	// Token: 0x02000020 RID: 32
	public class SAFTXMLHeader
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00007A04 File Offset: 0x00005C04
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00007A1C File Offset: 0x00005C1C
		public Dictionary<string, string> SaftDetails
		{
			get
			{
				return this._saftDetails;
			}
			set
			{
				this._saftDetails = value;
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00007A28 File Offset: 0x00005C28
		private string GetValue(string key)
		{
			string empty = string.Empty;
			this.SaftDetails.TryGetValue(key, out empty);
			return empty;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00007A50 File Offset: 0x00005C50
		public SAFTDate GetFiscalYear(DateTime startDate)
		{
			SAFTDate saftdate = new SAFTDate();
			try
			{
				using (SqlConnection sqlConnection = PulseConnection.CreateSQLConnection())
				{
					sqlConnection.Open();
					using (SqlCommand sqlCommand = new SqlCommand("spGetFiscalYear", sqlConnection))
					{
						sqlCommand.CommandType = CommandType.StoredProcedure;
						sqlCommand.Parameters.AddWithValue("@LocationCode", SystemSettings.PulseContext.LocationCode);
						sqlCommand.Parameters.AddWithValue("@StartDate", startDate);
						using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
						{
							bool hasRows = sqlDataReader.HasRows;
							if (hasRows)
							{
								while (sqlDataReader.Read())
								{
									saftdate.FiscalYear = (string.IsNullOrEmpty(sqlDataReader["FiscalYear"].ToString()) ? string.Empty : sqlDataReader["FiscalYear"].ToString());
									saftdate.MaxEndDate = Convert.ToDateTime(sqlDataReader["MaxEndDate"]);
									saftdate.MinEndDate = Convert.ToDateTime(sqlDataReader["MinEndDate"]);
								}
							}
						}
					}
					sqlConnection.Close();
				}
			}
			catch (Exception e)
			{
				Logger.Error(SystemSettings.PulseContext.Language.GetText(SAFTXMLHeader.UnhandledExceptionInHeaderDetailsMessage), e);
			}
			return saftdate;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00007C04 File Offset: 0x00005E04
		public void GetSAFTDetails()
		{
			try
			{
				using (SqlConnection sqlConnection = PulseConnection.CreateSQLConnection())
				{
					sqlConnection.Open();
					using (SqlCommand sqlCommand = new SqlCommand("spGetSAFTDetails", sqlConnection))
					{
						this._saftDetails = new Dictionary<string, string>();
						sqlCommand.CommandType = CommandType.StoredProcedure;
						sqlCommand.Parameters.AddWithValue("@LocationCode", SystemSettings.PulseContext.LocationCode);
						using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
						{
							bool hasRows = sqlDataReader.HasRows;
							if (hasRows)
							{
								while (sqlDataReader.Read())
								{
									this._saftDetails.Add(sqlDataReader["Setting"].ToString(), sqlDataReader["Value"].ToString());
								}
							}
						}
					}
					sqlConnection.Close();
				}
			}
			catch (Exception e)
			{
				Logger.Error(SystemSettings.PulseContext.Language.GetText(SAFTXMLHeader.UnhandledExceptionInSaftDetailsMessage), e);
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00007D38 File Offset: 0x00005F38
		public SAFTHeader GetSAFTXMLHeaderDetails()
		{
			SAFTHeader saftheader = new SAFTHeader();
			this.GetSAFTDetails();
			try
			{
				using (SqlConnection sqlConnection = PulseConnection.CreateSQLConnection())
				{
					sqlConnection.Open();
					using (SqlCommand sqlCommand = new SqlCommand("spGetSAFTXMLHeaderDetails", sqlConnection))
					{
						sqlCommand.CommandType = CommandType.StoredProcedure;
						sqlCommand.Parameters.AddWithValue("@LocationCode", SystemSettings.PulseContext.LocationCode);
						using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
						{
							bool hasRows = sqlDataReader.HasRows;
							if (hasRows)
							{
								while (sqlDataReader.Read())
								{
									saftheader.AuditFileVersion = this.GetValue("AuditFileVersion");
									saftheader.CompanyID = this.GetValue("CompanyID");
									saftheader.TaxRegistrationNumber = this.GetValue("TaxRegistrationNumber");
									saftheader.TaxAccountingBasis = this.GetValue("TaxAccountingBasis");
									saftheader.CompanyName = this.GetValue("CompanyName");
									saftheader.CompanyAddress = new CompanyAddress();
									saftheader.CompanyAddress.AddressDetail = this.GetValue("AddressDetail");
									saftheader.CompanyAddress.City = this.GetValue("City");
									saftheader.CompanyAddress.Country = this.GetValue("Country");
									saftheader.CompanyAddress.PostalCode = this.GetValue("PostalCode");
									saftheader.CurrencyCode = this.GetValue("CurrencyCode");
									saftheader.DateCreated = DateTime.Now;
									saftheader.TaxEntity = (string.IsNullOrEmpty(sqlDataReader["LocationCode"].ToString()) ? string.Empty : sqlDataReader["LocationCode"].ToString());
									saftheader.ProductCompanyTaxID = this.GetValue("ProductCompanyTaxID");
									saftheader.SoftwareCertificateNumber = this.GetValue("SoftwareCertificateNumber");
									saftheader.ProductID = this.GetValue("ProductID");
									saftheader.ProductVersion = (string.IsNullOrEmpty(sqlDataReader["Version"].ToString()) ? string.Empty : sqlDataReader["Version"].ToString());
								}
							}
						}
					}
					sqlConnection.Close();
				}
			}
			catch (Exception e)
			{
				Logger.Error(SystemSettings.PulseContext.Language.GetText(SAFTXMLHeader.UnhandledExceptionInHeaderDetailsMessage), e);
			}
			return saftheader;
		}

		// Token: 0x040000EB RID: 235
		private static readonly int UnhandledExceptionInHeaderDetailsMessage = 51880;

		// Token: 0x040000EC RID: 236
		private static readonly int UnhandledExceptionInSaftDetailsMessage = 51879;

		// Token: 0x040000ED RID: 237
		private Dictionary<string, string> _saftDetails;
	}
}
