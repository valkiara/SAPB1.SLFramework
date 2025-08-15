using B1SLayer;
using Flurl.Http;
using SAPB1.SLFramework.Abstractions.Interfaces;
using System.Collections.Concurrent;
using System.Text;

namespace SAPB1.SLFramework.ServiceLayer
{
    public sealed class SqlQueryService : ISqlQueryService
    {
        private readonly SLConnection _sl;
        private static readonly ConcurrentDictionary<string, SemaphoreSlim> _locks = new();

        public SqlQueryService(SLConnection slConnection)
        {
            _sl = slConnection ?? throw new ArgumentNullException(nameof(slConnection));
        }

        public async Task EnsureExistsAsync(string sqlCode, string sqlName, string sqlText, CancellationToken ct = default)
        {
            if (await ExistsAsync(sqlCode, ct)) return;

            var gate = _locks.GetOrAdd(sqlCode, _ => new SemaphoreSlim(1, 1));
            await gate.WaitAsync(ct);
            try
            {
                if (!await ExistsAsync(sqlCode, ct))
                    await CreateOrUpdateAsync(sqlCode, sqlName, sqlText, ct);
            }
            finally
            {
                gate.Release();
            }
        }

        public async Task CreateOrUpdateAsync(string sqlCode, string sqlName, string sqlText, CancellationToken ct = default)
        {
            var body = new { SqlCode = sqlCode, SqlName = sqlName, SqlText = sqlText };

            try
            {
                await _sl.Request("SQLQueries").PostAsync(body);
                return;
            }
            catch (FlurlHttpException ex)
            {
                var status = ex.Call?.Response?.StatusCode;
                var content = await ex.Call?.Response?.GetStringAsync()!;
                if (status != (int)System.Net.HttpStatusCode.BadRequest &&
                    status != (int)System.Net.HttpStatusCode.Conflict ||
                    !content.Contains("already exists", StringComparison.OrdinalIgnoreCase))
                    throw;
            }

            await _sl.Request($"SQLQueries('{sqlCode}')").PatchAsync(new { SqlName = sqlName, SqlText = sqlText });
        }

        public async Task<bool> ExistsAsync(string sqlCode, CancellationToken ct = default)
        {
            try
            {
                await _sl.Request($"SQLQueries('{sqlCode}')").GetStringAsync();
                return true;
            }
            catch (FlurlHttpException ex)
            {
                return ex.Call?.Response?.StatusCode != (int)System.Net.HttpStatusCode.NotFound;
            }
        }

        public async Task DeleteAsync(string sqlCode, CancellationToken ct = default)
        {
            await _sl.Request($"SQLQueries('{sqlCode}')").DeleteAsync();
        }

        public async Task<IList<T>> RunListAsync<T>(string sqlCode, IDictionary<string, object>? parameters = null, CancellationToken ct = default)
        {
            var paramList = BuildParamList(parameters);
            object data = string.IsNullOrEmpty(paramList)
                ? new { }
                : new { ParamList = paramList };

            return await _sl.Request($"SQLQueries('{sqlCode}')/List").PostAsync<IList<T>>(data);
        }

        public async Task<int?> GetIncomingPaymentTransIdAsync(int docEntry, CancellationToken ct = default)
        {
            const string Code = "IP_GetTransId";
            const string Name = "Get TransId from Incoming Payment";
            const string Sql = "SELECT \"TransId\" AS \"TransId\" FROM \"ORCT\" WHERE \"DocEntry\" = :DocEntry";

            await EnsureExistsAsync(Code, Name, Sql, ct);

            var rows = await RunListAsync<TransIdRow>(Code, new Dictionary<string, object> { ["DocEntry"] = docEntry }, ct);
            var first = rows.FirstOrDefault();
            if (first == null) return null;

            if (first.TransId.HasValue) return first.TransId.Value;
            if (int.TryParse(first.TransIdText, out var id)) return id;
            return null;
        }

        private static string? BuildParamList(IDictionary<string, object>? args)
        {
            if (args == null || args.Count == 0) return null;
            var sb = new StringBuilder();
            foreach (var kv in args)
            {
                if (sb.Length > 0) sb.Append(';');
                sb.Append(kv.Key).Append('=');
                sb.Append(kv.Value switch
                {
                    DateTime dt => dt.ToString("yyyy-MM-dd"),
                    bool b => b ? "1" : "0",
                    _ => kv.Value?.ToString()
                });
            }
            return sb.ToString();
        }

        private sealed class TransIdRow
        {
            public int? TransId { get; set; }
            public string? TransIdText { get; set; }
        }
    }
}
