using System;
using System.Collections.Generic;
using Amido.Stacks.Domain;

namespace Htec.Poc.Domain.Entities;

public class Basket : IEntity<Guid>
{
    public Basket(List<BasketItem> items = null)
    {
        this.Items = items ?? new List<BasketItem>();
    }

    public List<BasketItem> Items { get; set; }

    public Guid Id { get; set; }
}