using Amido.Stacks.Application.CQRS.Commands;
using System;

namespace HTEC.Engagement.CQRS.Commands
{
    public class IssuePoints : ICommand
    {
        public int OperationCode => (int)Common.Operations.OperationCode.IssuePoints;

        public Guid CorrelationId { get; }

        public Guid PointsId { get; set; }

        public int Points { get; set; }
    }
}
