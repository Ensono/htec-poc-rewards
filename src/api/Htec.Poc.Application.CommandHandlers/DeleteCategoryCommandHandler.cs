using System.Collections.Generic;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Htec.Poc.Application.CQRS.Events;
using Htec.Poc.Application.Integration;
using Htec.Poc.CQRS.Commands;
using Htec.Poc.Domain;

namespace Htec.Poc.Application.CommandHandlers;

public class DeleteCategoryCommandHandler : RewardCommandHandlerBase<DeleteCategory, bool>
{
    public DeleteCategoryCommandHandler(IRewardRepository repository, IApplicationEventPublisher applicationEventPublisher)
        : base(repository, applicationEventPublisher)
    {
    }

    public override Task<bool> HandleCommandAsync(Reward reward, DeleteCategory command)
    {
        reward.RemoveCategory(command.CategoryId);

        return Task.FromResult(true);
    }

    public override IEnumerable<IApplicationEvent> RaiseApplicationEvents(Reward reward, DeleteCategory command)
    {
        return new IApplicationEvent[] {
            new RewardUpdatedEvent(command, command.RewardId),
            new CategoryDeletedEvent(command, command.RewardId, command.CategoryId)
        };
    }
}
