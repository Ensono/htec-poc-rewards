using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amido.Stacks.Data.Documents;
using Amido.Stacks.Data.Documents.Abstractions;
using FluentAssertions;
using NSubstitute;
using Xunit;
using Htec.Poc.Application.Integration;
using Htec.Poc.Domain;
using Htec.Poc.Domain.Entities;
using Htec.Poc.Infrastructure.Repositories;

namespace Htec.Poc.Infrastructure.UnitTests;

public class CosmosDbRewardRepositoryTests
{
    private readonly CosmosDbRewardRepository repository;
    private readonly IDocumentStorage<Reward> fakeRewardRepository;
    private Reward reward;

    public CosmosDbRewardRepositoryTests()
    {
        fakeRewardRepository = SetupFakeRewardRepository();
        repository = new CosmosDbRewardRepository(fakeRewardRepository);
    }

    [Fact]
    public void CosmosDbRewardRepository_Should_ImplementIRewardRepository()
    {
        // Arrange
        // Act
        // Assert
        typeof(CosmosDbRewardRepository).Should().Implement<IRewardRepository>();
    }

    [Fact]
    public async Task GetByIdAsync()
    {
        // Arrange
        // Act
        var result = await repository.GetByIdAsync(Guid.Empty);

        // Assert
        await fakeRewardRepository.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>());

        result.Should().BeOfType<Reward>();
        result.Should().Be(reward);
    }

    [Fact]
    public async Task SaveAsync()
    {
        // Arrange
        // Act
        var result = await repository.SaveAsync(reward);

        // Assert
        await fakeRewardRepository.Received().SaveAsync(Arg.Any<string>(), Arg.Any<string>(), reward, Arg.Any<string>());

        result.Should().Be(true);
    }

    [Fact]
    public async Task DeleteAsync()
    {
        // Arrange
        // Act
        var result = await repository.DeleteAsync(Guid.Empty);

        // Assert
        await fakeRewardRepository.Received().DeleteAsync(Arg.Any<string>(), Arg.Any<string>());

        result.Should().Be(true);
    }

    private IDocumentStorage<Reward> SetupFakeRewardRepository()
    {
        var rewardRepository = Substitute.For<IDocumentStorage<Domain.Reward>>();
        reward = new Reward(Guid.Empty, "testName", Guid.Empty, "testDescription", true, new List<Category>());
        var fakeTypeResponse = new OperationResult<Reward>(true, reward, new Dictionary<string, string>());
        var fakeNonTypeResponse = new OperationResult(true, new Dictionary<string, string>());

        rewardRepository.GetByIdAsync(Arg.Any<string>(), Arg.Any<string>())
            .Returns(Task.FromResult(fakeTypeResponse));
        rewardRepository.SaveAsync(Arg.Any<string>(), Arg.Any<string>(), reward, Arg.Any<string>())
            .Returns(Task.FromResult(fakeTypeResponse));
        rewardRepository.DeleteAsync(Arg.Any<string>(), Arg.Any<string>())
            .Returns(Task.FromResult(fakeNonTypeResponse));

        return rewardRepository;
    }
}
