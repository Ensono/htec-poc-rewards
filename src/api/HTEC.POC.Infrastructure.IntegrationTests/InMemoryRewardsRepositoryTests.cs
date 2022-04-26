using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using Xunit;
using Xunit.Abstractions;
using HTEC.POC.Domain;
using HTEC.POC.Infrastructure.Fakes;

namespace HTEC.POC.Infrastructure.IntegrationTests;

/// <summary>
/// The purpose of this integration test is to validate the implementation
/// of RewardsRepository againt the data store at development\integration
/// It is not intended to test if the configuration is valid for a release
/// Configuration issues will be surfaced on e2e or acceptance tests
/// </summary>
[Trait("TestType", "IntegrationTests")]
public class InMemoryRewardsRepositoryTests
{
    private readonly ITestOutputHelper output;

    public InMemoryRewardsRepositoryTests(ITestOutputHelper output)
    {
        this.output = output;
    }

    //GetByIdTest will be tested as part of Save+Get OR Get+Delete+Get
    //public void GetByIdTest() { }

    /// <summary>
    /// Ensure the implementation of RewardsRepository.Save() submit 
    /// the rewards information and is retrieved properly
    /// </summary>
    [Theory, AutoData]
    public async Task SaveAndGetTest(InMemoryRewardsRepository repository, Rewards rewards)
    {
        output.WriteLine($"Creating the rewards '{rewards.Id}' in the repository");
        await repository.SaveAsync(rewards);
        output.WriteLine($"Retrieving the rewards '{rewards.Id}' from the repository");
        var dbItem = await repository.GetByIdAsync(rewards.Id);

        Assert.NotNull(dbItem);
        Assert.Equal(dbItem.Id, rewards.Id);
        Assert.Equal(dbItem.Name, rewards.Name);
        Assert.Equal(dbItem.TenantId, rewards.TenantId);
        Assert.Equal(dbItem.Description, rewards.Description);
        Assert.Equal(dbItem.Enabled, rewards.Enabled);
        Assert.Equal(dbItem.Categories, rewards.Categories);
    }

    /// <summary>
    /// Ensure the implementation of RewardsRepository.Delete() 
    /// removes an existing rewards and is not retrieved when requested
    /// </summary>
    [Theory, AutoData]
    public async Task DeleteTest(InMemoryRewardsRepository repository, Rewards rewards)
    {
        output.WriteLine($"Creating the rewards '{rewards.Id}' in the repository");
        await repository.SaveAsync(rewards);
        output.WriteLine($"Retrieving the rewards '{rewards.Id}' from the repository");
        var dbItem = await repository.GetByIdAsync(rewards.Id);
        Assert.NotNull(dbItem);

        output.WriteLine($"Removing the rewards '{rewards.Id}' from the repository");
        await repository.DeleteAsync(rewards.Id);
        output.WriteLine($"Retrieving the rewards '{rewards.Id}' from the repository");
        dbItem = await repository.GetByIdAsync(rewards.Id);
        Assert.Null(dbItem);
    }

    /// <summary>
    /// This test will run 100 operations concurrently to test concurrency issues
    /// </summary>
    [Theory, AutoData]
    public async Task ParallelRunTest(InMemoryRewardsRepository repository)
    {
        Task[] tasks = new Task[100];

        Fixture fixture = new Fixture();
        for (int i = 0; i < tasks.Length; i++)
        {
            if (i % 2 == 0)
                tasks[i] = Task.Run(async () => await SaveAndGetTest(repository, fixture.Create<Rewards>()));
            else
                tasks[i] = Task.Run(async () => await DeleteTest(repository, fixture.Create<Rewards>()));
        }

        await Task.WhenAll(tasks);

        for (int i = 0; i < tasks.Length; i++)
        {
            Assert.False(tasks[i].IsFaulted, tasks[i].Exception?.Message);
        }
    }
}
