using System;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Core.Operations;
using Newtonsoft.Json;

namespace HTEC.POC.Application.CQRS.Events;

public class RewardsUpdatedEvent : IApplicationEvent
{
    [JsonConstructor]
    public RewardsUpdatedEvent(int operationCode, Guid correlationId, Guid rewardsId)
    {
        OperationCode = operationCode;
        CorrelationId = correlationId;
        RewardsId = rewardsId;
    }

    public RewardsUpdatedEvent(IOperationContext context, Guid rewardsId)
    {
        OperationCode = context.OperationCode;
        CorrelationId = context.CorrelationId;
        RewardsId = rewardsId;
    }

    public int EventCode => (int)Enums.EventCode.RewardsUpdated;

    public int OperationCode { get; }

    public Guid CorrelationId { get; }

    public Guid RewardsId { get; set; }
}
