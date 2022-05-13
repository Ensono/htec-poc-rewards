using System;
using System.Collections.Generic;
using System.Linq;

namespace Htec.Poc.CQRS.Commands.Models
{
    public class Basket
    {
        public Basket(List<BasketItem> items = null)
        {
            this.Items = items ?? new List<BasketItem>();
        }

        public List<BasketItem> Items { get; set; }

        public static Basket FromEntity(Domain.Entities.Basket i)
        {
            return new Basket(i.Items?.Select(BasketItem.FromEntity).ToList());
        }

        public Domain.Entities.Basket ToEntity()
        {
            return new Domain.Entities.Basket(this.Items?.Select(BasketItem.ToEntity).ToList());
        }
    }
}
