using B1SLayer;
using SAPB1.SLFramework.Abstractions.Interfaces;
using SAPB1.SLFramework.Abstractions.Models;
using SAPB1.SLFramework.ServiceLayer;
using System.Text.Json;

namespace SAPb1.SLFramework.Tests
{
    public class ServiceLayerRepositoryTests
    {
        public IServiceLayerRepository<BusinessPartners> ServiceLayerRepositoryBp { get; set; }
        public IServiceLayerRepository<Countries> ServiceLayerRepositoryCountryCode { get; set; }
        public IServiceLayerRepository<Orders> OrdersRepository { get; set; }
        public ICompanyInfoService CompanyInfoService { get; set; }
        public IServiceLayerQueryService ServiceLayerQueryService { get; set; }


        public ServiceLayerRepositoryTests()
        {
            var slConn = new SLConnection("https://srv-pl4:50000/b1s/v2/", "SalesDB", "beka", "1234");
           
            
            //var slConn = new SLConnection("https://10.132.10.103:50000/b1s/v2/", "BATUMI_RIVIERA_TEST", "manager", "Aa123456!");


            ServiceLayerRepositoryBp = new ServiceLayerRepository<BusinessPartners>(slConn);
            ServiceLayerRepositoryCountryCode = new ServiceLayerRepository<Countries>(slConn);
            OrdersRepository = new ServiceLayerRepository<Orders>(slConn);

            CompanyInfoService = new CompanyInfoService(slConn);

            ServiceLayerQueryService = new ServiceLayerQueryService(slConn);
        }

        [Fact]
        public async Task FirstOrDefaultAsync_ShouldReturnSingleResult()
        {
            // Act
            var result = await OrdersRepository.FirstOrDefaultAsync(x => x.Cancelled == SAPB1.SLFramework.Enums.BoYesNoEnum.tNO);
            // Assert
            Assert.NotNull(result);
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


        [Fact]
        public async Task GetQueryAsync_ShouldReturnFilteredResults()
        {
            // Act
            var result = await ServiceLayerQueryService.PostQueryAsync(
     "$crossjoin(PurchaseDeliveryNotes, PurchaseDeliveryNotes/DocumentLines)",
     "$expand=PurchaseDeliveryNotes($select=DocEntry),PurchaseDeliveryNotes/DocumentLines($select=BaseEntry,BaseType)" +
     "&$filter=PurchaseDeliveryNotes/DocEntry eq PurchaseDeliveryNotes/DocumentLines/DocEntry and PurchaseDeliveryNotes/DocumentLines/BaseEntry eq 91991 and PurchaseDeliveryNotes/DocumentLines/BaseType eq 22"
 );
            // Assert
            Assert.NotNull(result);

            var docEntries = ExtractDocEntriesFromCrossJoin(result);

            foreach (var docEntry in docEntries)
            {
                Console.WriteLine($"Found PDN DocEntry: {docEntry}");
            }
        }


        [Fact]
        public async Task OrderRepoTest()
        {
            var result = await OrdersRepository.FirstOrDefaultAsync(x => x.DocumentStatus == SAPB1.SLFramework.Enums.BoStatus.bost_Close || x.Cancelled == SAPB1.SLFramework.Enums.BoYesNoEnum.tYES);


            Assert.NotNull(result);
        }

        public static List<int> ExtractDocEntriesFromCrossJoin(string json)
        {
            var result = new List<int>();

            using var doc = JsonDocument.Parse(json);
            if (!doc.RootElement.TryGetProperty("value", out var valueArray) || valueArray.ValueKind != JsonValueKind.Array)
                return result;

            foreach (var item in valueArray.EnumerateArray())
            {
                if (item.TryGetProperty("PurchaseDeliveryNotes", out var pdnElement) &&
                    pdnElement.TryGetProperty("DocEntry", out var docEntryElement) &&
                    docEntryElement.TryGetInt32(out var docEntry))
                {
                    result.Add(docEntry);
                }
            }

            return result;
        }
    }
}