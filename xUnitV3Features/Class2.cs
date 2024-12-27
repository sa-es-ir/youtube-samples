namespace xUnitV3Features;

[Collection("GuidCollection")]
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

    [Fact(Skip = "skip", SkipWhen = nameof(SkipWhen))]
    public async Task Test1()
    {
        _output.WriteLine($"Test1: {guid}");

        await Task.Delay(1000);

    }

    private async Task Waiting(ITestOutputHelper output, CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            _output.WriteLine("running");

            try
            {
                await Task.Delay(500, cancellationToken);
            }
            catch (Exception ex)
            {
                output.WriteLine(ex.Message);
            }
        }
    }

    [Fact(Timeout = 1500)]
    public async Task Test2()
    {
        _output.WriteLine($"Test2: {guid}");

        await Waiting(_output, TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task Test3()
    {
        _output.WriteLine($"Test3: {guid}");

        await Task.Delay(5000);
    }
}
