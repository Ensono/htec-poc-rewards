using System.Collections.Generic;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using HTEC.POC.Application.CQRS.Events;
using HTEC.POC.Application.Integration;
using HTEC.POC.CQRS.Commands;
using HTEC.POC.Domain;

namespace HTEC.POC.Application.CommandHandlers;

public class DeleteRewardsItemCommandHandler : RewardsCommandHandlerBase<DeleteRewardsItem, bool>
{
    public DeleteRewardsItemCommandHandler(IRewardsRepository repository, IApplicationEventPublisher applicationEventPublisher)
        : base(repository, applicationEventPublisher)
    {
    }

    public override Task<bool> HandleCommandAsync(Rewards rewards, DeleteRewardsItem command)
    {
        rewards.RemoveRewardsItem(command.CategoryId, command.RewardsItemId);

        return Task.FromResult(true);
    }

    public override IEnumerable<IApplicationEvent> RaiseApplicationEvents(Rewards rewards, DeleteRewardsItem command)
    {
        return new IApplicationEvent[] {
            new RewardsUpdatedEvent(command, command.RewardsId),
            new CategoryUpdatedEvent(command, command.RewardsId, command.CategoryId),
            new RewardsItemDeletedEvent(command, command.RewardsId, command.CategoryId, command.RewardsItemId)
        };
    }
}
