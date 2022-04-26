using System;
using Amido.Stacks.Core.Exceptions;

namespace HTEC.POC.Domain.RewardsAggregateRoot.Exceptions;

[Serializable]
public class CategoryAlreadyExistsException : DomainExceptionBase
{
    private CategoryAlreadyExistsException(
        string message
    ) : base(message)
    {
    }

    public override int ExceptionCode { get; protected set; } = (int)Common.Exceptions.ExceptionCode.CategoryAlreadyExists;

    public static void Raise(Guid rewardsId, string categoryName, string message)
    {
        var exception = new CategoryAlreadyExistsException(
            message ?? $"A category with name '{categoryName}' already exists in the rewards '{rewardsId}'."
        );
        exception.Data["RewardsId"] = rewardsId;
        throw exception;
    }

    public static void Raise(Guid rewardsId, string categoryName)
    {
        Raise(rewardsId, categoryName, null);
    }
}
