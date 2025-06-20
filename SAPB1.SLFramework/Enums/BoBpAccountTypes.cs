using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BoBpAccountTypes
    {
        bpat_General = 0,
        bpat_DownPayment = 1,
        bpat_AssetsAccount = 2,
        bpat_Receivable = 3,
        bpat_Payable = 4,
        bpat_OnCollection = 5,
        bpat_Presentation = 6,
        bpat_AssetsPayable = 7,
        bpat_Discounted = 8,
        bpat_Unpaid = 9,
        bpat_OpenDebts = 10,
        bpat_Domestic = 11,
        bpat_Foreign = 12,
        bpat_CashDiscountInterim = 13,
        bpat_ExchangeRateInterim = 14
    }

}
