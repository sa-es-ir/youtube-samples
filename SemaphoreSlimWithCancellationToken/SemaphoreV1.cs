namespace SemaphoreSlimWithCancellationToken;

public class SemaphoreV1
{
    private readonly SemaphoreSlim _semaphore = new(initialCount: 1, maxCount: 1);

    public async Task DoSomethingAsync(CancellationToken cancellationToken)
    {
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            Console.WriteLine($"Enter to Critical zone at {TimeProvider.System.GetLocalNow()}");

            await Task.Delay(1000, cancellationToken);
        }

        catch (Exception ex)
        {
            Console.WriteLine($"-->ERR: {ex.Message}");
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
