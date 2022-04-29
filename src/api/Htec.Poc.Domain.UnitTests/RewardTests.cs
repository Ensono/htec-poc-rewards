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
    
    [Theory, AutoData]
    public void AddCategory(
        Reward reward,
        Guid categoryId,
        string name,
        string description)
    {
        // Arrange
        // Act
        reward.AddCategory(categoryId, name, description);

        // Assert
        reward.Categories.Last().Id.Should().Be(categoryId);
        reward.Categories.Last().Name.Should().Be(name);
        reward.Categories.Last().Description.Should().Be(description);
    }

    [Theory, AutoData]
    public void UpdateCategory(
        Reward reward,
        Guid categoryId,
        string name,
        string description,
        string updatedName,
        string updatedDescription)
    {
        // Arrange
        reward.AddCategory(categoryId, name, description);

        // Act
        reward.UpdateCategory(categoryId, updatedName, updatedDescription);

        // Assert
        reward.Categories.Last().Id.Should().Be(categoryId);
        reward.Categories.Last().Name.Should().Be(updatedName);
        reward.Categories.Last().Description.Should().Be(updatedDescription);
    }

    [Theory, AutoData]
    public void RemoveCategory(
        Reward reward,
        Guid categoryId,
        string name,
        string description)
    {
        // Arrange
        reward.AddCategory(categoryId, name, description);
        var categoriesLength = reward.Categories.Count;
        
        // Assert
        reward.Categories.Should().Contain(category => category.Id == categoryId);

        // Act
        reward.RemoveCategory(categoryId);

        // Assert
        reward.Categories.Should().NotContain(category => category.Id == categoryId);
        reward.Categories.Count.Should().Be(categoriesLength - 1);
    }
    
    [Theory, AutoData]
    public void AddRewardItem(
        Reward reward,
        Guid categoryId,
        string categoryName,
        string categoryDescription,
        Guid rewardItemId,
        string rewardItemName,
        string rewardItemDescription,
        double rewardItemPrice,
        bool rewardItemAvailable)
    {
        // Arrange
        reward.AddCategory(categoryId, categoryName, categoryDescription);

        // Act
        reward.AddRewardItem(categoryId, rewardItemId, rewardItemName, rewardItemDescription, rewardItemPrice, rewardItemAvailable);
        
        // Assert
        reward
            .Categories
            .Last()
            .Items
            .Should()
            .Contain(rewardItem => rewardItem.Id == rewardItemId && 
                                 rewardItem.Name == rewardItemName &&
                                 rewardItem.Description == rewardItemDescription &&
                                 rewardItem.Price == rewardItemPrice &&
                                 rewardItem.Available == rewardItemAvailable);
    }

    [Theory, AutoData]
    public void UpdateRewardItem(
        Reward reward,
        Guid categoryId,
        string categoryName,
        string categoryDescription,
        Guid rewardItemId,
        string rewardItemName,
        string rewardItemDescription,
        double rewardItemPrice,
        bool rewardItemAvailable,
        string updatedRewardItemName,
        string updatedRewardItemDescription,
        double updatedRewardItemPrice,
        bool updatedRewardItemAvailable)
    {
        // Arrange
        reward.AddCategory(categoryId, categoryName, categoryDescription);
        reward.AddRewardItem(categoryId, rewardItemId, rewardItemName, rewardItemDescription, rewardItemPrice, rewardItemAvailable);

        // Act
        reward.UpdateRewardItem(categoryId, rewardItemId, updatedRewardItemName, updatedRewardItemDescription, updatedRewardItemPrice, updatedRewardItemAvailable);
        
        // Assert
        reward
            .Categories
            .Last()
            .Items
            .Should()
            .NotContain(rewardItem => rewardItem.Id == rewardItemId && 
                                    rewardItem.Name == rewardItemName && 
                                    rewardItem.Description == rewardItemDescription && 
                                    rewardItem.Price == rewardItemPrice && 
                                    rewardItem.Available == rewardItemAvailable);
        
        reward
            .Categories
            .Last()
            .Items
            .Should()
            .Contain(rewardItem => rewardItem.Id == rewardItemId && 
                                 rewardItem.Name == updatedRewardItemName && 
                                 rewardItem.Description == updatedRewardItemDescription && 
                                 rewardItem.Price == updatedRewardItemPrice && 
                                 rewardItem.Available == updatedRewardItemAvailable);
    }

    [Theory, AutoData]
    public void RemoveRewardItem(
        Reward reward,
        Guid categoryId,
        string categoryName,
        string categoryDescription,
        Guid rewardItemId,
        string rewardItemName,
        string rewardItemDescription,
        double rewardItemPrice,
        bool rewardItemAvailable)
    {
        // Arrange
        reward.AddCategory(categoryId, categoryName, categoryDescription);
        reward.AddRewardItem(categoryId, rewardItemId, rewardItemName, rewardItemDescription, rewardItemPrice, rewardItemAvailable);

        // Assert
        reward
            .Categories
            .Last()
            .Items
            .Should()
            .Contain(rewardItem => rewardItem.Id == rewardItemId &&
                                 rewardItem.Name == rewardItemName &&
                                 rewardItem.Description == rewardItemDescription &&
                                 rewardItem.Price == rewardItemPrice &&
                                 rewardItem.Available == rewardItemAvailable);

        // Act
        reward.RemoveRewardItem(categoryId, rewardItemId);

        // Assert
        reward
            .Categories
            .Last()
            .Items
            .Should()
            .NotContain(rewardItem => rewardItem.Id == rewardItemId &&
                                    rewardItem.Name == rewardItemName &&
                                    rewardItem.Description == rewardItemDescription &&
                                    rewardItem.Price == rewardItemPrice &&
                                    rewardItem.Available == rewardItemAvailable);
    }
}
