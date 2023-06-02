using CQRSMediatrDDD.Domain.Contracts.v1;
using CQRSMediatrDDD.Domain.Core.v1;
using CQRSMediatrDDD.Infra.Repository;
using CQRSMediatrDDD.Infra.Repository.Repositories.v1;
using EasyNetQ;
using MongoDB.Driver;

namespace CQRSMediatrDDD.Consumer;

public static class Bootstrap
{
    public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoSettings = configuration.GetSection(nameof(MongoRepositorySettings));
        var clientSettings =
            MongoClientSettings.FromConnectionString(mongoSettings.Get<MongoRepositorySettings>().ConnectionString);

        services.Configure<MongoRepositorySettings>(mongoSettings);
        services.AddSingleton<IMongoClient>(new MongoClient(clientSettings));
        services.AddSingleton<IPersonRepository, PersonRepository>();
    }

    public static void AddMessageBroker(this IServiceCollection services, IConfiguration configuration)
    {
        var messageBrokerSection = configuration.GetSection(nameof(MessageBrokerSettings));
        var messageBrokerSettings = messageBrokerSection.Get<MessageBrokerSettings>();

        var mq = RabbitHutch.CreateBus(messageBrokerSettings.ConnectionString);

        services.AddSingleton(mq);
    }
}