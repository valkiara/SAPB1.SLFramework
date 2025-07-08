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
            var result = await ServiceLayerRepositoryBp.QueryAsync(
                filter: x => x.Valid == SAPB1.SLFramework.Enums.BoYesNoEnum.tYES && x.GroupCode == 100,
                select: x => new BusinessPartners() { CardCode = x.CardCode, CardName = x.CardName , Valid = x.Valid, GroupCode = x.GroupCode });

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task WhereAsync_CountriesCode()
        {
            // Act
            var result = await ServiceLayerRepositoryCountryCode.QueryAsync(
                filter: x => x.BankCodeDigits == 1,
                select: x => new Countries() { Code = x.Code , Name = x.Name , BankCodeDigits = x.BankCodeDigits });

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