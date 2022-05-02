using System.Threading.Tasks;

namespace Htec.Poc.Application.Integration;

public interface IRulesEngine
{
    Task<bool> CalculateReward();

}