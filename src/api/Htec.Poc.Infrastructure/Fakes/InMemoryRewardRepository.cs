using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Htec.Poc.Application.Integration;
using Htec.Poc.Domain;

namespace Htec.Poc.Infrastructure.Fakes;

public class InMemoryRewardRepository : IRewardRepository
{
    private static readonly Object @lock = new Object();
    private static readonly Dictionary<Guid, Reward> storage = new Dictionary<Guid, Reward>();

    public async Task<Reward> GetByIdAsync(Guid id)
    {
        if (storage.ContainsKey(id))
            return await Task.FromResult(storage[id]);
        else
            return await Task.FromResult((Reward)null);
    }

    public async Task<bool> SaveAsync(Reward entity)
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
