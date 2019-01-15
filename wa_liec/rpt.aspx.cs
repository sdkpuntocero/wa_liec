using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace wa_liec
{
    public partial class rpt : System.Web.UI.Page
    {
        public static Guid guid_idmc;

        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
                if (!IsPostBack)
                {
                    defual_rpt();
                }
                else
                {
                }
            //}
            //catch
            //{
            //    Response.Redirect("acceso.aspx");
            //}
        }

        private void defual_rpt()
        {
            guid_idmc = (Guid)(Session["id_muestra"]);
            string str_nrpt = null;
            using (dd_liecEntities data_clte = new dd_liecEntities())
            {
                var i_clte = (from t_clte in data_clte.inf_mrp_concreto
                              where t_clte.id_mrp_concreto == guid_idmc
                              select t_clte).ToList();

                if (i_clte.Count == 0)
                {
                    
                }
                else
                {
                    str_nrpt = i_clte[0].no_muesra;


                }
            }

            #region ds00
            // Setup DataSet
            DataSet ds00 = new DataSet();

            SqlDataAdapter da00 = new SqlDataAdapter();
            SqlCommand cmd00 = new SqlCommand(@"SELECT * FROM [dd_liec].[dbo].[tbl_rpt_v01] ('" + guid_idmc + "')");
            cmd00.CommandType = CommandType.Text;
            cmd00.Connection = new SqlConnection(cn.cn_SQL);
            da00.SelectCommand = cmd00;

            da00.Fill(ds00, "ds00");


            // Create Report DataSource
            ReportDataSource rds00 = new ReportDataSource("ds00", ds00.Tables[0]);
            #endregion
 
            #region ds0
            #endregion
            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Server.MapPath("~/reportes/rpt_concreto.rdl"); ;
            viewer.LocalReport.EnableHyperlinks = true;
            viewer.LocalReport.DataSources.Add(rds00); // Add datasource here
            //viewer.LocalReport.DataSources.Add(rds1); // Add datasource here
            //viewer.LocalReport.DataSources.Add(rds3); // Add datasource 
            //viewer.LocalReport.DataSources.Add(rds7); // Add datasource 
            //viewer.LocalReport.DataSources.Add(rds14); // Add datasource 
            //viewer.LocalReport.DataSources.Add(rds28); // Add datasource 
            //ReportViewer1.LocalReport.DataSources.Clear();
            //ReportDataSource rds = new ReportDataSource("ds00", ds00.Tables[0]);
            //viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSources1", ds00.Tables[0]));
            
            //viewer.LocalReport.DataSources.Add(rds);
            //viewer.LocalReport.DataSources.Add(rds1);
            //viewer.LocalReport.DataSources.Add(rds3);
            //viewer.LocalReport.DataSources.Add(rds7);
            //viewer.LocalReport.DataSources.Add(rds14);
            //viewer.LocalReport.DataSources.Add(rds28);



            byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=" + "" + str_nrpt +"" + "." + extension);
            Response.BinaryWrite(bytes); // create the file
            Response.Flush(); // send it to the client to download
        }

        protected void lkbtn_guardar_emp_Click(object sender, EventArgs e)
        {
            CreatePDF("LIEC-1494");
        }

        private void CreatePDF(string fileName)
        {
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/reportes/rpt_concreto.rdl");

            System.Data.DataSet ds = new System.Data.DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand(@"SELECT
  v_rpt_conc.id_est_obra
  ,v_rpt_conc.clave_obra
  ,v_rpt_conc.desc_obra
  ,v_rpt_conc.coordinador
  ,v_rpt_conc.contacto_obra
  ,v_rpt_conc.no_muesra
  ,v_rpt_conc.fecha_colado
  ,v_rpt_conc.procedecia
  ,v_rpt_conc.elemento
  ,v_rpt_conc.dosificacion
  ,v_rpt_conc.resistencia
  ,v_rpt_conc.reva
  ,v_rpt_conc.tma
  ,v_rpt_conc.olla
  ,v_rpt_conc.remision
  ,v_rpt_conc.sali_planta
  ,v_rpt_conc.llega_obra
  ,v_rpt_conc.desca_ini
  ,v_rpt_conc.desca_term
  ,v_rpt_conc.vol
  ,v_rpt_conc.revb
  ,v_rpt_conc.localiza
  ,v_rpt_conc.clase
  ,v_rpt_conc.presion
  ,v_rpt_conc.razon_social
FROM
  v_rpt_conc
WHERE v_rpt_conc.no_muesra in ('1494')");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = new SqlConnection(cn.cn_SQL);
            da.SelectCommand = cmd;

            da.Fill(ds, "DataSet1");
            ReportDataSource datasource = new ReportDataSource("DataSet1", ds.Tables[0]);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            ReportViewer1.LocalReport.EnableHyperlinks = true;
            ReportViewer1.LocalReport.DisplayName = "LIEC-1494";
            //ReportViewer1.LocalReport.Refresh();

            //Code For Download Direct PDF

            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename= LIEC-1494.pdf");
            Response.BinaryWrite(bytes); // create the file
            Response.Flush();
        }
    }
}