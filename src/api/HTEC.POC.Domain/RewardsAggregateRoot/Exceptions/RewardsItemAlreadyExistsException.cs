using System;
using Amido.Stacks.Core.Exceptions;

namespace HTEC.POC.Domain.RewardsAggregateRoot.Exceptions;

[Serializable]
public class RewardsItemAlreadyExistsException : DomainExceptionBase
{
    private RewardsItemAlreadyExistsException(
        string message
    ) : base(message)
    {
    }

    public override int ExceptionCode { get; protected set; } = (int)Common.Exceptions.ExceptionCode.RewardsItemAlreadyExists;


    public static void Raise(Guid categoryId, string rewardsItemName, string message)
    {
        var exception = new RewardsItemAlreadyExistsException(
            message ?? $"The item {rewardsItemName} already exist in the category '{categoryId}'."
        );

        exception.Data["CategoryId"] = categoryId;
        exception.Data["RewardsItemName"] = rewardsItemName;

        throw exception;
    }

    public static void Raise(Guid categoryId, string rewardsItemName)
    {
        Raise(categoryId, rewardsItemName, null);
    }
}
