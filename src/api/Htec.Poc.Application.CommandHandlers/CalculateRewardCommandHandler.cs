using System;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Application.CQRS.Commands;
using Htec.Poc.Application.CQRS.Events;
using Htec.Poc.Application.Integration;
using Htec.Poc.CQRS.Commands;
using Htec.Poc.Domain;

namespace Htec.Poc.Application.CommandHandlers;

public class CalculateRewardCommandHandler : ICommandHandler<CalculateReward, Guid>
{
    private readonly IApplicationEventPublisher applicationEventPublisher;
    private readonly IRulesEngine rulesEngine;

    public CalculateRewardCommandHandler(IApplicationEventPublisher applicationEventPublisher, IRulesEngine rulesEngine)
    {
        this.applicationEventPublisher = applicationEventPublisher;
        this.rulesEngine = rulesEngine;
    }

    public async Task<Guid> HandleAsync(CalculateReward command)
    {
        var id = Guid.NewGuid();

        // TODO: CALCULATE REWARD
        if (!await rulesEngine.CalculateReward())
        {
            throw new Exception();
        }

        await applicationEventPublisher.PublishAsync(new RewardCalculatedEvent(command, id));

        return id;
    }
}
