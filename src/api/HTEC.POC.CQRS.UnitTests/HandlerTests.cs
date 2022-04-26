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
using HTEC.POC.Application.CommandHandlers;
using HTEC.POC.Application.Integration;
using HTEC.POC.Application.QueryHandlers;
using HTEC.POC.Common.Exceptions;
using HTEC.POC.CQRS.Commands;
using HTEC.POC.CQRS.Queries.GetRewardsById;
using HTEC.POC.CQRS.Queries.SearchRewards;
using HTEC.POC.Domain.RewardsAggregateRoot.Exceptions;
using Query = HTEC.POC.CQRS.Queries;

namespace HTEC.POC.CQRS.UnitTests;

/// <summary>
/// Series of tests for command handlers
/// </summary>
[Trait("TestType", "UnitTests")]
public class HandlerTests
{
    private Fixture fixture;
    private IRewardsRepository rewardsRepo;
    private IApplicationEventPublisher eventPublisher;
    private IDocumentSearch<Domain.Rewards> storage;

    public HandlerTests()
    {
        fixture = new Fixture();
        fixture.Register(() => Substitute.For<IOperationContext>());
        fixture.Register(() => Substitute.For<IRewardsRepository>());
        fixture.Register(() => Substitute.For<IApplicationEventPublisher>());
        fixture.Register(() => Substitute.For<IDocumentSearch<Domain.Rewards>>());

        rewardsRepo = fixture.Create<IRewardsRepository>();
        eventPublisher = fixture.Create<IApplicationEventPublisher>();
        storage = fixture.Create<IDocumentSearch<Domain.Rewards>>();
    }

    #region CREATE

    [Theory, AutoData]
    public async void CreateRewardsCommandHandler_HandleAsync(CreateRewards command)
    {
        // Arrange
        var handler = new CreateRewardsCommandHandler(rewardsRepo, eventPublisher);

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        await rewardsRepo.Received(1).SaveAsync(Arg.Any<Domain.Rewards>());
        await eventPublisher.Received(1).PublishAsync(Arg.Any<IApplicationEvent>());

        result.ShouldBeOfType<Guid>();
    }

    [Theory, AutoData]
    public async void CreateCategoryCommandHandler_HandleAsync(Domain.Rewards rewards, CreateCategory command)
    {
        // Arrange
        var handler = new CreateCategoryCommandHandler(rewardsRepo, eventPublisher);

        // Act
        var result = await handler.HandleCommandAsync(rewards, command);

        // Assert
        result.ShouldBeOfType<Guid>();
    }

    [Theory, AutoData]
    public async void CreateRewardsItemCommandHandler_HandleAsync(Domain.Rewards rewards, CreateRewardsItem command)
    {
        // Arrange
        var handler = new CreateRewardsItemCommandHandler(rewardsRepo, eventPublisher);
        command.CategoryId = rewards.Categories[0].Id;

        // Act
        var result = await handler.HandleCommandAsync(rewards, command);

        // Assert
        result.ShouldBeOfType<Guid>();
    }

    #endregion

    #region DELETE

    [Theory, AutoData]
    public async void DeleteRewardsCommandHandler_HandleAsync(Domain.Rewards rewards, DeleteRewards command)
    {
        // Arrange
        rewardsRepo.GetByIdAsync(Arg.Any<Guid>()).Returns(rewards);
        rewardsRepo.DeleteAsync(Arg.Any<Guid>()).Returns(true);

        var handler = new DeleteRewardsCommandHandler(rewardsRepo, eventPublisher);

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        await rewardsRepo.Received(1).DeleteAsync(Arg.Any<Guid>());
        await eventPublisher.Received(1).PublishAsync(Arg.Any<IApplicationEvent>());

        result.ShouldBeOfType<bool>();
        result.ShouldBeTrue();
    }

    [Theory, AutoData]
    public async void DeleteRewardsCommandHandler_HandleAsync_RewardsMissing_ShouldThrow(DeleteRewards command)
    {
        // Arrange
        var handler = fixture.Create<DeleteRewardsCommandHandler>();

        // Act
        // Assert
        await handler.HandleAsync(command).ShouldThrowAsync<RewardsDoesNotExistException>();
        await rewardsRepo.Received(0).DeleteAsync(Arg.Any<Guid>());
        await eventPublisher.Received(0).PublishAsync(Arg.Any<IApplicationEvent>());
    }

    [Theory, AutoData]
    public async void DeleteRewardsCommandHandler_HandleAsync_NotSuccessful_ShouldThrow(Domain.Rewards rewards, DeleteRewards command)
    {
        // Arrange
        rewardsRepo.GetByIdAsync(Arg.Any<Guid>()).Returns(rewards);
        rewardsRepo.DeleteAsync(Arg.Any<Guid>()).Returns(false);
        var handler = new DeleteRewardsCommandHandler(rewardsRepo, eventPublisher);

        // Act
        // Assert
        await handler.HandleAsync(command).ShouldThrowAsync<OperationFailedException>();
        await rewardsRepo.Received(1).DeleteAsync(Arg.Any<Guid>());
        await eventPublisher.Received(0).PublishAsync(Arg.Any<IApplicationEvent>());
    }

    #endregion

    #region UPDATE

    [Theory, AutoData]
    public async void UpdateRewardsCommandHandler_HandleAsync(Domain.Rewards rewards, UpdateRewards command)
    {
        // Arrange
        var handler = new UpdateRewardsCommandHandler(rewardsRepo, eventPublisher);

        // Act
        var result = await handler.HandleCommandAsync(rewards, command);

        // Assert
        result.ShouldBeOfType<bool>();
        result.ShouldBe(true);
    }

