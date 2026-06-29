using SAPB1.SLFramework.Abstractions.Attributes;
using SAPB1.SLFramework.Enums;

namespace SAPB1.SLFramework.Abstractions.Models
{
    [ServiceLayerResourcePath("PickLists")]
    public class PickList
    {
        public int? Absoluteentry { get; set; }
        public string? Name { get; set; }
        public int? OwnerCode { get; set; }
        public string? OwnerName { get; set; }
        public DateTimeOffset? PickDate { get; set; }
        public string? Remarks { get; set; }
        public BoPickStatus? Status { get; set; }
        public string? ObjectType { get; set; }
        public BoYesNoEnum? UseBaseUnits { get; set; }
        public IList<PickListsLine> PickListsLines { get; set; } = [];
    }

    public class PickListParams
    {
        public int? Absoluteentry { get; set; }
    }

    public class PickListsLine
    {
        public int? AbsoluteEntry { get; set; }
        public int? LineNumber { get; set; }
        public int? OrderEntry { get; set; }
        public int? OrderRowID { get; set; }
        public double? PickedQuantity { get; set; }
        public BoPickStatus? PickStatus { get; set; }
        public double? ReleasedQuantity { get; set; }
        public double? PreviouslyReleasedQuantity { get; set; }
        public int? BaseObjectType { get; set; }
        public IList<SerialNumber> SerialNumbers { get; set; } = [];
        public IList<BatchNumber> BatchNumbers { get; set; } = [];
        public IList<DocumentLinesBinAllocation> DocumentLinesBinAllocations { get; set; } = [];
    }

    public class SerialNumber
    {
        public int? SystemSerialNumber { get; set; }
        public string? InternalSerialNumber { get; set; }
        public string? ManufacturerSerialNumber { get; set; }
        public double? Quantity { get; set; }
        public int? BaseLineNumber { get; set; }
        public string? ItemCode { get; set; }
    }

    public class BatchNumber
    {
        public string? BatchNumberProperty { get; set; }
        public string? ManufacturerSerialNumber { get; set; }
        public string? InternalSerialNumber { get; set; }
        public double? Quantity { get; set; }
        public int? BaseLineNumber { get; set; }
        public string? ItemCode { get; set; }
    }

    public class DocumentLinesBinAllocation
    {
        public int? BinAbsEntry { get; set; }
        public double? Quantity { get; set; }
        public int? AllowNegativeQuantity { get; set; }
        public int? SerialAndBatchNumbersBaseLine { get; set; }
        public int? BaseLineNumber { get; set; }
    }
}
