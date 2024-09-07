using Microsoft.EntityFrameworkCore.Design;

namespace EFCoreLargeData;

public class TestDbContextDesignTime : IDesignTimeDbContextFactory<TestDbContext>
{
    public TestDbContext CreateDbContext(string[] args)
    {
        return new TestDbContext();
    }
}
