namespace SAFTExtractor.Models
{
    /// <summary>
    /// Representa um produto para o ficheiro SAFT
    /// </summary>
    public class Product
    {
        public string ProductType { get; set; } = "P"; // P=Product, S=Service, O=Other, E=Other (S&T)
        public string ProductCode { get; set; } = string.Empty;
        public string? ProductGroup { get; set; }
        public string ProductDescription { get; set; } = string.Empty;
        public string ProductNumberCode { get; set; } = string.Empty;
        
        // Campos extensíveis - adicione aqui novos campos
        // Exemplo: public string? CustomField { get; set; }
    }
}
