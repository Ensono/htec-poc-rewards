using System;

namespace HTEC.POC.CQRS.Commands;

public class UpdateRewards : IRewardsCommand
{
    public int OperationCode => (int)Common.Operations.OperationCode.UpdateRewards;

    public Guid CorrelationId { get; }

    public Guid RewardsId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool Enabled { get; set; }
}
