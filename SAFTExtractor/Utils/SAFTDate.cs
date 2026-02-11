namespace SAFTExtractor.Utils
{
    /// <summary>
    /// Utilitário para processamento de datas e anos fiscais SAFT
    /// </summary>
    public class SAFTDate
    {
        /// <summary>
        /// Ano fiscal
        /// </summary>
        public string FiscalYear { get; set; } = string.Empty;
        
        /// <summary>
        /// Data máxima permitida para o ano fiscal
        /// </summary>
        public DateTime MaxEndDate { get; set; }
        
        /// <summary>
        /// Data mínima permitida para o ano fiscal
        /// </summary>
        public DateTime MinEndDate { get; set; }
        
        /// <summary>
        /// Obtém o ano fiscal a partir de uma data
        /// </summary>
        public static int GetFiscalYear(DateTime date)
        {
            return date.Year;
        }
        
        /// <summary>
        /// Obtém informações completas do ano fiscal a partir de uma data
        /// </summary>
        public static SAFTDate GetFiscalYearInfo(DateTime date)
        {
            return new SAFTDate
            {
                FiscalYear = date.Year.ToString(),
                MinEndDate = new DateTime(date.Year, 1, 1),
                MaxEndDate = new DateTime(date.Year, 12, 31)
            };
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
        /// Valida se duas datas estão no mesmo ano fiscal
        /// </summary>
        public static bool AreDatesInSameFiscalYear(DateTime startDate, DateTime endDate)
        {
            return startDate.Year == endDate.Year;
        }
        
        /// <summary>
        /// Valida se a data está dentro do intervalo fiscal
        /// </summary>
        public static bool IsDateInFiscalRange(DateTime date, SAFTDate fiscalInfo)
        {
            return date >= fiscalInfo.MinEndDate && date <= fiscalInfo.MaxEndDate;
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
