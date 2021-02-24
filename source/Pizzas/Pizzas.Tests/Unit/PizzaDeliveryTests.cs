using Pizzas.Lib.Helpers;
using Pizzas.Lib.Models;
using Pizzas.Lib.Services;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Pizzas.Tests.Unit
{
    public class PizzaDeliveryTests
    {
        [Theory, ClassData(typeof(PizzaDeliveryProducesValidResult_TestData))]
        public void DeliveringPizzasDoesNotThrowExceptions(PizzaModel pizzaModel)
        {
            //Arrange
            void DeliverPizzas() => PizzaDeliveryService.DeliverPizzas(pizzaModel);

            //Act
            var exception = Record.Exception(DeliverPizzas);

            //Assert
            Assert.Null(exception);
        }

        [Theory, ClassData(typeof(PizzaDeliveryProducesValidResult_TestData))]
        public void PizzaDeliveryProducesValidResult(in PizzaModel pizzaModel)
        {
            var result = PizzaDeliveryService.DeliverPizzas(pizzaModel);

            Assert.True(
                result.TeamsDeliveredTo <= pizzaModel.Header.TeamSizeCounts.Sum(kvp => kvp.Value));
            Assert.True(result.TeamPizzas.Sum(teamDelivery =>
                teamDelivery.PizzaIndices.Length) <= pizzaModel.Header.PizzaCount);
            Assert.True(result.TeamPizzas.All(teamDelivery => 
                teamDelivery.PizzaIndices.Length == (int) teamDelivery.TeamSize));
        }

        private class PizzaDeliveryProducesValidResult_TestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                string[] fileNames = 
                {
                    "a_example", 
                    "b_little_bit_of_everything.in",
                    "c_many_ingredients.in",
                    "d_many_pizzas.in",
                    "e_many_teams.in"
                };

                return fileNames.Select(fileName => new object[]
                {
                    PizzaInputReader.ReadInput(ResourceHelper.GetManifestResourceStream(fileName))
                }).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
