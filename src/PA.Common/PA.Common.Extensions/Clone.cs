namespace PA.Common.Extensions
{
    public static partial class ObjectExtensions
    {
        /// Code originally from: http://stackoverflow.com/questions/2023210/cannot-access-protected-member-object-memberwiseclone
        /// <summary>
        /// Helper function to do a shallow clone on basic objects (Models)
        /// </summary>
        /// <param name="obj">The object to clone</param>
        /// <returns></returns>
        public static T? Clone<T>(this T obj)
        {
            if (obj == null) { return default; }

            var inst = obj.GetType().GetMethod("MemberwiseClone", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            if (inst != null) { return (T?)inst.Invoke(obj, null); }

            return default;
        }
    }
}
