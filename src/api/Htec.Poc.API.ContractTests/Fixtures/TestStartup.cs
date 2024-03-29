﻿using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using Htec.Poc.API.Authentication;
using Htec.Poc.Application.Integration;

namespace Htec.Poc.API.ContractTests.Fixtures;

public class TestStartup : Startup
{
    public TestStartup(IWebHostEnvironment env, IConfiguration configuration, ILogger<Startup> logger) : base(env, configuration, logger)
    {
    }


    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ProviderStateMiddleware>();

        services.Configure<KestrelServerOptions>(options =>
        {
            options.AllowSynchronousIO = true;
        });

        AddMocks(services);

        base.ConfigureServices(services);
    }

    public override void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<JwtBearerAuthenticationConfiguration> jwtBearerAuthenticationOptions)
    {
        app.UseMiddleware<ProviderStateMiddleware>();
        base.Configure(app, env, jwtBearerAuthenticationOptions);
    }

    public void AddMocks(IServiceCollection services)
    {
        services.AddSingleton<IRewardRepository>((svc) => Substitute.For<IRewardRepository>());
        services.AddSingleton<IApplicationEventPublisher>((svc) => Substitute.For<IApplicationEventPublisher>());
    }
}
