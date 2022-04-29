using System;

namespace Htec.Poc.CQRS.Commands;

/// <summary>
/// Define required parameters for commands executed against a reward item
/// </summary>
public interface IRewardItemCommand : ICategoryCommand
{
    Guid RewardItemId { get; }
}
