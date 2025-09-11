using SAPB1.SLFramework.Enums;

namespace SapB1.Models
{
    /// <summary>
    /// SAP Business One - VatGroups entity
    /// </summary>
    public partial class VatGroups
    {
        /// <summary>Field: AcqstnRvrs — Whether VAT group is Acquisition/Reverse.</summary>
        public BoYesNoEnum? AcquisitionReverse { get; set; } // "tYES"/"tNO" (or null)

        /// <summary>Corresponding tax code when AcquisitionReverse is used.</summary>
        public string? AcquisitionReverseCorrespondingTaxCode { get; set; }

        /// <summary>Field: AcqsTax — G/L account for acquisition tax (length 15, FK to ChartOfAccounts).</summary>
        public string? AcquisitionTax { get; set; }

        /// <summary>Returns the DataBrowser object (DI API concept) — placeholder.</summary>
        public object? Browser { get; set; }

        /// <summary>G/L for cash discount (offset) account.</summary>
        public string? CashDiscountAccount { get; set; }

        /// <summary>
        /// Field: Category — Output vs Input tax group (Service Layer returns values like "bovcOutputTax", "bovcInputTax").
        /// </summary>
        public string? Category { get; set; }

        /// <summary>Field: Code (PK) — length 8.</summary>
        public string Code { get; set; } = default!;

        /// <summary>Field: Correction — Whether this is a correction VAT group.</summary>
        public BoYesNoEnum? Correction { get; set; } // "tYES"/"tNO"

        /// <summary>Field: DeferrAcc — G/L for deferred tax (length 15, FK to ChartOfAccounts).</summary>
        public string? DeferredTaxAcc { get; set; }

        /// <summary>Offset account for down payment tax.</summary>
        public string? DownPaymentTaxOffsetAccount { get; set; }

        /// <summary>Field: EBVatCateg — E-Books VAT category code.</summary>
        public string? EBooksVatCategory { get; set; }

        /// <summary>E-Books VAT exemption cause.</summary>
        public string? EBooksVATExemptionCause { get; set; }

        /// <summary>E-Books VAT expense classification category.</summary>
        public string? EBooksVatExpClassCategory { get; set; }

        /// <summary>E-Books VAT expense classification type.</summary>
        public string? EBooksVatExpClassType { get; set; }

        /// <summary>Field: EquAccount — Equalization tax G/L account.</summary>
        public string? EqualizationTaxAccount { get; set; }

        /// <summary>Field: IsEC — EU applicability ("tYES"/"tNO").</summary>
        public BoYesNoEnum? EU { get; set; }

        /// <summary>Whether to exclude from tax summary (country specific).</summary>
        public string? ExcludedTaxSummary { get; set; }

        /// <summary>
        /// Field: GoddsShip — Goods shipment indicator (1 numeric char); applicable when TriangularDeal is not set.
        /// </summary>
        public string? GoodsShipment { get; set; }

        /// <summary>Inactive flag ("tYES"/"tNO").</summary>
        public BoYesNoEnum? Inactive { get; set; }

        /// <summary>Field: Name — length 50.</summary>
        public string? Name { get; set; }

        /// <summary>Field: NonDedct — Non-deductible tax percentage.</summary>
        public double? NonDeduct { get; set; }

        /// <summary>Field: NonDedAcc — G/L for non-deductible tax (length 15, FK to ChartOfAccounts).</summary>
        public string? NonDeductAcc { get; set; }

        /// <summary>Field: r349c* — Country specific (ES) 349 report code.</summary>
        public string? Report349Code { get; set; }

        /// <summary>Field: SAFTTaxCod — Portuguese SAF-T tax code (length 10).</summary>
        public string? SAFTTaxCode { get; set; }

        /// <summary>Field: SAFTTxCdEx — Portuguese SAF-T “define new” tax code (length 10).</summary>
        public string? SAFTTaxCodeEx { get; set; }

        /// <summary>
        /// Field: ServSupply — Service supply indicator (1 numeric char).
        /// Cleared if TriangularDeal or GoodsShipment is set.
        /// </summary>
        public string? ServiceSupply { get; set; }

        /// <summary>Standard Tax Code mapping (country specific).</summary>
        public string? StandardTaxCode { get; set; }

        /// <summary>Field: Account — G/L account for the tax group (length 15, FK to ChartOfAccounts).</summary>
        public string? TaxAccount { get; set; }

        /// <summary>Reason for tax exemption (country/legislation specific).</summary>
        public string? TaxExemptionReason { get; set; }

        /// <summary>Field: TaxRegion — e.g., "vgtrPT".</summary>
        public string? TaxRegion { get; set; }

        /// <summary>Tax Type Black List code (e.g., "ttblExcluded").</summary>
        public TaxTypeBlackListEnum? TaxTypeBlackList { get; set; }

        /// <summary>
        /// Field: Indicator — Triangular deal indicator (1 numeric char);
        /// applicable when GoodsShipment is not set.
        /// </summary>
        public string? TriangularDeal { get; set; }

        /// <summary>DI API UserFields holder (placeholder).</summary>
        public object? UserFields { get; set; }

        /// <summary>Field: VatCrctn — Correction VAT group code (length 8, FK to VatGroups).</summary>
        public string? VatCorrection { get; set; }

        /// <summary>VAT deductible account.</summary>
        public string? VATDeductibleAccount { get; set; }

        /// <summary>Child collection: VAT group rates per effective date.</summary>
        public List<VatGroups_Lines> VatGroups_Lines { get; set; } = new();

        /// <summary>VAT in revenue account.</summary>
        public string? VATInRevenueAccount { get; set; }
    }


    /// <summary>
    /// A single line from VatGroups_Lines (VTG1).
    /// </summary>
    public partial class VatGroups_Lines
    {
        /// <summary>
        /// Field: EffecDate — Date from which the tax percentage is effective (mandatory).
        /// </summary>
        public DateTime Effectivefrom { get; set; }

        /// <summary>
        /// Field: Rate — VAT percentage for the group.
        /// </summary>
        public double Rate { get; set; }

        /// <summary>
        /// Field: EquVatPr — Equalization tax percentage (if applicable).
        /// </summary>
        public double? EqualizationTax { get; set; }

        /// <summary>
        /// DATEV code (country-specific).
        /// </summary>
        public string? DatevCode { get; set; }

        /// <summary>
        /// Placeholder for DI API UserFields (Service Layer won’t populate this directly).
        /// </summary>
        public object? UserFields { get; set; }
    }
}
