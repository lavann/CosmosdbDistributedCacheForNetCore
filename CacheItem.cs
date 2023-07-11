namespace CosmosDbDistributedCache
{
    // This class represents a cache item in the Cosmos DB distributed cache.
    public class CacheItem
    {
        // The Id property represents the unique identifier for the cache item.
        // This is used as the key to retrieve the cache item from the Cosmos DB.
        public string Id { get; set; }

        // The Value property represents the actual data stored in the cache.
        // This is stored as a byte array, which allows for storing complex objects after serialization.
        public byte[] Value { get; set; }
    }
}
