using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SAPB1.SLFramework.Enums
{
    [Serializable]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum UDFLinkedSystemObjectTypesEnum
    {
        ulNone = 0,
        ulChartOfAccounts = 1,
        ulBusinessPartners = 2,
        ulBanks = 3,
        ulItems = 4,
        ulUsers = 12,
        ulInvoices = 13,
        ulCreditNotes = 14,
        ulDeliveryNotes = 15,
        ulReturns = 16,
        ulOrders = 17,
        ulPurchaseInvoices = 18,
        ulPurchaseCreditNotes = 19,
        ulPurchaseDeliveryNotes = 20,
        ulPurchaseReturns = 21,
        ulPurchaseOrders = 22,
        ulQuotations = 23,
        ulIncomingPayments = 24,
        ulDepositsService = 25,
        ulJournalEntries = 30,
        ulContacts = 33,
        ulVendorPayments = 46,
        ulChecksforPayment = 57,
        ulInventoryGenEntry = 59,
        ulInventoryGenExit = 60,
        ulWarehouses = 64,
        ulProductTrees = 66,
        ulStockTransfer = 67,
        ulSalesOpportunities = 97,
        ulDrafts = 112,
        ulMaterialRevaluation = 162,
        ulEmployeesInfo = 171,
        ulCustomerEquipmentCards = 176,
        ulServiceContracts = 190,
        ulServiceCalls = 191,
        ulProductionOrders = 202,
        ulInventoryTransferRequest = 1250000001,
        ulBlanketAgreementsService = 1250000025,
        ulProjectManagementService = 234000021,
        ulReturnRequest = 234000031,
        ulGoodsReturnRequest = 234000032
    }
}
