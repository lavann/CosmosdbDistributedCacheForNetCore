# Cosmos DB Distributed Cache for ASP.NET Core

This project provides an implementation of `IDistributedCache` that uses Azure Cosmos DB as the backing store. It allows you to use Cosmos DB as a distributed cache in your ASP.NET Core applications.

## Getting Started

To use this distributed cache implementation, you'll need to add the `CosmosDbDistributedCache` service in your application's startup configuration.

Here's an example of how to do this in a .NET 6 application:

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCosmosDbDistributedCache(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("CosmosDb");
    options.DatabaseName = "YourDatabaseName";
    options.TableName = "weather_cache";
    options.PartitionKeyName = "id";
    options.TTLAttributeName = "cache_ttl";
});

// Other builder configuration...

var app = builder.Build();

// Configure the HTTP request pipeline.

// Other app configuration...

app.Run();
 