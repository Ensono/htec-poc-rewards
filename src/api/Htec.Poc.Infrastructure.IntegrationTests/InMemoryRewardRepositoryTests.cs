using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using Xunit;
using Xunit.Abstractions;
using Htec.Poc.Domain;
using Htec.Poc.Infrastructure.Fakes;

namespace Htec.Poc.Infrastructure.IntegrationTests;

/// <summary>
/// The purpose of this integration test is to validate the implementation
/// of RewardRepository againt the data store at development\integration
/// It is not intended to test if the configuration is valid for a release
/// Configuration issues will be surfaced on e2e or acceptance tests
/// </summary>
[Trait("TestType", "IntegrationTests")]
public class InMemoryRewardRepositoryTests
{
    private readonly ITestOutputHelper output;

    public InMemoryRewardRepositoryTests(ITestOutputHelper output)
    {
        this.output = output;
    }

    //GetByIdTest will be tested as part of Save+Get OR Get+Delete+Get
    //public void GetByIdTest() { }

    /// <summary>
    /// Ensure the implementation of RewardRepository.Save() submit 
    /// the reward information and is retrieved properly
    /// </summary>
    [Theory, AutoData]
    public async Task SaveAndGetTest(InMemoryRewardRepository repository, Reward reward)
    {
        output.WriteLine($"Creating the reward '{reward.Id}' in the repository");
        await repository.SaveAsync(reward);
        output.WriteLine($"Retrieving the reward '{reward.Id}' from the repository");
        var dbItem = await repository.GetByIdAsync(reward.Id);

        Assert.NotNull(dbItem);
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
    [Theory, AutoData]
    public async Task DeleteTest(InMemoryRewardRepository repository, Reward reward)
    {
        output.WriteLine($"Creating the reward '{reward.Id}' in the repository");
        await repository.SaveAsync(reward);
        output.WriteLine($"Retrieving the reward '{reward.Id}' from the repository");
        var dbItem = await repository.GetByIdAsync(reward.Id);
        Assert.NotNull(dbItem);

        output.WriteLine($"Removing the reward '{reward.Id}' from the repository");
        await repository.DeleteAsync(reward.Id);
        output.WriteLine($"Retrieving the reward '{reward.Id}' from the repository");
        dbItem = await repository.GetByIdAsync(reward.Id);
        Assert.Null(dbItem);
    }

    /// <summary>
    /// This test will run 100 operations concurrently to test concurrency issues
    /// </summary>
    [Theory, AutoData]
    public async Task ParallelRunTest(InMemoryRewardRepository repository)
    {
        Task[] tasks = new Task[100];

        Fixture fixture = new Fixture();
        for (int i = 0; i < tasks.Length; i++)
        {
            if (i % 2 == 0)
                tasks[i] = Task.Run(async () => await SaveAndGetTest(repository, fixture.Create<Reward>()));
            else
                tasks[i] = Task.Run(async () => await DeleteTest(repository, fixture.Create<Reward>()));
        }

        await Task.WhenAll(tasks);

        for (int i = 0; i < tasks.Length; i++)
        {
            Assert.False(tasks[i].IsFaulted, tasks[i].Exception?.Message);
        }
    }
}
