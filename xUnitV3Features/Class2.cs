using xUnitV3Features;

[assembly: AssemblyFixture(typeof(GuidFixutre))]
namespace xUnitV3Features;

public class Class2
{
    private readonly ITestOutputHelper _output;
    private readonly Guid guid;

    public static bool SkipWhen => true;

    public Class2(ITestOutputHelper output, GuidFixutre guidFixutre)
    {
        guid = guidFixutre.Guid;
        _output = output;
    }

    [Fact(Skip = "skip test", SkipWhen = nameof(SkipWhen))]
    public async Task Test1()
    {
        _output.WriteLine($"Test1: {guid}");

        await Task.Delay(1000, TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task Test2()
    {
        _output.WriteLine($"Test2: {guid}");

        await Task.Delay(2000, TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task Test3()
    {
        _output.WriteLine($"Test3: {guid}");

        await Task.Delay(5000, TestContext.Current.CancellationToken);
    }
}
