using B1SLayer;
using SAPB1.SLFramework.Abstractions.Attributes;
using SAPB1.SLFramework.Abstractions.Interfaces;
using SAPB1.SLFramework.Abstractions.Models;
using SAPB1.SLFramework.Extensions;
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

            // Use [ServiceLayerResourcePath] if available, fallback to typeof(T).Name
            var attr = typeof(T).GetCustomAttribute<ServiceLayerResourcePathAttribute>();
            _resource = attr?.ResourcePath ?? typeof(T).Name;
        }

        public async Task<T> AddAsync(T entity)
        {
            return await _connection.Request(_resource).PostAsync<T>(entity);
        }

        public T Add(T entity)
            => AddAsync(entity).GetAwaiter().GetResult();

        public async Task<T?> GetAsync(object id)
        {
            return await _connection.Request(_resource, id)
                                     .GetAsync<T>();
        }

        public T? Get(object id)
            => GetAsync(id).GetAwaiter().GetResult();

        public async Task<ODataResult<IEnumerable<T>>> GetAllAsync(IDictionary<string, string>? query = null)
        {
            var req = _connection.Request(_resource);
            if (query != null && query.Any())
                req = req.SetQueryParams(query);    

            return await req.GetAsync<ODataResult<IEnumerable<T>>>();
        }

        public ODataResult<IEnumerable<T>> GetAll(IDictionary<string, string>? query = null)
            => GetAllAsync(query).GetAwaiter().GetResult();

        public async Task<ODataResult<IEnumerable<T>>> QueryAsync(string filter)
        {
            return await _connection.Request(_resource)
                                     .SetQueryParam("$filter", filter)
                                     .GetAsync<ODataResult<IEnumerable<T>>>(false);
        }

        public ODataResult<IEnumerable<T>> Query(string filter)
            => QueryAsync(filter).GetAwaiter().GetResult();

        public async Task<long> GetCountAsync(IDictionary<string, string> query)
        {
            var result = await GetAllAsync(query);
            return (long)result.Count!;
        }

        public long GetCount(IDictionary<string, string> query)
            => GetCountAsync(query).GetAwaiter().GetResult();

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
        public async Task<bool> ExistsAsync(string odataFilter)
        {
            // 1) Build a request to: /b1s/v1/<Resource>?$filter=<odataFilter>&$top=1
            //    We only need “any” result, so $top=1 is enough.
            var response = await _connection
                                  .Request(_resource)                      // e.g. "UserTablesMD"
                                  .SetQueryParam("$filter", odataFilter)   // e.g. "TableName eq 'MY_TABLE'"
                                  .SetQueryParam("$top", "1")
                                  .SetQueryParam("$count", "true")
                                  .GetAsync<ODataResult<List<T>>>(false)
                                  .ConfigureAwait(false);

            // 2) If any value is returned, then “Exists” = true
            return response.Count > 0;
        }


        public async Task<T?> FirstOrDefaultAsync(string filter)
        {
            var result = await _connection.Request(_resource)
                                          .SetQueryParam("$filter", filter)
                                          .SetQueryParam("$top", "1")
                                          .GetAsync<ODataResult<List<T>>>(false)
                                          .ConfigureAwait(false);

            return result.Value?.FirstOrDefault();
        }

        public T? FirstOrDefault(string filter)
            => FirstOrDefaultAsync(filter).GetAwaiter().GetResult();


        public async Task<T> FirstAsync(string filter)
        {
            var entity = await FirstOrDefaultAsync(filter);
            return entity ?? throw new InvalidOperationException($"No elements match the filter: {filter}");
        }

        public T First(string filter)
            => FirstAsync(filter).GetAwaiter().GetResult();


        public async Task<T?> SingleOrDefaultAsync(string filter)
        {
            var result = await _connection.Request(_resource)
                                          .SetQueryParam("$filter", filter)
                                          .SetQueryParam("$top", "2") // to check for more than one
                                          .GetAsync<ODataResult<List<T>>>(false)
                                          .ConfigureAwait(false);

            var list = result.Value;
            if (list == null || list.Count == 0)
                return null;

            if (list.Count > 1)
                throw new InvalidOperationException($"More than one element matches the filter: {filter}");

            return list.First();
        }

        public T? SingleOrDefault(string filter)
            => SingleOrDefaultAsync(filter).GetAwaiter().GetResult();


        public async Task<T> SingleAsync(string filter)
        {
            var entity = await SingleOrDefaultAsync(filter);
            return entity ?? throw new InvalidOperationException($"No elements match the filter: {filter}");
        }

        public T Single(string filter)
            => SingleAsync(filter).GetAwaiter().GetResult();



    }
}