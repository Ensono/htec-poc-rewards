using System;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Core.Operations;
using Newtonsoft.Json;

namespace HTEC.POC.Application.CQRS.Events;

public class RewardsItemCreatedEvent : IApplicationEvent
{
    [JsonConstructor]
    public RewardsItemCreatedEvent(int operationCode, Guid correlationId, Guid rewardsId, Guid categoryId, Guid rewardsItemId)
    {
        OperationCode = operationCode;
        CorrelationId = correlationId;
        RewardsId = rewardsId;
        CategoryId = categoryId;
        RewardsItemId = rewardsItemId;
    }

    public RewardsItemCreatedEvent(IOperationContext context, Guid rewardsId, Guid categoryId, Guid rewardsItemId)
    {
        OperationCode = context.OperationCode;
        CorrelationId = context.CorrelationId;
        RewardsId = rewardsId;
        CategoryId = categoryId;
        RewardsItemId = rewardsItemId;
    }

    public int EventCode => (int)Enums.EventCode.RewardsItemCreated;

    public int OperationCode { get; }

    public Guid CorrelationId { get; }

    public Guid RewardsId { get; set; }

    public Guid CategoryId { get; set; }

    public Guid RewardsItemId { get; set; }
}
