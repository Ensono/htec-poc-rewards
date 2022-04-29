using System;
using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Htec.Poc.API.Controllers;

namespace Htec.Poc.API.UnitTests.Controllers;

public class GetRewardByIdV2ControllerTests
{
    [Fact]
    public void GetRewardByIdV2Controller_Should_BeDerivedFrom_ApiControllerBase()
    {
        // Arrange
        // Act
        // Assert
        typeof(GetRewardByIdV2Controller)
            .Should()
            .BeDerivedFrom<ApiControllerBase>();
    }

    [Fact]
    public void Constructor_Should_BeDecoratedWith_ConsumesAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(GetRewardByIdV2Controller)
            .Should()
            .BeDecoratedWith<ConsumesAttribute>();
    }

    [Fact]
    public void Constructor_Should_BeDecoratedWith_ProducesAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(GetRewardByIdV2Controller)
            .Should()
            .BeDecoratedWith<ProducesAttribute>();
    }

    [Fact]
    public void Constructor_Should_BeDecoratedWith_ApiExplorerSettingsAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(GetRewardByIdV2Controller)
            .Should()
            .BeDecoratedWith<ApiExplorerSettingsAttribute>();
    }

    [Fact]
    public void Constructor_Should_BeDecoratedWith_ApiControllerAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(GetRewardByIdV2Controller)
            .Should()
            .BeDecoratedWith<ApiControllerAttribute>();
    }

    [Fact]
    public void GetRewardV2_Should_BeDecoratedWith_HttpGetAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(GetRewardByIdV2Controller)
            .Methods()
            .First(x => x.Name == "GetRewardV2")
            .Should()
            .BeDecoratedWith<HttpGetAttribute>(attribute => attribute.Template == "/v2/reward/{id}");
    }

    [Fact]
    public void GetRewardV2_Should_BeDecoratedWith_AuthorizeAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(GetRewardByIdV2Controller)
            .Methods()
            .First(x => x.Name == "GetRewardV2")
            .Should()
            .BeDecoratedWith<AuthorizeAttribute>();
    }

    [Fact]
    public void GetRewardV2_Should_BeDecoratedWith_ProducesResponseTypeAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(GetRewardByIdV2Controller)
            .Methods()
            .First(x => x.Name == "GetRewardV2")
            .Should()
            .BeDecoratedWith<ProducesResponseTypeAttribute>();
    }

    [Fact]
    public void GetRewardV2_Should_Return_StatusCodeNoContent()
    {
        // Arrange
        var sut = new GetRewardByIdV2Controller();

        // Act
        var result = sut.GetRewardV2(Guid.Empty);

        // Assert
        result
            .Should()
            .BeOfType<BadRequestResult>();
    }
}
