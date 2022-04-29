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
/// These are the steps required for testing the Reward endpoints
/// </summary>
public class RewardSteps
{
    private RewardRequest createRewardRequest;
    private RewardRequest updateRewardRequest;
    private HttpResponseMessage lastResponse;
    public string existingRewardId;
    private readonly string baseUrl;
    public const string rewardPath = "v1/reward/";

    public RewardSteps()
    {
        var config = ConfigAccessor.GetApplicationConfiguration();
        baseUrl = config.BaseUrl;
    }

    #region Step Definitions

    #region Given

    public async Task<string> GivenARewardAlreadyExists()
    {
        createRewardRequest = new RewardRequestBuilder()
            .SetDefaultValues("Yumido Test Reward")
            .Build();

        try
        {
            lastResponse = await HttpRequestFactory.Post(baseUrl, rewardPath, createRewardRequest);

            if (lastResponse.StatusCode == HttpStatusCode.Created)
            {
                existingRewardId = JsonConvert
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
                $"Reward could not be created. API response: {await lastResponse.Content.ReadAsStringAsync()}");
        }

        return existingRewardId;
    }

    public void GivenIHaveSpecfiedAFullReward()
    {
        createRewardRequest = new RewardRequestBuilder()
            .SetDefaultValues("Yumido Test Reward")
            .Build();
    }

    #endregion Given

    #region When

    public async Task WhenISendAnUpdateRewardRequest()
    {
        updateRewardRequest = new RewardRequestBuilder()
            .WithName("Updated Reward Name")
            .WithDescription("Updated Description")
            .SetEnabled(true)
            .Build();

        lastResponse = await HttpRequestFactory.Put(baseUrl, $"{rewardPath}{existingRewardId}", updateRewardRequest);
    }

    public async Task WhenICreateTheReward()
    {
        lastResponse = await HttpRequestFactory.Post(baseUrl, rewardPath, createRewardRequest);
    }

    public async Task WhenIDeleteAReward()
    {
        lastResponse = await HttpRequestFactory.Delete(baseUrl, $"{rewardPath}{existingRewardId}");
    }

    public async Task WhenIGetAReward()
    {
        lastResponse = await HttpRequestFactory.Get(baseUrl, $"{rewardPath}{existingRewardId}");
    }

    #endregion When

    #region Then

    public void ThenTheRewardHasBeenCreated()
    {
        lastResponse.StatusCode.ShouldBe(HttpStatusCode.Created,
            $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");
    }

    public void ThenTheRewardHasBeenDeleted()
    {
        lastResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent,
            $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");
    }

    public async Task ThenICanReadTheRewardReturned()
    {
        lastResponse.StatusCode.ShouldBe(HttpStatusCode.OK,
            $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");

        var responseReward = JsonConvert.DeserializeObject<Reward>(await lastResponse.Content.ReadAsStringAsync());

        //compare the initial request sent to the API against the actual response
        responseReward.name.ShouldBe(createRewardRequest.name,
            $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the reward as expected");

        responseReward.description.ShouldBe(createRewardRequest.description,
            $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the reward as expected");

        responseReward.enabled.ShouldBe(createRewardRequest.enabled,
            $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the reward as expected");
    }

    public async Task ThenTheRewardIsUpdatedCorrectly()
    {
        lastResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent,
            $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");

        var updatedResponse = await HttpRequestFactory.Get(baseUrl, $"{rewardPath}{existingRewardId}");

        if (updatedResponse.StatusCode == HttpStatusCode.OK)
        {
            var updateRewardResponse =
                JsonConvert.DeserializeObject<Reward>(await updatedResponse.Content.ReadAsStringAsync());

            updateRewardResponse.name.ShouldBe(updateRewardRequest.name,
                $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the reward as expected");

            updateRewardResponse.description.ShouldBe(updateRewardRequest.description,
                $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the reward as expected");

            updateRewardResponse.enabled.ShouldBe(updateRewardRequest.enabled,
                $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the reward as expected");
        }
        else
        {
            //throw exception rather than use assertions if the GET request fails as GET is not the subject of the test
            //Assertions should only be used on the subject of the test
            throw new Exception($"Could not retrieve the updated reward using GET /reward/{existingRewardId}");
        }
    }

    #endregion Then

    #endregion Step Definitions
}