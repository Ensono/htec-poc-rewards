using System;
using System.Linq;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace HTEC.POC.Domain.UnitTests;

[Trait("TestType", "UnitTests")]
public class RewardsTests
{
    [Theory, AutoData]
    public void Constructor(
        string name,
        string description,
        bool enabled)
    {
        // Arrange
        // Act
        var rewards = new Rewards(Guid.Empty, name, Guid.Empty, description, enabled);

        // Assert
        rewards.Name.Should().Be(name);
        rewards.Description.Should().Be(description);
        rewards.Enabled.Should().Be(enabled);
    }
    
    [Theory, AutoData]
    public void Update(
        Rewards rewards,
        string name,
        string description,
        bool enabled)
    {
        // Arrange
        // Act
        rewards.Update(name, description, enabled);

        // Assert
        rewards.Name.Should().Be(name);
        rewards.Description.Should().Be(description);
        rewards.Enabled.Should().Be(enabled);
    }    
    
    [Theory, AutoData]
    public void AddCategory(
        Rewards rewards,
        Guid categoryId,
        string name,
        string description)
    {
        // Arrange
        // Act
        rewards.AddCategory(categoryId, name, description);

        // Assert
        rewards.Categories.Last().Id.Should().Be(categoryId);
        rewards.Categories.Last().Name.Should().Be(name);
        rewards.Categories.Last().Description.Should().Be(description);
    }

    [Theory, AutoData]
    public void UpdateCategory(
        Rewards rewards,
        Guid categoryId,
        string name,
        string description,
        string updatedName,
        string updatedDescription)
    {
        // Arrange
        rewards.AddCategory(categoryId, name, description);

        // Act
        rewards.UpdateCategory(categoryId, updatedName, updatedDescription);

        // Assert
        rewards.Categories.Last().Id.Should().Be(categoryId);
        rewards.Categories.Last().Name.Should().Be(updatedName);
        rewards.Categories.Last().Description.Should().Be(updatedDescription);
    }

    [Theory, AutoData]
    public void RemoveCategory(
        Rewards rewards,
        Guid categoryId,
        string name,
        string description)
    {
        // Arrange
        rewards.AddCategory(categoryId, name, description);
        var categoriesLength = rewards.Categories.Count;
        
        // Assert
        rewards.Categories.Should().Contain(category => category.Id == categoryId);

        // Act
        rewards.RemoveCategory(categoryId);

        // Assert
        rewards.Categories.Should().NotContain(category => category.Id == categoryId);
        rewards.Categories.Count.Should().Be(categoriesLength - 1);
    }
    
    [Theory, AutoData]
    public void AddRewardsItem(
        Rewards rewards,
        Guid categoryId,
        string categoryName,
        string categoryDescription,
        Guid rewardsItemId,
        string rewardsItemName,
        string rewardsItemDescription,
        double rewardsItemPrice,
        bool rewardsItemAvailable)
    {
        // Arrange
        rewards.AddCategory(categoryId, categoryName, categoryDescription);

        // Act
        rewards.AddRewardsItem(categoryId, rewardsItemId, rewardsItemName, rewardsItemDescription, rewardsItemPrice, rewardsItemAvailable);
        
        // Assert
        rewards
            .Categories
            .Last()
            .Items
            .Should()
            .Contain(rewardsItem => rewardsItem.Id == rewardsItemId && 
                                 rewardsItem.Name == rewardsItemName &&
                                 rewardsItem.Description == rewardsItemDescription &&
                                 rewardsItem.Price == rewardsItemPrice &&
                                 rewardsItem.Available == rewardsItemAvailable);
    }

    [Theory, AutoData]
    public void UpdateRewardsItem(
        Rewards rewards,
        Guid categoryId,
        string categoryName,
        string categoryDescription,
        Guid rewardsItemId,
        string rewardsItemName,
        string rewardsItemDescription,
        double rewardsItemPrice,
        bool rewardsItemAvailable,
        string updatedRewardsItemName,
        string updatedRewardsItemDescription,
        double updatedRewardsItemPrice,
        bool updatedRewardsItemAvailable)
    {
        // Arrange
        rewards.AddCategory(categoryId, categoryName, categoryDescription);
        rewards.AddRewardsItem(categoryId, rewardsItemId, rewardsItemName, rewardsItemDescription, rewardsItemPrice, rewardsItemAvailable);

        // Act
        rewards.UpdateRewardsItem(categoryId, rewardsItemId, updatedRewardsItemName, updatedRewardsItemDescription, updatedRewardsItemPrice, updatedRewardsItemAvailable);
        
        // Assert
        rewards
            .Categories
            .Last()
            .Items
            .Should()
            .NotContain(rewardsItem => rewardsItem.Id == rewardsItemId && 
                                    rewardsItem.Name == rewardsItemName && 
                                    rewardsItem.Description == rewardsItemDescription && 
                                    rewardsItem.Price == rewardsItemPrice && 
                                    rewardsItem.Available == rewardsItemAvailable);
        
        rewards
            .Categories
            .Last()
            .Items
            .Should()
            .Contain(rewardsItem => rewardsItem.Id == rewardsItemId && 
                                 rewardsItem.Name == updatedRewardsItemName && 
                                 rewardsItem.Description == updatedRewardsItemDescription && 
                                 rewardsItem.Price == updatedRewardsItemPrice && 
                                 rewardsItem.Available == updatedRewardsItemAvailable);
    }

    [Theory, AutoData]
    public void RemoveRewardsItem(
        Rewards rewards,
        Guid categoryId,
        string categoryName,
        string categoryDescription,
        Guid rewardsItemId,
        string rewardsItemName,
        string rewardsItemDescription,
        double rewardsItemPrice,
        bool rewardsItemAvailable)
    {
        // Arrange
        rewards.AddCategory(categoryId, categoryName, categoryDescription);
        rewards.AddRewardsItem(categoryId, rewardsItemId, rewardsItemName, rewardsItemDescription, rewardsItemPrice, rewardsItemAvailable);

        // Assert
        rewards
            .Categories
            .Last()
            .Items
            .Should()
            .Contain(rewardsItem => rewardsItem.Id == rewardsItemId &&
                                 rewardsItem.Name == rewardsItemName &&
                                 rewardsItem.Description == rewardsItemDescription &&
                                 rewardsItem.Price == rewardsItemPrice &&
                                 rewardsItem.Available == rewardsItemAvailable);

        // Act
        rewards.RemoveRewardsItem(categoryId, rewardsItemId);

        // Assert
        rewards
            .Categories
            .Last()
            .Items
            .Should()
            .NotContain(rewardsItem => rewardsItem.Id == rewardsItemId &&
                                    rewardsItem.Name == rewardsItemName &&
                                    rewardsItem.Description == rewardsItemDescription &&
                                    rewardsItem.Price == rewardsItemPrice &&
                                    rewardsItem.Available == rewardsItemAvailable);
    }
}
