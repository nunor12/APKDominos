namespace SAFTExtractor.Models
{
    /// <summary>
    /// Representa um cliente para o ficheiro SAFT
    /// </summary>
    public class Customer
    {
        public string CustomerID { get; set; } = string.Empty;
        public string AccountID { get; set; } = string.Empty;
        public string CustomerTaxID { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string? Contact { get; set; }
        public string? Telephone { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public int SelfBillingIndicator { get; set; }
        
        // Billing Address
        public string? BuildingNumber { get; set; }
        public string? StreetName { get; set; }
        public string AddressDetail { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string? Region { get; set; }
        public string Country { get; set; } = "PT";
        
        // Campos extensíveis - adicione aqui novos campos
        // Exemplo: public string? NIFRepresentante { get; set; }
    }
}
