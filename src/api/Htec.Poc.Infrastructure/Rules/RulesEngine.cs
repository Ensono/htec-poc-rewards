using Htec.Poc.Application.Integration;
using System.Threading.Tasks;

namespace Htec.Poc.Infrastructure.Rules
{
    public class RulesEngine : IRulesEngine
    {
        public Task<bool> CalculateReward()
        {
            return Task.FromResult(true);
        }
    }
}