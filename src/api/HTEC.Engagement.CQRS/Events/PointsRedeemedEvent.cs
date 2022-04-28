using System;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Core.Operations;
using Newtonsoft.Json;

namespace HTEC.Engagement.Application.CQRS.Events
{
    public class PointsRedeemedEvent : IApplicationEvent
    {
        [JsonConstructor]
        public PointsRedeemedEvent(int operationCode, Guid correlationId, Guid pointsId, int points)
        {
            OperationCode = operationCode;
            CorrelationId = correlationId;
            PointsId = pointsId;
            Points = points;
        }

        public PointsRedeemedEvent(IOperationContext context, Guid pointsId, int points)
        {
            OperationCode = context.OperationCode;
            CorrelationId = context.CorrelationId;
            PointsId = pointsId;
            Points = points;
        }

        public int EventCode => (int)Enums.EventCode.PointsRedeemed;

        public int OperationCode { get; }

        public Guid CorrelationId { get; }

        public Guid PointsId { get; set; }

        public int Points { get; set; }
    }
}
