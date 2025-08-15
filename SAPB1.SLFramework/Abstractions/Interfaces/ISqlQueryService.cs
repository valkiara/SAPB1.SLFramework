namespace SAPB1.SLFramework.Abstractions.Interfaces
{
    public interface ISqlQueryService
    {
        Task EnsureExistsAsync(string sqlCode, string sqlName, string sqlText, CancellationToken ct = default);
        Task CreateOrUpdateAsync(string sqlCode, string sqlName, string sqlText, CancellationToken ct = default);
        Task<bool> ExistsAsync(string sqlCode, CancellationToken ct = default);
        Task DeleteAsync(string sqlCode, CancellationToken ct = default);

        /// <summary>Runs SQLQueries('<code>')/List and deserializes value[] to T.</summary>
        Task<IList<T>> RunListAsync<T>(string sqlCode, IDictionary<string, object>? parameters = null, CancellationToken ct = default);

        /// <summary>Convenience: returns ORCT.TransId by Incoming Payment DocEntry (or null if not found).</summary>
        Task<int?> GetIncomingPaymentTransIdAsync(int docEntry, CancellationToken ct = default);
    }
}
