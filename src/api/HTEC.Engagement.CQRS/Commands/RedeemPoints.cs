using Amido.Stacks.Application.CQRS.Commands;
using System;

namespace HTEC.Engagement.CQRS.Commands
{
    public class RedeemPoints : ICommand
    {
        public int OperationCode => (int)Common.Operations.OperationCode.RedeemPoints;

        public Guid CorrelationId { get; }

        public Guid PointsId { get; set; }

        public int Points { get; set; }
    }

}
