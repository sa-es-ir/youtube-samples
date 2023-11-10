namespace MultpleInterfaceImp;

public delegate IFooService StrategyFooService(FooType type);

public enum FooType
{
    Foo,
    Bar
}
