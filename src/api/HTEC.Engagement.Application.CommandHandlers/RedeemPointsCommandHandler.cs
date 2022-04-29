using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Application.CQRS.Commands;
using HTEC.Engagement.Application.CQRS.Events;
using HTEC.Engagement.Application.Integration;
using HTEC.Engagement.CQRS.Commands;

namespace HTEC.Engagement.Application.CommandHandlers
{
    public class RedeemPointsCommandHandler : ICommandHandler<RedeemPoints, bool>
    {
        private readonly IPointsRepository repository;
        private readonly IApplicationEventPublisher applicationEventPublisher;

        public RedeemPointsCommandHandler(IPointsRepository repository, IApplicationEventPublisher applicationEventPublisher)
        {
            this.repository = repository;
            this.applicationEventPublisher = applicationEventPublisher;
        }

        public async Task<bool> HandleAsync(RedeemPoints command)
        {
            await applicationEventPublisher.PublishAsync(new PointsRedeemedEvent(command, command.PointsId, command.Points));
            return true;
        }
    }
}
