using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Htec.Poc.API.Models.Requests;

/// <summary>
/// Represents the basket.
/// </summary>
public class Basket
{
    /// <summary>
    /// Represents all the items in the basket.
    /// </summary>
    [Required]
    public BasketItem[] Items { get; set;}

    public CQRS.Commands.Models.Basket ToEntity()
    {
        return new CQRS.Commands.Models.Basket(this.Items?.Select(BasketItem.ToEntity).ToList());
    }
}
