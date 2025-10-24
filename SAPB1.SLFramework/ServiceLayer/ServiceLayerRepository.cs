using B1SLayer;
using SAPB1.SLFramework.Abstractions.Attributes;
using SAPB1.SLFramework.Abstractions.Interfaces;
using SAPB1.SLFramework.Abstractions.Models;
using SAPB1.SLFramework.Utilities;
using System.Linq.Expressions;
using System.Reflection;

namespace SAPB1.SLFramework.ServiceLayer
{
    /// <summary>
    /// Generic repository for CRUD, query, and session operations against SAP B1 Service Layer.
    /// </summary>
    /// <typeparam name="T">Service Layer entity type (e.g., UserTableMD, BusinessPartnerDTO).</typeparam>
    public class ServiceLayerRepository<T> : IServiceLayerRepository<T> where T : class
    {
        private readonly SLConnection _connection;
        private readonly string _resource;

        public ServiceLayerRepository(SLConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));

            var attr = typeof(T).GetCustomAttribute<ServiceLayerResourcePathAttribute>();
            _resource = attr?.ResourcePath ?? typeof(T).Name;
        }

        #region Helpers

        private SLRequest Req(string resource) => _connection.Request(resource);
        private SLRequest Req(string resource, object id) => _connection.Request(resource, id);

        private static string EnsureKeyWrapped(object id)
        {
            var s = id?.ToString() ?? throw new ArgumentNullException(nameof(id));
            return s.StartsWith("(") ? s : $"({s})";
        }

        private static string BuildOrderBy(IEnumerable<(Expression<Func<T, object>> expr, bool desc)> items)
            => ODataOrderByBuilder.ToODataOrderBy(items);

        #endregion

        #region Create

        public async Task<T> AddAsync(T entity, CancellationToken ct = default)
            => await Req(_resource).PostAsync<T>(entity, unwrapCollection: true).WaitAsync(ct);

        public async Task AddAsyncNoContent(T entity, CancellationToken ct = default)
            => await Req(_resource).PostAsync(entity).WaitAsync(ct);

#pragma warning disable SYSLIB0006
        [Obsolete("Use AddAsync instead")]
        public T Add(T entity) => AddAsync(entity).GetAwaiter().GetResult();

        [Obsolete("Use AddAsyncNoContent instead")]
        public void AddNoContent(T entity) => AddAsyncNoContent(entity).GetAwaiter().GetResult();
#pragma warning restore SYSLIB0006

        #endregion

        #region Read (single)

        public async Task<T?> GetAsync(object id, CancellationToken ct = default)
            => await Req(_resource, id).GetAsync<T>().WaitAsync(ct);

#pragma warning disable SYSLIB0006
        [Obsolete("Use GetAsync instead")]
        public T? Get(object id) => GetAsync(id).GetAwaiter().GetResult();
