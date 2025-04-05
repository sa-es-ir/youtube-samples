namespace DeepDiveToDI.Domain;

public interface IScopedService
{
    void PrintMe();
}

public class ScopedService : IScopedService
{
    public ScopedService()
    {
        Console.WriteLine($"\n\t ScopedService created -> {Guid.NewGuid()} \n");
    }

    public void PrintMe()
    {
        Console.WriteLine($"\n\t <------------ScopedService PrintMe() called----------> \n");
    }
}
