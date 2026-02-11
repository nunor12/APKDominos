namespace SAFTExtractor.Enums
{
    /// <summary>
    /// Tipo de documento para faturação SAFT-PT
    /// </summary>
    public enum SAFTPTSourceBilling
    {
        /// <summary>
        /// Fatura
        /// </summary>
        Invoice,
        
        /// <summary>
        /// Fatura Simplificada
        /// </summary>
        SimplifiedInvoice,
        
        /// <summary>
        /// Nota de Crédito
        /// </summary>
        CreditNote,
        
        /// <summary>
        /// Nota de Débito
        /// </summary>
        DebitNote,
        
        /// <summary>
        /// Recibo
        /// </summary>
        Receipt,
        
        /// <summary>
        /// Guia de Transporte
        /// </summary>
        ShippingDocument,
        
        /// <summary>
        /// Outros
        /// </summary>
        Other
    }
}
