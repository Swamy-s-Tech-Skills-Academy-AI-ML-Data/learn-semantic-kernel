namespace BuilderPatternDemo.Builders;

public sealed class Pizza
{
    public string Dough { get; set; } = string.Empty;

    public string Sauce { get; set; } = string.Empty;

    public string Toppings { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"Pizza with dough: {Dough}, sauce: {Sauce}, toppings: {Toppings}";
    }
}

