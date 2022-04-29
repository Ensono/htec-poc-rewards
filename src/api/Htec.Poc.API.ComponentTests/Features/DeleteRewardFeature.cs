using Xunit;

namespace Htec.Poc.API.ComponentTests.Features;

[Trait("TestType", "ComponentTests")]
public class DeleteRewardFeature
{
    /* SCENARIOS: Delete a reward
      
         Examples: 
         -------------------------------------------------------------------------
        | AsRole                        | Reward Condition   | Outcome              |
        |-------------------------------|------------------|----------------------|
        | Admin, Employee               | Valid Reward       | 204 No Content       |
        | Admin                         | Invalid Reward     | 400 Bad  Request     |
        | Admin                         | Reward not exist   | 404 Bad  Request     |
        | Customer, UnauthenticatedUser | Valid Reward       | 403 Forbidden        |

    */

    //TODO: Implement test scenarios
}
