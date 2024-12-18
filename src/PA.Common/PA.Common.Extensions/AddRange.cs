namespace PA.Common.Extensions
{
    public static partial class CollectionExtensions
    {
        /// <summary>
        /// Adds a range of objects of type T, to a collection of objects of type T.
        /// </summary>
        /// <typeparam name="T">The type of objects in the collections.</typeparam>
        /// <param name="collection">The collection of objects that is getting added to.</param>
        /// <param name="itemsToAdd">The collection of objects to add.</param>
        /// <returns></returns>
        public static IEnumerable<T> AddRange<T>(this IEnumerable<T> collection, IEnumerable<T> itemsToAdd)
        {
            var local = collection as IList<T> ?? collection.ToList();

            foreach (var item in itemsToAdd)
            {
                local.Add(item);
            }

            collection = local;

            return collection;
        }
    }
}
