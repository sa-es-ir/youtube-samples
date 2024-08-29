namespace SemaphoreSlimWithCancellationToken;

internal class LockAndSemaphore
{
    private readonly SemaphoreSlim _semaphore = new(initialCount: 2);

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
        lock (_semaphore)
        {
            Console.WriteLine($"{taskNumber} Enter to Critical zone at {TimeProvider.System.GetLocalNow()}");

            Task.Delay(1000).Wait();
        }

        await Task.CompletedTask;
    }
}
