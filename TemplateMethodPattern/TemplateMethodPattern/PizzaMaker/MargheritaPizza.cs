namespace TemplateMethodPattern.PizzaMaker;

public class MargheritaPizza : PizzaBase, IPizza
{
    public override PizzaType Type => PizzaType.Margherita;

    protected override async Task AddToppings()
    {
        await Task.Delay(1000);
        Console.WriteLine("Adding tomato sauce, mozzarella, and basil.");
    }
}
