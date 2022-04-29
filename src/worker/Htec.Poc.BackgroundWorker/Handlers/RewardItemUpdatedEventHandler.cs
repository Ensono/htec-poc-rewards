using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Extensions.Logging;
using Htec.Poc.Application.CQRS.Events;

namespace Htec.Poc.BackgroundWorker.Handlers;

public class RewardItemUpdatedEventHandler : IApplicationEventHandler<RewardItemUpdatedEvent>
{
    private readonly ILogger<RewardItemUpdatedEventHandler> log;

    public RewardItemUpdatedEventHandler(ILogger<RewardItemUpdatedEventHandler> log)
    {
        this.log = log;
    }

    public Task HandleAsync(RewardItemUpdatedEvent appEvent)
    {
        log.LogInformation($"Executing RewardItemUpdatedEventHandler...");
        return Task.CompletedTask;
    }
}
