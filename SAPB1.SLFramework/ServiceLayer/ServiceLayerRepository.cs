﻿using B1SLayer;
using SAPB1.SLFramework.Abstractions.Interfaces;
using SAPB1.SLFramework.Abstractions.Models;
using SAPB1.SLFramework.Extensions;

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
            _resource = typeof(T).Name;
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
                                     .GetAsync<ODataResult<IEnumerable<T>>>();
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
            await _connection.LoginAsync();
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
            await _connection.Request(_resource, id)
                             .PatchAsync(entity);
        }

        public void Cancel(object id)
            => CancelAsync(id).GetAwaiter().GetResult();

        public async Task CancelAsync(object id)
        {
            await _connection.LoginAsync();
            await _connection.Request(_resource, id)
                             .DeleteAsync();
        }

        public Task LoginAsync()
            => _connection.LoginAsync();

        public Task<bool> ExistsAsync(string tableName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}