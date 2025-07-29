using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BoAPARDocumentTypes
    {
        /// <summary>Sales invoice type (INV)</summary>
        bodt_Invoice = 13,

        /// <summary>Sales credit memo type (RIN)</summary>
        bodt_CreditNote = 14,

        /// <summary>Sales delivery note type (DLN)</summary>
        bodt_DeliveryNote = 15,

        /// <summary>Sales returns type (RDN)</summary>
        bodt_Return = 16,

        /// <summary>Sales order type (ORD)</summary>
        bodt_Order = 17,

        /// <summary>Purchase invoice type (PCH)</summary>
        bodt_PurchaseInvoice = 18,

        /// <summary>Purchase credit memo type (RPC)</summary>
        bodt_PurchaseCreditNote = 19,

        /// <summary>Goods receipt PO type (PDN)</summary>
        bodt_PurchaseDeliveryNote = 20,

        /// <summary>Purchase goods returns type (RPD)</summary>
        bodt_PurchaseReturn = 21,

        /// <summary>Purchase order type (POR)</summary>
        bodt_PurchaseOrder = 22,

        /// <summary>Sales quotation type (QUT)</summary>
        bodt_Quotation = 23,

        /// <summary>A/P correction invoice (CORR-AP)</summary>
        bodt_CorrectionAPInvoice = 163,

        /// <summary>A/R correction invoice (CORR-AR)</summary>
        bodt_CorrectionARInvoice = 165,

        /// <summary>Purchase quotation type (PQUT)</summary>
        bodt_PurchaseQutation = 540000006
    }
}
