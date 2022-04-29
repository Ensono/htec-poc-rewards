using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Extensions.Logging;
using Htec.Poc.Application.CQRS.Events;

namespace Htec.Poc.BackgroundWorker.Handlers;

public class RewardItemCreatedEventHandler : IApplicationEventHandler<RewardItemCreatedEvent>
{
    private readonly ILogger<RewardItemCreatedEventHandler> log;

    public RewardItemCreatedEventHandler(ILogger<RewardItemCreatedEventHandler> log)
    {
        this.log = log;
    }

    public Task HandleAsync(RewardItemCreatedEvent appEvent)
    {
        log.LogInformation($"Executing RewardItemCreatedEventHandler...");
        return Task.CompletedTask;
    }
}
