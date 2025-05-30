﻿using SAPB1.SLFramework.Abstractions.Models;

namespace SAPB1.SLFramework.Abstractions.Interfaces
{
    /// <summary>
    /// Provides full CRUD and query operations against the SAP B1 Service Layer.
    /// </summary>
    /// <typeparam name="T">Type of Service Layer entity (e.g., BusinessPartners, Orders, UDT/UDF models).</typeparam>
    public interface IServiceLayerRepository<T> where T : class
    {
        // Create
        Task<T> AddAsync(T entity);
        T Add(T entity);

        // Read single
        Task<T?> GetAsync(object id);
        T? Get(object id);

        // Read multiple
        Task<ODataResult<IEnumerable<T>>> GetAllAsync(IDictionary<string, string>? query = null);
        ODataResult<IEnumerable<T>> GetAll(IDictionary<string, string>? query = null);

        // Query with custom OData filter
        Task<ODataResult<IEnumerable<T>>> QueryAsync(string query = "");
        ODataResult<IEnumerable<T>> Query(string query = "");

        // Count entities matching filter
        Task<long> GetCountAsync(IDictionary<string, string> query);
        long GetCount(IDictionary<string, string> query);

        // Update
        void Update(object id, string entityJson);
        Task UpdateAsync(object id, string entityJson);

        // Delete or cancel operation
        void Cancel(object id);
        Task CancelAsync(object id);

        // Session management
        Task LoginAsync();
        Task<bool> ExistsAsync(string tableName, CancellationToken cancellationToken);
        void Update(object id, T entity);
        Task UpdateAsync(object id, T entity);
    }
}
