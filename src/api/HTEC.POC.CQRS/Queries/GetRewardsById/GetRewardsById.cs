using System;
using Amido.Stacks.Application.CQRS.Queries;

namespace HTEC.POC.CQRS.Queries.GetRewardsById;

public class GetRewardsById : IQueryCriteria
{
    public int OperationCode => (int)Common.Operations.OperationCode.GetRewardsById;

    public Guid CorrelationId { get; }

    public Guid Id { get; set; }
}
