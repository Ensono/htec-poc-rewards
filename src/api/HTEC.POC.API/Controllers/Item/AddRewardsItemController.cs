﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HTEC.POC.API.Models.Requests;
using HTEC.POC.API.Models.Responses;
using HTEC.POC.CQRS.Commands;

namespace HTEC.POC.API.Controllers;

using Microsoft.AspNetCore.Http;

/// <summary>
/// Item related operations
/// </summary>
[Consumes("application/json")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = "Item")]
public class AddRewardsItemController : ApiControllerBase
{
    readonly ICommandHandler<CreateRewardsItem, Guid> commandHandler;

    public AddRewardsItemController(ICommandHandler<CreateRewardsItem, Guid> commandHandler)
    {
        this.commandHandler = commandHandler ?? throw new ArgumentNullException(nameof(commandHandler));
    }

    /// <summary>
    /// Create an item to a category in the rewards
    /// </summary>
    /// <remarks>Adds a new item to a category in the rewards</remarks>
    /// <param name="id">rewards id</param>
    /// <param name="categoryId">Id for Category being removed</param>
    /// <param name="body">Category being added</param>
    /// <response code="201">Resource created</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Resource not found</response>
    /// <response code="409">Conflict, an item already exists</response>
    [HttpPost("/v1/rewards/{id}/category/{categoryId}/items/")]
    [Authorize]
    [ProducesResponseType(typeof(ResourceCreatedResponse), 201)]
    public async Task<IActionResult> AddRewardsItem([FromRoute][Required]Guid id, [FromRoute][Required]Guid categoryId, [FromBody]CreateItemRequest body)
    {
        // NOTE: Please ensure the API returns the response codes annotated above

        var rewardsItemId = await commandHandler.HandleAsync(
            new CreateRewardsItem(
                correlationId: GetCorrelationId(),
                rewardsId: id,
                categoryId: categoryId,
                name: body.Name,
                description: body.Description,
                price: body.Price,
                available: body.Available
            )
        );

        return StatusCode(StatusCodes.Status201Created, new ResourceCreatedResponse(rewardsItemId));
    }
}
