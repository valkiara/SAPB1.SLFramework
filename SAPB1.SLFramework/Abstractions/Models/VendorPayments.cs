using SAPB1.SLFramework.Abstractions.Attributes;

namespace SAPB1.SLFramework.Abstractions.Models
{
    [SapTable("ORCT")]
    public partial class VendorPayments : Payments
    {
        public VendorPayments()
        {
            DocObjectCode = Enums.BoPaymentsObjectType.bopot_OutgoingPayments;
        }
    }
}
