using System;
using Amido.Stacks.Core.Exceptions;
using Amido.Stacks.Core.Operations;
using Htec.Poc.Common.Operations;

namespace Htec.Poc.Common.Exceptions;

[Serializable]
public class RewardDoesNotExistException : ApplicationExceptionBase
{
    private RewardDoesNotExistException(
        ExceptionCode exceptionCode,
        OperationCode operationCode,
        Guid correlationId,
        string message
    ) : base((int)operationCode, correlationId, message)
    {
        ExceptionCode = (int)exceptionCode;

        HttpStatusCode = (int)ExceptionCodeToHttpStatusCodeConverter.GetHttpStatusCode((int)exceptionCode);
    }

    public override int ExceptionCode { get; protected set; }

    public static void Raise(OperationCode operationCode, Guid correlationId, Guid rewardId, string message)
    {
        var exception = new RewardDoesNotExistException(
            Exceptions.ExceptionCode.RewardDoesNotExist,
            operationCode,
            correlationId,
            message ?? $"A reward with id '{rewardId}' does not exist."
        );
        exception.Data["RewardId"] = rewardId;
        throw exception;
    }

    public static void Raise(IOperationContext context, Guid rewardId)
    {
        Raise((OperationCode)context.OperationCode, context.CorrelationId, rewardId, null);
    }
}
