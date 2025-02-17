using Azure.Data.Tables.Models;
using Azure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureTableStorageService.Services
{
    public interface IAzureTableStorageService<T>
    {
        Response<TableItem> CreateTable();
        Task DeleteEntityAsync(string partitionKey, string rowKey);
        Task<T> GetEntityAsync(string partitionKey, string rowKey);
        List<T> GetEntities(string filter = null);
        Task<T> UpsertEntityAsync(T entity);
    }
}
