using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BoItemTreeTypes
    {
        iNotATree = 0,       // The item is not a tree.
        iAssemblyTree = 1,   // Sets item to an assembly tree.
        iSalesTree = 2,      // Sets item to a sales tree.
        iProductionTree = 3, // Sets item to a production tree.
        iTemplateTree = 4,   // Sets item to a template tree.
        iIngredient = 5      // Sets item to ingredient.
    }

    /*
     * Remarks:
     * Specifies the tree type for an item (used in BOMs and structures):
     * - 'iNotATree': The item is not part of a tree.
     * - 'iAssemblyTree': Defines an assembly tree.
     * - 'iSalesTree': Defines a sales tree.
     * - 'iProductionTree': Defines a production tree.
     * - 'iTemplateTree': Defines a template tree.
     * - 'iIngredient': Defines an ingredient item in a recipe/BOM context.
     */
}
