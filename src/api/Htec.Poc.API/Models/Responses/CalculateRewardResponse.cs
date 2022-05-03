using System;
using Htec.Poc.CQRS.Commands.Models;

namespace Htec.Poc.API.Models.Responses;

/// <summary>
/// Response model used by CalculateReward api endpoint
/// </summary>
public class CalculateRewardResponse
{
    public Guid MemberId { get; private set; }

    public int Points { get; private set; }

    public static CalculateRewardResponse FromResult(CalculateRewardResult result)
    {
        return new CalculateRewardResponse
        {
            MemberId = result.MemberId,
            Points = result.Points
        };
    }
}