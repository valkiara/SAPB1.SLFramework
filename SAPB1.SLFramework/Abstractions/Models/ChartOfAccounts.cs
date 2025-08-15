using System;
using SAPB1.SLFramework.Enums;

namespace SAPB1.SLFramework.Abstractions.Models
{
    /// <summary>
    /// SAP B1 Chart of Accounts (OACT) model for Service Layer.
    /// </summary>
    public class ChartOfAccounts
    {
        // ——— Structure / classification ———

        /// <summary>Returns the level of the account. (OACT.Levels)</summary>
        public int? AccountLevel { get; set; }

        /// <summary>Specifies the account type (revenues, expenses, other). (OACT.ActType)</summary>
        public BoAccountTypes? AccountType { get; set; }

        /// <summary>G/L account code. Mandatory if segmentation is not used. (OACT.AcctCode)</summary>
        public string? Code { get; set; }

        /// <summary>Account name (max 100). (OACT.AcctName)</summary>
        public string? Name { get; set; }

        /// <summary>Account number when segmentation is defined (max 210). Mandatory with segmentation. (OACT.FormatCode)</summary>
        public string? FormatCode { get; set; }

        /// <summary>Parent account key (max 15). Mandatory. (OACT.FatherNum)</summary>
        public string? FatherAccountKey { get; set; }

        /// <summary>Category (foreign key to OACG). (OACT.Category)</summary>
        public int? Category { get; set; }

        /// <summary>Primary account flag (fixed). (OACT.Fixed)</summary>
        public BoYesNoEnum? PrimaryAccount { get; set; }

        // ——— States / flags ———

        /// <summary>Active (postable) vs title account. (OACT.Postable)</summary>
        public BoYesNoEnum? ActiveAccount { get; set; }

        /// <summary>Cash/monetary account flag. (OACT.CashBox)</summary>
        public BoYesNoEnum? CashAccount { get; set; }

        /// <summary>Budget relevant. (OACT.BudgAccount)</summary>
        public BoYesNoEnum? BudgetAccount { get; set; }

        /// <summary>Allow VAT group changes. (OACT.VatChange)</summary>
        public BoYesNoEnum? AllowChangeVatGroup { get; set; }

        /// <summary>Allow multiple linking within same template. (OACT.MultiLink)</summary>
        public BoYesNoEnum? AllowMultipleLinking { get; set; }

        /// <summary>Cost accounting only. (property)</summary>
        public BoYesNoEnum? CostAccountingOnly { get; set; }

        /// <summary>Cash flow relevant. (property)</summary>
        public BoYesNoEnum? CashFlowRelevant { get; set; }

        /// <summary>Project relevant. (OACT.PrjRelvnt)</summary>
        public BoYesNoEnum? ProjectRelevant { get; set; }

        /// <summary>Protected (confidential). (OACT.Protected)</summary>
        public BoYesNoEnum? Protected { get; set; }

        /// <summary>Include in rate conversion differences. (OACT.RateTrans)</summary>
        public BoYesNoEnum? RateConversion { get; set; }

        /// <summary>Reconciliation account. (OACT.RealAcct)</summary>
        public BoYesNoEnum? ReconciledAccount { get; set; }

        /// <summary>Liable for advances. (OACT.Advance)</summary>
        public BoYesNoEnum? LiableForAdvances { get; set; }

        /// <summary>Lock manual transaction to top drawers. (OACT.LocManTran)</summary>
        public BoYesNoEnum? LockManualTransaction { get; set; }

        /// <summary>Account valid flag. (property)</summary>
        public BoYesNoEnum? ValidFor { get; set; }

        /// <summary>Frozen flag. (property)</summary>
        public BoYesNoEnum? FrozenFor { get; set; }

        /// <summary>Tax exempt account. (OACT.ExmIncome)</summary>
        public BoYesNoEnum? TaxExemptAccount { get; set; }

        /// <summary>Tax liable account. (property)</summary>
        public BoYesNoEnum? TaxLiableAccount { get; set; }

        // ——— Dates / validity ———

        /// <summary>Valid from. (property)</summary>
        public DateTime? ValidFrom { get; set; }

        /// <summary>Valid to. (property)</summary>
        public DateTime? ValidTo { get; set; }

        /// <summary>Valid remarks. (property)</summary>
        public string? ValidRemarks { get; set; }

        /// <summary>Freeze from date. (property)</summary>
        public DateTime? FrozenFrom { get; set; }

        /// <summary>Freeze to date. (property)</summary>
        public DateTime? FrozenTo { get; set; }

        /// <summary>Freeze remarks. (property)</summary>
        public string? FrozenRemarks { get; set; }

        // ——— Currency / balances ———

        /// <summary>Account currency (3 chars). (OACT.ActCurr)</summary>
        public string? AcctCurrency { get; set; }

        /// <summary>Balance in local currency. (OACT.CurrTotal)</summary>
        public double? Balance { get; set; }

        /// <summary>Balance in foreign currency. (OACT.FcTotal)</summary>
        public double? Balance_FrgnCurr { get; set; }

        /// <summary>Balance in system currency. (OACT.SysTotal)</summary>
        public double? Balance_syscurr { get; set; }

        // ——— VAT / taxation ———

        /// <summary>Default VAT group (8 chars). (OACT.DfltVat)</summary>
        public string? DefaultVatGroup { get; set; }

