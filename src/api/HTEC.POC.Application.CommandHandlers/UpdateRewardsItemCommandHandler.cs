using System.Collections.Generic;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using HTEC.POC.Application.CQRS.Events;
using HTEC.POC.Application.Integration;
using HTEC.POC.CQRS.Commands;
using HTEC.POC.Domain;

namespace HTEC.POC.Application.CommandHandlers;

public class UpdateRewardsItemCommandHandler : RewardsCommandHandlerBase<UpdateRewardsItem, bool>
{
    public UpdateRewardsItemCommandHandler(IRewardsRepository repository, IApplicationEventPublisher applicationEventPublisher)
        : base(repository, applicationEventPublisher)
    {
    }

    public override Task<bool> HandleCommandAsync(Rewards rewards, UpdateRewardsItem command)
    {
        rewards.UpdateRewardsItem(
            command.CategoryId,
            command.RewardsItemId,
            command.Name,
            command.Description,
            command.Price,
            command.Available
        );

        return Task.FromResult(true);
    }

    public override IEnumerable<IApplicationEvent> RaiseApplicationEvents(Rewards rewards, UpdateRewardsItem command)
    {
        return new IApplicationEvent[] {
            new RewardsUpdatedEvent(command, command.RewardsId),
            //new CategoryUpdated(command, command.RewardsId, command.CategoryId),
            new RewardsItemUpdatedEvent(command, command.RewardsId, command.CategoryId, command.RewardsItemId)
        };
    }
}
