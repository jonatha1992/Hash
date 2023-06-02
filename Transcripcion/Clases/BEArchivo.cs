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
    }


}
