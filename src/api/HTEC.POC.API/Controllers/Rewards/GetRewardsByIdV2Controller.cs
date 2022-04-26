using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HTEC.POC.API.Models.Responses;

namespace HTEC.POC.API.Controllers;

/// <summary>
/// Rewards related operations
/// </summary>
[Produces("application/json")]
[Consumes("application/json")]
[ApiExplorerSettings(GroupName = "Rewards")]
[ApiController]
public class GetRewardsByIdV2Controller : ApiControllerBase
{
    /// <summary>
    /// Get a rewards
    /// </summary>
    /// <remarks>By passing the rewards id, you can get access to available categories and items in the rewards </remarks>
    /// <param name="id">rewards id</param>
    /// <response code="200">Rewards</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Resource not found</response>
    [HttpGet("/v2/rewards/{id}")]
    [Authorize]
    [ProducesResponseType(typeof(Rewards), 200)]
    public virtual IActionResult GetRewardsV2([FromRoute][Required]Guid id)
    {
        // NOTE: Please ensure the API returns the response codes annotated above

        return BadRequest();
    }
}
