using B1SLayer;
using SAPB1.SLFramework.Abstractions.Attributes;
using SAPB1.SLFramework.Abstractions.Interfaces;
using SAPB1.SLFramework.Abstractions.Models;
using SAPB1.SLFramework.Extensions;
using SAPB1.SLFramework.Utilities;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;

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

            // Use [ServiceLayerResourcePath] if available, fallback to typeof(T).Name
            var attr = typeof(T).GetCustomAttribute<ServiceLayerResourcePathAttribute>();
            _resource = attr?.ResourcePath ?? typeof(T).Name;
        }

        public async Task<T> AddAsync(T entity)
        {
            return await _connection.Request(_resource).PostAsync<T>(entity);
        }

        public async Task AddAsyncNoContent(T entity)
        {
            await _connection.Request(_resource).PostAsync(entity);
        }


        public T Add(T entity)
            => AddAsync(entity).GetAwaiter().GetResult();

        public void AddNoContent(T entity)
            => AddAsyncNoContent(entity).GetAwaiter().GetResult();


        public async Task<T?> GetAsync(object id)
        {
            return await _connection.Request(_resource, id)
                                     .GetAsync<T>();
        }

        public T? Get(object id)
            => GetAsync(id).GetAwaiter().GetResult();

        public void Update(object id, string entityJson)
            => UpdateAsync(id, entityJson).GetAwaiter().GetResult();

        public async Task UpdateAsync(object id, string entityJson)
        {
            await _connection.Request(_resource, id)
                             .PatchStringAsync(entityJson);
        }

        /// <summary>
        /// Update by passing a CLR object; it will be serialized to JSON under the covers.
        /// </summary>
        public void Update(object id, T entity)
        {
            // Option A: let Flurl handle serialization
            UpdateObjectAsync(id, entity).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Update by passing a CLR object; it will be serialized to JSON under the covers.
        /// </summary>
        public async Task UpdateAsync(object id, T entity)
        {
            await UpdateObjectAsync(id, entity);
        }

        /// <summary>
        /// Core object-based update; calls Flurl PatchJsonAsync(T).
        /// </summary>
        private async Task UpdateObjectAsync(object id, T entity)
        {
            // Flurl will serialize 'entity' with System.Text.Json
            if (typeof(T) == typeof(UserFieldsMD))
            {
                // id should be "TableName='UDT01',FieldID=0"
                // We build a full resource: "UserFieldsMD(TableName='UDT01',FieldID=0')"
                string fullResource = $"{_resource}({id})";
                await _connection
                      .Request(fullResource)     // exactly UserFieldsMD(TableName='UDT01',FieldID=0)
                      .PatchAsync(entity);
            }
            else
            {
                await _connection.Request(_resource, id)
                             .PatchAsync(entity);
            }
        }

        public void Delete(object id)
           => DeleteAsync(id).GetAwaiter().GetResult();

        public async Task DeleteAsync(object id)
        {
            await _connection.Request(_resource, id)
                             .DeleteAsync();
        }

        public void Cancel(object id)
            => CancelAsync(id).GetAwaiter().GetResult();

        public async Task CancelAsync(object id)
        {
            await _connection.Request(_resource, id)
                             .DeleteAsync();
        }

        public Task LoginAsync()
            => _connection.LoginAsync();

        /// <summary>
        /// Returns true if any record of type T satisfies the given OData filter.
        /// </summary>
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> filter)
        {
            var filterStr = ODataFilterBuilder.ToODataFilter(filter);

            var response = await _connection
                                 .Request(_resource)
                                 .SetQueryParam("$filter", filterStr)
                                 .SetQueryParam("$top", "1")
                                 .SetQueryParam("$count", "true")
                                 .GetAsync<ODataResult<List<T>>>(false)
                                 .ConfigureAwait(false);

            return response.Count > 0;
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter)
        {
            var filterStr = ODataFilterBuilder.ToODataFilter(filter);
            var result = await _connection.Request(_resource)
                                          .SetQueryParam("$filter", filterStr)
                                          .SetQueryParam("$top", "1")
                                          .GetAsync<ODataResult<List<T>>>(false)
                                          .ConfigureAwait(false);

            return result.Value?.FirstOrDefault();
        }

        public T? FirstOrDefault(Expression<Func<T, bool>> filter)
            => FirstOrDefaultAsync(filter).GetAwaiter().GetResult();

        public async Task<T> FirstAsync(Expression<Func<T, bool>> filter)
        {
            var entity = await FirstOrDefaultAsync(filter);
            return entity ?? throw new InvalidOperationException($"No elements match the filter.");
        }

        public T First(Expression<Func<T, bool>> filter)
            => FirstAsync(filter).GetAwaiter().GetResult();

        public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> filter)
        {
            var filterStr = ODataFilterBuilder.ToODataFilter(filter);
            var result = await _connection.Request(_resource)
                                          .SetQueryParam("$filter", filterStr)
                                          .SetQueryParam("$top", "2")
                                          .GetAsync<ODataResult<List<T>>>(false)
                                          .ConfigureAwait(false);

            var list = result.Value;
            if (list == null || list.Count == 0)
                return null;

            if (list.Count > 1)
                throw new InvalidOperationException($"More than one element matches the filter.");

            return list.First();
        }

        public T? SingleOrDefault(Expression<Func<T, bool>> filter)
            => SingleOrDefaultAsync(filter).GetAwaiter().GetResult();

        public async Task<T> SingleAsync(Expression<Func<T, bool>> filter)
        {
            var entity = await SingleOrDefaultAsync(filter);
            return entity ?? throw new InvalidOperationException($"No elements match the filter.");
        }

        public T Single(Expression<Func<T, bool>> filter)
            => SingleAsync(filter).GetAwaiter().GetResult();


        public async Task<IEnumerable<T>> SelectAsync(Expression<Func<T, T>> selector)
        {
            var selectFields = ODataSelectBuilder.ToODataSelect(selector);

            var result = await _connection.Request(_resource)
                                          .SetQueryParam("$select", selectFields)
                                          .GetAsync<ODataResult<IEnumerable<T>>>(false);

            return result.Value ?? Enumerable.Empty<T>();
        }

        public async Task<IEnumerable<T>> QueryAsync(
         Expression<Func<T, bool>>? filter = null,
         Expression<Func<T, T>>? select = null,
         IEnumerable<(Expression<Func<T, object>> expr, bool desc)>? orderBy = null,
         int? page = null,
         int? pageSize = null)
            {
            var request = _connection.Request(_resource);

            if (filter is not null)
            {
                var filterStr = ODataFilterBuilder.ToODataFilter(filter);
                request = request.SetQueryParam("$filter", filterStr);
            }

            if (select is not null)
            {
                var selectStr = ODataSelectBuilder.ToODataSelect(select);
                request = request.SetQueryParam("$select", selectStr);
            }

            if (orderBy is not null && orderBy.Any())
            {
                var orderByStr = ODataOrderByBuilder.ToODataOrderBy(orderBy);
                request = request.SetQueryParam("$orderby", orderByStr);
            }

            if (page.HasValue && pageSize.HasValue)
            {
                request = request.SetQueryParam("$top", pageSize.Value.ToString());
                request = request.SetQueryParam("$skip", ((page.Value - 1) * pageSize.Value).ToString());
            }
            else if (pageSize.HasValue)
            {
                request = request.SetQueryParam("$top", pageSize.Value.ToString());
            }

            return await request.GetAsync<IEnumerable<T>>();
        }


        public async Task<ODataResult<IEnumerable<T>>> QueryAsync(string rawQuery)
        {
            if (string.IsNullOrWhiteSpace(rawQuery))
                throw new ArgumentException("Query string must not be null or empty.", nameof(rawQuery));

            var request = _connection.Request($"{_resource}?{rawQuery}");
            return await request.GetAsync<ODataResult<IEnumerable<T>>>(false);
        }

        public ODataResult<IEnumerable<T>> Query(string rawQuery)
        {
            return QueryAsync(rawQuery).GetAwaiter().GetResult();
        }


        public async Task<long> CountAsync(Expression<Func<T, bool>>? filter = null)
        {
            var request = _connection.Request(_resource).SetQueryParam("$count", "true");

            if (filter is not null)
            {
                var filterStr = ODataFilterBuilder.ToODataFilter(filter);
                request = request.SetQueryParam("$filter", filterStr);
            }

            var result = await request.GetAsync<ODataResult<IEnumerable<T>>>(false);
            return result.Count ?? 0;
        }
    }
}