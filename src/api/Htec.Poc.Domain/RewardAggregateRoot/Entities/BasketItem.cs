using System;
using Amido.Stacks.Domain;

namespace Htec.Poc.Domain.Entities;

public class BasketItem : IEntity<Guid>
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
}