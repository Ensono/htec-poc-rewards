using System.Collections.Generic;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using HTEC.POC.Application.CQRS.Events;
using HTEC.POC.Application.Integration;
using HTEC.POC.CQRS.Commands;
using HTEC.POC.Domain;

namespace HTEC.POC.Application.CommandHandlers;

public class UpdateCategoryCommandHandler : RewardsCommandHandlerBase<UpdateCategory, bool>
{
    public UpdateCategoryCommandHandler(IRewardsRepository repository, IApplicationEventPublisher applicationEventPublisher)
        : base(repository, applicationEventPublisher)
    {
    }

    public override Task<bool> HandleCommandAsync(Rewards rewards, UpdateCategory command)
    {
        rewards.UpdateCategory(command.CategoryId, command.Name, command.Description);

        return Task.FromResult(true);
    }

    public override IEnumerable<IApplicationEvent> RaiseApplicationEvents(Rewards rewards, UpdateCategory command)
    {
        return new IApplicationEvent[] {
            new RewardsUpdatedEvent(command, command.RewardsId),
            new CategoryUpdatedEvent(command, command.RewardsId, command.CategoryId)
        };
    }
}
