namespace SAFTExtractor.Utils
{
    /// <summary>
    /// Utilitário para processamento de datas e anos fiscais SAFT
    /// </summary>
    public class SAFTDate
    {
        /// <summary>
        /// Obtém o ano fiscal a partir de uma data
        /// </summary>
        public static int GetFiscalYear(DateTime date)
        {
            return date.Year;
        }
        
        /// <summary>
        /// Obtém a data de início do ano fiscal
        /// </summary>
        public static DateTime GetFiscalYearStartDate(int year)
        {
            return new DateTime(year, 1, 1);
        }
        
        /// <summary>
        /// Obtém a data de fim do ano fiscal
        /// </summary>
        public static DateTime GetFiscalYearEndDate(int year)
        {
            return new DateTime(year, 12, 31);
        }
        
        /// <summary>
        /// Valida se a data está dentro do período fiscal
        /// </summary>
        public static bool IsDateInFiscalYear(DateTime date, int fiscalYear)
        {
            return date.Year == fiscalYear;
        }
        
        /// <summary>
        /// Formata data no formato SAFT (yyyy-MM-dd)
        /// </summary>
        public static string FormatSAFTDate(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
        
        /// <summary>
        /// Formata data e hora no formato SAFT (yyyy-MM-ddTHH:mm:ss)
        /// </summary>
        public static string FormatSAFTDateTime(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm:ss");
        }
    }
}
