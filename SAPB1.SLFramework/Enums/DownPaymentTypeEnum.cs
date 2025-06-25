using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DownPaymentTypeEnum
    {
        dptRequest = 0,
        dptInvoice = 1
    }
}
