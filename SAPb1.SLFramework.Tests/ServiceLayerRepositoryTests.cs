using B1SLayer;
using SAPB1.SLFramework.Abstractions.Interfaces;
using SAPB1.SLFramework.Abstractions.Models;
using SAPB1.SLFramework.ServiceLayer;
using SAPB1.SLFramework.Utilities;
using System.Linq.Expressions;

namespace SAPb1.SLFramework.Tests
{
    public class ServiceLayerRepositoryTests
    {
        public IServiceLayerRepository<BusinessPartners> ServiceLayerRepository { get; set; }

        public ServiceLayerRepositoryTests()
        {
            ServiceLayerRepository = new ServiceLayerRepository<BusinessPartners>(
                new SLConnection("https://10.132.10.103:50000/b1s/v2/", "BATUMI_RIVIERA_TEST", "manager", "Aa123456!"));
        }

        [Fact]
        public async Task WhereAsync_ShouldReturnFilteredResults()
        {
            // Act
            var result = await ServiceLayerRepository.QueryAsync(
                filter: x => x.CardCode == "BPS0253",
                select: x => new BusinessPartners() { CardCode = x.CardCode, CardName = x.CardName });

            // Assert
            Assert.NotNull(result);
        }
    }
}