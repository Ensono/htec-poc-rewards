using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Extensions.Logging;
using HTEC.POC.Application.CQRS.Events;

namespace HTEC.POC.BackgroundWorker.Handlers;

public class RewardsDeletedEventHandler : IApplicationEventHandler<RewardsDeletedEvent>
{
    private readonly ILogger<RewardsDeletedEventHandler> log;

    public RewardsDeletedEventHandler(ILogger<RewardsDeletedEventHandler> log)
    {
        this.log = log;
    }

    public Task HandleAsync(RewardsDeletedEvent appEvent)
    {
        log.LogInformation($"Executing RewardsDeletedEventHandler...");
        return Task.CompletedTask;
    }
}
