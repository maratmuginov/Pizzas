namespace Pizzas.Lib.Models
{
    public readonly struct PizzaDeliveryResult
    {
        public int TeamsDeliveredTo => TeamPizzas.Length;
        public TeamDelivery[] TeamPizzas { get; init; }
    }

    public readonly struct TeamDelivery
    {
        public TeamSize TeamSize { get; init; }
        public int[] PizzaIndices { get; init; }
    }
}
