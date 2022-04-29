using System;

namespace Htec.Poc.CQRS.Commands;

public class CreateCategory : IRewardCommand
{
    public int OperationCode => (int)Common.Operations.OperationCode.CreateCategory;

    public Guid CorrelationId { get; set; }

    public Guid RewardId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public CreateCategory(Guid correlationId, Guid rewardId, string name, string description)
    {
        CorrelationId = correlationId;
        RewardId = rewardId;
        Name = name;
        Description = description;
    }
}
