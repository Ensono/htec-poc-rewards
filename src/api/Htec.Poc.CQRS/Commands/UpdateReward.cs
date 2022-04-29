using System;

namespace Htec.Poc.CQRS.Commands;

public class UpdateReward : IRewardCommand
{
    public int OperationCode => (int)Common.Operations.OperationCode.UpdateReward;

    public Guid CorrelationId { get; }

    public Guid RewardId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool Enabled { get; set; }
}
