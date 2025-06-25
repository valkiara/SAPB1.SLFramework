using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BoPaymentsObjectType
    {
        bopot_IncomingPayments = 0,
        bopot_OutgoingPayments = 1
    }
}
