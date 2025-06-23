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
        // Create
        Task<T> AddAsync(T entity);
        T Add(T entity);

        // Read single
        Task<T?> GetAsync(object id);
        T? Get(object id);


        // Update
        void Update(object id, string entityJson);
        Task UpdateAsync(object id, string entityJson);

        // Delete or cancel operation
        void Cancel(object id);
        Task CancelAsync(object id);

        // Session management
        Task LoginAsync();
        /// <summary>
        /// Returns true if any T matches the given OData filter expression.
        /// For example:
        ///   For UDTs:     filter = $"TableName eq '{tableName}'"
        ///   For UDFs:     filter = $"TableName eq '{tableName}' and Name eq '{fieldName}'"
        ///   For BPs:      filter = $"CardCode eq '{cardCode}'"
        ///   For Orders:   filter = $"DocEntry eq {docEntry}"
        ///   Or any custom OData filter you need.
        /// </summary>
        public Task<bool> ExistsAsync(Expression<Func<T, bool>> filter);
        void Update(object id, T entity);
        Task UpdateAsync(object id, T entity);
        Task<IEnumerable<T>> SelectAsync(Expression<Func<T, T>> selector);
        public Task<IEnumerable<T>> QueryAsync(
         Expression<Func<T, bool>>? filter = null,
         Expression<Func<T, T>>? select = null,
         IEnumerable<(Expression<Func<T, object>> expr, bool desc)>? orderBy = null,
         int? page = null,
         int? pageSize = null);

        Task<long> CountAsync(Expression<Func<T, bool>>? filter = null);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter);
        T? FirstOrDefault(Expression<Func<T, bool>> filter);
        Task<T> FirstAsync(Expression<Func<T, bool>> filter);
        T First(Expression<Func<T, bool>> filter);
        Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> filter);
        T? SingleOrDefault(Expression<Func<T, bool>> filter);
        Task<T> SingleAsync(Expression<Func<T, bool>> filter);
        T Single(Expression<Func<T, bool>> filter);
    }
}
