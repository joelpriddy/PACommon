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
            _Logger?.LogDebug("Constructed successfully without initialization.");
        }

        public EncryptedSecretService(ILogger<EncryptedSecretService>? logger, IEncryptionService encryption, string collectionLocation, string collectionKey)
            : base(logger, encryption, collectionLocation, collectionKey)
        {
            _Logger = logger;
            _Logger?.LogDebug("Constructed successfully with initialization.");
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

            secrets[key] = value;// Update or add the secret
                                 // 
            var serializedSecrets = JsonSerializer.Serialize(secrets);

            serializedSecrets = _Encryption.Encrypt(serializedSecrets, Convert.FromBase64String(CollectionKey));
            File.WriteAllText(CollectionLocation, serializedSecrets);

            _Logger?.LogDebug($"Set secret for key: {key}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vaultLocation"></param>
        /// <param name="vaultKey"></param>
        /// <param name="secrets"></param>
        /// <exception cref="ArgumentNullException">Occurs when the provided vaultLocation or vaultKey are null, empty, or only contain white spaces.</exception>
        /// <exception cref="IOException">Occurs when the file name provided already exists.</exception>
        public void CreateSecretVault(string vaultLocation, string vaultKey, IDictionary<string,string>? secrets = null)
        {
            if (string.IsNullOrWhiteSpace(vaultLocation)) {
                _Logger?.LogCritical($"{nameof(vaultLocation)} must be a valid file location.");
                throw new ArgumentNullException(nameof(vaultLocation)); 
            }

            if (string.IsNullOrWhiteSpace(vaultKey)) 
            { 
                _Logger?.LogCritical($"{nameof(vaultKey)} must not be null, empty, or contain only white space.");
                throw new ArgumentNullException(nameof(vaultKey)); 
            }

            if (File.Exists(vaultLocation))
            {
                _Logger?.LogCritical($"A file already exists at {vaultLocation}.");
                throw new IOException($"A file already exists at {vaultLocation}.");
            }

            var serializedSecrets = JsonSerializer.Serialize(secrets ?? new Dictionary<string, string>());// Initialize an empty secret dictionary

            serializedSecrets = _Encryption.Encrypt(serializedSecrets, Convert.FromBase64String(vaultKey));
            File.WriteAllText(vaultLocation, serializedSecrets);// Write the encrypted vault to the specified location

            _Logger?.LogDebug($"Created a new secret vault at location {vaultLocation}.");
            // Update internal state to reference the newly created vault
            Init(new Dictionary<string, object> {{ nameof(CollectionLocation), vaultLocation }, { nameof(CollectionKey), vaultKey }});
        }
        #endregion
    }
}
