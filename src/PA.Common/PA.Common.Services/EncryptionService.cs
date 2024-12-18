using System.Security.Cryptography;
using System.Text;
using PA.Common.Interfaces;

namespace PA.Common.Services
{
    public class EncryptionService : IEncryptionService
    {
        public string Encrypt(string unencrypted, byte[] password)
        {
            using var aes = Aes.Create();
            aes.Key = DeriveKey(password, aes.KeySize / 8); // Derive the key
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            var unencryptedBytes = Encoding.UTF8.GetBytes(unencrypted);

            var encryptedBytes = encryptor.TransformFinalBlock(unencryptedBytes, 0, unencryptedBytes.Length);

            var result = Convert.ToBase64String(aes.IV) + ":" + Convert.ToBase64String(encryptedBytes);
            return result;
        }

        public string Decrypt(string encrypted, byte[] password)
        {
            var parts = encrypted.Split(':');
            var iv = Convert.FromBase64String(parts[0]);
            var encryptedBytes = Convert.FromBase64String(parts[1]);

            using var aes = Aes.Create();
            aes.Key = DeriveKey(password, aes.KeySize / 8); // Derive the key
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            var decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

            return Encoding.UTF8.GetString(decryptedBytes);
        }

        private byte[] DeriveKey(byte[] password, int keySize)
        {
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(password);

            // Return the required number of bytes for the key
            return hash[..keySize];
        }

        public async Task<string> EncryptAsync(string unencrypted, byte[] password)
        {
            return await Task.FromResult(Encrypt(unencrypted, password));
        }

        public async Task<string> DecryptAsync(string encrypted, byte[] password)
        {
            return await Task.FromResult(Decrypt(encrypted, password));
        }
    }
}
