using System;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Application.CQRS.Commands;
using HTEC.POC.Application.CQRS.Events;
using HTEC.POC.Application.Integration;
using HTEC.POC.CQRS.Commands;
using HTEC.POC.Domain;

namespace HTEC.POC.Application.CommandHandlers;

public class CreateRewardsCommandHandler : ICommandHandler<CreateRewards, Guid>
{
    private readonly IRewardsRepository repository;
    private readonly IApplicationEventPublisher applicationEventPublisher;

    public CreateRewardsCommandHandler(IRewardsRepository repository, IApplicationEventPublisher applicationEventPublisher)
    {
        this.repository = repository;
        this.applicationEventPublisher = applicationEventPublisher;
    }

    public async Task<Guid> HandleAsync(CreateRewards command)
    {
        var id = Guid.NewGuid();

        // TODO: Check if the user owns the resource before any operation
        // if(command.User.TenantId != rewards.TenantId)
        // {
        //     throw NotAuthorizedException()
        // }


        var newRewards = new Rewards(
            id: id,
            name: command.Name,
            tenantId: command.TenantId,
            description: command.Description,
            categories: null,
            enabled: command.Enabled
        );

        await repository.SaveAsync(newRewards);

        await applicationEventPublisher.PublishAsync(new RewardsCreatedEvent(command, id));

        return id;
    }
}
