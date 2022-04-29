using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Extensions.Logging;
using Htec.Poc.Application.CQRS.Events;

namespace Htec.Poc.BackgroundWorker.Handlers;

public class RewardDeletedEventHandler : IApplicationEventHandler<RewardDeletedEvent>
{
    private readonly ILogger<RewardDeletedEventHandler> log;

    public RewardDeletedEventHandler(ILogger<RewardDeletedEventHandler> log)
    {
        this.log = log;
    }

    public Task HandleAsync(RewardDeletedEvent appEvent)
    {
        log.LogInformation($"Executing RewardDeletedEventHandler...");
        return Task.CompletedTask;
    }
}
