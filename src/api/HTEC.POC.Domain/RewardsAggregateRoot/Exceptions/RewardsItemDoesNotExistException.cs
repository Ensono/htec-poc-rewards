using System;
using Amido.Stacks.Core.Exceptions;

namespace HTEC.POC.Domain.RewardsAggregateRoot.Exceptions;

[Serializable]
public class RewardsItemDoesNotExistException : DomainExceptionBase
{
    private RewardsItemDoesNotExistException(
        string message
    ) : base(message)
    {
    }


    public override int ExceptionCode { get; protected set; } = (int)Common.Exceptions.ExceptionCode.RewardsItemDoesNotExist;

    public static void Raise(Guid categoryId, Guid rewardsItemId, string message)
    {
        var exception = new RewardsItemDoesNotExistException(
            message ?? $"The item {rewardsItemId} does not exist in the category '{categoryId}'."
        );

        exception.Data["CategoryId"] = categoryId;
        exception.Data["RewardsItemId"] = rewardsItemId;

        throw exception;
    }

    public static void Raise(Guid categoryId, Guid rewardsItemId)
    {
        Raise(categoryId, rewardsItemId, null);
    }
}
