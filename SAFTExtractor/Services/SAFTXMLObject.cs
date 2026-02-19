using System.Xml;
using System.Xml.Serialization;
using SAFTExtractor.Models;
using SAFTExtractor.Utils;

namespace SAFTExtractor.Services
{
    /// <summary>
    /// Classe responsável pela construção e exportação do ficheiro SAFT em XML
    /// Recebe os dados da SAFTXMLDetails e faz a serialização para XML conforme schema SAFT-PT
    /// </summary>
    public class SAFTXMLObject
    {
        private readonly SAFTXMLDetails _details;
        
        public SAFTXMLObject()
        {
            _details = new SAFTXMLDetails();
        }
        
        /// <summary>
        /// Gera o ficheiro SAFT XML completo (por ano fiscal)
        /// </summary>
        public void GenerateSAFTFile(string outputPath, int fiscalYear)
        {
            DateTime startDate = SAFTDate.GetFiscalYearStartDate(fiscalYear);
            DateTime endDate = SAFTDate.GetFiscalYearEndDate(fiscalYear);
            GenerateSAFTFile(outputPath, startDate, endDate);
        }
        
        /// <summary>
        /// Gera o ficheiro SAFT XML completo (por datas específicas)
        /// </summary>
        public void GenerateSAFTFile(string outputPath, DateTime startDate, DateTime endDate)
        {
            try
            {
                int fiscalYear = SAFTDate.GetFiscalYear(startDate);
                var header = _details.GetHeader(fiscalYear);
                var customers = _details.GetCustomers();
                var products = _details.GetProducts();
                var invoices = _details.GetInvoices(startDate, endDate);
                
                // Criar o XML manualmente para ter controle total sobre a estrutura
                var settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "  ",
                    NewLineChars = "\n",
                    NewLineHandling = NewLineHandling.Replace,
                    Encoding = System.Text.Encoding.UTF8
                };
                
                using (var writer = XmlWriter.Create(outputPath, settings))
                {
                    writer.WriteStartDocument();
                    
                    // Namespace SAFT-PT
                    writer.WriteStartElement("AuditFile", "urn:OECD:StandardAuditFile-Tax:PT_1.04_01");
                    writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
                    
                    // Header (com datas personalizadas)
                    WriteHeader(writer, header, startDate, endDate);
                    
                    // MasterFiles
                    WriteMasterFiles(writer, customers, products);
                    
                    // SourceDocuments
                    WriteSourceDocuments(writer, invoices);
                    
                    writer.WriteEndElement(); // AuditFile
                    writer.WriteEndDocument();
                }
                
                Console.WriteLine($"Ficheiro SAFT gerado com sucesso: {outputPath}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao gerar ficheiro SAFT: {ex.Message}", ex);
            }
        }
        
