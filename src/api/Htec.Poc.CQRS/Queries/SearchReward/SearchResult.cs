using System.Collections.Generic;

namespace Htec.Poc.CQRS.Queries.SearchReward;

public class SearchRewardResult
{
    public int? PageSize { get; set; }

    public int? PageNumber { get; set; }

    public IEnumerable<SearchRewardResultItem> Results { get; set; }
}
