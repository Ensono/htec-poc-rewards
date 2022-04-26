using TestStack.BDDfy;
using Xunit;
using HTEC.POC.API.FunctionalTests.Tests.Fixtures;
using HTEC.POC.API.FunctionalTests.Tests.Steps;

namespace HTEC.POC.API.FunctionalTests.Tests.Functional;

//Define the story/feature being tested
[Story(
    AsA = "user of the Yumido website",
    IWant = "to be able to view specific rewardss",
    SoThat = "I can choose what to eat")]
public class GetRewardsByIdTests : IClassFixture<AuthFixture>
{
    private readonly AuthFixture fixture;
    private readonly RewardsSteps steps;

    public GetRewardsByIdTests(AuthFixture fixture)
    {
        //Get instances of the fixture and steps required for the test
        this.fixture = fixture;
        steps = new RewardsSteps();
    }

    //Add all tests that make up the story to this class.
    [Fact]
    public void Users_Can_View_Existing_Rewardss()
    {
        this.Given(s => fixture.GivenAUser())
            .And(s => steps.GivenARewardsAlreadyExists())
            .When(s => steps.WhenIGetARewards())
            .Then(s => steps.ThenICanReadTheRewardsReturned())
            .BDDfy();
    }

    [Fact]
    [Trait("Category", "SmokeTest")]
    public void Admins_Can_View_Existing_Rewardss()
    {
        this.Given(s => fixture.GivenAnAdmin())
            .And(s => steps.GivenARewardsAlreadyExists())
            .When(s => steps.WhenIGetARewards())
            .Then(s => steps.ThenICanReadTheRewardsReturned())
            .BDDfy();
    }
}