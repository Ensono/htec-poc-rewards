using System;
using Amido.Stacks.Core.Exceptions;

namespace HTEC.POC.Domain.RewardsAggregateRoot.Exceptions;

[Serializable]
public class RewardsItemPriceMustNotBeZeroException : DomainExceptionBase
{
    private RewardsItemPriceMustNotBeZeroException(
        string message
    ) : base(message)
    {
    }

    public override int ExceptionCode { get; protected set; } = (int)Common.Exceptions.ExceptionCode.RewardsItemPriceMustNotBeZero;

    public static void Raise(string itemName, string message)
    {
        var exception = new RewardsItemPriceMustNotBeZeroException(
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
