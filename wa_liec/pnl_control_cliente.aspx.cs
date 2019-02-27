using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wa_liec
{
    public partial class pnl_control_cliente : System.Web.UI.Page
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
                Response.Redirect("acceso_cliente.aspx");
            }
        }

        private void inf_usr_oper()
        {
            guid_idusr = (Guid)(Session["ss_idusr"]);

            using (dd_liecEntities m_usuario = new dd_liecEntities())
            {
                var i_usuario = (from i_u in m_usuario.inf_clte


                                 where i_u.id_clte == guid_idusr
                                 select new
                                 {
                                     i_u.razon_social,


                                 }).FirstOrDefault();

                lbl_usr_oper.Text = "Contacto de cliente";
                lbl_tusr.Text = "Cliente";

                lbl_emp_oper.Text = i_usuario.razon_social;

            }
        }

        #region clte_obras

        protected void btn_buscar_clte_obras_Click(object sender, EventArgs e)
        {
            string str_clte = txt_buscar_clte_obras.Text.ToUpper().Trim();

            if (str_clte == "TODO")
            {
                using (dd_liecEntities data_clte = new dd_liecEntities())
                {
                    var i_clte = (from t_clte in data_clte.inf_clte_obras
                                  where t_clte.id_clte == guid_idusr
                                  select new
                                  {
                                      t_clte.clave_obra,
                                      t_clte.desc_obra,
                                      t_clte.fecha_registro
                                  }).OrderBy(x => x.clave_obra).ToList();

                    gv_clte_obras.DataSource = i_clte;
                    gv_clte_obras.DataBind();
                    gv_clte_obras.Visible = true;
                }
            }
            else
            {
                try
                {
                    string n_rub;
                    Guid guid_usrid;
                    Char char_s = '|';
                    string d_rub = txt_buscar_clte_obras.Text.Trim();
                    String[] de_rub = d_rub.Trim().Split(char_s);

                    n_rub = de_rub[1].Trim();

                    using (dd_liecEntities edm_nclte = new dd_liecEntities())
                    {
                        var i_nclte = (from c in edm_nclte.inf_clte
                                       where c.cod_clte == n_rub
                                       select c).FirstOrDefault();

                        guid_usrid = i_nclte.id_clte;

                        var i_cltef = (from t_clte in edm_nclte.inf_clte_obras
                                       where t_clte.id_clte == guid_usrid
                                       select new
                                       {
                                           t_clte.clave_obra,
                                           t_clte.desc_obra,
                                           t_clte.fecha_registro
                                       }).OrderBy(x => x.clave_obra).ToList();

                        gv_clte_obras.DataSource = i_cltef;
                        gv_clte_obras.DataBind();
                        gv_clte_obras.Visible = true;

                        if (i_cltef.Count == 0)
                        {
                            gv_clte_obras.DataSource = i_cltef;
                            gv_clte_obras.DataBind();
                            gv_clte_obras.Visible = true;
                            gv_clte_obras.Visible = true;

                            Mensaje("Empresa no encontrado.");
                        }
                        else
                        {
                            gv_clte_obras.DataSource = i_cltef;
                            gv_clte_obras.DataBind();
                            gv_clte_obras.Visible = true;
                            gv_clte_obras.Visible = true;
                        }
                    }
                }
                catch
                {

                    //div_prospecto.Visible = false;
                    Mensaje("Usuario no encontrado.");
                }
            }
            //Guid guclte_IDf;
            //string str_clteobra;

            //limp_clte_obras_ctrl();
            //gv_clte_obras.Visible = false;
            //Char char_s = '|';
            //try
            //{
            //    int startIndex = txt_buscar_clte_obras.Text.Trim().IndexOf(char_s);
            //    int endIndex = txt_buscar_clte_obras.Text.Trim().Length;
            //    int length = endIndex - startIndex;
            //    str_clteobra = txt_buscar_clte_obras.Text.Substring(startIndex, length).Replace("|", "").Trim();

            //    using (bd_labEntities data_clte = new bd_labEntities())
            //    {
            //        var i_clte = (from t_clte in data_clte.inf_clte
            //                      where t_clte.cod_clte == str_clteobra
            //                      select t_clte).FirstOrDefault();

            //        guclte_IDf = i_clte.clte_ID;
            //    }

            //    using (bd_labEntities data_clte = new bd_labEntities())
            //    {
            //        var i_clte = (from t_clte in data_clte.inf_clte_obras
            //                      where t_clte.clte_ID == guclte_IDf
            //                      select new
            //                      {
            //                          t_clte.clave_obra,
            //                          t_clte.desc_obra,
            //                          t_clte.registro
            //                      }).ToList();

            //        gv_clte_obras.DataSource = i_clte;
            //        gv_clte_obras.DataBind();
            //        gv_clte_obras.Visible = true;
            //    }
            //}
            //catch
            //{
            //    gv_clte_obras.Visible = false;
            //}
        }



        private void limp_txt_clte_obras()
        {

        }


        private void Mensaje(string contenido)
        {
            lblModalTitle.Text = "LIEC";
            lblModalBody.Text = contenido;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();
        }

        protected void lkb_clte_obras_Click(object sender, EventArgs e)
        {
            pnl_clte_obras.Visible = true;
            up_clte_obras.Update();
        }

        protected void gv_clte_obras_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                //gvr.BackColor = Color.FromArgb(227, 76, 14);
                string num_m = gvr.Cells[0].Text.ToString().Trim();




                using (dd_liecEntities data_clte = new dd_liecEntities())
                {

                    var i_cltef = (from t_clte in data_clte.inf_clte_obras
                                   where t_clte.clave_obra == num_m
                                   select new
                                   {
                                       t_clte.clave_obra,
                                       t_clte.desc_obra,
                                       t_clte.fecha_registro
                                   }).OrderBy(x => x.clave_obra).ToList();

                    gv_clte_obras.DataSource = i_cltef;
                    gv_clte_obras.DataBind();
                    gv_clte_obras.Visible = true;

                    if (i_cltef.Count == 0)
                    {
                        gv_clte_obras.DataSource = i_cltef;
                        gv_clte_obras.DataBind();
                        gv_clte_obras.Visible = true;
                        gv_clte_obras.Visible = true;

                        Mensaje("Empresa no encontrado.");
                    }
                    else
                    {
                        gv_clte_obras.DataSource = i_cltef;
                        gv_clte_obras.DataBind();
                        gv_clte_obras.Visible = true;
                        gv_clte_obras.Visible = true;
                    }

                    var i_clte = (from t_clte in data_clte.inf_mrp_concreto
                                  where t_clte.no_muesra == num_m
                                  select new
                                  {
                                      t_clte.no_muesra,
                                      t_clte.fecha_registro
                                  }).ToList();

                    gv_rppc.DataSource = i_clte;
                    gv_rppc.DataBind();
                    gv_rppc.Visible = true;
                }
            }
            catch
            { }

        }

        protected void gv_clte_obras_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string str_clteobraID = e.Row.Cells[0].Text;
                int int_estatusID;

                using (dd_liecEntities data_clte = new dd_liecEntities())
                {
                    var i_clte = (from t_clte in data_clte.inf_clte_obras
                                  where t_clte.clave_obra == str_clteobraID
                                  select new
                                  {
                                      t_clte.id_est_obra,
                                  }).FirstOrDefault();

                    int_estatusID = int.Parse(i_clte.id_est_obra.ToString());
                }

                DropDownList DropDownList1 = (e.Row.FindControl("ddl_clteobra_estatus") as DropDownList);

                using (dd_liecEntities db_sepomex = new dd_liecEntities())
                {
                    var tbl_sepomex = (from c in db_sepomex.fact_est_obra
                                       select c).ToList();

                    DropDownList1.DataSource = tbl_sepomex;

                    DropDownList1.DataTextField = "desc_est_obra";
                    DropDownList1.DataValueField = "id_est_obra";
                    DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
                    DropDownList1.SelectedValue = int_estatusID.ToString();
                }
            }
        }

        protected void chk_clte_obras_CheckedChanged(object sender, EventArgs e)
        {
            string str_clte_obra;
            int int_clteobra;

            foreach (GridViewRow row in gv_clte_obras.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chk_clte_obras") as CheckBox);
                    if (chkRow.Checked)
                    {
                        row.BackColor = Color.FromArgb(227, 76, 14);
                        str_clte_obra = row.Cells[1].Text;

                        using (dd_liecEntities edm_clte = new dd_liecEntities())
                        {
                            var i_clte = (from c in edm_clte.inf_clte_obras
                                          where c.clave_obra == str_clte_obra
                                          select c).FirstOrDefault();

                            int_clteobra = i_clte.id_clte_obras;
                        }

                        using (dd_liecEntities edm_clte = new dd_liecEntities())
                        {
                            var i_clte = (from t_clte in edm_clte.inf_clte_obras
                                          where t_clte.id_clte_obras == int_clteobra
                                          select new
                                          {
                                              t_clte.clave_obra,
                                              t_clte.desc_obra,
                                              t_clte.coordinador,
                                              t_clte.contacto_obra,
                                              t_clte.id_tipo_servicio,
                                              t_clte.id_est_obra
                                          }).FirstOrDefault();

                        }
                    }
                    else
                    {
                        row.BackColor = Color.White;
                    }
                }
            }
        }

        protected void ddl_clteobra_estatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str_ddl;

            GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);
            DropDownList duty = (DropDownList)gvr.FindControl("ddl_clteobra_estatus");
            str_ddl = duty.SelectedItem.Value;

            if (str_ddl == "2")
            {

            }
            else
            {

            }
        }

        protected void chkb_desactivar_clte_obras_CheckedChanged(object sender, EventArgs e)
        {
            rfv_buscar_clte_obras.Enabled = false;

        }


        #endregion clte_obras
    }
}