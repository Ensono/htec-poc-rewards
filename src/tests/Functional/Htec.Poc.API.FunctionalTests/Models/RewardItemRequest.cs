namespace Htec.Poc.API.FunctionalTests.Models;

public class RewardItemRequest
{
    public string name { get; set; }
    public string description { get; set; }
    public double price { get; set; }
    public bool available { get; set; }
}