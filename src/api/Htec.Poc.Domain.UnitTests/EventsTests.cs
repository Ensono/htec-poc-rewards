using Amido.Stacks.Domain.Events;
using FluentAssertions;
using Xunit;
using Htec.Poc.Domain.Events;

namespace Htec.Poc.Domain.UnitTests;

public class EventsTests
{
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
}
