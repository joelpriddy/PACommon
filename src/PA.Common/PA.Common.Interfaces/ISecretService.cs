namespace PA.Common.Interfaces
{
    public interface ISecretService : IReadOnlySecretService
    {
        /// <summary>
        /// Sets the value for the specified existing secret. If the secret doesn't exist, this call will create it and set the value.
        /// </summary>
        /// <param name="key">The key for the desired secret.</param>
        /// <param name="value">The value to assign to the secret.</param>
        void SetSecret(string key, string value);

        /// <summary>
        /// Creates the secret vault in which the secrets will be stored.
        /// </summary>
        /// <param name="vaultLocation">The location where the vault will be created.</param>
        /// <param name="vaultKey">The key to encrypt the vault.</param>
        void CreateSecretVault(string vaultLocation, string vaultKey, IDictionary<string,string>? secrets = null);
    }
}