#pragma warning restore SYSLIB0006

        #endregion

        #region Update (PATCH)

        public void Update(object id, string entityJson)
            => UpdateAsync(id, entityJson).GetAwaiter().GetResult();

        public async Task UpdateAsync(object id, string entityJson, CancellationToken ct = default)
            => await Req(_resource, id).PatchStringAsync(entityJson).WaitAsync(ct);

        public void Update(object id, T entity)
            => UpdateAsync(id, entity).GetAwaiter().GetResult();

        public async Task UpdateAsync(object id, T entity, CancellationToken ct = default)
            => await UpdateObjectAsync(id, entity, ct);

        private async Task UpdateObjectAsync(object id, T entity, CancellationToken ct)
        {
            if (typeof(T) == typeof(UserFieldsMD))
            {
                var key = EnsureKeyWrapped(id);
                await Req($"{_resource}{key}").PatchAsync(entity).WaitAsync(ct);
            }
            else
            {
                await Req(_resource, id).PatchAsync(entity).WaitAsync(ct);
            }
        }

        #endregion

        #region Delete / Cancel

        public void Delete(object id) => DeleteAsync(id).GetAwaiter().GetResult();

        public async Task DeleteAsync(object id, CancellationToken ct = default)
            => await Req(_resource, id).DeleteAsync().WaitAsync(ct);

        public void Cancel(object id) => CancelAsync(id).GetAwaiter().GetResult();

        /// <summary>
        /// Cancels a marketing document via POST {Resource}({key})/Cancel.
        /// </summary>
        public async Task CancelAsync(object id, CancellationToken ct = default)
        {
            var key = EnsureKeyWrapped(id); // e.g., "(6)"
            await Req($"{_resource}{key}/Cancel").PostAsync().WaitAsync(ct);
        }


        #endregion

        #region Simple existence / first / single

        /// <summary>
        /// Lighter existence check (no $count) – asks for a single minimal field with $top=1.
        /// </summary>
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> filter, CancellationToken ct = default)
        {
            var filterStr = ODataFilterBuilder.ToODataFilter(filter);
            var res = await Req(_resource)
                .SetQueryParam("$filter", filterStr)
                .SetQueryParam("$select", ODataSelectBuilder.MinimalKeySelect<T>())
                .SetQueryParam("$top", "1")
                .GetAsync<ODataResult<List<T>>>(unwrapCollection: false)
                .WaitAsync(ct);

            return (res.Value?.Count ?? 0) > 0;
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter, CancellationToken ct = default)
        {
            var filterStr = ODataFilterBuilder.ToODataFilter(filter);
            var result = await Req(_resource)
                .SetQueryParam("$filter", filterStr)
                .SetQueryParam("$top", "1")
                .GetAsync<ODataResult<List<T>>>(false)
                .WaitAsync(ct);

            return result.Value?.FirstOrDefault();
        }

        public T? FirstOrDefault(Expression<Func<T, bool>> filter)
            => FirstOrDefaultAsync(filter).GetAwaiter().GetResult();

        public async Task<T> FirstAsync(Expression<Func<T, bool>> filter, CancellationToken ct = default)
            => await FirstOrDefaultAsync(filter, ct) ?? throw new InvalidOperationException("No elements match the filter.");

        public T First(Expression<Func<T, bool>> filter)
            => FirstAsync(filter).GetAwaiter().GetResult();

        public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> filter, CancellationToken ct = default)
        {
            var filterStr = ODataFilterBuilder.ToODataFilter(filter);
            var result = await Req(_resource)
                .SetQueryParam("$filter", filterStr)
                .SetQueryParam("$top", "2")
                .GetAsync<ODataResult<List<T>>>(false)
                .WaitAsync(ct);

            var list = result.Value;
            if (list == null || list.Count == 0) return null;
            if (list.Count > 1) throw new InvalidOperationException("More than one element matches the filter.");
            return list[0];
        }

        public T? SingleOrDefault(Expression<Func<T, bool>> filter)
            => SingleOrDefaultAsync(filter).GetAwaiter().GetResult();

        public async Task<T> SingleAsync(Expression<Func<T, bool>> filter, CancellationToken ct = default)
            => await SingleOrDefaultAsync(filter, ct) ?? throw new InvalidOperationException("No elements match the filter.");

        public T Single(Expression<Func<T, bool>> filter)
            => SingleAsync(filter).GetAwaiter().GetResult();

        #endregion

        #region Query – basic (one page)

        /// <summary>
        /// Select specific fields.
        /// </summary>
        public async Task<IEnumerable<T>> SelectAsync(Expression<Func<T, T>> selector, CancellationToken ct = default)
        {
            var selectFields = ODataSelectBuilder.ToODataSelect(selector);
            var result = await Req(_resource)
                .SetQueryParam("$select", selectFields)
                .GetAsync<ODataResult<List<T>>>(false)
                .WaitAsync(ct);

            return result.Value ?? new List<T>();
        }

        /// <summary>
        /// Query with filter / select / order / paging (returns one page).
        /// </summary>
        public async Task<IEnumerable<T>> QueryAsync(
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, T>>? select = null,
            IEnumerable<(Expression<Func<T, object>> expr, bool desc)>? orderBy = null,
            int? page = null,
            int? pageSize = null,
            CancellationToken ct = default)
        {
            var request = Req(_resource);

            if (filter is not null)
                request = request.SetQueryParam("$filter", ODataFilterBuilder.ToODataFilter(filter));

            if (select is not null)
                request = request.SetQueryParam("$select", ODataSelectBuilder.ToODataSelect(select));

            if (orderBy is not null && orderBy.Any())
                request = request.SetQueryParam("$orderby", BuildOrderBy(orderBy));

            if (page.HasValue && pageSize.HasValue)
            {
                request = request.SetQueryParam("$top", pageSize.Value.ToString());
                request = request.SetQueryParam("$skip", ((page.Value - 1) * pageSize.Value).ToString());
            }
            else if (pageSize.HasValue)
            {
                request = request.SetQueryParam("$top", pageSize.Value.ToString());
            }

            var res = await request.GetAsync<ODataResult<List<T>>>(false).WaitAsync(ct);
            return res.Value ?? new List<T>();
        }

        /// <summary>
        /// Convenience overload for a single order by.
        /// </summary>
        public Task<IEnumerable<T>> QueryAsync(
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, T>>? select = null,
            Expression<Func<T, object>>? orderBy = null,
            bool desc = false,
            int? page = null,
            int? pageSize = null,
            CancellationToken ct = default)
        {
            var tuple = orderBy is null ? null : new[] { (orderBy, desc) };
            return QueryAsync(filter, select, tuple, page, pageSize, ct);
        }

        /// <summary>
        /// Raw query string (keeps @odata.count and @odata.nextLink if present).
        /// </summary>
        public async Task<ODataResult<IEnumerable<T>>> QueryAsync(string rawQuery, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(rawQuery))
                throw new ArgumentException("Query string must not be null or empty.", nameof(rawQuery));

            return await Req($"{_resource}?{rawQuery}")
                .GetAsync<ODataResult<IEnumerable<T>>>(false)
                .WaitAsync(ct);
        }

        public ODataResult<IEnumerable<T>> Query(string rawQuery, CancellationToken ct = default)
            => QueryAsync(rawQuery, ct).GetAwaiter().GetResult();



        public Task<ODataResult<IEnumerable<T>>> QueryAsync(IDictionary<string, string>? query = null, CancellationToken ct = default)
        {
            var request = Req(_resource);

            if (query != null)
            {
                foreach (var item in query)
                {
                    request.SetQueryParam(item.Key, item.Value);
                }
            }
            return request.GetAsync<ODataResult<IEnumerable<T>>>(false);
        }

        public ODataResult<IEnumerable<T>> Query(IDictionary<string, string>? query = null, CancellationToken ct = default)
            => QueryAsync(query, ct).GetAwaiter().GetResult();
        #endregion

        #region Query – paging helpers

        /// <summary>
        /// Returns a single page with Count and NextLink preserved.
        /// </summary>
        public async Task<ODataResult<List<T>>> QueryPageAsync(
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, T>>? select = null,
            IEnumerable<(Expression<Func<T, object>> expr, bool desc)>? orderBy = null,
            int? top = null,
            int? skip = null,
            CancellationToken ct = default)
        {
            var req = Req(_resource).SetQueryParam("$count", "true");

            if (filter is not null)
                req = req.SetQueryParam("$filter", ODataFilterBuilder.ToODataFilter(filter));

            if (select is not null)
                req = req.SetQueryParam("$select", ODataSelectBuilder.ToODataSelect(select));

            if (orderBy?.Any() == true)
                req = req.SetQueryParam("$orderby", BuildOrderBy(orderBy));

            if (top is not null) req = req.SetQueryParam("$top", top.Value.ToString());
            if (skip is not null) req = req.SetQueryParam("$skip", skip.Value.ToString());

            return await req.GetAsync<ODataResult<List<T>>>(false).WaitAsync(ct);
        }

        /// <summary>
        /// Follows an @odata.nextLink returned by Service Layer (absolute or relative).
        /// </summary>
        public async Task<ODataResult<List<T>>> NextPageAsync(string nextLink, CancellationToken ct = default)
        {
            var qIndex = nextLink.IndexOf('?');
            if (qIndex < 0)
                throw new ArgumentException("Invalid @odata.nextLink (no query part found).", nameof(nextLink));

            var rawQuery = nextLink[(qIndex + 1)..]; // everything after '?'
            return await Req($"{_resource}?{rawQuery}").GetAsync<ODataResult<List<T>>>(false).WaitAsync(ct);
        }

        /// <summary>
        /// Streams all pages using @odata.nextLink. Server-driven paging friendly.
        /// </summary>
        public async IAsyncEnumerable<T> QueryAllAsync(
        Expression<Func<T, bool>>? filter = null,
        Expression<Func<T, T>>? select = null,
        IEnumerable<(Expression<Func<T, object>> expr, bool desc)>? orderBy = null,
        int pageSize = 200,
        [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken ct = default)
        {
            var skip = 0;

            while (true)
            {
                ct.ThrowIfCancellationRequested();

                var page = await QueryPageAsync(filter, select, orderBy, top: pageSize, skip: skip, ct);
                if (page.Value == null || page.Value.Count == 0)
                    yield break;

                foreach (var item in page.Value)
                    yield return item;

                // If we got fewer than pageSize, we're done
                if (page.Value.Count < pageSize)
                    yield break;

                // Otherwise, advance
                skip += page.Value.Count; // or: skip += pageSize;
            }
        }


        #endregion

        #region Count

        /// <summary>
        /// Returns count using $count endpoint or inline count when needed.
        /// </summary>
        public async Task<long> CountAsync(Expression<Func<T, bool>>? filter = null, CancellationToken ct = default)
        {
            if (filter is null)
            {
                // Use "{resource}/$count" and SLRequest.GetStringAsync()
                var s = await Req($"{_resource}/$count").GetStringAsync().WaitAsync(ct);
                return long.TryParse(s, out var n) ? n : 0;
            }

            // Filtered count via inline $count=true (keeps compatibility with SL quirks)
            var filterStr = ODataFilterBuilder.ToODataFilter(filter);
            var res = await Req(_resource)
                .SetQueryParam("$filter", filterStr)   // this is an SLRequestExtensions method
                .SetQueryParam("$count", "true")
                .GetAsync<ODataResult<List<T>>>(false)
                .WaitAsync(ct);

            return res.Count ?? 0;
        }


        #endregion

        #region Raw GET helpers

        public async Task<string> GetRawStringAsync(object id, CancellationToken ct = default)
            => await Req(_resource, id).GetStringAsync().WaitAsync(ct);

        public async Task<byte[]> GetRawBytesAsync(object id, CancellationToken ct = default)
            => await Req(_resource, id).GetBytesAsync().WaitAsync(ct);

        public async Task<Stream> GetRawStreamAsync(object id, CancellationToken ct = default)
            => await Req(_resource, id).GetStreamAsync().WaitAsync(ct);

        #endregion

        #region Raw POST/PATCH/PUT helpers

        public async Task<TOut> PostAsync<TOut>(object data, CancellationToken ct = default)
            => await Req(_resource).PostAsync<TOut>(data, unwrapCollection: true).WaitAsync(ct);

        public async Task<TOut> PostStringAsync<TOut>(string json, CancellationToken ct = default)
            => await Req(_resource).PostStringAsync<TOut>(json, unwrapCollection: true).WaitAsync(ct);

        public async Task PostAsync(object data, CancellationToken ct = default)
            => await Req(_resource).PostAsync(data).WaitAsync(ct);

        public async Task<string> PostReceiveStringAsync(CancellationToken ct = default)
            => await Req(_resource).PostReceiveStringAsync().WaitAsync(ct);

        public async Task PostStringAsync(string json, CancellationToken ct = default)
            => await Req(_resource).PostStringAsync(json).WaitAsync(ct);

        public async Task PatchStringAsync(object id, string json, CancellationToken ct = default)
            => await Req(_resource, id).PatchStringAsync(json).WaitAsync(ct);

        public async Task PutAsync(object id, object data, CancellationToken ct = default)
            => await Req(_resource, id).PutAsync(data).WaitAsync(ct);

        public async Task PutStringAsync(object id, string json, CancellationToken ct = default)
            => await Req(_resource, id).PutStringAsync(json).WaitAsync(ct);

        #endregion

        #region File (PATCH multipart)

        /// <summary>
        /// Patch with file (e.g., attachments) to the resource instance.
        /// </summary>
        public async Task PatchWithFileAsync(object id, string fileName, byte[] bytes, CancellationToken ct = default)
            => await Req(_resource, id).PatchWithFileAsync(fileName, bytes).WaitAsync(ct);

        public async Task PatchWithFileAsync(object id, string fileName, Stream stream, CancellationToken ct = default)
            => await Req(_resource, id).PatchWithFileAsync(fileName, stream).WaitAsync(ct);

        public async Task PatchWithFileAsync(object id, string path, CancellationToken ct = default)
            => await Req(_resource, id).PatchWithFileAsync(path).WaitAsync(ct);

        #endregion

        #region Session

        public Task LoginAsync() => _connection.LoginAsync();

        #endregion
    }
}
