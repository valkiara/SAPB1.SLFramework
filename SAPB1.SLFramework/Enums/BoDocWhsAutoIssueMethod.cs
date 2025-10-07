using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BoDocWhsAutoIssueMethod
    {
        /// <summary>
        /// 0 – Single Choice Only  
        /// Allocates items only when there is one possible bin location.  
        /// If multiple options exist, no automatic allocation is performed.
        /// </summary>
        whsBinSingleChoiceOnly = 0,

        /// <summary>
        /// 1 – Bin Location Code Order  
        /// Allocates items based on the alphanumeric order of bin location codes.
        /// </summary>
        whsBinCodeOrder = 1,

        /// <summary>
        /// 2 – Alternative Sort Code Order  
        /// Allocates items according to the alphanumeric order of alternative sort codes.
        /// </summary>
        whsAlternativeSortCodeOrder = 2,

        /// <summary>
        /// 3 – Quantity Descending Order  
        /// Allocates items from bin locations starting with the largest available quantity.
        /// </summary>
        whsQtyDescendingOrder = 3,

        /// <summary>
        /// 4 – Quantity Ascending Order  
        /// Allocates items from bin locations starting with the smallest available quantity.
        /// </summary>
        whsQtyAscendingOrder = 4,

        /// <summary>
        /// 5 – Bin FIFO (First In, First Out)
        /// </summary>
        whsBinFIFO = 5,

        /// <summary>
        /// 6 – Bin LIFO (Last In, First Out)
        /// </summary>
        whsBinLIFO = 6,

        /// <summary>
        /// 7 – Single Bin Preferred  
        /// Prefer a single bin location for allocation when possible.
        /// </summary>
        whsSingleBinPreferred = 7
    }
}
