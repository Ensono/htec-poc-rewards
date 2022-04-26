using System;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Core.Operations;
using Newtonsoft.Json;

namespace HTEC.POC.Application.CQRS.Events;

public class RewardsItemDeletedEvent : IApplicationEvent
{
    [JsonConstructor]
    public RewardsItemDeletedEvent(int operationCode, Guid correlationId, Guid rewardsId, Guid categoryId, Guid rewardsItemId)
    {
        OperationCode = operationCode;
        CorrelationId = correlationId;
        RewardsId = rewardsId;
        CategoryId = categoryId;
        RewardsItemId = rewardsItemId;
    }

    public RewardsItemDeletedEvent(IOperationContext context, Guid rewardsId, Guid categoryId, Guid rewardsItemId)
    {
        OperationCode = context.OperationCode;
        CorrelationId = context.CorrelationId;
        RewardsId = rewardsId;
        CategoryId = categoryId;
        RewardsItemId = rewardsItemId;
    }

    public int EventCode => (int)Enums.EventCode.RewardsItemDeleted;

    public int OperationCode { get; }

    public Guid CorrelationId { get; }

    public Guid RewardsId { get; }

    public Guid CategoryId { get; }

    public Guid RewardsItemId { get; }
}
