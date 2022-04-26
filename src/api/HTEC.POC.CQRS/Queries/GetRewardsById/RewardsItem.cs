using System;
using System.ComponentModel.DataAnnotations;
using Entity = HTEC.POC.Domain.Entities;

namespace HTEC.POC.CQRS.Queries.GetRewardsById;

public class RewardsItem
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

    public static RewardsItem FromEntity(Entity.RewardsItem i)
    {
        return new RewardsItem()
        {
            Id = i.Id,
            Name = i.Name,
            Description = i.Description,
            Price = i.Price,
            Available = i.Available
        };
    }
}
