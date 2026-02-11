using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using Dominos.Core;
using Dominos.LogForPulse;
using Dominos.Pulse;

namespace Dp.SAFTXMLAddition
{
	// Token: 0x0200001F RID: 31
	public class SAFTXMLDetails
	{
		// Token: 0x06000149 RID: 329 RVA: 0x00004BB5 File Offset: 0x00002DB5
		public SAFTXMLDetails()
		{
			this._saftXMLAuditFile = new AuditFile();
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00004BCC File Offset: 0x00002DCC
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00004BE4 File Offset: 0x00002DE4
		public AuditFile SAFTXMLAuditFile
		{
			get
			{
				return this._saftXMLAuditFile;
			}
			set
			{
				this._saftXMLAuditFile = value;
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004BF0 File Offset: 0x00002DF0
		private void GetCustomers()
		{
			string value = string.Join("|", this._invoiceNumbers);
			this._saftXMLAuditFile.MasterFiles = new AuditFileMasterFiles();
			List<string> list = new List<string>();
			try
			{
				using (SqlConnection sqlConnection = PulseConnection.CreateSQLConnection())
				{
					sqlConnection.Open();
					using (SqlCommand sqlCommand = new SqlCommand("spGetCustomersForSAFTXML", sqlConnection))
					{
						sqlCommand.CommandType = CommandType.StoredProcedure;
						sqlCommand.CommandTimeout = 0;
						sqlCommand.Parameters.AddWithValue("@LocationCode", SystemSettings.PulseContext.LocationCode);
						sqlCommand.Parameters.AddWithValue("@InvoiceNumbers", value);
						using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
						{
							ArrayList arrayList = new ArrayList();
							string a = string.Empty;
							string a2 = string.Empty;
							int num = 0;
							int num2 = 0;
							bool hasRows = sqlDataReader.HasRows;
							if (hasRows)
							{
								while (sqlDataReader.Read())
								{
									bool flag = a != sqlDataReader["Customer_Code"].ToString();
									if (flag)
									{
										Customer customer = new Customer();
										customer.BillingAddress = new AddressStructure();
										customer.AccountID = this.GetValue("AccountID");
										customer.SelfBillingIndicator = this.GetValue("SelfBillingIndicator");
										customer.BillingAddress.Country = this.GetValue("Country");
										bool flag2 = string.IsNullOrEmpty(sqlDataReader["Customer_Code"].ToString()) || sqlDataReader["Customer_Code"].ToString() == SAFTXMLDetails.GenericCustomerCode || (string.IsNullOrEmpty(sqlDataReader["TaxID"].ToString()) && num2 < 1);
										if (flag2)
										{
											customer.CustomerID = this.GetValue("GenericCustomer");
											customer.CustomerTaxID = this.GetValue("CustomerTaxId");
											customer.CompanyName = this.GetValue("GenericCustomer");
											customer.BillingAddress.AddressDetail = this.GetValue("AccountID");
											customer.BillingAddress.City = this.GetValue("AccountID");
											customer.BillingAddress.PostalCode = this.GetValue("AccountID");
											num2++;
											arrayList.Add(customer);
											num++;
										}
										else
										{
											bool flag3 = !string.IsNullOrEmpty(sqlDataReader["TaxID"].ToString());
											if (flag3)
											{
												customer.CustomerID = sqlDataReader["Customer_Code"].ToString();
												list.Add(customer.CustomerID);
												customer.CustomerTaxID = sqlDataReader["TaxID"].ToString();
												customer.CompanyName = (string.IsNullOrEmpty(sqlDataReader["Name"].ToString().Trim()) ? sqlDataReader["Customer_Name"].ToString() : sqlDataReader["Name"].ToString());
												bool flag4 = string.IsNullOrEmpty(sqlDataReader["StreetNumber"].ToString().Trim()) && string.IsNullOrEmpty(sqlDataReader["StreetName"].ToString());
												if (flag4)
												{
													bool flag5 = string.IsNullOrEmpty(sqlDataReader["OrderStreetNumber"].ToString().Trim()) && string.IsNullOrEmpty(sqlDataReader["OrderStreetName"].ToString());
													if (flag5)
													{
														customer.BillingAddress.AddressDetail = this.GetValue("AccountID");
													}
													else
													{
														bool flag6 = string.IsNullOrEmpty(sqlDataReader["OrderStreetNumber"].ToString().Trim());
														if (flag6)
														{
															customer.BillingAddress.AddressDetail = sqlDataReader["OrderStreetName"].ToString();
														}
														else
														{
															bool flag7 = string.IsNullOrEmpty(sqlDataReader["OrderStreetName"].ToString().Trim());
															if (flag7)
															{
																customer.BillingAddress.AddressDetail = sqlDataReader["OrderStreetNumber"].ToString();
															}
															else
															{
																customer.BillingAddress.AddressDetail = sqlDataReader["OrderStreetNumber"].ToString() + ' ' + sqlDataReader["OrderStreetName"].ToString();
															}
														}
													}
												}
												else
												{
													bool flag8 = string.IsNullOrEmpty(sqlDataReader["StreetNumber"].ToString().Trim());
													if (flag8)
													{
														customer.BillingAddress.AddressDetail = sqlDataReader["StreetName"].ToString();
													}
													else
													{
														bool flag9 = string.IsNullOrEmpty(sqlDataReader["StreetName"].ToString().Trim());
														if (flag9)
														{
															customer.BillingAddress.AddressDetail = sqlDataReader["StreetNumber"].ToString();
														}
														else
														{
															customer.BillingAddress.AddressDetail = sqlDataReader["StreetNumber"].ToString() + ' ' + sqlDataReader["StreetName"].ToString();
														}
													}
												}
												customer.BillingAddress.City = (string.IsNullOrEmpty(sqlDataReader["City"].ToString().Trim()) ? (string.IsNullOrEmpty(sqlDataReader["OrderCityName"].ToString().Trim()) ? this.GetValue("AccountID") : sqlDataReader["OrderCityName"].ToString()) : sqlDataReader["City"].ToString());
												customer.BillingAddress.PostalCode = (string.IsNullOrEmpty(sqlDataReader["Postal"].ToString().Trim()) ? (string.IsNullOrEmpty(sqlDataReader["OrderPostalCode"].ToString().Trim()) ? this.GetValue("AccountID") : sqlDataReader["OrderPostalCode"].ToString()) : sqlDataReader["Postal"].ToString());
												arrayList.Add(customer);
												num++;
											}
										}
									}
									else
									{
										bool flag10 = a2 != sqlDataReader["TaxID"].ToString() && num2 < 1;
										if (flag10)
										{
											Customer customer2 = new Customer();
											customer2.BillingAddress = new AddressStructure();
											customer2.AccountID = this.GetValue("AccountID");
											customer2.SelfBillingIndicator = this.GetValue("SelfBillingIndicator");
											customer2.BillingAddress.Country = this.GetValue("Country");
											customer2.CustomerID = this.GetValue("GenericCustomer");
											customer2.CustomerTaxID = this.GetValue("CustomerTaxId");
											customer2.CompanyName = this.GetValue("GenericCustomer");
											customer2.BillingAddress.AddressDetail = this.GetValue("AccountID");
											customer2.BillingAddress.City = this.GetValue("AccountID");
											customer2.BillingAddress.PostalCode = this.GetValue("AccountID");
											num2++;
											arrayList.Add(customer2);
											num++;
										}
									}
									a = (string.IsNullOrEmpty(sqlDataReader["Customer_Code"].ToString()) ? string.Empty : sqlDataReader["Customer_Code"].ToString());
									a2 = (string.IsNullOrEmpty(sqlDataReader["TaxID"].ToString()) ? string.Empty : sqlDataReader["TaxID"].ToString());
								}
								Dictionary<string, Collection<AddressStructure>> dictionary = new Dictionary<string, Collection<AddressStructure>>();
								dictionary = this.FetchShipToAddressesForCustomers(list);
								foreach (object obj in arrayList)
								{
									Customer customer3 = (Customer)obj;
									Collection<AddressStructure> shipToAddress = new Collection<AddressStructure>();
									dictionary.TryGetValue(customer3.CustomerID, out shipToAddress);
									customer3.ShipToAddress = shipToAddress;
								}
								this._saftXMLAuditFile.MasterFiles.Customer = new Customer[num];
								int num3 = 0;
								foreach (object obj2 in arrayList)
								{
									Customer customer4 = (Customer)obj2;
									bool flag11 = num3 != num;
									if (flag11)
									{
										this._saftXMLAuditFile.MasterFiles.Customer[num3] = customer4;
										num3++;
									}
								}
							}
						}
					}
					sqlConnection.Close();
				}
			}
			catch (Exception e)
			{
				Logger.Error(SystemSettings.PulseContext.Language.GetText(SAFTXMLDetails.UnhandledExceptionInSaftDetailsMessage), e);
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000557C File Offset: 0x0000377C
		private Dictionary<string, Collection<AddressStructure>> FetchShipToAddressesForCustomers(List<string> customerIds)
		{
			string value = string.Join("|", customerIds);
			string value2 = string.Join("|", this._invoiceNumbers);
			Dictionary<string, Collection<AddressStructure>> dictionary = new Dictionary<string, Collection<AddressStructure>>();
			Collection<AddressStructure> collection = new Collection<AddressStructure>();
			Collection<AddressStructure> value3 = new Collection<AddressStructure>();
			try
			{
				using (SqlConnection sqlConnection = PulseConnection.CreateSQLConnection())
				{
					sqlConnection.Open();
					using (SqlCommand sqlCommand = new SqlCommand("spGetShipToAddressesForSAFTCustomers", sqlConnection))
					{
						sqlCommand.CommandType = CommandType.StoredProcedure;
						sqlCommand.Parameters.AddWithValue("@LocationCode", SystemSettings.PulseContext.LocationCode);
						sqlCommand.Parameters.AddWithValue("@CustomerIds", value);
						sqlCommand.Parameters.AddWithValue("@InvoiceNumbers", value2);
						using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
						{
							string text = string.Empty;
							string key = string.Empty;
							bool hasRows = sqlDataReader.HasRows;
							if (hasRows)
							{
								while (sqlDataReader.Read())
								{
									AddressStructure addressStructure = new AddressStructure();
									addressStructure.AddressDetail = sqlDataReader["StreetNumber"].ToString() + ' ' + sqlDataReader["StreetName"].ToString();
									addressStructure.Country = this.GetValue("Country");
									addressStructure.PostalCode = sqlDataReader["Postal"].ToString();
									addressStructure.City = sqlDataReader["City"].ToString();
									bool flag = text == string.Empty || sqlDataReader["CustomerCode"].ToString() == text;
									if (flag)
									{
										key = sqlDataReader["CustomerCode"].ToString();
										bool flag2 = !string.IsNullOrEmpty(sqlDataReader["CustomerCode"].ToString());
										if (flag2)
										{
											collection.Add(addressStructure);
										}
									}
									else
									{
										bool flag3 = text != string.Empty && sqlDataReader["CustomerCode"].ToString() != text;
										if (flag3)
										{
											key = sqlDataReader["CustomerCode"].ToString();
											value3 = collection;
											collection = new Collection<AddressStructure>();
											collection.Add(addressStructure);
											dictionary.Add(text, value3);
										}
									}
									text = sqlDataReader["CustomerCode"].ToString();
								}
								dictionary.Add(key, collection);
							}
						}
					}
					sqlConnection.Close();
				}
			}
			catch (Exception e)
			{
				Logger.Error(SystemSettings.PulseContext.Language.GetText(SAFTXMLDetails.UnhandledExceptionInSaftDetailsMessage), e);
			}
			return dictionary;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000058A0 File Offset: 0x00003AA0
		private bool GetHeader(DateTime startDate, DateTime endDate)
		{
			SAFTHeader saftheader = new SAFTHeader();
			saftheader = this._saftXMLHeader.GetSAFTXMLHeaderDetails();
			this._saftXMLAuditFile.Header = new Header();
			this._saftDetails = this._saftXMLHeader.SaftDetails;
			this._saftXMLAuditFile.Header.CompanyID = saftheader.CompanyID;
			this._saftXMLAuditFile.Header.TaxRegistrationNumber = saftheader.TaxRegistrationNumber;
			this._saftXMLAuditFile.Header.TaxAccountingBasis = saftheader.TaxAccountingBasis;
			this._saftXMLAuditFile.Header.CompanyName = saftheader.CompanyName;
			this._saftXMLAuditFile.Header.CompanyAddress = new AddressStructurePT();
			this._saftXMLAuditFile.Header.CompanyAddress.AddressDetail = saftheader.CompanyAddress.AddressDetail.Trim();
			this._saftXMLAuditFile.Header.CompanyAddress.City = saftheader.CompanyAddress.City.Trim();
			this._saftXMLAuditFile.Header.CompanyAddress.Country = saftheader.CompanyAddress.Country.Trim();
			this._saftXMLAuditFile.Header.CompanyAddress.PostalCode = saftheader.CompanyAddress.PostalCode.Trim();
			this._saftXMLAuditFile.Header.CurrencyCode = saftheader.CurrencyCode;
			this._saftXMLAuditFile.Header.StartDate = Convert.ToDateTime(startDate.Date.ToString(SAFTXMLDetails.DateFormat));
			this._saftXMLAuditFile.Header.EndDate = Convert.ToDateTime(endDate.Date.ToString(SAFTXMLDetails.DateFormat));
			this._saftXMLAuditFile.Header.FiscalYear = this._saftXMLHeader.GetFiscalYear(startDate).FiscalYear;
			this._saftXMLAuditFile.Header.DateCreated = Convert.ToDateTime(saftheader.DateCreated.Date.ToString(SAFTXMLDetails.DateFormat));
			this._saftXMLAuditFile.Header.TaxEntity = saftheader.TaxEntity;
			this._saftXMLAuditFile.Header.ProductCompanyTaxID = saftheader.ProductCompanyTaxID;
			this._saftXMLAuditFile.Header.SoftwareCertificateNumber = saftheader.SoftwareCertificateNumber;
			this._saftXMLAuditFile.Header.ProductID = saftheader.ProductID;
			this._saftXMLAuditFile.Header.ProductVersion = saftheader.ProductVersion;
			this._saftXMLAuditFile.Header.AuditFileVersion = saftheader.AuditFileVersion;
			return saftheader.IsValid && saftheader.CompanyAddress.IsValid;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005B5C File Offset: 0x00003D5C
		private void GetInvoices(DateTime startDate, DateTime endDate)
		{
			try
			{
				using (SqlConnection sqlConnection = PulseConnection.CreateSQLConnection())
				{
					sqlConnection.Open();
					using (SqlCommand sqlCommand = new SqlCommand("spGetInvoicesForSAFTXML", sqlConnection))
					{
						sqlCommand.CommandType = CommandType.StoredProcedure;
						sqlCommand.Parameters.AddWithValue("@LocationCode", SystemSettings.PulseContext.LocationCode);
						sqlCommand.Parameters.AddWithValue("@StartDate", startDate);
						sqlCommand.Parameters.AddWithValue("@EndDate", endDate);
						using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
						{
							ArrayList arrayList = new ArrayList();
							ArrayList arrayList2 = new ArrayList();
							ArrayList arrayList3 = new ArrayList();
							ArrayList arrayList4 = new ArrayList();
							ArrayList arrayList5 = new ArrayList();
							SourceDocumentsSalesInvoicesInvoice sourceDocumentsSalesInvoicesInvoice = new SourceDocumentsSalesInvoicesInvoice();
							SourceDocumentsSalesInvoicesInvoice sourceDocumentsSalesInvoicesInvoice2 = new SourceDocumentsSalesInvoicesInvoice();
							decimal num = 0m;
							decimal num2 = 0m;
							decimal num3 = 0m;
							decimal d = 0m;
							string text = string.Empty;
							int num4 = 0;
							int num5 = 0;
							string text2 = string.Empty;
							bool flag = false;
							bool flag2 = false;
							this._taxPercentagesCount = 0;
							this._taxPercentages = new Collection<decimal>();
							this._invoiceNumbers = new Collection<string>();
							this._productCodes = new Collection<string>();
							this._saftXMLAuditFile.SourceDocuments = new SourceDocuments();
							this._saftXMLAuditFile.SourceDocuments.SalesInvoices = new SourceDocumentsSalesInvoices();
							int num6 = 0;
							int num7 = 0;
							int num8 = 0;
							bool hasRows = sqlDataReader.HasRows;
							if (hasRows)
							{
								while (sqlDataReader.Read())
								{
									sourceDocumentsSalesInvoicesInvoice = new SourceDocumentsSalesInvoicesInvoice();
									sourceDocumentsSalesInvoicesInvoice.InvoiceNo = (string.IsNullOrEmpty(sqlDataReader["InvoiceNumber"].ToString()) ? string.Empty : sqlDataReader["InvoiceNumber"].ToString());
									sourceDocumentsSalesInvoicesInvoice.ATCUD = "0";
									DateTime t = new DateTime(2023, 1, 1);
									bool flag3 = sqlDataReader["VerificationCode"] != DBNull.Value && sqlDataReader["InvoiceNumber"] != DBNull.Value && startDate >= t;
									if (flag3)
									{
										sourceDocumentsSalesInvoicesInvoice.ATCUD = this.GetATCUD(sqlDataReader["VerificationCode"].ToString(), sqlDataReader["InvoiceNumber"].ToString());
									}
									sourceDocumentsSalesInvoicesInvoice.DocumentStatus = new SourceDocumentsSalesInvoicesInvoiceDocumentStatus();
									sourceDocumentsSalesInvoicesInvoice.DocumentStatus.InvoiceStatus = this.GetValue("InvoiceStatus");
									bool flag4 = sqlDataReader["ActualOrderDate"] != DBNull.Value;
									if (flag4)
									{
										sourceDocumentsSalesInvoicesInvoice.DocumentStatus.InvoiceStatusDate = Convert.ToDateTime(sqlDataReader["ActualOrderDate"]);
										sourceDocumentsSalesInvoicesInvoice.SystemEntryDate = Convert.ToDateTime(sqlDataReader["ActualOrderDate"]);
									}
									else
									{
										sourceDocumentsSalesInvoicesInvoice.DocumentStatus.InvoiceStatusDate = DateTime.MinValue;
										sourceDocumentsSalesInvoicesInvoice.SystemEntryDate = DateTime.MinValue;
									}
									sourceDocumentsSalesInvoicesInvoice.DocumentStatus.SourceID = (string.IsNullOrEmpty(sqlDataReader["OrdPayUpdateUserCode"].ToString()) ? sqlDataReader["Added_By"].ToString() : sqlDataReader["OrdPayUpdateUserCode"].ToString());
									sourceDocumentsSalesInvoicesInvoice.Hash = (string.IsNullOrEmpty(sqlDataReader["HashValue"].ToString()) ? string.Empty : sqlDataReader["HashValue"].ToString());
									bool flag5 = string.IsNullOrEmpty(sqlDataReader["ManualInvoiceReferenceNumber"].ToString());
									if (flag5)
									{
										sourceDocumentsSalesInvoicesInvoice.HashControl = this.GetValue("HashControl");
									}
									else
									{
										sourceDocumentsSalesInvoicesInvoice.HashControl = this.GetValue("HashControl") + SAFTXMLDetails.Hiphen + sqlDataReader["ManualInvoiceReferenceNumber"].ToString();
									}
									bool flag6 = Convert.ToBoolean(sqlDataReader["IsManual"]);
									if (flag6)
									{
										sourceDocumentsSalesInvoicesInvoice.DocumentStatus.SourceBilling = SAFTPTSourceBilling.M.ToString();
									}
									else
									{
										sourceDocumentsSalesInvoicesInvoice.DocumentStatus.SourceBilling = SAFTPTSourceBilling.P.ToString();
									}
									bool flag7 = sqlDataReader["OrderDate"] != DBNull.Value;
									if (flag7)
									{
										sourceDocumentsSalesInvoicesInvoice.InvoiceDate = Convert.ToDateTime(sqlDataReader["OrderDate"]);
									}
									else
									{
										sourceDocumentsSalesInvoicesInvoice.InvoiceDate = DateTime.MinValue;
									}
									bool flag8 = Convert.ToBoolean(sqlDataReader["IsDelivery"]) && !string.IsNullOrEmpty(sqlDataReader["IsOfficial"].ToString());
									if (flag8)
									{
										sourceDocumentsSalesInvoicesInvoice.ShipTo = new ShippingPointStructure();
										sourceDocumentsSalesInvoicesInvoice.ShipFrom = new ShippingPointStructure();
										sourceDocumentsSalesInvoicesInvoice.ShipTo.Address = new AddressStructure();
										sourceDocumentsSalesInvoicesInvoice.ShipFrom.Address = new AddressStructure();
										sourceDocumentsSalesInvoicesInvoice.ShipTo.Address.AddressDetail = ((string.IsNullOrEmpty(sqlDataReader["StreetNumber"].ToString().Trim()) && string.IsNullOrEmpty(sqlDataReader["StreetName"].ToString())) ? this.GetValue("AccountID") : (sqlDataReader["StreetNumber"].ToString() + ' ' + sqlDataReader["StreetName"].ToString()));
										sourceDocumentsSalesInvoicesInvoice.ShipTo.Address.City = (string.IsNullOrEmpty(sqlDataReader["City"].ToString().Trim()) ? this.GetValue("AccountID") : sqlDataReader["City"].ToString());
										sourceDocumentsSalesInvoicesInvoice.ShipTo.Address.Country = this.GetValue("Country");
										sourceDocumentsSalesInvoicesInvoice.ShipTo.Address.PostalCode = (string.IsNullOrEmpty(sqlDataReader["Postal"].ToString().Trim()) ? this.GetValue("AccountID") : sqlDataReader["Postal"].ToString());
										sourceDocumentsSalesInvoicesInvoice.ShipFrom.Address.AddressDetail = this._saftXMLAuditFile.Header.CompanyAddress.AddressDetail;
										sourceDocumentsSalesInvoicesInvoice.ShipFrom.Address.City = this._saftXMLAuditFile.Header.CompanyAddress.City;
										sourceDocumentsSalesInvoicesInvoice.ShipFrom.Address.PostalCode = this._saftXMLAuditFile.Header.CompanyAddress.PostalCode;
										sourceDocumentsSalesInvoicesInvoice.ShipFrom.Address.Country = this._saftXMLAuditFile.Header.CompanyAddress.Country;
										sourceDocumentsSalesInvoicesInvoice.MovementStartTime = Convert.ToDateTime(sqlDataReader["ActualOrderDate"]).ToString(SAFTXMLDetails.DateTimeFormat);
									}
									sourceDocumentsSalesInvoicesInvoice.InvoiceType = (string.IsNullOrEmpty(sqlDataReader["InvoiceSeriesCode"].ToString()) ? string.Empty : sqlDataReader["InvoiceSeriesCode"].ToString().Substring(0, 2));
									sourceDocumentsSalesInvoicesInvoice.SpecialRegimes = new SpecialRegimes();
									sourceDocumentsSalesInvoicesInvoice.SpecialRegimes.CashVATSchemeIndicator = this.GetValue("CashVATSchemeIndicator");
									sourceDocumentsSalesInvoicesInvoice.SpecialRegimes.SelfBillingIndicator = this.GetValue("SelfBillingIndicator");
									sourceDocumentsSalesInvoicesInvoice.SpecialRegimes.ThirdPartiesBillingIndicator = this.GetValue("ThirdPartiesBillingIndicator");
									sourceDocumentsSalesInvoicesInvoice.SourceID = (string.IsNullOrEmpty(sqlDataReader["OrdPayCSRCode"].ToString()) ? sqlDataReader["Added_By"].ToString() : sqlDataReader["OrdPayCSRCode"].ToString());
									sourceDocumentsSalesInvoicesInvoice.CustomerID = ((string.IsNullOrEmpty(sqlDataReader["Customer_Code"].ToString()) || sqlDataReader["Customer_Code"].ToString() == "0" || string.IsNullOrEmpty(sqlDataReader["TaxID"].ToString())) ? this.GetValue("GenericCustomer") : sqlDataReader["Customer_Code"].ToString());
									this._invoiceNumbers.Add(string.IsNullOrEmpty(sqlDataReader["InvoiceNumber"].ToString()) ? string.Empty : sqlDataReader["InvoiceNumber"].ToString());
									this._productCodes.Add(string.IsNullOrEmpty(sqlDataReader["ProductCode"].ToString()) ? string.Empty : sqlDataReader["ProductCode"].ToString());
									sourceDocumentsSalesInvoicesInvoice.DocumentTotals = new SourceDocumentsSalesInvoicesInvoiceDocumentTotals();
									sourceDocumentsSalesInvoicesInvoice.DocumentTotals.NetTotal = decimal.Round(Convert.ToDecimal(sqlDataReader["SubTotal"]), 2);
									sourceDocumentsSalesInvoicesInvoice.DocumentTotals.TaxPayable = decimal.Round(Convert.ToDecimal(sqlDataReader["Sales_Tax1"]), 2);
									sourceDocumentsSalesInvoicesInvoice.DocumentTotals.GrossTotal = decimal.Round(Convert.ToDecimal(sqlDataReader["OrderFinalPrice"]), 2);
									PaymentMethod paymentMethod = new PaymentMethod();
									SourceDocumentsSalesInvoicesInvoiceLine sourceDocumentsSalesInvoicesInvoiceLine = new SourceDocumentsSalesInvoicesInvoiceLine();
									bool flag9 = !string.IsNullOrEmpty(sqlDataReader["InvoiceNumber"].ToString()) && text != sqlDataReader["InvoiceNumber"].ToString();
									if (flag9)
									{
										num4 = num7;
										num5 = num8;
										arrayList2 = new ArrayList();
										arrayList4 = new ArrayList();
										num7 = 0;
										num8 = 0;
										text2 = string.Empty;
										d = num3;
										flag2 = flag;
									}
									flag = !string.IsNullOrEmpty(sqlDataReader["IsOfficial"].ToString());
									bool flag10 = decimal.Round(Convert.ToDecimal(sqlDataReader["DiscountAmount"]), 2) > 0m;
									if (flag10)
									{
										num3 = decimal.Round(Convert.ToDecimal(sqlDataReader["DiscountAmount"]), 4);
									}
									else
									{
										num3 = 0m;
									}
									sourceDocumentsSalesInvoicesInvoiceLine.LineNumber = (string.IsNullOrEmpty(sqlDataReader["Line_Number"].ToString()) ? string.Empty : sqlDataReader["Line_Number"].ToString());
									sourceDocumentsSalesInvoicesInvoiceLine.ProductCode = (string.IsNullOrEmpty(sqlDataReader["ProductCode"].ToString()) ? string.Empty : sqlDataReader["ProductCode"].ToString());
									sourceDocumentsSalesInvoicesInvoiceLine.ProductDescription = (string.IsNullOrEmpty(sqlDataReader["ProdTextProductDesc"].ToString()) ? sourceDocumentsSalesInvoicesInvoiceLine.ProductCode : sqlDataReader["ProdTextProductDesc"].ToString());
									sourceDocumentsSalesInvoicesInvoiceLine.Quantity = Convert.ToDecimal(sqlDataReader["Quantity"]);
									sourceDocumentsSalesInvoicesInvoiceLine.UnitOfMeasure = this.GetValue("UnitOfMeasure");
									sourceDocumentsSalesInvoicesInvoiceLine.Description = SystemSettings.PulseContext.Language.GetText(SAFTXMLDetails.OrderNumberStringKey) + (string.IsNullOrEmpty(sqlDataReader["Order_Number"].ToString()) ? string.Empty : sqlDataReader["Order_Number"].ToString());
									bool flag11 = string.IsNullOrEmpty(sqlDataReader["IsOfficial"].ToString());
									if (flag11)
									{
										sourceDocumentsSalesInvoicesInvoiceLine.ItemElementName = ItemChoiceType1.DebitAmount;
										sourceDocumentsSalesInvoicesInvoiceLine.Item = decimal.Round(Convert.ToDecimal(sqlDataReader["OrdLineFinalPrice"]) - Convert.ToDecimal(sqlDataReader["OrdLineTaxAmt"]), 4);
										sourceDocumentsSalesInvoicesInvoiceLine.UnitPrice = decimal.Round(sourceDocumentsSalesInvoicesInvoiceLine.Item / sourceDocumentsSalesInvoicesInvoiceLine.Quantity, 4);
										sourceDocumentsSalesInvoicesInvoiceLine.References = new References[1];
										sourceDocumentsSalesInvoicesInvoiceLine.References[0] = new References();
										sourceDocumentsSalesInvoicesInvoiceLine.References[0].Reference = (Convert.ToBoolean(sqlDataReader["IsManual"]) ? sqlDataReader["ManualInvoiceReferenceNumber"].ToString() : sqlDataReader["CreditReferenceNumber"].ToString());
										bool flag12 = !string.IsNullOrEmpty(sqlDataReader["InvoiceNumber"].ToString()) && text != sqlDataReader["InvoiceNumber"].ToString();
										if (flag12)
										{
											num += sourceDocumentsSalesInvoicesInvoice.DocumentTotals.NetTotal;
										}
									}
									else
									{
										sourceDocumentsSalesInvoicesInvoiceLine.ItemElementName = ItemChoiceType1.CreditAmount;
										sourceDocumentsSalesInvoicesInvoiceLine.Item = decimal.Round(Convert.ToDecimal(sqlDataReader["OrdLineFinalPrice"]) - Convert.ToDecimal(sqlDataReader["OrdLineTaxAmt"]), 4);
										sourceDocumentsSalesInvoicesInvoiceLine.UnitPrice = decimal.Round(sourceDocumentsSalesInvoicesInvoiceLine.Item / sourceDocumentsSalesInvoicesInvoiceLine.Quantity, 4);
										bool flag13 = !string.IsNullOrEmpty(sqlDataReader["InvoiceNumber"].ToString()) && text != sqlDataReader["InvoiceNumber"].ToString();
										if (flag13)
										{
											num2 += sourceDocumentsSalesInvoicesInvoice.DocumentTotals.NetTotal;
										}
										bool flag14 = decimal.Round(Convert.ToDecimal(sqlDataReader["LineDiscountAmount"]), 4) < 0m;
										if (flag14)
										{
											sourceDocumentsSalesInvoicesInvoiceLine.SettlementAmountSpecified = true;
											sourceDocumentsSalesInvoicesInvoiceLine.SettlementAmount = Math.Abs(decimal.Round(Convert.ToDecimal(sqlDataReader["LineDiscountAmount"]), 4));
										}
									}
									bool flag15 = sqlDataReader["OrderDate"] != DBNull.Value;
									if (flag15)
									{
										sourceDocumentsSalesInvoicesInvoiceLine.TaxPointDate = Convert.ToDateTime(sqlDataReader["OrderDate"]);
									}
									else
									{
										sourceDocumentsSalesInvoicesInvoiceLine.TaxPointDate = DateTime.MinValue;
									}
									sourceDocumentsSalesInvoicesInvoiceLine.Tax = new Tax();
									bool flag16 = this._taxRates.ContainsKey(Convert.ToDecimal(sqlDataReader["TaxRate"]));
									if (flag16)
									{
										sourceDocumentsSalesInvoicesInvoiceLine.Tax.TaxCode = this._taxRates[Convert.ToDecimal(sqlDataReader["TaxRate"])];
									}
									else
									{
										sourceDocumentsSalesInvoicesInvoiceLine.Tax.TaxCode = SAFTXMLDetails.DefaultTaxCode;
									}
									sourceDocumentsSalesInvoicesInvoiceLine.Tax.TaxCountryRegion = this.GetValue("TaxCountryRegion");
									sourceDocumentsSalesInvoicesInvoiceLine.Tax.TaxType = this.GetValue("TaxType");
									sourceDocumentsSalesInvoicesInvoiceLine.Tax.ItemElementName = ItemChoiceType.TaxPercentage;
									sourceDocumentsSalesInvoicesInvoiceLine.Tax.Item = decimal.Round(Convert.ToDecimal(string.Format(SAFTXMLDetails.PercentageFormat, Convert.ToDecimal(sqlDataReader["TaxRate"]))), 2);
									bool flag17 = !this._taxPercentages.Contains(Convert.ToDecimal(sqlDataReader["TaxRate"]));
									if (flag17)
									{
										this._taxPercentages.Add(decimal.Round(Convert.ToDecimal(string.Format(SAFTXMLDetails.PercentageFormat, Convert.ToDecimal(sqlDataReader["TaxRate"]))), 2));
										this._taxPercentagesCount++;
									}
									decimal num9 = (sqlDataReader["OrdPayAmt"] == DBNull.Value) ? 0m : Convert.ToDecimal(sqlDataReader["OrdPayAmt"]);
									bool flag18 = num9 >= 0m && !string.IsNullOrEmpty(sqlDataReader["OrderPayTypeCode"].ToString()) && !string.IsNullOrEmpty(sqlDataReader["IsOfficial"].ToString()) && !Convert.ToBoolean(sqlDataReader["OrdPayIsLater"]);
									if (flag18)
									{
										string text3 = Convert.ToString(sqlDataReader["OrderPayTypeCode"]);
										uint num10 = <PrivateImplementationDetails>.ComputeStringHash(text3);
										if (num10 <= 856466825U)
										{
											if (num10 != 806133968U)
											{
												if (num10 != 822911587U)
												{
													if (num10 != 856466825U)
													{
														goto IL_104B;
													}
													if (!(text3 == "6"))
													{
														goto IL_104B;
													}
													paymentMethod.PaymentMechanism = PaymentMechanism.CO;
												}
												else
												{
													if (!(text3 == "4"))
													{
														goto IL_104B;
													}
													paymentMethod.PaymentMechanism = PaymentMechanism.CC;
												}
											}
											else
											{
												if (!(text3 == "5"))
												{
													goto IL_104B;
												}
												paymentMethod.PaymentMechanism = PaymentMechanism.OU;
											}
										}
										else if (num10 <= 906799682U)
										{
											if (num10 != 873244444U)
											{
												if (num10 != 906799682U)
												{
													goto IL_104B;
												}
												if (!(text3 == "3"))
												{
													goto IL_104B;
												}
												paymentMethod.PaymentMechanism = PaymentMechanism.OU;
											}
											else
											{
												if (!(text3 == "1"))
												{
													goto IL_104B;
												}
												paymentMethod.PaymentMechanism = PaymentMechanism.NU;
											}
										}
										else if (num10 != 923577301U)
										{
											if (num10 != 2682644962U)
											{
												goto IL_104B;
											}
											if (!(text3 == "7 "))
											{
												goto IL_104B;
											}
											paymentMethod.PaymentMechanism = PaymentMechanism.TR;
										}
										else
										{
											if (!(text3 == "2"))
											{
												goto IL_104B;
											}
											paymentMethod.PaymentMechanism = PaymentMechanism.CH;
										}
										IL_1057:
										paymentMethod.PaymentAmount = decimal.Round(num9, 2);
										paymentMethod.PaymentDate = Convert.ToDateTime(sqlDataReader["InvoiceDate"]);
										bool flag19 = (sourceDocumentsSalesInvoicesInvoiceLine.LineNumber == text2 && sourceDocumentsSalesInvoicesInvoiceLine.LineNumber == "1") || string.IsNullOrEmpty(text2);
										if (flag19)
										{
											bool flag20 = ((flag && Convert.ToBoolean(sqlDataReader["IsOfficial"].ToString())) || !Convert.ToBoolean(sqlDataReader["IsOfficial"].ToString())) && string.IsNullOrEmpty(sqlDataReader["TaxID"].ToString());
											if (flag20)
											{
												arrayList4.Add(paymentMethod);
												num8++;
											}
										}
										goto IL_1117;
										IL_104B:
										paymentMethod.PaymentMechanism = PaymentMechanism.OU;
										goto IL_1057;
									}
									IL_1117:
									bool flag21 = sourceDocumentsSalesInvoicesInvoiceLine.LineNumber != text2;
									if (flag21)
									{
										arrayList2.Add(sourceDocumentsSalesInvoicesInvoiceLine);
										num7++;
									}
									text2 = sourceDocumentsSalesInvoicesInvoiceLine.LineNumber;
									bool flag22 = !string.IsNullOrEmpty(text) && text != sqlDataReader["InvoiceNumber"].ToString();
									if (flag22)
									{
										int num11 = 0;
										int num12 = 0;
										sourceDocumentsSalesInvoicesInvoice2.Line = new SourceDocumentsSalesInvoicesInvoiceLine[num4];
										sourceDocumentsSalesInvoicesInvoice2.DocumentTotals.Payment = new PaymentMethod[num5];
										foreach (object obj in arrayList5)
										{
											SourceDocumentsSalesInvoicesInvoiceLine sourceDocumentsSalesInvoicesInvoiceLine2 = (SourceDocumentsSalesInvoicesInvoiceLine)obj;
											bool flag23 = num11 != num4;
											if (flag23)
											{
												bool flag24 = d > 0m && flag2;
												if (flag24)
												{
													sourceDocumentsSalesInvoicesInvoiceLine2.SettlementAmountSpecified = true;
													sourceDocumentsSalesInvoicesInvoiceLine2.SettlementAmount = decimal.Round(sourceDocumentsSalesInvoicesInvoiceLine2.SettlementAmount + Math.Abs(d / num4), 4);
												}
												sourceDocumentsSalesInvoicesInvoice2.Line[num11] = sourceDocumentsSalesInvoicesInvoiceLine2;
												num11++;
											}
										}
										foreach (object obj2 in arrayList3)
										{
											PaymentMethod paymentMethod2 = (PaymentMethod)obj2;
											bool flag25 = num12 != num5;
											if (flag25)
											{
												sourceDocumentsSalesInvoicesInvoice2.DocumentTotals.Payment[num12] = paymentMethod2;
												num12++;
											}
										}
										arrayList.Add(sourceDocumentsSalesInvoicesInvoice2);
										num6++;
									}
									sourceDocumentsSalesInvoicesInvoice2 = sourceDocumentsSalesInvoicesInvoice;
									arrayList5 = arrayList2;
									arrayList3 = arrayList4;
									text = sourceDocumentsSalesInvoicesInvoice.InvoiceNo;
								}
								int num13 = 0;
								sourceDocumentsSalesInvoicesInvoice.Line = new SourceDocumentsSalesInvoicesInvoiceLine[num7];
								foreach (object obj3 in arrayList5)
								{
									SourceDocumentsSalesInvoicesInvoiceLine sourceDocumentsSalesInvoicesInvoiceLine3 = (SourceDocumentsSalesInvoicesInvoiceLine)obj3;
									bool flag26 = num13 != num7;
									if (flag26)
									{
										bool flag27 = num3 > 0m && flag;
										if (flag27)
										{
											sourceDocumentsSalesInvoicesInvoiceLine3.SettlementAmountSpecified = true;
											sourceDocumentsSalesInvoicesInvoiceLine3.SettlementAmount = decimal.Round(sourceDocumentsSalesInvoicesInvoiceLine3.SettlementAmount + Math.Abs(num3 / num7), 4);
										}
										sourceDocumentsSalesInvoicesInvoice.Line[num13] = sourceDocumentsSalesInvoicesInvoiceLine3;
										num13++;
									}
								}
								int num14 = 0;
								sourceDocumentsSalesInvoicesInvoice.DocumentTotals.Payment = new PaymentMethod[num8];
								foreach (object obj4 in arrayList3)
								{
									PaymentMethod paymentMethod3 = (PaymentMethod)obj4;
									bool flag28 = num14 != num8;
									if (flag28)
									{
										sourceDocumentsSalesInvoicesInvoice2.DocumentTotals.Payment[num14] = paymentMethod3;
										num14++;
									}
								}
								arrayList.Add(sourceDocumentsSalesInvoicesInvoice);
								num6++;
								this._saftXMLAuditFile.SourceDocuments.SalesInvoices.Invoice = new SourceDocumentsSalesInvoicesInvoice[num6];
								this._saftXMLAuditFile.SourceDocuments.SalesInvoices.NumberOfEntries = num6.ToString();
								this._saftXMLAuditFile.SourceDocuments.SalesInvoices.TotalDebit = num;
								this._saftXMLAuditFile.SourceDocuments.SalesInvoices.TotalCredit = num2;
								int num15 = 0;
								foreach (object obj5 in arrayList)
								{
									SourceDocumentsSalesInvoicesInvoice sourceDocumentsSalesInvoicesInvoice3 = (SourceDocumentsSalesInvoicesInvoice)obj5;
									bool flag29 = num15 != num6;
									if (flag29)
									{
										this._saftXMLAuditFile.SourceDocuments.SalesInvoices.Invoice[num15] = sourceDocumentsSalesInvoicesInvoice3;
										num15++;
									}
								}
							}
						}
					}
					sqlConnection.Close();
				}
			}
			catch (Exception e)
			{
				Logger.Error(SystemSettings.PulseContext.Language.GetText(SAFTXMLDetails.UnhandledExceptionInSaftDetailsMessage), e);
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000071B0 File Offset: 0x000053B0
		private void GetProducts()
		{
			string value = string.Join("|", this._productCodes);
			try
			{
				using (SqlConnection sqlConnection = PulseConnection.CreateSQLConnection())
				{
					sqlConnection.Open();
					using (SqlCommand sqlCommand = new SqlCommand("spGetProductsForSAFTXML", sqlConnection))
					{
						sqlCommand.CommandType = CommandType.StoredProcedure;
						sqlCommand.Parameters.AddWithValue("@LocationCode", SystemSettings.PulseContext.LocationCode);
						sqlCommand.Parameters.AddWithValue("@ProductCodes", value);
						using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
						{
							ArrayList arrayList = new ArrayList();
							int num = 0;
							bool hasRows = sqlDataReader.HasRows;
							if (hasRows)
							{
								while (sqlDataReader.Read())
								{
									arrayList.Add(new Product
									{
										ProductType = this.GetValue("ProductType"),
										ProductCode = (string.IsNullOrEmpty(sqlDataReader["ProductCode"].ToString()) ? string.Empty : sqlDataReader["ProductCode"].ToString()),
										ProductGroup = (string.IsNullOrEmpty(sqlDataReader["ProductCategoryCode"].ToString()) ? string.Empty : sqlDataReader["ProductCategoryCode"].ToString()),
										ProductDescription = (string.IsNullOrEmpty(sqlDataReader["ProdTextProductDesc"].ToString()) ? string.Empty : sqlDataReader["ProdTextProductDesc"].ToString()),
										ProductNumberCode = (string.IsNullOrEmpty(sqlDataReader["ProductCode"].ToString()) ? string.Empty : sqlDataReader["ProductCode"].ToString())
									});
									num++;
								}
								this._saftXMLAuditFile.MasterFiles.Product = new Product[num];
								int num2 = 0;
								foreach (object obj in arrayList)
								{
									Product product = (Product)obj;
									bool flag = num2 != num;
									if (flag)
									{
										this._saftXMLAuditFile.MasterFiles.Product[num2] = product;
										num2++;
									}
								}
							}
						}
					}
					sqlConnection.Close();
				}
			}
			catch (Exception e)
			{
				Logger.Error(SystemSettings.PulseContext.Language.GetText(SAFTXMLDetails.UnhandledExceptionInSaftDetailsMessage), e);
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000074B4 File Offset: 0x000056B4
		private void GetTaxTable()
		{
			try
			{
				this._taxRates = new Dictionary<decimal, string>();
				using (SqlConnection sqlConnection = PulseConnection.CreateSQLConnection())
				{
					sqlConnection.Open();
					using (SqlCommand sqlCommand = new SqlCommand("spGetTaxTable", sqlConnection))
					{
						this._taxRates = new Dictionary<decimal, string>();
						sqlCommand.CommandType = CommandType.StoredProcedure;
						sqlCommand.Parameters.AddWithValue("@LocationCode", SystemSettings.PulseContext.LocationCode);
						using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
						{
							bool hasRows = sqlDataReader.HasRows;
							if (hasRows)
							{
								while (sqlDataReader.Read())
								{
									this._taxRates.Add(Convert.ToDecimal(sqlDataReader["TaxRate"]), sqlDataReader["TaxCode"].ToString());
								}
							}
						}
					}
					sqlConnection.Close();
				}
			}
			catch (Exception e)
			{
				Logger.Error(SystemSettings.PulseContext.Language.GetText(SAFTXMLDetails.UnhandledExceptionInSaftDetailsMessage), e);
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000075F4 File Offset: 0x000057F4
		private string GetValue(string key)
		{
			string empty = string.Empty;
			this._saftDetails.TryGetValue(key, out empty);
			return empty;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000761C File Offset: 0x0000581C
		private void MapTaxTable()
		{
			int num = 0;
			this._saftXMLAuditFile.MasterFiles.TaxTable = new TaxTableEntry[this._taxPercentagesCount];
			foreach (decimal num2 in this._taxPercentages)
			{
				bool flag = num != this._taxPercentagesCount;
				if (flag)
				{
					this._saftXMLAuditFile.MasterFiles.TaxTable[num] = new TaxTableEntry();
					this._saftXMLAuditFile.MasterFiles.TaxTable[num].TaxCountryRegion = this.GetValue("TaxCountryRegion");
					this._saftXMLAuditFile.MasterFiles.TaxTable[num].TaxType = this.GetValue("TaxType");
					this._saftXMLAuditFile.MasterFiles.TaxTable[num].ItemElementName = ItemChoiceType.TaxPercentage;
					this._saftXMLAuditFile.MasterFiles.TaxTable[num].Item = num2;
					this._saftXMLAuditFile.MasterFiles.TaxTable[num].Description = Convert.ToString(num2.ToString()) + "%";
					bool flag2 = this._taxRates.ContainsKey(num2);
					if (flag2)
					{
						this._saftXMLAuditFile.MasterFiles.TaxTable[num].TaxCode = this._taxRates[num2];
					}
					else
					{
						this._saftXMLAuditFile.MasterFiles.TaxTable[num].TaxCode = SAFTXMLDetails.DefaultTaxCode;
					}
					num++;
				}
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000077C8 File Offset: 0x000059C8
		private void SaveAuditLogData(string audLogActionName, string audLogReference, string audLogOriginalValue, string audLogNewValue, string audLogUserCode, string locationCode)
		{
			IConnection connection = PulseConnection.CreateOpen();
			IConnection connection2 = connection;
			string name = "spSaveAuditLog";
			object[] array = new object[20];
			array[0] = "Location_Code";
			array[1] = locationCode;
			array[2] = "AudLogTimestamp";
			array[4] = "AudLogId";
			array[6] = "AudLogActionName";
			array[7] = audLogActionName;
			array[8] = "AudLogReference";
			array[9] = audLogReference;
			array[10] = "AudLogOriginalValue";
			array[11] = audLogOriginalValue;
			array[12] = "AudLogNewValue";
			array[13] = audLogNewValue;
			array[14] = "AudLogComputerName";
			array[15] = Env.ComputerName;
			array[16] = "AudLogUserCode";
			array[17] = audLogUserCode;
			array[18] = "System_Date";
			connection2.SpExecNamed(name, Fields.Create(array));
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00007874 File Offset: 0x00005A74
		private string GetATCUD(string VerificationCode, string invoiceNumber)
		{
			string str = invoiceNumber.Substring(invoiceNumber.IndexOf('/') + 1);
			return VerificationCode + "-" + str;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000078A4 File Offset: 0x00005AA4
		public AuditFile GetSaftXmlDetails(DateTime startDate, DateTime endDate)
		{
			this._saftXMLHeader = new SAFTXMLHeader();
			bool header = this.GetHeader(startDate, endDate);
			bool flag = header;
			AuditFile result;
			if (flag)
			{
				this.GetTaxTable();
				this.GetInvoices(startDate, endDate.AddDays(1.0));
				this.GetCustomers();
				this.GetProducts();
				this.MapTaxTable();
				this.SaveAuditLogData(SystemSettings.PulseContext.Language.GetText(SAFTXMLDetails.SaftGeneratedMessage), string.Format(SystemSettings.PulseContext.Language.GetText(SAFTXMLDetails.SaftGeneratedWithDatesMessage), Convert.ToString(startDate), Convert.ToString(endDate)), Convert.ToString(startDate), Convert.ToString(endDate), SystemSettings.PulseContext.User.EmployeeCode.ToString(), SystemSettings.PulseContext.LocationCode);
				result = this._saftXMLAuditFile;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x040000D7 RID: 215
		private static readonly string BlankSpace = " ";

		// Token: 0x040000D8 RID: 216
		private static readonly string DateFormat = "yyyy/MM/dd";

		// Token: 0x040000D9 RID: 217
		private static readonly string DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss";

		// Token: 0x040000DA RID: 218
		private static readonly string DefaultTaxCode = "OUT";

		// Token: 0x040000DB RID: 219
		private static readonly string NonExemptTaxCode = "INT";

		// Token: 0x040000DC RID: 220
		private static readonly string GenericCustomerCode = "0";

		// Token: 0x040000DD RID: 221
		private static readonly string Hiphen = "-";

		// Token: 0x040000DE RID: 222
		private static readonly int OrderNumberStringKey = 51881;

		// Token: 0x040000DF RID: 223
		private static readonly string PercentageFormat = "{0:0.00}";

		// Token: 0x040000E0 RID: 224
		private static readonly int SaftGeneratedMessage = 51861;

		// Token: 0x040000E1 RID: 225
		private static readonly int SaftGeneratedWithDatesMessage = 51862;

		// Token: 0x040000E2 RID: 226
		private static readonly int UnhandledExceptionInSaftDetailsMessage = 51879;

		// Token: 0x040000E3 RID: 227
		private Collection<string> _invoiceNumbers;

		// Token: 0x040000E4 RID: 228
		private Collection<string> _productCodes;

		// Token: 0x040000E5 RID: 229
		private Dictionary<string, string> _saftDetails;

		// Token: 0x040000E6 RID: 230
		private AuditFile _saftXMLAuditFile;

		// Token: 0x040000E7 RID: 231
		private SAFTXMLHeader _saftXMLHeader;

		// Token: 0x040000E8 RID: 232
		private Collection<decimal> _taxPercentages;

		// Token: 0x040000E9 RID: 233
		private Dictionary<decimal, string> _taxRates;

		// Token: 0x040000EA RID: 234
		private int _taxPercentagesCount;
	}
}
