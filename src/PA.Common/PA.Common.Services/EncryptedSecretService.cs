using System.Text.Json;
using Microsoft.Extensions.Logging;
using PA.Common.Interfaces;

namespace PA.Common.Services
{
    public class EncryptedSecretService : ReadOnlyEncryptedSecretService, ISecretService
    {
        #region Non-Public Properties
        private readonly ILogger<EncryptedSecretService>? _Logger;
        #endregion

        #region Constructor(s) and Init
        public EncryptedSecretService(ILogger<EncryptedSecretService>? logger, IEncryptionService encryption) : base(logger, encryption)
        {
            _Logger = logger;
        }

        public EncryptedSecretService(ILogger<EncryptedSecretService>? logger, IEncryptionService encryption, string collectionLocation, string collectionKey)
            : base(logger, encryption, collectionLocation, collectionKey)
        {
            _Logger = logger;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the value for the specified existing secret. If the secret doesn't exist, this call will create it and set the value.
        /// </summary>
        /// <param name="key">The key for the desired secret.</param>
        /// <param name="value">The value to assign to the secret.</param>
        public void SetSecret(string key, string value)
        {
            EnsureInitialized();

            // Load existing secrets
            var secrets = LoadSecrets();

            // Update or add the secret
            secrets[key] = value;

            // Save the updated secrets back to the file
            SaveSecrets(secrets);
        }

        public void CreateSecretVault(string vaultLocation, string vaultKey, IDictionary<string,string>? secrets = null)
        {
            if (File.Exists(vaultLocation))
            {
                throw new IOException($"A vault already exists at {vaultLocation}.");
            }

            // Initialize an empty secret dictionary
            var serializedSecrets = JsonSerializer.Serialize(secrets ?? new Dictionary<string, string>());

            // Encrypt the vault if a key is provided
            if (!string.IsNullOrEmpty(vaultKey))
            {
                var password = Convert.FromBase64String(vaultKey);
                serializedSecrets = _Encryption.Encrypt(serializedSecrets, password);
            }

            // Write the encrypted or plain vault to the specified location
            File.WriteAllText(vaultLocation, serializedSecrets);

            // Update internal state to reference the newly created vault
            Init(new Dictionary<string, object> {{ nameof(CollectionLocation), vaultLocation }, { nameof(CollectionKey), vaultKey }});
        }
        #endregion

        #region Non-Public Methods
        /// <summary>
        /// Saves the secrets dictionary back to the file in encrypted format.
        /// </summary>
        /// <param name="secrets">The updated secrets dictionary.</param>
        private void SaveSecrets(IDictionary<string, string> secrets)
        {
            var serializedSecrets = JsonSerializer.Serialize(secrets);

            if (IsEncrypted)
            {
                var password = Convert.FromBase64String(CollectionKey);
                serializedSecrets = _Encryption.Encrypt(serializedSecrets, password);
            }

            File.WriteAllText(CollectionLocation, serializedSecrets);
        }
        #endregion
    }
}
