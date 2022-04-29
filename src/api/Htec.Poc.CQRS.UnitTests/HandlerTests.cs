using System;
using System.Collections.Generic;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Core.Operations;
using Amido.Stacks.Data.Documents;
using Amido.Stacks.Data.Documents.Abstractions;
using AutoFixture;
using AutoFixture.Xunit2;
using NSubstitute;
using Shouldly;
using Xunit;
using Htec.Poc.Application.CommandHandlers;
using Htec.Poc.Application.Integration;
using Htec.Poc.Application.QueryHandlers;
using Htec.Poc.Common.Exceptions;
using Htec.Poc.CQRS.Commands;
using Htec.Poc.CQRS.Queries.GetRewardById;
using Htec.Poc.CQRS.Queries.SearchReward;
using Htec.Poc.Domain.RewardAggregateRoot.Exceptions;
using Query = Htec.Poc.CQRS.Queries;

namespace Htec.Poc.CQRS.UnitTests;

/// <summary>
/// Series of tests for command handlers
/// </summary>
[Trait("TestType", "UnitTests")]
public class HandlerTests
{
    private Fixture fixture;
    private IRewardRepository rewardRepo;
    private IApplicationEventPublisher eventPublisher;
    private IDocumentSearch<Domain.Reward> storage;

    public HandlerTests()
    {
        fixture = new Fixture();
        fixture.Register(() => Substitute.For<IOperationContext>());
        fixture.Register(() => Substitute.For<IRewardRepository>());
        fixture.Register(() => Substitute.For<IApplicationEventPublisher>());
        fixture.Register(() => Substitute.For<IDocumentSearch<Domain.Reward>>());

        rewardRepo = fixture.Create<IRewardRepository>();
        eventPublisher = fixture.Create<IApplicationEventPublisher>();
        storage = fixture.Create<IDocumentSearch<Domain.Reward>>();
    }

    #region CREATE

    [Theory, AutoData]
    public async void CreateRewardCommandHandler_HandleAsync(CreateReward command)
    {
        // Arrange
        var handler = new CreateRewardCommandHandler(rewardRepo, eventPublisher);

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        await rewardRepo.Received(1).SaveAsync(Arg.Any<Domain.Reward>());
        await eventPublisher.Received(1).PublishAsync(Arg.Any<IApplicationEvent>());

        result.ShouldBeOfType<Guid>();
    }

    [Theory, AutoData]
    public async void CreateCategoryCommandHandler_HandleAsync(Domain.Reward reward, CreateCategory command)
    {
        // Arrange
        var handler = new CreateCategoryCommandHandler(rewardRepo, eventPublisher);

        // Act
        var result = await handler.HandleCommandAsync(reward, command);

        // Assert
        result.ShouldBeOfType<Guid>();
    }

    [Theory, AutoData]
    public async void CreateRewardItemCommandHandler_HandleAsync(Domain.Reward reward, CreateRewardItem command)
    {
        // Arrange
        var handler = new CreateRewardItemCommandHandler(rewardRepo, eventPublisher);
        command.CategoryId = reward.Categories[0].Id;

        // Act
        var result = await handler.HandleCommandAsync(reward, command);

        // Assert
        result.ShouldBeOfType<Guid>();
    }

    #endregion

    #region DELETE

    [Theory, AutoData]
    public async void DeleteRewardCommandHandler_HandleAsync(Domain.Reward reward, DeleteReward command)
    {
        // Arrange
        rewardRepo.GetByIdAsync(Arg.Any<Guid>()).Returns(reward);
        rewardRepo.DeleteAsync(Arg.Any<Guid>()).Returns(true);

        var handler = new DeleteRewardCommandHandler(rewardRepo, eventPublisher);

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        await rewardRepo.Received(1).DeleteAsync(Arg.Any<Guid>());
        await eventPublisher.Received(1).PublishAsync(Arg.Any<IApplicationEvent>());

        result.ShouldBeOfType<bool>();
        result.ShouldBeTrue();
    }

    [Theory, AutoData]
    public async void DeleteRewardCommandHandler_HandleAsync_RewardMissing_ShouldThrow(DeleteReward command)
    {
        // Arrange
        var handler = fixture.Create<DeleteRewardCommandHandler>();

        // Act
        // Assert
        await handler.HandleAsync(command).ShouldThrowAsync<RewardDoesNotExistException>();
        await rewardRepo.Received(0).DeleteAsync(Arg.Any<Guid>());
        await eventPublisher.Received(0).PublishAsync(Arg.Any<IApplicationEvent>());
    }

    [Theory, AutoData]
    public async void DeleteRewardCommandHandler_HandleAsync_NotSuccessful_ShouldThrow(Domain.Reward reward, DeleteReward command)
    {
        // Arrange
        rewardRepo.GetByIdAsync(Arg.Any<Guid>()).Returns(reward);
        rewardRepo.DeleteAsync(Arg.Any<Guid>()).Returns(false);
        var handler = new DeleteRewardCommandHandler(rewardRepo, eventPublisher);

        // Act
        // Assert
        await handler.HandleAsync(command).ShouldThrowAsync<OperationFailedException>();
        await rewardRepo.Received(1).DeleteAsync(Arg.Any<Guid>());
        await eventPublisher.Received(0).PublishAsync(Arg.Any<IApplicationEvent>());
    }

    #endregion

    #region UPDATE

    [Theory, AutoData]
    public async void UpdateRewardCommandHandler_HandleAsync(Domain.Reward reward, UpdateReward command)
    {
        // Arrange
        var handler = new UpdateRewardCommandHandler(rewardRepo, eventPublisher);

        // Act
        var result = await handler.HandleCommandAsync(reward, command);

        // Assert
        result.ShouldBeOfType<bool>();
        result.ShouldBe(true);
    }

