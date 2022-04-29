using System;
using Query = Htec.Poc.CQRS.Queries.SearchReward;

namespace Htec.Poc.API.Models.Responses;

/// <summary>
/// Response model representing a search result item in the SearchReward api endpoint
/// </summary>
public class SearchRewardResponseItem
{
    /// <example>d290f1ee-6c54-4b01-90e6-d701748f0851</example>
    public Guid Id { get; private set; }

    /// <example>Reward name</example>
    public string Name { get; private set; }

    /// <example>Reward description</example>
    public string Description { get; private set; }

    /// <summary>
    /// Represents the status of the reward. False if disabled
    /// </summary>
    public bool Enabled { get; private set; }

    public static SearchRewardResponseItem FromSearchRewardResultItem(Query.SearchRewardResultItem item)
    {
        return new SearchRewardResponseItem()
        {
            Id = item.Id ?? Guid.Empty,
            Name = item.Name,
            Description = item.Description,
            Enabled = item.Enabled ?? false
        };
    }
}
