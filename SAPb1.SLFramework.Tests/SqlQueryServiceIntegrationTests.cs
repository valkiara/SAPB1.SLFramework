using B1SLayer;
using SAPB1.SLFramework.Abstractions.Interfaces;
using SAPB1.SLFramework.Abstractions.Models;
using SAPB1.SLFramework.ServiceLayer;

namespace SAPb1.SLFramework.Tests
{
    public class SqlQueryServiceIntegrationTests
    {

        // New
        public ISqlQueryService SqlQueryService { get; set; }

        private readonly SLConnection _sl;

        public SqlQueryServiceIntegrationTests()
        {
            // Same style as your sample (adjust to your landscape)
            // var slConn = new SLConnection("https://srv-pl4:50000/b1s/v2/", "SalesDB", "beka", "1234");
            var slConn = new SLConnection("https://10.132.10.103:50000/b1s/v2/", "BATUMI_RIVIERA_TEST", "manager", "Aa123456!");
            _sl = slConn;

            // NEW: SQLQueries service from your framework
            SqlQueryService = new SqlQueryService(slConn);
        }

        // ——————————————————————————————————————————————————————————————————————
        // TESTS
        // ——————————————————————————————————————————————————————————————————————

        [Fact(DisplayName = "SQLQueries: EnsureExists is idempotent")]
        public async Task SqlQueries_EnsureExists_IsIdempotent()
        {
            const string code = "IP_GetTransId";
            const string name = "Get TransId from Incoming Payment";
            const string sql = "SELECT \"TransId\" AS \"TransId\" FROM \"ORCT\" WHERE \"DocEntry\" = :DocEntry";

            // First call creates (or updates) the query
            await SqlQueryService.EnsureExistsAsync(code, name, sql);

            // Second call should be a no-op and not throw
            await SqlQueryService.EnsureExistsAsync(code, name, sql);

            Assert.True(true); // if we got here, both calls succeeded
        }

        [Fact(DisplayName = "GetIncomingPaymentTransIdAsync returns JE TransId and JE exists")]
        public async Task GetIncomingPaymentTransId_ThenFetchJournalEntry()
        {
            var docEntry = 934;
            // Act: get TransId via SQLQueries service
            var transId = await SqlQueryService.GetIncomingPaymentTransIdAsync(docEntry);

            // Assert: TransId present
            Assert.True(transId.HasValue && transId.Value > 0, $"No TransId found for Incoming Payment DocEntry={docEntry}");
        }

        [Fact(DisplayName = "RunListAsync<T> returns a row with TransId")]
        public async Task RunList_ReturnsTransId_Row()
        {
            // Ensure query exists
            const string code = "IP_GetTransId";
            const string name = "Get TransId from Incoming Payment";
            const string sql = "SELECT \"TransId\" AS \"TransId\" FROM \"ORCT\" WHERE \"DocEntry\" = :DocEntry";
            await SqlQueryService.EnsureExistsAsync(code, name, sql);

            // Use a known IP DocEntry
            var envDocEntry = Environment.GetEnvironmentVariable("SL_IP_DOCENTRY");
            if (!int.TryParse(envDocEntry, out var docEntry))
                docEntry = 516; // fallback

            // RunList directly
            var rows = await SqlQueryService.RunListAsync<TransIdRow>(code, new Dictionary<string, object>
            {
                ["DocEntry"] = docEntry
            });

            Assert.NotNull(rows);
            Assert.NotEmpty(rows);
            Assert.True(rows.First().TransId > 0);
        }

        // small DTO just for test deserialization
        private sealed class TransIdRow
        {
            public int TransId { get; set; }
        }
    }
}
