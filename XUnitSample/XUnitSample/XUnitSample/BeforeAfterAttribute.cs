using System.Reflection;
using Xunit.Sdk;

namespace XUnitSample;

public class BeforeAfterAttribute : BeforeAfterTestAttribute
{
    public override void Before(MethodInfo methodUnderTest)
    {
        var name = methodUnderTest.Name;
        var type = methodUnderTest.DeclaringType;
    }

    public override void After(MethodInfo methodUnderTest)
    {
        var name = methodUnderTest.Name;
        var type = methodUnderTest.DeclaringType;
    }
}
