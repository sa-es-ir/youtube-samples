namespace SemaphoreSlimWithCancellationToken;

public class FooService
{
    private readonly SemaphoreSlim _semaphore = new(initialCount: 1);

    public async Task DoSomethingAsync(CancellationToken cancellationToken)
    {
        await _semaphore.WaitAsync();
        try
        {
            Console.WriteLine($"Enter to Critical zone at {TimeProvider.System.GetLocalNow()}");

            await Task.Delay(1000, cancellationToken);
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
