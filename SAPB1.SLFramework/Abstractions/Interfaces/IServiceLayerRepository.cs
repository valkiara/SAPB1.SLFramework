using SAPB1.SLFramework.Abstractions.Models;
using System.Linq.Expressions;

namespace SAPB1.SLFramework.Abstractions.Interfaces
{
    /// <summary>
    /// Provides full CRUD and query operations against the SAP B1 Service Layer.
    /// </summary>
    /// <typeparam name="T">Type of Service Layer entity (e.g., BusinessPartners, Orders, UDT/UDF models).</typeparam>
    public interface IServiceLayerRepository<T> where T : class
    {
        // Session
        Task LoginAsync();

        // Create
        Task<T> AddAsync(T entity, CancellationToken ct = default);
        Task AddAsyncNoContent(T entity, CancellationToken ct = default);

        [System.Obsolete("Use AddAsync instead")]
        T Add(T entity);

        [System.Obsolete("Use AddAsyncNoContent instead")]
        void AddNoContent(T entity);

        // Read (single)
        Task<T?> GetAsync(object id, CancellationToken ct = default);

        [System.Obsolete("Use GetAsync instead")]
        T? Get(object id);

        // Update (PATCH)
        void Update(object id, string entityJson);
        Task UpdateAsync(object id, string entityJson, CancellationToken ct = default);

        void Update(object id, T entity);
        Task UpdateAsync(object id, T entity, CancellationToken ct = default);

        // Delete / Cancel
        [System.Obsolete("Use DeleteAsync instead")]
        void Delete(object id);

        Task DeleteAsync(object id, CancellationToken ct = default);

        [System.Obsolete("Use CancelAsync instead")]
        void Cancel(object id);

        /// <summary>POST {Resource}({key})/Cancel for marketing documents.</summary>
        Task CancelAsync(object id, CancellationToken ct = default);

        // Existence / Count
        /// <summary>
        /// Returns true if any T matches the given OData filter expression.
        /// </summary>
        Task<bool> ExistsAsync(Expression<Func<T, bool>> filter, CancellationToken ct = default);

        Task<long> CountAsync(Expression<Func<T, bool>>? filter = null, CancellationToken ct = default);

        // First/Single
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter, CancellationToken ct = default);
        [System.Obsolete("Use FirstOrDefaultAsync instead")]
        T? FirstOrDefault(Expression<Func<T, bool>> filter);

        Task<T> FirstAsync(Expression<Func<T, bool>> filter, CancellationToken ct = default);
        [System.Obsolete("Use FirstAsync instead")]
        T First(Expression<Func<T, bool>> filter);

        Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> filter, CancellationToken ct = default);
        [System.Obsolete("Use SingleOrDefaultAsync instead")]
        T? SingleOrDefault(Expression<Func<T, bool>> filter);

        Task<T> SingleAsync(Expression<Func<T, bool>> filter, CancellationToken ct = default);
        [System.Obsolete("Use SingleAsync instead")]
        T Single(Expression<Func<T, bool>> filter);

        // Select (projection)
        Task<IEnumerable<T>> SelectAsync(Expression<Func<T, T>> selector, CancellationToken ct = default);

        // Query – one-page (flexible)
        Task<IEnumerable<T>> QueryAsync(
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, T>>? select = null,
            IEnumerable<(Expression<Func<T, object>> expr, bool desc)>? orderBy = null,
            int? page = null,
            int? pageSize = null,
            CancellationToken ct = default);

        // Query – convenience overload for single order-by
        Task<IEnumerable<T>> QueryAsync(
            Expression<Func<T, bool>>? filter,
            Expression<Func<T, T>>? select,
            Expression<Func<T, object>>? orderBy,
            bool desc = false,
            int? page = null,
            int? pageSize = null,
            CancellationToken ct = default);

        // Raw OData query (preserves @odata.count/@odata.nextLink)
        Task<ODataResult<IEnumerable<T>>> QueryAsync(string rawQuery, CancellationToken ct = default);

        [System.Obsolete("Use QueryAsync(rawQuery, ct) instead")]
        ODataResult<IEnumerable<T>> Query(string rawQuery);

        // Paging helpers
        Task<ODataResult<List<T>>> QueryPageAsync(
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, T>>? select = null,
            IEnumerable<(Expression<Func<T, object>> expr, bool desc)>? orderBy = null,
            int? top = null,
            int? skip = null,
            CancellationToken ct = default);

        Task<ODataResult<List<T>>> NextPageAsync(string nextLink, CancellationToken ct = default);

        IAsyncEnumerable<T> QueryAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, T>>? select = null,
            IEnumerable<(Expression<Func<T, object>> expr, bool desc)>? orderBy = null,
            int pageSize = 20,
            CancellationToken ct = default);

        // Raw GET helpers
        Task<string> GetRawStringAsync(object id, CancellationToken ct = default);
        Task<byte[]> GetRawBytesAsync(object id, CancellationToken ct = default);
        Task<Stream> GetRawStreamAsync(object id, CancellationToken ct = default);

        // Raw POST/PATCH/PUT helpers
        Task<TOut> PostAsync<TOut>(object data, CancellationToken ct = default);
        Task<TOut> PostStringAsync<TOut>(string json, CancellationToken ct = default);
        Task PostAsync(object data, CancellationToken ct = default);
        Task<string> PostReceiveStringAsync(CancellationToken ct = default);
        Task PostStringAsync(string json, CancellationToken ct = default);

        Task PatchStringAsync(object id, string json, CancellationToken ct = default);

        Task PutAsync(object id, object data, CancellationToken ct = default);
        Task PutStringAsync(object id, string json, CancellationToken ct = default);

        // File (PATCH multipart)
        Task PatchWithFileAsync(object id, string fileName, byte[] bytes, CancellationToken ct = default);
        Task PatchWithFileAsync(object id, string fileName, Stream stream, CancellationToken ct = default);
        Task PatchWithFileAsync(object id, string path, CancellationToken ct = default);
    }
}
