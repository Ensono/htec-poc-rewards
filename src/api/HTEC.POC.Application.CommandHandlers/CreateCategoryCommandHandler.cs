using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using HTEC.POC.Application.CQRS.Events;
using HTEC.POC.Application.Integration;
using HTEC.POC.CQRS.Commands;
using HTEC.POC.Domain;

namespace HTEC.POC.Application.CommandHandlers;

public class CreateCategoryCommandHandler : RewardsCommandHandlerBase<CreateCategory, Guid>
{
    public CreateCategoryCommandHandler(IRewardsRepository repository, IApplicationEventPublisher applicationEventPublisher)
        : base(repository, applicationEventPublisher)
    {
    }

    Guid id;
    public override Task<Guid> HandleCommandAsync(Rewards rewards, CreateCategory command)
    {
        id = Guid.NewGuid();

        rewards.AddCategory(
            id,
            command.Name,
            command.Description
        );

        return Task.FromResult(id);
    }

    public override IEnumerable<IApplicationEvent> RaiseApplicationEvents(Rewards rewards, CreateCategory command)
    {
        return new IApplicationEvent[] {
            new RewardsUpdatedEvent(command, command.RewardsId),
            new CategoryCreatedEvent(command, command.RewardsId, id)
        };
    }
}
