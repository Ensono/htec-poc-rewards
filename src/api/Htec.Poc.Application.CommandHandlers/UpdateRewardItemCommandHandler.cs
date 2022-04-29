using System.Collections.Generic;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Htec.Poc.Application.CQRS.Events;
using Htec.Poc.Application.Integration;
using Htec.Poc.CQRS.Commands;
using Htec.Poc.Domain;

namespace Htec.Poc.Application.CommandHandlers;

public class UpdateRewardItemCommandHandler : RewardCommandHandlerBase<UpdateRewardItem, bool>
{
    public UpdateRewardItemCommandHandler(IRewardRepository repository, IApplicationEventPublisher applicationEventPublisher)
        : base(repository, applicationEventPublisher)
    {
    }

    public override Task<bool> HandleCommandAsync(Reward reward, UpdateRewardItem command)
    {
        reward.UpdateRewardItem(
            command.CategoryId,
            command.RewardItemId,
            command.Name,
            command.Description,
            command.Price,
            command.Available
        );

        return Task.FromResult(true);
    }

    public override IEnumerable<IApplicationEvent> RaiseApplicationEvents(Reward reward, UpdateRewardItem command)
    {
        return new IApplicationEvent[] {
            new RewardUpdatedEvent(command, command.RewardId),
            //new CategoryUpdated(command, command.RewardId, command.CategoryId),
            new RewardItemUpdatedEvent(command, command.RewardId, command.CategoryId, command.RewardItemId)
        };
    }
}
