using System.Collections.Generic;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Htec.Poc.Application.CQRS.Events;
using Htec.Poc.Application.Integration;
using Htec.Poc.CQRS.Commands;
using Htec.Poc.Domain;

namespace Htec.Poc.Application.CommandHandlers;

public class UpdateRewardCommandHandler : RewardCommandHandlerBase<UpdateReward, bool>
{
    public UpdateRewardCommandHandler(IRewardRepository repository, IApplicationEventPublisher applicationEventPublisher)
        : base(repository, applicationEventPublisher)
    {
    }

    public override Task<bool> HandleCommandAsync(Reward reward, UpdateReward command)
    {
        reward.Update(command.Name, command.Description, command.Enabled);

        return Task.FromResult(true);
    }

    public override IEnumerable<IApplicationEvent> RaiseApplicationEvents(Reward reward, UpdateReward command)
    {
        return new IApplicationEvent[] {
            new RewardUpdatedEvent(command, command.RewardId)
        };
    }
}
