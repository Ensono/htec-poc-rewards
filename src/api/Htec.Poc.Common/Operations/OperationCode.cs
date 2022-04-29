namespace Htec.Poc.Common.Operations;

public enum OperationCode
{
    // Reward operations
    CreateReward = 101,
    UpdateReward = 102,
    DeleteReward = 103,
    GetRewardById = 104,
    CalculateReward = 105,
    SearchReward = 110,

    // Categories Operations
    CreateCategory = 201,
    UpdateCategory = 202,
    DeleteCategory = 203,

    // Items Operations
    CreateRewardItem = 301,
    UpdateRewardItem = 302,
    DeleteRewardItem = 303
}
