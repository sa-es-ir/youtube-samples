using Xunit.Abstractions;

namespace xUnitTestRunBasic;

[Collection("GuidCollection")]
public class Class1
{
    private readonly ITestOutputHelper _output;
    private readonly Guid guid;

    public Class1(ITestOutputHelper output, GuidFixture guidFixture)
    {
        guid = guidFixture.Guid;
        _output = output;
    }

    [Fact]
    public async Task Test1()
    {
        _output.WriteLine($"Test1: {guid}");

        await Task.Delay(1000);
    }

    [Fact]
    public async Task Test2()
    {
        _output.WriteLine($"Test2: {guid}");

        await Task.Delay(2000);
    }

    [Fact]
    public async Task Test3()
    {
        _output.WriteLine($"Test3: {guid}");

        await Task.Delay(3000);
    }
}


public class GuidFixture : IDisposable
{
    public Guid Guid { get; }
    public GuidFixture()
    {
        Guid = Guid.NewGuid();
    }
    public void Dispose()
    {
    }
}

[CollectionDefinition("GuidCollection", DisableParallelization = false)]
public class CollectionFixture : ICollectionFixture<GuidFixture>
{
}