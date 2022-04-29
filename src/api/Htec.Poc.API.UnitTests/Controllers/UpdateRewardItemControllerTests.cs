using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.Model;
using Amido.Stacks.Application.CQRS.Commands;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;
using Htec.Poc.API.Controllers;
using Htec.Poc.CQRS.Commands;

namespace Htec.Poc.API.UnitTests.Controllers;

public class UpdateRewardItemControllerTests
{
    [Fact]
    public void UpdateRewardItemController_Should_BeDerivedFrom_ApiControllerBase()
    {
        // Arrange
        // Act
        // Assert
        typeof(UpdateRewardItemController)
            .Should()
            .BeDerivedFrom<ApiControllerBase>();
    }

    [Fact]
    public void Constructor_Should_ThrowArgumentNullException_When_ICommandHandlerIsNull()
    {
        // Arrange
        // Act
        Action action = () => new UpdateRewardItemController(null);

        // Assert
        action
            .Should()
            .Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'commandHandler')");
    }

    [Fact]
    public void Constructor_Should_Not_Throw_When_ICommandHandlerIsPresent()
    {
        // Arrange
        // Act
        Action action = () => new UpdateRewardItemController(Substitute.For<ICommandHandler<UpdateRewardItem, bool>>());

        // Assert
        action
            .Should()
            .NotThrow();
    }

    [Fact]
    public void Constructor_Should_BeDecoratedWith_ConsumesAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(UpdateRewardItemController)
            .Should()
            .BeDecoratedWith<ConsumesAttribute>();
    }

    [Fact]
    public void Constructor_Should_BeDecoratedWith_ProducesAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(UpdateRewardItemController)
            .Should()
            .BeDecoratedWith<ProducesAttribute>();
    }

    [Fact]
    public void Constructor_Should_BeDecoratedWith_ApiExplorerSettingsAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(UpdateRewardItemController)
            .Should()
            .BeDecoratedWith<ApiExplorerSettingsAttribute>();
    }

    [Fact]
    public void UpdateRewardItem_Should_BeDecoratedWith_HttpPutAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(UpdateRewardItemController)
            .Methods()
            .First(x => x.Name == "UpdateRewardItem")
            .Should()
            .BeDecoratedWith<HttpPutAttribute>(attribute => attribute.Template == "/v1/reward/{id}/category/{categoryId}/items/{itemId}");
    }

    [Fact]
    public void UpdateRewardItem_Should_BeDecoratedWith_AuthorizeAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(UpdateRewardItemController)
            .Methods()
            .First(x => x.Name == "UpdateRewardItem")
            .Should()
            .BeDecoratedWith<AuthorizeAttribute>();
    }
}
