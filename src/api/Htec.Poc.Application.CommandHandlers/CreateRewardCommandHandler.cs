using System;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Application.CQRS.Commands;
using Htec.Poc.Application.CQRS.Events;
using Htec.Poc.Application.Integration;
using Htec.Poc.CQRS.Commands;
using Htec.Poc.Domain;

namespace Htec.Poc.Application.CommandHandlers;

public class CreateRewardCommandHandler : ICommandHandler<CreateReward, Guid>
{
    private readonly IRewardRepository repository;
    private readonly IApplicationEventPublisher applicationEventPublisher;

    public CreateRewardCommandHandler(IRewardRepository repository, IApplicationEventPublisher applicationEventPublisher)
    {
        this.repository = repository;
        this.applicationEventPublisher = applicationEventPublisher;
    }

    public async Task<Guid> HandleAsync(CreateReward command)
    {
        var id = Guid.NewGuid();

        var newReward = new Reward(
            id: id,
            name: command.Name,
            tenantId: command.TenantId,
            description: command.Description,
            enabled: command.Enabled
        );

        await repository.SaveAsync(newReward);

        await applicationEventPublisher.PublishAsync(new RewardCreatedEvent(command, id));

        return id;
    }
}
