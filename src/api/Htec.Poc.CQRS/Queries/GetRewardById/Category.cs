using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using Entity = Htec.Poc.Domain.Entities;

namespace Htec.Poc.CQRS.Queries.GetRewardById;

public class Category
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public List<RewardItem> Items { get; set; }

    public static Category FromEntity(Entity.Category c)
    {
        return new Category()
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            Items = c.Items?.Select(RewardItem.FromEntity).ToList()
        };
    }
}
