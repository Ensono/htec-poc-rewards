using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NSubstitute;
using Htec.Poc.Application.Integration;
using Htec.Poc.Domain;

namespace Htec.Poc.API.ContractTests.Fixtures;

public class ProviderStateMiddleware : IMiddleware
{
    private readonly IDictionary<string, Action> providerStates;
    readonly IRewardRepository repository;

    public ProviderStateMiddleware(IRewardRepository repository)
    {
        this.repository = repository;

        //Provider states are from the Given clause in the consumer tests
        providerStates = new Dictionary<string, Action>
        {
            {
                //These are case sensitive. Consumer and Provider should share list of states when states are required
                "An existing reward", 
                ExistingReward
            },
            {
                "A reward does not exist",
                RewardDoesNotExist
            }
        };
    }

    //These functions set up the mocked provider API state by mocking the response the repository gives
    private void ExistingReward()
    {
        var reward = new Fixture().Create<Reward>();

        repository.GetByIdAsync(Arg.Any<Guid>()).Returns(Task.FromResult(reward));
        repository.SaveAsync(Arg.Any<Reward>()).Returns(true);
    }

    private void RewardDoesNotExist()
    {
        repository.GetByIdAsync(Arg.Any<Guid>()).Returns(Task.FromResult((Reward)null));
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.Request.Path.Value == "/provider-states")
        {
            HandleProviderStatesRequest(context);
            await context.Response.WriteAsync(string.Empty);
        }
        else
        {
            await next(context);
        }
    }

    private void HandleProviderStatesRequest(HttpContext context)
    {
        context.Response.StatusCode = (int)HttpStatusCode.OK;

        if (context.Request.Method.ToUpper() == HttpMethod.Post.ToString().ToUpper() &&
            context.Request.Body != null)
        {
            var jsonRequestBody = string.Empty;
            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
            {
                jsonRequestBody = reader.ReadToEnd();
            }

            var providerState = JsonConvert.DeserializeObject<ProviderState>(jsonRequestBody);

            //A null or empty provider state key must be handled
            if (providerState != null && !string.IsNullOrEmpty(providerState.State))
            {
                providerStates[providerState.State].Invoke();
            }
        }
    }

}
