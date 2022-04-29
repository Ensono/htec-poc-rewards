using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Htec.Poc.Application.CQRS.Events;
using Htec.Poc.Application.Integration;
using Htec.Poc.CQRS.Commands;
using Htec.Poc.Domain;

namespace Htec.Poc.Application.CommandHandlers;

public class CreateRewardItemCommandHandler : RewardCommandHandlerBase<CreateRewardItem, Guid>
{
    public CreateRewardItemCommandHandler(IRewardRepository repository, IApplicationEventPublisher applicationEventPublisher)
        : base(repository, applicationEventPublisher)
    {
    }

    Guid id;
    public override Task<Guid> HandleCommandAsync(Reward reward, CreateRewardItem command)
    {
        id = Guid.NewGuid();

        reward.AddRewardItem(
            command.CategoryId,
            id,
            command.Name,
            command.Description,
            command.Price,
            command.Available
        );

        return Task.FromResult(id);
    }

    public override IEnumerable<IApplicationEvent> RaiseApplicationEvents(Reward reward, CreateRewardItem command)
    {
        return new IApplicationEvent[] {
            new RewardUpdatedEvent(command, command.RewardId),
            new CategoryUpdatedEvent(command, command.RewardId, command.CategoryId),
            new RewardItemCreatedEvent(command, command.RewardId, command.CategoryId, id)
        };
    }
}
