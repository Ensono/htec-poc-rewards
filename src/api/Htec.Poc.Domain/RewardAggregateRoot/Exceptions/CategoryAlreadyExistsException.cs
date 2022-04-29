using System;
using Amido.Stacks.Core.Exceptions;

namespace Htec.Poc.Domain.RewardAggregateRoot.Exceptions;

[Serializable]
public class CategoryAlreadyExistsException : DomainExceptionBase
{
    private CategoryAlreadyExistsException(
        string message
    ) : base(message)
    {
    }

    public override int ExceptionCode { get; protected set; } = (int)Common.Exceptions.ExceptionCode.CategoryAlreadyExists;

    public static void Raise(Guid rewardId, string categoryName, string message)
    {
        var exception = new CategoryAlreadyExistsException(
            message ?? $"A category with name '{categoryName}' already exists in the reward '{rewardId}'."
        );
        exception.Data["RewardId"] = rewardId;
        throw exception;
    }

    public static void Raise(Guid rewardId, string categoryName)
    {
        Raise(rewardId, categoryName, null);
    }
}
