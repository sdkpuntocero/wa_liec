using System;
using System.Linq;
using System.Web.UI;

namespace wa_liec
{
    public partial class acceso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btn_acceso_Click(object sender, EventArgs e)
        {
            string dominio, user, pass, cod_area;
            Guid guid_idusr;
            //dominio = "192.168.1.253";
            user = txt_usuario.Text;
            pass = txt_clave.Text;

            using (var m_usr = new dd_liecEntities())
            {
                var i_usr = (from c in m_usr.inf_usuarios
                             join f_a in m_usr.fact_areas on c.id_area equals f_a.id_area
                             where c.usr == user
                             select new
                             {
                                 c.id_usuario,
                                 f_a.cod_area
                             }).FirstOrDefault();
                cod_area = i_usr.cod_area;
                guid_idusr = i_usr.id_usuario;
            }

            switch (cod_area)
            {
                case "LIEC-AREA017":

                    Session["ss_idusr"] = guid_idusr;

                    Response.Redirect("panel_capt.aspx");
                    break;

                case "LIEC-AREA019":
                    Session["ss_idusr"] = guid_idusr;

                    Response.Redirect("pnl_ctrl.aspx");
                    break;

                case "LIEC-AREA001":
                    Session["ss_idusr"] = guid_idusr;

                    Response.Redirect("pnl_ctrl.aspx");
                    break;

                case "LIEC-AREA013":
                    Session["ss_idusr"] = guid_idusr;

                    Response.Redirect("pnl_cont.aspx");
                    break;

                default:
                    Console.WriteLine("Default case");
                    break;
            }
        }

        private void Mensaje(string contenido)
        {
            lblModalTitle.Text = "LIEC";
            lblModalBody.Text = contenido;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();
        }
    }
}