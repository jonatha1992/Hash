//using BE;
using System.Windows.Forms;

namespace Hash
{
    public partial class Form_Impresion : Form
    {

        //    public Form_Impresion(BEPAdreHallazgo Bepadre)
        //    {
        //        InitializeComponent();
        //        if (Bepadre is BEHallazgo)
        //        {
        //            ePAdreHallazgo = (BEHallazgo)Bepadre;
        //        }
        //        else
        //        {
        //            ePAdreHallazgo = (BEEntrega)Bepadre;

        //        }
        //    }

        //    BEPAdreHallazgo ePAdreHallazgo;
        //    Acta acta;
        //    private void FormImpresion_Load(object sender, EventArgs e)
        //    {
        //        try
        //        {

        //            this.reportViewer1.LocalReport.DataSources.Clear();
        //            //this.reportViewer1.RefreshReport();

        //            if (ePAdreHallazgo is BEHallazgo)
        //            {
        //                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Presentacion_UI.ActaHallazgo.rdlc";
        //                acta = new Acta((BEHallazgo)ePAdreHallazgo);
        //            }
        //            else
        //            {
        //                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Presentacion_UI.ActaEntrega.rdlc";
        //                acta = new Acta((BEEntrega)ePAdreHallazgo);
        //            }


        //            ReportDataSource reportDataSource1 = new ReportDataSource("DataSetElemento", acta.elementos);
        //            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
        //            this.reportViewer1.LocalReport.SetParameters(acta.Parametros);
        //            this.reportViewer1.RefreshReport();
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show($"{ex.Message}");
        //        }
        //    }


    }
}
