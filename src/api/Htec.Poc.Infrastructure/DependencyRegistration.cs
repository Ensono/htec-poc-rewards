﻿using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Application.CQRS.Commands;
using Amido.Stacks.Application.CQRS.Queries;
using Amido.Stacks.Configuration.Extensions;
using Amido.Stacks.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Htec.Poc.Application.CommandHandlers;
using Htec.Poc.Application.Integration;
using Htec.Poc.Application.QueryHandlers;
using Htec.Poc.Infrastructure.Rules;
using Amido.Stacks.Messaging.Azure.ServiceBus.Configuration;
using Amido.Stacks.Messaging.Azure.ServiceBus.Senders.Publishers;

namespace Htec.Poc.Infrastructure;

public static class DependencyRegistration
{
    static readonly ILogger log = Log.Logger;

    /// <summary>
    /// Register static services that does not change between environment or contexts(i.e: tests)
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureStaticDependencies(IServiceCollection services)
    {
        AddCommandHandlers(services);
        AddQueryHandlers(services);
    }

    /// <summary>
    /// Register dynamic services that changes between environments or context(i.e: tests)
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureProductionDependencies(WebHostBuilderContext context, IServiceCollection services)
    {
        services.AddSecrets();

        services.Configure<ServiceBusConfiguration>(context.Configuration.GetSection("ServiceBusConfiguration"));
        services.AddServiceBus();
        services.AddTransient<IApplicationEventPublisher, EventPublisher>();

        //services.Configure<CosmosDbConfiguration>(context.Configuration.GetSection("CosmosDb"));
        //services.AddCosmosDB();
        //services.AddTransient<IRewardRepository, CosmosDbRewardRepository>();

        //var healthChecks = services.AddHealthChecks();
        //healthChecks.AddCheck<CustomHealthCheck>("Sample"); //This is a sample health check, remove if not needed, more info: https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/monitor-app-health
        //healthChecks.AddCheck<CosmosDbDocumentStorage<Reward>>("CosmosDB");

        services.AddSingleton<IRulesEngine, RulesEngine>();
    }

    private static void AddCommandHandlers(IServiceCollection services)
    {
        log.Information("Loading implementations of {interface}", typeof(ICommandHandler<,>).Name);
        var definitions = typeof(CreateRewardCommandHandler).Assembly.GetImplementationsOf(typeof(ICommandHandler<,>));
        foreach (var definition in definitions)
        {
            log.Information("Registering '{implementation}' as implementation of '{interface}'",
                definition.implementation.FullName, definition.interfaceVariation.FullName);
            services.AddTransient(definition.interfaceVariation, definition.implementation);
        }
    }

    private static void AddQueryHandlers(IServiceCollection services)
    {
        log.Information("Loading implementations of {interface}", typeof(IQueryHandler<,>).Name);
        var definitions = typeof(GetRewardByIdQueryHandler).Assembly.GetImplementationsOf(typeof(IQueryHandler<,>));
        foreach (var definition in definitions)
        {
            log.Information("Registering '{implementation}' as implementation of '{interface}'",
                definition.implementation.FullName, definition.interfaceVariation.FullName);
            services.AddTransient(definition.interfaceVariation, definition.implementation);
        }
    }
}
