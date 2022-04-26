using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HTEC.POC.Application.Integration;
using HTEC.POC.Domain;

namespace HTEC.POC.Infrastructure.Fakes;

public class InMemoryRewardsRepository : IRewardsRepository
{
    private static readonly Object @lock = new Object();
    private static readonly Dictionary<Guid, Rewards> storage = new Dictionary<Guid, Rewards>();

    public async Task<Rewards> GetByIdAsync(Guid id)
    {
        if (storage.ContainsKey(id))
            return await Task.FromResult(storage[id]);
        else
            return await Task.FromResult((Rewards)null);
    }

    public async Task<bool> SaveAsync(Rewards entity)
    {
        if (entity == null)
            return await Task.FromResult(false);

        lock (@lock)
        {
            if (storage.ContainsKey(entity.Id))
                storage[entity.Id] = entity;
            else
                storage.Add(entity.Id, entity);
        }

        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        bool result;
        lock (@lock)
        {
            if (!storage.ContainsKey(id))
                return false;

            result = storage.Remove(id);
        }

        return await Task.FromResult(result);
    }

}
