using System;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Core.Operations;
using Newtonsoft.Json;

namespace Htec.Poc.Application.CQRS.Events;

public class RewardUpdatedEvent : IApplicationEvent
{
    [JsonConstructor]
    public RewardUpdatedEvent(int operationCode, Guid correlationId, Guid rewardId)
    {
        OperationCode = operationCode;
        CorrelationId = correlationId;
        RewardId = rewardId;
    }

    public RewardUpdatedEvent(IOperationContext context, Guid rewardId)
    {
        OperationCode = context.OperationCode;
        CorrelationId = context.CorrelationId;
        RewardId = rewardId;
    }

    public int EventCode => (int)Enums.EventCode.RewardUpdated;

    public int OperationCode { get; }

    public Guid CorrelationId { get; }

    public Guid RewardId { get; set; }
}
