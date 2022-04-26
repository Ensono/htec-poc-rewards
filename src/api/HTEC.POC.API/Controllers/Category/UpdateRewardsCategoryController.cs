﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HTEC.POC.API.Models.Requests;
using HTEC.POC.CQRS.Commands;

namespace HTEC.POC.API.Controllers;

/// <summary>
/// Category related operations
/// </summary>
[Consumes("application/json")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = "Category")]
public class UpdateRewardsCategoryController : ApiControllerBase
{
    readonly ICommandHandler<UpdateCategory, bool> commandHandler;

    public UpdateRewardsCategoryController(ICommandHandler<UpdateCategory, bool> commandHandler)
    {
        this.commandHandler = commandHandler ?? throw new ArgumentNullException(nameof(commandHandler));
    }

    /// <summary>
    /// Update a category in the rewards
    /// </summary>
    /// <remarks>Update a category to rewards</remarks>
    /// <param name="id">rewards id</param>
    /// <param name="categoryId">Id for Category being removed</param>
    /// <param name="body">Category being added</param>
    /// <response code="204">No Content</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Resource not found</response>
    /// <response code="409">Conflict, an item already exists</response>
    [HttpPut("/v1/rewards/{id}/category/{categoryId}")]
    [Authorize]
    public async Task<IActionResult> UpdateRewardsCategory([FromRoute][Required]Guid id, [FromRoute][Required]Guid categoryId, [FromBody]UpdateCategoryRequest body)
    {
        // NOTE: Please ensure the API returns the response codes annotated above

        await commandHandler.HandleAsync(
            new UpdateCategory(
                correlationId: GetCorrelationId(),
                rewardsId: id,
                categoryId: categoryId,
                name: body.Name,
                description: body.Description
            )
        );

        return StatusCode(204);
    }
}
