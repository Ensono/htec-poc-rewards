using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using HTEC.POC.Application.CQRS.Events;
using HTEC.POC.Application.Integration;
using HTEC.POC.CQRS.Commands;
using HTEC.POC.Domain;

namespace HTEC.POC.Application.CommandHandlers;

public class CreateRewardsItemCommandHandler : RewardsCommandHandlerBase<CreateRewardsItem, Guid>
{
    public CreateRewardsItemCommandHandler(IRewardsRepository repository, IApplicationEventPublisher applicationEventPublisher)
        : base(repository, applicationEventPublisher)
    {
    }

    Guid id;
    public override Task<Guid> HandleCommandAsync(Rewards rewards, CreateRewardsItem command)
    {
        id = Guid.NewGuid();

        rewards.AddRewardsItem(
            command.CategoryId,
            id,
            command.Name,
            command.Description,
            command.Price,
            command.Available
        );

        return Task.FromResult(id);
    }

    public override IEnumerable<IApplicationEvent> RaiseApplicationEvents(Rewards rewards, CreateRewardsItem command)
    {
        return new IApplicationEvent[] {
            new RewardsUpdatedEvent(command, command.RewardsId),
            new CategoryUpdatedEvent(command, command.RewardsId, command.CategoryId),
            new RewardsItemCreatedEvent(command, command.RewardsId, command.CategoryId, id)
        };
    }
}
