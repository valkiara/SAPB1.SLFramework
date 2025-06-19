using SAPB1.SLFramework.Abstractions.Attributes;

namespace SAPB1.SLFramework.Abstractions.Models
{
    [SapTable("OITM")]
    public class Items
    {
        public required string ItemCode { get; set; }
        public required string ItemName { get; set; }
    }
}
