var builder = new PizzaBuilder()
    .SetDough("Classic")
    .SetSauce("Tomato")
    .SetToppings("Cheese and pepperoni")
    .Build();

Console.WriteLine(builder);

