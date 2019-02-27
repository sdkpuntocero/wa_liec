using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wa_liec
{
    public partial class acceso_cliente : System.Web.UI.Page
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

            try
            {
                using (var m_usr = new dd_liecEntities())
                {
                    var i_usr = (from c in m_usr.inf_clte
           
                                 where c.cod_clte == user
                                 select new
                                 {
                                     c.id_clte,
                                     c.razon_social

                                 }).FirstOrDefault();
          
                    guid_idusr = i_usr.id_clte;

                    Session["ss_idusr"] = guid_idusr;

                    Response.Redirect("pnl_control_cliente.aspx");
                }


            }
            catch
            {
                Mensaje("Datos incorrectos, favor de reintetar");
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