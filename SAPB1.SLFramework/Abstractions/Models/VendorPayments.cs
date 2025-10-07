using SAPB1.SLFramework.Abstractions.Attributes;
using SAPB1.SLFramework.Enums;

namespace SAPB1.SLFramework.Abstractions.Models
{
    [SapTable("ORCT")]
    public partial class VendorPayments : Payments
    {
        public VendorPayments()
        {
            DocObjectCode = BoPaymentsObjectType.bopot_OutgoingPayments;
        }
    }
}
