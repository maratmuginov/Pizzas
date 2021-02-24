using System.Collections.Generic;

namespace Pizzas.Lib.Models
{
    public readonly struct PizzaModel
    {
        public PizzaHeaderModel Header { get; init; }
        public IDictionary<int, string[]> PizzaIngredients { get; init; }
    }
}
