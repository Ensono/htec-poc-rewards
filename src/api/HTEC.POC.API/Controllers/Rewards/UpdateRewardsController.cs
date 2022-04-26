using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HTEC.POC.API.Models.Requests;
using HTEC.POC.CQRS.Commands;

namespace HTEC.POC.API.Controllers;

/// <summary>
/// Rewards related operations
/// </summary>
[Produces("application/json")]
[Consumes("application/json")]
[ApiExplorerSettings(GroupName = "Rewards")]
[ApiController]
public class UpdateRewardsController : ApiControllerBase
{
    readonly ICommandHandler<UpdateRewards, bool> commandHandler;

    public UpdateRewardsController(ICommandHandler<UpdateRewards, bool> commandHandler)
    {
        this.commandHandler = commandHandler ?? throw new ArgumentNullException(nameof(commandHandler));
    }


    /// <summary>
    /// Update a rewards
    /// </summary>
    /// <remarks>Update a rewards with new information</remarks>
    /// <param name="id">rewards id</param>
    /// <param name="body">Rewards being updated</param>
    /// <response code="204">No Content</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Resource not found</response>
    [HttpPut("/v1/rewards/{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateRewards([FromRoute][Required]Guid id, [FromBody]UpdateRewardsRequest body)
    {
        // NOTE: Please ensure the API returns the response codes annotated above

        await commandHandler.HandleAsync(
            new UpdateRewards()
            {
                RewardsId = id,
                Name = body.Name,
                Description = body.Description,
                Enabled = body.Enabled
            });

        return StatusCode(204);
    }
}
