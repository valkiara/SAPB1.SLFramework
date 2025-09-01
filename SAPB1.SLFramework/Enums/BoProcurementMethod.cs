using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BoProcurementMethod
    {
        bom_Buy = 0,   // Buy the items from vendors.
        bom_Make = 1   // Make the items internally.
    }

    /*
     * Remarks:
     * Defines how the item is procured:
     * - Use 'bom_Buy' when the item is purchased from vendors.
     * - Use 'bom_Make' when the item is manufactured internally.
     */
}
