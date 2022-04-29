using System;
using Amido.Stacks.Core.Exceptions;

namespace Htec.Poc.Domain.RewardAggregateRoot.Exceptions;

[Serializable]
public class RewardItemDoesNotExistException : DomainExceptionBase
{
    private RewardItemDoesNotExistException(
        string message
    ) : base(message)
    {
    }


    public override int ExceptionCode { get; protected set; } = (int)Common.Exceptions.ExceptionCode.RewardItemDoesNotExist;

    public static void Raise(Guid categoryId, Guid rewardItemId, string message)
    {
        var exception = new RewardItemDoesNotExistException(
            message ?? $"The item {rewardItemId} does not exist in the category '{categoryId}'."
        );

        exception.Data["CategoryId"] = categoryId;
        exception.Data["RewardItemId"] = rewardItemId;

        throw exception;
    }

    public static void Raise(Guid categoryId, Guid rewardItemId)
    {
        Raise(categoryId, rewardItemId, null);
    }
}
