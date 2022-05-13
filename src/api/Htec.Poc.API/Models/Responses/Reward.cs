using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Query = Htec.Poc.CQRS.Queries.GetRewardById;

namespace Htec.Poc.API.Models.Responses;

/// <summary>
/// Response model used by GetById api endpoint
/// </summary>
public class Reward
{
    /// <example>d290f1ee-6c54-4b01-90e6-d701748f0851</example>
    [Required]
    public Guid? Id { get; private set; }

    /// <example>Reward name</example>
    [Required]
    public string Name { get; private set; }

    /// <example>Reward description</example>
    public string Description { get; private set; }

    /// <summary>
    /// Represents the status of the reward. False if disabled
    /// </summary>
    [Required]
    public bool? Enabled { get; private set; }

    public static Reward FromQuery(Query.Reward reward)
    {
        return new Reward
        {
            Id = reward.Id,
            Name = reward.Name,
            Description = reward.Description,
            Enabled = reward.Enabled
        };
    }
}
