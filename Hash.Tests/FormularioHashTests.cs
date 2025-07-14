using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hash;
using System.Collections.Generic;

namespace Hash.Tests
{
    [TestClass]
    public class FormularioHashTests
    {
        [TestMethod]
        public void Constructor_Default_InicializaListaArchivos()
        {
            // Act
            var formulario = new Formulario_Hash();

            // Assert
            Assert.IsNotNull(formulario.ListaArchivos);
            Assert.AreEqual(0, formulario.ListaArchivos.Count);
            Assert.AreEqual(0, formulario.PesoTotal);
            Assert.AreEqual("", formulario.pesototal);
        }

        [TestMethod]
        public void Contar_SinArchivos_TodosContadoresCero()
        {
            // Arrange
            var formulario = new Formulario_Hash();

            // Act
            formulario.Contar();

            // Assert
            Assert.AreEqual(0, formulario.Imagenes);
            Assert.AreEqual(0, formulario.Clips);
            Assert.AreEqual(0, formulario.Audio);
            Assert.AreEqual(0, formulario.Texto);
            Assert.AreEqual(0, formulario.Varios);
        }

        [TestMethod]
        public void Contar_ConImagenes_CuentaCorrectamente()
        {
            // Arrange
            var formulario = new Formulario_Hash();
            formulario.ListaArchivos.Add(new BEArchivo { Extension = ".jpg", Peso = 1024 });
            formulario.ListaArchivos.Add(new BEArchivo { Extension = ".png", Peso = 2048 });
            formulario.ListaArchivos.Add(new BEArchivo { Extension = ".jpeg", Peso = 1536 });
            formulario.ListaArchivos.Add(new BEArchivo { Extension = ".bmp", Peso = 512 });

            // Act
            formulario.Contar();

            // Assert
            Assert.AreEqual(4, formulario.Imagenes);
            Assert.AreEqual(0, formulario.Clips);
            Assert.AreEqual(0, formulario.Audio);
            Assert.AreEqual(0, formulario.Texto);
            Assert.AreEqual(0, formulario.Varios);
        }

        [TestMethod]
        public void Contar_ConClips_CuentaCorrectamente()
        {
            // Arrange
            var formulario = new Formulario_Hash();
            formulario.ListaArchivos.Add(new BEArchivo { Extension = ".mp4", Peso = 1024000 });
            formulario.ListaArchivos.Add(new BEArchivo { Extension = ".avi", Peso = 2048000 });
            formulario.ListaArchivos.Add(new BEArchivo { Extension = ".mov", Peso = 1536000 });

            // Act
            formulario.Contar();

            // Assert
            Assert.AreEqual(0, formulario.Imagenes);
            Assert.AreEqual(3, formulario.Clips);
            Assert.AreEqual(0, formulario.Audio);
            Assert.AreEqual(0, formulario.Texto);
            Assert.AreEqual(0, formulario.Varios);
        }

        [TestMethod]
        public void Contar_ConAudio_CuentaCorrectamente()
        {
            // Arrange
            var formulario = new Formulario_Hash();
            formulario.ListaArchivos.Add(new BEArchivo { Extension = ".mp3", Peso = 3072000 });
            formulario.ListaArchivos.Add(new BEArchivo { Extension = ".wav", Peso = 4096000 });
            formulario.ListaArchivos.Add(new BEArchivo { Extension = ".flac", Peso = 2048000 });

            // Act
            formulario.Contar();

            // Assert
            Assert.AreEqual(0, formulario.Imagenes);
            Assert.AreEqual(0, formulario.Clips);
            Assert.AreEqual(3, formulario.Audio);
            Assert.AreEqual(0, formulario.Texto);
            Assert.AreEqual(0, formulario.Varios);
        }

        [TestMethod]
        public void Contar_ConTexto_CuentaCorrectamente()
        {
            // Arrange
            var formulario = new Formulario_Hash();
            formulario.ListaArchivos.Add(new BEArchivo { Extension = ".pdf", Peso = 512000 });
            formulario.ListaArchivos.Add(new BEArchivo { Extension = ".txt", Peso = 1024 });
            formulario.ListaArchivos.Add(new BEArchivo { Extension = ".docx", Peso = 256000 });
            formulario.ListaArchivos.Add(new BEArchivo { Extension = ".doc", Peso = 128000 });

            // Act
            formulario.Contar();

            // Assert
            Assert.AreEqual(0, formulario.Imagenes);
            Assert.AreEqual(0, formulario.Clips);
            Assert.AreEqual(0, formulario.Audio);
            Assert.AreEqual(4, formulario.Texto);
            Assert.AreEqual(0, formulario.Varios);
        }

