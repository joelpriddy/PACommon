using Microsoft.VisualStudio.TestTools.UnitTesting;
using PA.Common.Interfaces;
using PA.Common.Services;
using System.Text;

[TestClass]
public class EncryptionServiceTests
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private IEncryptionService _Encryption;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    [TestInitialize]
    public void Setup()
    {
        _Encryption = new EncryptionService();
    }

    [TestMethod]
    public void Encrypt_Decrypt_ShouldReturnOriginalValue()
    {
        // Arrange
        string original = "TestString";
        byte[] password = Encoding.UTF8.GetBytes("SecurePassword");

        // Act
        string encrypted = _Encryption.Encrypt(original, password);
        string decrypted = _Encryption.Decrypt(encrypted, password);

        // Assert
        Assert.AreEqual(original, decrypted);
    }

    [TestMethod]
    public async Task EncryptAsync_DecryptAsync_ShouldReturnOriginalValue()
    {
        // Arrange
        string original = "AsyncTestString";
        byte[] password = Encoding.UTF8.GetBytes("SecurePassword");

        // Act
        string encrypted = await _Encryption.EncryptAsync(original, password);
        string decrypted = await _Encryption.DecryptAsync(encrypted, password);

        // Assert
        Assert.AreEqual(original, decrypted);
    }
}
