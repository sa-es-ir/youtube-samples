using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using Microsoft.EntityFrameworkCore;

namespace EFCoreLargeData;

[MemoryDiagnoser(false)]
[Config(typeof(Config))]
public class EFBenchmark
{
    private class Config : ManualConfig
    {
        public Config()
        {
            SummaryStyle =
                SummaryStyle.Default.WithRatioStyle(RatioStyle.Percentage);
        }
    }

    [Benchmark]
    public async Task Async1MB()
    {
        using var dbcontext = new TestDbContext();

        _ = await dbcontext.TextTable1MBs.FirstOrDefaultAsync();
    }

    [Benchmark]
    public void Sync1MB()
    {
        using var dbcontext = new TestDbContext();

        _ = dbcontext.TextTable1MBs.FirstOrDefault();
    }

    [Benchmark]
    public async Task Async2MB()
    {
        using var dbcontext = new TestDbContext();

        _ = await dbcontext.TextTable2MBs.FirstOrDefaultAsync();
    }

    [Benchmark]
    public void Sync2MB()
    {
        using var dbcontext = new TestDbContext();

        _ = dbcontext.TextTable2MBs.FirstOrDefault();
    }
}
