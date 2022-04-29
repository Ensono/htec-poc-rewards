using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Application.CQRS.Commands;
using Htec.Poc.Application.CQRS.Events;
using Htec.Poc.Application.Integration;
using Htec.Poc.Common.Exceptions;
using Htec.Poc.CQRS.Commands;

namespace Htec.Poc.Application.CommandHandlers;

public class DeleteRewardCommandHandler : ICommandHandler<DeleteReward, bool>
{
    readonly IRewardRepository repository;
    readonly IApplicationEventPublisher applicationEventPublisher;

    public DeleteRewardCommandHandler(IRewardRepository repository, IApplicationEventPublisher applicationEventPublisher)
    {
        this.repository = repository;
        this.applicationEventPublisher = applicationEventPublisher;
    }

    public async Task<bool> HandleAsync(DeleteReward command)
    {
        var reward = await repository.GetByIdAsync(command.RewardId);

        if (reward == null)
            RewardDoesNotExistException.Raise(command, command.RewardId);

        // TODO: Check if the user owns the resource before any operation
        // if(command.User.TenantId != reward.TenantId)
        // {
        //     throw NotAuthorizedException()
        // }

        var successful = await repository.DeleteAsync(command.RewardId);

        if (!successful)
            OperationFailedException.Raise(command, command.RewardId, "Unable to delete reward");

        await applicationEventPublisher.PublishAsync(
            new RewardDeletedEvent(command, command.RewardId)
        );

        return successful;
    }
}
