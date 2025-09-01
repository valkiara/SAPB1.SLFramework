using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BoPlanningSystem
    {
        bop_MRP = 0,   // Material Requirement Planning system.
        bop_None = 1   // No planning system.
    }

    /*
     * Remarks:
     * This property defines the default inventory planning system for an item.
     * - Use 'bop_MRP' when the item is planned through the MRP run.
     * - Use 'bop_None' when the item is excluded from MRP calculations.
     */
}
