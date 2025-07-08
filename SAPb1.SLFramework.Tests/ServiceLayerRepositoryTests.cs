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
        public ICompanyInfoService CompanyInfoService { get; set; }


        public ServiceLayerRepositoryTests()
        {
            //var slConn = new SLConnection("https://10.132.10.103:50000/b1s/v2/", "BATUMI_RIVIERA_TEST", "manager", "Aa123456!");

            var slConn = new SLConnection("https://srv-pl4:50000/b1s/v2/", "SalesDB", "beka", "1234");

            ServiceLayerRepository = new ServiceLayerRepository<BusinessPartners>(slConn);

            CompanyInfoService = new CompanyInfoService(slConn);
        }

        [Fact]
        public async Task WhereAsync_ShouldReturnFilteredResults()
        {
            // Act
            var result = await ServiceLayerRepository.QueryAsync(
                filter: x => x.CardType == SAPB1.SLFramework.Enums.BoCardTypes.cCustomer,
                select: x => new BusinessPartners() { CardCode = x.CardCode, CardName = x.CardName });

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllResults()
        {
            // Act
            var result = await CompanyInfoService.GetAsync();
            // Assert
            Assert.NotNull(result);
        }
    }
}