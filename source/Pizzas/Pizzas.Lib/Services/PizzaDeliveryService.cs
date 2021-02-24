using Pizzas.Lib.Models;
using System.Collections.Generic;
using System.Linq;

namespace Pizzas.Lib.Services
{
    public class PizzaDeliveryService
    {
        public static PizzaDeliveryResult DeliverPizzas(in PizzaModel pizzaModel)
        {
            var pizzasWithMostIngredients = pizzaModel.PizzaIngredients.OrderBy(kvp => kvp.Value.Length);

            var pizzaStack = new Stack<KeyValuePair<int, string[]>>(pizzasWithMostIngredients);
            var sequenceOfTeams =
                pizzaModel.Header.TeamSizeCounts.SelectMany(kvp => Enumerable.Range(0, kvp.Value).Select(_ => kvp.Key))
                    .OrderByDescending(teamSize => (int) teamSize);
            var teamStack = new Stack<TeamSize>(sequenceOfTeams);

            var teamDeliveries = new List<TeamDelivery>(teamStack.Count);

            while (pizzaStack.Any() && teamStack.Any())
            {
                var teamSize = teamStack.Pop();
                int teamMemberCount = (int) teamSize;

                if (pizzaStack.Count < teamMemberCount)
                    continue;

                var pizzaIndices = new List<int>(teamMemberCount);

                for (int i = 0; i < teamMemberCount; i++)
                    pizzaIndices.Add(pizzaStack.Pop().Key);
                
                teamDeliveries.Add(new TeamDelivery
                {
                    TeamSize = teamSize,
                    PizzaIndices = pizzaIndices.ToArray()
                });
            }

            return new PizzaDeliveryResult
            {
                TeamPizzas = teamDeliveries.ToArray()
            };
        }
    }
}
