using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureTableStorageService.Services
{
    public class AzureTableStorageService<T> : IAzureTableStorageService<T> where T : class, ITableEntity, new()
    {
        private readonly TableServiceClient _tableServiceClient;
        private readonly TableClient _tableClient;

        public AzureTableStorageService(string connectionString, string tableName) : this(new TableServiceClient(connectionString), tableName) { }

        public AzureTableStorageService(TableServiceClient tableServiceClient, string tableName)
        {
            _tableServiceClient = tableServiceClient;
            _tableClient = tableServiceClient.GetTableClient(tableName);
        }

        /// <summary>
        /// Creates the target table if it does not exist.
        /// </summary>
        /// <returns></returns>
        public Response<TableItem> CreateTable()
        {
            return _tableServiceClient.CreateTableIfNotExists(_tableClient.Name);
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
        public List<T> GetEntities(string filter = null)
        {
            var pageable = _tableClient.Query<T>(filter);

            var entityList = new List<T>();
            foreach (var entity in pageable)
            {
                entityList.Add(entity);
            }

            return entityList;
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
