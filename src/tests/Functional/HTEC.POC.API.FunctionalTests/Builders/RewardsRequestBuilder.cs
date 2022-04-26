using HTEC.POC.API.FunctionalTests.Models;

namespace HTEC.POC.API.FunctionalTests.Builders;

public class RewardsRequestBuilder : IBuilder<RewardsRequest>
{
    private RewardsRequest rewards;

    public RewardsRequestBuilder()
    {
        rewards = new RewardsRequest();
    }

    public RewardsRequestBuilder SetDefaultValues(string name)
    {
        rewards.name = name;
        rewards.description = "Updated rewards description";
        rewards.enabled = true;
        return this;
    }

    public RewardsRequestBuilder WithName(string name)
    {
        rewards.name = name;
        return this;
    }

    public RewardsRequestBuilder WithDescription(string description)
    {
        rewards.description = description;
        return this;
    }

    public RewardsRequestBuilder SetEnabled(bool enabled)
    {
        rewards.enabled = enabled;
        return this;
    }

    public RewardsRequest Build()
    {
        return rewards;
    }
}