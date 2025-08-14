using SAPB1.SLFramework.Abstractions.Models;

namespace SAPB1.SLFramework.Abstractions.Interfaces
{
    public interface ICashFlowLineItemsService
    {
        Task<IReadOnlyList<CashFlowLineItems>> ListAsync(CancellationToken cancellationToken = default);
    }
}
