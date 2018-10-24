using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wa_liec
{
    public partial class pnl_desa_tec : System.Web.UI.Page
    {
        private static int int_areas, int_pnlID, int_e_env, int_usr;
        private static Guid guid_emp = Guid.Parse("d8a03556-6791-45f3-babe-ecf267b865f1");

        protected void Page_Load(object sender, EventArgs e)
        {
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
                    string query = "SELECT [cod_area],[desc_areas] FROM [lab_liec].[dbo].[fact_areas]  WHERE [desc_areas] LIKE '" + str_fclte + "%' ";
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
                    string query = "SELECT [desc_areas],[cod_area] FROM [lab_liec].[dbo].[fact_areas]  WHERE [desc_areas] LIKE '" + str_fclte + "%' ";
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

        #region menu
        protected void lkb_areas_Click(object sender, EventArgs e)
        {
            int_areas = 0;
            int_pnlID = 2;
            i_areas.Attributes["style"] = "color:#104D8d";
            lbl_areas.Attributes["style"] = "color:#104D8d";
            i_e_envio.Attributes["style"] = "color:#E34C0E";
            lbl_e_envio.Attributes["style"] = "color:#104D8dE34C0E";

            i_agregar_areas.Attributes["style"] = "color:white";
            i_editar_areas.Attributes["style"] = "color:white";

            pnl_areas.Visible = true;
            up_areas.Update();

            limp_txt_area();
            div_busc_clt.Visible = false;
            gv_areas.Visible = false;
            pnl_usrs.Visible = false;
            up_usrs.Update();
            pnl_e_env.Visible = false;
            up_e_env.Update();
        }

        protected void lkb_e_env_Click(object sender, EventArgs e)
        {
            int_e_env = 0;
            int_pnlID = 0;

            i_areas.Attributes["style"] = "color:#E34C0E";
            lbl_areas.Attributes["style"] = "color:#E34C0E";

            i_e_envio.Attributes["style"] = "color:#104D8d";
            lbl_e_envio.Attributes["style"] = "color:#104D8d";



            i_agregar_e_env.Attributes["style"] = "color:white";
            i_editar_e_env.Attributes["style"] = "color:white";



            pnl_areas.Visible = false;
            up_areas.Update();

            limpia_e_env();
            div_busc_e_env.Visible = false;
            gv_e_env.Visible = false;

            pnl_usrs.Visible = false;
            up_usrs.Update();

            pnl_e_env.Visible = true;
            up_e_env.Update();
        }

        protected void lkb_e_rec_Click(object sender, EventArgs e)
        {

        }

        protected void lkb_usr_Click(object sender, EventArgs e)
        {
            int_usr = 0;
            int_pnlID = 3;

            i_usr.Attributes["style"] = "color:#104D8d";
            lbl_usr.Attributes["style"] = "color:#104D8d";

            i_agregar_usrs.Attributes["style"] = "color:white";
            i_editar_usrs.Attributes["style"] = "color:white";
            limp_txt_usr();
            div_busc_usr.Visible = false;

            pnl_areas.Visible = false;
            up_areas.Update();

            limpia_e_env();
            div_busc_e_env.Visible = false;
            gv_e_env.Visible = false;


            pnl_e_env.Visible = false;
            up_e_env.Update();

            pnl_usrs.Visible = true;
            up_usrs.Update();
        }
        #endregion

        #region areas



        protected void btn_guardar_areas_Click(object sender, EventArgs e)
        {
            if (int_areas == 0)
            {
                Mensaje("Favor de seleccionar una acción");
            }
            else
            {
                try
                {
                    Guid guid_area = Guid.NewGuid();

                    string str_area = txt_areas_coment.Text.ToUpper().Trim();
                    string str_codigo_area;

                    if (int_areas == 1)
                    {
                        using (dd_liecEntities edm_nclte = new dd_liecEntities())
                        {
                            var i_nclte = (from c in edm_nclte.fact_areas
                                           where c.desc_areas == str_area
                                           select c).ToList();

                            if (i_nclte.Count == 0)
                            {
                                using (dd_liecEntities edm_fclte = new dd_liecEntities())
                                {
                                    var i_fclte = (from c in edm_fclte.fact_areas
                                                   select c).ToList();

                                    if (i_fclte.Count == 0)
                                    {
                                        str_codigo_area = "LIEC-AREA" + string.Format("{0:000}", (object)(i_nclte.Count + 1));

                                        using (var m_clte = new dd_liecEntities())
                                        {
                                            var i_clte = new fact_areas

                                            {
                                                id_area = guid_area,
                                                cod_area = str_codigo_area,
                                                id_est_areas = 1,
                                                desc_areas = str_area,
                                                id_emp = guid_emp,
                                                fecha_registro = DateTime.Now
                                            };

                                            m_clte.fact_areas.Add(i_clte);
                                            m_clte.SaveChanges();
                                        }
                                        limp_txt_area();
                                        Mensaje("Datos de área agregados con éxito.");
                                    }
                                    else
                                    {
                                        str_codigo_area = "LIEC-AREA" + string.Format("{0:000}", (object)(i_fclte.Count + 1));

                                        using (var m_clte = new dd_liecEntities())
                                        {
                                            var i_clte = new fact_areas

                                            {
                                                id_area = guid_area,
                                                cod_area = str_codigo_area,
                                                id_est_areas = 1,
                                                desc_areas = str_area,
                                                id_emp = guid_emp,
                                                fecha_registro = DateTime.Now
                                            };

                                            m_clte.fact_areas.Add(i_clte);
                                            m_clte.SaveChanges();
                                        }
                                        limp_txt_area();

                                        Mensaje("Datos de área agregados con éxito.");
                                    }
                                }
                            }
                            else
                            {
                                limp_txt_area();
                                rfv_areas_coment.Enabled = false;
                                Mensaje("Área existe en la base de datos, favor de revisar.");
                            }
                        }
                    }
                    else if (int_areas == 2)
                    {
                        int int_ddl, int_f_clte = 0;
                        int int_estatusID = 0;
                        string str_fclte = null;
                        string v_area = null;
                        foreach (GridViewRow row in gv_areas.Rows)
                        {
                            // int key = (int)GridView1.DataKeys[row.RowIndex].Value;
                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                CheckBox chkRow = (row.Cells[0].FindControl("chk_areas") as CheckBox);
                                if (chkRow.Checked)
                                {
                                    int_f_clte = int_f_clte + 1;
                                    str_fclte = row.Cells[1].Text;
                                    v_area = row.Cells[2].Text;
                                    DropDownList dl = (DropDownList)row.FindControl("ddl_area_estatus");

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

                        if (int_estatusID == 0)
                        {
                            Mensaje("Favor de seleccionar una área.");
                        }
                        else
                        {
                            using (var m_clte = new dd_liecEntities())
                            {
                                var i_clte = (from c in m_clte.fact_areas
                                              where c.cod_area == str_fclte
                                              select c).FirstOrDefault();

                                if (str_area == i_clte.desc_areas)
                                {
                                    var i_area = (from c in m_clte.fact_areas
                                                  where c.cod_area == str_fclte
                                                  select c).FirstOrDefault();

                                    i_area.id_est_areas = int_estatusID;
                                    i_area.desc_areas = str_area;

                                    m_clte.SaveChanges();

                                    rfv_buscar_areas.Enabled = false;
                                    rfv_areas_coment.Enabled = false;
                                    limp_txt_area();
                                    Mensaje("Datos de área actualizados con éxito.");
                                }
                                else
                                {
                                    var i_nclte = (from c in m_clte.fact_areas
                                                   where c.desc_areas == str_area
                                                   select c).ToList();

                                    if (i_nclte.Count == 0)
                                    {
                                        var i_fareas = (from c in m_clte.fact_areas
                                                        where c.cod_area == str_fclte
                                                        select c).FirstOrDefault();

                                        i_fareas.id_est_areas = int_estatusID;
                                        i_fareas.desc_areas = str_area;

                                        m_clte.SaveChanges();

                                        rfv_buscar_areas.Enabled = false;
                                        rfv_areas_coment.Enabled = false;
                                        limp_txt_area();
                                        string str_clte = txt_buscar_areas.Text.ToUpper().Trim();
                                        try
                                        {
                                            if (str_clte == "TODOS")
                                            {
                                                using (dd_liecEntities data_areas = new dd_liecEntities())
                                                {
                                                    var i_areas = (from t_areas in data_areas.fact_areas
                                                                   join t_est in data_areas.fact_est_areas on t_areas.id_est_areas equals t_est.id_est_area

                                                                   select new
                                                                   {
                                                                       t_areas.cod_area,
                                                                       t_est.desc_est_area,
                                                                       t_areas.desc_areas,
                                                                       t_areas.fecha_registro
                                                                   }).OrderBy(x => x.cod_area).ToList();

                                                    gv_areas.DataSource = i_areas;
                                                    gv_areas.DataBind();
                                                    gv_areas.Visible = true;
                                                }
                                            }
                                            else
                                            {
                                                string n_rub;
                                                Char char_s = '|';
                                                string d_rub = txt_buscar_areas.Text.Trim();
                                                String[] de_rub = d_rub.Trim().Split(char_s);
                                                n_rub = de_rub[1].Trim();

                                                using (dd_liecEntities data_areas = new dd_liecEntities())
                                                {
                                                    var i_areas = (from t_areas in data_areas.fact_areas
                                                                   join t_est in data_areas.fact_est_areas on t_areas.id_est_areas equals t_est.id_est_area
                                                                   where t_areas.cod_area == n_rub

                                                                   select new
                                                                   {
                                                                       t_areas.cod_area,
                                                                       t_est.desc_est_area,
                                                                       t_areas.desc_areas,
                                                                       t_areas.fecha_registro
                                                                   }).OrderBy(x => x.cod_area).ToList();

                                                    gv_areas.DataSource = i_areas;
                                                    gv_areas.DataBind();
                                                    gv_areas.Visible = true;
                                                }
                                            }
                                        }
                                        catch
                                        {
                                            txt_areas_coment.Text = null;
                                            txt_buscar_areas.Text = null;
                                            Mensaje("Área no existe, favor de revisar.");
                                        }
                                        Mensaje("Datos de área actualizados con éxito.");
                                    }
                                    else
                                    {
                                        limp_txt_area();
                                        Mensaje("Área ya existe en la base de datos, favor de revisar.");
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                { }
            }
        }

        private void limp_txt_area()
        {
        }

        protected void btn_agregar_areas_Click(object sender, EventArgs e)

        {
            int_areas = 1;

            i_agregar_areas.Attributes["style"] = "color:#E34C0E";
            i_editar_areas.Attributes["style"] = "color:white";
            rfv_areas_coment.Enabled = true;
            rfv_buscar_areas.Enabled = false;
            div_busc_clt.Visible = false;
            gv_areas.Visible = false;
        }

        protected void btn_buscar_areas_Click(object sender, EventArgs e)
        {
            txt_areas_coment.Text = null;
            string str_clte = txt_buscar_areas.Text.ToUpper().Trim();
            try
            {
                if (str_clte == "TODOS")
                {
                    using (dd_liecEntities data_areas = new dd_liecEntities())
                    {
                        var i_areas = (from t_areas in data_areas.fact_areas
                                       join t_est in data_areas.fact_est_areas on t_areas.id_est_areas equals t_est.id_est_area

                                       select new
                                       {
                                           t_areas.cod_area,
                                           t_est.desc_est_area,
                                           t_areas.desc_areas,
                                           t_areas.fecha_registro
                                       }).OrderBy(x => x.cod_area).ToList();

                        gv_areas.DataSource = i_areas;
                        gv_areas.DataBind();
                        gv_areas.Visible = true;
                    }
                }
                else
                {
                    string n_rub;
                    Char char_s = '|';
                    string d_rub = txt_buscar_areas.Text.Trim();
                    String[] de_rub = d_rub.Trim().Split(char_s);
                    n_rub = de_rub[1].Trim();

                    using (dd_liecEntities data_areas = new dd_liecEntities())
                    {
                        var i_areas = (from t_areas in data_areas.fact_areas
                                       join t_est in data_areas.fact_est_areas on t_areas.id_est_areas equals t_est.id_est_area
                                       where t_areas.cod_area == n_rub

                                       select new
                                       {
                                           t_areas.cod_area,
                                           t_est.desc_est_area,
                                           t_areas.desc_areas,
                                           t_areas.fecha_registro
                                       }).OrderBy(x => x.cod_area).ToList();

                        gv_areas.DataSource = i_areas;
                        gv_areas.DataBind();
                        gv_areas.Visible = true;
                    }
                }
            }
            catch
            {
                txt_areas_coment.Text = null;
                txt_buscar_areas.Text = null;
                Mensaje("Área no existe, favor de revisar.");
            }
        }

        protected void chk_areas_CheckedChanged(object sender, EventArgs e)
        {
            string str_rub;
            Guid guid_rub;

            foreach (GridViewRow row in gv_areas.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chk_areas") as CheckBox);
                    if (chkRow.Checked)
                    {
                        row.BackColor = Color.FromArgb(227, 76, 14);
                        str_rub = row.Cells[1].Text;

                        using (dd_liecEntities edm_rub = new dd_liecEntities())
                        {
                            var i_rub = (from c in edm_rub.fact_areas
                                         where c.cod_area == str_rub
                                         select c).FirstOrDefault();

                            guid_rub = i_rub.id_area;

                            var f_rub = (from r in edm_rub.fact_areas
                                         where r.id_area == guid_rub
                                         select new
                                         {
                                             r.desc_areas,
                                         }).FirstOrDefault();

                            txt_areas_coment.Text = f_rub.desc_areas;
                        }
                    }
                    else
                    {
                        row.BackColor = Color.White;
                    }
                }
            }
        }

        protected void gv_areas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string str_clteID = e.Row.Cells[1].Text;
                int int_estatusID;

                using (dd_liecEntities data_clte = new dd_liecEntities())
                {
                    var i_clte = (from t_clte in data_clte.fact_areas
                                  where t_clte.cod_area == str_clteID
                                  select new
                                  {
                                      t_clte.id_est_areas,
                                  }).FirstOrDefault();

                    int_estatusID = int.Parse(i_clte.id_est_areas.ToString());
                }

                DropDownList DropDownList1 = (e.Row.FindControl("ddl_area_estatus") as DropDownList);

                using (dd_liecEntities db_sepomex = new dd_liecEntities())
                {
                    var tbl_sepomex = (from c in db_sepomex.fact_est_areas
                                       select c).ToList();

                    DropDownList1.DataSource = tbl_sepomex;

                    DropDownList1.DataTextField = "desc_est_area";
                    DropDownList1.DataValueField = "id_est_area";
                    DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
                    DropDownList1.SelectedValue = int_estatusID.ToString();
                }
            }
        }

        protected void gv_areas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_areas.PageIndex = e.NewPageIndex;
            string str_clte = txt_buscar_areas.Text.ToUpper().Trim();

            if (str_clte == "TODOS")
            {
                using (dd_liecEntities data_areas = new dd_liecEntities())
                {
                    var i_areas = (from t_areas in data_areas.fact_areas
                                   select new
                                   {
                                       t_areas.cod_area,
                                       t_areas.desc_areas,
                                       t_areas.fecha_registro
                                   }).OrderBy(x => x.cod_area).ToList();

                    gv_areas.DataSource = i_areas;
                    gv_areas.DataBind();
                    gv_areas.Visible = true;
                }
            }
            else
            {
                string n_rub;
                Char char_s = '|';
                string d_rub = txt_buscar_areas.Text.Trim();
                String[] de_rub = d_rub.Trim().Split(char_s);
                n_rub = de_rub[1].Trim();

                using (dd_liecEntities data_areas = new dd_liecEntities())
                {
                    var i_areas = (from t_areas in data_areas.fact_areas
                                   where t_areas.cod_area == n_rub

                                   select new
                                   {
                                       t_areas.cod_area,

                                       t_areas.desc_areas,
                                       t_areas.fecha_registro
                                   }).OrderBy(x => x.cod_area).ToList();

                    gv_areas.DataSource = i_areas;
                    gv_areas.DataBind();
                    gv_areas.Visible = true;
                }
            }

            limp_txt_area();
        }



        protected void chkb_desactivar_areas_CheckedChanged(object sender, EventArgs e)
        {
            int_areas = 0;
            i_agregar_areas.Attributes["style"] = "color:white";
            i_editar_areas.Attributes["style"] = "color:white";
            rfv_areas_coment.Enabled = false;
            rfv_buscar_areas.Enabled = false;
            div_busc_clt.Visible = false;
        }

        protected void btn_editar_areas_Click(object sender, EventArgs e)
        {
            int_areas = 2;

            i_agregar_areas.Attributes["style"] = "color:white";
            i_editar_areas.Attributes["style"] = "color:#E34C0E";
            div_busc_clt.Visible = true;
            rfv_buscar_areas.Enabled = true;
        }

        #endregion areas

        #region corro_envio
        protected void btn_buscar_e_env_Click(object sender, EventArgs e)
        {

            string str_e = txt_buscar_e_env.Text.ToUpper().Trim();
            try
            {
                if (str_e == "TODOS")
                {
                    using (dd_liecEntities data_e_env = new dd_liecEntities())
                    {
                        var i_e_env = (from t_e_env in data_e_env.inf_email_envio
                                       join t_est in data_e_env.fact_est_e_env on t_e_env.id_est_e_env equals t_est.id_est_e_env

                                       select new
                                       {
                                           t_e_env.email,

                                           t_e_env.fecha_registro
                                       }).OrderBy(x => x.email).ToList();

                        gv_e_env.DataSource = i_e_env;
                        gv_e_env.DataBind();
                        gv_e_env.Visible = true;
                    }
                }
                else
                {


                    using (dd_liecEntities data_e_env = new dd_liecEntities())
                    {
                        var i_e_env = (from t_e_env in data_e_env.inf_email_envio
                                       join t_est in data_e_env.fact_est_e_env on t_e_env.id_est_e_env equals t_est.id_est_e_env
                                       where t_e_env.email == str_e
                                       select new
                                       {
                                           t_e_env.email,

                                           t_e_env.fecha_registro
                                       }).OrderBy(x => x.email).ToList();

                        gv_e_env.DataSource = i_e_env;
                        gv_e_env.DataBind();
                        gv_e_env.Visible = true;
                    }
                }
            }
            catch
            {

                txt_buscar_e_env.Text = null;
                Mensaje("Correo no existe, favor de revisar.");
            }
        }

        protected void btn_agregar_e_env_Click(object sender, EventArgs e)
        {

            int_e_env = 1;

            i_agregar_e_env.Attributes["style"] = "color:#E34C0E";
            i_editar_e_env.Attributes["style"] = "color:white";

            rfv_buscar_e_env.Enabled = false;
            div_busc_e_env.Visible = false;
            gv_e_env.Visible = false;

            rfv_correo_envio.Enabled = true;
            rfv_asunto_envio.Enabled = true;
            rfv_usuario_envio.Enabled = true;
            rfv_servidor_smtp.Enabled = true;
            rfv_clave_envio.Enabled = true;
            rfv_puerto_envio.Enabled = true;



        }

        protected void btn_editar_e_env_Click(object sender, EventArgs e)
        {
            int_e_env = 2;

            i_agregar_e_env.Attributes["style"] = "color:white";
            i_editar_e_env.Attributes["style"] = "color:#E34C0E";

            div_busc_e_env.Visible = true;
            rfv_buscar_e_env.Enabled = true;

        }

        protected void gv_e_env_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_e_env.PageIndex = e.NewPageIndex;
            string str_e = txt_buscar_e_env.Text.ToUpper().Trim();
            try
            {
                if (str_e == "TODOS")
                {
                    using (dd_liecEntities data_e_env = new dd_liecEntities())
                    {
                        var i_e_env = (from t_e_env in data_e_env.inf_email_envio
                                       join t_est in data_e_env.fact_est_e_env on t_e_env.id_est_e_env equals t_est.id_est_e_env

                                       select new
                                       {
                                           t_e_env.email,

                                           t_e_env.fecha_registro
                                       }).OrderBy(x => x.email).ToList();

                        gv_e_env.DataSource = i_e_env;
                        gv_e_env.DataBind();
                        gv_e_env.Visible = true;
                    }
                }
                else
                {


                    using (dd_liecEntities data_e_env = new dd_liecEntities())
                    {
                        var i_e_env = (from t_e_env in data_e_env.inf_email_envio
                                       join t_est in data_e_env.fact_est_e_env on t_e_env.id_est_e_env equals t_est.id_est_e_env
                                       where t_e_env.email == str_e
                                       select new
                                       {
                                           t_e_env.email,

                                           t_e_env.fecha_registro
                                       }).OrderBy(x => x.email).ToList();

                        gv_e_env.DataSource = i_e_env;
                        gv_e_env.DataBind();
                        gv_e_env.Visible = true;
                    }
                }
            }
            catch
            {

                txt_buscar_e_env.Text = null;
                Mensaje("Correo no existe, favor de revisar.");
            }
        }

        protected void gv_e_env_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string str_clteID = e.Row.Cells[1].Text;
                int int_estatusID;

                using (dd_liecEntities data_clte = new dd_liecEntities())
                {
                    var i_clte = (from t_clte in data_clte.inf_email_envio
                                  where t_clte.email == str_clteID
                                  select new
                                  {
                                      t_clte.id_est_e_env,
                                  }).FirstOrDefault();

                    int_estatusID = int.Parse(i_clte.id_est_e_env.ToString());
                }

                DropDownList DropDownList1 = (e.Row.FindControl("ddl_est_e_env") as DropDownList);

                using (dd_liecEntities db_sepomex = new dd_liecEntities())
                {
                    var tbl_sepomex = (from c in db_sepomex.fact_est_e_env
                                       select c).ToList();

                    DropDownList1.DataSource = tbl_sepomex;

                    DropDownList1.DataTextField = "descid_est_e_env";
                    DropDownList1.DataValueField = "id_est_e_env";
                    DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
                    DropDownList1.SelectedValue = int_estatusID.ToString();
                }
            }
        }

        protected void chk_e_env_CheckedChanged(object sender, EventArgs e)
        {
            string str_rub;
            Guid guid_rub;

            foreach (GridViewRow row in gv_e_env.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chk_e_env") as CheckBox);
                    if (chkRow.Checked)
                    {
                        row.BackColor = Color.FromArgb(227, 76, 14);
                        str_rub = row.Cells[1].Text;

                        using (dd_liecEntities edm_rub = new dd_liecEntities())
                        {
                            var i_rub = (from c in edm_rub.inf_email_envio
                                         where c.email == str_rub
                                         select c).FirstOrDefault();

                            guid_rub = i_rub.id_email_env;

                            var f_rub = (from r in edm_rub.inf_email_envio
                                         where r.id_email_env == guid_rub
                                         select new
                                         {
                                             r.email,
                                             r.usuario,
                                             r.asunto,
                                             r.servidor_smtp,
                                             r.puerto

                                         }).FirstOrDefault();

                            txt_correo_envio.Text = f_rub.email;
                            txt_usuario_envio.Text = f_rub.usuario;

                            txt_asunto_envio.Text = f_rub.asunto;
                            txt_servidor_smtp.Text = f_rub.servidor_smtp;
                            txt_puerto_envio.Text = f_rub.puerto.ToString();
                        }
                    }
                    else
                    {
                        row.BackColor = Color.White;
                    }
                }
            }
        }

        protected void chkb_des_e_env_CheckedChanged(object sender, EventArgs e)
        {
            int_e_env = 0;

            rfv_buscar_e_env.Enabled = false;
            rfv_correo_envio.Enabled = false;
            rfv_asunto_envio.Enabled = false;
            rfv_usuario_envio.Enabled = false;
            rfv_servidor_smtp.Enabled = false;
            rfv_clave_envio.Enabled = false;
            rfv_puerto_envio.Enabled = false;
        }

        protected void btn_guardar_envio_Click(object sender, EventArgs e)
        {
            if (int_e_env == 0)
            {
                Mensaje("Favor de seleccionar una acción");
            }
            else
            {
                Guid guid_ncorreoenvio = Guid.NewGuid();
                string str_correoenvio = txt_correo_envio.Text;
                string str_usuarioenvio = txt_usuario_envio.Text;
                string str_claveenvio = encrypta.Encrypt(txt_clave_envio.Text);
                string str_asunto = txt_asunto_envio.Text;
                string str_svrsmtp = txt_servidor_smtp.Text;
                int str_puertoenvio = int.Parse(txt_puerto_envio.Text);

                Guid guid_fcorreoenvio;

                if (int_e_env == 1)
                {
                    using (dd_liecEntities data_user = new dd_liecEntities())
                    {
                        var items_user = (from c in data_user.inf_email_envio
                                          where c.email == str_correoenvio
                                          select c).ToList();

                        if (items_user.Count == 0)
                        {

                            var i_usuario = new inf_email_envio
                            {
                                id_email_env = guid_ncorreoenvio,
                                id_est_e_env = 1,
                                email = str_correoenvio,
                                usuario = str_usuarioenvio,
                                clave = str_claveenvio,
                                asunto = str_asunto,
                                servidor_smtp = str_svrsmtp,
                                puerto = str_puertoenvio,
                                fecha_registro = DateTime.Now,
                                id_emp = guid_emp
                            };

                            data_user.inf_email_envio.Add(i_usuario);
                            data_user.SaveChanges();




                            limpia_e_env();
                            Mensaje("Datos agregados con éxito");
                        }
                        else
                        {
                            Mensaje("Correo ya existe en la base de datos, favor de reintentar o editar información");
                        }
                    }
                }
                else if (int_e_env == 2)
                {
                    using (var m_fusuarioff = new dd_liecEntities())
                    {
                        var i_fusuarioff = (from c in m_fusuarioff.inf_email_envio
                                            select c).FirstOrDefault();

                        guid_fcorreoenvio = i_fusuarioff.id_email_env;

                        var i_ff = (from c in m_fusuarioff.inf_email_envio
                                    where c.id_email_env == guid_fcorreoenvio
                                    select c).FirstOrDefault();

                        i_fusuarioff.email = str_correoenvio;
                        i_fusuarioff.usuario = str_usuarioenvio;
                        i_fusuarioff.clave = str_claveenvio;
                        i_fusuarioff.asunto = str_asunto;
                        i_fusuarioff.servidor_smtp = str_svrsmtp;
                        i_fusuarioff.puerto = str_puertoenvio;

                        m_fusuarioff.SaveChanges();
                    }


                    Mensaje("Datos actualizados con éxito");
                }

            }
        }
        private void limpia_e_env()
        {
            txt_correo_envio.Text = null;
            txt_usuario_envio.Text = null;
            txt_clave_envio.Text = null;
            txt_asunto_envio.Text = null;
            txt_servidor_smtp.Text = null;
            txt_puerto_envio.Text = null;
        }
        #endregion

        #region usuarios



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
            rfv_usr_i.Enabled = true;
            rfv_clave_i.Enabled = true;
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
                string cod_usr, nom_usr, apater_usr, amater_usr, e_user, tel_usr, movil_usr, callenum_usr, cp_usr, code_i, email_i, clave_i;
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
                code_i = txt_usr_i.Text.Trim().ToLower();
                email_i = txt_correo_i.Text.Trim();
                clave_i = encrypta.Encrypt(txt_clave_i.Text.Trim().ToLower());


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
                            i_usrs.usr = code_i;
                            i_usrs.email = email_i;
                            i_usrs.clave = clave_i;
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
                                    usr = code_i,
                                    email = email_i,
                                    clave = clave_i,
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
                                    usr = code_i,
                                    email = email_i,
                                    clave = clave_i,
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
                            rfv_usr_i.Enabled = true;
                            rfv_clave_i.Enabled = true;
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