namespace PA.Common.Interfaces
{
    public interface IInitializable
    {
        /// <summary>
        /// A method to initialize the object.
        /// </summary>
        /// <param name="args">A dictionary containing the values and objects necessary for initialization of the object.</param>
        void Init(IDictionary<string, object> args);
    }
}
