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
        public SAFTXMLDetails()
        {
        }
        
        /// <summary>
        /// Obtém clientes para o SAFT XML
        /// </summary>
        public List<Customer> GetCustomers()
        {
            var customers = new List<Customer>();
            
            using (var connection = PulseConnection.CreateSQLConnection())
            {
                connection.Open();
                
                using (var command = new SqlCommand("spGetCustomersForSAFTXML", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;
                    command.Parameters.AddWithValue("@LocationCode", SystemSettings.LocationCode);
                    // Parâmetro @InvoiceNumbers será adicionado quando necessário
                    
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
            
            using (var connection = PulseConnection.CreateSQLConnection())
            {
                connection.Open();
                
                using (var command = new SqlCommand("spGetProductsForSAFTXML", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;
                    command.Parameters.AddWithValue("@LocationCode", SystemSettings.LocationCode);
                    // Parâmetro @ProductCodes será adicionado quando necessário
                    
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
            
            using (var connection = PulseConnection.CreateSQLConnection())
            {
                connection.Open();
                
                using (var command = new SqlCommand("spGetInvoicesForSAFTXML", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;
                    command.Parameters.AddWithValue("@LocationCode", SystemSettings.LocationCode);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
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
            
            using (var connection = PulseConnection.CreateSQLConnection())
            {
                connection.Open();
                
                // Nota: Esta SP não existe nas SPs originais, mas é usada no código
                using (var command = new SqlCommand("spGetInvoiceLinesForSAFTXML", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;
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
            try
            {
                using (var connection = PulseConnection.CreateSQLConnection())
                {
                    connection.Open();
                    
                    // Usar o nome correto da SP: spGetSAFTXMLHeaderDetails
                    using (var command = new SqlCommand("spGetSAFTXMLHeaderDetails", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;
                        command.Parameters.AddWithValue("@LocationCode", SystemSettings.LocationCode);
                        
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
                            else
                            {
                                // SP executou mas não retornou dados
                                throw new Exception(
                                    $"A Stored Procedure 'spGetSAFTXMLHeaderDetails' não retornou dados.\n\n" +
                                    $"LocationCode usado: '{SystemSettings.LocationCode}'\n\n" +
                                    $"Verifique se:\n" +
                                    $"1. O LocationCode '{SystemSettings.LocationCode}' existe na base de dados\n" +
                                    $"2. Existem dados de empresa configurados para este LocationCode\n" +
                                    $"3. A SP está a retornar os campos corretos"
                                );
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Erro específico de SQL Server
                if (sqlEx.Number == 2812) // SP não encontrada
                {
                    throw new Exception(
                        $"ERRO: Stored Procedure 'spGetSAFTXMLHeaderDetails' não foi encontrada!\n\n" +
                        $"A SP precisa existir na base de dados para a aplicação funcionar.\n\n" +
                        $"Verifique a pasta 'Original SP' no repositório para obter o código SQL.\n\n" +
                        $"Detalhes técnicos: {sqlEx.Message}",
                        sqlEx
                    );
                }
                else if (sqlEx.Number == 4060) // Base de dados não existe
                {
                    throw new Exception(
                        $"ERRO: Base de dados não encontrada!\n\n" +
                        $"Connection String: {PulseConnection.GetConnectionStringSafe()}\n\n" +
                        $"Verifique o App.config e confirme que a base de dados existe.\n\n" +
                        $"Detalhes técnicos: {sqlEx.Message}",
                        sqlEx
                    );
                }
                else if (sqlEx.Number == 18456) // Login failed
                {
                    throw new Exception(
                        $"ERRO: Falha na autenticação!\n\n" +
                        $"Verifique as credenciais no App.config.\n" +
                        $"Se usar Integrated Security, confirme que tem permissões.\n\n" +
                        $"Detalhes técnicos: {sqlEx.Message}",
                        sqlEx
                    );
                }
                else
                {
                    throw new Exception(
                        $"ERRO SQL ao obter header SAFT:\n\n" +
                        $"LocationCode: '{SystemSettings.LocationCode}'\n" +
                        $"Código erro SQL: {sqlEx.Number}\n" +
                        $"Mensagem: {sqlEx.Message}\n\n" +
                        $"Verifique a configuração da base de dados no App.config.",
                        sqlEx
                    );
                }
            }
            catch (Exception ex) when (ex.Message.Contains("spGetSAFTXMLHeaderDetails"))
            {
                // Re-throw se já for uma exceção que tratamos
                throw;
            }
            catch (Exception ex)
            {
                // Erro genérico
                throw new Exception(
                    $"ERRO ao conectar à base de dados PULSE DOMINOS:\n\n" +
                    $"Connection String: {PulseConnection.GetConnectionStringSafe()}\n" +
                    $"LocationCode: '{SystemSettings.LocationCode}'\n\n" +
                    $"Verifique:\n" +
                    $"1. O SQL Server está em execução\n" +
                    $"2. O App.config tem a connection string correta\n" +
                    $"3. Tem permissões para aceder à base de dados\n\n" +
                    $"Detalhes técnicos: {ex.Message}",
                    ex
                );
            }
        }
    }
}
