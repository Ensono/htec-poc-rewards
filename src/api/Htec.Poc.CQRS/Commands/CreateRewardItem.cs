using System;

namespace Htec.Poc.CQRS.Commands;

public class CreateRewardItem : ICategoryCommand
{
    public int OperationCode => (int)Common.Operations.OperationCode.CreateRewardItem;

    public Guid CorrelationId { get; set; }

    public Guid RewardId { get; set; }

    public Guid CategoryId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    public bool Available { get; set; }

    public CreateRewardItem(Guid correlationId, Guid rewardId, Guid categoryId, string name, string description, double price, bool available)
    {
        CorrelationId = correlationId;
        RewardId = rewardId;
        CategoryId = categoryId;
        Name = name;
        Description = description;
        Price = price;
        Available = available;
    }
}
