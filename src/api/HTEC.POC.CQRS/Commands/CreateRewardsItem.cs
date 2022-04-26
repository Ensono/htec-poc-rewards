using System;

namespace HTEC.POC.CQRS.Commands;

public class CreateRewardsItem : ICategoryCommand
{
    public int OperationCode => (int)Common.Operations.OperationCode.CreateRewardsItem;

    public Guid CorrelationId { get; set; }

    public Guid RewardsId { get; set; }

    public Guid CategoryId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    public bool Available { get; set; }

    public CreateRewardsItem(Guid correlationId, Guid rewardsId, Guid categoryId, string name, string description, double price, bool available)
    {
        CorrelationId = correlationId;
        RewardsId = rewardsId;
        CategoryId = categoryId;
        Name = name;
        Description = description;
        Price = price;
        Available = available;
    }
}
