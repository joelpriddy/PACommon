using System.Text.Json;
using Microsoft.Extensions.Logging;
using PA.Common.Interfaces;

namespace PA.Common.Services
{
    public class ReadOnlyEncryptedSecretService : IReadOnlySecretService
    {
        #region Non-Public Properties
        private readonly ILogger<ReadOnlyEncryptedSecretService>? _Logger;
        protected readonly IEncryptionService _Encryption;
        private bool _Initialized;
        #endregion

        #region Public Properties
        public string CollectionKey { get; private set; } = string.Empty;
        public string CollectionLocation { get; private set; } = string.Empty;

        public bool IsEncrypted { get; private set; } = true;
        #endregion

        #region Constructor(s) and Init
        public ReadOnlyEncryptedSecretService(ILogger<ReadOnlyEncryptedSecretService>? logger, IEncryptionService encryption)
        {
            _Logger = logger;
            _Encryption = encryption;
            _Logger?.LogDebug("Constructed successfully without initialization.");
        }

        public ReadOnlyEncryptedSecretService(ILogger<ReadOnlyEncryptedSecretService>? logger, IEncryptionService encryption, string collectionLocation, string collectionKey)
        {
            _Logger = logger;
            _Encryption = encryption;
            CollectionLocation = collectionLocation;
            CollectionKey = collectionKey;
            _Logger?.LogDebug("Constructed successfully with initialization.");
        }

        public void Init(IDictionary<string, object> args)
        {
            if (args.TryGetValue(nameof(CollectionLocation), out var location))
            {
                CollectionLocation = location?.ToString() ?? string.Empty;
            }

            if (args.TryGetValue(nameof(CollectionKey), out var key))
            {
                CollectionKey = key?.ToString() ?? string.Empty;
            }

            EnsureInitialized();
            _Logger?.LogDebug("Initialized.");
        }
        #endregion

        #region Public Methods
        public string GetSecret(string key)
        {
            EnsureInitialized();

            try
            {
                return LoadSecrets().TryGetValue(key, out string? value) ? value : string.Empty;
            }
            catch(Exception ex)
            {
                _Logger?.LogError(ex, string.Empty);
                throw;
            }
            finally
            {
                _Logger?.LogDebug($"Retrieved secret for key: {key}");
            }
        }

        public async Task<string> GetSecretAsync(string key)
        {
            EnsureInitialized();

            try
            {
                return (await LoadSecretsAsync()).TryGetValue(key, out var value) ? value : string.Empty;
            }
            catch(Exception ex)
            {
                _Logger?.LogError(ex, string.Empty);
                throw;
            }
            finally
            {
                _Logger?.LogDebug($"Retrieved secret asynchronously for key: {key}");
            }
        }
        #endregion

        #region Non-Public Methods
        protected void EnsureInitialized()
        {
            if (!_Initialized)
            {
                if (string.IsNullOrWhiteSpace(CollectionLocation)) { throw new ArgumentNullException(nameof(CollectionLocation)); }
                if (string.IsNullOrWhiteSpace(CollectionKey))
                {
                    throw new ArgumentNullException(nameof(CollectionKey));
                }

                _Initialized = true;
            }
        }

        protected IDictionary<string, string> LoadSecrets()
        {
            if (!File.Exists(CollectionLocation))
            {
                throw new FileNotFoundException($"Secrets file not found at location: {CollectionLocation}");
            }

            var fileContent = File.ReadAllText(CollectionLocation);

            if (IsEncrypted)
            {
                var password = Convert.FromBase64String(CollectionKey);
                fileContent = _Encryption.Decrypt(fileContent, password);
            }

            return JsonSerializer.Deserialize<IDictionary<string, string>>(fileContent) ?? new Dictionary<string, string>();
        }

        protected async Task<IDictionary<string, string>> LoadSecretsAsync()
        {
            if (!File.Exists(CollectionLocation))
            {
                throw new FileNotFoundException($"Secrets file not found at location: {CollectionLocation}");
            }

            var fileContent = await File.ReadAllTextAsync(CollectionLocation);

            if (IsEncrypted)
            {
                var password = Convert.FromBase64String(CollectionKey);
                fileContent = await _Encryption.DecryptAsync(fileContent, password);
            }

            return JsonSerializer.Deserialize<IDictionary<string, string>>(fileContent) ?? new Dictionary<string, string>();
        }
        #endregion
    }
}
