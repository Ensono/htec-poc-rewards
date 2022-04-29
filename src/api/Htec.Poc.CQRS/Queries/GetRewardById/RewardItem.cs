using System;
using System.ComponentModel.DataAnnotations;
using Entity = Htec.Poc.Domain.Entities;

namespace Htec.Poc.CQRS.Queries.GetRewardById;

public class RewardItem
{
    [Required]
    public Guid? Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public double Price { get; set; }

    [Required]
    public bool Available { get; set; }

    public static RewardItem FromEntity(Entity.RewardItem i)
    {
        return new RewardItem()
        {
            Id = i.Id,
            Name = i.Name,
            Description = i.Description,
            Price = i.Price,
            Available = i.Available
        };
    }
}
