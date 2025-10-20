using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    /// <summary>
    /// Defines document sub-types used for creating documents with separate numbering.
    /// Source: SAP Business One DI API 10.0 (10.00.300)
    /// </summary>
    public enum BoDocumentSubType
    {
        /// <summary>
        /// None.
        /// </summary>
        bod_None = 0,

        /// <summary>
        /// Exempt invoice.
        /// </summary>
        bod_InvoiceExempt = 1,

        /// <summary>
        /// Debit memo.
        /// </summary>
        bod_DebitMemo = 2,

        /// <summary>
        /// Bill.
        /// </summary>
        bod_Bill = 3,

        /// <summary>
        /// Exempt bill.
        /// </summary>
        bod_ExemptBill = 4,

        /// <summary>
        /// Purchase debit memo.
        /// </summary>
        bod_PurchaseDebitMemo = 5,

        /// <summary>
        /// Export invoice.
        /// </summary>
        bod_ExportInvoice = 6,

        /// <summary>
        /// GST tax invoice.
        /// </summary>
        bod_GSTTaxInvoice = 7,

        /// <summary>
        /// GST debit memo.
        /// </summary>
        bod_GSTDebitMemo = 8,

        /// <summary>
        /// Refund voucher.
        /// </summary>
        bod_RefundVoucher = 9
    }
}
