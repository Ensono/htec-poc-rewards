using System;
using System.ComponentModel.DataAnnotations;

namespace Htec.Poc.API.Models.Requests;

/// <summary>
/// Represents the items in the basket.
/// </summary>
public class BasketItem
{
    /// <example>d290f1ee-6c54-4b01-90e6-d701748f0851</example>
    [Required]
    public Guid ItemId { get; set; }

    /// <example>F1</example>
    [Required]
    public string ItemGroup { get; set; }

    /// <example>1.90</example>
    [Required]
    public decimal Price { get; set; }

    /// <example>10</example>
    [Required]
    public int Quantity { get; set; }
}