using System;
using Amido.Stacks.Application.CQRS.Commands;

namespace HTEC.Engagement.CQRS.Commands
{
    public class CreatePoints : ICommand
    {
        public int OperationCode => (int)Common.Operations.OperationCode.CreatePoints;

        public Guid CorrelationId { get; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; }

        public int Balance { get; set; }

        public CreatePoints(Guid correlationId, string name, string description, bool enabled, int balance)
        {
            CorrelationId = correlationId;
            Name = name;
            Description = description;
            Enabled = enabled;
            Balance = balance;
        }
    }
}
