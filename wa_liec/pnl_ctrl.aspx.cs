using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wa_liec
{
    public partial class pnl_ctrl : System.Web.UI.Page
    {
        public static Guid guid_emp;
        public static Guid guid_idusr;
        static private int acc_rubro, acc_gasto, acc_caja, int_pnlID, int_idperf;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    inf_usr_oper();
                }
                else
                {
                }
            }
            catch
            {
                Response.Redirect("acceso.aspx");
            }
        }

        private void inf_usr_oper()
        {
            guid_idusr = (Guid)(Session["ss_idusr"]);

            using (dd_liecEntities m_usuario = new dd_liecEntities())
            {
                var i_usuario = (from i_u in m_usuario.inf_usuarios
                                 join i_tu in m_usuario.fact_perfil on i_u.id_perfil equals i_tu.id_perfil
                                 join i_c in m_usuario.inf_emp on i_u.id_emp equals i_c.id_emp

                                 where i_u.id_usuario == guid_idusr
                                 select new
                                 {
                                     i_u.nombres,
                                     i_u.a_paterno,
                                     i_u.a_materno,
                                     i_tu.desc_perfil,
                                     i_tu.id_perfil,
                                     i_c.razon_social,
                                     i_c.id_emp
                                 }).FirstOrDefault();

                lbl_usr_oper.Text = i_usuario.nombres + " " + i_usuario.a_paterno + " " + i_usuario.a_materno;
                lbl_tusr.Text = i_usuario.desc_perfil;
                int_idperf = i_usuario.id_perfil;
                lbl_emp_oper.Text = i_usuario.razon_social;
                guid_emp = i_usuario.id_emp;
            }
        }

        protected void lkb_salir_Click(object sender, EventArgs e)
        {
            Response.Redirect("acceso.aspx");
        }

        #region menu
        protected void lkb_arqu_Click(object sender, EventArgs e)
        {

        }

        protected void lkb_cali_Click(object sender, EventArgs e)
        {

        }

        protected void lkb_capt_Click(object sender, EventArgs e)
        {
            pnl_desa_tec.Visible = false;
            pnl_desa_tec.Focus();
            up_desa_tec.Update();

            pnl_cont.Visible = false;
            pnl_cont.Focus();
            up_cont.Update();

            pnl_recu_hum.Visible = false;
            pnl_recu_hum.Focus();
            up_recu_hum.Update();

            pnl_capt.Visible = true;
            pnl_capt.Focus();
            up_capt.Update();
        }

        protected void lkb_cobr_Click(object sender, EventArgs e)
        {

        }

        protected void lkb_cont_Click(object sender, EventArgs e)
        {
            pnl_recu_hum.Visible = false;
            pnl_recu_hum.Focus();
            up_recu_hum.Update();

            pnl_cont.Visible = true;
            pnl_cont.Focus();
            up_cont.Update();

            pnl_desa_tec.Visible = false;
            pnl_desa_tec.Focus();
            up_desa_tec.Update();

            pnl_capt.Visible = false;
            pnl_capt.Focus();
            up_capt.Update();
        }

        protected void lkb_coor_labcent_Click(object sender, EventArgs e)
        {

        }

        protected void lkb_desa_tec_Click(object sender, EventArgs e)
        {
            pnl_recu_hum.Visible = false;
            pnl_recu_hum.Focus();
            up_recu_hum.Update();

            pnl_cont.Visible = false;
            pnl_cont.Focus();
            up_cont.Update();

            pnl_desa_tec.Visible = true;
            pnl_desa_tec.Focus();
            up_desa_tec.Update();

            pnl_capt.Visible = false;
            pnl_capt.Focus();
            up_capt.Update();
        }

        protected void lkb_desa_neg_Click(object sender, EventArgs e)
        {

        }

        protected void lkb_estr_Click(object sender, EventArgs e)
        {

        }

        protected void lkb_gere_adm_Click(object sender, EventArgs e)
        {

        }

        protected void lkb_gere_cal_Click(object sender, EventArgs e)
        {

        }

        protected void lkb_gere_gen_Click(object sender, EventArgs e)
        {

        }

        protected void lkb_gere_tec_Click(object sender, EventArgs e)
        {

        }

        protected void lkb_mant_Click(object sender, EventArgs e)
        {

        }

        protected void lkb_meca_sue_Click(object sender, EventArgs e)
        {

        }

        protected void lkb_pers_obra_Click(object sender, EventArgs e)
        {

        }

        protected void lkb_recep_Click(object sender, EventArgs e)
        {

        }

        protected void lkb_recu_hum_Click(object sender, EventArgs e)
        {
            pnl_desa_tec.Visible = false;
            pnl_desa_tec.Focus();
            up_desa_tec.Update();

            pnl_cont.Visible = false;
            pnl_cont.Focus();
            up_cont.Update();

            pnl_recu_hum.Visible = true;
            pnl_recu_hum.Focus();
            up_recu_hum.Update();

            pnl_capt.Visible = false;
            pnl_capt.Focus();
            up_capt.Update();

        }

        protected void lkb_supe_Click(object sender, EventArgs e)
        {

        }


        #endregion

        protected void lkb_recu_hum_i_Click(object sender, EventArgs e)
        {
            Response.Redirect("pnl_rh.aspx");
        }

        protected void lkb_cont_i_Click(object sender, EventArgs e)
        {
            Response.Redirect("pnl_cont.aspx");
        }

        protected void lkb_desa_tec_i_Click(object sender, EventArgs e)
        {
            Response.Redirect("pnl_desa_tec.aspx");
        }

        protected void lkb_capt_i_Click(object sender, EventArgs e)
        {
            Response.Redirect("pnl_capt.aspx");
        }
    }
}