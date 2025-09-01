using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BoTaxTypes
    {
        tt_Yes = 0,        // Yes
        tt_No = 1,         // No
        tt_UseTax = 2,     // Use Tax
        tt_OffsetTax = 3   // Offset Tax
    }

    /*
     * Remarks:
     * Defines the sales tax system for USA and Canada:
     * - 'tt_Yes': Tax is applied.
     * - 'tt_No': No tax is applied.
     * - 'tt_UseTax': Use Tax is applied.
     * - 'tt_OffsetTax': Offset Tax is applied.
     */
}
