using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Htec.Poc.API.Models.Responses;
using Query = Htec.Poc.CQRS.Queries.GetRewardById;

namespace Htec.Poc.API.Controllers;

/// <summary>
/// Reward related operations
/// </summary>
[Produces("application/json")]
[Consumes("application/json")]
[ApiExplorerSettings(GroupName = "Reward", IgnoreApi = true)]
[ApiController]
public class GetRewardByIdController : ApiControllerBase
{
    readonly IQueryHandler<Query.GetRewardById, Query.Reward> queryHandler;

    public GetRewardByIdController(IQueryHandler<Query.GetRewardById, Query.Reward> queryHandler)
    {
        this.queryHandler = queryHandler ?? throw new ArgumentNullException(nameof(queryHandler));
    }

    /// <summary>
    /// Get a reward
    /// </summary>
    /// <remarks>By passing the reward id, you can get access to available categories and items in the reward </remarks>
    /// <param name="id">reward id</param>
    /// <response code="200">Reward</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Resource not found</response>
    [HttpGet("/v1/reward/{id}")]
    [Authorize]
    [ProducesResponseType(typeof(Reward), 200)]
    public async Task<IActionResult> GetReward([FromRoute][Required]Guid id)
    {
        // NOTE: Please ensure the API returns the response codes annotated above

        var result = await queryHandler.ExecuteAsync(new Query.GetRewardById() { Id = id });

        if (result == null)
            return NotFound();

        var reward = Reward.FromQuery(result);

        return new ObjectResult(reward);
    }
}
