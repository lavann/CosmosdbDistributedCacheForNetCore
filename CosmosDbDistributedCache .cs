using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Caching.Distributed;

namespace CosmosDbDistributedCache
{
    // This class implements the IDistributedCache interface and uses Azure Cosmos DB as the cache store.
    public class CosmosDbDistributedCache : IDistributedCache
    {
        // Connection string for your Cosmos DB account.
        public string ConnectionString { get; }

        // Name of the Cosmos DB database that you'll be using for caching.
        public string DatabaseName { get; }

        // Name of the Cosmos DB container (also known as a table) that you'll be using for caching.
        public string TableName { get; }

        // Name of the attribute that you'll use as the partition key in your Cosmos DB container.
        public string PartitionKeyName { get; }

        // Name of the attribute that you'll use for the Time-to-Live (TTL) feature in Cosmos DB.
        public string TTLAttributeName { get; }

        // CosmosClient is the client object that interacts with Cosmos DB.
        private readonly CosmosClient _cosmosClient;

        // Container is the Cosmos DB container (table) where the cache items are stored.
        private readonly Container _container;

        // Constructor for the CosmosDbDistributedCache class.
        public CosmosDbDistributedCache(string connectionString, string databaseName, string tableName, string partitionKeyName, string tTLAttributeName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
            TableName = tableName;
            PartitionKeyName = partitionKeyName;
            TTLAttributeName = tTLAttributeName;

            // Initialize the CosmosClient and Container objects.
            _cosmosClient = new CosmosClient(ConnectionString);
            var database = _cosmosClient.GetDatabase(DatabaseName);
            _container = database.GetContainer(TableName);
        }

        // Get a cache item by its key.
        public byte[]? Get(string key)
        {
            var response = _container.ReadItemAsync<CacheItem>(key, new PartitionKey(key)).GetAwaiter().GetResult();
            return response.Resource.Value;
        }

        // Asynchronously get a cache item by its key.
        public async Task<byte[]?> GetAsync(string key, CancellationToken token = default)
        {
            var response = await _container.ReadItemAsync<CacheItem>(key, new PartitionKey(key), cancellationToken: token);
            return response.Resource.Value;
        }

        // Refresh a cache item by its key. This method is not applicable in this context as Cosmos DB does not have a built-in sliding expiration mechanism.
        public void Refresh(string key)
        {
            // Refreshing is not applicable in Cosmos DB in this context as there's no built-in sliding expiration mechanism.
        }

        // Asynchronously refresh a cache item by its key. This method is not applicable in this context as Cosmos DB does not have a built-in sliding expiration mechanism.
        public Task RefreshAsync(string key, CancellationToken token = default)
        {
            // Refreshing is not applicable in Cosmos DB in this context as there's no built-in sliding expiration mechanism.
            return Task.CompletedTask;
        }

        // Remove a cache item by its key.
        public void Remove(string key)
        {
            _container.DeleteItemAsync<CacheItem>(key, new PartitionKey(key)).GetAwaiter().GetResult();
        }

        // Asynchronously remove a cache item by its key.
        public async Task RemoveAsync(string key, CancellationToken token = default)
        {
            await _container.DeleteItemAsync<CacheItem>(key, new PartitionKey(key), cancellationToken: token);
        }

        // Set a cache item by its key.
        public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            var item = new CacheItem { Id = key, Value = value };
            _container.UpsertItemAsync(item, new PartitionKey(key)).GetAwaiter().GetResult();
        }

        // Asynchronously set a cache item by its key.
        public async Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default)
        {
            var item = new CacheItem { Id = key, Value = value };
            await _container.UpsertItemAsync(item, new PartitionKey(key), cancellationToken: token);
        }
    }
}
