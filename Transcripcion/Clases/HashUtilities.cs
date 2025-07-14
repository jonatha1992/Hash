using System;
using System.IO;
using System.Security.Cryptography;

namespace Hash
{
    /// <summary>
    /// Utilidad para cálculos de hash que puede ser probada unitariamente
    /// </summary>
    public static class HashUtilities
    {
        /// <summary>
        /// Calcula el hash SHA256 de un archivo
        /// </summary>
        /// <param name="rutaArchivo">Ruta del archivo</param>
        /// <returns>Hash SHA256 en formato hexadecimal mayúsculo</returns>
        public static string CalcularHashSHA256(string rutaArchivo)
        {
            if (string.IsNullOrWhiteSpace(rutaArchivo))
                throw new ArgumentException("La ruta del archivo no puede estar vacía", nameof(rutaArchivo));

            if (!File.Exists(rutaArchivo))
                throw new FileNotFoundException("El archivo especificado no existe", rutaArchivo);

            using (var stream = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 1048576, FileOptions.SequentialScan))
            {
                using (var sha = SHA256.Create())
                {
                    byte[] buffer = new byte[1048576];
                    int bytesRead;
                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        sha.TransformBlock(buffer, 0, bytesRead, buffer, 0);
                    }
                    sha.TransformFinalBlock(buffer, 0, 0);
                    byte[] hashBytes = sha.Hash;
                    return BitConverter.ToString(hashBytes).Replace("-", "");
                }
            }
        }

        /// <summary>
        /// Valida si una cadena es un hash SHA256 válido
        /// </summary>
        /// <param name="hash">Hash a validar</param>
        /// <returns>True si es un hash SHA256 válido</returns>
        public static bool EsHashSHA256Valido(string hash)
        {
            if (string.IsNullOrWhiteSpace(hash))
                return false;

            if (hash.Length != 64)
                return false;

            foreach (char c in hash)
            {
                if (!((c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f')))
                    return false;
            }

            return true;
        }
    }
}