using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BoGLMethods
    {
        glm_WH = 0,         // Default G/L accounts are set by the warehouse definition (Warehouses).
        glm_ItemClass = 1,  // Default G/L accounts are set by the item group definition (ItemGroups).
        glm_ItemLevel = 2   // The G/L account is set in the item level.
    }

    /*
     * Remarks:
     * Defines the default G/L accounts for posting transactions related to the item:
     * - Use 'glm_WH' when G/L accounts are derived from warehouse setup.
     * - Use 'glm_ItemClass' when G/L accounts are derived from the item group.
     * - Use 'glm_ItemLevel' when G/L accounts are specified at the item master level.
     */
}
