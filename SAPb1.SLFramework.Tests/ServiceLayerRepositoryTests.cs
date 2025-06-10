using B1SLayer;
using SAPB1.SLFramework.Abstractions.Interfaces;
using SAPB1.SLFramework.Abstractions.Models;
using SAPB1.SLFramework.ServiceLayer;

namespace SAPb1.SLFramework.Tests
{
    public class ServiceLayerRepositoryTests
    {
        public IServiceLayerRepository<UserTablesMD> ServiceLayerRepository { get; set; }

        public ServiceLayerRepositoryTests()
        {
            ServiceLayerRepository = new ServiceLayerRepository<UserTablesMD>(
                new SLConnection("https://localhost:50000/b1s/v2/", "Test", "manager", "Aa123456!"));
        }

        [Fact]
        public async void Test1()
        {
            var result = await ServiceLayerRepository.ExistsAsync("TableName eq 'RSM_BTXV'");
            Assert.True(result);
        }
    }
}