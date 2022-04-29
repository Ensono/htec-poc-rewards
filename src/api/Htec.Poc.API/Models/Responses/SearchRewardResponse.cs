using System;
using System.Collections.Generic;
using System.Linq;
using Htec.Poc.CQRS.Queries.SearchReward;

namespace Htec.Poc.API.Models.Responses;

/// <summary>
/// Response model used by SearchReward api endpoint
/// </summary>
public class SearchRewardResponse
{
    /// <example>10</example>
    public int Size { get; private set; }

    /// <example>0</example>
    public int Offset { get; private set; }

    /// <summary>
    /// Contains the items returned from the search
    /// </summary>
    public List<SearchRewardResponseItem> Results { get; private set; }

    public static SearchRewardResponse FromRewardResultItem(SearchRewardResult results)
    {
        return new SearchRewardResponse()
        {
            Offset = (results?.PageNumber ?? 0) * (results?.PageSize ?? 0),
            Size = (results?.PageSize ?? 0),
            Results = results?.Results?.Select(SearchRewardResponseItem.FromSearchRewardResultItem).ToList()
        };
    }
}
