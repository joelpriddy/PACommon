namespace PA.Common.Interfaces
{
    public interface IEncryptionService
    {
        string Encrypt(string unencrypted, byte[] password);
        string Decrypt(string encrypted, byte[] password);
        Task<string> DecryptAsync(string encrypted, byte[] password);
        Task<string> EncryptAsync(string unencrypted, byte[] password);
    }
}
