using System;

namespace HTEC.POC.CQRS.Commands;

public class DeleteCategory : ICategoryCommand
{
    public int OperationCode => (int)Common.Operations.OperationCode.DeleteCategory;

    public Guid CorrelationId { get; }

    public Guid RewardsId { get; set; }

    public Guid CategoryId { get; set; }

    public DeleteCategory(Guid correlationId, Guid rewardsId, Guid categoryId)
    {
        CorrelationId = correlationId;
        RewardsId = rewardsId;
        CategoryId = categoryId;
    }
}
