using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace Hash
{
    public partial class Form_Impresion : Form
    {
        Formulario_Hash Formulario_Hash;
        Formulario_Custodia Formulario_Custodia;

        public Form_Impresion(Formulario_Hash formulario_Hash)
        {
            InitializeComponent();
            Formulario_Hash = formulario_Hash;

        }

        public Form_Impresion(Formulario_Custodia formulario_Custodia)
        {
            InitializeComponent();
            Formulario_Custodia = formulario_Custodia;
        }

        private void FormImpresion_Load(object sender, EventArgs e)
        { 
            try
            {

                this.reportViewer1.LocalReport.DataSources.Clear();

                if(Formulario_Custodia == null)// si es hash
                {
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
                                                                                    new ReportParameter("Procedimiento",Formulario_Hash.Procedimiento.ToUpper()),
                                                                                    new ReportParameter("PesoTotal",Formulario_Hash.pesototal.ToUpper())
                                                                                   };

                    if (Formulario_Hash.OfRecibe != null)
                    {
                        Parametros.Add(new ReportParameter("NombreRecibe", Formulario_Custodia.OfRecibe.ToString()));
                        Parametros.Add(new ReportParameter("ControlRecibe", "LUP" + Formulario_Custodia.OfRecibe.Legajo.ToString()));
                        Parametros.Add(new ReportParameter("DependenciaRecibe", Formulario_Custodia.OfRecibe.ToString()));
                    }

                    this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
                    this.reportViewer1.LocalReport.SetParameters(Parametros);
                    this.reportViewer1.RefreshReport();
                }
                else
                {

                    this.reportViewer1.LocalReport.ReportEmbeddedResource = "Hash.ActaCustodia.rdlc"; // si es cusotodia

                    // Crear el DataTable y las columnas
                    DataTable dataTable = new DataTable();
                    PropertyInfo[] propiedades = typeof(Formulario_Custodia).GetProperties();
                    foreach (PropertyInfo propiedad in propiedades)
                    {
                        dataTable.Columns.Add(propiedad.Name);
                    }

                    // Crear la fila con los valores de las propiedades del objeto
                    DataRow fila = dataTable.NewRow();
                    foreach (PropertyInfo propiedad in propiedades)
                    {
                        fila[propiedad.Name] = propiedad.GetValue(Formulario_Custodia, null);
                    }

                    // Agregar la fila al DataTable
                    dataTable.Rows.Add(fila);

                    ReportDataSource reportDataSource1 = new ReportDataSource("DataSetElementos", dataTable);



                    ReportParameterCollection Parametros = new ReportParameterCollection(){ new ReportParameter("NroActa",Formulario_Custodia.Nro_Hash.ToString()),
                                                                                    new ReportParameter("HoraEntrega",Formulario_Custodia.Hora.ToShortTimeString()),
                                                                                    new ReportParameter("FechaEntrega",Formulario_Custodia.Hora.ToShortDateString()),
                                                                                    new ReportParameter("Caratula", Formulario_Custodia.Cartula.ToString()),
                                                                                    new ReportParameter("Sumario",Formulario_Custodia.Sumario.ToString()),
                                                                                    new ReportParameter("Juzgado",Formulario_Custodia.Juzgado.ToString()),
                                                                                    new ReportParameter("Secretaria",Formulario_Custodia.Secretaria.ToString()),
                                                                                    new ReportParameter("Lugar",Formulario_Custodia.Lugar_Recoleccion.ToString()),
                                                                                    new ReportParameter("Utilidad",Formulario_Custodia.Utilidad.ToString()),
                                                                                    new ReportParameter("Identificacion",Formulario_Custodia.Identificacion.ToString()),
                                                                                    new ReportParameter("Descripcion",Formulario_Custodia.Descripcion.ToString()),
                                                                                    new ReportParameter("NombreEntrega",Formulario_Custodia.OfEntrega.ToString()),
                                                                                    new ReportParameter("ControlEntrega","LUP" + Formulario_Custodia.OfEntrega.Legajo.ToString()),
                                                                                    new ReportParameter("DependenciaEntrega",Formulario_Custodia.OfEntrega.Dependencia),
                                                                                   };
                    
                    if (Formulario_Custodia.OfRecibe != null)
                    {
                        Parametros.Add(new ReportParameter("NombreRecibe", Formulario_Custodia.OfRecibe.ToString()));
                        Parametros.Add(new ReportParameter("ControlRecibe", "LUP" + Formulario_Custodia.OfRecibe.Legajo.ToString()));
                        Parametros.Add(new ReportParameter("DependenciaRecibe", Formulario_Custodia.OfRecibe.ToString()));
                    }

                    this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
                    this.reportViewer1.LocalReport.SetParameters(Parametros);
                    this.reportViewer1.RefreshReport();
                }

            
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }

    }
}
