using System;
using System.ComponentModel.DataAnnotations;

namespace HTEC.Engagement.API.Models.Requests
{
    /// <summary>
    /// Issue Points Request
    /// </summary>
    public class IssuePointsRequest
    {
        /// <summary>
        /// How many points to issue
        /// </summary>
        [Required]
        public int Points { get; set; }
    }
}
