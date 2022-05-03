using Htec.Poc.Application.Integration;
using Htec.Poc.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Htec.Poc.Infrastructure.Rules
{
    public class RulesEngine : IRulesEngine
    {
        const string ItemGroup = "F1";
        const int Multiplier = 2;

        public Task<int> CalculateReward(Basket basket)
        {
            var totalAmount = 0;

            if (basket == null || basket.Items == null || basket.Items.Count == 0)
            {
                return Task.FromResult(totalAmount);
            }

            if (basket.Items.Exists(x => x.ItemGroup == ItemGroup))
            {
                // Get total number of litres
                int totalOfLitres = 0;
                foreach (BasketItem item in basket.Items.FindAll(x => x.ItemGroup == ItemGroup))
                {
                    totalOfLitres += item.Quantity;
                }

                totalAmount += totalOfLitres * Multiplier;
            }
            else
            {
                // Get total amount no fuel
                decimal totalAmountNoFuel = 0;
                foreach (BasketItem item in basket.Items.FindAll(x => x.ItemGroup != ItemGroup))
                {
                    totalAmountNoFuel += decimal.Round(item.Quantity * item.Price, 0);
                }

                totalAmount += Convert.ToInt32(totalAmountNoFuel);
            }

            return Task.FromResult(totalAmount);
        }
    }
}