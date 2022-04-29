using TestStack.BDDfy;
using Xunit;
using Htec.Poc.API.FunctionalTests.Tests.Fixtures;
using Htec.Poc.API.FunctionalTests.Tests.Steps;

namespace Htec.Poc.API.FunctionalTests.Tests.Functional;

//Define the story/feature being tested
[Story(AsA = "Administrator of a restaurant",
    IWant = "To be able to delete old rewards",
    SoThat = "Customers do not see out of date options")]
public class DeleteRewardTests : IClassFixture<AuthFixture>
{
    private readonly RewardSteps steps;
    private readonly AuthFixture fixture;

    public DeleteRewardTests(AuthFixture fixture)
    {
        //Get instances of the fixture and steps required for the test
        this.fixture = fixture;
        steps = new RewardSteps();
    }

    //Add all tests that make up the story to this class
    [Fact]
    public void Admins_Can_Delete_Rewards()
    {
        this.Given(step => fixture.GivenAUser())
            .And(step => steps.GivenARewardAlreadyExists())
            .When(step => steps.WhenIDeleteAReward())
            .Then(step => steps.ThenTheRewardHasBeenDeleted())
            //Then(step => steps.ThenSomeActionIsMade())
            //This step is to verify the outcome of the event in the Subcriber. (e.g. a field is updated in DB).
            .BDDfy();
    }
}