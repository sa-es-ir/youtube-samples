namespace MultpleInterfaceImp
{
    public interface IFooService
    {
        void DoSomething();
    }

    public class FooService : IFooService
    {
        public void DoSomething()
        {
            Console.WriteLine("I'm Foo");
        }
    }

    public class BarService : IFooService
    {
        public void DoSomething()
        {
            Console.WriteLine("I'm Bar");
        }
    }
}
