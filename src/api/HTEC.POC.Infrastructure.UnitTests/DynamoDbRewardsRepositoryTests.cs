using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amido.Stacks.Data.Documents;
using Amido.Stacks.DynamoDB.Abstractions;
using FluentAssertions;
using NSubstitute;
using Xunit;
using HTEC.POC.Application.Integration;
using HTEC.POC.Domain;
using HTEC.POC.Domain.Entities;
using HTEC.POC.Infrastructure.Repositories;

namespace HTEC.POC.Infrastructure.UnitTests;

public class DynamoDbRewardsRepositoryTests
{
    private readonly DynamoDbRewardsRepository repository;
    private readonly IDynamoDbObjectStorage<Rewards> fakeRewardsRepository;
    private Rewards rewards;

    public DynamoDbRewardsRepositoryTests()
    {
        fakeRewardsRepository = SetupFakeRewardsRepository();
        repository = new DynamoDbRewardsRepository(fakeRewardsRepository);
    }

    [Fact]
    public void DynamoDbRewardsRepository_Should_ImplementIRewardsRepository()
    {
        // Arrange
        // Act
        // Assert
        typeof(DynamoDbRewardsRepository).Should().Implement<IRewardsRepository>();
    }

    [Fact]
    public async Task GetByIdAsync()
    {
        // Arrange
        // Act
        var result = await repository.GetByIdAsync(Guid.Empty);

        // Assert
        await fakeRewardsRepository.Received().GetByIdAsync(Arg.Any<string>());

        result.Should().BeOfType<Rewards>();
        result.Should().Be(rewards);
    }

    [Fact]
    public async Task SaveAsync()
    {
        // Arrange
        // Act
        var result = await repository.SaveAsync(rewards);

        // Assert
        await fakeRewardsRepository.Received().SaveAsync(rewards.Id.ToString(), rewards);

        result.Should().Be(true);
    }

    [Fact]
    public async Task DeleteAsync()
    {
        // Arrange
        // Act
        var result = await repository.DeleteAsync(Guid.Empty);

        // Assert
        await fakeRewardsRepository.Received().DeleteAsync(Arg.Any<string>());

        result.Should().Be(true);
    }

    private IDynamoDbObjectStorage<Rewards> SetupFakeRewardsRepository()
    {
        var rewardsRepository = Substitute.For<IDynamoDbObjectStorage<Rewards>>();
        rewards = new Rewards(Guid.Empty, "testName", Guid.Empty, "testDescription", true, new List<Category>());
        var fakeTypeResponse = new OperationResult<Rewards>(true, rewards, new Dictionary<string, string>());
        var fakeNonTypeResponse = new OperationResult(true, new Dictionary<string, string>());

        rewardsRepository.GetByIdAsync(Arg.Any<string>())
            .Returns(Task.FromResult(fakeTypeResponse));
        rewardsRepository.SaveAsync(rewards.Id.ToString(), rewards)
            .Returns(Task.FromResult(fakeTypeResponse));
        rewardsRepository.DeleteAsync(Arg.Any<string>())
            .Returns(Task.FromResult(fakeNonTypeResponse));

        return rewardsRepository;
    }
}
