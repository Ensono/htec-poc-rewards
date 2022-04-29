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
using Htec.Poc.CQRS.Commands;

namespace Htec.Poc.API.UnitTests.Controllers;

public class DeleteRewardItemControllerTests
{
    [Fact]
    public void DeleteRewardItemController_Should_BeDerivedFrom_ApiControllerBase()
    {
        // Arrange
        // Act
        // Assert
        typeof(DeleteRewardItemController)
            .Should()
            .BeDerivedFrom<ApiControllerBase>();
    }

    [Fact]
    public void Constructor_Should_ThrowArgumentNullException_When_ICommandHandlerIsNull()
    {
        // Arrange
        // Act
        Action action = () => new DeleteRewardItemController(null);

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
        Action action = () => new DeleteRewardItemController(Substitute.For<ICommandHandler<DeleteRewardItem, bool>>());

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
        typeof(DeleteRewardItemController)
            .Should()
            .BeDecoratedWith<ConsumesAttribute>();
    }

    [Fact]
    public void Constructor_Should_BeDecoratedWith_ProducesAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(DeleteRewardItemController)
            .Should()
            .BeDecoratedWith<ProducesAttribute>();
    }

    [Fact]
    public void Constructor_Should_BeDecoratedWith_ApiExplorerSettingsAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(DeleteRewardItemController)
            .Should()
            .BeDecoratedWith<ApiExplorerSettingsAttribute>();
    }

    [Fact]
    public void DeleteRewardItem_Should_BeDecoratedWith_HttpPutAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(DeleteRewardItemController)
            .Methods()
            .First(x => x.Name == "DeleteRewardItem")
            .Should()
            .BeDecoratedWith<HttpDeleteAttribute>(attribute => attribute.Template == "/v1/reward/{id}/category/{categoryId}/items/{itemId}");
    }

    [Fact]
    public void DeleteRewardItem_Should_BeDecoratedWith_AuthorizeAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(DeleteRewardItemController)
            .Methods()
            .First(x => x.Name == "DeleteRewardItem")
            .Should()
            .BeDecoratedWith<AuthorizeAttribute>();
    }

    [Fact]
    public async Task DeleteRewardItem_Should_Return_StatusCodeNoContent()
    {
        // Arrange
        var fakeCommandHandler = Substitute.For<ICommandHandler<DeleteRewardItem, bool>>();
        var correlationId = Guid.NewGuid();
        fakeCommandHandler.HandleAsync(Arg.Any<DeleteRewardItem>()).Returns(Task.FromResult(true));

        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["x-correlation-id"] = correlationId.ToString();

        var sut = new DeleteRewardItemController(fakeCommandHandler)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            }
        };

        // Act
        var result = await sut.DeleteRewardItem(Guid.Empty, Guid.Empty, Guid.Empty);

        // Assert
        await fakeCommandHandler.Received().HandleAsync(Arg.Any<DeleteRewardItem>());

        result
            .Should()
            .BeOfType<StatusCodeResult>()
            .Which
            .StatusCode
            .Should()
            .Be(204);
    }
}
