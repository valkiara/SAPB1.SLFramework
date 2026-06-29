using B1SLayer;
using SAPB1.SLFramework.Abstractions.Interfaces;
using SAPB1.SLFramework.Abstractions.Models;

namespace SAPB1.SLFramework.ServiceLayer
{
    public class PickListsService(SLConnection connection) : IPickListsService
    {
        private readonly SLConnection _connection = connection ?? throw new ArgumentNullException(nameof(connection));

        public async Task<PickList> GetReleasedAllocationAsync(int absoluteEntry, CancellationToken cancellationToken = default)
        {
            return await _connection
                .Request($"PickLists({absoluteEntry})/SAPB1.GetReleasedAllocation()")
                .GetAsync<PickList>(unwrapCollection: false)
                .WaitAsync(cancellationToken);
        }

        public async Task CloseAsync(PickList pickList, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(pickList);

            await _connection
                .Request("PickListsService_Close")
                .PostAsync(new { PickList = pickList })
                .WaitAsync(cancellationToken);
        }

        public async Task UpdateReleasedAllocationAsync(PickList pickList, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(pickList);

            await _connection
                .Request("PickListsService_UpdateReleasedAllocation")
                .PostAsync(new { PickList = pickList })
                .WaitAsync(cancellationToken);
        }

        public async Task<PickList> PickAllAsync(int absoluteEntry, CancellationToken cancellationToken = default)
        {
            var pickList = await GetReleasedAllocationAsync(absoluteEntry, cancellationToken);

            foreach (var line in pickList.PickListsLines)
            {
                line.PickedQuantity = line.ReleasedQuantity;
            }

            await UpdateReleasedAllocationAsync(pickList, cancellationToken);
            return pickList;
        }
    }
}
