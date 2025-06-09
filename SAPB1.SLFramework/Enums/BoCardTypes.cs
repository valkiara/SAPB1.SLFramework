using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BoCardTypes
    {
        cCustomer = 0,
        cSupplier = 1,
        cLid = 2,
    }
}
