using SAPB1.SLFramework.Enums;

namespace SAPB1.SLFramework.Abstractions.Models
{
    public class CashFlowLineItems
    {
        public BoYesNoEnum? ActiveLineItem { get; set; }
        public int? Drawer { get; set; }
        public int? Level { get; set; }
        public int LineItemID { get; set; }
        public string? LineItemName { get; set; }
        public int? ParentArticle { get; set; }
    }
}
