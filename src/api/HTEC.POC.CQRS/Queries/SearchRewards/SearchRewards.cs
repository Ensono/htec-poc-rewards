using System;
using Amido.Stacks.Application.CQRS.Queries;

namespace HTEC.POC.CQRS.Queries.SearchRewards;

public class SearchRewards : IQueryCriteria
{
    public int OperationCode => (int)Common.Operations.OperationCode.SearchRewards;

    public Guid CorrelationId { get; }

    public string SearchText { get; }

    public Guid? TenantId { get; }

    public int? PageSize { get; }

    public int? PageNumber { get; }

    public SearchRewards(Guid correlationId, string searchText, Guid? restaurantId, int? pageSize, int? pageNumber)
    {
        CorrelationId = correlationId;
        SearchText = searchText;
        TenantId = restaurantId;
        PageSize = pageSize;
        PageNumber = pageNumber;
    }
}
