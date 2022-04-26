using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Application.CQRS.Commands;
using Amido.Stacks.Core.Exceptions;
using HTEC.POC.Application.Integration;
using HTEC.POC.Common.Exceptions;
using HTEC.POC.CQRS.Commands;
using HTEC.POC.Domain;

namespace HTEC.POC.Application.CommandHandlers;

/// <summary>
/// RewardsCommandHandlerBase used for common operations updating an existing rewards
/// Creation and Deletion of the AggregateRoot should have it's own implementation
/// Command specific logic should be handled within HandleCommandAsync() method implementation
/// </summary>
/// <typeparam name="TCommand">The type of command being handled</typeparam>
/// <typeparam name="TResult">The type of result expected. Use bool when no return value is expected</typeparam>
public abstract class RewardsCommandHandlerBase<TCommand, TResult> : ICommandHandler<TCommand, TResult> where TCommand : IRewardsCommand
{
    protected IRewardsRepository repository;
    private readonly IApplicationEventPublisher applicationEventPublisher;

    public RewardsCommandHandlerBase(IRewardsRepository repository, IApplicationEventPublisher applicationEventPublisher)
    {
        this.repository = repository;
        this.applicationEventPublisher = applicationEventPublisher;
    }

    public async Task<TResult> HandleAsync(TCommand command)
    {
        TResult result = default(TResult);

        var rewards = await repository.GetByIdAsync(command.RewardsId);

        if (rewards == null)
            RewardsDoesNotExistException.Raise(command, command.RewardsId);

        // TODO: Check if the user owns the resource before any operation
        // if(command.User.TenantId != rewards.TenantId)
        // {
        //     throw NotAuthorizedException()
        // }

        try
        {
            result = await HandleCommandAsync(rewards, command);

            var issuccessful = await repository.SaveAsync(rewards);

            if (!issuccessful)
                OperationFailedException.Raise(command, command.RewardsId, 
                    $"Unable to handle command {typeof(ICommand).Name}");

            foreach (var appEvent in RaiseApplicationEvents(rewards, command))
            {
                await applicationEventPublisher.PublishAsync(appEvent);
            }
        }
        catch (DomainExceptionBase ex)
        {
            DomainRuleViolationException.Raise(command, command.RewardsId, ex);
        }
        catch (ApplicationExceptionBase)
        {
            // TODO: handle applicaiton exception handling
            // possible failures is missing data or information, validations, and so on
            throw;
        }
        catch (InfrastructureExceptionBase)
        {
            //TODO: handle  infrastructure exception handling
            //possible failures is calling database, queue or any other dependency
            throw;
        }
        catch (Exception ex)
        {
            //TODO: Enrich the exception details with context information to track in the logs
            ex.Data["OperationCode"] = command.OperationCode;
            ex.Data["CorrelationId"] = command.CorrelationId;
            ex.Data["RewardsId"] = command.RewardsId;

            throw;
        }

        return result;
    }

    /// <summary>
    /// The base command handler will pre-load the aggregate root and provide it to the command handler with the command
    /// </summary>
    /// <param name="rewards">the rewards loaded from the repository</param>
    /// <param name="command"></param>
    /// <returns></returns>
    public abstract Task<TResult> HandleCommandAsync(Rewards rewards, TCommand command);

    /// <summary>
    /// Application events that should be raised when the command succeed.
    /// Implement this method send application events to the message bus.
    /// </summary>
    /// <param name="rewards"></param>
    /// <param name="command"></param>
    /// <returns>Application events to be published in the message bus</returns>
    public abstract IEnumerable<IApplicationEvent> RaiseApplicationEvents(Rewards rewards, TCommand command);

}
