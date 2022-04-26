using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HTEC.POC.API.Models.Responses;
using HTEC.POC.CQRS.Queries.SearchRewards;

namespace HTEC.POC.API.Controllers;

/// <summary>
/// Rewards related operations
/// </summary>
[Produces("application/json")]
[Consumes("application/json")]
[ApiExplorerSettings(GroupName = "Rewards")]
[ApiController]
public class SearchRewardsController : ApiControllerBase
{
    readonly IQueryHandler<SearchRewards, SearchRewardsResult> queryHandler;

    public SearchRewardsController(IQueryHandler<SearchRewards, SearchRewardsResult> queryHandler)
    {
        this.queryHandler = queryHandler ?? throw new ArgumentNullException(nameof(queryHandler));
    }


    /// <summary>
    /// Get or search a list of rewardss
    /// </summary>
    /// <remarks>By passing in the appropriate options, you can search for available rewardss in the system </remarks>
    /// <param name="searchTerm">pass an optional search string for looking up rewardss</param>
    /// <param name="RestaurantId">id of restaurant to look up for rewards's</param>
    /// <param name="pageNumber">page number</param>
    /// <param name="pageSize">maximum number of records to return per page</param>
    /// <response code="200">search results matching criteria</response>
    /// <response code="400">bad request</response>
    [HttpGet("/v1/rewards/")]
    [Authorize]
    [ProducesResponseType(typeof(SearchRewardsResponse), 200)]
    public async Task<IActionResult> SearchRewards(
        [FromQuery]string searchTerm,
        [FromQuery]Guid? RestaurantId,
        [FromQuery][Range(0, 50)]int? pageSize = 20,
        [FromQuery]int? pageNumber = 1)
    {
        // NOTE: Please ensure the API returns the response codes annotated above

        var criteria = new SearchRewards(
            correlationId: GetCorrelationId(),
            searchText: searchTerm,
            restaurantId: RestaurantId,
            pageSize: pageSize,
            pageNumber: pageNumber
        );

        var result = await queryHandler.ExecuteAsync(criteria);

        var response = SearchRewardsResponse.FromRewardsResultItem(result);

        return new ObjectResult(response);
    }
}
