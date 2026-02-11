using SAFTExtractor.Enums;

namespace SAFTExtractor.Models
{
    /// <summary>
    /// Representa uma fatura/documento para o ficheiro SAFT
    /// </summary>
    public class Invoice
    {
        public string InvoiceNo { get; set; } = string.Empty;
        public string? ATCUD { get; set; } // Código Único de Documento
        public string? Hash { get; set; }
        public string? HashControl { get; set; }
        public DateTime InvoiceDate { get; set; }
        public SAFTPTSourceBilling InvoiceType { get; set; }
        public string? SourceID { get; set; }
        public DateTime SystemEntryDate { get; set; }
        public string? TransactionID { get; set; }
        public string CustomerID { get; set; } = string.Empty;
        
        // Totais
        public decimal TaxPayable { get; set; }
        public decimal NetTotal { get; set; }
        public decimal GrossTotal { get; set; }
        
        // Linhas
        public List<InvoiceLine> Lines { get; set; } = new List<InvoiceLine>();
        
        // Totais de documentos
        public DocumentTotals? DocumentTotals { get; set; }
        
        // Campos extensíveis - adicione aqui novos campos
    }
    
    /// <summary>
    /// Representa uma linha de fatura
    /// </summary>
    public class InvoiceLine
    {
        public int LineNumber { get; set; }
        public string ProductCode { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string UnitOfMeasure { get; set; } = "UN";
        public decimal UnitPrice { get; set; }
        public string TaxType { get; set; } = "IVA";
        public string TaxCountryRegion { get; set; } = "PT";
        public string TaxCode { get; set; } = "NOR";
        public decimal TaxPercentage { get; set; }
        public string? TaxExemptionReason { get; set; }
        public decimal CreditAmount { get; set; }
        
        // Campos extensíveis
    }
    
    /// <summary>
    /// Totais do documento
    /// </summary>
    public class DocumentTotals
    {
        public decimal TaxPayable { get; set; }
        public decimal NetTotal { get; set; }
        public decimal GrossTotal { get; set; }
    }
}
