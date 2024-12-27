#pragma warning disable xUnit1051

namespace xUnitV3Features;


public class Class1
{
    private readonly ITestOutputHelper _output;
    private readonly Guid guid;

    public Class1(ITestOutputHelper output, GuidFixutre guidFixutre)
    {
        guid = guidFixutre.Guid;
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

public class GuidFixutre : IDisposable
{
    public Guid Guid;

    public GuidFixutre()
    {
        Guid = Guid.NewGuid();
    }

    public void Dispose()
    {
        // dispose of the guid
    }
}