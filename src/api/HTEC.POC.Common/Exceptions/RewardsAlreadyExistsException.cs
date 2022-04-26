using System;
using Amido.Stacks.Core.Exceptions;
using Amido.Stacks.Core.Operations;
using HTEC.POC.Common.Operations;

namespace HTEC.POC.Common.Exceptions;

[Serializable]
public class RewardsAlreadyExistsException : ApplicationExceptionBase
{
    private RewardsAlreadyExistsException(
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

    public static void Raise(OperationCode operationCode, Guid correlationId, Guid rewardsId, string message)
    {
        var exception = new RewardsAlreadyExistsException(
            Exceptions.ExceptionCode.RewardsAlreadyExists,
            operationCode,
            correlationId,
            message ?? $"A rewards with id '{rewardsId}' already exists."
        );
        exception.Data["RewardsId"] = rewardsId;
        throw exception;
    }

    public static void Raise(IOperationContext context, Guid rewardsId)
    {
        Raise((OperationCode)context.OperationCode, context.CorrelationId, rewardsId, null);
    }
}
