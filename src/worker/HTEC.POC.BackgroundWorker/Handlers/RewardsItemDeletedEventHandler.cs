using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Extensions.Logging;
using HTEC.POC.Application.CQRS.Events;

namespace HTEC.POC.BackgroundWorker.Handlers;

public class RewardsItemDeletedEventHandler : IApplicationEventHandler<RewardsItemDeletedEvent>
{
    private readonly ILogger<RewardsItemDeletedEventHandler> log;

    public RewardsItemDeletedEventHandler(ILogger<RewardsItemDeletedEventHandler> log)
    {
        this.log = log;
    }

    public Task HandleAsync(RewardsItemDeletedEvent appEvent)
    {
        log.LogInformation($"Executing RewardsItemDeletedEventHandler...");
        return Task.CompletedTask;
    }
}
