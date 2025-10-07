using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Abstractions.Enums
{
    /// <summary>
    /// Defines the automatic allocation method for items when they are received into bin locations.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AutoAllocOnReceiptMethodEnum
    {
        /// <summary>
        /// 0 – Default Bin  
        /// Allocates received items to the warehouse's default bin.
        /// </summary>
        aaormDefaultBin = 0,

        /// <summary>
        /// 1 – Last Bin Received Item  
        /// Allocates items to the last bin location where the same item was received.
        /// </summary>
        aaormLastBinReceivedItem = 1,

        /// <summary>
        /// 2 – Item Current Bins  
        /// Allocates items to bin locations where the item currently exists.
        /// </summary>
        aaormItemCurrentBins = 2,

        /// <summary>
        /// 3 – Item Current and Historical Bins  
        /// Allocates items to bin locations where the item currently exists or was stored in the past.
        /// </summary>
        aaormItemCurrentAndHistoricalBins = 3
    }
}
