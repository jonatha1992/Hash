using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;

namespace Hash
{
    public partial class Form_Impresion : Form
    {
        Formulario_Hash Formulario_Hash;

        public Form_Impresion(Formulario_Hash formulario_Hash)
        {
            InitializeComponent();
            Formulario_Hash = formulario_Hash;
        }

        private void FormImpresion_Load(object sender, EventArgs e)
        {
            try
            {

                this.reportViewer1.LocalReport.DataSources.Clear();

                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Hash.ActaHash.rdlc";

                ReportDataSource reportDataSource1 = new ReportDataSource("DataSetElemento", Formulario_Hash.ListaArchivos);


                ReportParameterCollection Parametros = new ReportParameterCollection(){ new ReportParameter("NroActa",Formulario_Hash.Nro_Hash.ToString()),
                                                                                    new ReportParameter("NombreEntrega",Formulario_Hash.OfEntrega.ToString().ToUpper()),
                                                                                    new ReportParameter("ControlEntrega", Formulario_Hash.OfEntrega.Legajo.ToString()),
                                                                                    new ReportParameter("NombreRecibe",Formulario_Hash.OfRecibe.ToString().ToUpper()),
                                                                                    new ReportParameter("ControlRecibe", Formulario_Hash.OfRecibe.Legajo.ToString()),
                                                                                    new ReportParameter("Imagenes", Formulario_Hash.Imagenes.ToString()),
                                                                                    new ReportParameter("Clips",Formulario_Hash.Clips.ToString()),
                                                                                    new ReportParameter("Audio",Formulario_Hash.Audio.ToString()),
                                                                                    new ReportParameter("Tipo_procedimiento",Formulario_Hash.Tipo.ToUpper()),
                                                                                    new ReportParameter("Procedimiento",Formulario_Hash.Procedimiento.ToUpper())
                                                                                   };


                this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewer1.LocalReport.SetParameters(Parametros);
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }

    }
}
