using System.Reflection;

namespace PA.Common.Extensions
{
    public static partial class InterfaceExtensions
    {
        public static bool IsInterface<TInterface>(this Type tImplementation)
        {
            return tImplementation == typeof(TInterface)
                   || typeof(TInterface).IsAssignableFrom(tImplementation)
                   || typeof(TInterface).GetTypeInfo().IsAssignableFrom(tImplementation.GetTypeInfo());
        }
    }
}
