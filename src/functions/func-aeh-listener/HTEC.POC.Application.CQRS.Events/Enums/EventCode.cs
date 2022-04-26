namespace HTEC.POC.Application.CQRS.Events.Enums
{
	public enum EventCode
	{
		// Rewards operations
		RewardsCreated = 101,
		RewardsUpdated = 102,
		RewardsDeleted = 103,

		// Categories Operations
		CategoryCreated = 201,
		CategoryUpdated = 202,
		CategoryDeleted = 203,

		// Items Operations
		RewardsItemCreated = 301,
		RewardsItemUpdated = 302,
		RewardsItemDeleted = 303,

		// CosmosDB change feed operations
		EntityUpdated = 999
	}
}
