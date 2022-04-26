using System;

namespace HTEC.POC.CQRS.Commands;

public class UpdateCategory : ICategoryCommand
{
    public int OperationCode => (int)Common.Operations.OperationCode.UpdateCategory;

    public Guid CorrelationId { get; }

    public Guid RewardsId { get; set; }

    public Guid CategoryId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public UpdateCategory(Guid correlationId, Guid rewardsId, Guid categoryId, string name, string description)
    {
        CorrelationId = correlationId;
        RewardsId = rewardsId;
        CategoryId = categoryId;
        Name = name;
        Description = description;
    }
}
