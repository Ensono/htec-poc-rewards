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
public class CategorySteps
{
    private readonly string baseUrl;
    private HttpResponseMessage lastResponse;
    private string existingRewardsId;
    private CategoryRequest createCategoryRequest;
    private CategoryRequest updateCategoryRequest;
    private string existingCategoryId;
    private const string categoryPath = "/category/";
    private readonly RewardsSteps rewardsSteps = new RewardsSteps();

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

    public async Task<string> WhenICreateTheCategoryForAnExistingRewards()
    {
        existingRewardsId = await rewardsSteps.GivenARewardsAlreadyExists();

        lastResponse = await HttpRequestFactory.Post(baseUrl,
            $"{RewardsSteps.rewardsPath}{existingRewardsId}{categoryPath}", createCategoryRequest);

        existingCategoryId = JsonConvert
            .DeserializeObject<CreateObjectResponse>(await lastResponse.Content.ReadAsStringAsync()).id;
        return existingCategoryId;
    }

    public async Task<string> CreateCategoryForSpecificRewards(String rewardsId)
    {
        lastResponse = await HttpRequestFactory.Post(baseUrl,
            $"{RewardsSteps.rewardsPath}{rewardsId}{categoryPath}",
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
        String path = $"{RewardsSteps.rewardsPath}{rewardsSteps.existingRewardsId}{categoryPath}{existingCategoryId}";

        lastResponse = await HttpRequestFactory.Put(baseUrl, path, updateCategoryRequest);
    }

    public async Task WhenIDeleteTheCategory()
    {
        lastResponse = await HttpRequestFactory.Delete(baseUrl,
            $"{RewardsSteps.rewardsPath}{existingRewardsId}{categoryPath}{existingCategoryId}");
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

        var getCurrentRewards = await HttpRequestFactory.Get(baseUrl, $"{RewardsSteps.rewardsPath}{existingRewardsId}");
        if (getCurrentRewards.StatusCode == HttpStatusCode.OK)
        {
            var getCurrentRewardsResponse =
                JsonConvert.DeserializeObject<Rewards>(await getCurrentRewards.Content.ReadAsStringAsync());

            getCurrentRewardsResponse.categories.ShouldBeEmpty();
        }
    }

    public async Task ThenTheCategoryIsUpdatedCorrectly()
    {
        lastResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent,
            $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");

        var updatedResponse = await HttpRequestFactory.Get(baseUrl, $"{RewardsSteps.rewardsPath}{existingRewardsId}");

        if (updatedResponse.StatusCode == HttpStatusCode.OK)
        {
            var updateCategoryResponse =
                JsonConvert.DeserializeObject<Rewards>(await updatedResponse.Content.ReadAsStringAsync());


            updateCategoryResponse.categories[0].name.ShouldBe(updateCategoryRequest.name,
                $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the rewards as expected");

            updateCategoryResponse.categories[0].description.ShouldBe(updateCategoryRequest.description,
                $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the rewards as expected");
        }
        else
        {
            throw new Exception($"Could not retrieve the updated rewards using GET /rewards/{existingRewardsId}");
        }
    }

    #endregion Then

    #endregion Step Definitions
}