using System;
using System.Collections.Generic;
using Htec.Poc.API.FunctionalTests.Models;

namespace Htec.Poc.API.FunctionalTests.Builders;

public class RewardBuilder : IBuilder<Reward>
{
    private Reward reward;

    public RewardBuilder()
    {
        reward = new Reward();
    }


    public RewardBuilder SetDefaultValues(string name)
    {
        var categoryBuilder = new CategoryBuilder();

        reward.id = Guid.NewGuid().ToString();
        reward.name = name;
        reward.description = "Default test reward description";
        reward.enabled = true;
        reward.categories = new List<Category>()
        {
            categoryBuilder.SetDefaultValues("Burgers")
                .Build()
        };

        return this;
    }

    public RewardBuilder WithId(Guid id)
    {
        reward.id = id.ToString();
        return this;
    }

    public RewardBuilder WithName(string name)
    {
        reward.name = name;
        return this;
    }

    public RewardBuilder WithDescription(string description)
    {
        reward.description = description;
        return this;
    }

    public RewardBuilder WithNoCategories()
    {
        reward.categories = new List<Category>();
        return this;
    }

    public RewardBuilder WithCategories(List<Category> categories)
    {
        reward.categories = categories;
        return this;
    }

    public RewardBuilder SetEnabled(bool enabled)
    {
        reward.enabled = enabled;
        return this;
    }

    public Reward Build()
    {
        return reward;
    }
}