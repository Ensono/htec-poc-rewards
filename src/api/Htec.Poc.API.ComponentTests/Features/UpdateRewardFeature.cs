using Xunit;

namespace Htec.Poc.API.ComponentTests.Features;

[Trait("TestType", "ComponentTests")]
public class UpdateRewardFeature
{
    /* SCENARIOS: Update a reward
      
         Examples: 
         -----------------------------------------------------------------------------------
        | AsRole                        | Reward Condition             | Outcome              |
        |-------------------------------|----------------------------|----------------------|
        | Admin, Employee               | Valid Reward                 | 204 No Content       |
        | Admin, Employee               | Reward from other restaurant | 404 Not found        |
        | Admin, Employee               | Invalid Reward               | 400 Bad  Request     | 
        | Customer, UnauthenticatedUser | Valid Reward                 | 403 Forbidden        |

    */

    //TODO: Implement test scenarios
}
