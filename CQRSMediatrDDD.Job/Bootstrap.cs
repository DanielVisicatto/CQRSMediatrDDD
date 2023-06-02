using CQRSMediatrDDD.Domain.Contracts.v1;
using CQRSMediatrDDD.Infra.Cache;
using CQRSMediatrDDD.Infra.Cache.Repositories.v1;
using CQRSMediatrDDD.Infra.Repository;
using CQRSMediatrDDD.Infra.Repository.Repositories.v1;
using MongoDB.Driver;

namespace CQRSMediatrDDD.Job;

public static class Bootstrap
{
    public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoSettings = configuration.GetSection(nameof(MongoRepositorySettings));
        var clientSettings = MongoClientSettings.FromConnectionString(mongoSettings.Get < MongoRepositorySettings>().ConnectionString);

        services.Configure<MongoRepositorySettings>(mongoSettings);
        services.AddSingleton<IMongoClient>(new MongoClient(clientSettings));
        services.AddSingleton<IPersonRepository, PersonRepository>();
    }

    public static void AddCache (this IServiceCollection services, IConfiguration configuration)
    {
        var cacheSection = configuration.GetSection(nameof(CacheRepositorySettings));
        var cacheSettings = cacheSection.Get<CacheRepositorySettings>();

        services.AddStackExchangeRedisCache(options => { options.Configuration = cacheSettings.ConnectionString; });
        services.AddSingleton(cacheSettings);
        services.AddSingleton(typeof(ICacheRepository<>), typeof(CacheRepository<>));
    }
}
