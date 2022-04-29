using System;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Core.Operations;
using Newtonsoft.Json;

namespace Htec.Poc.Application.CQRS.Events;

public class RewardDeletedEvent : IApplicationEvent
{
    [JsonConstructor]
    public RewardDeletedEvent(int operationCode, Guid correlationId, Guid rewardId)
    {
        OperationCode = operationCode;
        CorrelationId = correlationId;
        RewardId = rewardId;
    }

    public RewardDeletedEvent(IOperationContext context, Guid rewardId)
    {
        OperationCode = context.OperationCode;
        CorrelationId = context.CorrelationId;
        RewardId = rewardId;
    }

    public int EventCode => (int)Enums.EventCode.RewardDeleted;

    public int OperationCode { get; }

    public Guid CorrelationId { get; }

    public Guid RewardId { get; }
}
