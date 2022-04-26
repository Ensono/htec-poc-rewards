using System;

namespace HTEC.POC.CQRS.Commands;

/// <summary>
/// Define required parameters for commands executed against a rewards item
/// </summary>
public interface IRewardsItemCommand : ICategoryCommand
{
    Guid RewardsItemId { get; }
}
