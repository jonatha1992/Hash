using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hash;
using System.IO;

namespace Hash.Tests
{
    [TestClass]
    public class BEArchivoTests
    {
        [TestMethod]
        public void VerificarPeso_PesoEnBytes_DevuelveKB()
        {
            // Arrange
            var archivo = new BEArchivo();
            archivo.Peso = 512; // 512 bytes

            // Act
            archivo.VerificarPeso();

            // Assert
            Assert.AreEqual("0.50 KB", archivo.PesoArchivo);
        }

        [TestMethod]
        public void VerificarPeso_PesoEnKB_DevuelveKB()
        {
            // Arrange
            var archivo = new BEArchivo();
            archivo.Peso = 1024; // 1 KB

            // Act
            archivo.VerificarPeso();

            // Assert
            Assert.AreEqual("1.00 KB", archivo.PesoArchivo);
        }

        [TestMethod]
        public void VerificarPeso_PesoEnMB_DevuelveMB()
        {
            // Arrange
            var archivo = new BEArchivo();
            archivo.Peso = 1024 * 1024; // 1 MB

            // Act
            archivo.VerificarPeso();

            // Assert
            Assert.AreEqual("1.00 MB", archivo.PesoArchivo);
        }

        [TestMethod]
        public void VerificarPeso_PesoEnGB_DevuelveGB()
        {
            // Arrange
            var archivo = new BEArchivo();
            archivo.Peso = 1024L * 1024L * 1024L; // 1 GB

            // Act
            archivo.VerificarPeso();

            // Assert
            Assert.AreEqual("1.00 GB", archivo.PesoArchivo);
        }

        [TestMethod]
        public void VerificarPeso_PesoGrande_DevuelveGB()
        {
            // Arrange
            var archivo = new BEArchivo();
            archivo.Peso = 2L * 1024L * 1024L * 1024L; // 2 GB

            // Act
            archivo.VerificarPeso();

            // Assert
            Assert.AreEqual("2.00 GB", archivo.PesoArchivo);
        }

        [TestMethod]
        public void VerificarPeso_PesoCero_DevuelveCeroKB()
        {
            // Arrange
            var archivo = new BEArchivo();
            archivo.Peso = 0;

            // Act
            archivo.VerificarPeso();

            // Assert
            Assert.AreEqual("0.00 KB", archivo.PesoArchivo);
        }

        [TestMethod]
        public void VerificarPeso_PesoDecimal_FormateoCorrect()
        {
            // Arrange
            var archivo = new BEArchivo();
            archivo.Peso = 1536; // 1.5 KB

            // Act
            archivo.VerificarPeso();

            // Assert
            Assert.AreEqual("1.50 KB", archivo.PesoArchivo);
        }

        [TestMethod]
        public void VerificarPeso_PesoLimiteKB_DevuelveKB()
        {
            // Arrange
            var archivo = new BEArchivo();
            archivo.Peso = 1024 * 1024; // Exactamente 1024 KB = 1 MB

            // Act
            archivo.VerificarPeso();

            // Assert
            Assert.AreEqual("1.00 MB", archivo.PesoArchivo);
        }

        [TestMethod]
        public void VerificarPeso_PesoLimiteMB_DevuelveMB()
        {
            // Arrange
            var archivo = new BEArchivo();
            archivo.Peso = 1024L * 1024L * 1024L; // Exactamente 1024 MB = 1 GB

            // Act
            archivo.VerificarPeso();

            // Assert
            Assert.AreEqual("1.00 GB", archivo.PesoArchivo);
        }

        [TestMethod]
        public void Constructor_Default_InicializaPropiedades()
        {
            // Act
            var archivo = new BEArchivo();

            // Assert
            Assert.AreEqual(0, archivo.Nro_Orden);
            Assert.IsNull(archivo.Nombre);
            Assert.IsNull(archivo.Extension);
            Assert.AreEqual(0, archivo.Peso);
            Assert.IsNull(archivo.PesoArchivo);
            Assert.IsNull(archivo.Hash);
            Assert.AreEqual("SI", archivo.Si);
        }

        [TestMethod]
        public void Si_Property_SiempreDevuelveSI()
        {
            // Arrange
            var archivo = new BEArchivo();

            // Act & Assert
            Assert.AreEqual("SI", archivo.Si);
        }
    }
}