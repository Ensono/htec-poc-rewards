using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Htec.Poc.API.Models.Responses;

namespace Htec.Poc.API.Controllers;

/// <summary>
/// Reward related operations
/// </summary>
[Produces("application/json")]
[Consumes("application/json")]
[ApiExplorerSettings(GroupName = "Reward")]
[ApiController]
public class GetRewardByIdV2Controller : ApiControllerBase
{
    /// <summary>
    /// Get a reward
    /// </summary>
    /// <remarks>By passing the reward id, you can get access to available categories and items in the reward </remarks>
    /// <param name="id">reward id</param>
    /// <response code="200">Reward</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Resource not found</response>
    [HttpGet("/v2/reward/{id}")]
    [Authorize]
    [ProducesResponseType(typeof(Reward), 200)]
    public virtual IActionResult GetRewardV2([FromRoute][Required]Guid id)
    {
        // NOTE: Please ensure the API returns the response codes annotated above

        return BadRequest();
    }
}
