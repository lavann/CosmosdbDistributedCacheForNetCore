namespace CosmosDbDistributedCache;

public class CosmosDbDistributedCacheOptions
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

}
