using Azure.Data.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureTableStorageService.Services
{
    public class AzureTableStorageService<T> : IAzureTableStorageService<T> where T : class, ITableEntity, new()
    {
        private readonly TableClient _tableClient;

        public AzureTableStorageService(string connectionString, string tableName) : this(new TableServiceClient(connectionString), tableName) { }

        public AzureTableStorageService(TableServiceClient tableServiceClient, string tableName)
        {
            _tableClient = tableServiceClient.GetTableClient(tableName);
        }

        /// <summary>
        /// Deletes an entity from the table storage.
        /// </summary>
        /// <param name="partitionKey"></param>
        /// <param name="rowKey"></param>
        /// <returns></returns>
        public async Task DeleteEntityAsync(string partitionKey, string rowKey)
        {
            await _tableClient.DeleteEntityAsync(partitionKey, rowKey);
        }

        /// <summary>
        /// Gets an entity from the table storage.
        /// </summary>
        /// <param name="partitionKey"></param>
        /// <param name="rowKey"></param>
        /// <returns></returns>
        public async Task<T> GetEntityAsync(string partitionKey, string rowKey)
        {
            return await _tableClient.GetEntityAsync<T>(partitionKey, rowKey);
        }

        /// <summary>
        /// Gets all entities from the table storage with an optional filter.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<T> GetEntities(string filter = null)
        {
            return _tableClient.Query<T>(filter);
        }

        /// <summary>
        /// Adds or updates an entity in the table storage.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> UpsertEntityAsync(T entity)
        {
            await _tableClient.UpsertEntityAsync(entity);
            return entity;
        }
    }
}
