using System;

namespace Htec.Poc.CQRS.Commands;

public class UpdateRewardItem : IRewardItemCommand
{
    public int OperationCode => (int)Common.Operations.OperationCode.UpdateRewardItem;

    public Guid CorrelationId { get; }

    public Guid RewardId { get; set; }

    public Guid CategoryId { get; set; }

    public Guid RewardItemId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    public bool Available { get; set; }

    public UpdateRewardItem(Guid correlationId, Guid rewardId, Guid categoryId, Guid rewardItemId, string name, string description, double price, bool available)
    {
        CorrelationId = correlationId;
        RewardId = rewardId;
        CategoryId = categoryId;
        RewardItemId = rewardItemId;
        Name = name;
        Description = description;
        Price = price;
        Available = available;
    }
}
