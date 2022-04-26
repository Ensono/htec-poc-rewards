using System;
using System.Collections.Generic;
using HTEC.POC.API.FunctionalTests.Models;

namespace HTEC.POC.API.FunctionalTests.Builders;

public class RewardsBuilder : IBuilder<Rewards>
{
    private Rewards rewards;

    public RewardsBuilder()
    {
        rewards = new Rewards();
    }


    public RewardsBuilder SetDefaultValues(string name)
    {
        var categoryBuilder = new CategoryBuilder();

        rewards.id = Guid.NewGuid().ToString();
        rewards.name = name;
        rewards.description = "Default test rewards description";
        rewards.enabled = true;
        rewards.categories = new List<Category>()
        {
            categoryBuilder.SetDefaultValues("Burgers")
                .Build()
        };

        return this;
    }

    public RewardsBuilder WithId(Guid id)
    {
        rewards.id = id.ToString();
        return this;
    }

    public RewardsBuilder WithName(string name)
    {
        rewards.name = name;
        return this;
    }

    public RewardsBuilder WithDescription(string description)
    {
        rewards.description = description;
        return this;
    }

    public RewardsBuilder WithNoCategories()
    {
        rewards.categories = new List<Category>();
        return this;
    }

    public RewardsBuilder WithCategories(List<Category> categories)
    {
        rewards.categories = categories;
        return this;
    }

    public RewardsBuilder SetEnabled(bool enabled)
    {
        rewards.enabled = enabled;
        return this;
    }

    public Rewards Build()
    {
        return rewards;
    }
}