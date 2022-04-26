using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Application.CQRS.Commands;
using HTEC.POC.Application.CQRS.Events;
using HTEC.POC.Application.Integration;
using HTEC.POC.Common.Exceptions;
using HTEC.POC.CQRS.Commands;

namespace HTEC.POC.Application.CommandHandlers;

public class DeleteRewardsCommandHandler : ICommandHandler<DeleteRewards, bool>
{
    readonly IRewardsRepository repository;
    readonly IApplicationEventPublisher applicationEventPublisher;

    public DeleteRewardsCommandHandler(IRewardsRepository repository, IApplicationEventPublisher applicationEventPublisher)
    {
        this.repository = repository;
        this.applicationEventPublisher = applicationEventPublisher;
    }

    public async Task<bool> HandleAsync(DeleteRewards command)
    {
        var rewards = await repository.GetByIdAsync(command.RewardsId);

        if (rewards == null)
            RewardsDoesNotExistException.Raise(command, command.RewardsId);

        // TODO: Check if the user owns the resource before any operation
        // if(command.User.TenantId != rewards.TenantId)
        // {
        //     throw NotAuthorizedException()
        // }

        var successful = await repository.DeleteAsync(command.RewardsId);

        if (!successful)
            OperationFailedException.Raise(command, command.RewardsId, "Unable to delete rewards");

        await applicationEventPublisher.PublishAsync(
            new RewardsDeletedEvent(command, command.RewardsId)
        );

        return successful;
    }
}
