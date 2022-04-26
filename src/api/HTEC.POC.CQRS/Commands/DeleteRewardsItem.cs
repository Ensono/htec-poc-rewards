using System;

namespace HTEC.POC.CQRS.Commands;

public class DeleteRewardsItem : IRewardsItemCommand
{
    public int OperationCode => (int)Common.Operations.OperationCode.DeleteRewardsItem;

    public Guid CorrelationId { get; }

    public Guid RewardsId { get; set; }

    public Guid CategoryId { get; set; }

    public Guid RewardsItemId { get; set; }

    public DeleteRewardsItem(Guid correlationId, Guid rewardsId, Guid categoryId, Guid rewardsItemId)
    {
        CorrelationId = correlationId;
        RewardsId = rewardsId;
        CategoryId = categoryId;
        RewardsItemId = rewardsItemId;
    }
}
