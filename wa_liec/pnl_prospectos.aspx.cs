using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wa_liec
{
    public partial class pnl_prospectos : System.Web.UI.Page
    {
        public static int int_prospecto, int_prospcont, int_pnlID, int_idperf;
        public static Guid guid_emp;
        public static Guid guid_idusr;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    inf_usr_oper();
                    carga_ddl();
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
                carga_ddl();
            }
        }

        private void carga_ddl()
        {
            ddl_colonia_prospecto.Items.Clear();
            ddl_colonia_prospecto.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
            ddl_tipocont_prospecto.Items.Clear();

            using (dd_liecEntities db_sepomex = new dd_liecEntities())
            {
                var tbl_sepomex = (from c in db_sepomex.fact_tipo_contprosp
                                   select c).ToList();

                ddl_tipocont_prospecto.DataSource = tbl_sepomex;
                ddl_tipocont_prospecto.DataTextField = "desc_tipo_contprosp";
                ddl_tipocont_prospecto.DataValueField = "id_tipo_contprosp";
                ddl_tipocont_prospecto.DataBind();
            }
            ddl_tipocont_prospecto.Items.Insert(0, new ListItem("SELECCIONAR", "0"));

            ddl_acc_prospecto.Items.Clear();

            using (dd_liecEntities db_sepomex = new dd_liecEntities())
            {
                var tbl_sepomex = (from c in db_sepomex.fact_taccion_prosp
                                   select c).ToList();

                ddl_acc_prospecto.DataSource = tbl_sepomex;
                ddl_acc_prospecto.DataTextField = "desc_taccion_prosp";
                ddl_acc_prospecto.DataValueField = "id_taccion_prosp";
                ddl_acc_prospecto.DataBind();
            }
            ddl_acc_prospecto.Items.Insert(0, new ListItem("SELECCIONAR", "0"));

            ddl_giro_prospecto.Items.Clear();

            using (dd_liecEntities db_sepomex = new dd_liecEntities())
            {
                var tbl_sepomex = (from c in db_sepomex.fact_giro_prosp
                                   select c).ToList();

                ddl_giro_prospecto.DataSource = tbl_sepomex;
                ddl_giro_prospecto.DataTextField = "desc_giro_prosp";
                ddl_giro_prospecto.DataValueField = "id_giro_prosp";
                ddl_giro_prospecto.DataBind();
            }
            ddl_giro_prospecto.Items.Insert(0, new ListItem("SELECCIONAR", "0"));

            ddl_serv_prospecto.Items.Clear();

            using (dd_liecEntities db_sepomex = new dd_liecEntities())
            {
                var tbl_sepomex = (from c in db_sepomex.fact_tserv_prosp
                                   select c).ToList();

                ddl_serv_prospecto.DataSource = tbl_sepomex;
                ddl_serv_prospecto.DataTextField = "desc_tserv_prosp";
                ddl_serv_prospecto.DataValueField = "id_tserv_prosp";
                ddl_serv_prospecto.DataBind();
            }
            ddl_serv_prospecto.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
        }

        [ScriptMethod()]
        [WebMethod]
        public static List<string> SearchCustomers(string prefixText, int count)
        {
            List<String> columnData = new List<String>();
            string str_fclte = prefixText.ToUpper();

            if (int_pnlID == 1)
            {
                string f_desccajaf = prefixText.ToUpper();

                using (dd_liecEntities m_rub = new dd_liecEntities())
                {
                    var i_rub = (from i_u in m_rub.inf_prospectos
                                 where i_u.empresa.Contains(str_fclte)

                                 select new
                                 {
                                     i_u.empresa,
                                     i_u.cod_prospecto,
                                 }).ToList();

                    foreach (var ff_rub in i_rub)
                    {
                        columnData.Add(ff_rub.empresa + " | " + ff_rub.cod_prospecto);
                    }
                }
            }

            return columnData;
        }

        #region menu

        protected void lkb_prosp_prosp_Click(object sender, EventArgs e)
        {
            int_pnlID = 1;
            pnl_prospecto.Visible = true;
            pnl_prospcontf.Visible = false;

            i_prosp.Attributes["style"] = "color:#E34C0E";
            lbl_prosp_prosp.Attributes["style"] = "color:#E34C0E";
            i_adat.Attributes["style"] = "color:#337ab7";
            lbl_adat_prosp.Attributes["style"] = "color:#337ab7";
            i_seg_prosp.Attributes["style"] = "color:#337ab7";
            lbl_seg_prosp.Attributes["style"] = "color:#337ab7";
            limp_txt_prospecto();
            div_busc_clt.Visible = false;
            btn_gprosp_alt.Visible = false;
            gv_prospecto.Visible = false;
            chkb_desactivar_prospecto.Checked = false;

            i_agregar_prospecto.Attributes["style"] = "color:white";
            i_editar_prospecto.Attributes["style"] = "color:white";
        }

        protected void lkb_adat_prosp_Click(object sender, EventArgs e)
        {
            int_pnlID = 1;
            pnl_prospecto.Visible = false;
            pnl_prospcontf.Visible = true;
            i_prosp.Attributes["style"] = "color:#337ab7";
            lbl_prosp_prosp.Attributes["style"] = "color:#337ab7";
            i_adat.Attributes["style"] = "color:#E34C0E";
            lbl_adat_prosp.Attributes["style"] = "color:#E34C0E";
            i_seg_prosp.Attributes["style"] = "color:#337ab7";
            lbl_seg_prosp.Attributes["style"] = "color:#337ab7";
        }

        protected void lkb_seg_prosp_Click(object sender, EventArgs e)
        {
            int_pnlID = 3;
            pnl_prospecto.Visible = false;
            pnl_prospcontf.Visible = false;
            i_prosp.Attributes["style"] = "color:#337ab7";
            lbl_prosp_prosp.Attributes["style"] = "color:#337ab7";
            i_adat.Attributes["style"] = "color:#337ab7";
            lbl_adat_prosp.Attributes["style"] = "color:#337ab7";
            i_seg_prosp.Attributes["style"] = "color:#E34C0E";
            lbl_seg_prosp.Attributes["style"] = "color:#E34C0E";
        }

        protected void lkb_salir_Click(object sender, EventArgs e)
        {
            pnl_prospecto.Visible = false;
            Response.Redirect("acceso.aspx");
        }

        #endregion menu

        #region prospectos

        protected void btn_buscar_prospecto_Click(object sender, EventArgs e)
        {
            limp_txt_prospecto();
            string str_rub = txt_buscar_prospecto.Text.ToUpper().Trim();

            if (str_rub == "TODO")
            {
                using (dd_liecEntities data_user = new dd_liecEntities())
                {
                    var inf_user = (from i_r in data_user.inf_prospectos
                                    join i_c in data_user.inf_usuarios on i_r.id_usuario equals i_c.id_usuario
                                    select new
                                    {
                                        i_r.id_prospecto,
                                        i_r.cod_prospecto,
                                        i_r.empresa,
                                        i_r.fecha_registro,
                                        nom_usr = i_c.nombres + " " + i_c.a_paterno + " " + i_c.a_materno
                                    }).OrderBy(x => x.cod_prospecto).ToList();

                    if (inf_user.Count == 0)
                    {
                        gv_prospecto.DataSource = inf_user;
                        gv_prospecto.DataBind();
                        gv_prospecto.Visible = true;
                        div_prospecto.Visible = true;

                        Mensaje("Empresa no encontrado.");
                    }
                    else
                    {
                        gv_prospecto.DataSource = inf_user;
                        gv_prospecto.DataBind();
                        gv_prospecto.Visible = true;
                        div_prospecto.Visible = true;
                    }
                }
            }
            else
            {
                try
                {
                    string n_rub;
                    Guid guid_fclte;
                    Char char_s = '|';
                    string d_rub = txt_buscar_prospecto.Text.Trim();
                    String[] de_rub = d_rub.Trim().Split(char_s);

                    n_rub = de_rub[1].Trim();

                    using (dd_liecEntities edm_nclte = new dd_liecEntities())
                    {
                        var i_nclte = (from c in edm_nclte.inf_prospectos
                                       where c.cod_prospecto == n_rub
                                       select c).FirstOrDefault();

                        guid_fclte = i_nclte.id_prospecto;
                    }

                    using (dd_liecEntities data_user = new dd_liecEntities())
                    {
                        var inf_user = (from i_r in data_user.inf_prospectos
                                        join i_c in data_user.inf_usuarios on i_r.id_usuario equals i_c.id_usuario
                                        where i_r.id_prospecto == guid_fclte
                                        select new
                                        {
                                            i_r.id_prospecto,
                                            i_r.cod_prospecto,
                                            i_r.empresa,
                                            i_r.fecha_registro,
                                            nom_usr = i_c.nombres + " " + i_c.a_paterno + " " + i_c.a_materno
                                        }).OrderBy(x => x.cod_prospecto).ToList();

                        if (inf_user.Count == 0)
                        {
                            gv_prospecto.DataSource = inf_user;
                            gv_prospecto.DataBind();
                            gv_prospecto.Visible = true;
                            div_prospecto.Visible = true;

                            Mensaje("Empresa no encontrado.");
                        }
                        else
                        {
                            gv_prospecto.DataSource = inf_user;
                            gv_prospecto.DataBind();
                            gv_prospecto.Visible = true;
                            div_prospecto.Visible = true;
                        }
                    }
                }
                catch
                {
                    limp_txt_prospecto();
                    div_prospecto.Visible = false;
                    Mensaje("Empresa no encontrado.");
                }
            }
        }

        protected void btn_agregar_prospecto_Click(object sender, EventArgs e)
        {
            int_prospecto = 1;

            div_cont_prosp.Visible = true;

            limp_txt_prospecto();
            div_busc_clt.Visible = false;
            btn_gprosp_alt.Visible = false;
            gv_prospecto.Visible = false;
            chkb_desactivar_prospecto.Checked = false;

            i_agregar_prospecto.Attributes["style"] = "color:#E34C0E";
            i_editar_prospecto.Attributes["style"] = "color:white";
            rfv_buscar_prospecto.Enabled = false;

            rfv_tipocont_prospecto.Enabled = true;
            rfv_emp_prospecto.Enabled = true;
            rfv_cont_prospecto.Enabled = false;
            rfv_giro_prospecto.Enabled = false;
            rfv_serv_prospecto.Enabled = false;

            rfv_acc_prospecto.Enabled = false;
            rfv_dpto_prosp.Enabled = false;
            rfv_prospecto_coment.Enabled = false;
        }

        protected void btn_editar_prospecto_Click(object sender, EventArgs e)
        {
            int_prospecto = 2;
            div_busc_clt.Visible = true;
            div_cont_prosp.Visible = false;
            btn_gprosp_alt.Visible = true;

            i_agregar_prospecto.Attributes["style"] = "color:white";
            i_editar_prospecto.Attributes["style"] = "color:#E34C0E";
            rfv_buscar_prospecto.Enabled = true;
            rfv_tipocont_prospecto.Enabled = false;
            rfv_emp_prospecto.Enabled = false;
            rfv_cont_prospecto.Enabled = false;
            rfv_giro_prospecto.Enabled = false;
            rfv_serv_prospecto.Enabled = false;

            rfv_dpto_prosp.Enabled = false;
            rfv_acc_prospecto.Enabled = false;
            rfv_prospecto_coment.Enabled = false;

            Mensaje("Solo se pueden editar datos generales del prospecto, para editar los contactos favor de seleccionar 'Anexo de datos'");
        }

        protected void chk_prospecto_CheckedChanged(object sender, EventArgs e)
        {
            limp_txt_prospecto();
            rfv_colonia_prospecto.Enabled = false;
            Guid guid_prospecto, guid_cont_prosp;
            int int_estatusID = 0;

            foreach (GridViewRow row in gv_prospecto.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chk_prospecto") as CheckBox);
                    if (chkRow.Checked)
                    {
                        int_estatusID = int_estatusID + 1;
                        row.BackColor = Color.FromArgb(227, 76, 14);
                        guid_prospecto = Guid.Parse(row.Cells[1].Text);

                        using (dd_liecEntities edm_prospecto = new dd_liecEntities())
                        {
                            var i_prospecto = (from t_prospecto in edm_prospecto.inf_prospectos

                                               where t_prospecto.id_prospecto == guid_prospecto
                                               select new
                                               {
                                                   t_prospecto.id_tipo_contprosp,
                                                   t_prospecto.empresa,

                                                   t_prospecto.id_giro_prosp,
                                                   t_prospecto.id_tserv_prosp,

                                                   t_prospecto.callenum,
                                                   t_prospecto.d_codigo,
                                                   t_prospecto.id_asenta_cpcons,
                                               }).FirstOrDefault();

                            ddl_tipocont_prospecto.SelectedValue = i_prospecto.id_tipo_contprosp.ToString();
                            txt_emp_prospecto.Text = i_prospecto.empresa;

                            ddl_giro_prospecto.SelectedValue = i_prospecto.id_giro_prosp.ToString();
                            ddl_serv_prospecto.SelectedValue = i_prospecto.id_tserv_prosp.ToString();

                         

                            try
                            {
                                txt_callenum_prospecto.Text = i_prospecto.callenum.Trim();
                                txt_cp_prospecto.Text = i_prospecto.d_codigo;
                                int int_col = int.Parse(i_prospecto.id_asenta_cpcons.ToString());

                                using (dd_liecEntities db_sepomex = new dd_liecEntities())
                                {
                                    var tbl_sepomex = (from c in db_sepomex.inf_sepomex
                                                       where c.id_asenta_cpcons == int_col
                                                       where c.d_codigo == i_prospecto.d_codigo
                                                       select c).ToList();

                                    ddl_colonia_prospecto.DataSource = tbl_sepomex;
                                    ddl_colonia_prospecto.DataTextField = "d_asenta";
                                    ddl_colonia_prospecto.DataValueField = "id_asenta_cpcons";
                                    ddl_colonia_prospecto.DataBind();

                                    txt_municipio_prospecto.Text = tbl_sepomex[0].d_mnpio;
                                    txt_estado_prospecto.Text = tbl_sepomex[0].d_estado;
                                }
                            }
                            catch
                            {
                                ddl_colonia_prospecto.Items.Clear();
                                ddl_colonia_prospecto.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
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
                limp_txt_prospecto();
            }
        }

        protected void gv_prospecto_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_prospecto.PageIndex = e.NewPageIndex;
            limp_txt_prospecto();
            string str_rub = txt_buscar_prospecto.Text.ToUpper().Trim();

            if (str_rub == "TODO")
            {
                using (dd_liecEntities data_user = new dd_liecEntities())
                {
                    var inf_user = (from i_r in data_user.inf_prospectos
                                    join i_c in data_user.inf_usuarios on i_r.id_usuario equals i_c.id_usuario
                                    select new
                                    {
                                        i_r.id_prospecto,
                                        i_r.cod_prospecto,
                                        i_r.empresa,
                                        i_r.fecha_registro,
                                        nom_usr = i_c.nombres + " " + i_c.a_paterno + " " + i_c.a_materno
                                    }).OrderBy(x => x.cod_prospecto).ToList();

                    if (inf_user.Count == 0)
                    {
                        gv_prospecto.DataSource = inf_user;
                        gv_prospecto.DataBind();
                        gv_prospecto.Visible = true;
                        div_prospecto.Visible = true;

                        Mensaje("Empresa no encontrado.");
                    }
                    else
                    {
                        gv_prospecto.DataSource = inf_user;
                        gv_prospecto.DataBind();
                        gv_prospecto.Visible = true;
                        div_prospecto.Visible = true;
                    }
                }
            }
            else
            {
                try
                {
                    string n_rub;
                    Guid guid_fclte;
                    Char char_s = '|';
                    string d_rub = txt_buscar_prospecto.Text.Trim();
                    String[] de_rub = d_rub.Trim().Split(char_s);

                    n_rub = de_rub[1].Trim();

                    using (dd_liecEntities edm_nclte = new dd_liecEntities())
                    {
                        var i_nclte = (from c in edm_nclte.inf_prospectos
                                       where c.cod_prospecto == n_rub
                                       select c).FirstOrDefault();

                        guid_fclte = i_nclte.id_prospecto;
                    }

                    using (dd_liecEntities data_user = new dd_liecEntities())
                    {
                        var inf_user = (from i_r in data_user.inf_prospectos
                                        join i_c in data_user.inf_usuarios on i_r.id_usuario equals i_c.id_usuario
                                        select new
                                        {
                                            i_r.id_prospecto,
                                            i_r.cod_prospecto,
                                            i_r.empresa,
                                            i_r.fecha_registro,
                                            nom_usr = i_c.nombres + " " + i_c.a_paterno + " " + i_c.a_materno
                                        }).OrderBy(x => x.cod_prospecto).ToList();

                        if (inf_user.Count == 0)
                        {
                            gv_prospecto.DataSource = inf_user;
                            gv_prospecto.DataBind();
                            gv_prospecto.Visible = true;
                            div_prospecto.Visible = true;

                            Mensaje("Empresa no encontrado.");
                        }
                        else
                        {
                            gv_prospecto.DataSource = inf_user;
                            gv_prospecto.DataBind();
                            gv_prospecto.Visible = true;
                            div_prospecto.Visible = true;
                        }
                    }
                }
                catch
                {
                    limp_txt_prospecto();
                    div_prospecto.Visible = false;
                    Mensaje("Empresa no encontrado.");
                }
            }
        }

        protected void btn_gprosp_alt_Click(object sender, EventArgs e)
        {
            if (int_prospecto == 0)
            {
                Mensaje("Favor de seleccionr una acción.");
            }
            else
            {
                edita_prospecto();
            }
        }

        private void edita_prospecto()
        {
            Guid guid_prospecto = Guid.NewGuid();
            Guid guid_contprosp = Guid.NewGuid();
            Guid guid_sprospecto = Guid.NewGuid();

            string str_cod_prospecto, str_nom_prospecto;
            int int_tcont = Convert.ToInt32(ddl_tipocont_prospecto.SelectedValue);
            string str_emp = txt_emp_prospecto.Text.Trim().ToUpper();
            string str_contacto = txt_cont_prospecto.Text.ToUpper().Trim();
            string str_dpto = txt_dpto_prosp.Text.ToUpper().Trim();
            int int_giro = Convert.ToInt32(ddl_giro_prospecto.SelectedValue);
            int int_servicio = Convert.ToInt32(ddl_serv_prospecto.SelectedValue);
            string str_tel1 = txt_tel1_prospecto.Text.Trim();
            string str_tel2 = txt_tel2_prospecto.Text.Trim();
            string str_email1 = txt_email1_prospecto.Text.Trim();
            string str_email2 = txt_email2_prospecto.Text.Trim();
            string str_callenum = txt_callenum_prospecto.Text.ToUpper().Trim();
            string str_cp = txt_cp_prospecto.Text;
            int int_colonia = Convert.ToInt32(ddl_colonia_prospecto.SelectedValue);
            int int_accion = Convert.ToInt32(ddl_acc_prospecto.SelectedValue);

            string str_coment = txt_prospecto_coment.Text.Trim().ToUpper();

            if (int_giro == 0)
            {
                int_giro = 1;
            }

            if (int_servicio == 0)
            {
                int_servicio = 1;
            }

            if (int_accion == 0)
            {
                int_accion = 1;
            }

            if (int_prospecto == 2)
            {
                int int_ddl, int_f_prospecto = 0;
                int int_estatusID = 0;
                Guid str_fclte = Guid.Empty;
                foreach (GridViewRow row in gv_prospecto.Rows)
                {
                    // int key = (int)GridView1.DataKeys[row.RowIndex].Value;
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chk_prospecto") as CheckBox);
                        if (chkRow.Checked)
                        {
                            int_f_prospecto = int_f_prospecto + 1;
                            str_fclte = Guid.Parse(row.Cells[1].Text);
                            str_nom_prospecto = row.Cells[2].Text;
                            //DropDownList dl = (DropDownList)row.FindControl("ddl_prospecto_estatus");

                            //int_ddl = int.Parse(dl.SelectedValue);
                            if (int_f_prospecto == 1)
                            {
                                int_estatusID = 1;
                                break;
                            }
                            else if (int_f_prospecto == 2)
                            {
                                int_estatusID = 2;
                                break;
                            }
                            else if (int_f_prospecto == 3)
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
                    limp_txt_prospecto();
                    Mensaje("Favor de seleccionar un cliente.");
                }
                else
                {
                    str_coment = txt_prospecto_coment.Text;

                    using (var m_prospecto = new dd_liecEntities())
                    {
                        var i_prospecto = (from c in m_prospecto.inf_prospectos
                                           where c.id_prospecto == str_fclte
                                           select c).FirstOrDefault();

                        i_prospecto.id_giro_prosp = int_giro;
                        i_prospecto.id_tserv_prosp = int_servicio;
                        i_prospecto.id_est_prospecto = 1;
                        i_prospecto.id_tipo_contprosp = int_tcont;
                        i_prospecto.empresa = str_emp;
                        i_prospecto.callenum = str_callenum;
                        i_prospecto.d_codigo = str_cp;
                        i_prospecto.id_asenta_cpcons = int_colonia;

                        m_prospecto.SaveChanges();

                        if (string.IsNullOrEmpty(str_contacto))
                        {
                            if (int_prospcont == 1)
                            {
                                if (string.IsNullOrEmpty(str_contacto))
                                {
                                }
                                else
                                {
                                    var i_cont_prospff = new inf_cont_prosp

                                    {
                                        id_cont_prosp = guid_contprosp,
                                        dpto = str_dpto,
                                        contacto = str_contacto,
                                        tel1 = str_tel1,
                                        email1 = str_email1,
                                        tel2 = str_tel2,
                                        email2 = str_email2,
                                        fecha_registro = DateTime.Now,
                                        id_usuario = guid_idusr,
                                        id_prospecto = str_fclte,
                                    };

                                    m_prospecto.inf_cont_prosp.Add(i_cont_prospff);
                                    m_prospecto.SaveChanges();
                                }
                            }
                        }
                        else
                        {
                            if (int_prospcont == 1)
                            {
                                if (string.IsNullOrEmpty(str_contacto))
                                {
                                }
                                else
                                {
                                    var i_cont_prospff = new inf_cont_prosp

                                    {
                                        id_cont_prosp = guid_contprosp,
                                        dpto = str_dpto,
                                        contacto = str_contacto,
                                        tel1 = str_tel1,
                                        email1 = str_email1,
                                        tel2 = str_tel2,
                                        email2 = str_email2,
                                        fecha_registro = DateTime.Now,
                                        id_usuario = guid_idusr,
                                        id_prospecto = str_fclte,
                                    };

                                    m_prospecto.inf_cont_prosp.Add(i_cont_prospff);
                                    m_prospecto.SaveChanges();
                                }
                            }
                            var i_cont_prosp = (from c in m_prospecto.inf_cont_prosp
                                                where c.id_prospecto == str_fclte
                                                select c).ToList();

                            if (i_cont_prosp.Count == 0)
                            {
                                var i_cont_prospff = new inf_cont_prosp

                                {
                                    id_cont_prosp = guid_contprosp,
                                    dpto = str_dpto,
                                    contacto = str_contacto,
                                    tel1 = str_tel1,
                                    email1 = str_email1,
                                    tel2 = str_tel2,
                                    email2 = str_email2,
                                    fecha_registro = DateTime.Now,
                                    id_usuario = guid_idusr,
                                    id_prospecto = str_fclte,
                                };

                                m_prospecto.inf_cont_prosp.Add(i_cont_prospff);
                                m_prospecto.SaveChanges();
                            }
                        }
                        if (string.IsNullOrEmpty(str_coment))
                        { }
                        else
                        {
                            var i_prospecto_s = new inf_seg_prospecto

                            {
                                id_seg_prospecto = guid_sprospecto,
                                id_taccion_prosp = int_accion,
                                comentarios = str_coment,
                                fecha_registro = DateTime.Now,
                                id_cont_prosp = guid_prospecto,
                                id_usuario = guid_idusr
                            };

                            m_prospecto.inf_seg_prospecto.Add(i_prospecto_s);
                            m_prospecto.SaveChanges();
                        }
                    }
                    foreach (GridViewRow row in gv_prospecto.Rows)
                    {
                        // int key = (int)GridView1.DataKeys[row.RowIndex].Value;
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[0].FindControl("chk_prospecto") as CheckBox);
                            if (chkRow.Checked)
                            {
                                chkRow.Checked = false;
                                row.BackColor = Color.White;
                            }
                        }
                    }
                    limp_txt_prospecto();

                    rfv_buscar_prospecto.Enabled = true;
                    rfv_tipocont_prospecto.Enabled = false;
                    rfv_emp_prospecto.Enabled = false;
                    rfv_cont_prospecto.Enabled = false;
                    rfv_giro_prospecto.Enabled = false;
                    rfv_serv_prospecto.Enabled = false;

                    rfv_acc_prospecto.Enabled = false;
                    rfv_prospecto_coment.Enabled = false;

                    Mensaje("Datos de cliente actualizados con éxito.");
                }
            }
        }

        protected void btn_guardar_prospecto_Click(object sender, EventArgs e)
        {
            if (int_prospecto == 0)
            {
                Mensaje("Favor de seleccionr una acción.");
            }
            else
            {
                guarda_prospecto();
            }
        }

        private void guarda_prospecto()
        {
            Guid guid_prospecto = Guid.NewGuid();
            Guid guid_contprosp = Guid.NewGuid();
            Guid guid_sprospecto = Guid.NewGuid();

            string str_cod_prospecto, str_nom_prospecto;
            int int_tcont = Convert.ToInt32(ddl_tipocont_prospecto.SelectedValue);
            string str_emp = txt_emp_prospecto.Text.Trim().ToUpper();
            string str_contacto = txt_cont_prospecto.Text.ToUpper().Trim();
            string str_dpto = txt_dpto_prosp.Text.ToUpper().Trim();
            int int_giro = Convert.ToInt32(ddl_giro_prospecto.SelectedValue);
            int int_servicio = Convert.ToInt32(ddl_serv_prospecto.SelectedValue);
            string str_tel1 = txt_tel1_prospecto.Text.Trim();
            string str_tel2 = txt_tel2_prospecto.Text.Trim();
            string str_email1 = txt_email1_prospecto.Text.Trim();
            string str_email2 = txt_email2_prospecto.Text.Trim();
            string str_callenum = txt_callenum_prospecto.Text.ToUpper().Trim();
            string str_cp = txt_cp_prospecto.Text;
            int int_colonia = Convert.ToInt32(ddl_colonia_prospecto.SelectedValue);
            int int_accion = Convert.ToInt32(ddl_acc_prospecto.SelectedValue);
            string str_coment = txt_prospecto_coment.Text.Trim().ToUpper();

            if (int_giro == 0)
            {
                int_giro = 1;
            }

            if (int_servicio == 0)
            {
                int_servicio = 1;
            }

            if (int_accion == 0)
            {
                int_accion = 1;
            }

            if (int_prospecto == 1)
            {
                using (dd_liecEntities edm_nclte = new dd_liecEntities())
                {
                    var i_nclte = (from c in edm_nclte.inf_prospectos
                                   where c.empresa.Contains(str_emp)
                                   select c).ToList();

                    if (i_nclte.Count == 0)
                    {
                        var i_fclte = (from c in edm_nclte.inf_prospectos
                                       select c).ToList();

                        if (i_fclte.Count == 0)
                        {
                            str_cod_prospecto = "LIEC-PROSP" + string.Format("{0:000}", (object)(i_nclte.Count + 1));

                            using (var m_prospecto = new dd_liecEntities())
                            {
                                var i_prospecto = new inf_prospectos

                                {
                                    id_prospecto = guid_prospecto,
                                    id_giro_prosp = int_giro,
                                    id_tserv_prosp = int_servicio,
                                    id_est_prospecto = 1,
                                    id_tipo_contprosp = int_tcont,
                                    empresa = str_emp,
                                    cod_prospecto = str_cod_prospecto,

                                    callenum = str_callenum,
                                    d_codigo = str_cp,
                                    id_asenta_cpcons = int_colonia,
                                    fecha_registro = DateTime.Now,
                                    id_emp = guid_emp,
                                    id_usuario = guid_idusr
                                };

                                m_prospecto.inf_prospectos.Add(i_prospecto);
                                m_prospecto.SaveChanges();

                                if (string.IsNullOrEmpty(str_contacto))
                                { }
                                else
                                {
                                    var i_cont_prosp = new inf_cont_prosp

                                    {
                                        id_cont_prosp = guid_contprosp,
                                        dpto = str_dpto,
                                        contacto = str_contacto,
                                        tel1 = str_tel1,
                                        email1 = str_email1,
                                        tel2 = str_tel2,
                                        email2 = str_email2,
                                        fecha_registro = DateTime.Now,
                                        id_usuario = guid_idusr,
                                        id_prospecto = guid_prospecto,
                                    };

                                    m_prospecto.inf_cont_prosp.Add(i_cont_prosp);
                                    m_prospecto.SaveChanges();
                                }
                                if (string.IsNullOrEmpty(str_coment))
                                {
                                }
                                else
                                {
                                    var i_prospecto_s = new inf_seg_prospecto

                                    {
                                        id_seg_prospecto = guid_sprospecto,
                                        id_taccion_prosp = int_accion,
                                        comentarios = str_coment,
                                        fecha_registro = DateTime.Now,
                                        id_cont_prosp = guid_contprosp,
                                        id_usuario = guid_idusr
                                    };
                                    m_prospecto.inf_seg_prospecto.Add(i_prospecto_s);
                                    m_prospecto.SaveChanges();
                                }
                            }

                            limp_txt_prospecto();
                            Mensaje("Datos de prospecto agregados con éxito.");
                        }
                        else
                        {
                            str_cod_prospecto = "LIEC-PROSP" + string.Format("{0:000}", (object)(i_fclte.Count + 1));

                            using (var m_prospecto = new dd_liecEntities())
                            {
                                var i_prospecto = new inf_prospectos

                                {
                                    id_prospecto = guid_prospecto,
                                    id_giro_prosp = int_giro,
                                    id_tserv_prosp = int_servicio,
                                    id_est_prospecto = 1,
                                    id_tipo_contprosp = int_tcont,
                                    empresa = str_emp,
                                    cod_prospecto = str_cod_prospecto,

                                    callenum = str_callenum,
                                    d_codigo = str_cp,
                                    id_asenta_cpcons = int_colonia,
                                    fecha_registro = DateTime.Now,
                                    id_emp = guid_emp,
                                    id_usuario = guid_idusr
                                };

                                m_prospecto.inf_prospectos.Add(i_prospecto);
                                m_prospecto.SaveChanges();

                                if (string.IsNullOrEmpty(str_contacto))
                                { }
                                else
                                {
                                    var i_cont_prosp = new inf_cont_prosp

                                    {
                                        id_cont_prosp = guid_contprosp,
                                        dpto = str_dpto,
                                        contacto = str_contacto,
                                        tel1 = str_tel1,
                                        email1 = str_email1,
                                        tel2 = str_tel2,
                                        email2 = str_email2,
                                        fecha_registro = DateTime.Now,
                                        id_usuario = guid_idusr,
                                        id_prospecto = guid_prospecto,
                                    };

                                    m_prospecto.inf_cont_prosp.Add(i_cont_prosp);
                                    m_prospecto.SaveChanges();
                                }
                                if (string.IsNullOrEmpty(str_coment))
                                {
                                }
                                else
                                {
                                    var i_prospecto_s = new inf_seg_prospecto

                                    {
                                        id_seg_prospecto = guid_sprospecto,
                                        id_taccion_prosp = int_accion,
                                        comentarios = str_coment,
                                        fecha_registro = DateTime.Now,
                                        id_cont_prosp = guid_contprosp,
                                        id_usuario = guid_idusr
                                    };
                                    m_prospecto.inf_seg_prospecto.Add(i_prospecto_s);
                                    m_prospecto.SaveChanges();
                                }
                            }

                            limp_txt_prospecto();
                            Mensaje("Datos de prospecto agregados con éxito.");
                        }
                    }
                    else
                    {
                        limp_txt_prospecto();
                        rfv_emp_prospecto.Enabled = false;
                        rfv_cont_prospecto.Enabled = false;

                        Mensaje("Cliente existe en la base de datos, favor de revisar.");
                    }
                }
            }
        }

        protected void chkb_desactivar_prospecto_CheckedChanged(object sender, EventArgs e)
        {
            int_prospecto = 0;
            i_agregar_prospecto.Attributes["style"] = "color:white";
            i_editar_prospecto.Attributes["style"] = "color:white";
            rfv_buscar_prospecto.Enabled = false;
            rfv_tipocont_prospecto.Enabled = false;
            rfv_emp_prospecto.Enabled = false;
            rfv_cont_prospecto.Enabled = false;
            rfv_colonia_prospecto.Enabled = false;
            rfv_acc_prospecto.Enabled = false;
            rfv_prospecto_coment.Enabled = false;
        }

        #endregion prospectos

        #region contactos_prospectos

        protected void btn_buscar_prospcontf_Click(object sender, EventArgs e)
        {
            limp_txt_contprosp();
            string str_rub = txt_buscar_prospcontf.Text.ToUpper().Trim();

            if (str_rub == "")
            {
            }
            else
            {
                try
                {
                    string n_rub;
                    Guid guid_fclte;
                    Char char_s = '|';
                    string d_rub = txt_buscar_prospcontf.Text.Trim();
                    String[] de_rub = d_rub.Trim().Split(char_s);

                    n_rub = de_rub[1].Trim();

                    using (dd_liecEntities edm_nclte = new dd_liecEntities())
                    {
                        var i_nclte = (from c in edm_nclte.inf_prospectos
                                       where c.cod_prospecto == n_rub
                                       select c).FirstOrDefault();

                        guid_fclte = i_nclte.id_prospecto;
                    }

                    using (dd_liecEntities data_user = new dd_liecEntities())
                    {
                        var inf_user = (from i_r in data_user.inf_cont_prosp
                                        where i_r.id_prospecto == guid_fclte
                                        select new
                                        {
                                            i_r.id_cont_prosp,
                                            i_r.dpto,
                                            i_r.contacto,
                                            i_r.fecha_registro,
                                        }).ToList();
                        gv_cont_prosp.DataSource = inf_user;
                        gv_cont_prosp.DataBind();
                        gv_cont_prosp.Visible = true;
                    }
                }
                catch
                {
                    limp_txt_contprosp();
                    div_prospecto.Visible = false;
                    Mensaje("Empresa no encontrado.");
                }
            }
        }

        protected void btn_agregar_prospcontf_Click(object sender, EventArgs e)
        {
            int_prospecto = 1;

            div_cont_prosp.Visible = true;

            limp_txt_contprosp();

            gv_cont_prosp.Visible = false;
            chkb_desactivar_prospecto.Checked = false;

            i_agregar_prospcontf.Attributes["style"] = "color:#E34C0E";
            i_editar_prospcontf.Attributes["style"] = "color:white";

            rfv_buscar_prospcontf.Enabled = false;
            rfv_cont_prospcontf.Enabled = true;
        }

        private void limp_txt_contprosp()
        {
            txt_dpto_prospcontf.Text = null;
            txt_cont_prospcontf.Text = null;
            txt_tel1_prospcontf.Text = null;
            txt_tel2_prospcontf.Text = null;
            txt_email1_prospcontf.Text = null;
            txt_email2_prospcontf.Text = null;
            ddl_acc_prospecto.Items.Clear();

            using (dd_liecEntities db_sepomex = new dd_liecEntities())
            {
                var tbl_sepomex = (from c in db_sepomex.fact_taccion_prosp
                                   select c).ToList();

                ddl_acc_prospecto.DataSource = tbl_sepomex;
                ddl_acc_prospecto.DataTextField = "desc_taccion_prosp";
                ddl_acc_prospecto.DataValueField = "id_taccion_prosp";
                ddl_acc_prospecto.DataBind();
            }
            ddl_acc_prospecto.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
        }

        protected void btn_editar_prospcontf_Click(object sender, EventArgs e)
        {
            limp_txt_contprosp();
            gv_cont_prosp.Visible = false;
            chkb_desactivar_prospecto.Checked = false;

            i_agregar_prospcontf.Attributes["style"] = "color:white";
            i_editar_prospcontf.Attributes["style"] = "color:#E34C0E";

            rfv_buscar_prospcontf.Enabled = true;

            rfv_cont_prospcontf.Enabled = false;
        }

        protected void btn_cp_prospecto_Click(object sender, EventArgs e)
        {
            string str_cp = txt_cp_prospecto.Text;

            using (dd_liecEntities db_sepomex = new dd_liecEntities())
            {
                var tbl_sepomex = (from c in db_sepomex.inf_sepomex
                                   where c.d_codigo == str_cp
                                   select c).ToList();

                ddl_colonia_prospecto.DataSource = tbl_sepomex;
                ddl_colonia_prospecto.DataTextField = "d_asenta";
                ddl_colonia_prospecto.DataValueField = "id_asenta_cpcons";
                ddl_colonia_prospecto.DataBind();

                if (tbl_sepomex.Count == 1)
                {
                    txt_municipio_prospecto.Text = tbl_sepomex[0].d_mnpio;
                    txt_estado_prospecto.Text = tbl_sepomex[0].d_estado;
                    rfv_colonia_prospecto.Enabled = true;
                }
                if (tbl_sepomex.Count > 1)
                {
                    ddl_colonia_prospecto.Items.Insert(0, new ListItem("SELECCIONAR", "0"));

                    txt_municipio_prospecto.Text = tbl_sepomex[0].d_mnpio;
                    txt_estado_prospecto.Text = tbl_sepomex[0].d_estado;
                    rfv_colonia_prospecto.Enabled = true;
                }
                else if (tbl_sepomex.Count == 0)
                {
                    ddl_colonia_prospecto.Items.Clear();
                    ddl_colonia_prospecto.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
                    txt_municipio_prospecto.Text = null;
                    txt_estado_prospecto.Text = null;
                    rfv_colonia_prospecto.Enabled = false;
                }
            }
            ddl_colonia_prospecto.Focus();
        }

        protected void chkb_desactivar_prospcontf_CheckedChanged(object sender, EventArgs e)
        {
            rfv_cont_prospcontf.Enabled = false;
        }

        protected void btn_guardar_prospcontf_Click(object sender, EventArgs e)
        {
            Guid guid_contprosp = Guid.NewGuid();
            string str_contacto = txt_cont_prospcontf.Text.ToUpper().Trim();
            string str_dpto = txt_dpto_prospcontf.Text.ToUpper().Trim();

            string str_tel1 = txt_tel1_prospcontf.Text.Trim();
            string str_tel2 = txt_tel2_prospcontf.Text.Trim();
            string str_email1 = txt_email1_prospcontf.Text.Trim();
            string str_email2 = txt_email2_prospcontf.Text.Trim();

            Guid str_fclte = Guid.Empty;
            string n_rub;

            Char char_s = '|';
            string d_rub = txt_buscar_prospcontf.Text.Trim();
            String[] de_rub = d_rub.Trim().Split(char_s);

            n_rub = de_rub[1].Trim();

            using (dd_liecEntities edm_nclte = new dd_liecEntities())
            {
                var i_nclte = (from c in edm_nclte.inf_prospectos
                               where c.cod_prospecto == n_rub
                               select c).FirstOrDefault();

                str_fclte = i_nclte.id_prospecto;
            }

            using (dd_liecEntities edm_nclte = new dd_liecEntities())
            {
                var i_nclte = (from c in edm_nclte.inf_cont_prosp
                               where c.dpto == str_dpto
                               where c.contacto == str_contacto
                               select c).ToList();

                if (i_nclte.Count == 0)
                {
                    using (var m_prospecto = new dd_liecEntities())
                    {
                        var i_cont_prospff = new inf_cont_prosp

                        {
                            id_cont_prosp = guid_contprosp,
                            dpto = str_dpto,
                            contacto = str_contacto,
                            tel1 = str_tel1,
                            email1 = str_email1,
                            tel2 = str_tel2,
                            email2 = str_email2,
                            fecha_registro = DateTime.Now,
                            id_usuario = guid_idusr,
                            id_prospecto = str_fclte,
                        };

                        m_prospecto.inf_cont_prosp.Add(i_cont_prospff);
                        m_prospecto.SaveChanges();
                    }
                    limp_txt_contprosp();
                    Mensaje("Datos de contacto agregados con exito.");
                }
                else
                {
                    limp_txt_contprosp();
                    Mensaje("Existe el mismo contacto en el mismo departamento, favor de revisar.");
                }
            }
    
        }

        protected void chk_cont_prosp_CheckedChanged(object sender, EventArgs e)
        {
            Guid guid_prospecto, guid_cont_prosp;
            int int_estatusID = 0;

            foreach (GridViewRow row in gv_cont_prosp.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chk_cont_prosp") as CheckBox);
                    if (chkRow.Checked)
                    {
                        int_estatusID = int_estatusID + 1;
                        row.BackColor = Color.FromArgb(227, 76, 14);
                        guid_prospecto = Guid.Parse(row.Cells[1].Text);

                        using (dd_liecEntities edm_prospecto = new dd_liecEntities())
                        {
                            try
                            {
                                var i_cont_pros = (from t_prospecto in edm_prospecto.inf_cont_prosp

                                                   where t_prospecto.id_cont_prosp == guid_prospecto
                                                   select new
                                                   {
                                                       t_prospecto.id_cont_prosp,
                                                       t_prospecto.dpto,
                                                       t_prospecto.contacto,
                                                       t_prospecto.tel1,
                                                       t_prospecto.tel2,
                                                       t_prospecto.email1,
                                                       t_prospecto.email2,
                                                   }).FirstOrDefault();
                                txt_dpto_prospcontf.Text = i_cont_pros.dpto;
                                txt_cont_prospcontf.Text = i_cont_pros.contacto;
                                txt_tel1_prospcontf.Text = i_cont_pros.tel1;
                                txt_email1_prospcontf.Text = i_cont_pros.email1;
                                txt_tel2_prospcontf.Text = i_cont_pros.tel2;
                                txt_email2_prospcontf.Text = i_cont_pros.email2;

                                guid_cont_prosp = i_cont_pros.id_cont_prosp;

                                var inf_user = (from i_r in edm_prospecto.inf_seg_prospecto
                                                join i_acc in edm_prospecto.fact_taccion_prosp on i_r.id_taccion_prosp equals i_acc.id_taccion_prosp
                                                join i_c in edm_prospecto.inf_usuarios on i_r.id_usuario equals i_c.id_usuario
                                                where i_r.id_cont_prosp == guid_cont_prosp
                                                where i_r.comentarios != ""
                                                select new
                                                {
                                                    i_acc.desc_taccion_prosp,
                                                    i_r.id_seg_prospecto,
                                                    i_r.id_taccion_prosp,
                                                    i_r.comentarios,
                                                    i_r.fecha_registro,
                                                    nom_usr = i_c.nombres + " " + i_c.a_paterno + " " + i_c.a_materno
                                                }).ToList();

                                ddl_acc_prospecto.Items.Clear();

                                using (dd_liecEntities db_sepomex = new dd_liecEntities())
                                {
                                    var tbl_sepomex = (from c in db_sepomex.fact_taccion_prosp
                                                       where c.id_taccion_prosp != 1
                                                       select c).ToList();

                                    ddl_acc_prospecto.DataSource = tbl_sepomex;
                                    ddl_acc_prospecto.DataTextField = "desc_taccion_prosp";
                                    ddl_acc_prospecto.DataValueField = "id_taccion_prosp";
                                    ddl_acc_prospecto.DataBind();
                                }
                                ddl_acc_prospecto.Items.Insert(0, new ListItem("SELECCIONAR", "0"));

                                rfv_emp_prospecto.Enabled = true;
                            }
                            catch
                            {
                                txt_cont_prospecto.Text = null;
                                txt_tel1_prospecto.Text = null;
                                txt_email1_prospecto.Text = null;
                                txt_tel2_prospecto.Text = null;
                                txt_email2_prospecto.Text = null;
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
                limp_txt_prospecto();
            }
        }

        #endregion contactos_prospectos

        private void limp_txt_prospecto()
        {
            txt_emp_prospecto.Text = null;
            txt_cont_prospecto.Text = null;
            txt_tel1_prospecto.Text = null;
            txt_email1_prospecto.Text = null;
            txt_tel2_prospecto.Text = null;
            txt_email2_prospecto.Text = null;
            txt_callenum_prospecto.Text = null;
            txt_cp_prospecto.Text = null;
            txt_municipio_prospecto.Text = null;
            txt_estado_prospecto.Text = null;
            txt_dpto_prosp.Text = null;
            txt_prospecto_coment.Text = null;
            carga_ddl();
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