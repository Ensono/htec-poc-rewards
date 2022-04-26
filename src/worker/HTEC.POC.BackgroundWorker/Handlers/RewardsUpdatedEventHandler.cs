using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Extensions.Logging;
using HTEC.POC.Application.CQRS.Events;

namespace HTEC.POC.BackgroundWorker.Handlers;

public class RewardsUpdatedEventHandler : IApplicationEventHandler<RewardsUpdatedEvent>
{
    private readonly ILogger<RewardsUpdatedEventHandler> log;

    public RewardsUpdatedEventHandler(ILogger<RewardsUpdatedEventHandler> log)
    {
        this.log = log;
    }

    public Task HandleAsync(RewardsUpdatedEvent appEvent)
    {
        log.LogInformation($"Executing RewardsUpdatedEventHandler...");
        return Task.CompletedTask;
    }
}
