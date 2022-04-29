using TestStack.BDDfy;
using Xunit;
using Htec.Poc.API.FunctionalTests.Tests.Fixtures;
using Htec.Poc.API.FunctionalTests.Tests.Steps;

namespace Htec.Poc.API.FunctionalTests.Tests.Functional;

//Define the story/feature being tested
[Story(
    AsA = "user of the Yumido website",
    IWant = "to be able to view specific rewards",
    SoThat = "I can choose what to eat")]
public class GetRewardByIdTests : IClassFixture<AuthFixture>
{
    private readonly AuthFixture fixture;
    private readonly RewardSteps steps;

    public GetRewardByIdTests(AuthFixture fixture)
    {
        //Get instances of the fixture and steps required for the test
        this.fixture = fixture;
        steps = new RewardSteps();
    }

    //Add all tests that make up the story to this class.
    [Fact]
    public void Users_Can_View_Existing_Rewards()
    {
        this.Given(s => fixture.GivenAUser())
            .And(s => steps.GivenARewardAlreadyExists())
            .When(s => steps.WhenIGetAReward())
            .Then(s => steps.ThenICanReadTheRewardReturned())
            .BDDfy();
    }

    [Fact]
    [Trait("Category", "SmokeTest")]
    public void Admins_Can_View_Existing_Rewards()
    {
        this.Given(s => fixture.GivenAnAdmin())
            .And(s => steps.GivenARewardAlreadyExists())
            .When(s => steps.WhenIGetAReward())
            .Then(s => steps.ThenICanReadTheRewardReturned())
            .BDDfy();
    }
}