        /// <summary>
        /// Escreve o Header do SAFT
        /// </summary>
        private void WriteHeader(XmlWriter writer, SAFTHeader header, DateTime startDate, DateTime endDate)
        {
            writer.WriteStartElement("Header");
            
            writer.WriteElementString("AuditFileVersion", header.AuditFileVersion);
            writer.WriteElementString("CompanyID", header.CompanyID);
            writer.WriteElementString("TaxRegistrationNumber", header.TaxRegistrationNumber);
            writer.WriteElementString("TaxAccountingBasis", header.TaxAccountingBasis);
            writer.WriteElementString("CompanyName", header.CompanyName);
            
            // Business Address
            if (header.BusinessAddress != null)
            {
                writer.WriteStartElement("BusinessAddress");
                if (!string.IsNullOrEmpty(header.BusinessAddress.BuildingNumber))
                    writer.WriteElementString("BuildingNumber", header.BusinessAddress.BuildingNumber);
                if (!string.IsNullOrEmpty(header.BusinessAddress.StreetName))
                    writer.WriteElementString("StreetName", header.BusinessAddress.StreetName);
                writer.WriteElementString("AddressDetail", header.BusinessAddress.AddressDetail);
                writer.WriteElementString("City", header.BusinessAddress.City);
                writer.WriteElementString("PostalCode", header.BusinessAddress.PostalCode);
                if (!string.IsNullOrEmpty(header.BusinessAddress.Region))
                    writer.WriteElementString("Region", header.BusinessAddress.Region);
                writer.WriteElementString("Country", header.BusinessAddress.Country);
                writer.WriteEndElement(); // BusinessAddress
            }
            
            // Usar datas fornecidas em vez das do header
            writer.WriteElementString("FiscalYear", SAFTDate.GetFiscalYear(startDate).ToString());
            writer.WriteElementString("StartDate", SAFTDate.FormatSAFTDate(startDate));
            writer.WriteElementString("EndDate", SAFTDate.FormatSAFTDate(endDate));
            writer.WriteElementString("CurrencyCode", header.CurrencyCode);
            writer.WriteElementString("DateCreated", SAFTDate.FormatSAFTDate(DateTime.Today));
            writer.WriteElementString("TaxEntity", header.TaxEntity);
            writer.WriteElementString("ProductCompanyTaxID", header.ProductCompanyTaxID);
            writer.WriteElementString("SoftwareCertificateNumber", header.SoftwareCertificateNumber);
            writer.WriteElementString("ProductID", header.ProductID);
            writer.WriteElementString("ProductVersion", header.ProductVersion);
            
            writer.WriteEndElement(); // Header
        }
        
        /// <summary>
        /// Escreve a secção MasterFiles (Clientes e Produtos)
        /// </summary>
        private void WriteMasterFiles(XmlWriter writer, List<Customer> customers, List<Product> products)
        {
            writer.WriteStartElement("MasterFiles");
            
            // Customers
            foreach (var customer in customers)
            {
                writer.WriteStartElement("Customer");
                
                writer.WriteElementString("CustomerID", customer.CustomerID);
                writer.WriteElementString("AccountID", customer.AccountID);
                writer.WriteElementString("CustomerTaxID", customer.CustomerTaxID);
                writer.WriteElementString("CompanyName", customer.CompanyName);
                
                if (!string.IsNullOrEmpty(customer.Contact))
                    writer.WriteElementString("Contact", customer.Contact);
                if (!string.IsNullOrEmpty(customer.Telephone))
                    writer.WriteElementString("Telephone", customer.Telephone);
                if (!string.IsNullOrEmpty(customer.Fax))
                    writer.WriteElementString("Fax", customer.Fax);
                if (!string.IsNullOrEmpty(customer.Email))
                    writer.WriteElementString("Email", customer.Email);
                if (!string.IsNullOrEmpty(customer.Website))
                    writer.WriteElementString("Website", customer.Website);
                
                // Billing Address
                writer.WriteStartElement("BillingAddress");
                if (!string.IsNullOrEmpty(customer.BuildingNumber))
                    writer.WriteElementString("BuildingNumber", customer.BuildingNumber);
                if (!string.IsNullOrEmpty(customer.StreetName))
                    writer.WriteElementString("StreetName", customer.StreetName);
                writer.WriteElementString("AddressDetail", customer.AddressDetail);
                writer.WriteElementString("City", customer.City);
                writer.WriteElementString("PostalCode", customer.PostalCode);
                if (!string.IsNullOrEmpty(customer.Region))
                    writer.WriteElementString("Region", customer.Region);
                writer.WriteElementString("Country", customer.Country);
                writer.WriteEndElement(); // BillingAddress
                
                writer.WriteElementString("SelfBillingIndicator", customer.SelfBillingIndicator.ToString());
                
                writer.WriteEndElement(); // Customer
            }
            
            // Products
            foreach (var product in products)
            {
                writer.WriteStartElement("Product");
                
                writer.WriteElementString("ProductType", product.ProductType);
                writer.WriteElementString("ProductCode", product.ProductCode);
                if (!string.IsNullOrEmpty(product.ProductGroup))
                    writer.WriteElementString("ProductGroup", product.ProductGroup);
                writer.WriteElementString("ProductDescription", product.ProductDescription);
                writer.WriteElementString("ProductNumberCode", product.ProductNumberCode);
                
                writer.WriteEndElement(); // Product
            }
            
            writer.WriteEndElement(); // MasterFiles
        }
        
