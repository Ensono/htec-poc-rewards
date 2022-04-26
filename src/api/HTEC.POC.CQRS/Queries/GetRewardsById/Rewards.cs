using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HTEC.POC.CQRS.Queries.GetRewardsById;

public class Rewards
{
    [Required]
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public List<Category> Categories { get; set; }

    [Required]
    public bool? Enabled { get; set; }

    public static Rewards FromDomain(Domain.Rewards rewards)
    {
        return new Rewards()
        {
            Id = rewards.Id,
            TenantId = rewards.TenantId,
            Name = rewards.Name,
            Description = rewards.Description,
            Enabled = rewards.Enabled,
            Categories = rewards.Categories?.Select(Category.FromEntity).ToList()
        };
    }
}
