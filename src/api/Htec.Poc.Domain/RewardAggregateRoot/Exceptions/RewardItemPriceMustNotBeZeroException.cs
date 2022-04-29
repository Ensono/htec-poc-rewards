using System;
using Amido.Stacks.Core.Exceptions;

namespace Htec.Poc.Domain.RewardAggregateRoot.Exceptions;

[Serializable]
public class RewardItemPriceMustNotBeZeroException : DomainExceptionBase
{
    private RewardItemPriceMustNotBeZeroException(
        string message
    ) : base(message)
    {
    }

    public override int ExceptionCode { get; protected set; } = (int)Common.Exceptions.ExceptionCode.RewardItemPriceMustNotBeZero;

    public static void Raise(string itemName, string message)
    {
        var exception = new RewardItemPriceMustNotBeZeroException(
            message ?? $"The price for the item {itemName} had price as zero. A price must be provided."
        );

        exception.Data["ItemName"] = itemName;
        throw exception;
    }

    public static void Raise(string itemName)
    {
        Raise(itemName, null);
    }
}
