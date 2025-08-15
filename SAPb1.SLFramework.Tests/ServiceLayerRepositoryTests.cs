using B1SLayer;
using Newtonsoft.Json;
using SAPB1.SLFramework.Abstractions.Interfaces;
using SAPB1.SLFramework.Abstractions.Models;
using SAPB1.SLFramework.ServiceLayer;
using System.Runtime;
using System.Runtime.Intrinsics.Arm;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SAPb1.SLFramework.Tests
{
    public class ServiceLayerRepositoryTests
    {
        public IServiceLayerRepository<BusinessPartners> ServiceLayerRepositoryBp { get; set; }
        public IServiceLayerRepository<Countries> ServiceLayerRepositoryCountryCode { get; set; }
        public IServiceLayerRepository<Orders> OrdersRepository { get; set; }
        public ICompanyInfoService CompanyInfoService { get; set; }
        public IServiceLayerQueryService ServiceLayerQueryService { get; set; }
        public ISBOBobService SBOBobService { get; set; }
        public IServiceLayerRepository<DownPayments> DownPayments { get; set; }
        public IServiceLayerRepository<RSM_BDPM> DPMAccounts { get; set; }
        public IServiceLayerRepository<JournalEntries> JournalEntries { get; set; }
        public IServiceLayerRepository<ChartOfAccounts> ChartofAccounts { get; set; }
        public ICashFlowLineItemsService CashFlowLineItemsService { get; set; }


        public ServiceLayerRepositoryTests()
        {
            //var slConn = new SLConnection("https://srv-pl4:50000/b1s/v2/", "SalesDB", "beka", "1234");


            var slConn = new SLConnection("https://10.132.10.103:50000/b1s/v2/", "BATUMI_RIVIERA_TEST", "manager", "Aa123456!");


            ServiceLayerRepositoryBp = new ServiceLayerRepository<BusinessPartners>(slConn);
            ServiceLayerRepositoryCountryCode = new ServiceLayerRepository<Countries>(slConn);
            OrdersRepository = new ServiceLayerRepository<Orders>(slConn);
            DownPayments = new ServiceLayerRepository<DownPayments>(slConn);

            CompanyInfoService = new CompanyInfoService(slConn);

            ServiceLayerQueryService = new ServiceLayerQueryService(slConn);
            DPMAccounts = new ServiceLayerRepository<RSM_BDPM>(slConn);
            CashFlowLineItemsService = new CashFlowLineItemsService(slConn);
            SBOBobService = new SBOBobService(slConn);
            JournalEntries = new ServiceLayerRepository<JournalEntries>(slConn);
            ChartofAccounts = new ServiceLayerRepository<ChartOfAccounts>(slConn);
        }


        [Fact]
        public async Task PostDownPaymentTest()
        {
            // Arrange
            var downPayment = new DownPayments
            {
                DocCurrency = "GEL",
                CardCode = "175526",
                DocDate = DateTime.Now,
                DownPaymentType = SAPB1.SLFramework.Enums.DownPaymentTypeEnum.dptRequest,
                DocTotal = 1000,
                DocDueDate = DateTime.Now.AddDays(30),
                DocumentLines = new List<DocumentLine>
                {
                    new DocumentLine
                    {
                        ItemCode = "4167",
                        Quantity = 10,
                        BaseType = (int)SAPB1.SLFramework.Enums.BoAPARDocumentTypes.bodt_Order,
                        BaseEntry = 299, // Replace with a valid order DocEntry
                        BaseLine = 0, // Replace with a valid order line number
                        PriceAfterVAT = 100,
                        VatGroup = "VAT1",
                    }
                }
            };

            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters =
                {
                    new JsonStringEnumConverter()           // ← emit enum names
                }
            };

            var json = System.Text.Json.JsonSerializer.Serialize(downPayment, options);

            // Act
            var result = await DownPayments.AddAsync(downPayment);
            // Assert
            Assert.NotNull(result);
            Assert.Equal("C00001", result.CardCode);
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
                select: x => new BusinessPartners() { CardCode = x.CardCode, CardName = x.CardName, Valid = x.Valid, GroupCode = x.GroupCode });

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task WhereAsync_CountriesCode()
        {
            // Act
            var result = await ServiceLayerRepositoryCountryCode.QueryAsync(
                filter: x => x.BankCodeDigits == 1,
                select: x => new Countries() { Code = x.Code, Name = x.Name, BankCodeDigits = x.BankCodeDigits });

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

        [Fact]
        public async Task SBOBobService_GetCurrencyRate_Test()
        {
            // Arrange
            string currency = "EUR";
            DateTime date = new DateTime(2025, 1, 1);
            // Act
            var result = await SBOBobService.GetCurrencyRateAsync(currency, date);
            // Assert
            Assert.NotNull(result);
        }


        [Fact]
        public void FirstOrDefaultAsync_ShouldReturnObject_When_Enum_IsCondition()
        {
            var result = DPMAccounts.FirstOrDefaultAsync(x => x.U_Currency == "USD" && x.U_Status == AreaTypeEnum.Residential).Result;

            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldReturnAllSalesOrders()
        {
            var list = new List<Orders>();

            await foreach (var o in OrdersRepository.QueryAllAsync(
                               x => x.DocumentStatus == SAPB1.SLFramework.Enums.BoStatus.bost_Open))
            {
                list.Add(o);
            }

            Assert.NotEmpty(list);
        }

        [Fact]
        public async Task CashFlowLineItemsService_ShouldReturnList()
        {
            // Act
            var result = await CashFlowLineItemsService.ListAsync();
            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            foreach (var item in result)
            {
                Console.WriteLine($"LineItemID: {item.LineItemID}, LineItemName: {item.LineItemName}");
            }
        }

        [Fact]
        public async Task ShouldReturnJournalEntries()
        {
            var aa = await JournalEntries.GetAsync(23778);

        }



        [Fact]
        public async Task ShouldReturnChartOfAccounts()
        {
            var list = new List<ChartOfAccounts>();

            await foreach (var o in ChartofAccounts.QueryAllAsync())
            {
                list.Add(o);
            }

            Assert.NotEmpty(list);
        }
    }
}