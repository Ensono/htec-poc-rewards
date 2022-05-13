using System;

namespace Htec.Poc.CQRS.Commands.Models
{
    public class BasketItem
    {
        public BasketItem(Guid id, string itemGroup, decimal price, int quantity)
        {
            Id = id;
            ItemGroup = itemGroup;
            Price = price;
            Quantity = quantity;
        }

        public Guid Id { get; set; }

        public string ItemGroup { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public static BasketItem FromEntity(Domain.Entities.BasketItem i)
        {
            return new BasketItem(i.Id, i.ItemGroup, i.Price, i.Quantity);
        }

        public static Domain.Entities.BasketItem ToEntity(BasketItem i)
        {
            return new Domain.Entities.BasketItem(i.Id, i.ItemGroup, i.Price, i.Quantity);
        }
    }
}
