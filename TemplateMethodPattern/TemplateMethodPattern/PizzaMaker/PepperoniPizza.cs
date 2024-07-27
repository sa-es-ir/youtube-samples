namespace TemplateMethodPattern.PizzaMaker;

public class PepperoniPizza : PizzaBase, IPizza
{
    protected override PizzaType PizzaType => PizzaType.Pepperoni;

    protected override async Task AddToppings()
    {
        await Task.Delay(500);

        Console.WriteLine("Add toppings: tomato sauce, mozzarella, and pepperoni.");
    }

    protected override async Task AddExtraIngredients()
    {
        await Task.Delay(500);

        Console.WriteLine("Add extra ingredients: olives, mushrooms, and onions.");
    }
}