    [Theory, AutoData]
    public async void UpdateCategoryCommandHandler_HandleAsync(Domain.Rewards rewards, UpdateCategory command)
    {
        // Arrange
        var handler = new UpdateCategoryCommandHandler(rewardsRepo, eventPublisher);
        command.CategoryId = rewards.Categories[0].Id;

        // Act
        var result = await handler.HandleCommandAsync(rewards, command);

        // Assert
        result.ShouldBeOfType<bool>();
        result.ShouldBe(true);
    }

    [Theory, AutoData]
    public async void UpdateRewardsItemCommandHandler_HandleAsync(Domain.Rewards rewards, UpdateRewardsItem command)
    {
        // Arrange
        var handler = new UpdateRewardsItemCommandHandler(rewardsRepo, eventPublisher);
        command.CategoryId = rewards.Categories[0].Id;
        command.RewardsItemId = rewards.Categories[0].Items[0].Id;


        // Act
        var result = await handler.HandleCommandAsync(rewards, command);

        // Assert
        result.ShouldBeOfType<bool>();
        result.ShouldBe(true);
    }

    [Theory, AutoData]
    public async void UpdateCategoryCommandHandler_HandleAsync_NoCategory_ShouldThrow(Domain.Rewards rewards, UpdateCategory command)
    {
        // Arrange
        var handler = new UpdateCategoryCommandHandler(rewardsRepo, eventPublisher);

        // Act
        // Assert
        await Should.ThrowAsync<CategoryDoesNotExistException>(async () => await handler.HandleCommandAsync(rewards, command));
    }

    [Theory, AutoData]
    public async void UpdateRewardsItemCommandHandler_HandleAsync_NoRewardsItem_ShouldThrow(Domain.Rewards rewards, UpdateRewardsItem command)
    {
        // Arrange
        var handler = new UpdateRewardsItemCommandHandler(rewardsRepo, eventPublisher);
        command.CategoryId = rewards.Categories[0].Id;

        // Act
        // Assert
        await Should.ThrowAsync<RewardsItemDoesNotExistException>(async () => await handler.HandleCommandAsync(rewards, command));
    }

    #endregion

    #region QUERIES

    [Theory, AutoData]
    public async void GetRewardsByIdQueryHandler_ExecuteAsync(Domain.Rewards rewards, GetRewardsById criteria)
    {
        // Arrange
        rewardsRepo.GetByIdAsync(Arg.Any<Guid>()).Returns(rewards);
        var handler = new GetRewardsByIdQueryHandler(rewardsRepo);

        // Act
        var result = await handler.ExecuteAsync(criteria);

        // Assert
        await rewardsRepo.Received(1).GetByIdAsync(Arg.Any<Guid>());
        result.ShouldNotBeNull();
        result.ShouldBeOfType<Rewards>();
    }

    [Theory, AutoData]
    public async void GetRewardsByIdQueryHandler_ExecuteAsync_NoRewards_ReturnNull(GetRewardsById criteria)
    {
        // Arrange
        var handler = new GetRewardsByIdQueryHandler(rewardsRepo);

        // Act
        var result = await handler.ExecuteAsync(criteria);

        // Assert
        await rewardsRepo.Received(1).GetByIdAsync(Arg.Any<Guid>());
        result.ShouldBeNull();
    }

    [Theory, AutoData]
    public async void SearchRewardsQueryHandler_ExecuteAsync(SearchRewards criteria, OperationResult<IEnumerable<Domain.Rewards>> operationResult)
    {
        // Arrange
        storage.Search(
            Arg.Any<System.Linq.Expressions.Expression<Func<Domain.Rewards, bool>>>(),
            Arg.Any<string>(),
            Arg.Any<int>(),
            Arg.Any<int>())
        .Returns(operationResult);

        var handler = new SearchRewardsQueryHandler(storage);

        // Act
        var result = await handler.ExecuteAsync(criteria);

        // Assert
        await storage.Received(1).Search(
            Arg.Any<System.Linq.Expressions.Expression<Func<Domain.Rewards, bool>>>(),
            Arg.Any<string>(),
            Arg.Any<int>(),
            Arg.Any<int>());

        result.ShouldBeOfType<SearchRewardsResult>();
    }

    [Fact]
    public async void SearchRewardsQueryHandler_ExecuteAsync_NoCriteria_ShouldThrow()
    {
        // Arrange
        var handler = new SearchRewardsQueryHandler(storage);

        // Act
        // Assert
        await Should.ThrowAsync<ArgumentException>(async () => await handler.ExecuteAsync(null));
    }

    [Theory, AutoData]
    public async void SearchRewardsQueryHandler_ExecuteAsync_NotSuccessful(SearchRewards criteria)
    {
        // Arrange
        var operationResult = new OperationResult<IEnumerable<Domain.Rewards>>(false, null, null);

        storage.Search(
            Arg.Any<System.Linq.Expressions.Expression<Func<Domain.Rewards, bool>>>(),
            Arg.Any<string>(),
            Arg.Any<int>(),
            Arg.Any<int>())
        .Returns(operationResult);

        var handler = new SearchRewardsQueryHandler(storage);

        // Act
        var result = await handler.ExecuteAsync(criteria);

        // Assert
        await storage.Received(1).Search(
            Arg.Any<System.Linq.Expressions.Expression<Func<Domain.Rewards, bool>>>(),
            Arg.Any<string>(),
            Arg.Any<int>(),
            Arg.Any<int>());

        result.ShouldBeOfType<SearchRewardsResult>();
        result.Results.ShouldBeNull();
    }

    #endregion
}
