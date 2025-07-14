using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hash;
using System;
using System.IO;
using System.Text;

namespace Hash.Tests
{
    [TestClass]
    public class HashUtilitiesTests
    {
        private string tempTestFile;

        [TestInitialize]
        public void TestInitialize()
        {
            tempTestFile = Path.GetTempFileName();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (File.Exists(tempTestFile))
            {
                File.Delete(tempTestFile);
            }
        }

        #region CalcularHashSHA256 Tests

        [TestMethod]
        public void CalcularHashSHA256_ArchivoVacio_DevuelveHashConocido()
        {
            // Arrange
            File.WriteAllText(tempTestFile, "");
            string expectedHash = "E3B0C44298FC1C149AFBF4C8996FB92427AE41E4649B934CA495991B7852B855";

            // Act
            string actualHash = HashUtilities.CalcularHashSHA256(tempTestFile);

            // Assert
            Assert.AreEqual(expectedHash, actualHash);
        }

        [TestMethod]
        public void CalcularHashSHA256_ContenidoSimple_DevuelveHashCorrecto()
        {
            // Arrange
            File.WriteAllText(tempTestFile, "Hello World", Encoding.UTF8);
            string expectedHash = "A591A6D40BF420404A011733CFB7B190D62C65BF0BCDA32B57B277D9AD9F146E";

            // Act
            string actualHash = HashUtilities.CalcularHashSHA256(tempTestFile);

            // Assert
            Assert.AreEqual(expectedHash, actualHash);
        }

        [TestMethod]
        public void CalcularHashSHA256_MismoContenido_MismoHash()
        {
            // Arrange
            string content = "Contenido de prueba para hash";
            File.WriteAllText(tempTestFile, content, Encoding.UTF8);
            
            string tempFile2 = Path.GetTempFileName();
            File.WriteAllText(tempFile2, content, Encoding.UTF8);

            try
            {
                // Act
                string hash1 = HashUtilities.CalcularHashSHA256(tempTestFile);
                string hash2 = HashUtilities.CalcularHashSHA256(tempFile2);

                // Assert
                Assert.AreEqual(hash1, hash2);
            }
            finally
            {
                if (File.Exists(tempFile2))
                    File.Delete(tempFile2);
            }
        }

        [TestMethod]
        public void CalcularHashSHA256_ContenidoDiferente_HashDiferente()
        {
            // Arrange
            File.WriteAllText(tempTestFile, "Contenido 1", Encoding.UTF8);
            
            string tempFile2 = Path.GetTempFileName();
            File.WriteAllText(tempFile2, "Contenido 2", Encoding.UTF8);

            try
            {
                // Act
                string hash1 = HashUtilities.CalcularHashSHA256(tempTestFile);
                string hash2 = HashUtilities.CalcularHashSHA256(tempFile2);

                // Assert
                Assert.AreNotEqual(hash1, hash2);
            }
            finally
            {
                if (File.Exists(tempFile2))
                    File.Delete(tempFile2);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalcularHashSHA256_RutaVacia_LanzaExcepcion()
        {
            // Act
            HashUtilities.CalcularHashSHA256("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalcularHashSHA256_RutaNull_LanzaExcepcion()
        {
            // Act
            HashUtilities.CalcularHashSHA256(null);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void CalcularHashSHA256_ArchivoInexistente_LanzaExcepcion()
        {
            // Arrange
            string archivoInexistente = Path.Combine(Path.GetTempPath(), "archivo_inexistente_" + Guid.NewGuid().ToString());

            // Act
            HashUtilities.CalcularHashSHA256(archivoInexistente);
        }

        #endregion

        #region EsHashSHA256Valido Tests

        [TestMethod]
        public void EsHashSHA256Valido_HashValido_DevuelveTrue()
        {
            // Arrange
            string hashValido = "A591A6D40BF420404A011733CFB7B190D62C65BF0BCDA32B57B277D9AD9F146E";

            // Act
            bool resultado = HashUtilities.EsHashSHA256Valido(hashValido);

            // Assert
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void EsHashSHA256Valido_HashValidoMinusculas_DevuelveTrue()
        {
            // Arrange
            string hashValido = "a591a6d40bf420404a011733cfb7b190d62c65bf0bcda32b57b277d9ad9f146e";

            // Act
            bool resultado = HashUtilities.EsHashSHA256Valido(hashValido);

            // Assert
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void EsHashSHA256Valido_HashMixto_DevuelveTrue()
        {
            // Arrange
            string hashValido = "A591a6D40bF420404A011733CFb7B190d62C65BF0BCDA32B57B277D9AD9F146E";

            // Act
            bool resultado = HashUtilities.EsHashSHA256Valido(hashValido);

            // Assert
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void EsHashSHA256Valido_HashCorto_DevuelveFalse()
        {
            // Arrange
            string hashCorto = "A591A6D40BF420404A011733CFB7B190D62C65BF0BCDA32B57B277D9AD9F146";

            // Act
            bool resultado = HashUtilities.EsHashSHA256Valido(hashCorto);

            // Assert
            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void EsHashSHA256Valido_HashLargo_DevuelveFalse()
        {
            // Arrange
            string hashLargo = "A591A6D40BF420404A011733CFB7B190D62C65BF0BCDA32B57B277D9AD9F146EA";

            // Act
            bool resultado = HashUtilities.EsHashSHA256Valido(hashLargo);

            // Assert
            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void EsHashSHA256Valido_ConCaracteresInvalidos_DevuelveFalse()
        {
            // Arrange
            string hashInvalido = "A591A6D40BF420404A011733CFB7B190D62C65BF0BCDA32B57B277D9AD9F146G";

            // Act
            bool resultado = HashUtilities.EsHashSHA256Valido(hashInvalido);

            // Assert
            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void EsHashSHA256Valido_CadenaVacia_DevuelveFalse()
        {
            // Act
            bool resultado = HashUtilities.EsHashSHA256Valido("");

            // Assert
            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void EsHashSHA256Valido_CadenaNull_DevuelveFalse()
        {
            // Act
            bool resultado = HashUtilities.EsHashSHA256Valido(null);

            // Assert
            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void EsHashSHA256Valido_ConEspacios_DevuelveFalse()
        {
            // Arrange
            string hashConEspacios = "A591A6D4 0BF420404A011733CFB7B190D62C65BF0BCDA32B57B277D9AD9F146E";

            // Act
            bool resultado = HashUtilities.EsHashSHA256Valido(hashConEspacios);

            // Assert
            Assert.IsFalse(resultado);
        }

        #endregion
    }
}