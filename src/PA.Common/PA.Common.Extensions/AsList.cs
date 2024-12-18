using System.Collections;
using System.Reflection;

namespace PA.Common.Extensions
{
    public static partial class TypeExtensions
    {
        public static IList? AsList(this Type type)
        {
            Type listGenericType = typeof(List<>);
            Type list = listGenericType.MakeGenericType(type);
            ConstructorInfo? ci = list.GetConstructor([]);

            if (ci == null) { return null; }

            return (IList?)ci.Invoke([]);
        }
    }
}
