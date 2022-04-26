using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HTEC.POC.CQRS.Commands;

namespace HTEC.POC.API.Controllers;

/// <summary>
/// Item related operations
/// </summary>
[Consumes("application/json")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = "Item")]
public class DeleteRewardsItemController : ApiControllerBase
{
    readonly ICommandHandler<DeleteRewardsItem, bool> commandHandler;

    public DeleteRewardsItemController(ICommandHandler<DeleteRewardsItem, bool> commandHandler)
    {
        this.commandHandler = commandHandler ?? throw new ArgumentNullException(nameof(commandHandler));
    }

    /// <summary>
    /// Removes an item from rewards
    /// </summary>
    /// <remarks>Removes an item from rewards</remarks>
    /// <param name="id">rewards id</param>
    /// <param name="categoryId">Category ID</param>
    /// <param name="itemId">Id for Item being removed</param>
    /// <response code="204">No Content</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Resource not found</response>
    [HttpDelete("/v1/rewards/{id}/category/{categoryId}/items/{itemId}")]
    [Authorize]
    public async Task<IActionResult> DeleteRewardsItem([FromRoute][Required]Guid id, [FromRoute][Required]Guid categoryId, [FromRoute][Required]Guid itemId)
    {
        // NOTE: Please ensure the API returns the response codes annotated above

        await commandHandler.HandleAsync(
            new DeleteRewardsItem(
                correlationId: GetCorrelationId(),
                rewardsId: id,
                categoryId: categoryId,
                rewardsItemId: itemId
            )
        );

        return StatusCode(204);
    }
}
