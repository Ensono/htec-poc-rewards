using System;
using System.Threading.Tasks;
using HTEC.POC.Domain;

namespace HTEC.POC.Application.Integration;

public interface IRewardsRepository
{
    Task<Rewards> GetByIdAsync(Guid id);
    Task<bool> SaveAsync(Rewards entity);
    Task<bool> DeleteAsync(Guid id);
}
