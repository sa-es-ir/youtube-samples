using Xunit;

namespace XUnitSample
{
    public class UnitTest1
    {
        [Fact]
        [BeforeAfter]
        public async Task Test1()
        {
            await Task.Delay(1000);
        }
    }
}