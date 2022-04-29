using System;
using Amido.Stacks.Core.Exceptions;

namespace Htec.Poc.Domain.RewardAggregateRoot.Exceptions;

[Serializable]
public class RewardItemAlreadyExistsException : DomainExceptionBase
{
    private RewardItemAlreadyExistsException(
        string message
    ) : base(message)
    {
    }

    public override int ExceptionCode { get; protected set; } = (int)Common.Exceptions.ExceptionCode.RewardItemAlreadyExists;


    public static void Raise(Guid categoryId, string rewardItemName, string message)
    {
        var exception = new RewardItemAlreadyExistsException(
            message ?? $"The item {rewardItemName} already exist in the category '{categoryId}'."
        );

        exception.Data["CategoryId"] = categoryId;
        exception.Data["RewardItemName"] = rewardItemName;

        throw exception;
    }

    public static void Raise(Guid categoryId, string rewardItemName)
    {
        Raise(categoryId, rewardItemName, null);
    }
}
