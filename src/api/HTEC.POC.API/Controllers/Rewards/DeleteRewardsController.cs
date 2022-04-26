using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HTEC.POC.CQRS.Commands;

namespace HTEC.POC.API.Controllers;

/// <summary>
/// Rewards related operations
/// </summary>
[Produces("application/json")]
[Consumes("application/json")]
[ApiExplorerSettings(GroupName = "Rewards")]
[ApiController]
public class DeleteRewardsController : ApiControllerBase
{
    readonly ICommandHandler<DeleteRewards, bool> commandHandler;

    public DeleteRewardsController(ICommandHandler<DeleteRewards, bool> commandHandler)
    {
        this.commandHandler = commandHandler ?? throw new ArgumentNullException(nameof(commandHandler));
    }

    /// <summary>
    /// Removes a rewards with all its categories and items
    /// </summary>
    /// <remarks>Remove a rewards from a restaurant</remarks>
    /// <param name="id">rewards id</param>
    /// <response code="204">No Content</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Resource not found</response>
    [HttpDelete("/v1/rewards/{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteRewards([FromRoute][Required]Guid id)
    {
        // NOTE: Please ensure the API returns the response codes annotated above

        await commandHandler.HandleAsync(new DeleteRewards(id));
        return StatusCode(204);
    }
}
