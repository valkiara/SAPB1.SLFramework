using SAPB1.SLFramework.Abstractions.Attributes;

namespace SAPB1.SLFramework.Abstractions.Models
{
    [SapTable("ORCT")]
    public partial class IncomingPayments : Payments
    {
        public IncomingPayments()
        {
            DocObjectCode = Enums.BoPaymentsObjectType.bopot_IncomingPayments;
        }
    }
}
