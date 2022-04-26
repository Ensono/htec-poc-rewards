using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Extensions.Logging;
using HTEC.POC.Application.CQRS.Events;

namespace HTEC.POC.BackgroundWorker.Handlers;

public class RewardsCreatedEventHandler : IApplicationEventHandler<RewardsCreatedEvent>
{
    private readonly ILogger<RewardsCreatedEventHandler> log;

    public RewardsCreatedEventHandler(ILogger<RewardsCreatedEventHandler> log)
    {
        this.log = log;
    }

    public Task HandleAsync(RewardsCreatedEvent appEvent)
    {
        log.LogInformation($"Executing RewardsCreatedEventHandler...");
        return Task.CompletedTask;
    }
}
