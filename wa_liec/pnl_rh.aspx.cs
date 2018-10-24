using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wa_liec
{
    public partial class pnl_rh : System.Web.UI.Page
    {
        private static int int_areas, int_pnlID, int_usr, int_idperf;

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

        #region funciones

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
                    string query = "SELECT [cod_area],[desc_areas] FROM [dd_liec].[dbo].[fact_areas]  WHERE [desc_areas] LIKE '" + str_fclte + "%' ";
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
                    string query = "SELECT [desc_areas],[cod_area] FROM [dd_liec].[dbo].[fact_areas]  WHERE [desc_areas] LIKE '" + str_fclte + "%' ";
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
                    string query = @"SELECT         nom_completo,cod_usr
        FROM            (SELECT        cod_usr,  CONCAT(nombres, ' ', a_paterno, ' ', a_materno)  AS nom_completo
                                  FROM            inf_usuarios) AS DEV_USR WHERE nom_completo LIKE '" + str_fclte + "%' ";
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

            return columnData;
        }

        private void Mensaje(string contenido)
        {
            lblModalTitle.Text = "LIEC";
            lblModalBody.Text = contenido;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();
        }

        #endregion funciones

        #region usuarios

        protected void lkb_usr_Click(object sender, EventArgs e)
        {
            int_usr = 0;
            int_pnlID = 3;

            i_usr.Attributes["style"] = "color:#104D8d";
            lbl_usr.Attributes["style"] = "color:#104D8d";

            i_agregar_usrs.Attributes["style"] = "color:white";
            i_editar_usrs.Attributes["style"] = "color:white";
            limp_txt_usr();
            pnl_usrs.Visible = true;
            div_busc_usr.Visible = false;
            up_usrs.Update();
        }

        protected void btn_agregar_usrs_Click(object sender, EventArgs e)
        {
            int_usr = 1;

            i_agregar_usrs.Attributes["style"] = "color:#E34C0E";
            i_editar_usrs.Attributes["style"] = "color:white";
            div_busc_usr.Visible = false;

            rfv_area_usr.Enabled = true;
            rfv_perfil_usr.Enabled = true;
            rfv_fnac_usr.Enabled = true;
            rfv_nombre_usr.Enabled = true;
            rfv_apaterno_usr.Enabled = true;
            rfv_amaterno_usr.Enabled = true;
            rfv_usr_coment.Enabled = false;
            rfv_callenum_usr.Enabled = false;
            rfv_col_usr.Enabled = false;
            gv_usrs.Visible = false;
            limp_txt_usr();
        }

        protected void btn_editar_usrs_Click(object sender, EventArgs e)
        {
            int_usr = 2;

            i_agregar_usrs.Attributes["style"] = "color:white";
            i_editar_usrs.Attributes["style"] = "color:#E34C0E";
            div_busc_usr.Visible = true;
            rfv_buscar_usrs.Enabled = true;
            rfv_area_usr.Enabled = false;
            rfv_perfil_usr.Enabled = false;
            rfv_fnac_usr.Enabled = false;
            rfv_nombre_usr.Enabled = false;
            rfv_apaterno_usr.Enabled = false;
            rfv_amaterno_usr.Enabled = false;
            rfv_callenum_usr.Enabled = false;
            rfv_cp_usr.Enabled = false;
            rfv_usr_coment.Enabled = false;
            rfv_callenum_usr.Enabled = false;
            rfv_col_usr.Enabled = false;
            gv_usrs.Visible = false;
            limp_txt_usr();
        }

        protected void btn_guardar_usrs_Click(object sender, EventArgs e)
        {
            if (int_usr == 0)
            {
                Mensaje("Favor de seleccionar una acción");
            }
            else
            {
                //try
                //{
                Guid guid_area, guid_usrs, guid_fusrs;
                int idper_usr, idcol_usr;
                string cod_usr, nom_usr, apater_usr, amater_usr, e_user, tel_usr, movil_usr, callenum_usr, cp_usr;
                DateTime fnac_user;

                guid_usrs = Guid.NewGuid();
                guid_area = Guid.Parse(ddl_area_usr.SelectedValue);
                idper_usr = int.Parse(ddl_perfil_usr.SelectedValue);
                e_user = txt_email_usr.Text.Trim();
                nom_usr = txt_nombre_usr.Text.Trim().ToUpper();
                apater_usr = txt_apaterno_usr.Text.Trim().ToUpper();
                amater_usr = txt_amaterno_usr.Text.Trim().ToUpper();
                fnac_user = DateTime.Parse(txt_fnac_usr.Text);
                tel_usr = txt_tel_usr.Text.Trim();
                callenum_usr = txt_callenum_usr.Text.Trim().ToUpper();
                cp_usr = txt_cp_usr.Text.Trim();
                idcol_usr = int.Parse(ddl_col_usr.SelectedValue);

                if (int_usr == 2)
                {
                    int int_ddl, int_f_clte = 0;
                    int int_estatusID = 0;
                    string str_fclte = null;
                    string v_usrs = null;
                    foreach (GridViewRow row in gv_usrs.Rows)
                    {
                        // int key = (int)GridView1.DataKeys[row.RowIndex].Value;
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[0].FindControl("chk_usrs") as CheckBox);
                            if (chkRow.Checked)
                            {
                                int_f_clte = int_f_clte + 1;
                                str_fclte = row.Cells[1].Text;
                                v_usrs = row.Cells[2].Text;
                                DropDownList dl = (DropDownList)row.FindControl("ddl_usrs_estatus");

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
                                break;
                            }
                        }
                    }

                    if (int_f_clte == 0)
                    {
                        Mensaje("Favor de seleccionar una área.");
                    }
                    else
                    {
                        using (var m_usrs = new dd_liecEntities())
                        {
                            var i_usrs = (from c in m_usrs.inf_usuarios
                                          where c.cod_usr == str_fclte

                                          select c).FirstOrDefault();

                            i_usrs.id_area = guid_area;
                            i_usrs.id_est_usr = int_estatusID;
                            i_usrs.id_perfil = idper_usr;
                            i_usrs.nombres = nom_usr;
                            i_usrs.a_paterno = apater_usr;
                            i_usrs.a_materno = amater_usr;
                            m_usrs.SaveChanges();

                            guid_fusrs = i_usrs.id_usuario;

                            var i_usr_cont = (from c in m_usrs.inf_cont_usr
                                              where c.id_usuario == guid_fusrs
                                              select c).FirstOrDefault();

                            i_usr_cont.telefono = tel_usr;
                            i_usr_cont.email = e_user;
                            i_usr_cont.callenum = callenum_usr;
                            i_usr_cont.d_codigo = cp_usr;
                            i_usr_cont.id_asenta_cpcons = idcol_usr;

                            m_usrs.SaveChanges();
                        }
                        gv_usrs.Visible = false;
                        rfv_buscar_usrs.Enabled = false;
                        rfv_usr_coment.Enabled = false;
                        limp_txt_usr();
                        Mensaje("Datos de usuario actualizados con éxito.");
                    }
                }
                else if (int_usr == 1)
                {
                    using (dd_liecEntities edm_fusr = new dd_liecEntities())
                    {
                        var i_fusr = (from c in edm_fusr.inf_usuarios
                                      where c.nombres == nom_usr
                                      where c.a_paterno == apater_usr
                                      where c.a_materno == amater_usr
                                      select c).ToList();

                        if (i_fusr.Count == 0)
                        {
                            var i_usr = (from c in edm_fusr.inf_usuarios
                                         select c).ToList();

                            if (i_usr.Count == 0)
                            {
                                cod_usr = "LIEC-USR" + string.Format("{0:000}", (object)(i_usr.Count + 1));

                                var a_usr = new inf_usuarios

                                {
                                    id_usuario = guid_usrs,
                                    id_est_usr = 1,
                                    id_area = guid_area,
                                    id_perfil = idper_usr,
                                    cod_usr = cod_usr,
                                    nombres = nom_usr,
                                    a_paterno = apater_usr,
                                    a_materno = amater_usr,
                                    fecha_nacimiento = fnac_user,
                                    id_emp = guid_emp,
                                    fecha_registro = DateTime.Now
                                };

                                var a_usr_cot = new inf_cont_usr

                                {
                                    callenum = callenum_usr,
                                    d_codigo = cp_usr,
                                    id_asenta_cpcons = idcol_usr,
                                    telefono = tel_usr,
                                    email = e_user,
                                    id_usuario = guid_usrs
                                };

                                edm_fusr.inf_usuarios.Add(a_usr);
                                edm_fusr.inf_cont_usr.Add(a_usr_cot);

                                edm_fusr.SaveChanges();

                                limp_txt_usr();
                                Mensaje("Datos de usuario agregados con éxito.");
                            }
                            else
                            {
                                cod_usr = "LIEC-USR" + string.Format("{0:000}", (object)(i_usr.Count + 1));

                                var a_usr = new inf_usuarios

                                {
                                    id_usuario = guid_usrs,
                                    id_est_usr = 1,
                                    id_area = guid_area,
                                    id_perfil = idper_usr,
                                    cod_usr = cod_usr,
                                    nombres = nom_usr,
                                    a_paterno = apater_usr,
                                    a_materno = amater_usr,
                                    fecha_nacimiento = fnac_user,
                                    id_emp = guid_emp,
                                    fecha_registro = DateTime.Now
                                };

                                var a_usr_cot = new inf_cont_usr

                                {
                                    callenum = callenum_usr,
                                    d_codigo = cp_usr,
                                    id_asenta_cpcons = idcol_usr,
                                    telefono = tel_usr,
                                    email = e_user,
                                    id_usuario = guid_usrs
                                };

                                edm_fusr.inf_usuarios.Add(a_usr);
                                edm_fusr.inf_cont_usr.Add(a_usr_cot);
                                edm_fusr.SaveChanges();

                                limp_txt_usr();
                                Mensaje("Datos de usuario agregados con éxito.");
                            }
                        }
                        else
                        {
                            limp_txt_usr();
                            rfv_usr_coment.Enabled = false;
                            Mensaje("Usuario existe en la base de datos, favor de revisar.");
                        }
                    }
                }

                //}
                //catch
                //{ }
            }
        }

        protected void gv_usrs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_usrs.PageIndex = e.NewPageIndex;
            string str_clte = txt_buscar_usrs.Text.ToUpper().Trim();
            try
            {
                if (str_clte == "TODO")
                {
                    using (dd_liecEntities data_areas = new dd_liecEntities())
                    {
                        var i_areas = (from t_areas in data_areas.inf_usuarios
                                       select new
                                       {
                                           t_areas.cod_usr,
                                           nom_cmpleto = t_areas.nombres + " " + t_areas.a_paterno + " " + t_areas.a_paterno,
                                           t_areas.email,
                                           t_areas.fecha_registro
                                       }).OrderBy(x => x.cod_usr).ToList();

                        gv_usrs.DataSource = i_areas;
                        gv_usrs.DataBind();
                        gv_usrs.Visible = true;
                    }
                }
                else
                {
                    string n_rub;
                    Char char_s = '|';
                    string d_rub = txt_buscar_usrs.Text.Trim();
                    String[] de_rub = d_rub.Trim().Split(char_s);
                    n_rub = de_rub[1].Trim();

                    using (dd_liecEntities data_areas = new dd_liecEntities())
                    {
                        var i_areas = (from t_areas in data_areas.inf_usuarios
                                       where t_areas.cod_usr == n_rub
                                       select new
                                       {
                                           t_areas.cod_usr,
                                           nom_cmpleto = t_areas.nombres + " " + t_areas.a_paterno + " " + t_areas.a_paterno,
                                           t_areas.email,
                                           t_areas.fecha_registro
                                       }).OrderBy(x => x.cod_usr).ToList();

                        gv_usrs.DataSource = i_areas;
                        gv_usrs.DataBind();
                        gv_usrs.Visible = true;
                    }
                }
            }
            catch
            {
                txt_usr_coment.Text = null;
                txt_buscar_usrs.Text = null;
                Mensaje("Área no existe, favor de revisar.");
            }
        }

        protected void gv_usrs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string str_clteID = e.Row.Cells[1].Text;
                int int_estatusID;

                using (dd_liecEntities data_clte = new dd_liecEntities())
                {
                    var i_clte = (from t_clte in data_clte.inf_usuarios
                                  where t_clte.cod_usr == str_clteID
                                  select new
                                  {
                                      t_clte.id_est_usr,
                                  }).FirstOrDefault();

                    int_estatusID = int.Parse(i_clte.id_est_usr.ToString());
                }

                DropDownList DropDownList1 = (e.Row.FindControl("ddl_usrs_estatus") as DropDownList);

                using (dd_liecEntities db_sepomex = new dd_liecEntities())
                {
                    var tbl_sepomex = (from c in db_sepomex.fact_estatus
                                       select c).ToList();

                    DropDownList1.DataSource = tbl_sepomex;

                    DropDownList1.DataTextField = "desc_est_usr";
                    DropDownList1.DataValueField = "id_est_usr";
                    DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
                    DropDownList1.SelectedValue = int_estatusID.ToString();
                }
            }
        }

        protected void chk_usrs_CheckedChanged(object sender, EventArgs e)
        {
            string str_rub;
            Guid guid_rub;
    
            foreach (GridViewRow row in gv_usrs.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chk_usrs") as CheckBox);
                    if (chkRow.Checked)
                    {
                        row.BackColor = Color.FromArgb(227, 76, 14);
                        str_rub = row.Cells[1].Text;

                        using (dd_liecEntities edm_rub = new dd_liecEntities())
                        {
                            var i_rub = (from i_u in edm_rub.inf_usuarios

                                         where i_u.cod_usr == str_rub
                                         select i_u).FirstOrDefault();

                            guid_rub = i_rub.id_usuario;

                            var f_rub = (from i_u in edm_rub.inf_usuarios
                                         join i_tu in edm_rub.inf_cont_usr on i_u.id_usuario equals i_tu.id_usuario
                                         where i_u.id_usuario == guid_rub
                                         select new
                                         {
                                             i_u.id_area,
                                             i_u.id_perfil,
                                             i_u.nombres,
                                             i_u.a_paterno,
                                             i_u.a_materno,
                                             i_u.fecha_nacimiento,
                                             i_tu.telefono,
                                             i_tu.email,
                                             i_tu.callenum,
                                             i_tu.d_codigo,
                                             i_tu.id_asenta_cpcons
                                         }).FirstOrDefault();

                            DateTime f_nac = Convert.ToDateTime(f_rub.fecha_nacimiento);

                            ddl_area_usr.SelectedValue = f_rub.id_area.ToString();
                            ddl_perfil_usr.SelectedValue = f_rub.id_perfil.ToString();
                            txt_nombre_usr.Text = f_rub.nombres;
                            txt_apaterno_usr.Text = f_rub.a_paterno;
                            txt_amaterno_usr.Text = f_rub.a_materno;
                            txt_fnac_usr.Text = f_nac.ToString("yyyy-MM-dd");
                            txt_tel_usr.Text = f_rub.telefono;
                            txt_email_usr.Text = f_rub.email;
                            txt_callenum_usr.Text = f_rub.callenum;
                            txt_cp_usr.Text = f_rub.d_codigo;
                            try
                            {
                                using (dd_liecEntities db_sepomex = new dd_liecEntities())
                                {
                                    var tbl_sepomex = (from c in db_sepomex.inf_sepomex
                                                       where c.d_codigo == f_rub.d_codigo
                                                       select c).ToList();

                                    ddl_col_usr.DataSource = tbl_sepomex;
                                    ddl_col_usr.DataTextField = "d_asenta";
                                    ddl_col_usr.DataValueField = "id_asenta_cpcons";
                                    ddl_col_usr.DataBind();

                                    ddl_col_usr.SelectedValue = f_rub.id_asenta_cpcons.ToString();
                                    txt_municipio_usr.Text = tbl_sepomex[0].d_mnpio;
                                    txt_estado_usr.Text = tbl_sepomex[0].d_estado;
                                }
                            }
                            catch
                            {
                                ddl_col_usr.Items.Clear();
                                ddl_col_usr.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
                            }

                            txt_usr_coment.Text = null;
                        }
                    }
                    else
                    {
                        row.BackColor = Color.White;
                    }
                }
            }
        }

        protected void ddl_usrs_estatus_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void btn_buscar_usrs_Click(object sender, EventArgs e)
        {
            gv_usrs.Visible = false;
            string str_clte = txt_buscar_usrs.Text.ToUpper().Trim();
            try
            {
                if (str_clte == "TODO")
                {
                    limp_txt_usr();
                    using (dd_liecEntities data_areas = new dd_liecEntities())
                    {
                        var i_areas = (from t_areas in data_areas.inf_usuarios
                                       select new
                                       {
                                           t_areas.cod_usr,
                                           nom_cmpleto = t_areas.nombres + " " + t_areas.a_paterno + " " + t_areas.a_materno,
                                           t_areas.email,
                                           t_areas.fecha_registro
                                       }).OrderBy(x => x.cod_usr).ToList();

                        if (i_areas.Count == 0)
                        {
                            gv_usrs.Visible = false;
                            limp_txt_usr();
                            Mensaje("Sin registros, favor de agregar.");
                        }
                        else
                        {
                            gv_usrs.DataSource = i_areas;
                            gv_usrs.DataBind();
                            gv_usrs.Visible = true;
                        }
                    }
                }
                else
                {
                    string n_rub;
                    Char char_s = '|';
                    string d_rub = txt_buscar_usrs.Text.Trim();
                    String[] de_rub = d_rub.Trim().Split(char_s);
                    n_rub = de_rub[1].Trim();
                    limp_txt_usr();
                    using (dd_liecEntities data_areas = new dd_liecEntities())
                    {
                        var i_areas = (from t_areas in data_areas.inf_usuarios
                                       where t_areas.cod_usr == n_rub
                                       select new
                                       {
                                           t_areas.cod_usr,
                                           nom_cmpleto = t_areas.nombres + " " + t_areas.a_paterno + " " + t_areas.a_materno,
                                           t_areas.email,
                                           t_areas.fecha_registro
                                       }).OrderBy(x => x.cod_usr).ToList();

                        gv_usrs.DataSource = i_areas;
                        gv_usrs.DataBind();
                        gv_usrs.Visible = true;
                    }
                }
            }
            catch
            {
                gv_usrs.Visible = false;
                limp_txt_usr();
                Mensaje("Usuario no existe, favor de revisar.");
            }
        }

        protected void btn_cp_usr_Click(object sender, EventArgs e)
        {
            string str_codcp = txt_cp_usr.Text.Trim();

            using (dd_liecEntities db_sepomex = new dd_liecEntities())
            {
                var tbl_sepomex = (from c in db_sepomex.inf_sepomex
                                   where c.d_codigo == str_codcp
                                   select c).ToList();

                ddl_col_usr.DataSource = tbl_sepomex;
                ddl_col_usr.DataTextField = "d_asenta";
                ddl_col_usr.DataValueField = "id_asenta_cpcons";
                ddl_col_usr.DataBind();

                if (tbl_sepomex.Count == 1)
                {
                    txt_municipio_usr.Text = tbl_sepomex[0].d_mnpio;
                    txt_estado_usr.Text = tbl_sepomex[0].d_estado;

           
                }
                if (tbl_sepomex.Count > 1)
                {
                    ddl_col_usr.Items.Insert(0, new ListItem("SELECCIONAR", "0"));

                    txt_municipio_usr.Text = tbl_sepomex[0].d_mnpio;
                    txt_estado_usr.Text = tbl_sepomex[0].d_estado;

     
                }
                else if (tbl_sepomex.Count == 0)
                {
                    ddl_col_usr.Items.Clear();
                    ddl_col_usr.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
                    txt_municipio_usr.Text = null;
                    txt_estado_usr.Text = null;
                    txt_cp_usr.Focus();
                }
            }
            up_usrs.Update();
        }

        protected void chkb_desactivar_usrs_CheckedChanged(object sender, EventArgs e)
        {
            rfv_buscar_usrs.Enabled = false;
            rfv_area_usr.Enabled = false;
            rfv_perfil_usr.Enabled = false;
            rfv_nombre_usr.Enabled = false;
            rfv_apaterno_usr.Enabled = false;
            rfv_amaterno_usr.Enabled = false;
            rfv_fnac_usr.Enabled = false;
            rfv_usr_coment.Enabled = false;
            rfv_callenum_usr.Enabled = false;
            rfv_cp_usr.Enabled = false;
            rfv_col_usr.Enabled = false;
        }

        private void limp_txt_usr()
        {
            using (dd_liecEntities m_area = new dd_liecEntities())
            {
                var i_area = (from c in m_area.fact_areas
                              select c).ToList();

                ddl_area_usr.Items.Clear();

                ddl_area_usr.DataSource = i_area;
                ddl_area_usr.DataTextField = "desc_areas";
                ddl_area_usr.DataValueField = "id_area";
                ddl_area_usr.DataBind();

                ddl_area_usr.Items.Insert(0, new ListItem("SELECCIONAR", "0"));

                ddl_perfil_usr.Items.Clear();

                var i_perfil = (from c in m_area.fact_perfil
                                select c).ToList();

                ddl_perfil_usr.DataSource = i_perfil;
                ddl_perfil_usr.DataTextField = "desc_perfil";
                ddl_perfil_usr.DataValueField = "id_perfil";
                ddl_perfil_usr.DataBind();

                ddl_perfil_usr.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
            }
            ddl_col_usr.Items.Clear();
            ddl_col_usr.Items.Insert(0, new ListItem("SELECCIONAR", "0"));

            
            txt_nombre_usr.Text = null;
            txt_apaterno_usr.Text = null;
            txt_amaterno_usr.Text = null;
            txt_tel_usr.Text = null;
            txt_email_usr.Text = null;
            txt_fnac_usr.Text = null;
            txt_callenum_usr.Text = null;
            txt_cp_usr.Text = null;
            txt_municipio_usr.Text = null;
            txt_estado_usr.Text = null;
        }

        #endregion usuarios
    }
}