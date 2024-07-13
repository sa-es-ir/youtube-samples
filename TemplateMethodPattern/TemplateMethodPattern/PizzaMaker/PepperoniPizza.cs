namespace TemplateMethodPattern.PizzaMaker;

public class PepperoniPizza : PizzaBase, IPizza
{
    public override PizzaType Type => PizzaType.Pepperoni;

    protected override async Task AddToppings()
    {
        await Task.Delay(1000);
        Console.WriteLine("Adding tomato sauce, mozzarella, and pepperoni.");
    }

    protected override async Task AddExtraCheese()
    {
        await Task.Delay(1000);
        Console.WriteLine("Adding more cheese to make if more delicious");
    }
}