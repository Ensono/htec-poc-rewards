using System;

namespace Htec.Poc.CQRS.Commands;

public class UpdateCategory : ICategoryCommand
{
    public int OperationCode => (int)Common.Operations.OperationCode.UpdateCategory;

    public Guid CorrelationId { get; }

    public Guid RewardId { get; set; }

    public Guid CategoryId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public UpdateCategory(Guid correlationId, Guid rewardId, Guid categoryId, string name, string description)
    {
        CorrelationId = correlationId;
        RewardId = rewardId;
        CategoryId = categoryId;
        Name = name;
        Description = description;
    }
}
