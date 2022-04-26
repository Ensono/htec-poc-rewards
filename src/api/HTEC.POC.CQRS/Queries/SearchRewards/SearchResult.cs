using System.Collections.Generic;

namespace HTEC.POC.CQRS.Queries.SearchRewards;

public class SearchRewardsResult
{
    public int? PageSize { get; set; }

    public int? PageNumber { get; set; }

    public IEnumerable<SearchRewardsResultItem> Results { get; set; }
}
