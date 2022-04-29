using Newtonsoft.Json;
using Shouldly;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Htec.Poc.API.FunctionalTests.Builders;
using Htec.Poc.API.FunctionalTests.Configuration;
using Htec.Poc.API.FunctionalTests.Models;
using Htec.Poc.Tests.Api.Builders.Http;

namespace Htec.Poc.API.FunctionalTests.Tests.Steps;

/// <summary>
/// These are the steps required for testing the Category endpoints
/// </summary>
public class ItemSteps
{
    private readonly RewardSteps rewardSteps = new RewardSteps();
    private readonly CategorySteps categorySteps = new CategorySteps();
    private readonly string baseUrl;
    private HttpResponseMessage lastResponse;
    private string existingRewardId;
    private string existingCategoryId;
    private string existingItemId;
    private RewardItemRequest createItemRequest;
    private RewardItemRequest updateItemRequest;
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
        createItemRequest = new RewardItemBuilder()
            .SetDefaultValues("Yumido Test Item")
            .Build();
    }

    #endregion Given

    #region When

    public async Task WhenISendAnUpdateItemRequest()
    {
        updateItemRequest = new RewardItemBuilder()
            .WithName("Updated item name")
            .WithDescription("Updated item description")
            .WithPrice(4.5)
            .WithAvailablity(true)
            .Build();
        String path =
            $"{RewardSteps.rewardPath}{existingRewardId}{categoryPath}{existingCategoryId}{itemPath}{existingItemId}";

        lastResponse = await HttpRequestFactory.Put(baseUrl, path, updateItemRequest);
    }

    public async Task WhenICreateTheItemForAnExistingRewardAndCategory()
    {
        existingRewardId = await rewardSteps.GivenARewardAlreadyExists();
        existingCategoryId = await categorySteps.CreateCategoryForSpecificReward(existingRewardId);

        lastResponse = await HttpRequestFactory.Post(baseUrl,
            $"{RewardSteps.rewardPath}{existingRewardId}{categoryPath}{existingCategoryId}{itemPath}", createItemRequest);
        existingItemId = JsonConvert
            .DeserializeObject<CreateObjectResponse>(await lastResponse.Content.ReadAsStringAsync()).id;
    }

    public async Task WhenIDeleteTheItem()
    {
        lastResponse = await HttpRequestFactory.Delete(baseUrl,
            $"{RewardSteps.rewardPath}{existingRewardId}{categoryPath}{existingCategoryId}{itemPath}{existingItemId}");
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

        var getCurrentReward = await HttpRequestFactory.Get(baseUrl, $"{RewardSteps.rewardPath}{existingRewardId}");
        if (getCurrentReward.StatusCode == HttpStatusCode.OK)
        {
            var getCurrentRewardResponse =
                JsonConvert.DeserializeObject<Reward>(await getCurrentReward.Content.ReadAsStringAsync());

            getCurrentRewardResponse.categories[0].items.ShouldBeEmpty();
        }
    }

    public async Task ThenTheItemIsUpdatedCorrectly()
    {
        lastResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent,
            $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");

        var updatedResponse = await HttpRequestFactory.Get(baseUrl, $"{RewardSteps.rewardPath}{existingRewardId}");

        if (updatedResponse.StatusCode == HttpStatusCode.OK)
        {
            var updateItemResponse =
                JsonConvert.DeserializeObject<Reward>(await updatedResponse.Content.ReadAsStringAsync());


            updateItemResponse.categories[0].items[0].name.ShouldBe(updateItemRequest.name,
                $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the reward as expected");

            updateItemResponse.categories[0].items[0].description.ShouldBe(updateItemRequest.description,
                $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the reward as expected");

            updateItemResponse.categories[0].items[0].price.ShouldBe(updateItemRequest.price.ToString(),
                $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the reward as expected");
            updateItemResponse.categories[0].items[0].available.ShouldBeTrue();
        }
        else
        {
            throw new Exception($"Could not retrieve the updated reward using GET /reward/{existingRewardId}");
        }
    }

    #endregion Then

    #endregion Step Definitions
}