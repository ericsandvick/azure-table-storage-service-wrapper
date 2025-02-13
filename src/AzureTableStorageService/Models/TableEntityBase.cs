using Azure;
using Azure.Data.Tables;
using System;

namespace AzureTableStorageService.Models
{
    public abstract class TableEntityBase : ITableEntity
    {
        public TableEntityBase()
        {
            RowKey = Guid.NewGuid().ToString().ToUpper();
        }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
