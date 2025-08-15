using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    /// <summary>
    /// Payment priority levels (DI API / Service Layer).
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BoPaymentPriorities
    {
        /// <summary>Payment priority level 1.</summary>
        bopp_Priority_1 = 0,

        /// <summary>Payment priority level 2.</summary>
        bopp_Priority_2 = 1,

        /// <summary>Payment priority level 3.</summary>
        bopp_Priority_3 = 2,

        /// <summary>Payment priority level 4.</summary>
        bopp_Priority_4 = 3,

        /// <summary>Payment priority level 5.</summary>
        bopp_Priority_5 = 4,

        /// <summary>Payment priority level 6.</summary>
        bopp_Priority_6 = 5
    }
}
