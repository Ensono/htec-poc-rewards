using TestStack.BDDfy;
using Xunit;
using HTEC.POC.API.FunctionalTests.Tests.Fixtures;
using HTEC.POC.API.FunctionalTests.Tests.Steps;

namespace HTEC.POC.API.FunctionalTests.Tests.Functional;

//Define the story/feature being tested
[Story(
    AsA = "restaurant administrator",
    IWant = "to be able to create rewardss",
    SoThat = "customers know what we have on offer")]

public class CreateRewardsTests : IClassFixture<AuthFixture>
{
    private readonly RewardsSteps steps;
    private readonly AuthFixture fixture;

    public CreateRewardsTests(AuthFixture fixture)
    {
        //Get instances of the fixture and steps required for the test
        this.fixture = fixture;
        steps = new RewardsSteps();
    }

    //Add all tests that make up the story to this class.
    [Fact]
    public void Create_a_rewards()
    {
        this.Given(step => fixture.GivenAUser())
            .Given(step => steps.GivenIHaveSpecfiedAFullRewards())
            .When(step => steps.WhenICreateTheRewards())
            .Then(step => steps.ThenTheRewardsHasBeenCreated())
            //.Then(step => steps.ThenSomeActionIsMade())
            //This step is to verify the outcome of the event in the Subcriber. (e.g. a field is updated in DB, blob is created and so on).
            .BDDfy();
    }
}