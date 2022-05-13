using System;
using Amido.Stacks.Application.CQRS.Commands;
using Htec.Poc.CQRS.Commands.Models;

namespace Htec.Poc.CQRS.Commands;

public class CalculateReward : ICommand
{
    public int OperationCode => (int)Common.Operations.OperationCode.CalculateReward;

    public Guid CorrelationId { get; }

    public Guid MemberId { get; set; }

    public Basket Basket { get; set; }

    public CalculateReward(Guid correlationId, Guid memberId, Basket basket)
    {
        CorrelationId = correlationId;
        MemberId = memberId;
        Basket = basket;
    }
}
