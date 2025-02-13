using Azure;
using Azure.Data.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureTableStorageService.Services
{
    public class TableStorageService2 : ITableStorageService
    {
        //private const string TableName = "Referrals";
        private readonly string _connectionString;

        public TableStorageService(string connectionString)
        {
            _connectionString = connectionString;
        }

        private async Task<TableClient> GetTableClient(string tableName)
        {
            var serviceClient = new TableServiceClient(_connectionString);
            var tableClient = serviceClient.GetTableClient(tableName);
            await tableClient.CreateIfNotExistsAsync();
            return tableClient;
        }

        public async Task<IEnumerable<ITableEntity>> GetAllEntitiesAsync(string tableName)
        {
            var tableClient = await GetTableClient(tableName);
            Pageable<ITableEntity> x = tableClient.Query<ITableEntity>();

            var referralList = new List<ITableEntity>();
            foreach(var entity in x)
            {
                referralList.Add(entity);
            }

            return referralList;
        }

        public async Task<ITableEntity> GetEntityAsync(string tableName, string category, string id)
        {
            var tableClient = await GetTableClient();
            return await tableClient.GetEntityAsync<ITableEntity>(category, id);
        }

        public async Task<ITableEntity> UpsertEntityAsync(ITableEntity entity)
        {
            var tableClient = await GetTableClient();
            await tableClient.UpsertEntityAsync(entity);
            return entity;
        }

        public async Task DeleteEntityAsync(string category, string id)
        {
            var tableClient = await GetTableClient();
            await tableClient.DeleteEntityAsync(category, id);
        }
    }
}
