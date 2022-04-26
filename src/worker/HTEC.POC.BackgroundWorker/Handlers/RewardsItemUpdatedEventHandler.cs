using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Extensions.Logging;
using HTEC.POC.Application.CQRS.Events;

namespace HTEC.POC.BackgroundWorker.Handlers;

public class RewardsItemUpdatedEventHandler : IApplicationEventHandler<RewardsItemUpdatedEvent>
{
    private readonly ILogger<RewardsItemUpdatedEventHandler> log;

    public RewardsItemUpdatedEventHandler(ILogger<RewardsItemUpdatedEventHandler> log)
    {
        this.log = log;
    }

    public Task HandleAsync(RewardsItemUpdatedEvent appEvent)
    {
        log.LogInformation($"Executing RewardsItemUpdatedEventHandler...");
        return Task.CompletedTask;
    }
}
