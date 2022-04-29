using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Htec.Poc.Application.CQRS.Events;
using Htec.Poc.Application.Integration;
using Htec.Poc.CQRS.Commands;
using Htec.Poc.Domain;

namespace Htec.Poc.Application.CommandHandlers;

public class CreateCategoryCommandHandler : RewardCommandHandlerBase<CreateCategory, Guid>
{
    public CreateCategoryCommandHandler(IRewardRepository repository, IApplicationEventPublisher applicationEventPublisher)
        : base(repository, applicationEventPublisher)
    {
    }

    Guid id;
    public override Task<Guid> HandleCommandAsync(Reward reward, CreateCategory command)
    {
        id = Guid.NewGuid();

        reward.AddCategory(
            id,
            command.Name,
            command.Description
        );

        return Task.FromResult(id);
    }

    public override IEnumerable<IApplicationEvent> RaiseApplicationEvents(Reward reward, CreateCategory command)
    {
        return new IApplicationEvent[] {
            new RewardUpdatedEvent(command, command.RewardId),
            new CategoryCreatedEvent(command, command.RewardId, id)
        };
    }
}