        /// <summary>
        /// Escreve a secção SourceDocuments (Faturas)
        /// </summary>
        private void WriteSourceDocuments(XmlWriter writer, List<Invoice> invoices)
        {
            writer.WriteStartElement("SourceDocuments");
            writer.WriteStartElement("SalesInvoices");
            
            writer.WriteElementString("NumberOfEntries", invoices.Count.ToString());
            writer.WriteElementString("TotalDebit", invoices.Sum(i => i.GrossTotal).ToString("F2"));
            writer.WriteElementString("TotalCredit", "0.00");
            
            foreach (var invoice in invoices)
            {
                writer.WriteStartElement("Invoice");
                
                writer.WriteElementString("InvoiceNo", invoice.InvoiceNo);
                if (!string.IsNullOrEmpty(invoice.ATCUD))
                    writer.WriteElementString("ATCUD", invoice.ATCUD);
                if (!string.IsNullOrEmpty(invoice.Hash))
                    writer.WriteElementString("Hash", invoice.Hash);
                if (!string.IsNullOrEmpty(invoice.HashControl))
                    writer.WriteElementString("HashControl", invoice.HashControl);
                
                writer.WriteElementString("InvoiceDate", SAFTDate.FormatSAFTDate(invoice.InvoiceDate));
                writer.WriteElementString("InvoiceType", invoice.InvoiceType.ToString());
                writer.WriteElementString("SystemEntryDate", SAFTDate.FormatSAFTDateTime(invoice.SystemEntryDate));
                writer.WriteElementString("CustomerID", invoice.CustomerID);
                
                // Lines
                foreach (var line in invoice.Lines)
                {
                    writer.WriteStartElement("Line");
                    
                    writer.WriteElementString("LineNumber", line.LineNumber.ToString());
                    writer.WriteElementString("ProductCode", line.ProductCode);
                    writer.WriteElementString("ProductDescription", line.ProductDescription);
                    writer.WriteElementString("Quantity", line.Quantity.ToString("F2"));
                    writer.WriteElementString("UnitOfMeasure", line.UnitOfMeasure);
                    writer.WriteElementString("UnitPrice", line.UnitPrice.ToString("F2"));
                    writer.WriteElementString("TaxPointDate", SAFTDate.FormatSAFTDate(invoice.InvoiceDate));
                    
                    // Tax
                    writer.WriteStartElement("Tax");
                    writer.WriteElementString("TaxType", line.TaxType);
                    writer.WriteElementString("TaxCountryRegion", line.TaxCountryRegion);
                    writer.WriteElementString("TaxCode", line.TaxCode);
                    writer.WriteElementString("TaxPercentage", line.TaxPercentage.ToString("F2"));
                    writer.WriteEndElement(); // Tax
                    
                    // Campos de isenção (apenas quando aplicável)
                    if (!string.IsNullOrEmpty(line.TaxExemptionReason))
                        writer.WriteElementString("TaxExemptionReason", line.TaxExemptionReason);
                    if (!string.IsNullOrEmpty(line.TaxExemptionCode))
                        writer.WriteElementString("TaxExemptionCode", line.TaxExemptionCode);
                    
                    writer.WriteElementString("CreditAmount", line.CreditAmount.ToString("F2"));
                    
                    writer.WriteEndElement(); // Line
                }
                
                // DocumentTotals
                writer.WriteStartElement("DocumentTotals");
                writer.WriteElementString("TaxPayable", invoice.TaxPayable.ToString("F2"));
                writer.WriteElementString("NetTotal", invoice.NetTotal.ToString("F2"));
                writer.WriteElementString("GrossTotal", invoice.GrossTotal.ToString("F2"));
                writer.WriteEndElement(); // DocumentTotals
                
                writer.WriteEndElement(); // Invoice
            }
            
            writer.WriteEndElement(); // SalesInvoices
            writer.WriteEndElement(); // SourceDocuments
        }
    }
}
