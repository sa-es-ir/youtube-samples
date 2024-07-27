namespace TemplateMethodPattern.PizzaMaker;

public abstract class PizzaBase
{
    protected virtual PizzaType PizzaType => PizzaType.None;

    // The template method
    public async Task MakePizza()
    {
        Console.WriteLine($"Making a {PizzaType} pizza.");

        await PrepareDough();
        await Baking();
        await AddToppings();
        await AddExtraIngredients();
        await Cutting();
        await Serving();

        Console.WriteLine($"The {PizzaType} pizza is ready.");
    }

    protected async Task Serving()
    {
        await Task.Delay(500);

        Console.WriteLine("Serving the pizza.");
    }

    protected async Task Cutting()
    {
        await Task.Delay(500);

        Console.WriteLine("Cutting the pizza.");
    }

    protected virtual Task AddExtraIngredients() => Task.CompletedTask;

    protected abstract Task AddToppings();

    protected async Task Baking()
    {
        await Task.Delay(500);

        Console.WriteLine("Baking the pizza.");
    }

    protected async Task PrepareDough()
    {
        await Task.Delay(500);

        Console.WriteLine("Preparing the dough.");
    }
}
