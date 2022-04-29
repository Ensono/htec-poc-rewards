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
public class CategorySteps
{
    private readonly string baseUrl;
    private HttpResponseMessage lastResponse;
    private string existingRewardId;
    private CategoryRequest createCategoryRequest;
    private CategoryRequest updateCategoryRequest;
    private string existingCategoryId;
    private const string categoryPath = "/category/";
    private readonly RewardSteps rewardSteps = new RewardSteps();

    public CategorySteps()
    {
        var config = ConfigAccessor.GetApplicationConfiguration();
        baseUrl = config.BaseUrl;
    }

    #region Step Definitions

    #region Given

    public void GivenIHaveSpecfiedAFullCategory()
    {
        createCategoryRequest = new CategoryRequestBuilder()
            .SetDefaultValues("Yumido Test Category")
            .Build();
    }

    #endregion Given

    #region When

    public async Task<string> WhenICreateTheCategoryForAnExistingReward()
    {
        existingRewardId = await rewardSteps.GivenARewardAlreadyExists();

        lastResponse = await HttpRequestFactory.Post(baseUrl,
            $"{RewardSteps.rewardPath}{existingRewardId}{categoryPath}", createCategoryRequest);

        existingCategoryId = JsonConvert
            .DeserializeObject<CreateObjectResponse>(await lastResponse.Content.ReadAsStringAsync()).id;
        return existingCategoryId;
    }

    public async Task<string> CreateCategoryForSpecificReward(String rewardId)
    {
        lastResponse = await HttpRequestFactory.Post(baseUrl,
            $"{RewardSteps.rewardPath}{rewardId}{categoryPath}",
            new CategoryRequestBuilder()
                .SetDefaultValues("Yumido Test Category")
                .Build());

        existingCategoryId = JsonConvert
            .DeserializeObject<CreateObjectResponse>(await lastResponse.Content.ReadAsStringAsync()).id;
        return existingCategoryId;
    }

    public async Task WhenISendAnUpdateCategoryRequest()
    {
        updateCategoryRequest = new CategoryRequestBuilder()
            .WithName("Updated Category Name")
            .WithDescription("Updated Category Description")
            .Build();
        String path = $"{RewardSteps.rewardPath}{rewardSteps.existingRewardId}{categoryPath}{existingCategoryId}";

        lastResponse = await HttpRequestFactory.Put(baseUrl, path, updateCategoryRequest);
    }

    public async Task WhenIDeleteTheCategory()
    {
        lastResponse = await HttpRequestFactory.Delete(baseUrl,
            $"{RewardSteps.rewardPath}{existingRewardId}{categoryPath}{existingCategoryId}");
    }

    #endregion When

    #region Then

    public void ThenTheCategoryHasBeenCreated()
    {
        lastResponse.StatusCode.ShouldBe(HttpStatusCode.Created,
            $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");
    }

    public async Task ThenTheCategoryHasBeenDeleted()
    {
        lastResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent,
            $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");

        var getCurrentReward = await HttpRequestFactory.Get(baseUrl, $"{RewardSteps.rewardPath}{existingRewardId}");
        if (getCurrentReward.StatusCode == HttpStatusCode.OK)
        {
            var getCurrentRewardResponse =
                JsonConvert.DeserializeObject<Reward>(await getCurrentReward.Content.ReadAsStringAsync());

            getCurrentRewardResponse.categories.ShouldBeEmpty();
        }
    }

    public async Task ThenTheCategoryIsUpdatedCorrectly()
    {
        lastResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent,
            $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");

        var updatedResponse = await HttpRequestFactory.Get(baseUrl, $"{RewardSteps.rewardPath}{existingRewardId}");

        if (updatedResponse.StatusCode == HttpStatusCode.OK)
        {
            var updateCategoryResponse =
                JsonConvert.DeserializeObject<Reward>(await updatedResponse.Content.ReadAsStringAsync());


            updateCategoryResponse.categories[0].name.ShouldBe(updateCategoryRequest.name,
                $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the reward as expected");

            updateCategoryResponse.categories[0].description.ShouldBe(updateCategoryRequest.description,
                $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the reward as expected");
        }
        else
        {
            throw new Exception($"Could not retrieve the updated reward using GET /reward/{existingRewardId}");
        }
    }

    #endregion Then

    #endregion Step Definitions
}