using Pizzas.Lib.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pizzas.Lib.Services
{
    public class PizzaInputReader
    {
        public static PizzaModel ReadInput(Stream stream)
        {
            using var reader = new StreamReader(stream, leaveOpen: true);
            string headerLine = reader.ReadLine();
            var pizzaHeader = ParsePizzaHeader(headerLine);
            var pizza = new PizzaModel
            {
                Header = pizzaHeader,
                PizzaIngredients = new Dictionary<int, string[]>()
            };

            int pizzaIndex = 0;
            while (reader.Peek() is not -1)
            {
                string[] ingredients = reader.ReadLine().Split(' ').Skip(1).ToArray();
                pizza.PizzaIngredients.Add(pizzaIndex, ingredients);
                pizzaIndex++;
            }

            return pizza;
        }

        private static PizzaHeaderModel ParsePizzaHeader(string headerLine)
        {
            string[] headerColumns = headerLine.Split(' ');
            return new PizzaHeaderModel
            {
                PizzaCount = int.Parse(headerColumns[0]),
                TeamSizeCounts = new Dictionary<TeamSize, int>
                {
                    {TeamSize.Two, int.Parse(headerColumns[1])},
                    {TeamSize.Three, int.Parse(headerColumns[2])},
                    {TeamSize.Four, int.Parse(headerColumns[3])},
                }
            };
        }
    }
}
