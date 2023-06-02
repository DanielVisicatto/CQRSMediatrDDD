using CQRSMediatrDDD.Domain.Commands.v1.CreatePerson;
using CQRSMediatrDDD.Domain.Commands.v1.DeletePerson;
using CQRSMediatrDDD.Domain.Commands.v1.UpdatePerson;
using CQRSMediatrDDD.Domain.Contracts.v1;
using CQRSMediatrDDD.Domain.Core.v1;
using CQRSMediatrDDD.Domain.Queries.v1.GetPerson;
using CQRSMediatrDDD.Domain.Queries.v1.ListPerson;
using CQRSMediatrDDD.Infra.Cache;
using CQRSMediatrDDD.Infra.Cache.Repositories.v1;
using CQRSMediatrDDD.Infra.Repository;
using CQRSMediatrDDD.Infra.Repository.Repositories.v1;
using EasyNetQ;
using FluentValidation;
using FluentValidation.AspNetCore;
using MongoDB.Driver;

namespace CQRSMediatrDDD.API;

public static class Bootstrap
{
    public static IServiceCollection AddInjections(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositories(configuration);
        services.AddMappers();
        services.AddCache(configuration);
        services.AddCommands();
        services.AddQueries();
        services.AddValidators();
        services.AddMessageBroker(configuration);
        services.AddNotificationContext();

        return services;
    }

    private static void AddCommands(this IServiceCollection services)
    {
        services.AddScoped<CreatePersonCommandHandler>();
        services.AddScoped<UpdatePersonCommandHandler>();
        services.AddScoped<DeletePersonCommandHandler>();
    }

    private static void AddQueries(this IServiceCollection services)
    {
        services.AddScoped<ListPersonQueryHandler>();
        services.AddScoped<GetPersonQueryHandler>();
    }

    private static void AddValidators(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();

        services.AddScoped<IValidator<CreatePersonCommand>, CreatePersonCommandValidator>();
        services.AddScoped<IValidator<UpdatePersonCommand>, UpdatePersonCommandValidator>();
    }

    private static void AddMappers(this IServiceCollection services)
    {
        services.AddAutoMapper(
            typeof(CreatePersonCommandProfile),
            typeof(UpdatePersonCommandProfile),
            typeof(GetPersonQueryProfile),
            typeof(ListPersonQueryProfile)
            );
    }

    private static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoSettings = configuration.GetSection(nameof(MongoRepositorySettings));
        var clientSettings = MongoClientSettings.FromConnectionString(mongoSettings.Get<MongoRepositorySettings>().ConnectionString);

        services.Configure<MongoRepositorySettings>(mongoSettings);
        services.AddSingleton<IMongoClient>(new MongoClient(clientSettings));
        services.AddSingleton<IPersonRepository, PersonRepository>();
    }

    private static void AddCache(this IServiceCollection services, IConfiguration configuration)
    {
        var cacheSection = configuration.GetSection(nameof(CacheRepositorySettings));
        var cacheSettings = cacheSection.Get<CacheRepositorySettings>();

        services.AddStackExchangeRedisCache(options => { options.Configuration = cacheSettings.ConnectionString; });
        services.AddSingleton(cacheSettings);
        services.AddScoped(typeof(ICacheRepository<>), typeof(CacheRepository<>));
    }

    private static void AddNotificationContext(this IServiceCollection services)
    {
        services.AddScoped<INotificationContext, NotificationContext>();
    }

    private static void AddMessageBroker(this IServiceCollection services, IConfiguration configuration)
    {
        var messageBrokerSection = configuration.GetSection(nameof(MessageBrokerSettings));
        var messageBrokerSettings = messageBrokerSection.Get<MessageBrokerSettings>();

        var mq = RabbitHutch.CreateBus(messageBrokerSettings.ConnectionString);

        services.AddSingleton(mq);
    }
}
