using TestStack.BDDfy;
using Xunit;
using Htec.Poc.API.FunctionalTests.Tests.Fixtures;
using Htec.Poc.API.FunctionalTests.Tests.Steps;

namespace Htec.Poc.API.FunctionalTests.Tests.Functional;

//Define the story/feature being tested
[Story(AsA = "Administrator for a restaurant",
    IWant = "To be able to update existing rewards",
    SoThat = "The rewards are always showing our latest offerings"
)]
public class UpdateRewardById : IClassFixture<AuthFixture>
{
    private readonly AuthFixture fixture;
    private readonly RewardSteps steps;

    public UpdateRewardById(AuthFixture fixture)
    {
        //Get instances of the fixture and steps required for the test
        this.fixture = fixture;
        steps = new RewardSteps();
    }

    //Add all tests that make up the story to this class
    [Fact]
    public void Admins_Can_Update_Existing_Rewards()
    {
        this.Given(s => fixture.GivenAnAdmin())
            .And(s => steps.GivenARewardAlreadyExists())
            .When(s => steps.WhenISendAnUpdateRewardRequest())
            .Then(s => steps.ThenTheRewardIsUpdatedCorrectly())
            //Then(step => categorySteps.ThenSomeActionIsMade())
            //This step is to verify the outcome of the event in the Subcriber. (e.g. a field is updated in DB, blob is created and so on).
            .BDDfy();
    }
}