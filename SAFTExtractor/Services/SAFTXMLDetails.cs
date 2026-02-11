using System.Data;
using System.Data.SqlClient;
using SAFTExtractor.Models;
using SAFTExtractor.Utils;

namespace SAFTExtractor.Services
{
    /// <summary>
    /// Classe crítica que extrai dados das Stored Procedures e monta a estrutura SAFT
    /// Esta é a classe principal para extração de dados do PULSE DOMINOS
    /// </summary>
    public class SAFTXMLDetails
    {
        private readonly DatabaseConfig _dbConfig;
        
        public SAFTXMLDetails(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig ?? throw new ArgumentNullException(nameof(dbConfig));
        }
        
        /// <summary>
        /// Obtém clientes para o SAFT XML
        /// </summary>
        public List<Customer> GetCustomers()
        {
            var customers = new List<Customer>();
            
            using (var connection = new SqlConnection(_dbConfig.GetConnectionString()))
            {
                connection.Open();
                
                // TODO: Ajustar nome da SP conforme o seu sistema
                using (var command = new SqlCommand("spGetCustomersForSAFTXML", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var customer = new Customer
                            {
                                CustomerID = reader["CustomerID"].ToString() ?? string.Empty,
                                AccountID = reader["AccountID"].ToString() ?? string.Empty,
                                CustomerTaxID = reader["CustomerTaxID"].ToString() ?? string.Empty,
                                CompanyName = reader["CompanyName"].ToString() ?? string.Empty,
                                Contact = reader["Contact"] != DBNull.Value ? reader["Contact"].ToString() : null,
                                Telephone = reader["Telephone"] != DBNull.Value ? reader["Telephone"].ToString() : null,
                                Fax = reader["Fax"] != DBNull.Value ? reader["Fax"].ToString() : null,
                                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null,
                                Website = reader["Website"] != DBNull.Value ? reader["Website"].ToString() : null,
                                SelfBillingIndicator = reader["SelfBillingIndicator"] != DBNull.Value ? 
                                    Convert.ToInt32(reader["SelfBillingIndicator"]) : 0,
                                BuildingNumber = reader["BuildingNumber"] != DBNull.Value ? reader["BuildingNumber"].ToString() : null,
                                StreetName = reader["StreetName"] != DBNull.Value ? reader["StreetName"].ToString() : null,
                                AddressDetail = reader["AddressDetail"].ToString() ?? string.Empty,
                                City = reader["City"].ToString() ?? string.Empty,
                                PostalCode = reader["PostalCode"].ToString() ?? string.Empty,
                                Region = reader["Region"] != DBNull.Value ? reader["Region"].ToString() : null,
                                Country = reader["Country"].ToString() ?? "PT"
                                
                                // ADICIONE AQUI NOVOS CAMPOS:
                                // Exemplo: NIFRepresentante = reader["NIFRepresentante"] != DBNull.Value ? reader["NIFRepresentante"].ToString() : null
                            };
                            
                            customers.Add(customer);
                        }
                    }
                }
            }
            
            return customers;
        }
        
        /// <summary>
        /// Obtém produtos para o SAFT XML
        /// </summary>
        public List<Product> GetProducts()
        {
            var products = new List<Product>();
            
            using (var connection = new SqlConnection(_dbConfig.GetConnectionString()))
            {
                connection.Open();
                
                // TODO: Ajustar nome da SP conforme o seu sistema
                using (var command = new SqlCommand("spGetProductsForSAFTXML", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var product = new Product
                            {
                                ProductType = reader["ProductType"].ToString() ?? "P",
                                ProductCode = reader["ProductCode"].ToString() ?? string.Empty,
                                ProductGroup = reader["ProductGroup"] != DBNull.Value ? reader["ProductGroup"].ToString() : null,
                                ProductDescription = reader["ProductDescription"].ToString() ?? string.Empty,
                                ProductNumberCode = reader["ProductNumberCode"].ToString() ?? string.Empty
                                
                                // ADICIONE AQUI NOVOS CAMPOS:
                                // Exemplo: CustomField = reader["CustomField"] != DBNull.Value ? reader["CustomField"].ToString() : null
                            };
                            
                            products.Add(product);
                        }
                    }
                }
            }
            
