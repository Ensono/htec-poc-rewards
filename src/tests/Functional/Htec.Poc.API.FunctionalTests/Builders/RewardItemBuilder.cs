using Htec.Poc.API.FunctionalTests.Models;

namespace Htec.Poc.API.FunctionalTests.Builders;

public class RewardItemBuilder : IBuilder<RewardItemRequest>
{
    private readonly RewardItemRequest rewardItem;

    public RewardItemBuilder()
    {
        rewardItem = new RewardItemRequest();
    }

    public RewardItemBuilder WithName(string name)
    {
        rewardItem.name = name;
        return this;
    }

    public RewardItemBuilder WithDescription(string description)
    {
        rewardItem.description = description;
        return this;
    }

    public RewardItemBuilder WithPrice(double price)
    {
        rewardItem.price = price;
        return this;
    }

    public RewardItemBuilder WithAvailablity(bool available)
    {
        rewardItem.available = available;
        return this;
    }

    public RewardItemRequest Build()
    {
        return rewardItem;
    }
        
    public RewardItemBuilder SetDefaultValues(string name)
    {
        rewardItem.name = name;
        rewardItem.description = "Item description";
        rewardItem.price = 12.50;
        rewardItem.available = true;
        return this;
    }
}