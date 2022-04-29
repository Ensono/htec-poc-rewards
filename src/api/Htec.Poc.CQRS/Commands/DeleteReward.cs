using System;

namespace Htec.Poc.CQRS.Commands;

public class DeleteReward : IRewardCommand
{
    public int OperationCode => (int)Common.Operations.OperationCode.DeleteReward;

    public Guid CorrelationId { get; }

    public Guid RewardId { get; set; }

    public DeleteReward(Guid rewardId)
    {
        this.RewardId = rewardId;
    }
}
