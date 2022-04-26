using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;
using HTEC.POC.Domain.Entities;

namespace HTEC.POC.Domain.UnitTests;

[Trait("TestType", "UnitTests")]
public class RewardsItemTests
{
    [Theory, AutoData]
    public void Constructor(
        Guid categoryId,
        string name,
        string description,
        double price,
        bool available)
    {
        // Arrange
        // Act
        var rewardsItem = new RewardsItem(categoryId, name, description, price, available);

        // Assert
        rewardsItem.Id.Should().Be(categoryId);
        rewardsItem.Name.Should().Be(name);
        rewardsItem.Description.Should().Be(description);
        rewardsItem.Price.Should().Be(price);
        rewardsItem.Available.Should().Be(available);
    }
}
