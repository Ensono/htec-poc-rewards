using Amido.Stacks.Application.CQRS.Commands;
using Htec.Poc.API.Models.Requests;
using Htec.Poc.API.Models.Responses;
using Htec.Poc.CQRS.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Htec.Poc.API.Controllers;

/// <summary>
/// Reward related operations
/// </summary>
[Consumes("application/json")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = "Reward")]
[ApiController]
public class CalculateRewardController : ApiControllerBase
{
    readonly ICommandHandler<CalculateReward, Guid> commandHandler;

    public CalculateRewardController(ICommandHandler<CalculateReward, Guid> commandHandler)
    {
        this.commandHandler = commandHandler ?? throw new ArgumentNullException(nameof(commandHandler));
    }

    /// <summary>
    /// Calculates a reward
    /// </summary>
    /// <remarks>Calculate a reward</remarks>
    /// <param name="body">Reward being calculated</param>
    /// <response code="200">Reward</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Resource not found</response>
    [HttpPost("/v1/reward/calculate")]
    [Authorize]
    [ProducesResponseType(typeof(Reward), 200)]
    public async Task<IActionResult> CalculateReward([Required][FromBody] CalculateRewardRequest body)
    {
        var id = await commandHandler.HandleAsync(
            new CalculateReward(
                correlationId: GetCorrelationId(),
                memberId: body.MemberId
            )
        );

        var reward = new Reward();
        return new ObjectResult(reward);
    }
}