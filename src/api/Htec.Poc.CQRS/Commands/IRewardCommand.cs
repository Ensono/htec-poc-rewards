using System;
using Amido.Stacks.Application.CQRS.Commands;

namespace Htec.Poc.CQRS.Commands;

/// <summary>
/// Define required parameters for commands executed against a reward
/// </summary>
public interface IRewardCommand : ICommand
{
    Guid RewardId { get; }
}
