using TestStack.BDDfy;
using Xunit;
using HTEC.POC.API.FunctionalTests.Tests.Fixtures;
using HTEC.POC.API.FunctionalTests.Tests.Steps;

namespace HTEC.POC.API.FunctionalTests.Tests.Functional;

//Define the story/feature being tested
[Story(AsA = "Administrator for a restaurant",
    IWant = "To be able to update existing rewardss",
    SoThat = "The rewardss are always showing our latest offerings"
)]
public class UpdateRewardsById : IClassFixture<AuthFixture>
{
    private readonly AuthFixture fixture;
    private readonly RewardsSteps steps;

    public UpdateRewardsById(AuthFixture fixture)
    {
        //Get instances of the fixture and steps required for the test
        this.fixture = fixture;
        steps = new RewardsSteps();
    }

    //Add all tests that make up the story to this class
    [Fact]
    public void Admins_Can_Update_Existing_Rewardss()
    {
        this.Given(s => fixture.GivenAnAdmin())
            .And(s => steps.GivenARewardsAlreadyExists())
            .When(s => steps.WhenISendAnUpdateRewardsRequest())
            .Then(s => steps.ThenTheRewardsIsUpdatedCorrectly())
            //Then(step => categorySteps.ThenSomeActionIsMade())
            //This step is to verify the outcome of the event in the Subcriber. (e.g. a field is updated in DB, blob is created and so on).
            .BDDfy();
    }
}