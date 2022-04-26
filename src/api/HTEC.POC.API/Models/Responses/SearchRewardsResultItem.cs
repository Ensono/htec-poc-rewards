using System;

namespace HTEC.POC.API.Models.Responses;

/// <summary>
/// Response model representing a search result item in the SearchRewards api endpoint
/// </summary>
public class SearchRewardsResultItem
{
    /// <example>d290f1ee-6c54-4b01-90e6-d701748f0851</example>
    public Guid Id { get; set; }

    /// <example>Rewards name</example>
    public string Name { get; set; }

    /// <example>Rewards description</example>
    public string Description { get; set; }

    /// <summary>
    /// Represents the status of the rewards. False if disabled
    /// </summary>
    public bool Enabled { get; set; }
}
