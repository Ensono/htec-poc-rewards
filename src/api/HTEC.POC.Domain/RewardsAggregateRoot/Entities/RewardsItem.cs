using System;
using Amido.Stacks.Domain;

namespace HTEC.POC.Domain.Entities;

public class RewardsItem : IEntity<Guid>
{
    public RewardsItem(Guid id, string name, string description, double price, bool available)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Available = available;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    public bool Available { get; set; }
}
