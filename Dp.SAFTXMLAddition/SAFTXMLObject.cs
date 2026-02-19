using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Dominos.Pulse;

namespace Dp.SAFTXMLAddition
{
	// Token: 0x02000022 RID: 34
	public class SAFTXMLObject
	{
		// Token: 0x06000164 RID: 356 RVA: 0x00008444 File Offset: 0x00006644
		private void WriteSaftXmlFile(AuditFile saftXmlAuditFile, string destinationFolder, string fileName)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(AuditFile));
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.Indent = true;
			xmlWriterSettings.NewLineOnAttributes = true;
			xmlWriterSettings.Encoding = Encoding.GetEncoding(1252);
			string path = destinationFolder + fileName;
			using (FileStream fileStream = new FileStream(path, FileMode.Create))
			{
				xmlSerializer.Serialize(fileStream, saftXmlAuditFile);
				fileStream.Close();
			}
			string text = File.ReadAllText(path);
			int num = text.IndexOf("PT_") + 3;
			int num2 = text.IndexOf(">", num);
			bool flag = num > 0 && num2 > 0;
			if (flag)
			{
				text = text.Replace(text.Substring(num, num2 - num - 1), saftXmlAuditFile.Header.AuditFileVersion);
				File.WriteAllText(path, text);
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00008530 File Offset: 0x00006730
		public void CreateSaftXmlFile(DateTime startDate, DateTime endDate, string destinationFolder)
		{
			SAFTXMLDetails saftxmldetails = new SAFTXMLDetails();
			AuditFile saftXmlDetails = saftxmldetails.GetSaftXmlDetails(startDate, endDate);
			string fileName = string.Concat(new string[]
			{
				SystemSettings.PulseContext.LocationCode,
				SAFTXMLObject.Separator,
				string.Format(SAFTXMLObject.DayFormat, startDate),
				string.Format(SAFTXMLObject.MonthFormat, startDate),
				string.Format(SAFTXMLObject.YearFormat, startDate),
				SAFTXMLObject.Separator,
				string.Format(SAFTXMLObject.DayFormat, endDate),
				string.Format(SAFTXMLObject.MonthFormat, endDate),
				string.Format(SAFTXMLObject.YearFormat, endDate),
				SAFTXMLObject.Separator,
				SAFTXMLObject.SaftFile,
				SAFTXMLObject.FileExtension
			});
			bool flag = saftXmlDetails == null;
			if (flag)
			{
				MessageBox.Show(SystemSettings.PulseContext.Language.GetText(SAFTXMLObject.InvalidHeaderDataMessage), SystemSettings.PulseContext.Language.GetText(SAFTXMLObject.DialogWindowInformationTitle));
			}
			else
			{
				this.WriteSaftXmlFile(saftXmlDetails, destinationFolder, fileName);
				MessageBox.Show(string.Format(SystemSettings.PulseContext.Language.GetText(SAFTXMLObject.SaftGeneratedWithDatesMessage), startDate.Date.ToString(SAFTXMLObject.DateFormat), endDate.Date.ToString(SAFTXMLObject.DateFormat)), SystemSettings.PulseContext.Language.GetText(SAFTXMLObject.DialogWindowInformationTitle));
			}
		}

		// Token: 0x040000F5 RID: 245
		private static readonly string FileExtension = ".xml";

		// Token: 0x040000F6 RID: 246
		private static readonly string DateFormat = "yyyy/MM/dd";

		// Token: 0x040000F7 RID: 247
		private static readonly string DayFormat = "{0:dd}";

		// Token: 0x040000F8 RID: 248
		private static readonly int DialogWindowInformationTitle = 310;

		// Token: 0x040000F9 RID: 249
		private static readonly int InvalidHeaderDataMessage = 51868;

		// Token: 0x040000FA RID: 250
		private static readonly string MonthFormat = "{0:MM}";

		// Token: 0x040000FB RID: 251
		private static readonly string SaftFile = "SAFT";

		// Token: 0x040000FC RID: 252
		private static readonly int SaftGeneratedWithDatesMessage = 51862;

		// Token: 0x040000FD RID: 253
		private static readonly string Separator = "_";

		// Token: 0x040000FE RID: 254
		private static readonly string YearFormat = "{0:yyyy}";
	}
}
