using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BoRcptTypes
    {
        rCustomer = 'C',
        rAccount = 'A',
        rSupplier = 'S'
    }
}
