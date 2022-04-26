using System;
using Amido.Stacks.Core.Exceptions;
using FluentAssertions;
using Xunit;
using HTEC.POC.Domain.RewardsAggregateRoot.Exceptions;

namespace HTEC.POC.Domain.UnitTests;

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
            .WithMessage("A category with name 'testCategoryName' already exists in the rewards '00000000-0000-0000-0000-000000000000'.");
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
            .WithMessage("A category with id '00000000-0000-0000-0000-000000000000' does not exist in the rewards '00000000-0000-0000-0000-000000000000'.");
    }

    [Fact]
    public void RewardsItemAlreadyExistsException_DerivesFrom_DomainExceptionBase()
    {
        // Arrange
        // Act
        // Assert
        typeof(RewardsItemAlreadyExistsException).Should().BeDerivedFrom<DomainExceptionBase>();
    }

    [Fact]
    public void RewardsItemAlreadyExistsException_Throws_CorrespondingExceptionWithExpectedMessage()
    {
        // Arrange
        var expectedMessage = "This is the expected message";
        // Act
        Action act = () => RewardsItemAlreadyExistsException.Raise(Guid.Empty, "rewardsItemName", expectedMessage);

        // Assert
        act
            .Should()
            .Throw<RewardsItemAlreadyExistsException>()
            .WithMessage(expectedMessage);
    }

    [Fact]
    public void RewardsItemAlreadyExistsException_Throws_CorrespondingExceptionWithExpectedMessage_WhenMessageIsNull()
    {
        // Arrange
        // Act
        Action act = () => RewardsItemAlreadyExistsException.Raise(Guid.Empty, "rewardsItemName", null);

        // Assert
        act
            .Should()
            .Throw<RewardsItemAlreadyExistsException>()
            .WithMessage("The item rewardsItemName already exist in the category '00000000-0000-0000-0000-000000000000'.");
    }

    [Fact]
    public void RewardsItemDoesNotExistException_DerivesFrom_DomainExceptionBase()
    {
        // Arrange
        // Act
        // Assert
        typeof(RewardsItemDoesNotExistException).Should().BeDerivedFrom<DomainExceptionBase>();
    }

    [Fact]
    public void RewardsItemDoesNotExistException_Throws_CorrespondingExceptionWithExpectedMessage()
    {
        // Arrange
        var expectedMessage = "This is the expected message";
        // Act
        Action act = () => RewardsItemDoesNotExistException.Raise(Guid.Empty, Guid.Empty, expectedMessage);

        // Assert
        act
            .Should()
            .Throw<RewardsItemDoesNotExistException>()
            .WithMessage(expectedMessage);
    }

    [Fact]
    public void RewardsItemDoesNotExistException_Throws_CorrespondingExceptionWithExpectedMessage_WhenMessageIsNull()
    {
        // Arrange
        // Act
        Action act = () => RewardsItemDoesNotExistException.Raise(Guid.Empty, Guid.Empty, null);

        // Assert
        act
            .Should()
            .Throw<RewardsItemDoesNotExistException>()
            .WithMessage("The item 00000000-0000-0000-0000-000000000000 does not exist in the category '00000000-0000-0000-0000-000000000000'.");
    }

    [Fact]
    public void RewardsItemPriceMustNotBeZeroException_DerivesFrom_DomainExceptionBase()
    {
        // Arrange
        // Act
        // Assert
        typeof(RewardsItemPriceMustNotBeZeroException).Should().BeDerivedFrom<DomainExceptionBase>();
    }

    [Fact]
    public void RewardsItemPriceMustNotBeZeroException_Throws_CorrespondingExceptionWithExpectedMessage()
    {
        // Arrange
        var expectedMessage = "This is the expected message";
        // Act
        Action act = () => RewardsItemPriceMustNotBeZeroException.Raise("itemName", expectedMessage);

        // Assert
        act
            .Should()
            .Throw<RewardsItemPriceMustNotBeZeroException>()
            .WithMessage(expectedMessage);
    }

    [Fact]
    public void RewardsItemPriceMustNotBeZeroException_Throws_CorrespondingExceptionWithExpectedMessage_WhenMessageIsNull()
    {
        // Arrange
        // Act
        Action act = () => RewardsItemPriceMustNotBeZeroException.Raise("itemName", null);

        // Assert
        act
            .Should()
            .Throw<RewardsItemPriceMustNotBeZeroException>()
            .WithMessage("The price for the item itemName had price as zero. A price must be provided.");
    }
}
