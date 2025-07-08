using SAPB1.SLFramework.Enums;

namespace SAPB1.SLFramework.Abstractions.Models
{
    public partial class CompanyInfo
    {
        public int? Version { get; set; }
        public BoYesNoEnum? EnableExpensesManagement { get; set; }
        public BoYesNoEnum? EnableAccountSegmentation { get; set; }
        public BoYesNoEnum? EnableBillOfExchange { get; set; }
        public BoBaseDateRateEnum? BaseDateForExchangeRate { get; set; }
        public int? BISRBankActKey { get; set; }
        public string BISRBankCountry { get; set; }
        public string BISRBankNo { get; set; }
        public string BISRBankAccount { get; set; }
        public string BISRBranch { get; set; }
        public int? MaxRecordsInChooseFromList { get; set; }
        public BoYesNoEnum? EnableCheckQuantityInRDR { get; set; }
        public BoManageMethod? SRIManagementSystem { get; set; }
        public BoYesNoEnum? AutoSRICreationOnReceipt { get; set; }
        public BoYesNoEnum? IEPSPayer { get; set; }
        public int? DefaultDaysForOrdCanc { get; set; }
        public double? PercentOfTotalAcquisition { get; set; }
        public double? MinimumBaseAmountPerDoc { get; set; }
        public BoYesNoEnum? EnableSharingSeries { get; set; }
        public BoYesNoEnum? DataOwnershipIndication { get; set; }
        public double? MinimumAmountForAppndixOP { get; set; }
        public BoYesNoEnum? DisplayTransactionsByDflt { get; set; }
        public string DefaultStampTax { get; set; }
        public double? MinimumAmountForAnnualList { get; set; }
        public BoYesNoEnum? BlockStockNegativeQuantity { get; set; }
        public BoYesNoEnum? AutoCreateCustomerEqCard { get; set; }
        public int? MaxNumberOfDocumentsInPmt { get; set; }
        public BoYesNoEnum? EnableStockRelNoCostPrice { get; set; }
        public string CompanyName { get; set; }
        public BoYesNoEnum? GroupLinesInVATCalculation { get; set; }
        public TaxCalcSysEnum? TaxCalculationSystem { get; set; }
        public BoYesNoEnum? EnableTransactionNotification { get; set; }
        public BoYesNoEnum? EnableConversionDifferentAcct { get; set; }
        public int? B1iTimeOut { get; set; }
        public string Localization { get; set; }
    }
}
