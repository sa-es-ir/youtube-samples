using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;

namespace ManageConcurrencyWays;

[Config(typeof(Config))]
[HideColumns(Column.RatioSD, Column.AllocRatio)]
[MemoryDiagnoser(false)]
public class ConcurrencyBenchmark
{
    private readonly SemaphoreSlimCheck _semaphoreSlimCheck = new();
    private readonly ReaderWriterLockCheck _readerWriterLockCheck = new();
    private class Config : ManualConfig
    {
        public Config()
        {
            SummaryStyle = SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend);
        }
    }

    [GlobalSetup]
    public void Setup()
    {
    }

    [Benchmark]
    public async Task SemaphoreSlimCheck()
    {
        var readTask1 = _semaphoreSlimCheck.Get();
        var readTask2 = _semaphoreSlimCheck.Get();
        var writeTask = _semaphoreSlimCheck.Set();
        var readTask3 = _semaphoreSlimCheck.Get();

        await Task.WhenAll(readTask1, readTask2, writeTask, readTask3);
    }

    [Benchmark]
    public async Task ReaderWriterLockCheck()
    {
        var readTask1 = _readerWriterLockCheck.Get();
        var readTask2 = _readerWriterLockCheck.Get();
        var writeTask = _readerWriterLockCheck.Set();
        var readTask3 = _readerWriterLockCheck.Get();

        await Task.WhenAll(readTask1, readTask2, writeTask, readTask3);
    }
}
