namespace TemplateMethodPattern.PizzaMaker;

public class MargheritaPizza : IPizza
{
    public async Task MakePizza()
    {
        await Task.Delay(1000);

        Console.WriteLine("Preparing the dough.");

        await Task.Delay(1000);

        Console.WriteLine("Baking the pizza.");

        await Task.Delay(1000);

        Console.WriteLine("Add toppings: tomato sauce, mozzarella, and basil.");

        await Task.Delay(1000);

        Console.WriteLine("No need to add extra ingredients");

        await Task.Delay(1000);

        Console.WriteLine("Cutting the pizza.");

        await Task.Delay(1000);

        Console.WriteLine("Serving the pizza.");
    }
}
