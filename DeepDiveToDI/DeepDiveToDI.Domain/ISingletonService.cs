namespace DeepDiveToDI.Domain;

public interface ISingletonService
{
    void PrintMe();
}

public class SingletonService : ISingletonService, IDisposable
{
    public SingletonService()
    {
        Console.WriteLine($"\n\t SingletonService created -> {Guid.NewGuid()} \n");
    }

    public void Dispose()
    {
        Console.WriteLine($"\n\t SingletonService disposed \n");
    }

    public void PrintMe()
    {
        Console.WriteLine($"\n\t <------------SingletonService PrintMe() called----------> \n");
    }
}
