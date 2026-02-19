using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Dominos.LogForPulse;
using Dominos.Pulse;

namespace Dp.SAFTXMLAddition
{
	// Token: 0x02000021 RID: 33
	public class SAFTXMLHeaderObject
	{
		// Token: 0x06000161 RID: 353 RVA: 0x0000801C File Offset: 0x0000621C
		public string GetSAFTXMLHeaderDetails()
		{
			SAFTHeader saftheader = new SAFTHeader();
			string text = string.Empty;
			try
			{
				SAFTXMLHeader saftxmlheader = new SAFTXMLHeader();
				saftheader = saftxmlheader.GetSAFTXMLHeaderDetails();
				AuditFile auditFile = new AuditFile();
				auditFile.Header = new Header();
				auditFile.Header.CompanyID = saftheader.CompanyID;
				auditFile.Header.TaxRegistrationNumber = saftheader.TaxRegistrationNumber;
				auditFile.Header.TaxAccountingBasis = saftheader.TaxAccountingBasis;
				auditFile.Header.CompanyName = saftheader.CompanyName;
				auditFile.Header.CompanyAddress = new AddressStructurePT();
				auditFile.Header.CompanyAddress.AddressDetail = saftheader.CompanyAddress.AddressDetail;
				auditFile.Header.CompanyAddress.City = saftheader.CompanyAddress.City;
				auditFile.Header.CompanyAddress.Country = saftheader.CompanyAddress.Country;
				auditFile.Header.CompanyAddress.PostalCode = saftheader.CompanyAddress.PostalCode;
				auditFile.Header.CurrencyCode = saftheader.CurrencyCode;
				auditFile.Header.TaxEntity = saftheader.TaxEntity;
				auditFile.Header.ProductCompanyTaxID = saftheader.ProductCompanyTaxID;
				auditFile.Header.SoftwareCertificateNumber = saftheader.SoftwareCertificateNumber;
				auditFile.Header.ProductID = saftheader.ProductID;
				auditFile.Header.ProductVersion = saftheader.ProductVersion;
				auditFile.Header.AuditFileVersion = saftheader.AuditFileVersion;
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(AuditFile));
				FileStream fileStream = new FileStream(SAFTXMLHeaderObject.TempHeaderFile, FileMode.Create);
				xmlSerializer.Serialize(fileStream, auditFile);
				fileStream.Close();
				XmlTextReader xmlTextReader = new XmlTextReader(SAFTXMLHeaderObject.TempHeaderFile);
				xmlTextReader.MoveToContent();
				xmlTextReader.ReadToDescendant(SAFTXMLHeaderObject.HeaderString);
				do
				{
					XmlNodeType nodeType = xmlTextReader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType != XmlNodeType.Text)
						{
							if (nodeType == XmlNodeType.EndElement)
							{
								bool flag = !xmlTextReader.Name.Equals(SAFTXMLHeaderObject.AuditFileString) && !xmlTextReader.Name.Contains(SAFTXMLHeaderObject.DateString);
								if (flag)
								{
									text += string.Format("</{0}>", xmlTextReader.Name);
									text += Environment.NewLine;
								}
							}
						}
						else
						{
							bool flag2 = xmlTextReader.Value != SAFTXMLHeaderObject.DateMinValue;
							if (flag2)
							{
								text += string.Format(xmlTextReader.Value, Array.Empty<object>());
							}
						}
					}
					else
					{
						bool flag3 = !xmlTextReader.Name.Contains(SAFTXMLHeaderObject.DateString);
						if (flag3)
						{
							bool flag4 = text.EndsWith(">");
							if (flag4)
							{
								text += Environment.NewLine;
							}
							text += string.Format("<{0}", xmlTextReader.Name);
							while (xmlTextReader.MoveToNextAttribute())
							{
								text += string.Format(" {0}='{1}'", xmlTextReader.Name, xmlTextReader.Value);
							}
							text += string.Format(">", Array.Empty<object>());
						}
					}
				}
				while (xmlTextReader.Read());
				xmlTextReader.Close();
				File.Delete(SAFTXMLHeaderObject.TempHeaderFile);
			}
			catch (Exception ex)
			{
				Logger.Error(SystemSettings.PulseContext.Language.GetText(SAFTXMLHeaderObject.UnhandledExceptionInHeaderDetailsMessage), ex);
				MessageBox.Show(ex.Message + "\n\r" + ex.StackTrace, SystemSettings.PulseContext.Language.GetText(SAFTXMLHeaderObject.StartupFailureMessage), MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			return text;
		}

		// Token: 0x040000EE RID: 238
		private static readonly string AuditFileString = "AuditFile";

		// Token: 0x040000EF RID: 239
		private static readonly string DateMinValue = "0001-01-01";

		// Token: 0x040000F0 RID: 240
		private static readonly string DateString = "Date";

		// Token: 0x040000F1 RID: 241
		private static readonly string HeaderString = "Header";

		// Token: 0x040000F2 RID: 242
		private static readonly int StartupFailureMessage = 51876;

		// Token: 0x040000F3 RID: 243
		private static readonly string TempHeaderFile = "C:\\" + "SaftHeader.xml";

		// Token: 0x040000F4 RID: 244
		private static readonly int UnhandledExceptionInHeaderDetailsMessage = 51880;
	}
}
