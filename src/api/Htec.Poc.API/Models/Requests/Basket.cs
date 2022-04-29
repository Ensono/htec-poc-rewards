using System.ComponentModel.DataAnnotations;

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
}
