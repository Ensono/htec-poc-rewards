namespace HTEC.POC.Common.Events;

public enum EventCode
{
    // Rewards operations
    RewardsCreated = 101,
    RewardsUpdated = 102,
    RewardsDeleted = 103,

    //GetRewards = 104,
    //SearchRewards = 110,

    // Categories Operations
    CategoryCreated = 201,
    CategoryUpdated = 202,
    CategoryDeleted = 203,

    // Items Operations
    RewardsItemCreated = 301,
    RewardsItemUpdated = 302,
    RewardsItemDeleted = 303,

    CosmosDbChangeFeedEvent = 999
}
