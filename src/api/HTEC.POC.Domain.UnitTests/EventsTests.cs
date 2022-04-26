using Amido.Stacks.Domain.Events;
using FluentAssertions;
using Xunit;
using HTEC.POC.Domain.Events;

namespace HTEC.POC.Domain.UnitTests;

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
    public void RewardsChanged()
    {
        // Arrange
        // Act
        // Assert
        typeof(RewardsChanged).Should().Implement<IDomainEvent>();
    }

    [Fact]
    public void RewardsCreated()
    {
        // Arrange
        // Act
        // Assert
        typeof(RewardsCreated).Should().Implement<IDomainEvent>();
    }

    [Fact]
    public void RewardsItemChanged()
    {
        // Arrange
        // Act
        // Assert
        typeof(RewardsItemChanged).Should().Implement<IDomainEvent>();
    }

    [Fact]
    public void RewardsItemCreated()
    {
        // Arrange
        // Act
        // Assert
        typeof(RewardsItemCreated).Should().Implement<IDomainEvent>();
    }

    [Fact]
    public void RewardsItemRemoved()
    {
        // Arrange
        // Act
        // Assert
        typeof(RewardsItemRemoved).Should().Implement<IDomainEvent>();
    }
}
