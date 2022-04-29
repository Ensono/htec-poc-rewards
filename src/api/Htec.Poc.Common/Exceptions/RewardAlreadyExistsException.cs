using System;
using Amido.Stacks.Core.Exceptions;
using Amido.Stacks.Core.Operations;
using Htec.Poc.Common.Operations;

namespace Htec.Poc.Common.Exceptions;

[Serializable]
public class RewardAlreadyExistsException : ApplicationExceptionBase
{
    private RewardAlreadyExistsException(
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
        var exception = new RewardAlreadyExistsException(
            Exceptions.ExceptionCode.RewardAlreadyExists,
            operationCode,
            correlationId,
            message ?? $"A reward with id '{rewardId}' already exists."
        );
        exception.Data["RewardId"] = rewardId;
        throw exception;
    }

    public static void Raise(IOperationContext context, Guid rewardId)
    {
        Raise((OperationCode)context.OperationCode, context.CorrelationId, rewardId, null);
    }
}
