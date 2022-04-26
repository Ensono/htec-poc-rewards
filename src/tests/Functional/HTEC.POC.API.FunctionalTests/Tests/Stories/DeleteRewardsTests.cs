using TestStack.BDDfy;
using Xunit;
using HTEC.POC.API.FunctionalTests.Tests.Fixtures;
using HTEC.POC.API.FunctionalTests.Tests.Steps;

namespace HTEC.POC.API.FunctionalTests.Tests.Functional;

//Define the story/feature being tested
[Story(AsA = "Administrator of a restaurant",
    IWant = "To be able to delete old rewardss",
    SoThat = "Customers do not see out of date options")]
public class DeleteRewardsTests : IClassFixture<AuthFixture>
{
    private readonly RewardsSteps steps;
    private readonly AuthFixture fixture;

    public DeleteRewardsTests(AuthFixture fixture)
    {
        //Get instances of the fixture and steps required for the test
        this.fixture = fixture;
        steps = new RewardsSteps();
    }

    //Add all tests that make up the story to this class
    [Fact]
    public void Admins_Can_Delete_Rewardss()
    {
        this.Given(step => fixture.GivenAUser())
            .And(step => steps.GivenARewardsAlreadyExists())
            .When(step => steps.WhenIDeleteARewards())
            .Then(step => steps.ThenTheRewardsHasBeenDeleted())
            //Then(step => steps.ThenSomeActionIsMade())
            //This step is to verify the outcome of the event in the Subcriber. (e.g. a field is updated in DB).
            .BDDfy();
    }
}