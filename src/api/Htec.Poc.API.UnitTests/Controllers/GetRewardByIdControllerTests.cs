using System;
using System.Linq;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Queries;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;
using Htec.Poc.API.Controllers;
using Htec.Poc.CQRS.Queries.GetRewardById;

namespace Htec.Poc.API.UnitTests.Controllers;

public class GetRewardByIdControllerTests
{
    [Fact]
    public void GetRewardByIdController_Should_BeDerivedFrom_ApiControllerBase()
    {
        // Arrange
        // Act
        // Assert
        typeof(GetRewardByIdController)
            .Should()
            .BeDerivedFrom<ApiControllerBase>();
    }

    [Fact]
    public void Constructor_Should_ThrowArgumentNullException_When_ICommandHandlerIsNull()
    {
        // Arrange
        // Act
        Action action = () => new GetRewardByIdController(null);

        // Assert
        action
            .Should()
            .Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'queryHandler')");
    }

    [Fact]
    public void Constructor_Should_Not_Throw_When_ICommandHandlerIsPresent()
    {
        // Arrange
        // Act
        Action action = () => new GetRewardByIdController(Substitute.For<IQueryHandler<GetRewardById, Reward>>());

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
        typeof(GetRewardByIdController)
            .Should()
            .BeDecoratedWith<ConsumesAttribute>();
    }

    [Fact]
    public void Constructor_Should_BeDecoratedWith_ProducesAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(GetRewardByIdController)
            .Should()
            .BeDecoratedWith<ProducesAttribute>();
    }

    [Fact]
    public void Constructor_Should_BeDecoratedWith_ApiExplorerSettingsAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(GetRewardByIdController)
            .Should()
            .BeDecoratedWith<ApiExplorerSettingsAttribute>();
    }

    [Fact]
    public void UpdateCategory_Should_BeDecoratedWith_HttpPutAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(GetRewardByIdController)
            .Methods()
            .First(x => x.Name == "GetReward")
            .Should()
            .BeDecoratedWith<HttpGetAttribute>(attribute => attribute.Template == "/v1/reward/{id}");
    }

    [Fact]
    public void UpdateCategory_Should_BeDecoratedWith_AuthorizeAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(GetRewardByIdController)
            .Methods()
            .First(x => x.Name == "GetReward")
            .Should()
            .BeDecoratedWith<AuthorizeAttribute>();
    }

    [Fact]
    public async Task UpdateCategory_Should_Return_StatusCodeNoContent()
    {
        // Arrange
        var fakeCommandHandler = Substitute.For<IQueryHandler<GetRewardById, Reward>>();
        var correlationId = Guid.NewGuid();
        fakeCommandHandler.ExecuteAsync(Arg.Any<GetRewardById>()).Returns(Task.FromResult(new Reward()));

        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["x-correlation-id"] = correlationId.ToString();

        var sut = new GetRewardByIdController(fakeCommandHandler)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            }
        };

        // Act
        var result = await sut.GetReward(Guid.Empty);

        // Assert
        await fakeCommandHandler.Received().ExecuteAsync(Arg.Any<GetRewardById>());

        result
            .Should()
            .BeOfType<ObjectResult>()
            .Which
            .Value
            .Should()
            .BeOfType<Htec.Poc.API.Models.Responses.Reward>();
    }

    [Fact]
    public async Task UpdateCategory_Should_Return_StatusCodeNotFound()
    {
        // Arrange
        var fakeCommandHandler = Substitute.For<IQueryHandler<GetRewardById, Reward>>();
        var correlationId = Guid.NewGuid();
        fakeCommandHandler.ExecuteAsync(Arg.Any<GetRewardById>())!.Returns(default(Reward));

        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["x-correlation-id"] = correlationId.ToString();

        var sut = new GetRewardByIdController(fakeCommandHandler)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            }
        };

        // Act
        var result = await sut.GetReward(Guid.Empty);

        // Assert
        await fakeCommandHandler.Received().ExecuteAsync(Arg.Any<GetRewardById>());

        result
            .Should()
            .BeOfType<NotFoundResult>();
    }
}
