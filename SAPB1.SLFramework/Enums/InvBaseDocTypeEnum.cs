using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum InvBaseDocTypeEnum
    {
        /// <summary>
        /// Not specified, use the default value in the database.
        /// </summary>
        Default = 0,

        /// <summary>
        /// Set to empty.
        /// </summary>
        Empty = 1,

        /// <summary>
        /// Goods Receipt PO type (Purchase Delivery Notes).
        /// </summary>
        PurchaseDeliveryNotes = 2,

        /// <summary>
        /// Inventory General Entry document type.
        /// </summary>
        InventoryGeneralEntry = 3,

        /// <summary>
        /// Warehouse Transfers document type.
        /// </summary>
        WarehouseTransfers = 4,

        /// <summary>
        /// Inventory Transfer Request document type.
        /// </summary>
        InventoryTransferRequest = 5
    }
}
