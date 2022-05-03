using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Htec.Poc.API.Models.Requests;
using Htec.Poc.API.Models.Responses;
using Htec.Poc.CQRS.Commands;

namespace Htec.Poc.API.Controllers;

/// <summary>
/// Reward related operations
/// </summary>
[Consumes("application/json")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = "Reward", IgnoreApi = true)]
[ApiController]
public class CreateRewardController : ApiControllerBase
{
    readonly ICommandHandler<CreateReward, Guid> commandHandler;

    public CreateRewardController(ICommandHandler<CreateReward, Guid> commandHandler)
    {
        this.commandHandler = commandHandler ?? throw new ArgumentNullException(nameof(commandHandler));
    }

    /// <summary>
    /// Create a reward
    /// </summary>
    /// <remarks>Adds a reward</remarks>
    /// <param name="body">Reward being created</param>
    /// <response code="201">Resource created</response>
    /// <response code="400">Bad Request</response>
    /// <response code="409">Conflict, an item already exists</response>
    [HttpPost("/v1/reward/")]
    [Authorize]
    [ProducesResponseType(typeof(ResourceCreatedResponse), 201)]
    public async Task<IActionResult> CreateReward([Required][FromBody]CreateRewardRequest body)
    {
        // NOTE: Please ensure the API returns the response codes annotated above

        var id = await commandHandler.HandleAsync(
            new CreateReward(
                correlationId: GetCorrelationId(),
                tenantId: body.TenantId, //Should check if user logged-in owns it
                name: body.Name,
                description: body.Description,
                enabled: body.Enabled
            )
        );

        return new CreatedAtActionResult(
            "GetReward", "GetRewardById", new
            {
                id = id
            }, new ResourceCreatedResponse(id)
        );
    }
}
