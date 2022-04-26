using System;

namespace HTEC.POC.CQRS.Commands;

public class CreateCategory : IRewardsCommand
{
    public int OperationCode => (int)Common.Operations.OperationCode.CreateCategory;

    public Guid CorrelationId { get; set; }

    public Guid RewardsId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public CreateCategory(Guid correlationId, Guid rewardsId, string name, string description)
    {
        CorrelationId = correlationId;
        RewardsId = rewardsId;
        Name = name;
        Description = description;
    }
}
