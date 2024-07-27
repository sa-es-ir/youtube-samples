namespace TemplateMethodPattern.PizzaMaker;

public class MargheritaPizza : PizzaBase, IPizza
{
    protected override PizzaType PizzaType => PizzaType.Margherita;

    protected override async Task AddToppings()
    {
        await Task.Delay(500);

        Console.WriteLine("Add toppings: tomato sauce, mozzarella, and basil.");
    }
}
