using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Queries;
using Htec.Poc.Application.Integration;
using Htec.Poc.CQRS.Queries.GetRewardById;

namespace Htec.Poc.Application.QueryHandlers;

public class GetRewardByIdQueryHandler : IQueryHandler<GetRewardById, Reward>
{
    private readonly IRewardRepository repository;

    public GetRewardByIdQueryHandler(IRewardRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Reward> ExecuteAsync(GetRewardById criteria)
    {
        var reward = await repository.GetByIdAsync(criteria.Id);

        if (reward == null)
            return null;

        //You might prefer using AutoMapper in here
        var result = Reward.FromDomain(reward);

        return result;
    }
}
