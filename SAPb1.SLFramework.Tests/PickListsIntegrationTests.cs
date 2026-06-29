using B1SLayer;
using SAPB1.SLFramework.Abstractions.Interfaces;
using SAPB1.SLFramework.Abstractions.Models;
using SAPB1.SLFramework.ServiceLayer;

namespace SAPb1.SLFramework.Tests
{
    public class PickListsIntegrationTests
    {
        private readonly IServiceLayerRepository<PickList> _pickListsRepository;
        private readonly IPickListsService _pickListsService;

        public PickListsIntegrationTests()
        {
            var slConn = new SLConnection(
                GetSetting("SL_URL", "https://srv-pl4:50000/b1s/v2/"),
                GetSetting("SL_COMPANY_DB", "SBODemoUS"),
                GetSetting("SL_USERNAME", "beka"),
                GetSetting("SL_PASSWORD", "1234"));

            _pickListsRepository = new ServiceLayerRepository<PickList>(slConn);
            _pickListsService = new PickListsService(slConn);
        }

        [Fact(DisplayName = "PickLists: get released allocation by AbsoluteEntry")]
        [Trait("Category", "Integration")]
        public async Task GetReleasedAllocation_ByAbsoluteEntry_ReturnsPickList()
        {
            var absoluteEntry = GetRequiredIntSetting("SL_PICKLIST_ABSOLUTE_ENTRY");

            var pickList = await _pickListsService.GetReleasedAllocationAsync(absoluteEntry);

            Assert.NotNull(pickList);
            Assert.Equal(absoluteEntry, pickList.Absoluteentry);
            Assert.NotNull(pickList.PickListsLines);
        }

        [Fact(DisplayName = "PickLists: repository can fetch PickList by AbsoluteEntry")]
        [Trait("Category", "Integration")]
        public async Task Repository_GetByAbsoluteEntry_ReturnsPickList()
        {
            var absoluteEntry = GetRequiredIntSetting("SL_PICKLIST_ABSOLUTE_ENTRY");

            var pickList = await _pickListsRepository.GetAsync(absoluteEntry);

            Assert.NotNull(pickList);
            Assert.Equal(absoluteEntry, pickList.Absoluteentry);
        }

        [Fact(DisplayName = "PickLists: pick all released allocation lines")]
        [Trait("Category", "Integration")]
        [Trait("Operation", "Mutating")]
        public async Task PickAll_UpdateReleasedAllocation_SetsPickedQuantityToReleasedQuantity()
        {
            var absoluteEntry = GetRequiredIntSetting("SL_PICKLIST_ABSOLUTE_ENTRY");

            var pickList = await _pickListsService.PickAllAsync(absoluteEntry);

            Assert.NotNull(pickList);
            Assert.Equal(absoluteEntry, pickList.Absoluteentry);
            Assert.NotEmpty(pickList.PickListsLines);
            Assert.All(
                pickList.PickListsLines.Where(line => line.ReleasedQuantity.HasValue),
                line => Assert.Equal(line.ReleasedQuantity, line.PickedQuantity));
        }

        private static string GetSetting(string name, string fallback)
            => Environment.GetEnvironmentVariable(name) ?? fallback;

        private static int GetRequiredIntSetting(string name)
        {
            var value = Environment.GetEnvironmentVariable(name);
            return int.TryParse(value, out var number) ? number : 3;
        }
    }
}
