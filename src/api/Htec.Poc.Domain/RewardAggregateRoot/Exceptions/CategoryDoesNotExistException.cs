using System;
using Amido.Stacks.Core.Exceptions;

namespace Htec.Poc.Domain.RewardAggregateRoot.Exceptions;

[Serializable]
public class CategoryDoesNotExistException : DomainExceptionBase
{
    private CategoryDoesNotExistException(
        string message
    ) : base(message)
    {
    }

    public override int ExceptionCode { get; protected set; } = (int)Common.Exceptions.ExceptionCode.CategoryDoesNotExist;

    public static void Raise(Guid rewardId, Guid categoryId, string message)
    {
        var exception = new CategoryDoesNotExistException(
            message ?? $"A category with id '{categoryId}' does not exist in the reward '{rewardId}'."
        );

        exception.Data["RewardId"] = rewardId;
        throw exception;
    }

    public static void Raise(Guid rewardId, Guid categoryId)
    {
        Raise(rewardId, categoryId, null);
    }
}
