using Xunit;

namespace HTEC.POC.API.ComponentTests.Features;

[Trait("TestType", "ComponentTests")]
public class DeleteRewardsFeature
{
    /* SCENARIOS: Delete a rewards
      
         Examples: 
         -------------------------------------------------------------------------
        | AsRole                        | Rewards Condition   | Outcome              |
        |-------------------------------|------------------|----------------------|
        | Admin, Employee               | Valid Rewards       | 204 No Content       |
        | Admin                         | Invalid Rewards     | 400 Bad  Request     |
        | Admin                         | Rewards not exist   | 404 Bad  Request     |
        | Customer, UnauthenticatedUser | Valid Rewards       | 403 Forbidden        |

    */

    //TODO: Implement test scenarios
}
