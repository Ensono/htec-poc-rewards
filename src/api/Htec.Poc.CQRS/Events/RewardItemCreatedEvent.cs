﻿using System;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Core.Operations;
using Newtonsoft.Json;

namespace Htec.Poc.Application.CQRS.Events;

public class RewardItemCreatedEvent : IApplicationEvent
{
    [JsonConstructor]
    public RewardItemCreatedEvent(int operationCode, Guid correlationId, Guid rewardId, Guid categoryId, Guid rewardItemId)
    {
        OperationCode = operationCode;
        CorrelationId = correlationId;
        RewardId = rewardId;
        CategoryId = categoryId;
        RewardItemId = rewardItemId;
    }

    public RewardItemCreatedEvent(IOperationContext context, Guid rewardId, Guid categoryId, Guid rewardItemId)
    {
        OperationCode = context.OperationCode;
        CorrelationId = context.CorrelationId;
        RewardId = rewardId;
        CategoryId = categoryId;
        RewardItemId = rewardItemId;
    }

    public int EventCode => (int)Enums.EventCode.RewardItemCreated;

    public int OperationCode { get; }

    public Guid CorrelationId { get; }

    public Guid RewardId { get; }

    public Guid CategoryId { get; }

    public Guid RewardItemId { get; }
}