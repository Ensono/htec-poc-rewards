using Htec.Poc.Domain.Entities;
using System.Threading.Tasks;

namespace Htec.Poc.Application.Integration;

public interface IRulesEngine
{
    Task<int> CalculateReward(Basket basket);

}