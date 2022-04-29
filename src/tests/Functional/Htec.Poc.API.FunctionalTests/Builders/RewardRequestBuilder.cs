using Htec.Poc.API.FunctionalTests.Models;

namespace Htec.Poc.API.FunctionalTests.Builders;

public class RewardRequestBuilder : IBuilder<RewardRequest>
{
    private RewardRequest reward;

    public RewardRequestBuilder()
    {
        reward = new RewardRequest();
    }

    public RewardRequestBuilder SetDefaultValues(string name)
    {
        reward.name = name;
        reward.description = "Updated reward description";
        reward.enabled = true;
        return this;
    }

    public RewardRequestBuilder WithName(string name)
    {
        reward.name = name;
        return this;
    }

    public RewardRequestBuilder WithDescription(string description)
    {
        reward.description = description;
        return this;
    }

    public RewardRequestBuilder SetEnabled(bool enabled)
    {
        reward.enabled = enabled;
        return this;
    }

    public RewardRequest Build()
    {
        return reward;
    }
}