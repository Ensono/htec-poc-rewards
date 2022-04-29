using System;
using System.ComponentModel.DataAnnotations;

namespace HTEC.Engagement.API.Models.Requests
{
    /// <summary>
    /// Redeem Points Request
    /// </summary>
    public class RedeemPointsRequest
    {
        /// <summary>
        /// How many points to redeem.
        /// </summary>
        [Required]
        public int Points { get; set; }
    }
}
