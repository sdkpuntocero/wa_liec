using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wa_liec
{
    public partial class pnl_capt : System.Web.UI.Page
    {
        public static int int_clte, int_clte_obras, int_pnlID, int_rppc, int_idperf;
        public static string str_clte, str_usr_oper;
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

            i_agregar_rppc.Attributes["style"] = "color:white";
            i_editar_rppc.Attributes["style"] = "color:white";

            limp_txt_rppc();

            pnl_clte.Visible = false;
            up_clte.Update();
            pnl_clte_obras.Visible = false;
            up_clte_obras.Update();

            pnl_rppc.Visible = true;
            up_rppc.Update();
            pnl_ensa_comp.Visible = false;
            up_ensa_comp.Update();
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
                                Mensaje("Datos de cliente-obra agregados con éxito.");
                            }
                            else
                            {
                                limp_txt_clte();
                                rfv_rs.Enabled = false;
                                rfv_callenum_clte.Enabled = false;
                                rfv_cp_clte.Enabled = false;
                                rfv_colonia_clte.Enabled = false;
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
                    dt_fcol = DateTime.Parse(fcol_rppc.Text);
                    lbl_1_rppc.Text = dt_fcol.AddDays(1).ToShortDateString();
                }
            }
            else
            {
                chkb_1_rppc.Checked = false;
                lbl_1_rppc.Text = null;
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
                    dt_fcol = DateTime.Parse(fcol_rppc.Text);
                    lbl_3_rppc.Text = dt_fcol.AddDays(3).ToShortDateString();
                }
            }
            else
            {
                chkb_3_rppc.Checked = false;
                lbl_3_rppc.Text = null;
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
                    dt_fcol = DateTime.Parse(fcol_rppc.Text);
                    lbl_7_rppc.Text = dt_fcol.AddDays(7).ToShortDateString();
                }
            }
            else
            {
                chkb_7_rppc.Checked = false;
                lbl_7_rppc.Text = null;
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
                    dt_fcol = DateTime.Parse(fcol_rppc.Text);
                    lbl_14_rppc.Text = dt_fcol.AddDays(7).ToShortDateString();
                }
            }
            else
            {
                chkb_14_rppc.Checked = false;
                lbl_14_rppc.Text = null;
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
                    dt_fcol = DateTime.Parse(fcol_rppc.Text);
                    lbl_28_rppc.Text = dt_fcol.AddDays(7).ToShortDateString();
                }
            }
            else
            {
                chkb_28_rppc.Checked = false;
                lbl_28_rppc.Text = null;
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
                    if (string.IsNullOrEmpty(fotro_rppc.Text))

                    {
                    }
                    else
                    {
                        dt_fcol = DateTime.Parse(fcol_rppc.Text);
                        int int_fotro_rppc = int.Parse(fotro_rppc.Text);
                        lbl_otro_rppc.Text = dt_fcol.AddDays(int_fotro_rppc).ToShortDateString();
                    }
                }
            }
            else
            {
                chkb_otro_rppc.Checked = false;
                fotro_rppc.Text = null;
                lbl_otro_rppc.Text = null;
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
            string str_clte_obra;
            Guid int_clteobra;

            foreach (GridViewRow row in gv_rppc.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chk_rppc") as CheckBox);
                    if (chkRow.Checked)
                    {
                        row.BackColor = Color.FromArgb(227, 76, 14);
                        str_clte_obra = row.Cells[1].Text;

                        using (dd_liecEntities edm_clte = new dd_liecEntities())
                        {
                            var i_clte = (from c in edm_clte.inf_mrp_concreto
                                          where c.no_muesra == str_clte_obra
                                          select c).FirstOrDefault();

                            int_clteobra = i_clte.id_mrp_concreto;
                        }

                        using (dd_liecEntities edm_clte = new dd_liecEntities())
                        {
                            var i_clte = (from t_clte in edm_clte.inf_mrp_concreto
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
                                              t_clte.id_sit_concreto
                                          }).FirstOrDefault();
                            DateTime f_colad = Convert.ToDateTime(i_clte.fecha_colado);
                            DateTime f_recib = Convert.ToDateTime(i_clte.fecha_colado);

                            ddl_tesp_rppc.SelectedValue = i_clte.id_tipo_especimen.ToString();

                            ddl_tconc_rppc.SelectedValue = i_clte.id_tipo_concreto.ToString();

                            ddl_sit_rppc.SelectedValue = i_clte.id_sit_concreto.ToString();

                            nmue_rppc.Text = i_clte.no_muesra;
                            fcol_rppc.Text = f_colad.ToString("yyyy-MM-dd");
                            frec_rppc.Text = f_recib.ToString("yyyy-MM-dd");
                            entrega_rppc.Text = i_clte.recibe;
                            recibe_rppc.Text = i_clte.entrega;
                            r_rppc.Text = i_clte.presion.ToString();
                        }
                    }
                    else
                    {
                        row.BackColor = Color.White;
                    }
                }
            }
        }

        protected void ddl_rppc_est_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void btn_agregar_ensa_comp_Click(object sender, EventArgs e)
        {
        }

        protected void btn_editar_ensa_comp_Click(object sender, EventArgs e)
        {
        }

        protected void chkb_desacensa_comp_CheckedChanged(object sender, EventArgs e)
        {
        }

        protected void gv_ensa_comp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void gv_ensa_comp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
        }

        protected void chk_ensa_comp_CheckedChanged(object sender, EventArgs e)
        {
        }

        protected void btn_guardar_ensa_comp_Click(object sender, EventArgs e)
        {
        }

        protected void btn_editar_rppc_Click(object sender, EventArgs e)
        {
            int_rppc = 2;
            div_rppc.Visible = true;

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
                    var i_clte = (from t_clte in data_clte.inf_clte_obras
                                  where t_clte.clave_obra == n_rub
                                  select t_clte).FirstOrDefault();

                    guid_cltef = i_clte.id_clte_obras;
                }

                using (dd_liecEntities data_clte = new dd_liecEntities())
                {
                    var i_clte = (from t_clte in data_clte.inf_mrp_concreto
                                  where t_clte.id_clte_obras == guid_cltef
                                  select new
                                  {
                                      t_clte.no_muesra,
                                      t_clte.fecha_registro
                                  }).ToList();

                    gv_rppc.DataSource = i_clte;
                    gv_rppc.DataBind();
                    gv_rppc.Visible = true;
                }

                pnl_ensa_comp.Visible = true;
                up_ensa_comp.Update();
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
            rfv_fotro_rppc.Enabled = false;

            chkb_1_rppc.Checked = false;
            chkb_3_rppc.Checked = false;
            chkb_7_rppc.Checked = false;
            chkb_14_rppc.Checked = false;
            chkb_28_rppc.Checked = false;
            chkb_otro_rppc.Checked = false;

            lbl_1_rppc.Text = null;
            lbl_3_rppc.Text = null;
            lbl_7_rppc.Text = null;
            lbl_14_rppc.Text = null;
            lbl_28_rppc.Text = null;
            lbl_otro_rppc.Text = null;
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

            txt_buscar_rppc.Text = null;
            gv_rppc.Visible = false;

            nmue_rppc.Text = null;
            fcol_rppc.Text = null;
            frec_rppc.Text = null;
            entrega_rppc.Text = null;
            recibe_rppc.Text = null;
            r_rppc.Text = null;

            fotro_rppc.Text = null;
            chkb_1_rppc.Checked = false;
            chkb_3_rppc.Checked = false;
            chkb_7_rppc.Checked = false;
            chkb_14_rppc.Checked = false;
            chkb_28_rppc.Checked = false;
            chkb_otro_rppc.Checked = false;
            lbl_1_rppc.Text = null;
            lbl_3_rppc.Text = null;
            lbl_7_rppc.Text = null;
            lbl_14_rppc.Text = null;
            lbl_28_rppc.Text = null;
            lbl_otro_rppc.Text = null;
        }

        protected void btn_guardar_rppc_Click(object sender, EventArgs e)
        {
            if (int_rppc == 0)
            {
                Mensaje("Favor de seleccionar una acción");
            }
            else
            {
                try
                {
                    Guid guis_usr_capt = Guid.Parse("0298DF9A-4DBF-4EE5-84F7-1E7E519BE4F9");
                    Guid gui_nrppc;
                    string str_nomues_rppc, entr_rppc, recib_rppc;
                    DateTime fcolado_rrpc, fente_rrpc, f1_rppc, f3_rppc, f7_rppc, f14, rppc, f28_rppc, falt;
                    int int_tesp_rppc, int_tcont_rrp, int_sit_rppc, presi_rppc, int_co;

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

                    string n_rub;
                    Char char_s = '|';
                    string d_rub = txt_buscar_rppc.Text.Trim();
                    String[] de_rub = d_rub.Trim().Split(char_s);
                    n_rub = de_rub[0].Trim();

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
                                    id_usuario = guis_usr_capt,
                                    fecha_registro = DateTime.Now,
                                    id_emp = guid_emp,
                                    id_clte_obras = int_co
                                };

                                m_clte.inf_mrp_concreto.Add(i_clte);
                                m_clte.SaveChanges();
                            }

                            limp_txt_clte_obras();

                            rfv_clte_clave_obra.Enabled = false;
                            rfv_clte_desc_obra.Enabled = false;
                            rfv_clte_tservicio.Enabled = false;
                            rfv_clte_coordinador.Enabled = false;
                            rfv_clte_contobra.Enabled = false;
                            Mensaje("Datos de cliente-obra-muestra agregados con éxito.");
                        }
                        else
                        {
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

                                rfv_clte_clave_obra.Enabled = false;
                                rfv_clte_desc_obra.Enabled = false;
                                rfv_clte_tservicio.Enabled = false;
                                rfv_clte_coordinador.Enabled = false;
                                rfv_clte_contobra.Enabled = false;
                                Mensaje("Datos de cliente-obra actualizados con éxito.");
                            }
                            else
                            {
                                limp_txt_clte();
                                rfv_rs.Enabled = false;
                                rfv_callenum_clte.Enabled = false;
                                rfv_cp_clte.Enabled = false;
                                rfv_colonia_clte.Enabled = false;
                                Mensaje("Obra existe en la base de datos, favor de revisar.");
                            }
                        }
                    }
                }
                catch
                {
                    int_rppc = 0;
                    i_agregar_rppc.Attributes["style"] = "color:white";
                    i_editar_rppc.Attributes["style"] = "color:white";
                    Mensaje("Favor de seleccionar una acción");
                }
            }
        }

        #endregion rppc
    }
}