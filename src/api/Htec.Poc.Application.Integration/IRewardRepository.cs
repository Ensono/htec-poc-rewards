using System;
using System.Threading.Tasks;
using Htec.Poc.Domain;

namespace Htec.Poc.Application.Integration;

public interface IRewardRepository
{
    Task<Reward> GetByIdAsync(Guid id);
    Task<bool> SaveAsync(Reward entity);
    Task<bool> DeleteAsync(Guid id);
}