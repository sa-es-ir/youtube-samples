namespace TemplateMethodPattern.PizzaMaker;

public interface IPizza
{
    Task MakePizza();
}


// write a class that implements the IPizza interface using the template method pattern, use a base class to implement the template method and derived classes to implement the steps of the algorithm

public abstract class PizzaBase : IPizza
{
    // Template method
    public async Task MakePizza()
    {
        PrepareDough();
        await AddToppings();
        await AddExtraCheese();
        Bake();
        Cut();
        Serve();
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


public class MargheritaPizza : PizzaBase, IPizza
{
    protected override async Task AddToppings()
    {
        await Task.Delay(1000);
        Console.WriteLine("Adding tomato sauce, mozzarella, and basil.");
    }
}

public class PepperoniPizza : PizzaBase, IPizza
{
    protected override async Task AddToppings()
    {
        await Task.Delay(1000);
        Console.WriteLine("Adding tomato sauce, mozzarella, and pepperoni.");
    }
}