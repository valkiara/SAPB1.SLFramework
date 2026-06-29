using SAPB1.SLFramework.Abstractions.Models;

namespace SAPB1.SLFramework.Abstractions.Interfaces
{
    public interface IPickListsService
    {
        Task<PickList> GetReleasedAllocationAsync(int absoluteEntry, CancellationToken cancellationToken = default);
        Task CloseAsync(PickList pickList, CancellationToken cancellationToken = default);
        Task UpdateReleasedAllocationAsync(PickList pickList, CancellationToken cancellationToken = default);
        Task<PickList> PickAllAsync(int absoluteEntry, CancellationToken cancellationToken = default);
    }
}
