namespace PA.Common.Interfaces
{
    public interface IReadOnlySecretService : IInitializable
    {
        bool IsEncrypted { get; }
        string CollectionKey { get; }
        string CollectionLocation { get; }

        /// <summary>
        /// Gets the specified secret.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>The key of the secret to retrieve.</returns>
        string GetSecret(string key);

        /// <summary>
        /// Gets the specified secret asynchronously.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>The key of the secret to retrieve.</returns>
        Task<string> GetSecretAsync(string key);
    }
}
