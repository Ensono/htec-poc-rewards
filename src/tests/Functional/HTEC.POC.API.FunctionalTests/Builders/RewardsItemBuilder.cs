using HTEC.POC.API.FunctionalTests.Models;

namespace HTEC.POC.API.FunctionalTests.Builders;

public class RewardsItemBuilder : IBuilder<RewardsItemRequest>
{
    private readonly RewardsItemRequest rewardsItem;

    public RewardsItemBuilder()
    {
        rewardsItem = new RewardsItemRequest();
    }

    public RewardsItemBuilder WithName(string name)
    {
        rewardsItem.name = name;
        return this;
    }

    public RewardsItemBuilder WithDescription(string description)
    {
        rewardsItem.description = description;
        return this;
    }

    public RewardsItemBuilder WithPrice(double price)
    {
        rewardsItem.price = price;
        return this;
    }

    public RewardsItemBuilder WithAvailablity(bool available)
    {
        rewardsItem.available = available;
        return this;
    }

    public RewardsItemRequest Build()
    {
        return rewardsItem;
    }
        
    public RewardsItemBuilder SetDefaultValues(string name)
    {
        rewardsItem.name = name;
        rewardsItem.description = "Item description";
        rewardsItem.price = 12.50;
        rewardsItem.available = true;
        return this;
    }
}