            return products;
        }
        
        /// <summary>
        /// Obtém faturas/documentos para o SAFT XML
        /// </summary>
        public List<Invoice> GetInvoices(DateTime startDate, DateTime endDate)
        {
            var invoices = new List<Invoice>();
            
            using (var connection = new SqlConnection(_dbConfig.GetConnectionString()))
            {
                connection.Open();
                
                // TODO: Ajustar nome da SP conforme o seu sistema
                using (var command = new SqlCommand("spGetInvoicesForSAFTXML", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var invoice = new Invoice
                            {
                                InvoiceNo = reader["InvoiceNo"].ToString() ?? string.Empty,
                                ATCUD = reader["ATCUD"] != DBNull.Value ? reader["ATCUD"].ToString() : null,
                                Hash = reader["Hash"] != DBNull.Value ? reader["Hash"].ToString() : null,
                                HashControl = reader["HashControl"] != DBNull.Value ? reader["HashControl"].ToString() : null,
                                InvoiceDate = Convert.ToDateTime(reader["InvoiceDate"]),
                                SystemEntryDate = Convert.ToDateTime(reader["SystemEntryDate"]),
                                CustomerID = reader["CustomerID"].ToString() ?? string.Empty,
                                TaxPayable = Convert.ToDecimal(reader["TaxPayable"]),
                                NetTotal = Convert.ToDecimal(reader["NetTotal"]),
                                GrossTotal = Convert.ToDecimal(reader["GrossTotal"])
                                
                                // ADICIONE AQUI NOVOS CAMPOS:
                            };
                            
                            invoices.Add(invoice);
                        }
                    }
                }
                
                // Carregar linhas das faturas
                foreach (var invoice in invoices)
                {
                    invoice.Lines = GetInvoiceLines(invoice.InvoiceNo);
                }
            }
            
            return invoices;
        }
        
        /// <summary>
        /// Obtém linhas de uma fatura
        /// </summary>
        private List<InvoiceLine> GetInvoiceLines(string invoiceNo)
        {
            var lines = new List<InvoiceLine>();
            
            using (var connection = new SqlConnection(_dbConfig.GetConnectionString()))
            {
                connection.Open();
                
                // TODO: Ajustar nome da SP conforme o seu sistema
                using (var command = new SqlCommand("spGetInvoiceLinesForSAFTXML", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@InvoiceNo", invoiceNo);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var line = new InvoiceLine
                            {
                                LineNumber = Convert.ToInt32(reader["LineNumber"]),
                                ProductCode = reader["ProductCode"].ToString() ?? string.Empty,
                                ProductDescription = reader["ProductDescription"].ToString() ?? string.Empty,
                                Quantity = Convert.ToDecimal(reader["Quantity"]),
                                UnitOfMeasure = reader["UnitOfMeasure"].ToString() ?? "UN",
                                UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                                TaxType = reader["TaxType"].ToString() ?? "IVA",
                                TaxCountryRegion = reader["TaxCountryRegion"].ToString() ?? "PT",
                                TaxCode = reader["TaxCode"].ToString() ?? "NOR",
                                TaxPercentage = Convert.ToDecimal(reader["TaxPercentage"]),
                                TaxExemptionReason = reader["TaxExemptionReason"] != DBNull.Value ? 
                                    reader["TaxExemptionReason"].ToString() : null,
                                TaxExemptionCode = reader["TaxExemptionCode"] != DBNull.Value ? 
                                    reader["TaxExemptionCode"].ToString() : null,
                                CreditAmount = Convert.ToDecimal(reader["CreditAmount"])
                                
                                // ADICIONE AQUI NOVOS CAMPOS:
                            };
                            
                            lines.Add(line);
                        }
                    }
                }
            }
            
            return lines;
        }
        
        /// <summary>
        /// Obtém header/configurações da empresa
        /// </summary>
        public SAFTHeader GetHeader(int fiscalYear)
        {
            using (var connection = new SqlConnection(_dbConfig.GetConnectionString()))
            {
                connection.Open();
                
                // TODO: Ajustar nome da SP conforme o seu sistema
                using (var command = new SqlCommand("spGetSAFTHeader", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FiscalYear", fiscalYear);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new SAFTHeader
                            {
                                AuditFileVersion = reader["AuditFileVersion"].ToString() ?? "1.04_01",
                                CompanyID = reader["CompanyID"].ToString() ?? string.Empty,
                                TaxRegistrationNumber = reader["TaxRegistrationNumber"].ToString() ?? string.Empty,
                                TaxAccountingBasis = reader["TaxAccountingBasis"].ToString() ?? "F",
                                CompanyName = reader["CompanyName"].ToString() ?? string.Empty,
                                BusinessAddress = new CompanyAddress
                                {
                                    BuildingNumber = reader["BuildingNumber"] != DBNull.Value ? reader["BuildingNumber"].ToString() : null,
                                    StreetName = reader["StreetName"] != DBNull.Value ? reader["StreetName"].ToString() : null,
                                    AddressDetail = reader["AddressDetail"].ToString() ?? string.Empty,
                                    City = reader["City"].ToString() ?? string.Empty,
                                    PostalCode = reader["PostalCode"].ToString() ?? string.Empty,
                                    Region = reader["Region"] != DBNull.Value ? reader["Region"].ToString() : null,
                                    Country = reader["Country"].ToString() ?? "PT"
                                },
                                FiscalYear = new DateTime(fiscalYear, 1, 1),
                                StartDate = SAFTDate.GetFiscalYearStartDate(fiscalYear),
                                EndDate = SAFTDate.GetFiscalYearEndDate(fiscalYear),
                                CurrencyCode = reader["CurrencyCode"].ToString() ?? "EUR",
                                DateCreated = DateTime.Now,
                                TaxEntity = reader["TaxEntity"].ToString() ?? "Global",
                                ProductCompanyTaxID = reader["ProductCompanyTaxID"].ToString() ?? string.Empty,
                                SoftwareCertificateNumber = reader["SoftwareCertificateNumber"].ToString() ?? string.Empty,
                                ProductID = reader["ProductID"].ToString() ?? "SAFTExtractor/PULSE DOMINOS",
                                ProductVersion = reader["ProductVersion"].ToString() ?? "1.0"
                            };
                        }
                    }
                }
            }
            
            throw new Exception("Não foi possível obter o header SAFT. Verifique a configuração da base de dados.");
        }
    }
}
