using System.Collections.Generic;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using HTEC.POC.Application.CQRS.Events;
using HTEC.POC.Application.Integration;
using HTEC.POC.CQRS.Commands;
using HTEC.POC.Domain;

namespace HTEC.POC.Application.CommandHandlers;

public class DeleteCategoryCommandHandler : RewardsCommandHandlerBase<DeleteCategory, bool>
{
    public DeleteCategoryCommandHandler(IRewardsRepository repository, IApplicationEventPublisher applicationEventPublisher)
        : base(repository, applicationEventPublisher)
    {
    }

    public override Task<bool> HandleCommandAsync(Rewards rewards, DeleteCategory command)
    {
        rewards.RemoveCategory(command.CategoryId);

        return Task.FromResult(true);
    }

    public override IEnumerable<IApplicationEvent> RaiseApplicationEvents(Rewards rewards, DeleteCategory command)
    {
        return new IApplicationEvent[] {
            new RewardsUpdatedEvent(command, command.RewardsId),
            new CategoryDeletedEvent(command, command.RewardsId, command.CategoryId)
        };
    }
}
