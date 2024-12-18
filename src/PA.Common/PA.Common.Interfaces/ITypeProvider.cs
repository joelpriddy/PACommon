namespace PA.Common.Interfaces
{
    public interface ITypeProvider
    {
        Type GetImplementationType<TInterface>();
        Type GetImplementationType(Type interfaceType);
    }
}
