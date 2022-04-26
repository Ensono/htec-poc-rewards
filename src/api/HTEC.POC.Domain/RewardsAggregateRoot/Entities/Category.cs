using System;
using System.Collections.Generic;
using System.Linq;
using Amido.Stacks.Domain;
using Newtonsoft.Json;
using HTEC.POC.Domain.RewardsAggregateRoot.Exceptions;

namespace HTEC.POC.Domain.Entities;

public class Category : IEntity<Guid>
{
    public Category(Guid id, string name, string description, List<RewardsItem> items = null)
    {
        Id = id;
        Name = name;
        Description = description;
        this.items = items ?? new List<RewardsItem>();
    }

    [JsonProperty("Items")]
    private List<RewardsItem> items;


    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    [JsonIgnore]
    public IReadOnlyList<RewardsItem> Items { get => items?.AsReadOnly(); }


    internal void Update(string name, string description)
    {
        this.Name = name;
        this.Description = description;
    }

    internal void AddRewardsItem(RewardsItem item)
    {
        if (item.Price == 0)
            RewardsItemPriceMustNotBeZeroException.Raise(item.Name);

        if (items.Any(i => i.Name == Name))
            RewardsItemAlreadyExistsException.Raise(Id, item.Name);

        items.Add(item);
    }

    internal void RemoveRewardsItem(Guid rewardsItemId)
    {
        var item = GetRewardsItem(rewardsItemId);
        items.Remove(item);
    }

    internal void UpdateRewardsItem(RewardsItem rewardsItem)
    {
        var item = GetRewardsItem(rewardsItem.Id);

        item.Name = rewardsItem.Name;
        item.Description = rewardsItem.Description;
        item.Price = rewardsItem.Price;
        item.Available = rewardsItem.Available;
    }

    private RewardsItem GetRewardsItem(Guid rewardsItemId)
    {
        var item = items.SingleOrDefault(i => i.Id == rewardsItemId);

        if (item == null)
            RewardsItemDoesNotExistException.Raise(Id, rewardsItemId);

        return item;
    }
}
