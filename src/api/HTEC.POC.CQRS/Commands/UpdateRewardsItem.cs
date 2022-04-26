using System;

namespace HTEC.POC.CQRS.Commands;

public class UpdateRewardsItem : IRewardsItemCommand
{
    public int OperationCode => (int)Common.Operations.OperationCode.UpdateRewardsItem;

    public Guid CorrelationId { get; }

    public Guid RewardsId { get; set; }

    public Guid CategoryId { get; set; }

    public Guid RewardsItemId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    public bool Available { get; set; }

    public UpdateRewardsItem(Guid correlationId, Guid rewardsId, Guid categoryId, Guid rewardsItemId, string name, string description, double price, bool available)
    {
        CorrelationId = correlationId;
        RewardsId = rewardsId;
        CategoryId = categoryId;
        RewardsItemId = rewardsItemId;
        Name = name;
        Description = description;
        Price = price;
        Available = available;
    }
}
