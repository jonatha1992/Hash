using System.IO;

namespace Hash
{
    public class BEArchivo
    {

        public int Nro_Orden { get; set; }
        public string Nombre { get; set; }
        public string Extension { get; set; }
        public  long Peso { get; set; }
        public string PesoArchivo { get; set; }
        public string Hash { get; set; }
        public string Si { get => "SI"; }
        public string TipoArchivo { get => ObtenerTipoArchivo(); }
        public BEArchivo() { }
        public BEArchivo(string rutaArchivo)
        {
            this.Extension = Path.GetExtension(rutaArchivo);
            this.Peso = new FileInfo(rutaArchivo).Length;
            this.Nombre = Path.GetFileName(rutaArchivo);

            VerificarPeso();

        }


        public void VerificarPeso()
        {
            double kilobytes = Peso / 1024.0;
            if (kilobytes <= 1024)
            {
                PesoArchivo = $"{kilobytes:F2} KB";
            }
            else
            {
                double megabytes = kilobytes / 1024.0;
                if (megabytes <= 1024)
                {
                    PesoArchivo = $"{megabytes:F2} MB";
                }
                else
                {
                    double gigabytes = megabytes / 1024.0;
                    PesoArchivo = $"{gigabytes:F2} GB";
                }
            }
        }

        private string ObtenerTipoArchivo()
        {
            string ext = Extension?.ToLower() ?? "";
            
            // Extensiones de imágenes
            string[] extensionesImagenes = { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tiff", ".tif", ".webp", ".svg", ".ico", ".raw", ".cr2", ".nef", ".arw" };
            if (extensionesImagenes.Contains(ext)) return "Imagen";

            // Extensiones de videos/clips
            string[] extensionesClips = { ".mp4", ".avi", ".mov", ".wmv", ".mkv", ".flv", ".webm", ".m4v", ".3gp", ".mpg", ".mpeg", ".mts", ".m2ts", ".vob", ".f4v" };
            if (extensionesClips.Contains(ext)) return "Video";

            // Extensiones de audio
            string[] extensionesAudio = { ".mp3", ".wav", ".flac", ".aac", ".ogg", ".wma", ".m4a", ".opus", ".aiff", ".au", ".ra", ".mka", ".ape", ".ac3" };
            if (extensionesAudio.Contains(ext)) return "Audio";

            // Extensiones de texto/documentos
            string[] extensionesTexto = { ".pdf", ".txt", ".docx", ".doc", ".rtf", ".odt", ".xls", ".xlsx", ".ppt", ".pptx", ".csv", ".xml", ".json", ".log", ".md", ".html", ".htm" };
            if (extensionesTexto.Contains(ext)) return "Documento";

            return "Varios";
        }
    }


}
