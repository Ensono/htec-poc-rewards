using System;

namespace Htec.Poc.CQRS.Commands;

public class DeleteRewardItem : IRewardItemCommand
{
    public int OperationCode => (int)Common.Operations.OperationCode.DeleteRewardItem;

    public Guid CorrelationId { get; }

    public Guid RewardId { get; set; }

    public Guid CategoryId { get; set; }

    public Guid RewardItemId { get; set; }

    public DeleteRewardItem(Guid correlationId, Guid rewardId, Guid categoryId, Guid rewardItemId)
    {
        CorrelationId = correlationId;
        RewardId = rewardId;
        CategoryId = categoryId;
        RewardItemId = rewardItemId;
    }
}
