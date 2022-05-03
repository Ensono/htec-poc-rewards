using System;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Application.CQRS.Commands;
using Htec.Poc.Application.CQRS.Events;
using Htec.Poc.Application.Integration;
using Htec.Poc.CQRS.Commands;
using Htec.Poc.CQRS.Commands.Models;
using Htec.Poc.Domain;

namespace Htec.Poc.Application.CommandHandlers;

public class CalculateRewardCommandHandler : ICommandHandler<CalculateReward, CalculateRewardResult>
{
    private readonly IApplicationEventPublisher applicationEventPublisher;
    private readonly IRulesEngine rulesEngine;

    public CalculateRewardCommandHandler(IApplicationEventPublisher applicationEventPublisher, IRulesEngine rulesEngine)
    {
        this.applicationEventPublisher = applicationEventPublisher;
        this.rulesEngine = rulesEngine;
    }

    public async Task<CalculateRewardResult> HandleAsync(CalculateReward command)
    {
        var points = await rulesEngine.CalculateReward(command.Basket.ToEntity());
 
        //await applicationEventPublisher.PublishAsync(new RewardCalculatedEvent(command, command.MemberId, points));

        return new CalculateRewardResult(command.MemberId, points);
    }
}
