using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Queries;
using HTEC.POC.Application.Integration;
using HTEC.POC.CQRS.Queries.GetRewardsById;

namespace HTEC.POC.Application.QueryHandlers;

public class GetRewardsByIdQueryHandler : IQueryHandler<GetRewardsById, Rewards>
{
    private readonly IRewardsRepository repository;

    public GetRewardsByIdQueryHandler(IRewardsRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Rewards> ExecuteAsync(GetRewardsById criteria)
    {
        var rewards = await repository.GetByIdAsync(criteria.Id);

        if (rewards == null)
            return null;

        //You might prefer using AutoMapper in here
        var result = Rewards.FromDomain(rewards);

        return result;
    }
}
