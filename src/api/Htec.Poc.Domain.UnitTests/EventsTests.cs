using Amido.Stacks.Domain.Events;
using FluentAssertions;
using Xunit;
using Htec.Poc.Domain.Events;

namespace Htec.Poc.Domain.UnitTests;

public class EventsTests
{
    [Fact]
    public void CategoryChanged()
    {
        // Arrange
        // Act
        // Assert
        typeof(CategoryChanged).Should().Implement<IDomainEvent>();
    }

    [Fact]
    public void CategoryCreated()
    {
        // Arrange
        // Act
        // Assert
        typeof(CategoryCreated).Should().Implement<IDomainEvent>();
    }

    [Fact]
    public void CategoryRemoved()
    {
        // Arrange
        // Act
        // Assert
        typeof(CategoryRemoved).Should().Implement<IDomainEvent>();
    }

    [Fact]
    public void RewardChanged()
    {
        // Arrange
        // Act
        // Assert
        typeof(RewardChanged).Should().Implement<IDomainEvent>();
    }

    [Fact]
    public void RewardCreated()
    {
        // Arrange
        // Act
        // Assert
        typeof(RewardCreated).Should().Implement<IDomainEvent>();
    }

    [Fact]
    public void RewardItemChanged()
    {
        // Arrange
        // Act
        // Assert
        typeof(RewardItemChanged).Should().Implement<IDomainEvent>();
    }

    [Fact]
    public void RewardItemCreated()
    {
        // Arrange
        // Act
        // Assert
        typeof(RewardItemCreated).Should().Implement<IDomainEvent>();
    }

    [Fact]
    public void RewardItemRemoved()
    {
        // Arrange
        // Act
        // Assert
        typeof(RewardItemRemoved).Should().Implement<IDomainEvent>();
    }
}
