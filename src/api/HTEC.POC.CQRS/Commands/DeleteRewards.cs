using System;

namespace HTEC.POC.CQRS.Commands;

public class DeleteRewards : IRewardsCommand
{
    public int OperationCode => (int)Common.Operations.OperationCode.DeleteRewards;

    public Guid CorrelationId { get; }

    public Guid RewardsId { get; set; }

    public DeleteRewards(Guid rewardsId)
    {
        this.RewardsId = rewardsId;
    }
}
