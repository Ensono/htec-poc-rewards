using System;
using Amido.Stacks.Application.CQRS.Commands;

namespace Htec.Poc.CQRS.Commands;

public class CalculateReward : ICommand
{
    public int OperationCode => (int)Common.Operations.OperationCode.CalculateReward;

    public Guid CorrelationId { get; }

    public Guid MemberId { get; set; }


    public CalculateReward(Guid correlationId, Guid memberId)
    {
        CorrelationId = correlationId;
        MemberId = memberId;
    }
}
