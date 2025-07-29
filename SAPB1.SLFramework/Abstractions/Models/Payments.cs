using SAPB1.SLFramework.Enums;

namespace SAPB1.SLFramework.Abstractions.Models
{
    public abstract class Payments
    {
        /// <summary>
        ///Returns the document entry key that uniquely identifies the payment document
        /// </summary>
        public int? DocEntry { get; set; }

        public BoPaymentsObjectType? DocObjectCode { get; set; }

        public BoRcptTypes? DocType { get; set; }

        /// <summary>
        ///Sets or returns the business partner code or the account code.
        /// </summary>
        public string? CardCode { get; set; }

        /// <summary>
        /// Sets or returns the business partner's full name.
        /// </summary>
        public string? CardName { get; set; }


        /// <summary>
        /// Sets or returns the posting date of the payment document.
        /// </summary>
        public DateTimeOffset DocDate { get; set; }

        public double? DocRate { get; set; }
        /// <summary>
        /// Sequential number of the agreement that is assigned automatically by SAP Business One.
        /// </summary>
        public int? BlanketAgreement { get; set; }

        /// <summary>
        /// Sets or returns the document code of the currency used in this payment.
        /// </summary>
        public string? DocCurrency { get; set; }


        /// <summary>
        /// Sets or returns the cash G/L account used for this payment.
        /// </summary>
        public string? CashAccount { get; set; }

        /// <summary>
        /// Sets or returns the amount of cash in the current payment in local currency.
        /// </summary>
        public double? CashSum { get; set; }

        public BoYesNoEnum? IsPayToBank { get; set; }
    }

    public class PaymentAccount
    {
        public string? AccountCode { get; set; }
        public double? SumPaid { get; set; }
    }

    public class PaymentInvoice
    {
        public int DocEntry { get; set; }
        public BoRcptInvTypes? InvoiceType { get; set; }
        public double? PaidSum { get; set; }
        public double? SumApplied { get; set; }
        public double? AppliedFC { get; set; }
    }

    public class CashFlowAssignment
    {
        public PaymentMeansTypeEnum? PaymentMeans { get; set; }
        public double? AmountLC { get; set; }
        public double? AmountFC { get; set; }
    }
}
