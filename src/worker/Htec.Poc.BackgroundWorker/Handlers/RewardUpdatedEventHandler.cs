using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Extensions.Logging;
using Htec.Poc.Application.CQRS.Events;

namespace Htec.Poc.BackgroundWorker.Handlers;

public class RewardUpdatedEventHandler : IApplicationEventHandler<RewardUpdatedEvent>
{
    private readonly ILogger<RewardUpdatedEventHandler> log;

    public RewardUpdatedEventHandler(ILogger<RewardUpdatedEventHandler> log)
    {
        this.log = log;
    }

    public Task HandleAsync(RewardUpdatedEvent appEvent)
    {
        log.LogInformation($"Executing RewardUpdatedEventHandler...");
        return Task.CompletedTask;
    }
}
