using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Htec.Poc.CQRS.Queries.GetRewardById;

public class Reward
{
    [Required]
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public bool? Enabled { get; set; }

    public static Reward FromDomain(Domain.Reward reward)
    {
        return new Reward()
        {
            Id = reward.Id,
            TenantId = reward.TenantId,
            Name = reward.Name,
            Description = reward.Description,
            Enabled = reward.Enabled
        };
    }
}
