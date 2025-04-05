namespace DeepDiveToDI.Domain;

public interface ITransientService
{
    void PrintMe();
}

public class TransientService : ITransientService
{
    public TransientService()
    {
        Console.WriteLine($"\n\t TransientService created -> {Guid.NewGuid()} \n");
    }

    public void PrintMe()
    {
        Console.WriteLine($"\n\t <------------TransientService PrintMe() called----------> \n");
    }
}