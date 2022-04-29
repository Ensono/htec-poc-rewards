using System;
using System.Linq;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Commands;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;
using Htec.Poc.API.Controllers;
using Htec.Poc.API.Models.Requests;
using Htec.Poc.CQRS.Commands;

namespace Htec.Poc.API.UnitTests.Controllers;

public class AddRewardItemControllerTests
{
    [Fact]
    public void AddRewardItemController_Should_BeDerivedFrom_ApiControllerBase()
    {
        // Arrange
        // Act
        // Assert
        typeof(AddRewardItemController)
            .Should()
            .BeDerivedFrom<ApiControllerBase>();
    }

    [Fact]
    public void Constructor_Should_ThrowArgumentNullException_When_ICommandHandlerIsNull()
    {
        // Arrange
        // Act
        Action action = () => new AddRewardItemController(null);

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
        Action action = () => new AddRewardItemController(Substitute.For<ICommandHandler<CreateRewardItem, Guid>>());

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
        typeof(AddRewardItemController)
            .Should()
            .BeDecoratedWith<ConsumesAttribute>();
    }

    [Fact]
    public void Constructor_Should_BeDecoratedWith_ProducesAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(AddRewardItemController)
            .Should()
            .BeDecoratedWith<ProducesAttribute>();
    }

    [Fact]
    public void Constructor_Should_BeDecoratedWith_ApiExplorerSettingsAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(AddRewardItemController)
            .Should()
            .BeDecoratedWith<ApiExplorerSettingsAttribute>();
    }

    [Fact]
    public void CreateRewardItem_Should_BeDecoratedWith_HttpPostAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(AddRewardItemController)
            .Methods()
            .First(x => x.Name == "AddRewardItem")
            .Should()
            .BeDecoratedWith<HttpPostAttribute>(attribute => attribute.Template == "/v1/reward/{id}/category/{categoryId}/items/");
    }

    [Fact]
    public void CreateRewardItem_Should_BeDecoratedWith_AuthorizeAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(AddRewardItemController)
            .Methods()
            .First(x => x.Name == "AddRewardItem")
            .Should()
            .BeDecoratedWith<AuthorizeAttribute>();
    }

    [Fact]
    public async Task CreateRewardItem_Should_Return_StatusCreated()
    {
        // Arrange
        var fakeCommandHandler = Substitute.For<ICommandHandler<CreateRewardItem, Guid>>();
        var correlationId = Guid.NewGuid();
        fakeCommandHandler.HandleAsync(Arg.Any<CreateRewardItem>()).Returns(Task.FromResult(Guid.Empty));

        var body = new CreateItemRequest
        {
            Name = "testName",
            Description = "testDescription"
        };

        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["x-correlation-id"] = correlationId.ToString();

        var sut = new AddRewardItemController(fakeCommandHandler)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            }
        };

        // Act
        var result = await sut.AddRewardItem(Guid.Empty, Guid.Empty, body);

        // Assert
        await fakeCommandHandler.Received().HandleAsync(Arg.Any<CreateRewardItem>());

        result
            .Should()
            .BeOfType<ObjectResult>()
            .Which
            .StatusCode
            .Should()
            .Be(201);
    }
}
