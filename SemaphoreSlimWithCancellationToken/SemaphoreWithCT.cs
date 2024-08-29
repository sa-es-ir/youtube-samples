namespace SemaphoreSlimWithCancellationToken;

public class SemaphoreWithCT
{
    private readonly SemaphoreSlim _semaphore = new(initialCount: 1);

    public async Task DoSomethingAsync(CancellationToken cancellationToken)
    {
        await _semaphore.WaitAsync(TimeSpan.FromMilliseconds(500));
        try
        {
            Console.WriteLine($"Enter to Critical zone at {TimeProvider.System.GetLocalNow()}");

            await Task.Delay(1000);
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
