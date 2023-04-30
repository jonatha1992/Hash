using System;
using System.IO;
using System.Security.Cryptography;

namespace Hash
{
    public class BEArchivo
    {

        public int Nro_Orden { get; set; }
        public string Nombre { get; set; }
        public string Extension { get; set; }
        public long Peso { get; set; }
        public string Hash { get; set; }
        public string Si { get => "SI"; }
        public BEArchivo() { }
        public BEArchivo(string rutaArchivo)
        {
            this.Extension = Path.GetExtension(rutaArchivo);
            this.Peso = new FileInfo(rutaArchivo).Length;
            this.Nombre = Path.GetFileName(rutaArchivo);

            //using (var stream = new BufferedStream(File.OpenRead(rutaArchivo), 1200000))
            //{
            //    SHA1 sha = SHA1.Create();
            //    byte[] hashBytes = sha.ComputeHash(stream);
            //    this.Hash = BitConverter.ToString(hashBytes).Replace("-", "");
            //}
            //using (FileStream stream = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 16384, FileOptions.SequentialScan))
            //{

            //        SHA1 sha = SHA1.Create();
            //        byte[] hashBytes = sha.ComputeHash(stream);
            //        this.Hash = BitConverter.ToString(hashBytes).Replace("-", "");
            //}

            //using (var stream = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 1048576, FileOptions.SequentialScan))
            //{
            //    using (var sha = SHA256.Create())
            //    {
            //        byte[] buffer = new byte[1048576];
            //        int bytesRead;
            //        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
            //        {
            //            sha.TransformBlock(buffer, 0, bytesRead, buffer, 0);
            //        }
            //        sha.TransformFinalBlock(buffer, 0, 0);
            //        byte[] hashBytes = sha.Hash;
            //        this.Hash= BitConverter.ToString(hashBytes).Replace("-", "");
            //    }
            //}

        }
    }


}
