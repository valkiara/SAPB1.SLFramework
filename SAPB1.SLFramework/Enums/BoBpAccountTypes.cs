using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BoBpAccountTypes
    {
        Bpat_General = 0,
        Bpat_DownPayment = 1,
        Bpat_AssetsAccount = 2,
        Bpat_Receivable = 3,
        Bpat_Payable = 4,
        Bpat_OnCollection = 5,
        Bpat_Presentation = 6,
        Bpat_AssetsPayable = 7,
        Bpat_Discounted = 8,
        Bpat_Unpaid = 9,
        Bpat_OpenDebts = 10,
        Bpat_Domestic = 11,
        Bpat_Foreign = 12,
        Bpat_CashDiscountInterim = 13,
        Bpat_ExchangeRateInterim = 14
    }

}
