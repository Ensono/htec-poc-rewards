using Newtonsoft.Json;
using Shouldly;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HTEC.POC.API.FunctionalTests.Builders;
using HTEC.POC.API.FunctionalTests.Configuration;
using HTEC.POC.API.FunctionalTests.Models;
using HTEC.POC.Tests.Api.Builders.Http;

namespace HTEC.POC.API.FunctionalTests.Tests.Steps;

/// <summary>
/// These are the steps required for testing the Category endpoints
/// </summary>
public class ItemSteps
{
    private readonly RewardsSteps rewardsSteps = new RewardsSteps();
    private readonly CategorySteps categorySteps = new CategorySteps();
    private readonly string baseUrl;
    private HttpResponseMessage lastResponse;
    private string existingRewardsId;
    private string existingCategoryId;
    private string existingItemId;
    private RewardsItemRequest createItemRequest;
    private RewardsItemRequest updateItemRequest;
    private const string categoryPath = "/category/";
    private const string itemPath = "/items/";

    public ItemSteps()
    {
        var config = ConfigAccessor.GetApplicationConfiguration();
        baseUrl = config.BaseUrl;
    }

    #region Step Definitions

    #region Given

    public void GivenIHaveSpecfiedAFullItem()
    {
        createItemRequest = new RewardsItemBuilder()
            .SetDefaultValues("Yumido Test Item")
            .Build();
    }

    #endregion Given

    #region When

    public async Task WhenISendAnUpdateItemRequest()
    {
        updateItemRequest = new RewardsItemBuilder()
            .WithName("Updated item name")
            .WithDescription("Updated item description")
            .WithPrice(4.5)
            .WithAvailablity(true)
            .Build();
        String path =
            $"{RewardsSteps.rewardsPath}{existingRewardsId}{categoryPath}{existingCategoryId}{itemPath}{existingItemId}";

        lastResponse = await HttpRequestFactory.Put(baseUrl, path, updateItemRequest);
    }

    public async Task WhenICreateTheItemForAnExistingRewardsAndCategory()
    {
        existingRewardsId = await rewardsSteps.GivenARewardsAlreadyExists();
        existingCategoryId = await categorySteps.CreateCategoryForSpecificRewards(existingRewardsId);

        lastResponse = await HttpRequestFactory.Post(baseUrl,
            $"{RewardsSteps.rewardsPath}{existingRewardsId}{categoryPath}{existingCategoryId}{itemPath}", createItemRequest);
        existingItemId = JsonConvert
            .DeserializeObject<CreateObjectResponse>(await lastResponse.Content.ReadAsStringAsync()).id;
    }

    public async Task WhenIDeleteTheItem()
    {
        lastResponse = await HttpRequestFactory.Delete(baseUrl,
            $"{RewardsSteps.rewardsPath}{existingRewardsId}{categoryPath}{existingCategoryId}{itemPath}{existingItemId}");
    }

    #endregion When

    #region Then

    public void ThenTheItemHasBeenCreated()
    {
        lastResponse.StatusCode.ShouldBe(HttpStatusCode.Created,
            $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");
    }

    public async Task ThenTheItemHasBeenDeleted()
    {
        lastResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent,
            $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");

        var getCurrentRewards = await HttpRequestFactory.Get(baseUrl, $"{RewardsSteps.rewardsPath}{existingRewardsId}");
        if (getCurrentRewards.StatusCode == HttpStatusCode.OK)
        {
            var getCurrentRewardsResponse =
                JsonConvert.DeserializeObject<Rewards>(await getCurrentRewards.Content.ReadAsStringAsync());

            getCurrentRewardsResponse.categories[0].items.ShouldBeEmpty();
        }
    }

    public async Task ThenTheItemIsUpdatedCorrectly()
    {
        lastResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent,
            $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");

        var updatedResponse = await HttpRequestFactory.Get(baseUrl, $"{RewardsSteps.rewardsPath}{existingRewardsId}");

        if (updatedResponse.StatusCode == HttpStatusCode.OK)
        {
            var updateItemResponse =
                JsonConvert.DeserializeObject<Rewards>(await updatedResponse.Content.ReadAsStringAsync());


            updateItemResponse.categories[0].items[0].name.ShouldBe(updateItemRequest.name,
                $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the rewards as expected");

            updateItemResponse.categories[0].items[0].description.ShouldBe(updateItemRequest.description,
                $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the rewards as expected");

            updateItemResponse.categories[0].items[0].price.ShouldBe(updateItemRequest.price.ToString(),
                $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the rewards as expected");
            updateItemResponse.categories[0].items[0].available.ShouldBeTrue();
        }
        else
        {
            throw new Exception($"Could not retrieve the updated rewards using GET /rewards/{existingRewardsId}");
        }
    }

    #endregion Then

    #endregion Step Definitions
}