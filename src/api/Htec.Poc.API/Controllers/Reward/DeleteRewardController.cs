using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Htec.Poc.CQRS.Commands;

namespace Htec.Poc.API.Controllers;

/// <summary>
/// Reward related operations
/// </summary>
[Produces("application/json")]
[Consumes("application/json")]
[ApiExplorerSettings(GroupName = "Reward", IgnoreApi = true)]
[ApiController]
public class DeleteRewardController : ApiControllerBase
{
    readonly ICommandHandler<DeleteReward, bool> commandHandler;

    public DeleteRewardController(ICommandHandler<DeleteReward, bool> commandHandler)
    {
        this.commandHandler = commandHandler ?? throw new ArgumentNullException(nameof(commandHandler));
    }

    /// <summary>
    /// Removes a reward with all its categories and items
    /// </summary>
    /// <remarks>Remove a reward from a restaurant</remarks>
    /// <param name="id">reward id</param>
    /// <response code="204">No Content</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Resource not found</response>
    [HttpDelete("/v1/reward/{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteReward([FromRoute][Required]Guid id)
    {
        // NOTE: Please ensure the API returns the response codes annotated above

        await commandHandler.HandleAsync(new DeleteReward(id));
        return StatusCode(204);
    }
}
