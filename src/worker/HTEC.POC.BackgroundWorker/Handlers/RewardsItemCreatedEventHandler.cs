using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Extensions.Logging;
using HTEC.POC.Application.CQRS.Events;

namespace HTEC.POC.BackgroundWorker.Handlers;

public class RewardsItemCreatedEventHandler : IApplicationEventHandler<RewardsItemCreatedEvent>
{
    private readonly ILogger<RewardsItemCreatedEventHandler> log;

    public RewardsItemCreatedEventHandler(ILogger<RewardsItemCreatedEventHandler> log)
    {
        this.log = log;
    }

    public Task HandleAsync(RewardsItemCreatedEvent appEvent)
    {
        log.LogInformation($"Executing RewardsItemCreatedEventHandler...");
        return Task.CompletedTask;
    }
}
