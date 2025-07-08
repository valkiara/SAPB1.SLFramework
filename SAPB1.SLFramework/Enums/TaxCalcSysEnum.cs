using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TaxCalcSysEnum
    {
      
        PreconfiguredFormulaWithJurisdictionSupport = 0,
        UserDefinedFormula = 1,
        PreconfiguredFormula = 2
    }
}
