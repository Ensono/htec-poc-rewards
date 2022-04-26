using Xunit;

namespace HTEC.POC.API.ComponentTests.Features;

[Trait("TestType", "ComponentTests")]
public class UpdateRewardsFeature
{
    /* SCENARIOS: Update a rewards
      
         Examples: 
         -----------------------------------------------------------------------------------
        | AsRole                        | Rewards Condition             | Outcome              |
        |-------------------------------|----------------------------|----------------------|
        | Admin, Employee               | Valid Rewards                 | 204 No Content       |
        | Admin, Employee               | Rewards from other restaurant | 404 Not found        |
        | Admin, Employee               | Invalid Rewards               | 400 Bad  Request     | 
        | Customer, UnauthenticatedUser | Valid Rewards                 | 403 Forbidden        |

    */

    //TODO: Implement test scenarios
}
