using System;
using Amido.Stacks.Application.CQRS.Commands;

namespace Htec.Poc.CQRS.Commands;

public class CreateReward : ICommand
{
    public int OperationCode => (int)Common.Operations.OperationCode.CreateReward;

    public Guid CorrelationId { get; }

    public Guid TenantId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool Enabled { get; set; }

    public CreateReward(Guid correlationId, Guid tenantId, string name, string description, bool enabled)
    {
        CorrelationId = correlationId;
        TenantId = tenantId;
        Name = name;
        Description = description;
        Enabled = enabled;
    }
}