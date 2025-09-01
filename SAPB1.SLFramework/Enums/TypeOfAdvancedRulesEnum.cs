using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TypeOfAdvancedRulesEnum
    {
        toarGeneral = 0,    // General
        toarWarehouse = 1,  // Warehouse
        toarItemGroup = 2   // Item group
    }

    /*
     * Remarks:
     * Specifies the advanced rule type assigned to an item:
     * - Use 'toar_General' for rules that apply generally.
     * - Use 'toar_Warehouse' for rules specific to warehouse settings.
     * - Use 'toar_ItemGroup' for rules specific to the item group.
     */
}
