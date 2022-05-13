namespace Htec.Poc.Common.Events;

public enum EventCode
{
    // Reward operations
    RewardCreated = 101,
    RewardUpdated = 102,
    RewardDeleted = 103,
    RewardCalculated = 104,

    CosmosDbChangeFeedEvent = 999
}
