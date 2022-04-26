using System;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Core.Operations;
using Newtonsoft.Json;

namespace HTEC.POC.Application.CQRS.Events;

public class RewardsCreatedEvent : IApplicationEvent
{
    [JsonConstructor]
    public RewardsCreatedEvent(int operationCode, Guid correlationId, Guid rewardsId)
    {
        OperationCode = operationCode;
        CorrelationId = correlationId;
        RewardsId = rewardsId;
    }

    public RewardsCreatedEvent(IOperationContext context, Guid rewardsId)
    {
        OperationCode = context.OperationCode;
        CorrelationId = context.CorrelationId;
        RewardsId = rewardsId;
    }

    public int EventCode => (int)Enums.EventCode.RewardsCreated;

    public int OperationCode { get; }

    public Guid CorrelationId { get; }

    public Guid RewardsId { get; set; }
}
