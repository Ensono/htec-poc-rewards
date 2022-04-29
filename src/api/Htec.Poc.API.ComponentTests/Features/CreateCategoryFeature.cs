using Xbehave;
using Htec.Poc.API.ComponentTests.Fixtures;

namespace Htec.Poc.API.ComponentTests.Features;

public class CreateCategoryFeature
{
    /* SCENARIOS: Create a category in the reward
      
         Examples: 
         ---------------------------------------------------------------------------------
        | AsRole              | Existing Reward | Existing Category  | Outcome              |
        |---------------------|---------------|--------------------|----------------------|
        | Admin               | Yes           | No                 | 200 OK               |
        | Employee            | Yes           | No                 | 200 OK               |
        | Admin               | No            | No                 | 404 Not Found        |
        | Admin               | Yes           | Yes                | 409 Conflict         |
        | Customer            | Yes           | No                 | 403 Forbidden        |
        | UnauthenticatedUser | Yes           | No                 | 403 Forbidden        |

    */

    [Scenario]
    [CustomInlineAutoData("Admin")]
    [CustomInlineAutoData("Employee")]
    public void CreateCategoryShouldSucceed(string role, CreateCategoryFixture fixture)
    {
        $"Given the user is authenticated and has the {role} role".x(() => fixture.GivenTheUserIsAuthenticatedAndHasRole(role));
        "And an existing reward".x(fixture.GivenAnExistingReward);
        "And the reward belongs to the user restaurant".x(fixture.GivenTheRewardBelongsToUserRestaurant);
        "And the category being created does not exist in the reward".x(fixture.GivenTheCategoryDoesNotExist);
        "When a new category is submitted".x(fixture.WhenTheCategoryIsSubmitted);
        "Then a successful response is returned".x(fixture.ThenASuccessfulResponseIsReturned);
        "And the reward is loaded from the storage".x(fixture.ThenRewardIsLoadedFromStorage);
        "And the id of the new category is returned".x(fixture.ThenTheResourceCreatedResponseIsReturned);
        "And the category is added to the reward".x(fixture.ThenTheCategoryIsAddedToReward);
        "And the reward is persisted to the storage".x(fixture.ThenTheRewardShouldBePersisted);
        "And the event RewardUpdate is Raised".x(fixture.ThenARewardUpdatedEventIsRaised);
        "And the event CategoryCreated is Raised".x(fixture.ThenACategoryCreatedEventIsRaised);
    }

    [Scenario]
    [CustomInlineAutoData("Admin")]
    public void CreateCategoryShouldFailWhenRewardDoesNotExist(string role, CreateCategoryFixture fixture)
    {
        $"Given the user is authenticated and has the {role} role".x(() => fixture.GivenTheUserIsAuthenticatedAndHasRole(role));
        "And a reward does not exist".x(fixture.GivenARewardDoesNotExist);
        "When a new category is submitted".x(fixture.WhenTheCategoryIsSubmitted);
        "Then a failure response is returned".x(fixture.ThenAFailureResponseIsReturned);
        "And the response code is NotFound".x(fixture.ThenANotFoundResponseIsReturned);
        "And the reward is loaded from the storage".x(fixture.ThenRewardIsLoadedFromStorage);
        "And the reward is not persisted to the storage".x(fixture.ThenTheRewardShouldNotBePersisted);
        "And the event RewardUpdate should NOT be raised".x(fixture.ThenARewardUpdatedEventIsNotRaised);
        "And the event CategoryCreated should not be Raised".x(fixture.ThenACategoryCreatedEventIsNotRaised);
    }

    [Scenario]
    [CustomInlineAutoData("Admin")]
    public void CreateCategoryShouldFailWhenCategoryAlreadyExists(string role, CreateCategoryFixture fixture)
    {
        $"Given the user is authenticated and has the {role} role".x(() => fixture.GivenTheUserIsAuthenticatedAndHasRole(role));
        "And an existing reward".x(fixture.GivenAnExistingReward);
        "And the reward belongs to the user restaurant".x(fixture.GivenTheRewardBelongsToUserRestaurant);
        "And the category being created already exist in the reward".x(fixture.GivenTheCategoryAlreadyExist);
        "When a new category is submitted".x(fixture.WhenTheCategoryIsSubmitted);
        "Then a failure response is returned".x(fixture.ThenAFailureResponseIsReturned);
        "And the response code is Conflict".x(fixture.ThenAConflictResponseIsReturned);
        "And the reward is NOT persisted to the storage".x(fixture.ThenTheRewardShouldNotBePersisted);
        "And the event RewardUpdate should NOT be raised".x(fixture.ThenARewardUpdatedEventIsNotRaised);
        "And the event CategoryCreated is NOT Raised".x(fixture.ThenACategoryCreatedEventIsNotRaised);
    }

    [Scenario(Skip = "Disabled until security is implemented")]
    [CustomInlineAutoData("Customer")]
    [CustomInlineAutoData("UnauthenticatedUser")]
    public void CreateCategoryShouldFailWithForbidden(string role, CreateCategoryFixture fixture)
    {
        $"Given the user is authenticated and has the {role} role".x(() => fixture.GivenTheUserIsAuthenticatedAndHasRole(role));
        "And an existing reward".x(fixture.GivenAnExistingReward);
        "And the category being created does not exist in the reward".x(fixture.GivenTheCategoryDoesNotExist);
        "When a new category is submitted".x(fixture.WhenTheCategoryIsSubmitted);
        "Then a Forbidden response is returned".x(fixture.ThenAForbiddenResponseIsReturned);
        "And the reward is not persisted to the storage".x(fixture.ThenTheRewardShouldNotBePersisted);
        "And the event RewardUpdate is NOT Raised".x(fixture.ThenARewardUpdatedEventIsNotRaised);
        "And the event CategoryCreated is NOT Raised".x(fixture.ThenACategoryCreatedEventIsNotRaised);
    }


    [Scenario(Skip = "Disabled until security is implemented")]
    [CustomInlineAutoData("Admin")]
    [CustomInlineAutoData("Employee")]
    public void CreateCategoryShouldFailWhenRewardDoesNotBelongToUser(string role, CreateCategoryFixture fixture)
    {
        $"Given the user is authenticated and has the {role} role".x(() => fixture.GivenTheUserIsAuthenticatedAndHasRole(role));
        "And an existing reward".x(fixture.GivenAnExistingReward);
        "And the reward does not belong to users restaurant".x(fixture.GivenTheRewardDoesNotBelongToUserRestaurant);
        "When a new category is submitted".x(fixture.WhenTheCategoryIsSubmitted);
        "Then a failure response is returned".x(fixture.ThenAFailureResponseIsReturned);
        "And the response code is NotFound".x(fixture.ThenANotFoundResponseIsReturned);
        "And the reward is loaded from the storage".x(fixture.ThenRewardIsLoadedFromStorage);
        "And the reward is not persisted to the storage".x(fixture.ThenTheRewardShouldNotBePersisted);
        "And the event RewardUpdate should not be Raised".x(fixture.ThenARewardUpdatedEventIsNotRaised);
        "And the event CategoryCreated should not be Raised".x(fixture.ThenACategoryCreatedEventIsNotRaised);
    }

}
