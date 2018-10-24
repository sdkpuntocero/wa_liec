using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wa_liec
{
    public partial class pnl_capt : System.Web.UI.Page
    {
        public static int int_clte, int_clte_obras, int_pnlID, int_rppc, int_ensa_comp, int_idperf, int_dem = 0;
        public static string str_clte, str_usr_oper, nc = null;
        public static Guid guid_emp;
        public static Guid guid_idusr;
        
            
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

        [ScriptMethod()]
        [WebMethod]
        public static List<string> SearchCustomers(string prefixText, int count)
        {
            List<String> columnData = new List<String>();
            string str_fclte = prefixText.ToUpper();

            if (int_pnlID == 1)
            {
                using (SqlConnection connection = new SqlConnection(cn.cn_SQL))
                {
                    connection.Open();
                    string query = "SELECT razon_social FROM [dd_liec].[dbo].[inf_clte]  WHERE razon_social like '" + str_fclte + "%' ";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                columnData.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            else if (int_pnlID == 2)
            {
                using (SqlConnection connection = new SqlConnection(cn.cn_SQL))
                {
                    connection.Open();
                    string query = "SELECT razon_social,cod_clte FROM [dd_liec].[dbo].[inf_clte] WHERE razon_social like '" + str_fclte + "%' ";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                columnData.Add(reader.GetString(0) + " | " + reader.GetString(1).ToUpper());
                            }
                        }
                    }
                }
            }
            else if (int_pnlID == 3)
            {
                using (SqlConnection connection = new SqlConnection(cn.cn_SQL))
                {
                    connection.Open();
                    string query = @"SELECT inf_clte_obras.clave_obra, inf_clte.razon_social, inf_clte.cod_clte FROM inf_clte_obras INNER JOIN inf_clte ON inf_clte_obras.id_clte = inf_clte.id_clte WHERE inf_clte_obras.clave_obra like '" + str_fclte + "%' ";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                columnData.Add(reader.GetString(0) + " | " + reader.GetString(1).ToUpper() + " | " + reader.GetString(2).ToUpper());
                            }
                        }
                    }
                }
            }

            return columnData;
        }

        private void load_ddl()
        {
            ddl_colonia_clte.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
            ddl_colonia_clte.BackColor = Color.FromArgb(211, 211, 211);
            ddl_colonia_clte.ForeColor = Color.FromArgb(16, 77, 141);
        }

        #region menu

        protected void lkb_clte_Click(object sender, EventArgs e)
        {
            int_pnlID = 1;
            int_clte = 1;
            i_clte.Attributes["style"] = "color:#104D8d";
            lbl_clte.Attributes["style"] = "color:#104D8d";

            i_clte_obras.Attributes["style"] = "color:#E34C0E";
            lbl_clte_obras.Attributes["style"] = "color:#E34C0E";

            i_concreto.Attributes["style"] = "color:#E34C0E";
            lbl_concreto.Attributes["style"] = "color:#E34C0E";

            i_conc_ensaye.Attributes["style"] = "color:#E34C0E";
            lbl_conc_ensaye.Attributes["style"] = "color:#E34C0E";

            i_agregar_clte.Attributes["style"] = "color:white";
            i_editar_clte.Attributes["style"] = "color:white";

            limp_txt_clte();
            txt_buscar_clte.Text = null;
            gv_clte.Visible = false;

            pnl_clte.Visible = true;
            up_clte.Update();
            pnl_clte_obras.Visible = false;
            up_clte_obras.Update();

            pnl_rppc.Visible = false;
            up_rppc.Update();

            pnl_ensa_comp.Visible = false;
            up_ensa_comp.Update();
        }

        protected void lkb_clte_obras_Click(object sender, EventArgs e)
        {
            int_pnlID = 2;
            int_clte_obras = 0;
            i_clte.Attributes["style"] = "color:#E34C0E";
            lbl_clte.Attributes["style"] = "color:#E34C0E";

            i_clte_obras.Attributes["style"] = "color:#104D8d";
            lbl_clte_obras.Attributes["style"] = "color:#104D8d";

            i_concreto.Attributes["style"] = "color:#E34C0E";
            lbl_concreto.Attributes["style"] = "color:#E34C0E";

            i_conc_ensaye.Attributes["style"] = "color:#E34C0E";
            lbl_conc_ensaye.Attributes["style"] = "color:#E34C0E";

            i_agregar_clte_obras.Attributes["style"] = "color:white";
            i_editar_clte_obras.Attributes["style"] = "color:white";

            limp_txt_clte_obras();
            txt_buscar_clte_obras.Text = null;
            gv_clte_obras.Visible = false;
            rfv_buscar_clte_obras.Enabled = true;

            pnl_clte.Visible = false;
            up_clte.Update();

            pnl_clte_obras.Visible = true;
            up_clte_obras.Update();

            pnl_rppc.Visible = false;
            up_rppc.Update();

            pnl_ensa_comp.Visible = false;
            up_ensa_comp.Update();
        }

        protected void lkb_concreto_Click(object sender, EventArgs e)
        {
            int_pnlID = 3;
            int_rppc = 0;

            i_clte.Attributes["style"] = "color:#E34C0E";
            lbl_clte.Attributes["style"] = "color:#E34C0E";

            i_clte_obras.Attributes["style"] = "color:#E34C0E";
            lbl_clte_obras.Attributes["style"] = "color:#E34C0E";

            i_concreto.Attributes["style"] = "color:#104D8d";
            lbl_concreto.Attributes["style"] = "color:#104D8d";

            i_conc_ensaye.Attributes["style"] = "color:#E34C0E";
            lbl_conc_ensaye.Attributes["style"] = "color:#E34C0E";

            i_agregar_rppc.Attributes["style"] = "color:white";
            i_editar_rppc.Attributes["style"] = "color:white";

            limp_txt_rppc();
            gv_rppc.Visible = false;
            txt_buscar_rppc.Text = null;

            pnl_clte.Visible = false;
            up_clte.Update();
            pnl_clte_obras.Visible = false;
            up_clte_obras.Update();

            pnl_rppc.Visible = true;
            up_rppc.Update();

            pnl_ensa_comp.Visible = false;
            up_ensa_comp.Update();
        }

        protected void lkb_conc_ensaye_Click(object sender, EventArgs e)
        {
            int_pnlID = 4;
            int_ensa_comp = 0;

            i_clte.Attributes["style"] = "color:#E34C0E";
            lbl_clte.Attributes["style"] = "color:#E34C0E";

            i_clte_obras.Attributes["style"] = "color:#E34C0E";
            lbl_clte_obras.Attributes["style"] = "color:#E34C0E";

            i_concreto.Attributes["style"] = "color:#E34C0E";
            lbl_concreto.Attributes["style"] = "color:#E34C0E";

            i_conc_ensaye.Attributes["style"] = "color:#104D8d";
            lbl_conc_ensaye.Attributes["style"] = "color:#104D8d";

           
            i_editar_ensa_comp.Attributes["style"] = "color:white";

            limp_txt_ensa_comp();
            //gv_ensa_comp.Visible = false;
            txt_buscar_ensa_comp.Text = null;

            pnl_clte.Visible = false;
            up_clte.Update();

            pnl_clte_obras.Visible = false;
            up_clte_obras.Update();

            pnl_rppc.Visible = false;
            up_rppc.Update();

            pnl_ensa_comp.Visible = true;
            up_ensa_comp.Update();
        }

        private void limp_txt_ensa_comp()
        {
            txt_clavensa_a.Text = null;
            txt_masa_a.Text = null;
            txt_dire_a.Text = null;
            txt_inte_a.Text = null;
            txt_altu_aa.Text = null;
            txt_altu_ab.Text = null;
            txt_lad_aa.Text = null;
            txt_lad_ab.Text = null;
            txt_pres_a.Text = null;
            txt_tf_a.Text = null;
        }

        protected void lkb_salir_Click(object sender, EventArgs e)
        {
            Response.Redirect("acceso.aspx");
        }

        #endregion menu

        #region clte

        protected void btn_agregar_clte_Click(object sender, EventArgs e)
        {
            int_clte = 1;

            i_agregar_clte.Attributes["style"] = "color:#E34C0E";
            i_editar_clte.Attributes["style"] = "color:white";

            rfv_trfc_clte_fisc.Enabled = true;
            rfv_rfc_clte_fisc.Enabled = true;
            rfv_rs.Enabled = true;
            rfv_callenum_clte.Enabled = true;
            rfv_cp_clte.Enabled = true;

            div_busc_clt.Visible = false;

            gv_clte.Visible = false;

            limp_txt_clte();
        }

        protected void btn_editar_clte_Click(object sender, EventArgs e)
        {
            int_clte = 2;
            txt_buscar_clte.Text = null;
            i_agregar_clte.Attributes["style"] = "color:white";
            i_editar_clte.Attributes["style"] = "color:#E34C0E";

            rfv_trfc_clte_fisc.Enabled = false;
            rfv_rfc_clte_fisc.Enabled = false;
            rfv_rs.Enabled = false;
            rfv_callenum_clte.Enabled = false;
            rfv_cp_clte.Enabled = false;
            rfv_colonia_clte.Enabled = false;

            div_busc_clt.Visible = true;

            rfv_buscar_clte.Enabled = true;

            limp_txt_clte();
        }

        private void limp_txt_clte()
        {
            ddl_trfc_clte_fisc.Items.Clear();

            using (dd_liecEntities db_sepomex = new dd_liecEntities())
            {
                var tbl_sepomex = (from c in db_sepomex.fact_tipo_rfc
                                   select c).ToList();

                ddl_trfc_clte_fisc.DataSource = tbl_sepomex;
                ddl_trfc_clte_fisc.DataTextField = "desc_tipo_rfc";
                ddl_trfc_clte_fisc.DataValueField = "id_tipo_rfc";
                ddl_trfc_clte_fisc.DataBind();
            }
            ddl_trfc_clte_fisc.Items.Insert(0, new ListItem("SELECCIONAR", "0"));

            ddl_colonia_clte.Items.Clear();
            ddl_colonia_clte.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
            txt_rfc_clte_fisc.Text = null;
            txt_rs.Text = null;
            txt_telefono_clte.Text = null;
            txt_email_clte.Text = null;
            txt_callenum_clte.Text = null;
            txt_cp_clte.Text = null;
            txt_clte_coment.Text = null;
            txt_municipio_clte.Text = null;
            txt_estado_clte.Text = null;
        }

        private void guarda_clte()
        {
            try
            {
                Guid guid_clte = Guid.NewGuid();

                string str_cod_clte, str_nom_clte;
                int int_trfc = int.Parse(ddl_trfc_clte_fisc.SelectedValue);
                string str_rfc = txt_rfc_clte_fisc.Text.Trim().ToUpper();
                string str_razon_social = txt_rs.Text.ToUpper().Trim();
                string str_telefono = txt_telefono_clte.Text;
                string str_email = txt_email_clte.Text.Trim();
                string str_callenum = txt_callenum_clte.Text.ToUpper().Trim();
                string str_cp = txt_cp_clte.Text;
                string str_coment;
                int int_colony = Convert.ToInt32(ddl_colonia_clte.SelectedValue);

                if (int_clte == 2)
                {
                    int int_ddl, int_f_clte = 0;
                    int int_estatusID = 0;
                    string str_fclte = null;
                    foreach (GridViewRow row in gv_clte.Rows)
                    {
                        // int key = (int)GridView1.DataKeys[row.RowIndex].Value;
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[0].FindControl("chk_clte") as CheckBox);
                            if (chkRow.Checked)
                            {
                                int_f_clte = int_f_clte + 1;
                                str_fclte = row.Cells[1].Text;
                                str_nom_clte = row.Cells[2].Text;
                                DropDownList dl = (DropDownList)row.FindControl("ddl_clte_estatus");

                                int_ddl = int.Parse(dl.SelectedValue);
                                if (int_ddl == 1)
                                {
                                    int_estatusID = 1;
                                    break;
                                }
                                else if (int_ddl == 2)
                                {
                                    int_estatusID = 2;
                                    break;
                                }
                                else if (int_ddl == 3)
                                {
                                    int_estatusID = 3;
                                    break;
                                }
                                break;
                            }
                        }
                    }

                    if (int_estatusID == 0)
                    {
                        limp_txt_clte();
                        Mensaje("Favor de seleccionar un cliente.");
                    }
                    else
                    {
                        str_coment = txt_clte_coment.Text;

                        using (var m_clte = new dd_liecEntities())
                        {
                            var i_clte = (from c in m_clte.inf_clte
                                          where c.cod_clte == str_fclte
                                          select c).FirstOrDefault();

                            i_clte.id_tipo_rfc = int_trfc;
                            i_clte.rfc = str_rfc;
                            i_clte.id_est_clte = int_estatusID;
                            i_clte.razon_social = str_razon_social;
                            i_clte.telefono = str_telefono;
                            i_clte.email = str_email;
                            i_clte.callenum = str_callenum;
                            i_clte.d_codigo = str_cp;
                            i_clte.id_asenta_cpcons = int_colony;
                            i_clte.comentarios = str_coment;
                            i_clte.id_usuario = guid_idusr;

                            m_clte.SaveChanges();
                        }

                        rfv_rs.Enabled = false;
                        rfv_callenum_clte.Enabled = false;
                        rfv_cp_clte.Enabled = false;
                        rfv_colonia_clte.Enabled = false;
                        rfv_clte_coment.Enabled = false;
                        int_clte = 0;
                        limp_txt_clte();
                        gv_clte.Visible = false;
                        i_agregar_clte.Attributes["style"] = "color:white";
                        i_editar_clte.Attributes["style"] = "color:white";
                        Mensaje("Datos de cliente actualizados con éxito.");
                    }
                }
                else if (int_clte == 1)
                {
                    using (dd_liecEntities edm_nclte = new dd_liecEntities())
                    {
                        var i_nclte = (from c in edm_nclte.inf_clte
                                       where c.razon_social.Contains(str_razon_social)
                                       select c).ToList();

                        if (i_nclte.Count == 0)
                        {
                            using (dd_liecEntities edm_fclte = new dd_liecEntities())
                            {
                                var i_fclte = (from c in edm_fclte.inf_clte
                                               select c).ToList();

                                if (i_fclte.Count == 0)
                                {
                                    str_cod_clte = "LIEC-CLTE" + string.Format("{0:000}", (object)(i_nclte.Count + 1));

                                    using (var m_clte = new dd_liecEntities())
                                    {
                                        var i_clte = new inf_clte

                                        {
                                            id_clte = guid_clte,
                                            id_est_clte = 1,
                                            id_tipo_rfc = int_trfc,
                                            rfc = str_rfc,
                                            cod_clte = str_cod_clte,
                                            razon_social = str_razon_social,
                                            telefono = str_telefono,
                                            email = str_email,
                                            callenum = str_callenum,
                                            d_codigo = str_cp,
                                            id_asenta_cpcons = int_colony,
                                            fecha_registro = DateTime.Now,
                                            id_emp = guid_emp,
                                            id_usuario = guid_idusr
                                        };

                                        m_clte.inf_clte.Add(i_clte);
                                        m_clte.SaveChanges();
                                    }
                                    limp_txt_clte();
                                    Mensaje("Datos de cliente agregados con éxito.");
                                }
                                else
                                {
                                    str_cod_clte = "LIEC-CLTE" + string.Format("{0:000}", (object)(i_fclte.Count + 1));

                                    using (var m_clte = new dd_liecEntities())
                                    {
                                        var i_clte = new inf_clte

                                        {
                                            id_clte = guid_clte,
                                            id_est_clte = 1,
                                            id_tipo_rfc = int_trfc,
                                            rfc = str_rfc,
                                            cod_clte = str_cod_clte,
                                            razon_social = str_razon_social,
                                            telefono = str_telefono,
                                            email = str_email,
                                            callenum = str_callenum,
                                            d_codigo = str_cp,
                                            id_asenta_cpcons = int_colony,
                                            fecha_registro = DateTime.Now,
                                            id_emp = guid_emp,

                                            id_usuario = guid_idusr
                                        };

                                        m_clte.inf_clte.Add(i_clte);
                                        m_clte.SaveChanges();
                                    }
                                    limp_txt_clte();
                                    Mensaje("Datos de cliente agregados con éxito.");
                                }
                            }
                        }
                        else
                        {
                            limp_txt_clte();
                            rfv_rs.Enabled = false;
                            rfv_callenum_clte.Enabled = false;
                            rfv_cp_clte.Enabled = false;
                            rfv_colonia_clte.Enabled = false;
                            Mensaje("Cliente existe en la base de datos, favor de revisar.");
                        }
                    }
                }
            }
            catch
            { }
        }

        protected void btn_cp_clte_Click(object sender, EventArgs e)
        {
            string str_cp = txt_cp_clte.Text;

            datos_sepomex(str_cp);

            ddl_colonia_clte.Focus();
        }

        private void datos_sepomex(string str_codigo)
        {
            using (dd_liecEntities db_sepomex = new dd_liecEntities())
            {
                var tbl_sepomex = (from c in db_sepomex.inf_sepomex
                                   where c.d_codigo == str_codigo
                                   select c).ToList();

                ddl_colonia_clte.DataSource = tbl_sepomex;
                ddl_colonia_clte.DataTextField = "d_asenta";
                ddl_colonia_clte.DataValueField = "id_asenta_cpcons";
                ddl_colonia_clte.DataBind();

                if (tbl_sepomex.Count == 1)
                {
                    txt_municipio_clte.Text = tbl_sepomex[0].d_mnpio;
                    txt_estado_clte.Text = tbl_sepomex[0].d_estado;
                    rfv_colonia_clte.Enabled = true;
                }
                if (tbl_sepomex.Count > 1)
                {
                    ddl_colonia_clte.Items.Insert(0, new ListItem("SELECCIONAR", "0"));

                    txt_municipio_clte.Text = tbl_sepomex[0].d_mnpio;
                    txt_estado_clte.Text = tbl_sepomex[0].d_estado;
                    rfv_colonia_clte.Enabled = true;
                }
                else if (tbl_sepomex.Count == 0)
                {
                    ddl_colonia_clte.Items.Clear();
                    ddl_colonia_clte.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
                    txt_municipio_clte.Text = null;
                    txt_estado_clte.Text = null;
                    rfv_colonia_clte.Enabled = false;
                }
            }
            up_clte.Update();
        }

        protected void btn_guardar_clte_Click(object sender, EventArgs e)
        {
            if (int_clte == 0)
            {
                Mensaje("Favor de seleccionar una acción");
            }
            else
            {
                guarda_clte();
            }
        }

        protected void chkb_desactivar_clte_CheckedChanged(object sender, EventArgs e)
        {
            int_clte = 0;
            i_agregar_clte.Attributes["style"] = "color:white";
            i_editar_clte.Attributes["style"] = "color:white";

            rfv_buscar_clte.Enabled = false;

            rfv_trfc_clte_fisc.Enabled = false;
            rfv_rfc_clte_fisc.Enabled = false;
            rfv_rs.Enabled = false;
            rfv_callenum_clte.Enabled = false;
            rfv_cp_clte.Enabled = false;
            rfv_colonia_clte.Enabled = false;
            rfv_clte_coment.Enabled = false;
            limp_txt_clte();
            gv_clte.Visible = false;
        }

        protected void btn_buscar_clte_Click(object sender, EventArgs e)
        {
            string str_clte = txt_buscar_clte.Text.ToUpper().Trim();

            if (str_clte == "TODO")
            {
                using (dd_liecEntities data_clte = new dd_liecEntities())
                {
                    var i_clte = (from t_clte in data_clte.inf_clte
                                  select new
                                  {
                                      t_clte.cod_clte,
                                      t_clte.razon_social,
                                      t_clte.fecha_registro
                                  }).OrderBy(x => x.cod_clte).ToList();

                    gv_clte.DataSource = i_clte;
                    gv_clte.DataBind();
                    gv_clte.Visible = true;
                }
            }
            else
            {
                using (dd_liecEntities data_clte = new dd_liecEntities())
                {
                    var i_clte = (from t_clte in data_clte.inf_clte
                                  where t_clte.razon_social == str_clte
                                  select new
                                  {
                                      t_clte.cod_clte,
                                      t_clte.razon_social,
                                      t_clte.fecha_registro
                                  }).ToList();

                    gv_clte.DataSource = i_clte;
                    gv_clte.DataBind();
                    gv_clte.Visible = true;
                }
            }

            limp_txt_clte();
        }

        protected void chk_clte_CheckedChanged(object sender, EventArgs e)
        {
            Guid guid_clte;
            int int_estatusID = 0;

            foreach (GridViewRow row in gv_clte.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chk_clte") as CheckBox);
                    if (chkRow.Checked)
                    {
                        int_estatusID = int_estatusID + 1;
                        row.BackColor = Color.FromArgb(227, 76, 14);
                        str_clte = row.Cells[1].Text;

                        using (dd_liecEntities edm_clte = new dd_liecEntities())
                        {
                            var i_clte = (from c in edm_clte.inf_clte
                                          where c.cod_clte == str_clte
                                          select c).FirstOrDefault();

                            guid_clte = i_clte.id_clte;
                        }

                        using (dd_liecEntities edm_clte = new dd_liecEntities())
                        {
                            var i_clte = (from t_clte in edm_clte.inf_clte
                                          where t_clte.id_clte == guid_clte
                                          select new
                                          {
                                              t_clte.id_tipo_rfc,
                                              t_clte.rfc,
                                              t_clte.razon_social,
                                              t_clte.telefono,
                                              t_clte.email,
                                              t_clte.callenum,
                                              t_clte.d_codigo,
                                              t_clte.id_asenta_cpcons,
                                              t_clte.comentarios
                                          }).FirstOrDefault();

                            ddl_trfc_clte_fisc.SelectedValue = i_clte.id_tipo_rfc.ToString();
                            txt_rfc_clte_fisc.Text = i_clte.rfc;
                            txt_rs.Text = i_clte.razon_social;
                            txt_telefono_clte.Text = i_clte.telefono;
                            txt_email_clte.Text = i_clte.email;
                            txt_callenum_clte.Text = i_clte.callenum;
                            txt_cp_clte.Text = i_clte.d_codigo;
                            txt_clte_coment.Text = i_clte.comentarios;

                            try
                            {
                                int int_col = int.Parse(i_clte.id_asenta_cpcons.ToString());

                                using (dd_liecEntities db_sepomex = new dd_liecEntities())
                                {
                                    var tbl_sepomex = (from c in db_sepomex.inf_sepomex
                                                       where c.id_asenta_cpcons == int_col
                                                       where c.d_codigo == i_clte.d_codigo
                                                       select c).ToList();

                                    ddl_colonia_clte.DataSource = tbl_sepomex;
                                    ddl_colonia_clte.DataTextField = "d_asenta";
                                    ddl_colonia_clte.DataValueField = "id_asenta_cpcons";
                                    ddl_colonia_clte.DataBind();

                                    txt_municipio_clte.Text = tbl_sepomex[0].d_mnpio;
                                    txt_estado_clte.Text = tbl_sepomex[0].d_estado;
                                }
                            }
                            catch
                            { }
                            rfv_rs.Enabled = true;
                            rfv_callenum_clte.Enabled = true;
                            rfv_cp_clte.Enabled = true;
                        }
                    }
                    else
                    {
                        row.BackColor = Color.White;
                    }
                }
            }
            if (int_estatusID == 0)
            {
                limp_txt_clte();
            }
        }

        protected void gv_clte_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_clte.PageIndex = e.NewPageIndex;

            string str_clte = txt_buscar_clte.Text.ToUpper().Trim();

            if (str_clte == "TODO")
            {
                using (dd_liecEntities data_clte = new dd_liecEntities())
                {
                    var i_clte = (from t_clte in data_clte.inf_clte

                                  select new
                                  {
                                      t_clte.cod_clte,

                                      t_clte.razon_social,
                                      t_clte.fecha_registro
                                  }).OrderBy(x => x.cod_clte).ToList();

                    gv_clte.DataSource = i_clte;
                    gv_clte.DataBind();
                    gv_clte.Visible = true;
                }
            }
            else
            {
                using (dd_liecEntities data_clte = new dd_liecEntities())
                {
                    var i_clte = (from t_clte in data_clte.inf_clte

                                  where str_clte.Contains(t_clte.razon_social)
                                  select new
                                  {
                                      t_clte.cod_clte,

                                      t_clte.razon_social,
                                      t_clte.fecha_registro
                                  }).ToList();

                    gv_clte.DataSource = i_clte;
                    gv_clte.DataBind();
                    gv_clte.Visible = true;
                }
            }
        }

        protected void ddl_clte_estatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str_ddl;

            GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);
            DropDownList duty = (DropDownList)gvr.FindControl("ddl_clte_estatus");
            str_ddl = duty.SelectedItem.Value;

            if (str_ddl == "2" || str_ddl == "3")
            {
                txt_clte_coment.Enabled = true;
                rfv_clte_coment.Enabled = true;
            }
            else
            {
                txt_clte_coment.Enabled = false;
                rfv_clte_coment.Enabled = false;
            }
        }

        protected void gv_clte_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string str_clteID = e.Row.Cells[1].Text;
                int int_estatusID;

                using (dd_liecEntities data_clte = new dd_liecEntities())
                {
                    var i_clte = (from t_clte in data_clte.inf_clte
                                  where t_clte.cod_clte == str_clteID
                                  select new
                                  {
                                      t_clte.id_est_clte,
                                  }).FirstOrDefault();

                    int_estatusID = int.Parse(i_clte.id_est_clte.ToString());
                }

                DropDownList DropDownList1 = (e.Row.FindControl("ddl_clte_estatus") as DropDownList);

                using (dd_liecEntities db_sepomex = new dd_liecEntities())
                {
                    var tbl_sepomex = (from c in db_sepomex.fact_est_clte
                                       select c).ToList();

                    DropDownList1.DataSource = tbl_sepomex;

                    DropDownList1.DataTextField = "desc_est_clte";
                    DropDownList1.DataValueField = "id_est_clte";
                    DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
                    DropDownList1.SelectedValue = int_estatusID.ToString();
                }
            }
        }

        #endregion clte

        #region funciones

        private string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, @"[^0-9A-Za-z]", "", RegexOptions.None);
        }

        public static string RemoveAccentsWithRegEx(string inputString)
        {
            Regex replace_a_Accents = new Regex("[á|à|ä|â]", RegexOptions.Compiled);
            Regex replace_e_Accents = new Regex("[é|è|ë|ê]", RegexOptions.Compiled);
            Regex replace_i_Accents = new Regex("[í|ì|ï|î]", RegexOptions.Compiled);
            Regex replace_o_Accents = new Regex("[ó|ò|ö|ô]", RegexOptions.Compiled);
            Regex replace_u_Accents = new Regex("[ú|ù|ü|û]", RegexOptions.Compiled);
            inputString = replace_a_Accents.Replace(inputString, "a");
            inputString = replace_e_Accents.Replace(inputString, "e");
            inputString = replace_i_Accents.Replace(inputString, "i");
            inputString = replace_o_Accents.Replace(inputString, "o");
            inputString = replace_u_Accents.Replace(inputString, "u");
            return inputString;
        }

        private void genera_usuario()
        {
            //try
            //{
            //    string str_nombres = RemoveSpecialCharacters(RemoveAccentsWithRegEx(txt_nombres_admin.Text.ToLower()));
            //    string[] separados;

            //    separados = str_nombres.Split(" ".ToCharArray());

            //    string str_apaterno = RemoveSpecialCharacters(RemoveAccentsWithRegEx(txt_apaterno_admin.Text.ToLower()));
            //    string str_amaterno = RemoveSpecialCharacters(RemoveAccentsWithRegEx(txt_amaterno_admin.Text.ToLower()));

            //    string codigo_usuario = str_nombres + "_" + left_right_mid.left(str_apaterno, 2) + left_right_mid.left(str_amaterno, 2);
            //    txt_usuario_admin.Text = codigo_usuario;
            //}
            //catch
            //{
            //    Mensaje("Se requiere minimo 2 letras por cada campo(nombre,apellido paterno, apellido materno) para generar el usuario.");
            //}
        }

        private void Mensaje(string contenido)
        {
            lblModalTitle.Text = "LIEC";
            lblModalBody.Text = contenido;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();
        }

        #endregion funciones

        #region clte_obras

        protected void btn_buscar_clte_obras_Click(object sender, EventArgs e)
        {
            string str_clte = txt_buscar_clte.Text.ToUpper().Trim();

            using (dd_liecEntities data_clte = new dd_liecEntities())
            {
                var i_clte = (from t_clte in data_clte.inf_clte

                              where t_clte.razon_social == str_clte
                              select new
                              {
                                  t_clte.cod_clte,

                                  t_clte.razon_social,
                                  t_clte.fecha_registro
                              }).ToList();

                gv_clte.DataSource = i_clte;
                gv_clte.DataBind();
                gv_clte.Visible = true;
            }
        }

        protected void btn_agregar_clte_obras_Click(object sender, EventArgs e)
        {
            int_clte_obras = 1;

            i_agregar_clte_obras.Attributes["style"] = "color:#E34C0E";
            i_editar_clte_obras.Attributes["style"] = "color:white";

            rfv_clte_clave_obra.Enabled = true;
            rfv_clte_desc_obra.Enabled = true;
            rfv_clte_tservicio.Enabled = true;
            rfv_clte_coordinador.Enabled = true;
            rfv_clte_contobra.Enabled = true;

            limp_txt_clte_obras();
        }

        private void limp_txt_clte_obras()
        {
            ddl_clte_tservicio.Items.Clear();
            using (dd_liecEntities db_sepomex = new dd_liecEntities())
            {
                var tbl_sepomex = (from c in db_sepomex.fact_tipo_servicio

                                   select c).ToList();

                ddl_clte_tservicio.DataSource = tbl_sepomex;
                ddl_clte_tservicio.DataTextField = "desc_tipo_servicio";
                ddl_clte_tservicio.DataValueField = "id_tipo_servicio";
                ddl_clte_tservicio.DataBind();
            }
            ddl_clte_tservicio.Items.Insert(0, new ListItem("SELECCIONAR", "0"));

            txt_clte_clave_obra.Text = null;
            txt_clte_desc_obra.Text = null;
            txt_clte_coordinador.Text = null;
            txt_clte_contobra.Text = null;
        }

        protected void btn_guardar_clte_obras_Click(object sender, EventArgs e)
        {
            if (int_clte_obras == 0)
            {
                Mensaje("Favor de seleccionar una acción");
            }
            else
            {
                try
                {
                    string str_clteobra, str_claveobra, str_descobra, str_coordinador, str_contobra, str_coment;
                    int int_tservicio;
                    Guid guid_fclte;
                    Char char_s = '|';
                    int startIndex = txt_buscar_clte_obras.Text.Trim().IndexOf(char_s);
                    int endIndex = txt_buscar_clte_obras.Text.Trim().Length;
                    int length = endIndex - startIndex;
                    str_clteobra = txt_buscar_clte_obras.Text.Substring(startIndex, length).Replace("|", "").Trim();
                    str_claveobra = txt_clte_clave_obra.Text.Trim().ToUpper();
                    str_descobra = txt_clte_desc_obra.Text.Trim().ToUpper();
                    int_tservicio = int.Parse(ddl_clte_tservicio.SelectedValue);
                    str_coordinador = txt_clte_coordinador.Text.Trim().ToUpper();
                    str_contobra = txt_clte_contobra.Text.Trim().ToUpper();
                    str_coment = txt_coment_obras.Text.Trim().ToUpper();

                    if (int_clte_obras == 2)
                    {
                        int int_ddl;
                        int int_estatusID = 0;
                        foreach (GridViewRow row in gv_clte_obras.Rows)
                        {
                            // int key = (int)GridView1.DataKeys[row.RowIndex].Value;
                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                DropDownList dl = (DropDownList)row.FindControl("ddl_clteobra_estatus");

                                int_ddl = int.Parse(dl.SelectedValue);
                                if (int_ddl == 1)
                                {
                                    int_estatusID = 1;
                                }
                                else if (int_ddl == 2)
                                {
                                    int_estatusID = 2;
                                }
                            }
                        }

                        if (int_estatusID == 0)
                        {
                            Mensaje("Favor de seleccionar una obra.");
                        }
                        else
                        {
                            str_coment = txt_clte_coment.Text;

                            using (var m_clte = new dd_liecEntities())
                            {
                                var i_clte = (from c in m_clte.inf_clte_obras
                                              where c.clave_obra == str_claveobra
                                              select c).FirstOrDefault();

                                i_clte.id_est_obra = int_estatusID;
                                i_clte.desc_obra = str_descobra;
                                i_clte.coordinador = str_coordinador;
                                i_clte.contacto_obra = str_contobra;
                                i_clte.id_tipo_servicio = int_tservicio;
                                i_clte.comentarios = str_coment;

                                m_clte.SaveChanges();
                            }

                            rfv_clte_clave_obra.Enabled = false;
                            rfv_clte_desc_obra.Enabled = false;
                            rfv_clte_tservicio.Enabled = false;
                            rfv_clte_coordinador.Enabled = false;
                            rfv_clte_contobra.Enabled = false;
                            limp_txt_clte_obras();
                            gv_clte_obras.Visible = false;
                            Mensaje("Datos de cliente-obra actualizados con éxito.");
                        }
                    }
                    else if (int_clte_obras == 1)
                    {
                        using (dd_liecEntities edm_nclte = new dd_liecEntities())
                        {
                            var i_nclte = (from c in edm_nclte.inf_clte
                                           where c.cod_clte == str_clteobra
                                           select c).FirstOrDefault();

                            guid_fclte = i_nclte.id_clte;
                        }

                        using (dd_liecEntities edm_nclte = new dd_liecEntities())
                        {
                            var i_nclte = (from c in edm_nclte.inf_clte_obras
                                           where c.clave_obra == str_claveobra
                                           select c).ToList();

                            if (i_nclte.Count == 0)
                            {
                                using (var m_clte = new dd_liecEntities())
                                {
                                    var i_clte = new inf_clte_obras

                                    {
                                        id_est_obra = 1,
                                        clave_obra = str_claveobra,
                                        desc_obra = str_descobra,
                                        coordinador = str_coordinador,
                                        contacto_obra = str_contobra,
                                        id_tipo_servicio = int_tservicio,
                                        fecha_registro = DateTime.Now,
                                        id_clte = guid_fclte
                                    };

                                    m_clte.inf_clte_obras.Add(i_clte);
                                    m_clte.SaveChanges();
                                }

                                limp_txt_clte_obras();

                                rfv_clte_clave_obra.Enabled = false;
                                rfv_clte_desc_obra.Enabled = false;
                                rfv_clte_tservicio.Enabled = false;
                                rfv_clte_coordinador.Enabled = false;
                                rfv_clte_contobra.Enabled = false;
                                limp_txt_clte_obras();
                                gv_clte_obras.Visible = false;
                                Mensaje("Datos de cliente-obra agregados con éxito.");
                            }
                            else
                            {
                                limp_txt_clte();
                                rfv_rs.Enabled = false;
                                rfv_callenum_clte.Enabled = false;
                                rfv_cp_clte.Enabled = false;
                                rfv_colonia_clte.Enabled = false;
                                limp_txt_clte_obras();
                                gv_clte_obras.Visible = false;
                                Mensaje("Obra existe en la base de datos, favor de revisar.");
                            }
                        }
                    }
                }
                catch
                {
                    Mensaje("Sin registro, favor de revisar.");
                    limp_txt_clte_obras();
                    gv_clte.Visible = false;
                }
            }
        }

        protected void gv_clte_obras_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string str_clteobraID = e.Row.Cells[1].Text;
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

                            txt_clte_clave_obra.Text = i_clte.clave_obra;
                            txt_clte_desc_obra.Text = i_clte.desc_obra;
                            txt_clte_coordinador.Text = i_clte.coordinador;
                            txt_clte_contobra.Text = i_clte.contacto_obra;
                            ddl_clte_tservicio.SelectedValue = i_clte.id_tipo_servicio.ToString();
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
                txt_coment_obras.Enabled = true;
                rfv_coment_obras.Enabled = true;
            }
            else
            {
                txt_coment_obras.Enabled = false;
                rfv_coment_obras.Enabled = false;
            }
        }

        protected void chkb_desactivar_clte_obras_CheckedChanged(object sender, EventArgs e)
        {
            rfv_buscar_clte_obras.Enabled = false;
            rfv_clte_clave_obra.Enabled = false;
            rfv_clte_desc_obra.Enabled = false;
            rfv_clte_tservicio.Enabled = false;
            rfv_clte_coordinador.Enabled = false;
            rfv_clte_contobra.Enabled = false;
        }

        protected void btn_editar_clte_obras_Click(object sender, EventArgs e)
        {
            int_clte_obras = 2;

            i_agregar_clte_obras.Attributes["style"] = "color:white";
            i_editar_clte_obras.Attributes["style"] = "color:#E34C0E";

            Guid guid_cltef;
            string str_clteobra;

            limp_txt_clte_obras();
            gv_clte_obras.Visible = false;
            Char char_s = '|';
            try
            {
                int startIndex = txt_buscar_clte_obras.Text.Trim().IndexOf(char_s);
                int endIndex = txt_buscar_clte_obras.Text.Trim().Length;
                int length = endIndex - startIndex;
                str_clteobra = txt_buscar_clte_obras.Text.Substring(startIndex, length).Replace("|", "").Trim();

                using (dd_liecEntities data_clte = new dd_liecEntities())
                {
                    var i_clte = (from t_clte in data_clte.inf_clte
                                  where t_clte.cod_clte == str_clteobra
                                  select t_clte).FirstOrDefault();

                    guid_cltef = i_clte.id_clte;
                }

                using (dd_liecEntities data_clte = new dd_liecEntities())
                {
                    var i_clte = (from t_clte in data_clte.inf_clte_obras
                                  where t_clte.id_clte == guid_cltef
                                  select new
                                  {
                                      t_clte.clave_obra,
                                      t_clte.desc_obra,
                                      t_clte.fecha_registro
                                  }).ToList();

                    gv_clte_obras.DataSource = i_clte;
                    gv_clte_obras.DataBind();
                    gv_clte_obras.Visible = true;
                }
            }
            catch
            {
                gv_clte_obras.Visible = false;
            }
        }

        #endregion clte_obras

        #region rppc

        protected void btn_agregar_rppc_Click(object sender, EventArgs e)
        {
            int_rppc = 1;
            div_rppc.Visible = true;
            div_rpc.Visible = false;
            i_agregar_rppc.Attributes["style"] = "color:#E34C0E";
            i_editar_rppc.Attributes["style"] = "color:white";

            rfv_buscar_rppc.Enabled = true;
            rfv_nmue_rppc.Enabled = true;
            rfv_fcol_rppc.Enabled = true;
            rfv_frec_rppc.Enabled = true;
            rfv_entrega_rppc.Enabled = true;
            rfv_recibe_rppc.Enabled = true;
            rfv_r_rppc.Enabled = true;
            rfv_tesp_rppc.Enabled = true;
            rfv_tconc_rppc.Enabled = true;
            rfv_sit_rppc.Enabled = true;
            gv_rppc.Visible = false;
            limp_txt_rppc();
        }

        protected void chkb_1_rppc_CheckedChanged(object sender, EventArgs e)
        {
            DateTime dt_fcol;

            if (chkb_1_rppc.Checked)
            {
                if (string.IsNullOrEmpty(fcol_rppc.Text))
                {
                    Mensaje("Favor de seleccionar una fecha de colado");
                    chkb_1_rppc.Checked = false;
                }
                else
                {
                    rfv_f1_rppc.Enabled = true;
                    rvf1_rppc.Enabled = true;
                    dt_fcol = DateTime.Parse(fcol_rppc.Text);
                    txt_f1_rppc.Text = dt_fcol.AddDays(1).ToString("yyyy-MM-dd");
                    up_rppc.Update();
                }
            }
            else
            {
                rfv_f1_rppc.Enabled = false;
                rvf1_rppc.Enabled = false;
                chkb_1_rppc.Checked = false;
                txt_f1_rppc.Text = null;
            }
        }

        protected void chkb_3_rppc_CheckedChanged(object sender, EventArgs e)
        {
            DateTime dt_fcol;

            if (chkb_3_rppc.Checked)
            {
                if (string.IsNullOrEmpty(fcol_rppc.Text))
                {
                    Mensaje("Favor de seleccionar una fecha de colado");
                    chkb_3_rppc.Checked = false;
                }
                else
                {
                    rfv_f3_rppc.Enabled = true;
                    rvf3_rppc.Enabled = true;
                    dt_fcol = DateTime.Parse(fcol_rppc.Text);
                    txt_f3_rppc.Text = dt_fcol.AddDays(3).ToString("yyyy-MM-dd");
                }
            }
            else
            {
                rfv_f3_rppc.Enabled = false;
                rvf3_rppc.Enabled = false;
                chkb_3_rppc.Checked = false;
                txt_f3_rppc.Text = null;
            }
        }

        protected void chkb_7_rppc_CheckedChanged(object sender, EventArgs e)
        {
            DateTime dt_fcol;

            if (chkb_7_rppc.Checked)
            {
                if (string.IsNullOrEmpty(fcol_rppc.Text))
                {
                    Mensaje("Favor de seleccionar una fecha de colado");
                    chkb_7_rppc.Checked = false;
                }
                else
                {
                    rfv_f7_rppc.Enabled = true;
                    rvf7_rppc.Enabled = true;
                    dt_fcol = DateTime.Parse(fcol_rppc.Text);
                    txt_f7_rppc.Text = dt_fcol.AddDays(7).ToString("yyyy-MM-dd");
                }
            }
            else
            {
                rfv_f7_rppc.Enabled = false;
                rvf7_rppc.Enabled = false;
                chkb_7_rppc.Checked = false;
                txt_f7_rppc.Text = null;
            }
        }

        protected void chkb_14_rppc_CheckedChanged(object sender, EventArgs e)
        {
            DateTime dt_fcol;

            if (chkb_14_rppc.Checked)
            {
                if (string.IsNullOrEmpty(fcol_rppc.Text))
                {
                    Mensaje("Favor de seleccionar una fecha de colado");
                    chkb_14_rppc.Checked = false;
                }
                else
                {
                    rfv_f14_rppc.Enabled = true;
                    rvf14_rppc.Enabled = true;
                    dt_fcol = DateTime.Parse(fcol_rppc.Text);
                    txt_f14_rppc.Text = dt_fcol.AddDays(14).ToString("yyyy-MM-dd");
                }
            }
            else
            {
                rfv_f14_rppc.Enabled = false;
                rvf14_rppc.Enabled = false;
                chkb_14_rppc.Checked = false;
                txt_f14_rppc.Text = null;
            }
        }

        protected void chkb_28_rppc_CheckedChanged(object sender, EventArgs e)
        {
            DateTime dt_fcol;

            if (chkb_28_rppc.Checked)
            {
                if (string.IsNullOrEmpty(fcol_rppc.Text))
                {
                    Mensaje("Favor de seleccionar una fecha de colado");
                    chkb_28_rppc.Checked = false;
                }
                else
                {
                    rfv_f28_rppc.Enabled = true;
                    rvf28_rppc.Enabled = true;
                    dt_fcol = DateTime.Parse(fcol_rppc.Text);
                    txt_f28_rppc.Text = dt_fcol.AddDays(28).ToString("yyyy-MM-dd");
                }
            }
            else
            {
                rfv_f28_rppc.Enabled = false;
                rvf28_rppc.Enabled = false;
                chkb_28_rppc.Checked = false;
                txt_f28_rppc.Text = null;
            }
        }

        protected void chkb_otro_rppc_CheckedChanged(object sender, EventArgs e)
        {
            DateTime dt_fcol;

            if (chkb_otro_rppc.Checked)
            {
                if (string.IsNullOrEmpty(fcol_rppc.Text))
                {
                    Mensaje("Favor de seleccionar una fecha de colado");
                    chkb_otro_rppc.Checked = false;
                }
                else
                {
                    if (string.IsNullOrEmpty(txt_cantotro_rppc.Text))
                    {
                    }
                    else
                    {
                        rfv_ftro_rppc.Enabled = true;
                        rvfotro_rppc.Enabled = true;
                        dt_fcol = DateTime.Parse(fcol_rppc.Text);
                        int int_fotro_rppc = int.Parse(txt_cantotro_rppc.Text);
                        txt_fotro_rppc.Text = dt_fcol.AddDays(int_fotro_rppc).ToString("yyyy-MM-dd");
                    }
                }
            }
            else
            {
                rfv_ftro_rppc.Enabled = false;
                rvfotro_rppc.Enabled = false;
                chkb_otro_rppc.Checked = false;
                txt_cantotro_rppc.Text = null;
                txt_fotro_rppc.Text = null;
            }
        }

        protected void gv_rppc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string str_clteobraID = e.Row.Cells[1].Text;
                int int_estatusID;

                using (dd_liecEntities data_clte = new dd_liecEntities())
                {
                    var i_clte = (from t_clte in data_clte.inf_mrp_concreto
                                  where t_clte.no_muesra == str_clteobraID
                                  select new
                                  {
                                      t_clte.id_est_concreto,
                                  }).FirstOrDefault();

                    int_estatusID = int.Parse(i_clte.id_est_concreto.ToString());
                }

                DropDownList DropDownList1 = (e.Row.FindControl("ddl_rppc_est") as DropDownList);

                using (dd_liecEntities db_sepomex = new dd_liecEntities())
                {
                    var tbl_sepomex = (from c in db_sepomex.fact_est_concreto
                                       select c).ToList();

                    DropDownList1.DataSource = tbl_sepomex;

                    DropDownList1.DataTextField = "desc_est_concreto";
                    DropDownList1.DataValueField = "id_est_concreto";
                    DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
                    DropDownList1.SelectedValue = int_estatusID.ToString();
                }
            }
        }

        protected void chk_rppc_CheckedChanged(object sender, EventArgs e)
        {
            string str_num_muest = null;
            Guid int_clteobra;
            int int_act = 0;

            foreach (GridViewRow row in gv_rppc.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chk_rppc") as CheckBox);
                    if (chkRow.Checked)
                    {
                        int_act = int_act + 1;
                    }
                    else
                    {
                        row.BackColor = Color.White;
                    }
                }
            }

            if (int_act == 0)
            {
                limp_txt_rppc();
            }
            else
            {
                foreach (GridViewRow row in gv_rppc.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chk_rppc") as CheckBox);
                        if (chkRow.Checked)
                        {
                            int_act = int_act + 1;
                            limp_txt_rppc();
                            row.BackColor = Color.FromArgb(227, 76, 14);
                            str_num_muest = row.Cells[1].Text;
                        }
                        else
                        {
                            row.BackColor = Color.White;
                        }
                    }
                }
                try
                {
                    using (dd_liecEntities edm_clte = new dd_liecEntities())
                    {
                        var i_clte = (from c in edm_clte.inf_mrp_concreto
                                      where c.no_muesra == str_num_muest
                                      select c).FirstOrDefault();

                        int_clteobra = i_clte.id_mrp_concreto;
                    }

                    using (dd_liecEntities edm_clte = new dd_liecEntities())
                    {
                        var i_clte = (from t_clte in edm_clte.v_pfe
                                      where t_clte.id_mrp_concreto == int_clteobra

                                      select new
                                      {
                                          t_clte.no_muesra,
                                          t_clte.fecha_colado,
                                          t_clte.fecha_recibe,
                                          t_clte.recibe,
                                          t_clte.entrega,
                                          t_clte.presion,
                                          t_clte.id_tipo_especimen,
                                          t_clte.id_tipo_concreto,
                                          t_clte.id_sit_concreto,
                                          t_clte.procedecia,
                                          t_clte.elemento,
                                          t_clte.dosificacion,
                                          t_clte.resistencia,
                                          t_clte.clase,
                                          t_clte.reva,
                                          t_clte.tma,
                                          t_clte.olla,
                                          t_clte.remision,
                                          t_clte.sali_planta,
                                          t_clte.llega_obra,
                                          t_clte.desca_ini,
                                          t_clte.desca_term,
                                          t_clte.vol,
                                          t_clte.revb,
                                          t_clte.localiza,
                                          t_clte.dia_ensaye,
                                          t_clte.n_m,
                                          t_clte.fecha_ensaye
                                      }).ToList();
                        DateTime f_colad = Convert.ToDateTime(i_clte[0].fecha_colado);
                        DateTime f_recib = Convert.ToDateTime(i_clte[0].fecha_recibe);
                        TimeSpan? ts_splanta, ts_llobra, ts_ini, s_term;

                        ddl_tesp_rppc.SelectedValue = i_clte[0].id_tipo_especimen.ToString();
                        ddl_tconc_rppc.SelectedValue = i_clte[0].id_tipo_concreto.ToString();
                        ddl_sit_rppc.SelectedValue = i_clte[0].id_sit_concreto.ToString();

                        int int_sit = int.Parse(i_clte[0].id_sit_concreto.ToString());
                        if (int_sit == 3)
                        {
                            div_doc.Visible = true;
                            txt_proce_rppc.Text = i_clte[0].procedecia;
                            txt_elem_rppc.Text = i_clte[0].elemento;
                            txt_dosi_rppc.Text = i_clte[0].dosificacion;
                            txt_resis_rppc.Text = i_clte[0].resistencia;
                            txt_olla_rrpc.Text = i_clte[0].olla;
                            txt_remi_rppc.Text = i_clte[0].remision;
                            txt_loca_rppc.Text = i_clte[0].localiza;
                            txt_clase_rppc.Text = Convert.ToInt32(i_clte[0].clase).ToString();
                            txt_rev_rrpc.Text = Math.Round(Convert.ToDecimal(i_clte[0].reva), 2).ToString();
                            txt_tma_rrpc.Text = Math.Round(Convert.ToDecimal(i_clte[0].tma), 2).ToString();
                            txt_vol_rppc.Text = Math.Round(Convert.ToDecimal(i_clte[0].vol), 2).ToString();
                            ts_splanta = i_clte[0].sali_planta;
                            ts_llobra = i_clte[0].llega_obra;
                            ts_ini = i_clte[0].desca_ini;
                            s_term = i_clte[0].desca_term;
                            txt_revb_rppc.Text = Math.Round(Convert.ToDecimal(i_clte[0].revb), 2).ToString();
                            txt_splata_rrpc.Text = ts_splanta.ToString();
                            txt_llobra_rrpc.Text = ts_llobra.ToString();
                            txt_inic_rrpc.Text = ts_ini.ToString();
                            txt_term_rrpc.Text = s_term.ToString();
                        }
                        else
                        {
                            div_doc.Visible = false;
                        }

                        nmue_rppc.Text = i_clte[0].no_muesra;
                        fcol_rppc.Text = f_colad.ToString("yyyy-MM-dd");
                        frec_rppc.Text = f_recib.ToString("yyyy-MM-dd");
                        entrega_rppc.Text = i_clte[0].entrega;
                        recibe_rppc.Text = i_clte[0].recibe;
                        r_rppc.Text = i_clte[0].presion.ToString();
                        int int_nm = Convert.ToInt32(i_clte[0].n_m);
                        foreach (var nn in i_clte)
                        {
                            if (nn.dia_ensaye == "uno")
                            {
                                chkb_1_rppc.Checked = true;
                                txt_cant1_rppc.Text = nn.n_m.ToString();
                                f_colad = DateTime.Parse(fcol_rppc.Text);
                                txt_f1_rppc.Text = f_colad.AddDays(1).ToString("yyyy-MM-dd");
                            }
                            else if (nn.dia_ensaye == "tres")
                            {
                                chkb_3_rppc.Checked = true;
                                txt_cant3_rppc.Text = nn.n_m.ToString();
                                f_colad = DateTime.Parse(fcol_rppc.Text);
                                txt_f3_rppc.Text = f_colad.AddDays(3).ToString("yyyy-MM-dd");
                            }
                            else if (nn.dia_ensaye == "siete")
                            {
                                chkb_7_rppc.Checked = true;
                                txt_cant7_rppc.Text = nn.n_m.ToString();
                                f_colad = DateTime.Parse(fcol_rppc.Text);
                                txt_f7_rppc.Text = f_colad.AddDays(7).ToString("yyyy-MM-dd");
                            }
                            else if (nn.dia_ensaye == "catorce")
                            {
                                chkb_14_rppc.Checked = true;
                                txt_cant14_rppc.Text = nn.n_m.ToString();
                                f_colad = DateTime.Parse(fcol_rppc.Text);
                                txt_f14_rppc.Text = f_colad.AddDays(14).ToString("yyyy-MM-dd");
                            }
                            else if (nn.dia_ensaye == "vienteocho")
                            {
                                chkb_28_rppc.Checked = true;
                                txt_cant28_rppc.Text = nn.n_m.ToString();
                                f_colad = DateTime.Parse(fcol_rppc.Text);
                                txt_f28_rppc.Text = f_colad.AddDays(28).ToString("yyyy-MM-dd");
                            }
                            else if (nn.dia_ensaye == "cero")
                            {
                                chkb_otro_rppc.Checked = true;
                                txt_cantotro_rppc.Text = nn.n_m.ToString();
                                int int_fotro_rppc = int.Parse(txt_cantotro_rppc.Text);
                                f_colad = DateTime.Parse(fcol_rppc.Text);
                                txt_fotro_rppc.Text = f_colad.AddDays(int_fotro_rppc).ToString("yyyy-MM-dd");
                            }
                        }
                    }
                }
                catch
                {
                    Mensaje("Sin ensayos, favor de agregar");
                }
            }
        }

        protected void ddl_rppc_est_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void btn_buscar_rpc_Click(object sender, EventArgs e)
        {
            gv_rppc.Visible = false;
            limp_txt_rppc();
            try
            {
                string f_rpc = txt_buscar_rpc.Text.Trim();

                using (dd_liecEntities data_clte = new dd_liecEntities())
                {
                    var i_clte = (from t_clte in data_clte.inf_mrp_concreto
                                  where t_clte.no_muesra == f_rpc
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
            {
                gv_clte_obras.Visible = false;
            }
        }

        protected void btn_editar_rppc_Click(object sender, EventArgs e)
        {
            int_rppc = 2;
            div_rppc.Visible = true;
            div_rpc.Visible = true;
            i_agregar_rppc.Attributes["style"] = "color:white";
            i_editar_rppc.Attributes["style"] = "color:#E34C0E";
            limp_txt_rppc();
            gv_rppc.Visible = false;
            int guid_cltef;
            string str_clteobra;

            try
            {
                string n_rub;
                Char char_s = '|';
                string d_rub = txt_buscar_rppc.Text.Trim();
                String[] de_rub = d_rub.Trim().Split(char_s);
                n_rub = de_rub[0].Trim();

                using (dd_liecEntities data_clte = new dd_liecEntities())
                {
                    var i_co = (from t_clte in data_clte.inf_clte_obras
                                where t_clte.clave_obra == n_rub
                                select t_clte).FirstOrDefault();

                    guid_cltef = i_co.id_clte_obras;

                    var i_clte = (from t_clte in data_clte.inf_mrp_concreto
                                  where t_clte.id_clte_obras == guid_cltef
                                  select new
                                  {
                                      t_clte.no_muesra,
                                      t_clte.fecha_registro
                                  }).OrderBy(x => x.no_muesra).ToList();

                    gv_rppc.DataSource = i_clte;
                    gv_rppc.DataBind();
                    gv_rppc.Visible = true;
                }
            }
            catch
            {
                gv_clte_obras.Visible = false;
            }
        }

        protected void chkb_desactivar_rppc_CheckedChanged(object sender, EventArgs e)
        {
            rfv_buscar_rppc.Enabled = false;
            rfv_nmue_rppc.Enabled = false;
            rfv_fcol_rppc.Enabled = false;
            rfv_frec_rppc.Enabled = false;
            rfv_entrega_rppc.Enabled = false;
            rfv_recibe_rppc.Enabled = false;
            rfv_r_rppc.Enabled = false;
            rfv_tesp_rppc.Enabled = false;
            rfv_tconc_rppc.Enabled = false;
            rfv_sit_rppc.Enabled = false;
            rfv_f1_rppc.Enabled = false;
            rfv_f3_rppc.Enabled = false;
            rfv_f7_rppc.Enabled = false;
            rfv_f14_rppc.Enabled = false;
            rfv_f28_rppc.Enabled = false;
            rfv_ftro_rppc.Enabled = false;

            rfv_proce_rppc.Enabled = false;
            rfv_elem_rppc.Enabled = false;
            rfv_dosi_rppc.Enabled = false;
            rfv_resis_rppc.Enabled = false;
            rfv_clase_rppc.Enabled = false;
            rfv_rev_rrpc.Enabled = false;
            rfv_tma_rrpc.Enabled = false;
            rfv_olla_rrpc.Enabled = false;
            rfv_remi_rppc.Enabled = false;
            rfv_splata_rrpc.Enabled = false;
            rfv_llobra_rrpc.Enabled = false;
            rfvl_inic_rrpc.Enabled = false;
            rfv_term_rrpc.Enabled = false;
            rfv_vol_rppc.Enabled = false;
            rfv_revb_rppc.Enabled = false;
            rfv_loca_rppc.Enabled = false;

            chkb_1_rppc.Checked = false;
            chkb_3_rppc.Checked = false;
            chkb_7_rppc.Checked = false;
            chkb_14_rppc.Checked = false;
            chkb_28_rppc.Checked = false;
            chkb_otro_rppc.Checked = false;
            txt_cant1_rppc.Text = null;
            txt_cant3_rppc.Text = null;
            txt_cant7_rppc.Text = null;
            txt_cant14_rppc.Text = null;
            txt_cant28_rppc.Text = null;
            txt_cantotro_rppc.Text = null;
            txt_f1_rppc.Text = null;
            txt_f3_rppc.Text = null;
            txt_f7_rppc.Text = null;
            txt_f14_rppc.Text = null;
            txt_f28_rppc.Text = null;
            txt_fotro_rppc.Text = null;
        }

        private void limp_txt_rppc()
        {
            ddl_tesp_rppc.Items.Clear();

            using (dd_liecEntities db_sepomex = new dd_liecEntities())
            {
                var tbl_sepomex = (from c in db_sepomex.fact_especimen
                                   select c).ToList();

                ddl_tesp_rppc.DataSource = tbl_sepomex;
                ddl_tesp_rppc.DataTextField = "desc_tipo_especimen";
                ddl_tesp_rppc.DataValueField = "id_tipo_especimen";
                ddl_tesp_rppc.DataBind();
            }
            ddl_tesp_rppc.Items.Insert(0, new ListItem("SELECCIONAR", "0"));

            ddl_tconc_rppc.Items.Clear();

            using (dd_liecEntities db_sepomex = new dd_liecEntities())
            {
                var tbl_sepomex = (from c in db_sepomex.fac_tipo_concreto
                                   select c).ToList();

                ddl_tconc_rppc.DataSource = tbl_sepomex;
                ddl_tconc_rppc.DataTextField = "desc_tipo_concreto";
                ddl_tconc_rppc.DataValueField = "id_tipo_concreto";
                ddl_tconc_rppc.DataBind();
            }
            ddl_tconc_rppc.Items.Insert(0, new ListItem("SELECCIONAR", "0"));

            ddl_sit_rppc.Items.Clear();

            using (dd_liecEntities db_sepomex = new dd_liecEntities())
            {
                var tbl_sepomex = (from c in db_sepomex.fac_sit_concreto
                                   select c).ToList();

                ddl_sit_rppc.DataSource = tbl_sepomex;
                ddl_sit_rppc.DataTextField = "desc_sit_concreto";
                ddl_sit_rppc.DataValueField = "id_sit_concreto";
                ddl_sit_rppc.DataBind();
            }
            ddl_sit_rppc.Items.Insert(0, new ListItem("SELECCIONAR", "0"));

            nmue_rppc.Text = null;
            fcol_rppc.Text = null;
            frec_rppc.Text = null;
            entrega_rppc.Text = null;
            recibe_rppc.Text = null;
            r_rppc.Text = null;

            chkb_1_rppc.Checked = false;
            chkb_3_rppc.Checked = false;
            chkb_7_rppc.Checked = false;
            chkb_14_rppc.Checked = false;
            chkb_28_rppc.Checked = false;
            chkb_otro_rppc.Checked = false;
            txt_cant1_rppc.Text = null;
            txt_cant3_rppc.Text = null;
            txt_cant7_rppc.Text = null;
            txt_cant14_rppc.Text = null;
            txt_cant28_rppc.Text = null;
            txt_cantotro_rppc.Text = null;
            txt_f1_rppc.Text = null;
            txt_f3_rppc.Text = null;
            txt_f7_rppc.Text = null;
            txt_f14_rppc.Text = null;
            txt_f28_rppc.Text = null;
            txt_fotro_rppc.Text = null;

            txt_proce_rppc.Text = null;
            txt_elem_rppc.Text = null;
            txt_dosi_rppc.Text = null;
            txt_resis_rppc.Text = null;
            txt_olla_rrpc.Text = null;
            txt_remi_rppc.Text = null;
            txt_loca_rppc.Text = null;
            txt_clase_rppc.Text = null;
            txt_rev_rrpc.Text = null;
            txt_tma_rrpc.Text = null;
            txt_vol_rppc.Text = null;
            txt_revb_rppc.Text = null;
            txt_splata_rrpc.Text = null;
            txt_llobra_rrpc.Text = null;
            txt_inic_rrpc.Text = null;
            txt_term_rrpc.Text = null;
        }

        protected void btn_guardar_rppc_Click(object sender, EventArgs e)
        {
            if (int_rppc == 0)
            {
                Mensaje("Favor de seleccionar una acción");
            }
            else
            {
                //try
                //{
                Guid guis_usr_capt = Guid.Parse("0298DF9A-4DBF-4EE5-84F7-1E7E519BE4F9");
                Guid gui_nrppc;
                string str_nomues_rppc, entr_rppc, recib_rppc, proce_rppc, elem_rppc, dosi_rppc, resi_rppc, olla_rppc, remi_rppc, loca_rppc;
                DateTime fcolado_rrpc, fente_rrpc;
                bool bool_1d, bool_3d, bool_7d, bool_14d, bool_28d, bool_otrod;
                decimal dml_reva_rppc, dml_vol_rppc, dl_revb_rppc;
                TimeSpan dt_splan_rppc, dt_llobra_rppc, dt_desca_ini_rppc, dt_desca_term_rppc;
                int int_tesp_rppc, int_tcont_rrp, int_sit_rppc, presi_rppc, int_co, int_1d, int_3d, int_7d, int_14d, int_28d, int_otrod, int_clase, int_tma_rppc;

                gui_nrppc = Guid.NewGuid();
                str_nomues_rppc = nmue_rppc.Text.Trim().ToUpper();
                fcolado_rrpc = DateTime.Parse(fcol_rppc.Text);
                fente_rrpc = DateTime.Parse(frec_rppc.Text);
                entr_rppc = entrega_rppc.Text.Trim().ToUpper();
                recib_rppc = recibe_rppc.Text.Trim().ToUpper();
                presi_rppc = int.Parse(r_rppc.Text);

                int_tesp_rppc = int.Parse(ddl_tesp_rppc.SelectedValue);
                int_tcont_rrp = int.Parse(ddl_tconc_rppc.SelectedValue);
                int_sit_rppc = int.Parse(ddl_sit_rppc.SelectedValue);

                int int_sit = int.Parse(ddl_sit_rppc.SelectedValue);
                proce_rppc = null;
                elem_rppc = null;
                dosi_rppc = null;
                resi_rppc = null;
                olla_rppc = null;
                remi_rppc = null;
                loca_rppc = null;
                int_clase = 0;
                dml_reva_rppc = 0;
                int_tma_rppc = 0;
                dml_vol_rppc = 0;
                dl_revb_rppc = 0;
                dt_splan_rppc = TimeSpan.Zero;
                dt_llobra_rppc = TimeSpan.Zero;
                dt_desca_ini_rppc = TimeSpan.Zero;
                dt_desca_term_rppc = TimeSpan.Zero;
                if (int_sit == 3)
                {
                    proce_rppc = txt_proce_rppc.Text.Trim().ToUpper();
                    elem_rppc = txt_elem_rppc.Text.Trim().ToUpper();
                    dosi_rppc = txt_dosi_rppc.Text.Trim().ToUpper();
                    resi_rppc = txt_resis_rppc.Text.Trim().ToUpper();
                    olla_rppc = txt_olla_rrpc.Text.Trim().ToUpper();
                    remi_rppc = txt_remi_rppc.Text.Trim().ToUpper();
                    loca_rppc = txt_loca_rppc.Text.Trim().ToUpper();
                    int_clase = int.Parse(txt_clase_rppc.Text.Trim().ToUpper());
                    dml_reva_rppc = decimal.Parse(txt_rev_rrpc.Text.Trim().ToUpper());
                    int_tma_rppc = int.Parse(txt_tma_rrpc.Text.Trim().ToUpper());
                    dml_vol_rppc = decimal.Parse(txt_vol_rppc.Text.Trim().ToUpper());
                    dl_revb_rppc = decimal.Parse(txt_revb_rppc.Text.Trim().ToUpper());
                    dt_splan_rppc = TimeSpan.Parse(txt_splata_rrpc.Text);
                    dt_llobra_rppc = TimeSpan.Parse(txt_llobra_rrpc.Text);
                    dt_desca_ini_rppc = TimeSpan.Parse(txt_inic_rrpc.Text);
                    dt_desca_term_rppc = TimeSpan.Parse(txt_term_rrpc.Text);
                }
                if (chkb_1_rppc.Checked)
                {
                    bool_1d = true;
                    int_1d = int.Parse(txt_cant1_rppc.Text);
                }
                else
                {
                    bool_1d = false;
                    int_1d = 0;
                }
                if (chkb_3_rppc.Checked)
                {
                    bool_3d = true;
                    int_3d = int.Parse(txt_cant3_rppc.Text);
                }
                else
                {
                    bool_3d = false;
                    int_3d = 0;
                }
                if (chkb_7_rppc.Checked)
                {
                    bool_7d = true;
                    int_7d = int.Parse(txt_cant7_rppc.Text);
                }
                else
                {
                    bool_7d = false;
                    int_7d = 0;
                }
                if (chkb_14_rppc.Checked)
                {
                    bool_14d = true;
                    int_14d = int.Parse(txt_cant14_rppc.Text);
                }
                else
                {
                    bool_14d = false;
                    int_14d = 0;
                }
                if (chkb_28_rppc.Checked)
                {
                    bool_28d = true;
                    int_28d = int.Parse(txt_cant28_rppc.Text);
                }
                else
                {
                    bool_28d = false;
                    int_28d = 0;
                }
                if (chkb_otro_rppc.Checked)
                {
                    bool_otrod = true;
                    int_otrod = int.Parse(txt_cantotro_rppc.Text);
                }
                else
                {
                    bool_otrod = false;
                    int_otrod = 0;
                }
                string n_rub;
                int int_pfe = 0;
                int_pfe = int_1d + int_3d + int_7d + int_14d + int_28d + int_otrod;

                string[,] pfe_array = new string[1, 6];
                pfe_array[0, 0] = "uno," + int_1d.ToString();
                pfe_array[0, 1] = "tres," + int_3d.ToString();
                pfe_array[0, 2] = "siete," + int_7d.ToString();
                pfe_array[0, 3] = "catorce," + int_14d.ToString();
                pfe_array[0, 4] = "vienteocho," + int_28d.ToString();
                pfe_array[0, 5] = "cero," + int_otrod.ToString();

                Char char_s = '|';
                string d_rub = txt_buscar_rppc.Text.Trim();
                String[] de_rub = d_rub.Trim().Split(char_s);
                n_rub = de_rub[0].Trim();

                if (int_rppc == 2)
                {
                    int int_ddl;
                    int int_estatusID = 0;
                    int int_act = 0;
                    foreach (GridViewRow row in gv_rppc.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[0].FindControl("chk_rppc") as CheckBox);
                            if (chkRow.Checked)
                            {
                                int_act = int_act + 1;
                            }
                            else
                            {
                                row.BackColor = Color.White;
                            }
                        }
                    }
                    if (int_act == 0)
                    {
                        Mensaje("Favor de seleccionar una muestra.");
                    }
                    else
                    {
                        foreach (GridViewRow row_d in gv_rppc.Rows)
                        {
                            // int key = (int)GridView1.DataKeys[row.RowIndex].Value;
                            if (row_d.RowType == DataControlRowType.DataRow)
                            {
                                DropDownList dl = (DropDownList)row_d.FindControl("ddl_rppc_est");

                                int_ddl = int.Parse(dl.SelectedValue);
                                if (int_ddl == 1)
                                {
                                    int_estatusID = 1;
                                }
                                else if (int_ddl == 2)
                                {
                                    int_estatusID = 2;
                                }
                            }
                            if (row_d.RowType == DataControlRowType.DataRow)
                            {
                                CheckBox chkRow = (row_d.Cells[0].FindControl("chk_rppc") as CheckBox);
                                if (chkRow.Checked)
                                {
                                    string str_nm = row_d.Cells[1].Text;
                                    Guid guid_frppc;
                                    using (var m_clte = new dd_liecEntities())
                                    {
                                        var i_clte = (from c in m_clte.inf_mrp_concreto
                                                      where c.no_muesra == str_nm
                                                      select c).FirstOrDefault();

                                        guid_frppc = i_clte.id_mrp_concreto;

                                        i_clte.id_est_concreto = int_estatusID;
                                        i_clte.no_muesra = str_nomues_rppc;
                                        i_clte.fecha_colado = fcolado_rrpc;
                                        i_clte.fecha_recibe = fente_rrpc;
                                        i_clte.entrega = entr_rppc;
                                        i_clte.recibe = recib_rppc;
                                        i_clte.presion = presi_rppc;
                                        i_clte.id_tipo_especimen = int_tesp_rppc;
                                        i_clte.id_tipo_concreto = int_tcont_rrp;
                                        i_clte.id_sit_concreto = int_sit_rppc;
                                        i_clte.procedecia = proce_rppc;
                                        i_clte.elemento = elem_rppc;
                                        i_clte.dosificacion = dosi_rppc;
                                        i_clte.resistencia = resi_rppc;
                                        i_clte.clase = int_clase;
                                        i_clte.reva = dml_reva_rppc;
                                        i_clte.tma = int_tma_rppc;
                                        i_clte.olla = olla_rppc;
                                        i_clte.remision = remi_rppc;
                                        i_clte.sali_planta = dt_splan_rppc;
                                        i_clte.llega_obra = dt_llobra_rppc;
                                        i_clte.desca_ini = dt_desca_ini_rppc;
                                        i_clte.desca_term = dt_desca_term_rppc;
                                        i_clte.vol = dml_vol_rppc;
                                        i_clte.revb = dl_revb_rppc;
                                        i_clte.localiza = loca_rppc;

                                        m_clte.SaveChanges();
                                    }

                                    //using (var m_conc_ec = new dd_liecEntities())
                                    //{
                                    //    var i_s = (from c in m_conc_ec.inf_conc_ec
                                    //               where c.id_mrp_concreto == guid_frppc
                                    //               select c).ToList();
                                    //    if (i_s.Count != 0)
                                    //    {
                                    //        i_s.ForEach(c => m_conc_ec.inf_conc_ec.Remove(c));
                                    //        m_conc_ec.SaveChanges();
                                    //    }

                                    //}

                                    foreach (string n in pfe_array)
                                    {
                                        Char char_f = ',';
                                        String[] de_pfe = n.ToString().Trim().Split(char_f);
                                        string n_pfe = de_pfe[0].Trim();
                                        int f_pfe = int.Parse(de_pfe[1]);
                                        if (f_pfe == 0)
                                        {
                                        }
                                        else
                                        {
                                            int fcolm = 0;
                                            if (n_pfe == "uno")
                                            {
                                                fcolm = 1;
                                            }
                                            else if (n_pfe == "tres")
                                            {
                                                fcolm = 3;
                                            }
                                            else if (n_pfe == "siete")
                                            {
                                                fcolm = 7;
                                            }
                                            else if (n_pfe == "catorce")
                                            {
                                                fcolm = 14;
                                            }
                                            else if (n_pfe == "vienteocho")
                                            {
                                                fcolm = 28;
                                            }
                                            else if (n_pfe == "cero")
                                            {
                                                fcolm = 0;
                                            }

                                            if (f_pfe == 2)
                                            {
                                                for (int i = 0; i < f_pfe; i++)
                                                {
                                                    using (var m_conc_ec = new dd_liecEntities())
                                                    {
                                                        var i_conc_ec = (from c in m_conc_ec.inf_conc_ec
                                                                         where c.id_mrp_concreto == guid_frppc
                                                                         select c).FirstOrDefault();
                                                        i_conc_ec.dia_ensaye = n_pfe;
                                                        i_conc_ec.num_muesra = 1;
                                                        i_conc_ec.fecha_ensaye = fcolado_rrpc.AddDays(fcolm);
                                                        m_conc_ec.SaveChanges();
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                using (var m_conc_ec = new dd_liecEntities())
                                                {
                                                    var i_conc_ec = (from c in m_conc_ec.inf_conc_ec
                                                                     where c.id_mrp_concreto == guid_frppc
                                                                     select c).FirstOrDefault();
                                                    i_conc_ec.dia_ensaye = n_pfe;
                                                    i_conc_ec.num_muesra = 1;
                                                    i_conc_ec.fecha_ensaye = fcolado_rrpc.AddDays(fcolm);
                                                    m_conc_ec.SaveChanges();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        foreach (GridViewRow row in gv_rppc.Rows)
                        {
                        }

                        limp_txt_rppc();
                        gv_rppc.Visible = false;
                        Mensaje("Datos de cliente-obra-muestra actualizados con éxito.");
                    }
                }
                else
                {
                    using (dd_liecEntities edm_nclte = new dd_liecEntities())
                    {
                        var i_co = (from c in edm_nclte.inf_clte_obras
                                    where c.clave_obra == n_rub
                                    select c).FirstOrDefault();
                        int_co = i_co.id_clte_obras;

                        var i_nclte = (from c in edm_nclte.inf_mrp_concreto
                                       where c.no_muesra == str_nomues_rppc
                                       select c).ToList();

                        if (i_nclte.Count == 0)
                        {
                            using (var m_clte = new dd_liecEntities())
                            {
                                var i_clte = new inf_mrp_concreto

                                {
                                    id_mrp_concreto = gui_nrppc,
                                    id_est_concreto = 1,
                                    no_muesra = str_nomues_rppc,
                                    fecha_colado = fcolado_rrpc,
                                    fecha_recibe = fente_rrpc,
                                    entrega = entr_rppc,
                                    recibe = recib_rppc,
                                    presion = presi_rppc,
                                    id_tipo_especimen = int_tesp_rppc,
                                    id_tipo_concreto = int_tcont_rrp,
                                    id_sit_concreto = int_sit_rppc,
                                    procedecia = proce_rppc,
                                    elemento = elem_rppc,
                                    dosificacion = dosi_rppc,
                                    resistencia = resi_rppc,
                                    clase = int_clase,
                                    reva = dml_reva_rppc,
                                    tma = int_tma_rppc,
                                    olla = olla_rppc,
                                    remision = remi_rppc,
                                    sali_planta = dt_splan_rppc,
                                    llega_obra = dt_llobra_rppc,
                                    desca_ini = dt_desca_ini_rppc,
                                    desca_term = dt_desca_term_rppc,
                                    vol = dml_vol_rppc,
                                    revb = dl_revb_rppc,
                                    localiza = loca_rppc,
                                    id_usuario = guis_usr_capt,
                                    fecha_registro = DateTime.Now,
                                    id_emp = guid_emp,
                                    id_clte_obras = int_co
                                };

                                m_clte.inf_mrp_concreto.Add(i_clte);
                                m_clte.SaveChanges();
                            }

                            foreach (string n in pfe_array)
                            {
                                Char char_f = ',';
                                String[] de_pfe = n.ToString().Trim().Split(char_f);
                                string n_pfe = de_pfe[0].Trim();
                                int f_pfe = int.Parse(de_pfe[1]);
                                if (f_pfe == 0)
                                {
                                }
                                else
                                {
                                    int fcolm = 0;
                                    if (n_pfe == "uno")
                                    {
                                        fcolm = 1;
                                    }
                                    else if (n_pfe == "tres")
                                    {
                                        fcolm = 3;
                                    }
                                    else if (n_pfe == "siete")
                                    {
                                        fcolm = 7;
                                    }
                                    else if (n_pfe == "catorce")
                                    {
                                        fcolm = 14;
                                    }
                                    else if (n_pfe == "vienteocho")
                                    {
                                        fcolm = 28;
                                    }
                                    else if (n_pfe == "cero")
                                    {
                                        fcolm = 0;
                                    }
                                    string str_cod_esp;
                                    int int_c_e;
                                    using (dd_liecEntities edm_rub = new dd_liecEntities())
                                    {
                                        var i_rub = (from c in edm_rub.inf_conc_ec
                                                     where c.id_mrp_concreto == gui_nrppc
                                                     select c).ToList();
                                        int_c_e = i_rub.Count;
                                        if (int_c_e == 0)
                                        {
                                            str_cod_esp = "ESP-" + string.Format("{0:000}", int_c_e + 1);
                                        }
                                        else
                                        {
                                            str_cod_esp = "ESP-" + string.Format("{0:000}", int_c_e + 1);
                                        }
                                    }
                                    if (f_pfe == 2)
                                    {

                                        for (int i = 0; i < f_pfe; i++)
                                        {
                                            str_cod_esp = "ESP-" + string.Format("{0:000}", int_c_e + i + 1);
                                            using (var m_conc_ec = new dd_liecEntities())
                                            {
                                                var i_conc_ec = new inf_conc_ec

                                                {
                                                    id_conc_ec = Guid.NewGuid(),
                                                    cod_esp = str_cod_esp,
                                                    id_mrp_concreto = gui_nrppc,
                                                    dia_ensaye = n_pfe,
                                                    num_muesra = 1,
                                                    id_est_ensaye = 1,
                                                    fecha_ensaye = fcolado_rrpc.AddDays(fcolm),

                                                    fecha_registro = DateTime.Now,
                                                };

                                                m_conc_ec.inf_conc_ec.Add(i_conc_ec);
                                                m_conc_ec.SaveChanges();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        using (var m_conc_ec = new dd_liecEntities())
                                        {
                                            var i_conc_ec = new inf_conc_ec

                                            {
                                                id_conc_ec = Guid.NewGuid(),
                                                cod_esp = str_cod_esp,
                                                id_mrp_concreto = gui_nrppc,
                                                dia_ensaye = n_pfe,
                                                num_muesra = f_pfe,
                                                id_est_ensaye = 1,
                                                fecha_ensaye = fcolado_rrpc.AddDays(fcolm),

                                                fecha_registro = DateTime.Now,
                                            };

                                            m_conc_ec.inf_conc_ec.Add(i_conc_ec);
                                            m_conc_ec.SaveChanges();
                                        }
                                    }
                                }
                            }

                            limp_txt_rppc();
                            gv_rppc.Visible = false;
                            Mensaje("Datos de cliente-obra-muestra agregados con éxito.");
                        }
                        else
                        {
                            limp_txt_rppc();
                            gv_rppc.Visible = false;
                            Mensaje("Obra existe en la base de datos, favor de revisar.");
                        }
                    }
                }
                //}
                //catch
                //{
                //    int_rppc = 0;
                //    limp_txt_rppc();

                //    i_agregar_rppc.Attributes["style"] = "color:white";
                //    i_editar_rppc.Attributes["style"] = "color:white";
                //    Mensaje("Favor de seleccionar una muestra");
                //}
            }
        }

        protected void gv_rppc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int guid_cltef;
            gv_rppc.PageIndex = e.NewPageIndex;

            try
            {
                string n_rub;
                Char char_s = '|';
                string d_rub = txt_buscar_rppc.Text.Trim();
                String[] de_rub = d_rub.Trim().Split(char_s);
                n_rub = de_rub[0].Trim();

                using (dd_liecEntities data_clte = new dd_liecEntities())
                {
                    var i_co = (from t_clte in data_clte.inf_clte_obras
                                where t_clte.clave_obra == n_rub
                                select t_clte).FirstOrDefault();

                    guid_cltef = i_co.id_clte_obras;

                    var i_clte = (from t_clte in data_clte.inf_mrp_concreto
                                  where t_clte.id_clte_obras == guid_cltef
                                  select new
                                  {
                                      t_clte.no_muesra,
                                      t_clte.fecha_registro
                                  }).OrderBy(x => x.no_muesra).ToList();

                    gv_rppc.DataSource = i_clte;
                    gv_rppc.DataBind();
                    gv_rppc.Visible = true;
                }
            }
            catch
            {
                gv_clte_obras.Visible = false;
            }
        }

        #endregion rppc

        #region ensayo

        protected void btn_agregar_ensa_comp_Click(object sender, EventArgs e)
        {
            int_ensa_comp = 1;
            div_ensa_comp.Visible = true;
            div_rpc.Visible = false;
            
            i_editar_ensa_comp.Attributes["style"] = "color:white";

            rfv_buscar_ensa_comp.Enabled = true;
            rfv_clavensa_a.Enabled = true;
            rfv_dire_a.Enabled = true;

            rfv_masa_a.Enabled = true;
            rfv_inte_a.Enabled = true;

            rfv_altu_aa.Enabled = true;
            rfv_altu_ab.Enabled = true;
            rfv_lad_ab.Enabled = true;
            rfv_lad_aa.Enabled = true;

            rfv_pres_a.Enabled = true;

            rfv_tf_a.Enabled = true;

            limp_txt_ensa_comp();
        }

        protected void btn_editar_ensa_comp_Click(object sender, EventArgs e)
        {
        }

        protected void txt_altu_ab_TextChanged(object sender, EventArgs e)
        {
            decimal altu_aa, altu_ab, altu_ap;

            if (string.IsNullOrEmpty(txt_altu_aa.Text))
            {
                rfv_altu_aa.Enabled = true;
            }
            else
            {
                if (string.IsNullOrEmpty(txt_altu_ab.Text))
                {
                    rfv_altu_ab.Enabled = true;
                }
                else
                {
                    rfv_altu_aa.Enabled = false;
                    rfv_altu_ab.Enabled = false;
                    altu_aa = decimal.Parse(txt_altu_aa.Text);
                    altu_ab = decimal.Parse(txt_altu_aa.Text);
                    altu_ap = ((altu_aa + altu_ab) / 2);
                    txt_altu_ap.Text = altu_ap.ToString();
                }
            }
        }

        protected void txt_lad_ab_TextChanged(object sender, EventArgs e)
        {
            decimal altu_aa, altu_ab, altu_ap, area_mm2, masa_a;

            if (string.IsNullOrEmpty(txt_lad_aa.Text))
            {
                rfv_altu_aa.Enabled = true;
            }
            else
            {
                if (string.IsNullOrEmpty(txt_lad_ab.Text))
                {
                    rfv_altu_ab.Enabled = true;
                }
                else
                {
                    rfv_altu_aa.Enabled = false;
                    rfv_altu_ab.Enabled = false;
                    altu_aa = decimal.Parse(txt_lad_aa.Text);
                    altu_ab = decimal.Parse(txt_lad_ab.Text);
                    altu_ap = ((altu_aa + altu_ab) / 2);
                    area_mm2 = altu_aa * altu_ab;
                    txt_lad_ap.Text = altu_ap.ToString();
                    txt_area_a.Text = area_mm2.ToString();
                    decimal altu_ap_a = decimal.Parse(txt_altu_ap.Text);
                    masa_a = decimal.Parse(txt_masa_a.Text);
                    txt_masavol_a.Text = Math.Round(((masa_a / (altu_ap_a * area_mm2)) * 1000000000)).ToString();
                    txt_masavolprom_a.Text = Math.Round(((masa_a / (altu_ap_a * area_mm2)) * 1000000000)).ToString();
                }
            }
        }

        protected void txt_pres_a_TextChanged(object sender, EventArgs e)
        {
            decimal altu_aa, altu_ab, altu_ap;

            if (string.IsNullOrEmpty(txt_area_a.Text))
            {
            }
            else
            {
                if (string.IsNullOrEmpty(txt_pres_a.Text))
                {
                }
                else
                {
                    decimal v_m = Convert.ToDecimal(101.9716213);
                    altu_aa = decimal.Parse(txt_area_a.Text);
                    altu_ab = decimal.Parse(txt_pres_a.Text);
                    altu_ap = v_m * altu_ab / altu_aa * 100;
                    txt_esfu_a.Text = Math.Round(altu_ap).ToString();
                    txt_esfuprom_a.Text = Math.Round(altu_ap).ToString();
                    txt_dif_ab_a.Text = Math.Round(Math.Abs(altu_ap - altu_ap), 0).ToString();
                }
            }
        }

        protected void ddl_sit_rppc_SelectedIndexChanged(object sender, EventArgs e)
        {
            int int_sit = int.Parse(ddl_sit_rppc.SelectedValue);

            if (int_sit == 3)
            {
                div_doc.Visible = true;
                rfv_proce_rppc.Enabled = true;
                rfv_elem_rppc.Enabled = true;
                rfv_dosi_rppc.Enabled = true;
                rfv_resis_rppc.Enabled = true;
                rfv_clase_rppc.Enabled = true;
                rfv_rev_rrpc.Enabled = true;
                rfv_tma_rrpc.Enabled = true;
                rfv_olla_rrpc.Enabled = true;
                rfv_remi_rppc.Enabled = true;
                rfv_splata_rrpc.Enabled = true;
                rfv_llobra_rrpc.Enabled = true;
                rfvl_inic_rrpc.Enabled = true;
                rfv_term_rrpc.Enabled = true;
                rfv_vol_rppc.Enabled = true;
                rfv_revb_rppc.Enabled = true;
                rfv_loca_rppc.Enabled = true;
            }
            else
            {
                div_doc.Visible = false;
                rfv_proce_rppc.Enabled = false;
                rfv_elem_rppc.Enabled = false;
                rfv_dosi_rppc.Enabled = false;
                rfv_resis_rppc.Enabled = false;
                rfv_clase_rppc.Enabled = false;
                rfv_rev_rrpc.Enabled = false;
                rfv_tma_rrpc.Enabled = false;
                rfv_olla_rrpc.Enabled = false;
                rfv_remi_rppc.Enabled = false;
                rfv_splata_rrpc.Enabled = false;
                rfv_llobra_rrpc.Enabled = false;
                rfvl_inic_rrpc.Enabled = false;
                rfv_term_rrpc.Enabled = false;
                rfv_vol_rppc.Enabled = false;
                rfv_revb_rppc.Enabled = false;
                rfv_loca_rppc.Enabled = false;
            }
        }

        protected void chkb_desacensa_comp_CheckedChanged(object sender, EventArgs e)
        {
            rfv_buscar_ensa_comp.Enabled = false;
            rfv_clavensa_a.Enabled = false;
            rfv_dire_a.Enabled = false;

            rfv_masa_a.Enabled = false;
            rfv_inte_a.Enabled = false;

            rfv_altu_aa.Enabled = false;
            rfv_altu_ab.Enabled = false;
            rfv_lad_ab.Enabled = false;
            rfv_lad_aa.Enabled = false;

            rfv_pres_a.Enabled = false;

            rfv_tf_a.Enabled = false;
        }

        protected void gv_ensa_comp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string str_clteobraID = e.Row.Cells[1].Text;
                int int_estatusID;

                using (dd_liecEntities data_clte = new dd_liecEntities())
                {
                    var i_clte = (from t_clte in data_clte.inf_mrp_concreto
                                  where t_clte.no_muesra == str_clteobraID
                                  select new
                                  {
                                      t_clte.id_est_concreto,
                                  }).FirstOrDefault();

                    int_estatusID = int.Parse(i_clte.id_est_concreto.ToString());
                }

                DropDownList DropDownList1 = (e.Row.FindControl("ddl_ensa_comp_est") as DropDownList);

                using (dd_liecEntities db_sepomex = new dd_liecEntities())
                {
                    var tbl_sepomex = (from c in db_sepomex.fact_est_concreto
                                       select c).ToList();

                    DropDownList1.DataSource = tbl_sepomex;

                    DropDownList1.DataTextField = "desc_est_concreto";
                    DropDownList1.DataValueField = "id_est_concreto";
                    DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
                    DropDownList1.SelectedValue = int_estatusID.ToString();
                }
            }
        }

        protected void gv_ensa_comp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
        }

        protected void chk_ensa_comp_CheckedChanged(object sender, EventArgs e)
        {
            Guid guid_ensaye;
            int int_estatusID = 0;
            foreach (GridViewRow row in gv_ensa_comp.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chk_ensa_comp") as CheckBox);
                    if (chkRow.Checked)
                    {
                        int_estatusID = int_estatusID + 1;
                        row.BackColor = Color.FromArgb(227, 76, 14);
                        str_clte = row.Cells[1].Text;

                        for (int i = 3; i < 8; i++)
                        {
                            int_dem = int.Parse(row.Cells[i].Text);
                            nc = gv_ensa_comp.Columns[i].HeaderText;
                            if (int_dem != 0)
                            {


                                break;
                            }
                        }

                    }
                    else
                    {
                        row.BackColor = Color.White;
                    }
                }
            }
            if (int_estatusID == 0)
            {
                limp_txt_clte();
                div_eca.Visible = false;
            }
            if (int_dem == 1)
            {
                div_eca.Visible = true;
            }
            if (int_dem == 2)
            {
                div_eca.Visible = false;
            }

        }

        protected void btn_guardar_ensa_comp_Click(object sender, EventArgs e)
        {
            if (int_ensa_comp == 0)
            {
                Mensaje("Favor de seleccionar una acción");
            }
            else
            {
                Guid gui_nec_a, guid_numm;
                string int_ec_a, tipfall_ec_a, num_muestra = null;
                int clave_ec_a, direc_ec_a, int_ddl, int_estatusID = 0, int_select = 0, int_de, int_dem;
                decimal masa_ec_a, altu_ec_aa, altu_ec_ab, lad_ec_aa, lad_ec_ab, presion_ec_a;
                string n_rub;
                Char char_s = '|';
                string d_rub = txt_buscar_rppc.Text.Trim();
                String[] de_rub = d_rub.Trim().Split(char_s);
                n_rub = de_rub[0].Trim();

                if (int_ensa_comp == 2)
                {
                    foreach (GridViewRow row in gv_ensa_comp.Rows)
                    {
                        // int key = (int)GridView1.DataKeys[row.RowIndex].Value;
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            DropDownList dl = (DropDownList)row.FindControl("ddl_ensa_comp_est");

                            int_ddl = int.Parse(dl.SelectedValue);
                            if (int_ddl == 1)
                            {
                                int_estatusID = 1;
                            }
                            else if (int_ddl == 2)
                            {
                                int_estatusID = 2;
                            }
                        }
                    }

                    Mensaje("Datos muestra actualizado con éxito.");
                }
                else if (int_ensa_comp == 1)
                {
                    foreach (GridViewRow row in gv_ensa_comp.Rows)
                    {
                        // int key = (int)GridView1.DataKeys[row.RowIndex].Value;
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[0].FindControl("chk_ensa_comp") as CheckBox);
                            if (chkRow.Checked)
                            {
                                int_select = int_select + 1;
                                num_muestra = row.Cells[1].Text;

                                
                                
                            }
                        }
                    }

                    if (int_select == 0)
                    {
                        Mensaje("Favor de seleccionar una muestra.");
                    }
                    else
                    {
                        gui_nec_a = Guid.NewGuid();
                        clave_ec_a = int.Parse(txt_clavensa_a.Text.Trim());
                        masa_ec_a = decimal.Parse(txt_masa_a.Text);
                        direc_ec_a = int.Parse(txt_dire_a.Text);
                        int_ec_a = txt_inte_a.Text.Trim();
                        altu_ec_aa = decimal.Parse(txt_altu_aa.Text);
                        altu_ec_ab = decimal.Parse(txt_altu_ab.Text);
                        lad_ec_aa = decimal.Parse(txt_lad_aa.Text);
                        lad_ec_ab = decimal.Parse(txt_lad_ab.Text);
                        presion_ec_a = decimal.Parse(txt_pres_a.Text);
                        tipfall_ec_a = txt_tf_a.Text;

                        //using (dd_liecEntities data_clte = new dd_liecEntities())
                        //{
                        //    var i_clte = (from t_clte in data_clte.v_ensa_comp
                        //                  where t_clte.no_muesra == num_muestra
                        //                  select new
                        //                  {
                        //                      t_clte.fpfe,
                        //                      t_clte.fne,
                        //                  }).FirstOrDefault();

                        //    int_de = int.Parse(i_clte.fpfe.ToString());
                        //    int_dem = int.Parse(i_clte.fne.ToString());

                        //    var i_nm = (from t_clte in data_clte.inf_mrp_concreto
                        //                where t_clte.no_muesra == num_muestra
                        //                select new
                        //                {
                        //                    t_clte.id_mrp_concreto,
                        //                }).FirstOrDefault();

                        //    guid_numm = Guid.Parse(i_nm.id_mrp_concreto.ToString());

                        //    var i_conc_ec = new inf_conc_ec

                        //    {
                        //        cde = int_de,
                        //        cdem = int_dem,
                        //        id_conc_ec = gui_nec_a,
                        //        clave_ensa_a = clave_ec_a,
                        //        masa_a = masa_ec_a,
                        //        directo_a = direc_ec_a,
                        //        intensidad_a = int_ec_a,
                        //        altura_a = altu_ec_aa,
                        //        altura_b = altu_ec_ab,
                        //        lados_a = lad_ec_aa,
                        //        lados_b = lad_ec_ab,
                        //        presion_a = presion_ec_a,
                        //        tipofalla_a = tipfall_ec_a,
                        //        fecha_registro = DateTime.Now,
                        //        id_mrp_concreto = guid_numm
                        //    };

                        //    data_clte.inf_conc_ec.Add(i_conc_ec);
                        //    data_clte.SaveChanges();

                        //    var i_mpr = (from c in data_clte.inf_mrp_concreto
                        //                 where c.no_muesra == num_muestra
                        //                 select c).FirstOrDefault();

                        //    i_mpr.id_est_concreto = 5;

                        //    data_clte.SaveChanges();
                        //}

                        limp_txt_ensa_comp();
                        Mensaje("Registro agregado con exito.");
                    }
                }
            }
        }

        protected void btn_buscar_ensa_comp_Click(object sender, EventArgs e)
        {
            DateTime dt_dmuest = DateTime.Parse(txt_buscar_ensa_comp.Text);

            try
            {
                string f_rpc = txt_buscar_rpc.Text.Trim();

                using (dd_liecEntities data_clte = new dd_liecEntities())
                {
                    var i_clte = (from t_clte in data_clte.tbl_pfe()
                                  join i_tu in data_clte.inf_mrp_concreto on t_clte.no_muesra equals i_tu.no_muesra
                                  join i_ec in data_clte.inf_conc_ec on i_tu.id_mrp_concreto equals i_ec.id_mrp_concreto
                                  where i_ec.fecha_ensaye == dt_dmuest
                                  select new
                                  {
                                      t_clte.no_muesra,
                                      t_clte.uno,

                                      t_clte.tres,

                                      t_clte.siete,

                                      t_clte.catorce,

                                      t_clte.vienteocho,

                                      t_clte.cero,

                                      t_clte.fecha_colado,
                                  }).ToList();

                    gv_ensa_comp.DataSource = i_clte;
                    gv_ensa_comp.DataBind();
                    gv_ensa_comp.Visible = true;

                    //if (i_clte[0].fne == 1)
                    //{
                    //    div_ecb.Visible = false;
                    //}
                    //else
                    //{
                    //    div_ecb.Visible = true;
                    //}
                }
            }
            catch
            {
                gv_clte_obras.Visible = false;
            }
        }

        #endregion ensayo
    }
}