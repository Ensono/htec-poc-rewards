using System;

namespace Htec.Poc.CQRS.Commands.Models
{
    public class CalculateRewardResult
    {
        public int Points { get; set; }
        public Guid MemberId { get; set; }

        public CalculateRewardResult(Guid memberId, int points)
        {
            MemberId= memberId;
            Points = points;
        }
    }
}
