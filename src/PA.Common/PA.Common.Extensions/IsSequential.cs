using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PA.Common.Extensions
{
    public static partial class CollectionExtensions
    {
        public static bool IsSequential(this IEnumerable<short> items)
        {
            return items.IsSequentialAsc() || items.IsSequentialDesc();
        }

        public static bool IsSequentialAsc(this IEnumerable<short> items)
        {
            return items.Zip(items.Skip(1), (a, b) => (a + 1) == b).All(x => x);
        }

        public static bool IsSequentialDesc(this IEnumerable<short> items)
        {
            return items.Zip(items.Skip(1), (a, b) => (a - 1) == b).All(x => x);
        }

        //

        public static bool IsSequential(this IEnumerable<ushort> items)
        {
            return items.IsSequentialAsc() || items.IsSequentialDesc();
        }

        public static bool IsSequentialAsc(this IEnumerable<ushort> items)
        {
            return items.Zip(items.Skip(1), (a, b) => (a + 1) == b).All(x => x);
        }

        public static bool IsSequentialDesc(this IEnumerable<ushort> items)
        {
            return items.Zip(items.Skip(1), (a, b) => (a - 1) == b).All(x => x);
        }

        //

        public static bool IsSequential(this IEnumerable<int> items)
        {
            return items.IsSequentialAsc() || items.IsSequentialDesc();
        }

        public static bool IsSequentialAsc(this IEnumerable<int> items)
        {
            return items.Zip(items.Skip(1), (a, b) => (a + 1) == b).All(x => x);
        }

        public static bool IsSequentialDesc(this IEnumerable<int> items)
        {
            return items.Zip(items.Skip(1), (a, b) => (a - 1) == b).All(x => x);
        }

        //

        public static bool IsSequential(this IEnumerable<uint> items)
        {
            return items.IsSequentialAsc() || items.IsSequentialDesc();
        }

        public static bool IsSequentialAsc(this IEnumerable<uint> items)
        {
            return items.Zip(items.Skip(1), (a, b) => (a + 1) == b).All(x => x);
        }

        public static bool IsSequentialDesc(this IEnumerable<uint> items)
        {
            return items.Zip(items.Skip(1), (a, b) => (a - 1) == b).All(x => x);
        }

        //

        public static bool IsSequential(this IEnumerable<long> items)
        {
            return items.IsSequentialAsc() || items.IsSequentialDesc();
        }

        public static bool IsSequentialAsc(this IEnumerable<long> items)
        {
            return items.Zip(items.Skip(1), (a, b) => (a + 1) == b).All(x => x);
        }

        public static bool IsSequentialDesc(this IEnumerable<long> items)
        {
            return items.Zip(items.Skip(1), (a, b) => (a - 1) == b).All(x => x);
        }

        //

        public static bool IsSequential(this IEnumerable<ulong> items)
        {
            return items.IsSequentialAsc() || items.IsSequentialDesc();
        }

        public static bool IsSequentialAsc(this IEnumerable<ulong> items)
        {
            return items.Zip(items.Skip(1), (a, b) => (a + 1) == b).All(x => x);
        }

        public static bool IsSequentialDesc(this IEnumerable<ulong> items)
        {
            return items.Zip(items.Skip(1), (a, b) => (a - 1) == b).All(x => x);
        }

        //

        public static bool IsSequential(this IEnumerable<short?> items)
        {
            return items.IsSequentialAsc() || items.IsSequentialDesc();
        }

        public static bool IsSequentialAsc(this IEnumerable<short?> items)
        {
            return items.Zip(items.Skip(1), (a, b) => (a + 1) == b).All(x => x);
        }

        public static bool IsSequentialDesc(this IEnumerable<short?> items)
        {
            return items.Zip(items.Skip(1), (a, b) => (a - 1) == b).All(x => x);
        }

        //

        public static bool IsSequential(this IEnumerable<ushort?> items)
        {
            return items.IsSequentialAsc() || items.IsSequentialDesc();
        }

        public static bool IsSequentialAsc(this IEnumerable<ushort?> items)
        {
            return items.Zip(items.Skip(1), (a, b) => (a + 1) == b).All(x => x);
        }

        public static bool IsSequentialDesc(this IEnumerable<ushort?> items)
        {
            return items.Zip(items.Skip(1), (a, b) => (a - 1) == b).All(x => x);
        }

        //

        public static bool IsSequential(this IEnumerable<int?> items)
        {
            return items.IsSequentialAsc() || items.IsSequentialDesc();
        }

        public static bool IsSequentialAsc(this IEnumerable<int?> items)
        {
            return items.Zip(items.Skip(1), (a, b) => (a + 1) == b).All(x => x);
        }

        public static bool IsSequentialDesc(this IEnumerable<int?> items)
        {
            return items.Zip(items.Skip(1), (a, b) => (a - 1) == b).All(x => x);
        }

        //

        public static bool IsSequential(this IEnumerable<uint?> items)
        {
            return items.IsSequentialAsc() || items.IsSequentialDesc();
        }

        public static bool IsSequentialAsc(this IEnumerable<uint?> items)
        {
            return items.Zip(items.Skip(1), (a, b) => (a + 1) == b).All(x => x);
        }

        public static bool IsSequentialDesc(this IEnumerable<uint?> items)
        {
            return items.Zip(items.Skip(1), (a, b) => (a - 1) == b).All(x => x);
        }

        //

        public static bool IsSequential(this IEnumerable<long?> items)
        {
            return items.IsSequentialAsc() || items.IsSequentialDesc();
        }

        public static bool IsSequentialAsc(this IEnumerable<long?> items)
        {
            return items.Zip(items.Skip(1), (a, b) => (a + 1) == b).All(x => x);
        }

        public static bool IsSequentialDesc(this IEnumerable<long?> items)
        {
            return items.Zip(items.Skip(1), (a, b) => (a - 1) == b).All(x => x);
        }

        //

        public static bool IsSequential(this IEnumerable<ulong?> items)
        {
            return items.IsSequentialAsc() || items.IsSequentialDesc();
        }

        public static bool IsSequentialAsc(this IEnumerable<ulong?> items)
        {
            return items.Zip(items.Skip(1), (a, b) => (a + 1) == b).All(x => x);
        }

        public static bool IsSequentialDesc(this IEnumerable<ulong?> items)
        {
            return items.Zip(items.Skip(1), (a, b) => (a - 1) == b).All(x => x);
        }
    }
}
