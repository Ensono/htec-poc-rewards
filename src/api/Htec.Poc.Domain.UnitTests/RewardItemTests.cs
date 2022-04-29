using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;
using Htec.Poc.Domain.Entities;

namespace Htec.Poc.Domain.UnitTests;

[Trait("TestType", "UnitTests")]
public class RewardItemTests
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
        var rewardItem = new RewardItem(categoryId, name, description, price, available);

        // Assert
        rewardItem.Id.Should().Be(categoryId);
        rewardItem.Name.Should().Be(name);
        rewardItem.Description.Should().Be(description);
        rewardItem.Price.Should().Be(price);
        rewardItem.Available.Should().Be(available);
    }
}
