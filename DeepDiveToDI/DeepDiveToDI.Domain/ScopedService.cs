namespace DeepDiveToDI.Domain;

public interface IScopedService
{
    void PrintMe();
}

public class ScopedService : IScopedService, IDisposable
{
    public ScopedService()
    {
        Console.WriteLine($"\n\t ScopedService created -> {Guid.NewGuid()} \n");
    }

    public void Dispose()
    {
        Console.WriteLine($"\n\t ScopedService disposed \n");
    }

    public void PrintMe()
    {
        Console.WriteLine($"\n\t <------------ScopedService PrintMe() called----------> \n");
    }
}
