using System;
using Amido.Stacks.Core.Exceptions;
using FluentAssertions;
using Xunit;
using Htec.Poc.Domain.RewardAggregateRoot.Exceptions;

namespace Htec.Poc.Domain.UnitTests;

public class ExceptionsTests
{
    [Fact]
    public void CategoryAlreadyExistsException_DerivesFrom_DomainExceptionBase()
    {
        // Arrange
        // Act
        // Assert
        typeof(CategoryAlreadyExistsException).Should().BeDerivedFrom<DomainExceptionBase>();
    }

    [Fact]
    public void CategoryAlreadyExistsException_Throws_CorrespondingExceptionWithExpectedMessage()
    {
        // Arrange
        var expectedMessage = "This is the expected message";
        // Act
        Action act = () => CategoryAlreadyExistsException.Raise(Guid.Empty, "testCategoryName", expectedMessage);

        // Assert
        act
            .Should()
            .Throw<CategoryAlreadyExistsException>()
            .WithMessage(expectedMessage);
    }

    [Fact]
    public void CategoryAlreadyExistsException_Throws_CorrespondingExceptionWithExpectedMessage_WhenMessageIsNull()
    {
        // Arrange
        // Act
        Action act = () => CategoryAlreadyExistsException.Raise(Guid.Empty, "testCategoryName", null);

        // Assert
        act
            .Should()
            .Throw<CategoryAlreadyExistsException>()
            .WithMessage("A category with name 'testCategoryName' already exists in the reward '00000000-0000-0000-0000-000000000000'.");
    }

    [Fact]
    public void CategoryDoesNotExistException_DerivesFrom_DomainExceptionBase()
    {
        // Arrange
        // Act
        // Assert
        typeof(CategoryDoesNotExistException).Should().BeDerivedFrom<DomainExceptionBase>();
    }

    [Fact]
    public void CategoryDoesNotExistException_Throws_CorrespondingExceptionWithExpectedMessage()
    {
        // Arrange
        var expectedMessage = "This is the expected message";
        // Act
        Action act = () => CategoryDoesNotExistException.Raise(Guid.Empty, Guid.Empty, expectedMessage);

        // Assert
        act
            .Should()
            .Throw<CategoryDoesNotExistException>()
            .WithMessage(expectedMessage);
    }

    [Fact]
    public void CategoryDoesNotExistException_Throws_CorrespondingExceptionWithExpectedMessage_WhenMessageIsNull()
    {
        // Arrange
        // Act
        Action act = () => CategoryDoesNotExistException.Raise(Guid.Empty, Guid.Empty, null);

        // Assert
        act
            .Should()
            .Throw<CategoryDoesNotExistException>()
            .WithMessage("A category with id '00000000-0000-0000-0000-000000000000' does not exist in the reward '00000000-0000-0000-0000-000000000000'.");
    }

    [Fact]
    public void RewardItemAlreadyExistsException_DerivesFrom_DomainExceptionBase()
    {
        // Arrange
        // Act
        // Assert
        typeof(RewardItemAlreadyExistsException).Should().BeDerivedFrom<DomainExceptionBase>();
    }

    [Fact]
    public void RewardItemAlreadyExistsException_Throws_CorrespondingExceptionWithExpectedMessage()
    {
        // Arrange
        var expectedMessage = "This is the expected message";
        // Act
        Action act = () => RewardItemAlreadyExistsException.Raise(Guid.Empty, "rewardItemName", expectedMessage);

        // Assert
        act
            .Should()
            .Throw<RewardItemAlreadyExistsException>()
            .WithMessage(expectedMessage);
    }

    [Fact]
    public void RewardItemAlreadyExistsException_Throws_CorrespondingExceptionWithExpectedMessage_WhenMessageIsNull()
    {
        // Arrange
        // Act
        Action act = () => RewardItemAlreadyExistsException.Raise(Guid.Empty, "rewardItemName", null);

        // Assert
        act
            .Should()
            .Throw<RewardItemAlreadyExistsException>()
            .WithMessage("The item rewardItemName already exist in the category '00000000-0000-0000-0000-000000000000'.");
    }

    [Fact]
    public void RewardItemDoesNotExistException_DerivesFrom_DomainExceptionBase()
    {
        // Arrange
        // Act
        // Assert
        typeof(RewardItemDoesNotExistException).Should().BeDerivedFrom<DomainExceptionBase>();
    }

    [Fact]
    public void RewardItemDoesNotExistException_Throws_CorrespondingExceptionWithExpectedMessage()
    {
        // Arrange
        var expectedMessage = "This is the expected message";
        // Act
        Action act = () => RewardItemDoesNotExistException.Raise(Guid.Empty, Guid.Empty, expectedMessage);

        // Assert
        act
            .Should()
            .Throw<RewardItemDoesNotExistException>()
            .WithMessage(expectedMessage);
    }

    [Fact]
    public void RewardItemDoesNotExistException_Throws_CorrespondingExceptionWithExpectedMessage_WhenMessageIsNull()
    {
        // Arrange
        // Act
        Action act = () => RewardItemDoesNotExistException.Raise(Guid.Empty, Guid.Empty, null);

        // Assert
        act
            .Should()
            .Throw<RewardItemDoesNotExistException>()
            .WithMessage("The item 00000000-0000-0000-0000-000000000000 does not exist in the category '00000000-0000-0000-0000-000000000000'.");
    }

    [Fact]
    public void RewardItemPriceMustNotBeZeroException_DerivesFrom_DomainExceptionBase()
    {
        // Arrange
        // Act
        // Assert
        typeof(RewardItemPriceMustNotBeZeroException).Should().BeDerivedFrom<DomainExceptionBase>();
    }

    [Fact]
    public void RewardItemPriceMustNotBeZeroException_Throws_CorrespondingExceptionWithExpectedMessage()
    {
        // Arrange
        var expectedMessage = "This is the expected message";
        // Act
        Action act = () => RewardItemPriceMustNotBeZeroException.Raise("itemName", expectedMessage);

        // Assert
        act
            .Should()
            .Throw<RewardItemPriceMustNotBeZeroException>()
            .WithMessage(expectedMessage);
    }

    [Fact]
    public void RewardItemPriceMustNotBeZeroException_Throws_CorrespondingExceptionWithExpectedMessage_WhenMessageIsNull()
    {
        // Arrange
        // Act
        Action act = () => RewardItemPriceMustNotBeZeroException.Raise("itemName", null);

        // Assert
        act
            .Should()
            .Throw<RewardItemPriceMustNotBeZeroException>()
            .WithMessage("The price for the item itemName had price as zero. A price must be provided.");
    }
}
