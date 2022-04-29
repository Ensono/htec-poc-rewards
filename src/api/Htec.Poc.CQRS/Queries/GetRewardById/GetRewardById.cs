using System;
using Amido.Stacks.Application.CQRS.Queries;

namespace Htec.Poc.CQRS.Queries.GetRewardById;

public class GetRewardById : IQueryCriteria
{
    public int OperationCode => (int)Common.Operations.OperationCode.GetRewardById;

    public Guid CorrelationId { get; }

    public Guid Id { get; set; }
}
