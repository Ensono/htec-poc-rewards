﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Amido.Stacks.Configuration;
using Amido.Stacks.Data.Documents.Abstractions;
using Amido.Stacks.Data.Documents.CosmosDB;
using Amido.Stacks.Testing.Settings;
using AutoFixture;
using AutoFixture.Xunit2;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;
using Htec.Poc.Domain;
using Htec.Poc.Infrastructure.Repositories;

namespace Htec.Poc.Infrastructure.IntegrationTests;

/// <summary>
/// The purpose of this integration test is to validate the implementation
/// of RewardRepository againt the data store at development\integration
/// It is not intended to test if the configuration is valid for a release
/// Configuration issues will be surfaced on e2e or acceptance tests
/// </summary>
[Trait("TestType", "IntegrationTests")]
public class CosmosDbRewardRepositoryTests
{
    public CosmosDbRewardRepositoryTests()
    {
        var settings = Configuration.For<CosmosDbConfiguration>("CosmosDB");
        //Notes:
        // if using an azure instance to run the tests, make sure you set the environment variable before you start visual studio
        // Ex: CMD C:\> setx COSMOSDB_KEY=ABCDEFGASD==
        // On CosmosDB, make sure you create the collection 'Reward' in the same database defined in the config.
        // To overrride the appsettings values, set the environment variable using SectionName__PropertyName. i.e: CosmosDB__DatabaseAccountUri
        // Note the use of a double _ between the section and the property name
        if (Environment.GetEnvironmentVariable(settings.SecurityKeySecret.Identifier) == null)
        {
            //if localhost and running in visual studio use the default emulator key
            if (settings.DatabaseAccountUri.Contains("localhost", StringComparison.InvariantCultureIgnoreCase) && Environment.GetEnvironmentVariable("VisualStudioEdition") != null)
                Environment.SetEnvironmentVariable(settings.SecurityKeySecret.Identifier, "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==");
            else
                throw new ArgumentNullException($"The environment variable '{settings.SecurityKeySecret.Identifier}' has not been set. Ensure all environment variables required are set before running integration tests.");
        }
    }

    //GetByIdTest will be tested as part of Save+Get OR Get+Delete+Get
    //public void GetByIdTest() { }

    /// <summary>
    /// Ensure the implementation of RewardRepository.Save() submit
    /// the reward information and is retrieved properly
    /// </summary>
    [Theory, RewardRepositoryAutoData]
    public async Task SaveAndGetTest(CosmosDbRewardRepository repository, Reward reward)
    {
        await repository.SaveAsync(reward);
        var dbItem = await repository.GetByIdAsync(reward.Id);

        //Assert the values returned from DB matches the values sent
        Assert.Equal(dbItem.Id, reward.Id);
        Assert.Equal(dbItem.Name, reward.Name);
        Assert.Equal(dbItem.TenantId, reward.TenantId);
        Assert.Equal(dbItem.Description, reward.Description);
        Assert.Equal(dbItem.Enabled, reward.Enabled);
    }

    /// <summary>
    /// Ensure the implementation of RewardRepository.Delete()
    /// removes an existing reward and is not retrieved when requested
    /// </summary>
    [Theory, RewardRepositoryAutoData]
    public async Task DeleteTest(CosmosDbRewardRepository repository, Reward reward)
    {
        await repository.SaveAsync(reward);
        var dbItem = await repository.GetByIdAsync(reward.Id);
        Assert.NotNull(dbItem);

        await repository.DeleteAsync(reward.Id);
        dbItem = await repository.GetByIdAsync(reward.Id);
        Assert.Null(dbItem);
    }
}

public class RewardRepositoryAutoData : AutoDataAttribute
{
    public RewardRepositoryAutoData() : base(Customizations) { }

    public static IFixture Customizations()
    {
        var settings = Configuration.For<CosmosDbConfiguration>("CosmosDB");

        IFixture fixture = new Fixture();

        var loggerFactory = Substitute.For<ILoggerFactory>();
        loggerFactory.CreateLogger(Arg.Any<string>()).Returns(new Logger<CosmosDbDocumentStorage<Reward>>(loggerFactory));
        fixture.Register<ILogger<CosmosDbDocumentStorage<Reward>>>(() => new Logger<CosmosDbDocumentStorage<Reward>>(loggerFactory));
        fixture.Register<ISecretResolver<string>>(() => new SecretResolver());
        fixture.Register<IOptions<CosmosDbConfiguration>>(() => settings.AsOption());
        fixture.Register<IDocumentStorage<Reward>>(() => fixture.Create<CosmosDbDocumentStorage<Reward>>());

        return fixture;
    }
}
