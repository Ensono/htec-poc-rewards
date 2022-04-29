using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amido.Stacks.Domain;
using Htec.Poc.Domain.Entities;
using Htec.Poc.Domain.Events;
using Htec.Poc.Domain.RewardAggregateRoot.Exceptions;
using Amido.Stacks.Domain.Events;
using Amido.Stacks.DynamoDB.Converters;
using Htec.Poc.Domain.Converters;

namespace Htec.Poc.Domain;

public class Reward : AggregateRoot<Guid>
{
    [JsonProperty("Categories")]
    private List<Category> categories;

    public Reward(Guid id, string name, Guid tenantId, string description, bool enabled, List<Category> categories = null)
    {
        Id = id;
        Name = name;
        TenantId = tenantId;
        Description = description;
        this.categories = categories ?? new List<Category>();
        Enabled = enabled;
    }

    public string Name { get; private set; }

    public Guid TenantId { get; private set; }

    public string Description { get; private set; }

    [JsonIgnore]
    public IReadOnlyList<Category> Categories { get => categories?.AsReadOnly(); private set => categories = value.ToList(); }

    public bool Enabled { get; private set; }

    public void Update(string name, string description, bool enabled)
    {
        this.Name = name;
        this.Description = description;
        this.Enabled = enabled;

        Emit(new RewardChanged());
    }

    public void AddCategory(Guid categoryId, string name, string description)
    {
        if (categories.Any(c => c.Name == name))
            CategoryAlreadyExistsException.Raise(Id, name);

        categories.Add(new Category(categoryId, name, description));

        Emit(new CategoryCreated());
    }

    public void UpdateCategory(Guid categoryId, string name, string description)
    {
        var category = GetCategory(categoryId);

        category.Update(name, description);

        Emit(new CategoryChanged());
    }

    public void RemoveCategory(Guid categoryId)
    {
        var category = GetCategory(categoryId);

        categories.Remove(category);

        Emit(new CategoryRemoved());
    }

    public void AddRewardItem(
        Guid categoryId,
        Guid rewardItemId,
        string name,
        string description,
        double price,
        bool available)
    {
        var category = GetCategory(categoryId);

        category.AddRewardItem(
            new RewardItem(
                rewardItemId,
                name,
                description,
                price,
                available));

        Emit(new RewardItemCreated());
    }

    public void UpdateRewardItem(
        Guid categoryId,
        Guid rewardItemId,
        string name,
        string description,
        double price,
        bool available)
    {
        var category = GetCategory(categoryId);

        category.UpdateRewardItem(
            new RewardItem(
                rewardItemId,
                name,
                description,
                price,
                available));

        Emit(new RewardItemChanged());
    }

    public void RemoveRewardItem(Guid categoryId, Guid rewardItemId)
    {
        var category = GetCategory(categoryId);

        category.RemoveRewardItem(rewardItemId);

        Emit(new RewardItemRemoved());
    }

    private Category GetCategory(Guid categoryId)
    {
        var category = categories.SingleOrDefault(c => c.Id == categoryId);

        if (category == null)
            CategoryDoesNotExistException.Raise(Id, categoryId);

        return category;
    }
}

