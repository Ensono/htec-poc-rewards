using System;
using Amido.Stacks.Application.CQRS.Commands;

namespace HTEC.POC.CQRS.Commands;

/// <summary>
/// Define required parameters for commands executed against a rewards
/// </summary>
public interface IRewardsCommand : ICommand
{
    Guid RewardsId { get; }
}
