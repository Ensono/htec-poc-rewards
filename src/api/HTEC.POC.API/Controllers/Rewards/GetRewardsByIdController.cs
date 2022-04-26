using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HTEC.POC.API.Models.Responses;
using Query = HTEC.POC.CQRS.Queries.GetRewardsById;

namespace HTEC.POC.API.Controllers;

/// <summary>
/// Rewards related operations
/// </summary>
[Produces("application/json")]
[Consumes("application/json")]
[ApiExplorerSettings(GroupName = "Rewards")]
[ApiController]
public class GetRewardsByIdController : ApiControllerBase
{
    readonly IQueryHandler<Query.GetRewardsById, Query.Rewards> queryHandler;

    public GetRewardsByIdController(IQueryHandler<Query.GetRewardsById, Query.Rewards> queryHandler)
    {
        this.queryHandler = queryHandler ?? throw new ArgumentNullException(nameof(queryHandler));
    }

    /// <summary>
    /// Get a rewards
    /// </summary>
    /// <remarks>By passing the rewards id, you can get access to available categories and items in the rewards </remarks>
    /// <param name="id">rewards id</param>
    /// <response code="200">Rewards</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Resource not found</response>
    [HttpGet("/v1/rewards/{id}")]
    [Authorize]
    [ProducesResponseType(typeof(Rewards), 200)]
    public async Task<IActionResult> GetRewards([FromRoute][Required]Guid id)
    {
        // NOTE: Please ensure the API returns the response codes annotated above

        var result = await queryHandler.ExecuteAsync(new Query.GetRewardsById() { Id = id });

        if (result == null)
            return NotFound();

        var rewards = Rewards.FromQuery(result);

        return new ObjectResult(rewards);
    }
}
