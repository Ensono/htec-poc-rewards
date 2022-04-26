﻿using System;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Core.Operations;
using Newtonsoft.Json;

namespace HTEC.POC.Application.CQRS.Events;

public class CategoryDeletedEvent : IApplicationEvent
{
    [JsonConstructor]
    public CategoryDeletedEvent(int operationCode, Guid correlationId, Guid rewardsId, Guid categoryId)
    {
        OperationCode = operationCode;
        CorrelationId = correlationId;
        RewardsId = rewardsId;
        CategoryId = categoryId;
    }

    public CategoryDeletedEvent(IOperationContext context, Guid rewardsId, Guid categoryId)
    {
        OperationCode = context.OperationCode;
        CorrelationId = context.CorrelationId;
        RewardsId = rewardsId;
        CategoryId = categoryId;
    }

    public int EventCode => (int)Enums.EventCode.CategoryDeleted;

    public int OperationCode { get; }

    public Guid CorrelationId { get; }

    public Guid RewardsId { get; }

    public Guid CategoryId { get; }
}
