namespace TemplateMethodPattern.PizzaMaker;

public abstract class PizzaBase
{
    public virtual PizzaType Type { get; }

    // Template method
    public async Task MakePizza()
    {
        Console.WriteLine($"----------------------->Making a {Type} pizza.");

        PrepareDough();
        await AddToppings();
        await AddExtraCheese();
        Bake();
        Cut();
        Serve();

        Console.WriteLine($"----------------------->{Type} pizza is ready.");
    }

    protected virtual void PrepareDough()
    {
        Console.WriteLine("Preparing the dough.");
    }

    protected abstract Task AddToppings();
    protected virtual Task AddExtraCheese() => Task.CompletedTask;

    protected virtual void Bake()
    {
        Console.WriteLine("Baking the pizza.");
    }

    protected virtual void Cut()
    {
        Console.WriteLine("Cutting the pizza.");
    }

    protected virtual void Serve()
    {
        Console.WriteLine("Serving the pizza.");
    }
}
