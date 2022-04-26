using System;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Core.Operations;
using Newtonsoft.Json;

namespace HTEC.POC.Application.CQRS.Events;

public class CategoryCreatedEvent : IApplicationEvent
{
    [JsonConstructor]
    public CategoryCreatedEvent(int operationCode, Guid correlationId, Guid rewardsId, Guid categoryId)
    {
        OperationCode = operationCode;
        CorrelationId = correlationId;
        RewardsId = rewardsId;
        CategoryId = categoryId;
    }

    public CategoryCreatedEvent(IOperationContext context, Guid rewardsId, Guid categoryId)
    {
        OperationCode = context.OperationCode;
        CorrelationId = context.CorrelationId;
        RewardsId = rewardsId;
        CategoryId = categoryId;
    }

    public int EventCode => (int)Enums.EventCode.CategoryCreated;

    public int OperationCode { get; }

    public Guid CorrelationId { get; }

    public Guid RewardsId { get; set; }

    public Guid CategoryId { get; set; }
}
