using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Abstractions.Enums
{
    /// <summary>
    /// Defines the method by which items are received at receiving bin locations.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ReceivingBinLocationsMethodEnum
    {
        /// <summary>
        /// 0 – Bin Location Code Order  
        /// Receives items at receiving bin locations according to the alphanumeric order of their bin location codes.
        /// </summary>
        rblmBinLocationCodeOrder = 0,

        /// <summary>
        /// 1 – Alternative Sort Code Order  
        /// Receives items at receiving bin locations according to the alphanumeric order of their alternative sort codes.
        /// </summary>
        rblmAlternativeSortCodeOrder = 1
    }
}
