using Microsoft.Extensions.Logging;
using Moq;
using PA.Common.Interfaces;
using PA.Common.Services;
using System.Text;

namespace PA.Common.Test
{
    [TestClass]
    public class SecretServiceTests
    {
        private const string TestVault = "test_vault.json";
        private const string TestPassword = "SecurePassword";
        private IEncryptionService? _Encryption;
        private ISecretService? _SecretService;
        private IReadOnlySecretService? _ReadOnlySecretService;
        private Mock<ILogger<EncryptedSecretService>>? _MockSecretServiceLogger;
        private Mock<ILogger<ReadOnlyEncryptedSecretService>>? _MockRoServiceLogger;

        [TestInitialize]
        public void Setup()
        {
            _Encryption = new EncryptionService();

            // Mock loggers
            _MockSecretServiceLogger = new Mock<ILogger<EncryptedSecretService>>();
            _MockRoServiceLogger = new Mock<ILogger<ReadOnlyEncryptedSecretService>>();

            // Create a new vault for testing SecretService
            _SecretService = new EncryptedSecretService(_MockSecretServiceLogger.Object, _Encryption);
            _SecretService.CreateSecretVault(TestVault, Convert.ToBase64String(Encoding.UTF8.GetBytes(TestPassword)));
            _SecretService.Init(new Dictionary<string, object>
        {
            { nameof(_SecretService.CollectionLocation), TestVault },
            { nameof(_SecretService.CollectionKey), Convert.ToBase64String(Encoding.UTF8.GetBytes(TestPassword)) }
        });

            // Initialize ReadOnlySecretService for testing
            _ReadOnlySecretService = new ReadOnlyEncryptedSecretService(_MockRoServiceLogger.Object, _Encryption);
            _ReadOnlySecretService.Init(new Dictionary<string, object>
        {
            { nameof(_ReadOnlySecretService.CollectionLocation), TestVault },
            { nameof(_ReadOnlySecretService.CollectionKey), Convert.ToBase64String(Encoding.UTF8.GetBytes(TestPassword)) }
        });
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(TestVault))
            {
                File.Delete(TestVault);
            }
        }

        // ===========================
        // Tests for ReadOnlySecretService
        // ===========================
        [TestMethod]
        public void ReadOnly_GetSecret_ShouldLogAndReturnCorrectValue()
        {
            // Arrange
            _SecretService.SetSecret("DB_HOST", "localhost");

            // Act
            string host = _ReadOnlySecretService.GetSecret("DB_HOST");

            // Assert
            Assert.AreEqual("localhost", host);

            _MockRoServiceLogger.Verify(
                x => x.Log(LogLevel.Debug,
                           It.IsAny<EventId>(),
                           It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Retrieved secret for key: DB_HOST")),
                           null,
                           It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [TestMethod]
        public async Task ReadOnly_GetSecretAsync_ShouldLogAndReturnCorrectValue()
        {
            // Arrange
            _SecretService.SetSecret("DB_NAME", "TestDatabase");

            // Act
            string name = await _ReadOnlySecretService.GetSecretAsync("DB_NAME");

            // Assert
            Assert.AreEqual("TestDatabase", name);

            _MockRoServiceLogger.Verify(
                x => x.Log(LogLevel.Debug,
                           It.IsAny<EventId>(),
                           It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Retrieved secret asynchronously for key: DB_NAME")),
                           null,
                           It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        // ===========================
        // Tests for SecretService
        // ===========================
        [TestMethod]
        public void SecretService_SetSecret_ShouldLogAndUpdateSecretValue()
        {
            // Act
            _SecretService.SetSecret("DB_HOST", "localhost");

            // Assert
            string host = _SecretService.GetSecret("DB_HOST");
            Assert.AreEqual("localhost", host);

            _MockSecretServiceLogger.Verify(
                x => x.Log(LogLevel.Debug,
                           It.IsAny<EventId>(),
                           It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Set secret for key: DB_HOST")),
                           null,
                           It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [TestMethod]
        public void SecretService_CreateSecretVault_ShouldLogCreation()
        {
            // Assert
            Assert.IsTrue(File.Exists(TestVault));

            var fileContent = File.ReadAllText(TestVault);
            Assert.IsFalse(string.IsNullOrEmpty(fileContent));

            _MockSecretServiceLogger.Verify(
                x => x.Log(LogLevel.Debug,
                           It.IsAny<EventId>(),
                           It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Created a new secret vault at location")),
                           null,
                           It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [TestMethod]
        public void SecretService_SetSecret_ShouldLogCreationOfNewSecret()
        {
            // Act
            _SecretService.SetSecret("NEW_SECRET", "NewValue");

            // Assert
            string newSecret = _SecretService.GetSecret("NEW_SECRET");
            Assert.AreEqual("NewValue", newSecret);

            _MockSecretServiceLogger.Verify(
                x => x.Log(LogLevel.Debug,
                           It.IsAny<EventId>(),
                           It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Set secret for key: NEW_SECRET")),
                           null,
                           It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }
    }
}
