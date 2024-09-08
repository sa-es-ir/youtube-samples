using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using EFCoreLargeData.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreLargeData;

[MemoryDiagnoser(false)]
[Config(typeof(Config))]
[HideColumns(Column.RatioSD, Column.AllocRatio)]
public class EFBenchmark
{
    private class Config : ManualConfig
    {
        public Config()
        {
            SummaryStyle =
                SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend);
        }
    }

    [GlobalSetup]
    public async Task Setup()
    {
        var dbcontext = new TestDbContext();

        dbcontext.TextTable2MBs.Add(new TextTable2MB { Text = new string('x', 1024 * 1024 * 2) });

        await dbcontext.SaveChangesAsync();
    }

    [Benchmark(Baseline = true)]
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

    //[Benchmark]
    //public async Task Async2MBPS()
    //{
    //    using var dbcontext = new TestDbContext("Server=localhost;Database=EFCoreLargeData;user id=sa;password=P@ssw0rd.123!;TrustServerCertificate=True;Packet Size=32767");

    //    _ = await dbcontext.TextTable2MBs.FirstOrDefaultAsync();
    //}

    //[Benchmark]
    //public void Sync2MBPS()
    //{
    //    using var dbcontext = new TestDbContext("Server=localhost;Database=EFCoreLargeData;user id=sa;password=P@ssw0rd.123!;TrustServerCertificate=True;Packet Size=32767");

    //    _ = dbcontext.TextTable2MBs.FirstOrDefault();
    //}
}
