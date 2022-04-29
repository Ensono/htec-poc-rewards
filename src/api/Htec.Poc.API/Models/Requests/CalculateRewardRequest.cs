using System;
using System.ComponentModel.DataAnnotations;

namespace Htec.Poc.API.Models.Requests;

/// <summary>
/// Request model used by Calculate Reward Request api endpoint
/// </summary>
public class CalculateRewardRequest
{
    /// <example>d290f1ee-6c54-4b01-90e6-d701748f0851</example>
    [Required]
    public Guid MemberId { get; set; }

    /// <summary>
    /// Represents the basket.
    /// </summary>
    [Required]
    public Basket Basket { get; set; }
}