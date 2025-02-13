using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureTableStorageService.Services
{
    public interface IAzureTableStorageService<T>
    {
        Task DeleteEntityAsync(string partitionKey, string rowKey);
        Task<T> GetEntityAsync(string partitionKey, string rowKey);
        IEnumerable<T> GetEntities(string filter = null);
        Task<T> UpsertEntityAsync(T entity);
    }
}
