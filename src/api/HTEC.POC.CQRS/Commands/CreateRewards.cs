using System;
using Amido.Stacks.Application.CQRS.Commands;

namespace HTEC.POC.CQRS.Commands;

public class CreateRewards : ICommand
{
    public int OperationCode => (int)Common.Operations.OperationCode.CreateRewards;

    public Guid CorrelationId { get; }

    public Guid TenantId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool Enabled { get; set; }

    public CreateRewards(Guid correlationId, Guid tenantId, string name, string description, bool enabled)
    {
        CorrelationId = correlationId;
        TenantId = tenantId;
        Name = name;
        Description = description;
        Enabled = enabled;
    }
}
