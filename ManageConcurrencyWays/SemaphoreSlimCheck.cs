namespace ManageConcurrencyWays;

public class SemaphoreSlimCheck
{
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private ModelToCheck _modelToCheck = new();

    public async Task Get()
    {
        await _semaphore.WaitAsync();
        try
        {
            await Task.Delay(100);

            _ = _modelToCheck.AccessMe;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task Set()
    {
        await _semaphore.WaitAsync();
        try
        {
            await Task.Delay(200);

            _modelToCheck.AccessMe = Guid.NewGuid();
        }
        finally
        {
            _semaphore.Release();
        }
    }
}

public class ReaderWriterLockCheck
{
    private readonly ReaderWriterLockSlim _readerWriterLock = new(LockRecursionPolicy.SupportsRecursion);
    private ModelToCheck _modelToCheck = new();

    public async Task Get()
    {
        _readerWriterLock.EnterUpgradeableReadLock();
        Console.WriteLine("EnterReadLock");
        try
        {
            await Task.Delay(100);

            _ = _modelToCheck.AccessMe;
        }
        finally
        {
            if (_readerWriterLock.IsUpgradeableReadLockHeld)
            {
                Console.WriteLine("ExitReadLock");
                _readerWriterLock.ExitUpgradeableReadLock();
            }
        }
    }

    public async Task Set()
    {
        _readerWriterLock.TryEnterWriteLock(TimeSpan.FromSeconds(10));
        Console.WriteLine("EnterWriteLock");
        try
        {
            await Task.Delay(200);

            _modelToCheck.AccessMe = Guid.NewGuid();
        }
        finally
        {

            if (_readerWriterLock.IsWriteLockHeld)
            {
                Console.WriteLine("ExitWriteLock");
                _readerWriterLock.ExitWriteLock();
            }
        }
    }
}
