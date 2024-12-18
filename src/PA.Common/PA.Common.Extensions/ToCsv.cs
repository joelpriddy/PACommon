namespace PA.Common.Extensions
{
    public static partial class CollectionExtensions
    {
        public static string ToCsv<T>(this IEnumerable<T> values, bool spaceBetween = false)
        {
            return values.Aggregate(string.Empty, (c, n) => $"{(c == string.Empty ? c : $"{c},{(spaceBetween ? " " : "")}")}{n}");
        }
    }
}
