using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Htec.Poc.API.Models.Responses;
using Htec.Poc.CQRS.Queries.SearchReward;

namespace Htec.Poc.API.Controllers;

/// <summary>
/// Reward related operations
/// </summary>
[Produces("application/json")]
[Consumes("application/json")]
[ApiExplorerSettings(GroupName = "Reward", IgnoreApi = true)]
[ApiController]
public class SearchRewardController : ApiControllerBase
{
    readonly IQueryHandler<SearchReward, SearchRewardResult> queryHandler;

    public SearchRewardController(IQueryHandler<SearchReward, SearchRewardResult> queryHandler)
    {
        this.queryHandler = queryHandler ?? throw new ArgumentNullException(nameof(queryHandler));
    }


    /// <summary>
    /// Get or search a list of rewards
    /// </summary>
    /// <remarks>By passing in the appropriate options, you can search for available rewards in the system </remarks>
    /// <param name="searchTerm">pass an optional search string for looking up rewards</param>
    /// <param name="RestaurantId">id of restaurant to look up for reward's</param>
    /// <param name="pageNumber">page number</param>
    /// <param name="pageSize">maximum number of records to return per page</param>
    /// <response code="200">search results matching criteria</response>
    /// <response code="400">bad request</response>
    [HttpGet("/v1/reward/")]
    [Authorize]
    [ProducesResponseType(typeof(SearchRewardResponse), 200)]
    public async Task<IActionResult> SearchReward(
        [FromQuery]string searchTerm,
        [FromQuery]Guid? RestaurantId,
        [FromQuery][Range(0, 50)]int? pageSize = 20,
        [FromQuery]int? pageNumber = 1)
    {
        // NOTE: Please ensure the API returns the response codes annotated above

        var criteria = new SearchReward(
            correlationId: GetCorrelationId(),
            searchText: searchTerm,
            restaurantId: RestaurantId,
            pageSize: pageSize,
            pageNumber: pageNumber
        );

        var result = await queryHandler.ExecuteAsync(criteria);

        var response = SearchRewardResponse.FromRewardResultItem(result);

        return new ObjectResult(response);
    }
}
