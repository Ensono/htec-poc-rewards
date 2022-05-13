using System;
using System.Linq;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace Htec.Poc.Domain.UnitTests;

[Trait("TestType", "UnitTests")]
public class RewardTests
{
    [Theory, AutoData]
    public void Constructor(
        string name,
        string description,
        bool enabled)
    {
        // Arrange
        // Act
        var reward = new Reward(Guid.Empty, name, Guid.Empty, description, enabled);

        // Assert
        reward.Name.Should().Be(name);
        reward.Description.Should().Be(description);
        reward.Enabled.Should().Be(enabled);
    }
    
    [Theory, AutoData]
    public void Update(
        Reward reward,
        string name,
        string description,
        bool enabled)
    {
        // Arrange
        // Act
        reward.Update(name, description, enabled);

        // Assert
        reward.Name.Should().Be(name);
        reward.Description.Should().Be(description);
        reward.Enabled.Should().Be(enabled);
    }    
}