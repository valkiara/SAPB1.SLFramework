using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TransactionTypeEnum
    {
        ttAllTransactions = 0,                     // Any document type
        ttOpeningBalance = 1,                      // Opening balance
        ttClosingBalance = 2,                      // Closing balance
        ttARInvoice = 3,                           // A/R invoice
        ttARCredItnote = 4,                         // A/R credit memo
        ttDelivery = 5,                            // Delivery
        ttReturn = 6,                              // Return
        ttAPInvoice = 7,                           // A/P invoice
        ttAPCreditNote = 8,                        // A/P credit memo
        ttPurchaseDeliveryNote = 9,                // Goods receipt PO
        ttPurchaseReturn = 10,                     // Goods return
        ttReceipt = 11,                            // Incoming payment
        ttDeposit = 12,                            // Deposit
        ttJournalEntry = 13,                       // Journal entry
        ttVendorPayment = 14,                      // Outgoing payment
        ttChequesForPayment = 15,                  // Checks for payment
        ttStockList = 16,                          // Inventory list
        ttGeneralReceiptToStock = 17,              // Goods receipt
        ttGeneralReleaseFromStock = 18,            // Goods issue
        ttTransferBetweenWarehouses = 19,          // Inventory transfer
        ttWorkInstructions = 20,                   // Work order
        ttLandedCosts = 21,                        // Landed costs
        ttDeferredDeposit = 22,                    // Postdated deposit
        ttCorrectionInvoice = 23,                  // Correction invoice
        ttInventoryValuation = 24,                 // Inventory revaluation
        ttAPCorrectionInvoice = 25,                // A/P correction invoice
        ttAPCorrectionInvoiceReversal = 26,        // A/P correction invoice reversal
        ttARCorrectionInvoice = 27,                // A/R correction invoice
        ttARCorrectionInvoiceReversal = 28,        // A/R correction invoice reversal
        ttBoETransaction = 29,                     // Bill of Exchange
        ttProductionOrder = 30,                    // Production order
        ttDownPayment = 31,                        // A/R down payment invoice
        ttPurchaseDownPayment = 32,                // A/P down payment invoice
        ttInternalReconciliation = 33,             // Internal reconciliation
        ttInventoryPosting = 34,                   // Inventory posting
        ttInventoryOpeningBalance = 35             // Inventory opening balance
    }
}