        /// <summary>VAT registration number. (property)</summary>
        public string? VATRegNum { get; set; }

        // ——— Distribution rules / loading factors ———

        /// <summary>Distribution rule relevant (dim 1). (OACT.Dim1Relvnt)</summary>
        public BoYesNoEnum? DistributionRuleRelevant { get; set; }

        /// <summary>Distribution rule relevant (dim 2). (OACT.Dim2Relvnt)</summary>
        public BoYesNoEnum? DistributionRule2Relevant { get; set; }

        /// <summary>Distribution rule relevant (dim 3). (OACT.Dim3Relvnt)</summary>
        public BoYesNoEnum? DistributionRule3Relevant { get; set; }

        /// <summary>Distribution rule relevant (dim 4). (OACT.Dim4Relvnt)</summary>
        public BoYesNoEnum? DistributionRule4Relevant { get; set; }

        /// <summary>Distribution rule relevant (dim 5). (OACT.Dim5Relvnt)</summary>
        public BoYesNoEnum? DistributionRule5Relevant { get; set; }

        /// <summary>Loading type (association with distribution rules). (OACT.OverType)</summary>
        public BoYesNoEnum? LoadingType { get; set; }

        /// <summary>Distribution rule (dim 1). (OACT.OverCode)</summary>
        public string? LoadingFactorCode { get; set; }

        /// <summary>Distribution rule (dim 2). (OACT.OverCode2)</summary>
        public string? LoadingFactorCode2 { get; set; }

        /// <summary>Distribution rule (dim 3). (OACT.OverCode3)</summary>
        public string? LoadingFactorCode3 { get; set; }

        /// <summary>Distribution rule (dim 4). (OACT.OverCode4)</summary>
        public string? LoadingFactorCode4 { get; set; }

        /// <summary>Distribution rule (dim 5). (OACT.OverCode5)</summary>
        public string? LoadingFactorCode5 { get; set; }

        // ——— Reconciliation / matching ———

        /// <summary>External reconciliation number. (OACT.ExtrMatch)</summary>
        public int? ExternalReconNo { get; set; }

        /// <summary>Internal reconciliation number. (OACT.IntrMatch)</summary>
        public int? InternalReconNo { get; set; }

        /// <summary>Revaluation coordinated. (OACT.RevalMatch)</summary>
        public BoYesNoEnum? RevaluationCoordinated { get; set; }

        // ——— Texts / codes ———

        /// <summary>Details (max 254). (OACT.Details)</summary>
        public string? Details { get; set; }

        /// <summary>Foreign/alternate name (max 100). (OACT.FrgnName)</summary>
        public string? ForeignName { get; set; }

        /// <summary>Alternate export code (max 10). (OACT.ExportCode)</summary>
        public string? DataExportCode { get; set; }

        /// <summary>External code (max 12). (OACT.AccntntCod)</summary>
        public string? ExternalCode { get; set; }

        /// <summary>Official account code (max 15). (OACT.OffcCode)</summary>
        public string? OfficialAccountCode { get; set; }

        /// <summary>Planning level (2 chars). (OACT.PlngLevel)</summary>
        public string? PlanningLevel { get; set; }

        /// <summary>Project code (8 chars). (OACT.Project)</summary>
        public string? ProjectCode { get; set; }

        /// <summary>Transaction code (foreign key to OTRC, 4 chars). (OACT.TransCode)</summary>
        public string? TransactionCode { get; set; }

        /// <summary>Taxonomy code. (property)</summary>
        public string? TaxonomyCode { get; set; }

        /// <summary>Standard account code. (property)</summary>
        public string? StandardAccountCode { get; set; }

        /// <summary>Referential account code. (property)</summary>
        public string? ReferentialAccountCode { get; set; }

        /// <summary>Cost element code. (property)</summary>
        public string? CostElementCode { get; set; }

        /// <summary>Cost element relevant. (property)</summary>
        public BoYesNoEnum? CostElementRelevant { get; set; }

        /// <summary>Account purpose code. (property)</summary>
        public string? AccountPurposeCode { get; set; }

        /// <summary>Block manual posting. (property)</summary>
        public BoYesNoEnum? BlockManualPosting { get; set; }

        /// <summary>Business place ID. (property)</summary>
        public int? BPLID { get; set; }

        /// <summary>Business place name. (property)</summary>
        public string? BPLName { get; set; }

        /// <summary>PCN874 report relevant. (property)</summary>
        public BoYesNoEnum? PCN874ReportRelevant { get; set; }

        /// <summary>Income classification category. (OACT.InClassCat)</summary>
        public int? IncomeClassificationCategory { get; set; }

        /// <summary>Income classification type. (OACT.InClassTyp)</summary>
        public int? IncomeClassificationType { get; set; }

        /// <summary>Expense classification category. (OACT.ExClassCat)</summary>
        public int? ExpenseClassificationCategory { get; set; }

        /// <summary>Expense classification type. (OACT.ExClassTyp)</summary>
        public int? ExpenseClassificationType { get; set; }

        /// <summary>Datev account (localization). (property)</summary>
        public string? DatevAccount { get; set; }

        /// <summary>Datev auto account (localization). (property)</summary>
        public string? DatevAutoAccount { get; set; }

        /// <summary>Datev first data entry (localization). (property)</summary>
        public BoYesNoEnum? DatevFirstDataEntry { get; set; }
    }
}