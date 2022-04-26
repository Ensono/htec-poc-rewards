using Xbehave;
using HTEC.POC.API.ComponentTests.Fixtures;

namespace HTEC.POC.API.ComponentTests.Features;

public class CreateCategoryFeature
{
    /* SCENARIOS: Create a category in the rewards
      
         Examples: 
         ---------------------------------------------------------------------------------
        | AsRole              | Existing Rewards | Existing Category  | Outcome              |
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
        "And an existing rewards".x(fixture.GivenAnExistingRewards);
        "And the rewards belongs to the user restaurant".x(fixture.GivenTheRewardsBelongsToUserRestaurant);
        "And the category being created does not exist in the rewards".x(fixture.GivenTheCategoryDoesNotExist);
        "When a new category is submitted".x(fixture.WhenTheCategoryIsSubmitted);
        "Then a successful response is returned".x(fixture.ThenASuccessfulResponseIsReturned);
        "And the rewards is loaded from the storage".x(fixture.ThenRewardsIsLoadedFromStorage);
        "And the id of the new category is returned".x(fixture.ThenTheResourceCreatedResponseIsReturned);
        "And the category is added to the rewards".x(fixture.ThenTheCategoryIsAddedToRewards);
        "And the rewards is persisted to the storage".x(fixture.ThenTheRewardsShouldBePersisted);
        "And the event RewardsUpdate is Raised".x(fixture.ThenARewardsUpdatedEventIsRaised);
        "And the event CategoryCreated is Raised".x(fixture.ThenACategoryCreatedEventIsRaised);
    }

    [Scenario]
    [CustomInlineAutoData("Admin")]
    public void CreateCategoryShouldFailWhenRewardsDoesNotExist(string role, CreateCategoryFixture fixture)
    {
        $"Given the user is authenticated and has the {role} role".x(() => fixture.GivenTheUserIsAuthenticatedAndHasRole(role));
        "And a rewards does not exist".x(fixture.GivenARewardsDoesNotExist);
        "When a new category is submitted".x(fixture.WhenTheCategoryIsSubmitted);
        "Then a failure response is returned".x(fixture.ThenAFailureResponseIsReturned);
        "And the response code is NotFound".x(fixture.ThenANotFoundResponseIsReturned);
        "And the rewards is loaded from the storage".x(fixture.ThenRewardsIsLoadedFromStorage);
        "And the rewards is not persisted to the storage".x(fixture.ThenTheRewardsShouldNotBePersisted);
        "And the event RewardsUpdate should NOT be raised".x(fixture.ThenARewardsUpdatedEventIsNotRaised);
        "And the event CategoryCreated should not be Raised".x(fixture.ThenACategoryCreatedEventIsNotRaised);
    }

    [Scenario]
    [CustomInlineAutoData("Admin")]
    public void CreateCategoryShouldFailWhenCategoryAlreadyExists(string role, CreateCategoryFixture fixture)
    {
        $"Given the user is authenticated and has the {role} role".x(() => fixture.GivenTheUserIsAuthenticatedAndHasRole(role));
        "And an existing rewards".x(fixture.GivenAnExistingRewards);
        "And the rewards belongs to the user restaurant".x(fixture.GivenTheRewardsBelongsToUserRestaurant);
        "And the category being created already exist in the rewards".x(fixture.GivenTheCategoryAlreadyExist);
        "When a new category is submitted".x(fixture.WhenTheCategoryIsSubmitted);
        "Then a failure response is returned".x(fixture.ThenAFailureResponseIsReturned);
        "And the response code is Conflict".x(fixture.ThenAConflictResponseIsReturned);
        "And the rewards is NOT persisted to the storage".x(fixture.ThenTheRewardsShouldNotBePersisted);
        "And the event RewardsUpdate should NOT be raised".x(fixture.ThenARewardsUpdatedEventIsNotRaised);
        "And the event CategoryCreated is NOT Raised".x(fixture.ThenACategoryCreatedEventIsNotRaised);
    }

    [Scenario(Skip = "Disabled until security is implemented")]
    [CustomInlineAutoData("Customer")]
    [CustomInlineAutoData("UnauthenticatedUser")]
    public void CreateCategoryShouldFailWithForbidden(string role, CreateCategoryFixture fixture)
    {
        $"Given the user is authenticated and has the {role} role".x(() => fixture.GivenTheUserIsAuthenticatedAndHasRole(role));
        "And an existing rewards".x(fixture.GivenAnExistingRewards);
        "And the category being created does not exist in the rewards".x(fixture.GivenTheCategoryDoesNotExist);
        "When a new category is submitted".x(fixture.WhenTheCategoryIsSubmitted);
        "Then a Forbidden response is returned".x(fixture.ThenAForbiddenResponseIsReturned);
        "And the rewards is not persisted to the storage".x(fixture.ThenTheRewardsShouldNotBePersisted);
        "And the event RewardsUpdate is NOT Raised".x(fixture.ThenARewardsUpdatedEventIsNotRaised);
        "And the event CategoryCreated is NOT Raised".x(fixture.ThenACategoryCreatedEventIsNotRaised);
    }


    [Scenario(Skip = "Disabled until security is implemented")]
    [CustomInlineAutoData("Admin")]
    [CustomInlineAutoData("Employee")]
    public void CreateCategoryShouldFailWhenRewardsDoesNotBelongToUser(string role, CreateCategoryFixture fixture)
    {
        $"Given the user is authenticated and has the {role} role".x(() => fixture.GivenTheUserIsAuthenticatedAndHasRole(role));
        "And an existing rewards".x(fixture.GivenAnExistingRewards);
        "And the rewards does not belong to users restaurant".x(fixture.GivenTheRewardsDoesNotBelongToUserRestaurant);
        "When a new category is submitted".x(fixture.WhenTheCategoryIsSubmitted);
        "Then a failure response is returned".x(fixture.ThenAFailureResponseIsReturned);
        "And the response code is NotFound".x(fixture.ThenANotFoundResponseIsReturned);
        "And the rewards is loaded from the storage".x(fixture.ThenRewardsIsLoadedFromStorage);
        "And the rewards is not persisted to the storage".x(fixture.ThenTheRewardsShouldNotBePersisted);
        "And the event RewardsUpdate should not be Raised".x(fixture.ThenARewardsUpdatedEventIsNotRaised);
        "And the event CategoryCreated should not be Raised".x(fixture.ThenACategoryCreatedEventIsNotRaised);
    }

}
