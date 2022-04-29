using System.Collections.Generic;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Htec.Poc.Application.CQRS.Events;
using Htec.Poc.Application.Integration;
using Htec.Poc.CQRS.Commands;
using Htec.Poc.Domain;

namespace Htec.Poc.Application.CommandHandlers;

public class DeleteRewardItemCommandHandler : RewardCommandHandlerBase<DeleteRewardItem, bool>
{
    public DeleteRewardItemCommandHandler(IRewardRepository repository, IApplicationEventPublisher applicationEventPublisher)
        : base(repository, applicationEventPublisher)
    {
    }

    public override Task<bool> HandleCommandAsync(Reward reward, DeleteRewardItem command)
    {
        reward.RemoveRewardItem(command.CategoryId, command.RewardItemId);

        return Task.FromResult(true);
    }

    public override IEnumerable<IApplicationEvent> RaiseApplicationEvents(Reward reward, DeleteRewardItem command)
    {
        return new IApplicationEvent[] {
            new RewardUpdatedEvent(command, command.RewardId),
            new CategoryUpdatedEvent(command, command.RewardId, command.CategoryId),
            new RewardItemDeletedEvent(command, command.RewardId, command.CategoryId, command.RewardItemId)
        };
    }
}
