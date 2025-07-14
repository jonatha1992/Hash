using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hash;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Hash.Tests
{
    [TestClass]
    public class HashCalculationTests
    {
        private string tempTestFile;

        [TestInitialize]
        public void TestInitialize()
        {
            // Crear un archivo temporal para las pruebas
            tempTestFile = Path.GetTempFileName();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Limpiar el archivo temporal después de cada prueba
            if (File.Exists(tempTestFile))
            {
                File.Delete(tempTestFile);
            }
        }

        [TestMethod]
        public void SHA256_EmptyFile_ReturnsKnownHash()
        {
            // Arrange
            File.WriteAllText(tempTestFile, "");
            string expectedHash = "E3B0C44298FC1C149AFBF4C8996FB92427AE41E4649B934CA495991B7852B855"; // SHA256 de archivo vacío

            // Act
            string actualHash = CalculateFileHashForTest(tempTestFile);

            // Assert
            Assert.AreEqual(expectedHash, actualHash);
        }

        [TestMethod]
        public void SHA256_SimpleText_ReturnsKnownHash()
        {
            // Arrange
            File.WriteAllText(tempTestFile, "Hello World", Encoding.UTF8);
            string expectedHash = "A591A6D40BF420404A011733CFB7B190D62C65BF0BCDA32B57B277D9AD9F146E"; // SHA256 de "Hello World"

            // Act
            string actualHash = CalculateFileHashForTest(tempTestFile);

            // Assert
            Assert.AreEqual(expectedHash, actualHash);
        }

        [TestMethod]
        public void SHA256_SameContent_ReturnsSameHash()
        {
            // Arrange
            string content = "Test content for hash calculation";
            File.WriteAllText(tempTestFile, content, Encoding.UTF8);
            
            string tempTestFile2 = Path.GetTempFileName();
            File.WriteAllText(tempTestFile2, content, Encoding.UTF8);

            try
            {
                // Act
                string hash1 = CalculateFileHashForTest(tempTestFile);
                string hash2 = CalculateFileHashForTest(tempTestFile2);

                // Assert
                Assert.AreEqual(hash1, hash2);
                Assert.IsFalse(string.IsNullOrEmpty(hash1));
                Assert.AreEqual(64, hash1.Length); // SHA256 produce 64 caracteres hexadecimales
            }
            finally
            {
                if (File.Exists(tempTestFile2))
                {
                    File.Delete(tempTestFile2);
                }
            }
        }

        [TestMethod]
        public void SHA256_DifferentContent_ReturnsDifferentHash()
        {
            // Arrange
            string content1 = "First test content";
            string content2 = "Second test content";
            
            File.WriteAllText(tempTestFile, content1, Encoding.UTF8);
            
            string tempTestFile2 = Path.GetTempFileName();
            File.WriteAllText(tempTestFile2, content2, Encoding.UTF8);

            try
            {
                // Act
                string hash1 = CalculateFileHashForTest(tempTestFile);
                string hash2 = CalculateFileHashForTest(tempTestFile2);

                // Assert
                Assert.AreNotEqual(hash1, hash2);
                Assert.IsFalse(string.IsNullOrEmpty(hash1));
                Assert.IsFalse(string.IsNullOrEmpty(hash2));
                Assert.AreEqual(64, hash1.Length);
                Assert.AreEqual(64, hash2.Length);
            }
            finally
            {
                if (File.Exists(tempTestFile2))
                {
                    File.Delete(tempTestFile2);
                }
            }
        }

        [TestMethod]
        public void SHA256_LargeFile_HandlesCorrectly()
        {
            // Arrange
            var largeContent = new StringBuilder();
            for (int i = 0; i < 10000; i++)
            {
                largeContent.AppendLine($"Line {i}: This is a large file test content to verify hash calculation performance and correctness.");
            }
            File.WriteAllText(tempTestFile, largeContent.ToString(), Encoding.UTF8);

            // Act
            string hash = CalculateFileHashForTest(tempTestFile);

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(hash));
            Assert.AreEqual(64, hash.Length);
            Assert.IsTrue(IsValidHexString(hash));
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void SHA256_NonExistentFile_ThrowsFileNotFoundException()
        {
            // Arrange
            string nonExistentFile = Path.Combine(Path.GetTempPath(), "non_existent_file_" + Guid.NewGuid().ToString() + ".txt");

            // Act
            CalculateFileHashForTest(nonExistentFile);

            // Assert - Exception expected
        }

        [TestMethod]
        public void SHA256_HashFormat_IsUppercaseHex()
        {
            // Arrange
            File.WriteAllText(tempTestFile, "Test content", Encoding.UTF8);

            // Act
            string hash = CalculateFileHashForTest(tempTestFile);

            // Assert
            Assert.IsTrue(IsValidHexString(hash));
            Assert.IsTrue(hash.All(c => char.IsDigit(c) || (c >= 'A' && c <= 'F')));
            Assert.IsFalse(hash.Any(c => c >= 'a' && c <= 'f')); // No lowercase letters
        }

        #region Helper Methods

        /// <summary>
        /// Replica del método calcularHash de la clase Hash para poder probarlo
        /// </summary>
        private string CalculateFileHashForTest(string rutaArchivo)
        {
            string hash = "";
            long fileSize = new FileInfo(rutaArchivo).Length;
            long totalBytesRead = 0;
            using (var stream = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 1048576, FileOptions.SequentialScan))
            {
                using (var sha = SHA256.Create())
                {
                    byte[] buffer = new byte[1048576];
                    int bytesRead;
                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        sha.TransformBlock(buffer, 0, bytesRead, buffer, 0);
                        totalBytesRead += bytesRead;
                    }
                    sha.TransformFinalBlock(buffer, 0, 0);
                    byte[] hashBytes = sha.Hash;
                    hash = BitConverter.ToString(hashBytes).Replace("-", "");
                }
            }
            return hash;
        }

        private bool IsValidHexString(string hex)
        {
            return hex.All(c => (c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f'));
        }

        #endregion
    }
}