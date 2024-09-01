namespace SemaphoreSlimWithCancellationToken;

internal class LockAndSemaphore
{
    private readonly SemaphoreSlim _semaphore = new(initialCount: 2, maxCount: 2);
    private readonly object _lock = new();

    public async Task DoWithSemaphore(int taskNumber)
    {
        await _semaphore.WaitAsync();
        try
        {
            Console.WriteLine($"{taskNumber} Enter to Critical zone at {TimeProvider.System.GetLocalNow()}");

            await Task.Delay(1000);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task DoWithLock(int taskNumber)
    {
        lock (_lock)
        {
            Console.WriteLine($"{taskNumber} Enter to Critical zone at {TimeProvider.System.GetLocalNow()}");

            Task.Delay(1000).Wait();
        }

        await Task.CompletedTask;
    }
}
