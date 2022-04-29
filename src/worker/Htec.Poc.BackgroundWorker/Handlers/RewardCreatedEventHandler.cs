using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Extensions.Logging;
using Htec.Poc.Application.CQRS.Events;

namespace Htec.Poc.BackgroundWorker.Handlers;

public class RewardCreatedEventHandler : IApplicationEventHandler<RewardCreatedEvent>
{
    private readonly ILogger<RewardCreatedEventHandler> log;

    public RewardCreatedEventHandler(ILogger<RewardCreatedEventHandler> log)
    {
        this.log = log;
    }

    public Task HandleAsync(RewardCreatedEvent appEvent)
    {
        log.LogInformation($"Executing RewardCreatedEventHandler...");
        return Task.CompletedTask;
    }
}
