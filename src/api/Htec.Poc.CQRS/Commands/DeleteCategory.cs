using System;

namespace Htec.Poc.CQRS.Commands;

public class DeleteCategory : ICategoryCommand
{
    public int OperationCode => (int)Common.Operations.OperationCode.DeleteCategory;

    public Guid CorrelationId { get; }

    public Guid RewardId { get; set; }

    public Guid CategoryId { get; set; }

    public DeleteCategory(Guid correlationId, Guid rewardId, Guid categoryId)
    {
        CorrelationId = correlationId;
        RewardId = rewardId;
        CategoryId = categoryId;
    }
}
