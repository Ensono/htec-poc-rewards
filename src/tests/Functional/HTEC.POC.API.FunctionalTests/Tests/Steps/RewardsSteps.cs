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
/// These are the steps required for testing the Rewards endpoints
/// </summary>
public class RewardsSteps
{
    private RewardsRequest createRewardsRequest;
    private RewardsRequest updateRewardsRequest;
    private HttpResponseMessage lastResponse;
    public string existingRewardsId;
    private readonly string baseUrl;
    public const string rewardsPath = "v1/rewards/";

    public RewardsSteps()
    {
        var config = ConfigAccessor.GetApplicationConfiguration();
        baseUrl = config.BaseUrl;
    }

    #region Step Definitions

    #region Given

    public async Task<string> GivenARewardsAlreadyExists()
    {
        createRewardsRequest = new RewardsRequestBuilder()
            .SetDefaultValues("Yumido Test Rewards")
            .Build();

        try
        {
            lastResponse = await HttpRequestFactory.Post(baseUrl, rewardsPath, createRewardsRequest);

            if (lastResponse.StatusCode == HttpStatusCode.Created)
            {
                existingRewardsId = JsonConvert
                    .DeserializeObject<CreateObjectResponse>(await lastResponse.Content.ReadAsStringAsync()).id;
            }
            else
            {
                throw new Exception();
            }
        }
        catch
        {
            throw new Exception(
                $"Rewards could not be created. API response: {await lastResponse.Content.ReadAsStringAsync()}");
        }

        return existingRewardsId;
    }

    public void GivenIHaveSpecfiedAFullRewards()
    {
        createRewardsRequest = new RewardsRequestBuilder()
            .SetDefaultValues("Yumido Test Rewards")
            .Build();
    }

    #endregion Given

    #region When

    public async Task WhenISendAnUpdateRewardsRequest()
    {
        updateRewardsRequest = new RewardsRequestBuilder()
            .WithName("Updated Rewards Name")
            .WithDescription("Updated Description")
            .SetEnabled(true)
            .Build();

        lastResponse = await HttpRequestFactory.Put(baseUrl, $"{rewardsPath}{existingRewardsId}", updateRewardsRequest);
    }

    public async Task WhenICreateTheRewards()
    {
        lastResponse = await HttpRequestFactory.Post(baseUrl, rewardsPath, createRewardsRequest);
    }

    public async Task WhenIDeleteARewards()
    {
        lastResponse = await HttpRequestFactory.Delete(baseUrl, $"{rewardsPath}{existingRewardsId}");
    }

    public async Task WhenIGetARewards()
    {
        lastResponse = await HttpRequestFactory.Get(baseUrl, $"{rewardsPath}{existingRewardsId}");
    }

    #endregion When

    #region Then

    public void ThenTheRewardsHasBeenCreated()
    {
        lastResponse.StatusCode.ShouldBe(HttpStatusCode.Created,
            $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");
    }

    public void ThenTheRewardsHasBeenDeleted()
    {
        lastResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent,
            $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");
    }

    public async Task ThenICanReadTheRewardsReturned()
    {
        lastResponse.StatusCode.ShouldBe(HttpStatusCode.OK,
            $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");

        var responseRewards = JsonConvert.DeserializeObject<Rewards>(await lastResponse.Content.ReadAsStringAsync());

        //compare the initial request sent to the API against the actual response
        responseRewards.name.ShouldBe(createRewardsRequest.name,
            $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the rewards as expected");

        responseRewards.description.ShouldBe(createRewardsRequest.description,
            $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the rewards as expected");

        responseRewards.enabled.ShouldBe(createRewardsRequest.enabled,
            $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the rewards as expected");
    }

    public async Task ThenTheRewardsIsUpdatedCorrectly()
    {
        lastResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent,
            $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");

        var updatedResponse = await HttpRequestFactory.Get(baseUrl, $"{rewardsPath}{existingRewardsId}");

        if (updatedResponse.StatusCode == HttpStatusCode.OK)
        {
            var updateRewardsResponse =
                JsonConvert.DeserializeObject<Rewards>(await updatedResponse.Content.ReadAsStringAsync());

            updateRewardsResponse.name.ShouldBe(updateRewardsRequest.name,
                $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the rewards as expected");

            updateRewardsResponse.description.ShouldBe(updateRewardsRequest.description,
                $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the rewards as expected");

            updateRewardsResponse.enabled.ShouldBe(updateRewardsRequest.enabled,
                $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the rewards as expected");
        }
        else
        {
            //throw exception rather than use assertions if the GET request fails as GET is not the subject of the test
            //Assertions should only be used on the subject of the test
            throw new Exception($"Could not retrieve the updated rewards using GET /rewards/{existingRewardsId}");
        }
    }

    #endregion Then

    #endregion Step Definitions
}