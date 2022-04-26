using HTEC.POC.Application.CQRS.Events;
using Xbehave;
using Xunit;
using HTEC.POC.API.ComponentTests.Fixtures;

namespace HTEC.POC.API.ComponentTests.Features;

[Trait("TestType", "ComponentTests")]
public class CreateRewardsFeature
{
    /* SCENARIOS: Create a rewards

         Examples:
         -----------------------------------------------------------------
        | AsRole              | Rewards Condition     | Outcome              |
        |---------------------|--------------------|----------------------|
        | Admin               | Valid Rewards         | 201 Resource Create  |
        | Admin               | Invalid Rewards       | 400 Bad  Request     |
        | Employee, Customer,                                             |
        | UnauthenticatedUser | Valid Rewards         | 403 Forbidden        |

    */

    [Scenario, CustomAutoData]
    public void CreateRewardsAsAdminForValidRewardsShouldSucceed(CreateRewardsFixture fixture)
    {
        "Given the user is authenticated and has the Admin role".x(() => fixture.GivenTheUserIsAnAuthenticatedAdministrator());
        "And a valid rewards being submitted".x(fixture.GivenAValidRewards);
        "And the rewards does not does not exist".x(fixture.GivenARewardsDoesNotExist);
        "When the rewards is submitted".x(fixture.WhenTheRewardsCreationIsSubmitted);
        "Then a successful response is returned".x(fixture.ThenASuccessfulResponseIsReturned);
        "And the response code is CREATED".x(fixture.ThenACreatedResponseIsReturned);
        "And the id of the new rewards is returned".x(fixture.ThenTheResourceCreatedResponseIsReturned);
        "And the rewards data is submitted correctly to the database".x(fixture.ThenTheRewardsIsSubmittedToDatabase);
        $"And an event of type {nameof(RewardsCreatedEvent)} is raised".x(fixture.ThenARewardsCreatedEventIsRaised);
    }

    [Scenario, CustomAutoData]
    public void CreateRewardsAsAdminForInvalidRewardsShouldFail(CreateRewardsFixture fixture)
    {
        "Given the user is authenticated and has the Admin role".x(() => fixture.GivenTheUserIsAnAuthenticatedAdministrator());
        "And a valid rewards being submitted".x(fixture.GivenAInvalidRewards);
        "And the rewards does not does not exist".x(fixture.GivenARewardsDoesNotExist);
        "When the rewards is submitted".x(fixture.WhenTheRewardsCreationIsSubmitted);
        "Then a failure response is returned".x(fixture.ThenAFailureResponseIsReturned);
        "And the rewards is not submitted to the database".x(fixture.ThenTheRewardsIsNotSubmittedToDatabase);
        $"And an event of type {nameof(RewardsCreatedEvent)} should not be raised".x(fixture.ThenARewardsCreatedEventIsNotRaised);
    }

    [Scenario(Skip = "Only works when Auth is implemented")]
    [CustomInlineAutoData("Employee")]
    [CustomInlineAutoData("Customer")]
    [CustomInlineAutoData("UnauthenticatedUser")]
    public void CreateRewardsAsNonAdminForValidRewardsShouldFail(string role, CreateRewardsFixture fixture)
    {
        $"Given the user is authenticated and has the {role} role".x(() => fixture.GivenTheUserIsAuthenticatedAndHasRole(role));
        "And a valid rewards being submitted".x(fixture.GivenAValidRewards);
        "And the rewards does not does not exist".x(fixture.GivenARewardsDoesNotExist);
        "When the rewards is submitted".x(fixture.WhenTheRewardsCreationIsSubmitted);
        "Then a Forbidden response is returned".x(fixture.ThenAForbiddenResponseIsReturned);
        "And the rewards is not submitted to the database".x(fixture.ThenTheRewardsIsNotSubmittedToDatabase);
        $"And an event of type {nameof(RewardsCreatedEvent)} should not be raised".x(fixture.ThenARewardsCreatedEventIsNotRaised);
    }
}
