using System;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Application.CQRS.Commands;
using Htec.Poc.Application.CQRS.Events;
using Htec.Poc.CQRS.Commands;
using Htec.Poc.Domain;

namespace Htec.Poc.Application.CommandHandlers;

public class CalculateRewardCommandHandler : ICommandHandler<CalculateReward, Guid>
{
    private readonly IApplicationEventPublisher applicationEventPublisher;

    public CalculateRewardCommandHandler(IApplicationEventPublisher applicationEventPublisher)
    {
        this.applicationEventPublisher = applicationEventPublisher;
    }

    public async Task<Guid> HandleAsync(CalculateReward command)
    {
        var id = Guid.NewGuid();

        // TODO: CALCULATE REWARD

        var newReward = new Reward(
            id: id,
            name: command.Name,
            tenantId: command.TenantId,
            description: command.Description,
            categories: null,
            enabled: command.Enabled
        );

        await applicationEventPublisher.PublishAsync(new RewardCalculatedEvent(command, id));

        return id;
    }
}
