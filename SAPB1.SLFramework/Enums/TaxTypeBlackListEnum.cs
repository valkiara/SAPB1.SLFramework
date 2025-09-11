using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TaxTypeBlackListEnum
    {
        ttblExcluded = 0,
        ttblTaxable = 1,
        ttblExempt = 2,
        ttblNotTaxable = 3,
        ttblNonSubject = 4
    }
}
