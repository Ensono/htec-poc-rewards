using System.Collections.Generic;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Htec.Poc.Application.CQRS.Events;
using Htec.Poc.Application.Integration;
using Htec.Poc.CQRS.Commands;
using Htec.Poc.Domain;

namespace Htec.Poc.Application.CommandHandlers;

public class UpdateCategoryCommandHandler : RewardCommandHandlerBase<UpdateCategory, bool>
{
    public UpdateCategoryCommandHandler(IRewardRepository repository, IApplicationEventPublisher applicationEventPublisher)
        : base(repository, applicationEventPublisher)
    {
    }

    public override Task<bool> HandleCommandAsync(Reward reward, UpdateCategory command)
    {
        reward.UpdateCategory(command.CategoryId, command.Name, command.Description);

        return Task.FromResult(true);
    }

    public override IEnumerable<IApplicationEvent> RaiseApplicationEvents(Reward reward, UpdateCategory command)
    {
        return new IApplicationEvent[] {
            new RewardUpdatedEvent(command, command.RewardId),
            new CategoryUpdatedEvent(command, command.RewardId, command.CategoryId)
        };
    }
}
