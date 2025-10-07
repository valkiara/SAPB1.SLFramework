using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Abstractions.Enums
{
    /// <summary>
    /// Specifies the receiving method that defines the constraints for receiving items into a warehouse.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ReceivingUpToMethodEnum
    {
        /// <summary>
        /// 0 – Maximum Quantity  
        /// Receiving is limited by the maximum quantity allowed.
        /// </summary>
        rutmMaximumQty = 0,

        /// <summary>
        /// 1 – Maximum Weight  
        /// Receiving is limited by the maximum weight allowed.
        /// </summary>
        rutmMaximumWeight = 1,

        /// <summary>
        /// 2 – Both Maximum Quantity and Weight  
        /// Receiving is limited by both maximum quantity and maximum weight constraints.
        /// </summary>
        rutmBothMaxQtyAndWeight = 2
    }
}
