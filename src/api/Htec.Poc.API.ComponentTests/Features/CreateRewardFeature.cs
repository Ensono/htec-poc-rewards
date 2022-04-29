using Htec.Poc.Application.CQRS.Events;
using Xbehave;
using Xunit;
using Htec.Poc.API.ComponentTests.Fixtures;

namespace Htec.Poc.API.ComponentTests.Features;

[Trait("TestType", "ComponentTests")]
public class CreateRewardFeature
{
    /* SCENARIOS: Create a reward

         Examples:
         -----------------------------------------------------------------
        | AsRole              | Reward Condition     | Outcome              |
        |---------------------|--------------------|----------------------|
        | Admin               | Valid Reward         | 201 Resource Create  |
        | Admin               | Invalid Reward       | 400 Bad  Request     |
        | Employee, Customer,                                             |
        | UnauthenticatedUser | Valid Reward         | 403 Forbidden        |

    */

    [Scenario, CustomAutoData]
    public void CreateRewardAsAdminForValidRewardShouldSucceed(CreateRewardFixture fixture)
    {
        "Given the user is authenticated and has the Admin role".x(() => fixture.GivenTheUserIsAnAuthenticatedAdministrator());
        "And a valid reward being submitted".x(fixture.GivenAValidReward);
        "And the reward does not does not exist".x(fixture.GivenARewardDoesNotExist);
        "When the reward is submitted".x(fixture.WhenTheRewardCreationIsSubmitted);
        "Then a successful response is returned".x(fixture.ThenASuccessfulResponseIsReturned);
        "And the response code is CREATED".x(fixture.ThenACreatedResponseIsReturned);
        "And the id of the new reward is returned".x(fixture.ThenTheResourceCreatedResponseIsReturned);
        "And the reward data is submitted correctly to the database".x(fixture.ThenTheRewardIsSubmittedToDatabase);
        $"And an event of type {nameof(RewardCreatedEvent)} is raised".x(fixture.ThenARewardCreatedEventIsRaised);
    }

    [Scenario, CustomAutoData]
    public void CreateRewardAsAdminForInvalidRewardShouldFail(CreateRewardFixture fixture)
    {
        "Given the user is authenticated and has the Admin role".x(() => fixture.GivenTheUserIsAnAuthenticatedAdministrator());
        "And a valid reward being submitted".x(fixture.GivenAInvalidReward);
        "And the reward does not does not exist".x(fixture.GivenARewardDoesNotExist);
        "When the reward is submitted".x(fixture.WhenTheRewardCreationIsSubmitted);
        "Then a failure response is returned".x(fixture.ThenAFailureResponseIsReturned);
        "And the reward is not submitted to the database".x(fixture.ThenTheRewardIsNotSubmittedToDatabase);
        $"And an event of type {nameof(RewardCreatedEvent)} should not be raised".x(fixture.ThenARewardCreatedEventIsNotRaised);
    }

    [Scenario(Skip = "Only works when Auth is implemented")]
    [CustomInlineAutoData("Employee")]
    [CustomInlineAutoData("Customer")]
    [CustomInlineAutoData("UnauthenticatedUser")]
    public void CreateRewardAsNonAdminForValidRewardShouldFail(string role, CreateRewardFixture fixture)
    {
        $"Given the user is authenticated and has the {role} role".x(() => fixture.GivenTheUserIsAuthenticatedAndHasRole(role));
        "And a valid reward being submitted".x(fixture.GivenAValidReward);
        "And the reward does not does not exist".x(fixture.GivenARewardDoesNotExist);
        "When the reward is submitted".x(fixture.WhenTheRewardCreationIsSubmitted);
        "Then a Forbidden response is returned".x(fixture.ThenAForbiddenResponseIsReturned);
        "And the reward is not submitted to the database".x(fixture.ThenTheRewardIsNotSubmittedToDatabase);
        $"And an event of type {nameof(RewardCreatedEvent)} should not be raised".x(fixture.ThenARewardCreatedEventIsNotRaised);
    }
}
