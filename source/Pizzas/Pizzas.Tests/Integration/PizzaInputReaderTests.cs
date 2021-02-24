using Pizzas.Lib.Helpers;
using Pizzas.Lib.Models;
using Pizzas.Lib.Services;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Pizzas.Tests.Integration
{
    public class PizzaInputReaderTests
    {
        [Theory, ClassData(typeof(ReadsInputCorrectly_TestData))]
        public void ReadsInputCorrectly(string fileName, PizzaModel expected)
        {
            //Arrange
            using var stream = ResourceHelper.GetManifestResourceStream(fileName);
            
            //Act
            var actual = PizzaInputReader.ReadInput(stream);

            //Assert
            Assert.True(expected.PizzaIngredients.Values.SelectMany(value => value)
                .SequenceEqual(actual.PizzaIngredients.Values.SelectMany(value => value)));
            Assert.Equal(expected.Header.PizzaCount, actual.Header.PizzaCount);
            Assert.True(expected.Header.TeamSizeCounts.SequenceEqual(actual.Header.TeamSizeCounts));
        }

        private class ReadsInputCorrectly_TestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    "a_example",
                    new PizzaModel
                    {
                        Header = new PizzaHeaderModel
                        {
                            PizzaCount = 5,
                            TeamSizeCounts = new Dictionary<TeamSize, int>
                            {
                                {TeamSize.Two, 1},
                                {TeamSize.Three, 2},
                                {TeamSize.Four, 1}
                            }
                        },
                        PizzaIngredients = new Dictionary<int, string[]>
                        {
                            {0, new[] {"onion", "pepper", "olive"}},
                            {1, new[] {"mushroom", "tomato", "basil"}},
                            {2, new[] {"chicken", "mushroom", "pepper"}},
                            {3, new[] {"tomato", "mushroom", "basil"}},
                            {4, new[] {"chicken", "basil"}}
                        }
                    }
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory, ClassData(typeof(CanReadRealFileWithoutRaisingExceptions_TestData))]
        public void CanReadRealFileWithoutRaisingExceptions(string fileName)
        {
            using Stream stream = ResourceHelper.GetManifestResourceStream(fileName);
            
            var exception = Record.Exception(() => PizzaInputReader.ReadInput(stream));
            
            Assert.Null(exception);
        }

        private class CanReadRealFileWithoutRaisingExceptions_TestData : IEnumerable<object[]>
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

                return fileNames.Select(fileName => new object[] { fileName }).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
