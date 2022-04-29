using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Htec.Poc.API.Models.Requests;
using Htec.Poc.CQRS.Commands;

namespace Htec.Poc.API.Controllers;

/// <summary>
/// Reward related operations
/// </summary>
[Produces("application/json")]
[Consumes("application/json")]
[ApiExplorerSettings(GroupName = "Reward")]
[ApiController]
public class UpdateRewardController : ApiControllerBase
{
    readonly ICommandHandler<UpdateReward, bool> commandHandler;

    public UpdateRewardController(ICommandHandler<UpdateReward, bool> commandHandler)
    {
        this.commandHandler = commandHandler ?? throw new ArgumentNullException(nameof(commandHandler));
    }


    /// <summary>
    /// Update a reward
    /// </summary>
    /// <remarks>Update a reward with new information</remarks>
    /// <param name="id">reward id</param>
    /// <param name="body">Reward being updated</param>
    /// <response code="204">No Content</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Resource not found</response>
    [HttpPut("/v1/reward/{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateReward([FromRoute][Required]Guid id, [FromBody]UpdateRewardRequest body)
    {
        // NOTE: Please ensure the API returns the response codes annotated above

        await commandHandler.HandleAsync(
            new UpdateReward()
            {
                RewardId = id,
                Name = body.Name,
                Description = body.Description,
                Enabled = body.Enabled
            });

        return StatusCode(204);
    }
}
