﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentAssertions;
using Xunit;
using HTEC.POC.API.Models.Requests;

namespace HTEC.POC.API.UnitTests.Models;

public class UpdateRewardsRequestTests
{
    [Fact]
    public void Name_Should_BeDecoratedWith_RequiredAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(UpdateRewardsRequest)
            .Properties()
            .First(x => x.Name == "Name")
            .Should()
            .BeDecoratedWith<RequiredAttribute>();
    }

    [Fact]
    public void Name_Should_ReturnString()
    {
        // Arrange
        // Act
        // Assert
        typeof(UpdateRewardsRequest)
            .Properties()
            .First(x => x.Name == "Name")
            .Should()
            .Return<string>();
    }

    [Fact]
    public void Description_Should_ReturnString()
    {
        // Arrange
        // Act
        // Assert
        typeof(UpdateRewardsRequest)
            .Properties()
            .First(x => x.Name == "Description")
            .Should()
            .Return<string>();
    }

    [Fact]
    public void TenantId_Should_BeDecoratedWith_RequiredAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(UpdateRewardsRequest)
            .Properties()
            .First(x => x.Name == "RestaurantId")
            .Should()
            .BeDecoratedWith<RequiredAttribute>();
    }

    [Fact]
    public void TenantId_Should_ReturnGuid()
    {
        // Arrange
        // Act
        // Assert
        typeof(UpdateRewardsRequest)
            .Properties()
            .First(x => x.Name == "RestaurantId")
            .Should()
            .Return<Guid>();
    }

    [Fact]
    public void Enabled_Should_BeDecoratedWith_RequiredAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(UpdateRewardsRequest)
            .Properties()
            .First(x => x.Name == "Enabled")
            .Should()
            .BeDecoratedWith<RequiredAttribute>();
    }

    [Fact]
    public void Enabled_Should_ReturnBool()
    {
        // Arrange
        // Act
        // Assert
        typeof(UpdateRewardsRequest)
            .Properties()
            .First(x => x.Name == "Enabled")
            .Should()
            .Return<bool>();
    }
}
