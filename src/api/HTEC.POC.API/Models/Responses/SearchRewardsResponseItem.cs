using System;
using Query = HTEC.POC.CQRS.Queries.SearchRewards;

namespace HTEC.POC.API.Models.Responses;

/// <summary>
/// Response model representing a search result item in the SearchRewards api endpoint
/// </summary>
public class SearchRewardsResponseItem
{
    /// <example>d290f1ee-6c54-4b01-90e6-d701748f0851</example>
    public Guid Id { get; private set; }

    /// <example>Rewards name</example>
    public string Name { get; private set; }

    /// <example>Rewards description</example>
    public string Description { get; private set; }

    /// <summary>
    /// Represents the status of the rewards. False if disabled
    /// </summary>
    public bool Enabled { get; private set; }

    public static SearchRewardsResponseItem FromSearchRewardsResultItem(Query.SearchRewardsResultItem item)
    {
        return new SearchRewardsResponseItem()
        {
            Id = item.Id ?? Guid.Empty,
            Name = item.Name,
            Description = item.Description,
            Enabled = item.Enabled ?? false
        };
    }
}
