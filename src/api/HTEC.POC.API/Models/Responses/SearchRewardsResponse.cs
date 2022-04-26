using System;
using System.Collections.Generic;
using System.Linq;
using HTEC.POC.CQRS.Queries.SearchRewards;

namespace HTEC.POC.API.Models.Responses;

/// <summary>
/// Response model used by SearchRewards api endpoint
/// </summary>
public class SearchRewardsResponse
{
    /// <example>10</example>
    public int Size { get; private set; }

    /// <example>0</example>
    public int Offset { get; private set; }

    /// <summary>
    /// Contains the items returned from the search
    /// </summary>
    public List<SearchRewardsResponseItem> Results { get; private set; }

    public static SearchRewardsResponse FromRewardsResultItem(SearchRewardsResult results)
    {
        return new SearchRewardsResponse()
        {
            Offset = (results?.PageNumber ?? 0) * (results?.PageSize ?? 0),
            Size = (results?.PageSize ?? 0),
            Results = results?.Results?.Select(SearchRewardsResponseItem.FromSearchRewardsResultItem).ToList()
        };
    }
}
