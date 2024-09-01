namespace SemaphoreSlimWithCancellationToken;

public class SemaphoreV2
{
    private readonly SemaphoreSlim _semaphore = new(initialCount: 1, maxCount: 1);

    public async Task DoSomethingAsync(CancellationToken cancellationToken)
    {
        var lockTaken = false;
        try
        {
            lockTaken = await _semaphore.WaitAsync(Timeout.Infinite, cancellationToken);
            Console.WriteLine($"Enter to Critical zone at {TimeProvider.System.GetLocalNow()}");

            await Task.Delay(1000, cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"-->ERR: {ex.Message}");
        }
        finally
        {
            if (lockTaken)
            {
                _semaphore.Release();
            }
        }
    }
}
