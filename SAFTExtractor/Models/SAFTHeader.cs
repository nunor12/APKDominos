namespace SAFTExtractor.Models
{
    /// <summary>
    /// Representa o header/cabeçalho do ficheiro SAFT
    /// </summary>
    public class SAFTHeader
    {
        public string AuditFileVersion { get; set; } = "1.04_01";
        public string CompanyID { get; set; } = string.Empty;
        public string TaxRegistrationNumber { get; set; } = string.Empty;
        public string TaxAccountingBasis { get; set; } = "F"; // F=Faturação, C=Contabilidade
        public string CompanyName { get; set; } = string.Empty;
        public CompanyAddress? BusinessAddress { get; set; }
        public DateTime FiscalYear { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CurrencyCode { get; set; } = "EUR";
        public DateTime DateCreated { get; set; }
        public string TaxEntity { get; set; } = "Global";
        public string ProductCompanyTaxID { get; set; } = string.Empty;
        public string SoftwareCertificateNumber { get; set; } = string.Empty;
        public string ProductID { get; set; } = "SAFTExtractor/PULSE DOMINOS";
        public string ProductVersion { get; set; } = "1.0";
        
        // Campos extensíveis
    }
    
    /// <summary>
    /// Representa o endereço da empresa
    /// </summary>
    public class CompanyAddress
    {
        public string? BuildingNumber { get; set; }
        public string? StreetName { get; set; }
        public string AddressDetail { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string? Region { get; set; }
        public string Country { get; set; } = "PT";
    }
}