    [Theory, AutoData]
    public async void UpdateCategoryCommandHandler_HandleAsync(Domain.Reward reward, UpdateCategory command)
    {
        // Arrange
        var handler = new UpdateCategoryCommandHandler(rewardRepo, eventPublisher);
        command.CategoryId = reward.Categories[0].Id;

        // Act
        var result = await handler.HandleCommandAsync(reward, command);

        // Assert
        result.ShouldBeOfType<bool>();
        result.ShouldBe(true);
    }

    [Theory, AutoData]
    public async void UpdateRewardItemCommandHandler_HandleAsync(Domain.Reward reward, UpdateRewardItem command)
    {
        // Arrange
        var handler = new UpdateRewardItemCommandHandler(rewardRepo, eventPublisher);
        command.CategoryId = reward.Categories[0].Id;
        command.RewardItemId = reward.Categories[0].Items[0].Id;


        // Act
        var result = await handler.HandleCommandAsync(reward, command);

        // Assert
        result.ShouldBeOfType<bool>();
        result.ShouldBe(true);
    }

    [Theory, AutoData]
    public async void UpdateCategoryCommandHandler_HandleAsync_NoCategory_ShouldThrow(Domain.Reward reward, UpdateCategory command)
    {
        // Arrange
        var handler = new UpdateCategoryCommandHandler(rewardRepo, eventPublisher);

        // Act
        // Assert
        await Should.ThrowAsync<CategoryDoesNotExistException>(async () => await handler.HandleCommandAsync(reward, command));
    }

    [Theory, AutoData]
    public async void UpdateRewardItemCommandHandler_HandleAsync_NoRewardItem_ShouldThrow(Domain.Reward reward, UpdateRewardItem command)
    {
        // Arrange
        var handler = new UpdateRewardItemCommandHandler(rewardRepo, eventPublisher);
        command.CategoryId = reward.Categories[0].Id;

        // Act
        // Assert
        await Should.ThrowAsync<RewardItemDoesNotExistException>(async () => await handler.HandleCommandAsync(reward, command));
    }

    #endregion

    #region QUERIES

    [Theory, AutoData]
    public async void GetRewardByIdQueryHandler_ExecuteAsync(Domain.Reward reward, GetRewardById criteria)
    {
        // Arrange
        rewardRepo.GetByIdAsync(Arg.Any<Guid>()).Returns(reward);
        var handler = new GetRewardByIdQueryHandler(rewardRepo);

        // Act
        var result = await handler.ExecuteAsync(criteria);

        // Assert
        await rewardRepo.Received(1).GetByIdAsync(Arg.Any<Guid>());
        result.ShouldNotBeNull();
        result.ShouldBeOfType<Reward>();
    }

    [Theory, AutoData]
    public async void GetRewardByIdQueryHandler_ExecuteAsync_NoReward_ReturnNull(GetRewardById criteria)
    {
        // Arrange
        var handler = new GetRewardByIdQueryHandler(rewardRepo);

        // Act
        var result = await handler.ExecuteAsync(criteria);

        // Assert
        await rewardRepo.Received(1).GetByIdAsync(Arg.Any<Guid>());
        result.ShouldBeNull();
    }

    [Theory, AutoData]
    public async void SearchRewardQueryHandler_ExecuteAsync(SearchReward criteria, OperationResult<IEnumerable<Domain.Reward>> operationResult)
    {
        // Arrange
        storage.Search(
            Arg.Any<System.Linq.Expressions.Expression<Func<Domain.Reward, bool>>>(),
            Arg.Any<string>(),
            Arg.Any<int>(),
            Arg.Any<int>())
        .Returns(operationResult);

        var handler = new SearchRewardQueryHandler(storage);

        // Act
        var result = await handler.ExecuteAsync(criteria);

        // Assert
        await storage.Received(1).Search(
            Arg.Any<System.Linq.Expressions.Expression<Func<Domain.Reward, bool>>>(),
            Arg.Any<string>(),
            Arg.Any<int>(),
            Arg.Any<int>());

        result.ShouldBeOfType<SearchRewardResult>();
    }

    [Fact]
    public async void SearchRewardQueryHandler_ExecuteAsync_NoCriteria_ShouldThrow()
    {
        // Arrange
        var handler = new SearchRewardQueryHandler(storage);

        // Act
        // Assert
        await Should.ThrowAsync<ArgumentException>(async () => await handler.ExecuteAsync(null));
    }

    [Theory, AutoData]
    public async void SearchRewardQueryHandler_ExecuteAsync_NotSuccessful(SearchReward criteria)
    {
        // Arrange
        var operationResult = new OperationResult<IEnumerable<Domain.Reward>>(false, null, null);

        storage.Search(
            Arg.Any<System.Linq.Expressions.Expression<Func<Domain.Reward, bool>>>(),
            Arg.Any<string>(),
            Arg.Any<int>(),
            Arg.Any<int>())
        .Returns(operationResult);

        var handler = new SearchRewardQueryHandler(storage);

        // Act
        var result = await handler.ExecuteAsync(criteria);

        // Assert
        await storage.Received(1).Search(
            Arg.Any<System.Linq.Expressions.Expression<Func<Domain.Reward, bool>>>(),
            Arg.Any<string>(),
            Arg.Any<int>(),
            Arg.Any<int>());

        result.ShouldBeOfType<SearchRewardResult>();
        result.Results.ShouldBeNull();
    }

    #endregion
}
