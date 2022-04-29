using System;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Core.Operations;
using Newtonsoft.Json;

namespace Htec.Poc.Application.CQRS.Events;

public class RewardItemUpdatedEvent : IApplicationEvent
{
    [JsonConstructor]
    public RewardItemUpdatedEvent(int operationCode, Guid correlationId, Guid rewardId, Guid categoryId, Guid rewardItemId)
    {
        OperationCode = operationCode;
        CorrelationId = correlationId;
        RewardId = rewardId;
        CategoryId = categoryId;
        RewardItemId = rewardItemId;
    }

    public RewardItemUpdatedEvent(IOperationContext context, Guid rewardId, Guid categoryId, Guid rewardItemId)
    {
        OperationCode = context.OperationCode;
        CorrelationId = context.CorrelationId;
        RewardId = rewardId;
        CategoryId = categoryId;
        RewardItemId = rewardItemId;
    }

    public int EventCode => (int)Enums.EventCode.RewardItemUpdated;

    public int OperationCode { get; }

    public Guid CorrelationId { get; }

    public Guid RewardId { get; set; }

    public Guid CategoryId { get; set; }

    public Guid RewardItemId { get; set; }
}
