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

public class CreateRewardControllerTests
{
    [Fact]
    public void CreateRewardController_Should_BeDerivedFrom_ApiControllerBase()
    {
        // Arrange
        // Act
        // Assert
        typeof(CreateRewardController)
            .Should()
            .BeDerivedFrom<ApiControllerBase>();
    }

    [Fact]
    public void Constructor_Should_ThrowArgumentNullException_When_ICommandHandlerIsNull()
    {
        // Arrange
        // Act
        Action action = () => new CreateRewardController(null);

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
        Action action = () => new CreateRewardController(Substitute.For<ICommandHandler<CreateReward, Guid>>());

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
        typeof(CreateRewardController)
            .Should()
            .BeDecoratedWith<ConsumesAttribute>();
    }

    [Fact]
    public void Constructor_Should_BeDecoratedWith_ProducesAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(CreateRewardController)
            .Should()
            .BeDecoratedWith<ProducesAttribute>();
    }

    [Fact]
    public void Constructor_Should_BeDecoratedWith_ApiExplorerSettingsAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(CreateRewardController)
            .Should()
            .BeDecoratedWith<ApiExplorerSettingsAttribute>();
    }

    [Fact]
    public void CreateReward_Should_BeDecoratedWith_HttpPostAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(CreateRewardController)
            .Methods()
            .First(x => x.Name == "CreateReward")
            .Should()
            .BeDecoratedWith<HttpPostAttribute>(attribute => attribute.Template == "/v1/reward/");
    }

    [Fact]
    public void CreateReward_Should_BeDecoratedWith_AuthorizeAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(CreateRewardController)
            .Methods()
            .First(x => x.Name == "CreateReward")
            .Should()
            .BeDecoratedWith<AuthorizeAttribute>();
    }

    [Fact]
    public async Task CreateReward_Should_Return_StatusCodeNoContent()
    {
        // Arrange
        var fakeCommandHandler = Substitute.For<ICommandHandler<CreateReward, Guid>>();
        var correlationId = Guid.NewGuid();
        fakeCommandHandler.HandleAsync(Arg.Any<CreateReward>()).Returns(Task.FromResult(Guid.Empty));

        var body = new CreateRewardRequest
        {
            Name = "testName",
            Description = "testDescription"
        };

        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["x-correlation-id"] = correlationId.ToString();

        var sut = new CreateRewardController(fakeCommandHandler)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            }
        };

        // Act
        var result = await sut.CreateReward(body);

        // Assert
        await fakeCommandHandler.Received().HandleAsync(Arg.Any<CreateReward>());

        result
            .Should()
            .BeOfType<CreatedAtActionResult>()
            .Which
            .StatusCode
            .Should()
            .Be(201);
    }
}
