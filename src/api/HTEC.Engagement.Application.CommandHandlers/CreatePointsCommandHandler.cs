using System;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Application.CQRS.Commands;
using HTEC.Engagement.Application.CQRS.Events;
using HTEC.Engagement.Application.Integration;
using HTEC.Engagement.CQRS.Commands;
using HTEC.Engagement.Domain;

namespace HTEC.Engagement.Application.CommandHandlers
{
    public class CreatePointsCommandHandler : ICommandHandler<CreatePoints, Guid>
    {
        private readonly IPointsRepository repository;

        public CreatePointsCommandHandler(IPointsRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Guid> HandleAsync(CreatePoints command)
        {
            var id = Guid.NewGuid();

            var newPoints = new Points(
                id: id,
                name: command.Name,
                description: command.Description,
                enabled: command.Enabled,
                balance: command.Balance
            );

            await repository.SaveAsync(newPoints);

            return id;
        }
    }
}
