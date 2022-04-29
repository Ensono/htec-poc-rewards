using TestStack.BDDfy;
using Xunit;
using Htec.Poc.API.FunctionalTests.Tests.Fixtures;
using Htec.Poc.API.FunctionalTests.Tests.Steps;

namespace Htec.Poc.API.FunctionalTests.Tests.Functional;

//Define the story/feature being tested
[Story(
    AsA = "restaurant administrator",
    IWant = "to be able to create rewards",
    SoThat = "customers know what we have on offer")]

public class CreateRewardTests : IClassFixture<AuthFixture>
{
    private readonly RewardSteps steps;
    private readonly AuthFixture fixture;

    public CreateRewardTests(AuthFixture fixture)
    {
        //Get instances of the fixture and steps required for the test
        this.fixture = fixture;
        steps = new RewardSteps();
    }

    //Add all tests that make up the story to this class.
    [Fact]
    public void Create_a_reward()
    {
        this.Given(step => fixture.GivenAUser())
            .Given(step => steps.GivenIHaveSpecfiedAFullReward())
            .When(step => steps.WhenICreateTheReward())
            .Then(step => steps.ThenTheRewardHasBeenCreated())
            //.Then(step => steps.ThenSomeActionIsMade())
            //This step is to verify the outcome of the event in the Subcriber. (e.g. a field is updated in DB, blob is created and so on).
            .BDDfy();
    }
}