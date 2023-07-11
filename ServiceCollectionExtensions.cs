using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace CosmosDbDistributedCache
{
    public static class ServiceCollectionExtensions
    {
        // This method is an extension method for IServiceCollection.
        // It allows us to add our CosmosDbDistributedCache as a service in a fluent manner.
        public static IServiceCollection AddCosmosDbDistributedCache(this IServiceCollection services, Action<CosmosDbDistributedCacheOptions> setupAction)
        {
            // We create a new instance of our options class.
            var options = new CosmosDbDistributedCacheOptions();

            // We call the provided setupAction with our options instance.
            // This allows the caller to set the options for the CosmosDbDistributedCache.
            setupAction(options);

            // We add a singleton service for IDistributedCache.
            // The actual implementation is our CosmosDbDistributedCache.
            // We use the options set by the caller to create the CosmosDbDistributedCache.
            services.AddSingleton<IDistributedCache>(new CosmosDbDistributedCache(
                options.ConnectionString,
                options.DatabaseName,
                options.TableName,
                options.PartitionKeyName,
                options.TTLAttributeName));

            // We return the IServiceCollection so that additional calls can be chained.
            return services;
        }
    }
}
