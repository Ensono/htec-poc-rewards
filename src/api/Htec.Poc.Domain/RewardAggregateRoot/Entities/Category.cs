using System;
using System.Collections.Generic;
using System.Linq;
using Amido.Stacks.Domain;
using Newtonsoft.Json;
using Htec.Poc.Domain.RewardAggregateRoot.Exceptions;

namespace Htec.Poc.Domain.Entities;

public class Category : IEntity<Guid>
{
    public Category(Guid id, string name, string description, List<RewardItem> items = null)
    {
        Id = id;
        Name = name;
        Description = description;
        this.items = items ?? new List<RewardItem>();
    }

    [JsonProperty("Items")]
    private List<RewardItem> items;


    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    [JsonIgnore]
    public IReadOnlyList<RewardItem> Items { get => items?.AsReadOnly(); }


    internal void Update(string name, string description)
    {
        this.Name = name;
        this.Description = description;
    }

    internal void AddRewardItem(RewardItem item)
    {
        if (item.Price == 0)
            RewardItemPriceMustNotBeZeroException.Raise(item.Name);

        if (items.Any(i => i.Name == Name))
            RewardItemAlreadyExistsException.Raise(Id, item.Name);

        items.Add(item);
    }

    internal void RemoveRewardItem(Guid rewardItemId)
    {
        var item = GetRewardItem(rewardItemId);
        items.Remove(item);
    }

    internal void UpdateRewardItem(RewardItem rewardItem)
    {
        var item = GetRewardItem(rewardItem.Id);

        item.Name = rewardItem.Name;
        item.Description = rewardItem.Description;
        item.Price = rewardItem.Price;
        item.Available = rewardItem.Available;
    }

    private RewardItem GetRewardItem(Guid rewardItemId)
    {
        var item = items.SingleOrDefault(i => i.Id == rewardItemId);

        if (item == null)
            RewardItemDoesNotExistException.Raise(Id, rewardItemId);

        return item;
    }
}
