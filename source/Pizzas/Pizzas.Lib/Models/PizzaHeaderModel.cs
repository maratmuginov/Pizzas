using System.Collections.Generic;

namespace Pizzas.Lib.Models
{
    public readonly struct PizzaHeaderModel
    {
        public int PizzaCount { get; init; }
        public IDictionary<TeamSize, int> TeamSizeCounts { get; init; }
    }
}