        [TestMethod]
        public void Contar_ConVarios_CuentaCorrectamente()
        {
            // Arrange
            var formulario = new Formulario_Hash();
            formulario.ListaArchivos.Add(new BEArchivo { Extension = ".zip", Peso = 1024000 });
            formulario.ListaArchivos.Add(new BEArchivo { Extension = ".exe", Peso = 2048000 });
            formulario.ListaArchivos.Add(new BEArchivo { Extension = ".dll", Peso = 256000 });

            // Act
            formulario.Contar();

            // Assert
            Assert.AreEqual(0, formulario.Imagenes);
            Assert.AreEqual(0, formulario.Clips);
            Assert.AreEqual(0, formulario.Audio);
            Assert.AreEqual(0, formulario.Texto);
            Assert.AreEqual(3, formulario.Varios);
        }

        [TestMethod]
        public void Contar_ConTiposMixtos_CuentaCorrectamente()
        {
            // Arrange
            var formulario = new Formulario_Hash();
            formulario.ListaArchivos.Add(new BEArchivo { Extension = ".jpg", Peso = 1024 });
            formulario.ListaArchivos.Add(new BEArchivo { Extension = ".mp4", Peso = 1024000 });
            formulario.ListaArchivos.Add(new BEArchivo { Extension = ".mp3", Peso = 2048000 });
            formulario.ListaArchivos.Add(new BEArchivo { Extension = ".pdf", Peso = 512000 });
            formulario.ListaArchivos.Add(new BEArchivo { Extension = ".zip", Peso = 256000 });

            // Act
            formulario.Contar();

            // Assert
            Assert.AreEqual(1, formulario.Imagenes);
            Assert.AreEqual(1, formulario.Clips);
            Assert.AreEqual(1, formulario.Audio);
            Assert.AreEqual(1, formulario.Texto);
            Assert.AreEqual(1, formulario.Varios);
        }

        [TestMethod]
        public void Contar_AsignaNumeroOrdenCorrectamente()
        {
            // Arrange
            var formulario = new Formulario_Hash();
            formulario.ListaArchivos.Add(new BEArchivo { Extension = ".jpg", Peso = 1024 });
            formulario.ListaArchivos.Add(new BEArchivo { Extension = ".mp4", Peso = 1024000 });
            formulario.ListaArchivos.Add(new BEArchivo { Extension = ".pdf", Peso = 512000 });

            // Act
            formulario.Contar();

            // Assert
            Assert.AreEqual(1, formulario.ListaArchivos[0].Nro_Orden);
            Assert.AreEqual(2, formulario.ListaArchivos[1].Nro_Orden);
            Assert.AreEqual(3, formulario.ListaArchivos[2].Nro_Orden);
        }

        [TestMethod]
        public void VerificarPeso_PesoTotalEnKB_CalculaCorrectamente()
        {
            // Arrange
            var formulario = new Formulario_Hash();
            formulario.ListaArchivos.Add(new BEArchivo { Peso = 512 });
            formulario.ListaArchivos.Add(new BEArchivo { Peso = 512 });

            // Act
            formulario.VerificarPeso();

            // Assert
            Assert.AreEqual("1.00 KB", formulario.pesototal);
            Assert.AreEqual(1.0, formulario.PesoTotal, 0.01);
        }

        [TestMethod]
        public void VerificarPeso_PesoTotalEnMB_CalculaCorrectamente()
        {
            // Arrange
            var formulario = new Formulario_Hash();
            formulario.ListaArchivos.Add(new BEArchivo { Peso = 1024 * 1024 }); // 1 MB

            // Act
            formulario.VerificarPeso();

            // Assert
            Assert.AreEqual("1.00 MB", formulario.pesototal);
            Assert.AreEqual(1.0, formulario.PesoTotal, 0.01);
        }

        [TestMethod]
        public void VerificarPeso_PesoTotalEnGB_CalculaCorrectamente()
        {
            // Arrange
            var formulario = new Formulario_Hash();
            formulario.ListaArchivos.Add(new BEArchivo { Peso = 1024L * 1024L * 1024L }); // 1 GB

            // Act
            formulario.VerificarPeso();

            // Assert
            Assert.AreEqual("1.00 GB", formulario.pesototal);
            Assert.AreEqual(1.0, formulario.PesoTotal, 0.01);
        }

        [TestMethod]
        public void VerificarPeso_SinArchivos_PesoCero()
        {
            // Arrange
            var formulario = new Formulario_Hash();

            // Act
            formulario.VerificarPeso();

            // Assert
            Assert.AreEqual("0.00 KB", formulario.pesototal);
            Assert.AreEqual(0.0, formulario.PesoTotal);
        }

        [TestMethod]
        public void Contar_ExtensionesEnMayusculas_NoSeContabilizan()
        {
            // Arrange - Las extensiones en el código están hardcodeadas en minúsculas
            var formulario = new Formulario_Hash();
            formulario.ListaArchivos.Add(new BEArchivo { Extension = ".JPG", Peso = 1024 });
            formulario.ListaArchivos.Add(new BEArchivo { Extension = ".MP4", Peso = 1024000 });

            // Act
            formulario.Contar();

            // Assert - Estos archivos deberían contarse como "Varios" ya que las extensiones están en mayúsculas
            Assert.AreEqual(0, formulario.Imagenes);
            Assert.AreEqual(0, formulario.Clips);
            Assert.AreEqual(2, formulario.Varios);
        }
    }
}