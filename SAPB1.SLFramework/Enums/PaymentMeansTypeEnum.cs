using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentMeansTypeEnum
    {
        pmtNotAssigned = 0,
        pmtChecks = 1,
        pmtBankTransfer = 2,
        pmtCash = 3,
        pmtCreditCard = 4
    }
}
