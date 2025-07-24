using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CancelStatusEnum
    {
        /// <summary>
        /// The document is cancelled.
        /// </summary>
        csYes = 0,

        /// <summary>
        /// The document is not cancelled.
        /// </summary>
        csNo = 1,

        /// <summary>
        /// The document is a cancellation document.
        /// </summary>
        csCancellation = 2
    }
}
