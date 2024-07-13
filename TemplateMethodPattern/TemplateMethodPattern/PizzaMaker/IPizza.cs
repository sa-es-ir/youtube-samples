namespace TemplateMethodPattern.PizzaMaker;

public interface IPizza
{
    PizzaType Type { get; }
    Task MakePizza();
}

public abstract class PizzaBase : IPizza
{
    public virtual PizzaType Type => PizzaType.None;

    // Template method
    public async Task MakePizza()
    {
        Console.WriteLine($"Making a {Type} pizza.");

        PrepareDough();
        await AddToppings();
        await AddExtraCheese();
        Bake();
        Cut();
        Serve();

        Console.WriteLine($"The {Type} pizza is made and served.");
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
