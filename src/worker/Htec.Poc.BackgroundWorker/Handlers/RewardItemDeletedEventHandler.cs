using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Extensions.Logging;
using Htec.Poc.Application.CQRS.Events;

namespace Htec.Poc.BackgroundWorker.Handlers;

public class RewardItemDeletedEventHandler : IApplicationEventHandler<RewardItemDeletedEvent>
{
    private readonly ILogger<RewardItemDeletedEventHandler> log;

    public RewardItemDeletedEventHandler(ILogger<RewardItemDeletedEventHandler> log)
    {
        this.log = log;
    }

    public Task HandleAsync(RewardItemDeletedEvent appEvent)
    {
        log.LogInformation($"Executing RewardItemDeletedEventHandler...");
        return Task.CompletedTask;
    }
}
