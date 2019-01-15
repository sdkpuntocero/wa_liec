using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wa_liec
{
    public partial class pnl_cont : System.Web.UI.Page
    {
        static private int acc_rubro, acc_gasto, acc_caja, int_pnlID, int_idperf;
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

                switch (int_idperf)
                {
                    case 1:

                        break;

                    case 2:

                        break;

                    default:

                        break;
                }
            }
        }

        protected void lkb_salir_Click(object sender, EventArgs e)
        {
            Response.Redirect("acceso.aspx");
        }

        #region funciones

        [ScriptMethod()]
        [WebMethod]
        public static List<string> SearchCustomers(string prefixText, int count)
        {
            List<String> columnData = new List<String>();
            string d_rub = prefixText.ToUpper();

            if (int_pnlID == 1)
            {
                //using (SqlConnection connection = new SqlConnection(cn.cn_SQL))
                //{
                //    connection.Open();
                //    string query = "SELECT etiqueta_rubro,rubro,codigo_rubro FROM [dd_liec].[dbo].[inf_rubro]  WHERE etiqueta_rubro LIKE '" + d_rub + "%' ";
                //    using (SqlCommand command = new SqlCommand(query, connection))
                //    {
                //        using (SqlDataReader reader = command.ExecuteReader())
                //        {
                //            while (reader.Read())
                //            {
                //                columnData.Add(reader.GetString(0));
                //            }
                //        }
                //    }
                //}

                using (dd_liecEntities m_rub = new dd_liecEntities())
                {
                    var i_rub = (from i_u in m_rub.inf_rubro
                                 where i_u.etiqueta_rubro.Contains(d_rub)
                                 select new
                                 {
                                     i_u.etiqueta_rubro,
                                     i_u.rubro,
                                     i_u.codigo_rubro,
                                 }).ToList();

                    foreach (var f_rub in i_rub)
                    {
                        columnData.Add(f_rub.ToString());
                    }
                }
            }
            else if (int_pnlID == 2)
            {
                string f_desccajaf = prefixText.ToUpper();

                using (dd_liecEntities m_rub = new dd_liecEntities())
                {
                    var i_rub = (from i_u in m_rub.inf_rubro
                                 where i_u.etiqueta_rubro.Contains(d_rub)

                                 select new
                                 {
                                     i_u.etiqueta_rubro,
                                     i_u.rubro,
                                     i_u.codigo_rubro,
                                 }).ToList();

                    foreach (var ff_rub in i_rub)
                    {
                        columnData.Add(ff_rub.etiqueta_rubro + " | " + ff_rub.rubro + " | " + ff_rub.codigo_rubro);
                    }
                }
            }
            else if (int_pnlID == 3)
            {
                string f_rub = prefixText.ToUpper();

                using (SqlConnection connection = new SqlConnection(cn.cn_SQL))
                {
                    connection.Open();
                    string query = "SELECT  [desc_gasto],[codigo_gasto]  FROM [dd_liec].[dbo].[inf_gastos]  WHERE [desc_gasto] LIKE '" + f_rub + "%' ";
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
            else if (int_pnlID == 4)
            {
                string f_rub = prefixText.ToUpper();

                using (SqlConnection connection = new SqlConnection(cn.cn_SQL))
                {
                    connection.Open();
                    string query = "SELECT  [desc_caja],[codigo_caja]  FROM [dd_liec].[dbo].[inf_caja]  WHERE [desc_caja] LIKE '" + f_rub + "%' ";
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

        private void enviarcorreo(string correo_e, string usuario_e, string clave_e, string asunto_e, string detale_e, string smtp_e, int puerto_e, DateTime registro_e, string correo_r, string trubro_e, string descrubro, string montor, string montog, string montop, string usuario_reg)
        {
            string cuerpo_e = createEmailBody(detale_e, trubro_e, descrubro, montor, montog, montop, usuario_reg, registro_e);

            SendHtmlFormattedEmail(correo_e, asunto_e, cuerpo_e, correo_r, smtp_e, puerto_e, usuario_e, clave_e);
        }

        public string createEmailBody(string detale_e, string trubro_e, string descrubro, string montor, string montog, string montop, string usuario_reg, DateTime registro_e)

        {
            string body = string.Empty;
            //using streamreader for reading my htmltemplate

            using (StreamReader reader = new StreamReader(Server.MapPath("~/HtmlTemplate.html")))

            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{detale_e}", detale_e); //replacing the required things

            body = body.Replace("{trubro_e}", trubro_e);

            body = body.Replace("{descrubro}", descrubro);

            body = body.Replace("{montor}", montor);

            body = body.Replace("{montog}", montog);

            body = body.Replace("{montop}", montop);

            body = body.Replace("{usuario_reg}", usuario_reg);

            body = body.Replace("{registro}", registro_e.ToShortDateString());

            return body;
        }

        private void SendHtmlFormattedEmail(string correo_e, string asunto_e, string cuerpo_e, string correo_r, string smtp_e, int puerto_e, string usuario_e, string clave_e)

        {
            using (MailMessage mailMessage = new MailMessage())

            {
                mailMessage.From = new MailAddress(correo_e);

                mailMessage.Subject = asunto_e;

                mailMessage.Body = cuerpo_e;

                mailMessage.IsBodyHtml = true;

                mailMessage.To.Add(new MailAddress("svaldes@liec.com.mx"));
                mailMessage.CC.Add(new MailAddress("egarcia@liec.com.mx"));
                SmtpClient smtp = new SmtpClient();

                smtp.Host = smtp_e;

                smtp.EnableSsl = true;

                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();

                NetworkCred.UserName = usuario_e;

                NetworkCred.Password = clave_e;
                smtp.UseDefaultCredentials = true;

                smtp.Credentials = NetworkCred;

                smtp.Port = puerto_e;

                try
                {
                    smtp.Send(mailMessage);
                }
                catch
                {
                }
            }
        }

        #endregion funciones

        #region rubro

        protected void lkb_cont_rub_Click(object sender, EventArgs e)
        {
            acc_rubro = 0;
            int_pnlID = 2;

            pnl_gasto.Visible = false;
            up_gasto.Update();

            pnl_caja.Visible = false;
            up_caja.Update();

            pnl_rubro.Visible = true;
            div_busc_rub.Visible = false;
            i_agr_rubro.Attributes["style"] = "color:white";
            i_edit_rubro.Attributes["style"] = "color:white";
            gv_rubro.Visible = false;
            limpia_txt_rubro();
            up_rubro.Update();
        }

        protected void btn_agr_rubro_Click(object sender, EventArgs e)
        {
            acc_rubro = 1;

            div_busc_rub.Visible = false;

            gv_rubro.Visible = false;

            monto_extra.Enabled = false;

            limpia_txt_rubro();

            i_agr_rubro.Attributes["style"] = "color:#E34C0E";
            i_edit_rubro.Attributes["style"] = "color:white";

            rfv_eti_rub.Enabled = true;
            rfv_desc_rubro.Enabled = true;
            rfv_tipo_rubro.Enabled = true;
            rfv_mont_rubro.Enabled = true;
            rfv_minimo_rubro.Enabled = true;
            rfv_maximo_rubro.Enabled = true;
            rfv_pextra_rubro.Enabled = false;
        }

        protected void btn_edit_rubro_Click(object sender, EventArgs e)
        {
            acc_rubro = 2;

            div_busc_rub.Visible = true;
            rfv_buscar_rub.Enabled = true;
            monto_extra.Enabled = false;

            limpia_txt_rubro();

            i_agr_rubro.Attributes["style"] = "color:white";
            i_edit_rubro.Attributes["style"] = "color:#E34C0E";
        }

        protected void chkb_des_rubro_CheckedChanged(object sender, EventArgs e)
        {
            acc_rubro = 0;
            rfv_eti_rub.Enabled = false;
            rfv_desc_rubro.Enabled = false;
            rfv_tipo_rubro.Enabled = false;
            rfv_mont_rubro.Enabled = false;
            rfv_minimo_rubro.Enabled = false;
            rfv_maximo_rubro.Enabled = false;
            rfv_pextra_rubro.Enabled = false;
            rfv_buscar_rub.Enabled = false;
        }

        protected void mont_rubro_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(mont_rubro.Text))
            {
                rfv_minimo_rubro.Enabled = true;
            }
            else
            {
                string stringTest = mont_rubro.Text.Trim().Replace("$", "").Replace(",", "");

                try
                {
                    decimal moneyvalue = decimal.Parse(stringTest);
                    string html = String.Format("{0:C}", moneyvalue);

                    mont_rubro.Text = html;
                    mont_rubro.Focus();
                }
                catch
                {
                    mont_rubro.Text = null;
                }
            }
        }

        protected void btn_guardar_rubro_Click(object sender, EventArgs e)
        {
            int t_rub, min_rub = 0, max_rub = 0;
            string s_eti_rub, s_desc_rub, str_cod_rub;
            decimal d_mont_fijo, d_mont_ext, dbl_pextra;
            Guid guid_nrubro, guid_nrubrom, guid_emp;

            if (acc_rubro == 0)
            {
                Mensaje("Favor de seleccionar una acción");
            }
            else
            {
                if (acc_rubro == 2)
                {
                    int int_estatusID = 0;
                    string f_rub = null;
                    foreach (GridViewRow row in gv_rubro.Rows)
                    {
                        // int key = (int)GridView1.DataKeys[row.RowIndex].Value;
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[0].FindControl("chk_rubro") as CheckBox);
                            if (chkRow.Checked)
                            {
                                int_estatusID = int_estatusID + 1;
                                break;
                            }
                            else
                            {
                                int_estatusID = 0;
                            }
                        }
                    }

                    if (int_estatusID >= 1)
                    {
                        guid_nrubro = Guid.NewGuid();
                        guid_nrubrom = Guid.NewGuid();

                        t_rub = int.Parse(ddl_tipo_rubro.SelectedValue);
                        s_eti_rub = eti_rub.Text.Trim().ToUpper();
                        s_desc_rub = desc_rubro.Text.Trim().ToUpper();
                        min_rub = int.Parse(minimo_rubro.Text);
                        max_rub = int.Parse(maximo_rubro.Text);
                        d_mont_fijo = decimal.Parse(mont_rubro.Text.Replace("$", ""));

                        if ((min_rub + max_rub) == 0 || (min_rub + max_rub) > 100 || (min_rub + max_rub) < 100)
                        {
                            minimo_rubro.Text = null;
                            maximo_rubro.Text = null;
                            Mensaje("suma de minimo y máximo debe ser igual 100");
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(monto_extra.Text))
                            {
                            }
                            else
                            {
                                d_mont_ext = decimal.Parse(monto_extra.Text.Replace("$", ""));
                            }

                            Guid guid_idrubro;
                            int int_ddl, int_f_rub = 0;

                            foreach (GridViewRow row in gv_rubro.Rows)
                            {
                                // int key = (int)GridView1.DataKeys[row.RowIndex].Value;
                                if (row.RowType == DataControlRowType.DataRow)
                                {
                                    CheckBox chkRow = (row.Cells[0].FindControl("chk_rubro") as CheckBox);
                                    if (chkRow.Checked)
                                    {
                                        int_f_rub = int_f_rub + 1;
                                        f_rub = row.Cells[1].Text;

                                        DropDownList dl = (DropDownList)row.FindControl("ddl_rub_est");

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
                            using (dd_liecEntities data_user = new dd_liecEntities())
                            {
                                var items_user = (from c in data_user.inf_rubro
                                                  where c.codigo_rubro == f_rub
                                                  select c).FirstOrDefault();

                                guid_idrubro = items_user.id_rubro;
                            }

                            if (string.IsNullOrEmpty(monto_extra.Text))
                            {
                                d_mont_ext = 0;
                            }
                            else
                            {
                                d_mont_ext = decimal.Parse(monto_extra.Text);
                            }

                            using (var m_nrubro = new dd_liecEntities())
                            {
                                var i_nrubro = (from c in m_nrubro.inf_rubro
                                                where c.id_rubro == guid_idrubro
                                                select c).FirstOrDefault();

                                i_nrubro.id_est_rub = int_estatusID;
                                i_nrubro.id_tipo_rubro = t_rub;
                                i_nrubro.etiqueta_rubro = s_eti_rub;
                                i_nrubro.rubro = s_desc_rub;

                                m_nrubro.SaveChanges();

                                var i_nrubrom = (from c in m_nrubro.inf_rubro_mes
                                                 where c.id_rubro == guid_idrubro
                                                 select c).FirstOrDefault();

                                DateTime f_rubrom = DateTime.Parse(i_nrubrom.fecha_registro.ToString());
                                DateTime f_actua = DateTime.Now;

                                if (f_rubrom.Month == f_actua.Month)
                                {
                                    i_nrubrom.monto_fijo = d_mont_fijo;
                                    i_nrubrom.monto_extra = d_mont_ext;
                                    i_nrubrom.minimo = min_rub;
                                    i_nrubrom.maximo = max_rub;
                                    i_nrubrom.min = 0;
                                    i_nrubrom.max = 0;

                                    m_nrubro.SaveChanges();
                                }
                                else
                                {
                                    i_nrubrom.id_est_rubm = 2;
                                    m_nrubro.SaveChanges();

                                    var i_nrubm = new inf_rubro_mes
                                    {
                                        id_rubro_mes = guid_nrubrom,
                                        id_est_rubm = 1,
                                        monto_fijo = d_mont_fijo,
                                        minimo = min_rub,
                                        maximo = max_rub,
                                        monto_extra = 0,
                                        min = 0,
                                        max = 0,
                                        fecha_registro = DateTime.Now,
                                        id_rubro = guid_idrubro
                                    };

                                    m_nrubro.inf_rubro_mes.Add(i_nrubm);
                                    m_nrubro.SaveChanges();
                                }
                            }

                            limpia_txt_rubro();

                            rfv_eti_rub.Enabled = false;
                            rfv_desc_rubro.Enabled = false;
                            rfv_tipo_rubro.Enabled = false;
                            rfv_mont_rubro.Enabled = false;
                            rfv_minimo_rubro.Enabled = false;
                            rfv_maximo_rubro.Enabled = false;
                            rfv_pextra_rubro.Enabled = false;
                            gv_rubro.Visible = false;
                            Mensaje("Datos actualizados con éxito.");
                        }
                    }
                    else
                    {
                        Mensaje("Favor de seleccionar una un rubro");
                    }
                }
                else
                {
                    guid_nrubro = Guid.NewGuid();
                    guid_nrubrom = Guid.NewGuid();
                    guid_emp = Guid.Parse("D8A03556-6791-45F3-BABE-ECF267B865F1");
                    t_rub = int.Parse(ddl_tipo_rubro.SelectedValue);
                    s_eti_rub = eti_rub.Text.Trim().ToUpper();
                    s_desc_rub = desc_rubro.Text.Trim().ToUpper();
                    min_rub = int.Parse(minimo_rubro.Text);
                    max_rub = int.Parse(maximo_rubro.Text);
                    d_mont_fijo = decimal.Parse(mont_rubro.Text.Replace("$", ""));

                    if ((min_rub + max_rub) == 0 || (min_rub + max_rub) > 100 || (min_rub + max_rub) < 100)
                    {
                        minimo_rubro.Text = null;
                        maximo_rubro.Text = null;
                        Mensaje("suma de minimo y máximo debe ser igual 100");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(monto_extra.Text))
                        {
                        }
                        else
                        {
                            d_mont_ext = decimal.Parse(monto_extra.Text.Replace("$", ""));
                        }

                        using (dd_liecEntities edm_rub = new dd_liecEntities())
                        {
                            var i_rub = (from c in edm_rub.inf_rubro
                                         where c.id_tipo_rubro == t_rub
                                         where c.etiqueta_rubro == s_eti_rub
                                         select c).ToList();

                            if (i_rub.Count == 0)
                            {
                                var c_rub = (from c in edm_rub.inf_rubro
                                             select c).ToList();
                                if (c_rub.Count == 0)
                                {
                                    str_cod_rub = "LIEC-R" + string.Format("{0:000}", c_rub.Count + 1);

                                    var i_nrub = new inf_rubro
                                    {
                                        id_rubro = guid_nrubro,
                                        id_est_rub = 1,
                                        codigo_rubro = str_cod_rub,
                                        id_tipo_rubro = t_rub,
                                        etiqueta_rubro = s_eti_rub,
                                        rubro = s_desc_rub,
                                        id_usuario = guid_idusr,
                                        fecha_registro = DateTime.Now,
                                        id_emp = guid_emp
                                    };

                                    edm_rub.inf_rubro.Add(i_nrub);
                                    edm_rub.SaveChanges();

                                    var i_nrubm = new inf_rubro_mes
                                    {
                                        id_rubro_mes = guid_nrubrom,
                                        id_est_rubm = 1,
                                        monto_fijo = d_mont_fijo,
                                        minimo = min_rub,
                                        maximo = max_rub,
                                        min = 0,
                                        max = 0,
                                        monto_extra = 0,
                                        fecha_registro = DateTime.Now,
                                        id_rubro = guid_nrubro
                                    };

                                    edm_rub.inf_rubro_mes.Add(i_nrubm);
                                    edm_rub.SaveChanges();

                                    limpia_txt_rubro();
                                    Mensaje("Datos agregados con éxito.");
                                }
                                else
                                {
                                    str_cod_rub = "LIEC-R" + string.Format("{0:000}", c_rub.Count + 1);

                                    var i_nrub = new inf_rubro
                                    {
                                        id_rubro = guid_nrubro,
                                        id_est_rub = 1,
                                        codigo_rubro = str_cod_rub,
                                        id_tipo_rubro = t_rub,
                                        etiqueta_rubro = s_eti_rub,
                                        rubro = s_desc_rub,
                                        fecha_registro = DateTime.Now,
                                        id_usuario = guid_idusr,
                                        id_emp = guid_emp
                                    };

                                    edm_rub.inf_rubro.Add(i_nrub);
                                    edm_rub.SaveChanges();

                                    var i_nrubm = new inf_rubro_mes
                                    {
                                        id_rubro_mes = guid_nrubrom,
                                        id_est_rubm = 1,
                                        monto_fijo = d_mont_fijo,
                                        minimo = min_rub,
                                        maximo = max_rub,
                                        min = 0,
                                        max = 0,
                                        monto_extra = 0,
                                        fecha_registro = DateTime.Now,
                                        id_rubro = guid_nrubro
                                    };

                                    edm_rub.inf_rubro_mes.Add(i_nrubm);
                                    edm_rub.SaveChanges();

                                    limpia_txt_rubro();
                                    Mensaje("Datos agregados con éxito.");
                                }
                            }
                            else
                            {
                                Mensaje("Tipo de rubro y etiqueta, ya existen en la base de datos, favor de re-intentar.");
                            }
                        }
                    }
                }
            }
        }

        protected void btn_buscar_rub_Click(object sender, EventArgs e)
        {
            limpia_txt_rubro();
            string str_rub = txt_buscar_rub.Text.ToUpper().Trim();

            if (str_rub == "TODO")
            {
                using (dd_liecEntities data_user = new dd_liecEntities())
                {
                    var inf_user = (from i_r in data_user.inf_rubro
                                    join t_r in data_user.fact_tipo_rubro on i_r.id_tipo_rubro equals t_r.id_tipo_rubro
                                    select new
                                    {
                                        i_r.codigo_rubro,
                                        t_r.tipo_rubro,
                                        i_r.etiqueta_rubro,
                                        i_r.rubro,
                                        i_r.fecha_registro
                                    }).OrderBy(x => x.codigo_rubro).ToList();

                    if (inf_user.Count == 0)
                    {
                        gv_rubro.DataSource = inf_user;
                        gv_rubro.DataBind();
                        gv_rubro.Visible = true;

                        Mensaje("Rubro no encontrado.");
                    }
                    else
                    {
                        gv_rubro.DataSource = inf_user;
                        gv_rubro.DataBind();
                        gv_rubro.Visible = true;
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
                    string d_rub = txt_buscar_rub.Text.Trim();
                    String[] de_rub = d_rub.Trim().Split(char_s);

                    n_rub = de_rub[2].Trim();

                    using (dd_liecEntities edm_nclte = new dd_liecEntities())
                    {
                        var i_nclte = (from c in edm_nclte.inf_rubro
                                       where c.codigo_rubro == n_rub
                                       select c).FirstOrDefault();

                        guid_fclte = i_nclte.id_rubro;
                    }

                    using (dd_liecEntities data_user = new dd_liecEntities())
                    {
                        var inf_user = (from i_r in data_user.inf_rubro
                                        join t_r in data_user.fact_tipo_rubro on i_r.id_tipo_rubro equals t_r.id_tipo_rubro
                                        where i_r.id_rubro == guid_fclte

                                        select new
                                        {
                                            i_r.codigo_rubro,
                                            t_r.tipo_rubro,
                                            i_r.etiqueta_rubro,
                                            i_r.rubro,
                                            i_r.fecha_registro
                                        }).OrderBy(x => x.codigo_rubro).ToList();

                        if (inf_user.Count == 0)
                        {
                            gv_rubro.DataSource = inf_user;
                            gv_rubro.DataBind();
                            gv_rubro.Visible = true;

                            Mensaje("Rubro no encontrado.");
                        }
                        else
                        {
                            gv_rubro.DataSource = inf_user;
                            gv_rubro.DataBind();
                            gv_rubro.Visible = true;
                        }
                    }
                }
                catch
                {
                    limpia_txt_rubro();
                    Mensaje("Rubro no encontrado.");
                }
            }
        }

        protected void gv_rubro_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string str_clteID = e.Row.Cells[1].Text;
                int int_estatusID;

                using (dd_liecEntities data_clte = new dd_liecEntities())
                {
                    var i_clte = (from t_clte in data_clte.inf_rubro
                                  where t_clte.codigo_rubro == str_clteID
                                  select new
                                  {
                                      t_clte.id_est_rub,
                                  }).FirstOrDefault();

                    int_estatusID = int.Parse(i_clte.id_est_rub.ToString());
                }

                DropDownList DropDownList1 = (e.Row.FindControl("ddl_rub_est") as DropDownList);

                using (dd_liecEntities db_sepomex = new dd_liecEntities())
                {
                    var tbl_sepomex = (from c in db_sepomex.fact_est_rub
                                       select c).ToList();

                    DropDownList1.DataSource = tbl_sepomex;

                    DropDownList1.DataTextField = "desc_est_rub";
                    DropDownList1.DataValueField = "id_est_rub";
                    DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
                    DropDownList1.SelectedValue = int_estatusID.ToString();
                }
            }
        }

        protected void chk_rubro_CheckedChanged(object sender, EventArgs e)
        {
            Guid guid_rub;
            string str_rub;

            foreach (GridViewRow row in gv_rubro.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chk_rubro") as CheckBox);
                    if (chkRow.Checked)
                    {
                        row.BackColor = Color.FromArgb(227, 76, 14);
                        str_rub = row.Cells[1].Text;

                        using (dd_liecEntities edm_rub = new dd_liecEntities())
                        {
                            var i_rub = (from c in edm_rub.inf_rubro
                                         where c.codigo_rubro == str_rub
                                         select c).FirstOrDefault();

                            guid_rub = i_rub.id_rubro;

                            var f_rub = (from r in edm_rub.inf_rubro
                                         where r.id_rubro == guid_rub
                                         select new
                                         {
                                             r.id_tipo_rubro,
                                             r.etiqueta_rubro,
                                             r.rubro,
                                         }).FirstOrDefault();

                            try
                            {
                                var f_rubm = (from r in edm_rub.inf_rubro_mes
                                              where r.id_rubro == guid_rub
                                              where r.id_est_rubm == 1
                                              where r.fecha_registro.Value.Month == DateTime.Now.Month 
                                              select new
                                              {
                                                  r.monto_fijo,
                                                  r.monto_extra,
                                                  r.minimo,
                                                  r.maximo
                                              }).FirstOrDefault();

                                ddl_tipo_rubro.SelectedValue = f_rub.id_tipo_rubro.ToString();
                                eti_rub.Text = f_rub.etiqueta_rubro;
                                desc_rubro.Text = f_rub.rubro;
                                decimal moneyvalue = decimal.Parse(f_rubm.monto_fijo.ToString());
                                string monto_rub = String.Format("{0:C}", moneyvalue);
                                mont_rubro.Text = monto_rub;
                                minimo_rubro.Text = f_rubm.minimo.ToString();
                                maximo_rubro.Text = f_rubm.maximo.ToString();
                            }
                            catch
                            {
                                //cambiar por fin de año el filtro del mes

                                var f_rubm = (from r in edm_rub.inf_rubro_mes
                                              where r.id_rubro == guid_rub
                                              where r.id_est_rubm == 1
                                              where r.fecha_registro.Value.Month == DateTime.Now.Month + 11
                                              select new
                                              {
                                                  r.monto_fijo,
                                                  r.monto_extra,
                                                  r.minimo,
                                                  r.maximo
                                              }).FirstOrDefault();

                                ddl_tipo_rubro.SelectedValue = f_rub.id_tipo_rubro.ToString();
                                eti_rub.Text = f_rub.etiqueta_rubro;
                                desc_rubro.Text = f_rub.rubro;
                                decimal moneyvalue = decimal.Parse(f_rubm.monto_fijo.ToString());
                                string monto_rub = String.Format("{0:C}", moneyvalue);
                                mont_rubro.Text = monto_rub;
                                minimo_rubro.Text = f_rubm.minimo.ToString();
                                maximo_rubro.Text = f_rubm.maximo.ToString();
                            }
                        }
                    }
                    else
                    {
                        row.BackColor = Color.White;
                    }
                }
            }
        }

        protected void gv_rubro_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_rubro.PageIndex = e.NewPageIndex;

            string str_rub = txt_buscar_rub.Text.ToUpper().Trim();

            if (str_rub == "TODO")
            {
                using (dd_liecEntities data_user = new dd_liecEntities())
                {
                    var inf_user = (from i_r in data_user.inf_rubro
                                    join t_r in data_user.fact_tipo_rubro on i_r.id_tipo_rubro equals t_r.id_tipo_rubro
                                    select new
                                    {
                                        i_r.codigo_rubro,
                                        t_r.tipo_rubro,
                                        i_r.etiqueta_rubro,
                                        i_r.rubro,
                                        i_r.fecha_registro
                                    }).OrderBy(x => x.codigo_rubro).ToList();

                    if (inf_user.Count == 0)
                    {
                        gv_rubro.DataSource = inf_user;
                        gv_rubro.DataBind();
                        gv_rubro.Visible = true;

                        Mensaje("Rubro no encontrado.");
                    }
                    else
                    {
                        gv_rubro.DataSource = inf_user;
                        gv_rubro.DataBind();
                        gv_rubro.Visible = true;
                    }
                }
                rfv_eti_rub.Enabled = true;
                rfv_desc_rubro.Enabled = true;
                rfv_tipo_rubro.Enabled = true;
                rfv_mont_rubro.Enabled = true;
                rfv_minimo_rubro.Enabled = true;
                rfv_maximo_rubro.Enabled = true;
                rfv_pextra_rubro.Enabled = false;
            }
            else
            {
                using (dd_liecEntities data_user = new dd_liecEntities())
                {
                    var inf_user = (from i_r in data_user.inf_rubro
                                    join t_r in data_user.fact_tipo_rubro on i_r.id_tipo_rubro equals t_r.id_tipo_rubro
                                    where i_r.etiqueta_rubro.Contains(str_rub)
                                    select new
                                    {
                                        i_r.codigo_rubro,
                                        t_r.tipo_rubro,
                                        i_r.etiqueta_rubro,
                                        i_r.rubro,
                                        i_r.fecha_registro
                                    }).OrderBy(x => x.codigo_rubro).ToList();

                    if (inf_user.Count == 0)
                    {
                        gv_rubro.DataSource = inf_user;
                        gv_rubro.DataBind();
                        gv_rubro.Visible = true;

                        Mensaje("Rubro no encontrado.");
                    }
                    else
                    {
                        gv_rubro.DataSource = inf_user;
                        gv_rubro.DataBind();
                        gv_rubro.Visible = true;
                    }
                }
                rfv_eti_rub.Enabled = true;
                rfv_desc_rubro.Enabled = true;
                rfv_tipo_rubro.Enabled = true;
                rfv_mont_rubro.Enabled = true;
                rfv_minimo_rubro.Enabled = true;
                rfv_maximo_rubro.Enabled = true;
                rfv_pextra_rubro.Enabled = false;
            }
        }

        protected void ddl_rub_est_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void limpia_txt_rubro()
        {
            ddl_tipo_rubro.Items.Clear();
            using (dd_liecEntities m_genero = new dd_liecEntities())
            {
                var i_genero = (from f_tr in m_genero.fact_tipo_rubro
                                select f_tr).ToList();

                ddl_tipo_rubro.DataSource = i_genero;
                ddl_tipo_rubro.DataTextField = "desc_tipo_rubro";
                ddl_tipo_rubro.DataValueField = "id_tipo_rubro";
                ddl_tipo_rubro.DataBind();
            }
            ddl_tipo_rubro.Items.Insert(0, new ListItem("SELECCIONAR", "0"));

            desc_rubro.Text = null;
            mont_rubro.Text = null;
            minimo_rubro.Text = null;
            maximo_rubro.Text = null;
            monto_extra.Text = null;
            monto_gastado.Text = null;
            eti_rub.Text = null;
        }

        #endregion rubro

        #region gastos

        protected void lkb_cont_gast_Click(object sender, EventArgs e)
        {
            acc_gasto = 0;
            int_pnlID = 3;

            pnl_rubro.Visible = false;
            up_rubro.Update();

            pnl_caja.Visible = false;
            up_caja.Update();

            pnl_gasto.Visible = true;
            div_busc_rub.Visible = false;
            i_agr_gasto.Attributes["style"] = "color:white";
            i_edit_gasto.Attributes["style"] = "color:white";
            gv_gasto.Visible = false;
            limpia_txt_gasto();
            up_gasto.Update();
        }

        private void limpia_txt_gasto()
        {
            ddl_tipo_gasto.Items.Clear();
            using (dd_liecEntities m_genero = new dd_liecEntities())
            {
                var i_genero = (from f_tr in m_genero.fact_tipo_rubro
                                select f_tr).ToList();

                ddl_tipo_gasto.DataSource = i_genero;
                ddl_tipo_gasto.DataTextField = "tipo_rubro";
                ddl_tipo_gasto.DataValueField = "id_tipo_rubro";
                ddl_tipo_gasto.DataBind();
            }
            ddl_tipo_gasto.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
            ddl_eti_gasto.Items.Clear();
            ddl_eti_gasto.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
            eti_rub.Text = null;
            desc_gasto.Text = null;
            mont_gasto.Text = null;
        }

        protected void btn_buscar_gasto_Click(object sender, EventArgs e)
        {
            string str_rub = txt_buscar_gasto.Text.ToUpper().Trim();

            if (str_rub == "TODO")
            {
                using (dd_liecEntities data_user = new dd_liecEntities())
                {
                    var inf_user = (from i_g in data_user.inf_gastos
                                    join t_r in data_user.fact_tipo_rubro on i_g.id_tipo_rubro equals t_r.id_tipo_rubro
                                    join i_r in data_user.inf_rubro on i_g.id_rubro equals i_r.id_rubro
                                    select new
                                    {
                                        i_g.codigo_gasto,
                                        t_r.tipo_rubro,
                                        i_r.etiqueta_rubro,
                                        i_g.desc_gasto,
                                        i_g.fecha_registro
                                    }).OrderBy(x => x.codigo_gasto).ToList();

                    if (inf_user.Count == 0)
                    {
                        gv_gasto.DataSource = inf_user;
                        gv_gasto.DataBind();
                        gv_gasto.Visible = true;

                        Mensaje("Rubro no encontrado.");
                    }
                    else
                    {
                        gv_gasto.DataSource = inf_user;
                        gv_gasto.DataBind();
                        gv_gasto.Visible = true;
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
                    string d_rub = txt_buscar_gasto.Text.Trim();
                    String[] de_rub = d_rub.Trim().Split(char_s);

                    n_rub = de_rub[1].Trim();

                    using (dd_liecEntities edm_nclte = new dd_liecEntities())
                    {
                        var i_nclte = (from c in edm_nclte.inf_gastos
                                       where c.codigo_gasto == n_rub
                                       select c).FirstOrDefault();

                        guid_fclte = i_nclte.id_gasto;
                    }

                    using (dd_liecEntities data_user = new dd_liecEntities())
                    {
                        var inf_user = (from i_g in data_user.inf_gastos
                                        join t_r in data_user.fact_tipo_rubro on i_g.id_tipo_rubro equals t_r.id_tipo_rubro
                                        join i_r in data_user.inf_rubro on i_g.id_rubro equals i_r.id_rubro
                                        where i_g.id_gasto == guid_fclte
                                        select new
                                        {
                                            i_g.codigo_gasto,
                                            t_r.tipo_rubro,
                                            i_r.etiqueta_rubro,
                                            i_g.desc_gasto,
                                            i_g.fecha_registro
                                        }).OrderBy(x => x.codigo_gasto).ToList();

                        if (inf_user.Count == 0)
                        {
                            gv_gasto.DataSource = inf_user;
                            gv_gasto.DataBind();
                            gv_gasto.Visible = true;

                            Mensaje("Rubro no encontrado.");
                        }
                        else
                        {
                            gv_gasto.DataSource = inf_user;
                            gv_gasto.DataBind();
                            gv_gasto.Visible = true;
                        }
                    }
                }
                catch
                {
                    limpia_txt_gasto();
                    Mensaje("Rubro no encontrado.");
                }
            }
        }

        protected void btn_agr_gasto_Click(object sender, EventArgs e)
        {
            acc_gasto = 1;

            div_busc_rub.Visible = false;

            gv_gasto.Visible = false;

            monto_extra.Enabled = false;

            limpia_txt_gasto();

            i_agr_gasto.Attributes["style"] = "color:#E34C0E";
            i_edit_gasto.Attributes["style"] = "color:white";

            rfv_eti_gasto.Enabled = true;
            rfv_desc_gasto.Enabled = true;
            rfv_tipo_gasto.Enabled = true;
            rfv_mont_gasto.Enabled = true;
        }

        protected void btn_edit_gasto_Click(object sender, EventArgs e)
        {
            acc_gasto = 2;

            div_busc_gasto.Visible = true;
            rfv_buscar_gasto.Enabled = true;
            monto_extra.Enabled = false;

            limpia_txt_gasto();

            i_agr_gasto.Attributes["style"] = "color:white";
            i_edit_gasto.Attributes["style"] = "color:#E34C0E";
        }

        protected void chkb_des_gasto_CheckedChanged(object sender, EventArgs e)
        {
            acc_gasto = 0;
            rfv_eti_gasto.Enabled = false;
            rfv_desc_gasto.Enabled = false;
            rfv_tipo_gasto.Enabled = false;
            rfv_mont_gasto.Enabled = false;

            rfv_buscar_gasto.Enabled = false;
        }

        protected void gv_gasto_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_gasto.PageIndex = e.NewPageIndex;

            string str_rub = txt_buscar_gasto.Text.ToUpper().Trim();

            if (str_rub == "TODO")
            {
                using (dd_liecEntities data_user = new dd_liecEntities())
                {
                    var inf_user = (from i_g in data_user.inf_gastos
                                    join t_r in data_user.fact_tipo_rubro on i_g.id_tipo_rubro equals t_r.id_tipo_rubro
                                    join i_r in data_user.inf_rubro on i_g.id_rubro equals i_r.id_rubro
                                    select new
                                    {
                                        i_g.codigo_gasto,
                                        t_r.tipo_rubro,
                                        i_r.etiqueta_rubro,
                                        i_g.desc_gasto,
                                        i_g.fecha_registro
                                    }).OrderBy(x => x.codigo_gasto).ToList();

                    if (inf_user.Count == 0)
                    {
                        gv_gasto.DataSource = inf_user;
                        gv_gasto.DataBind();
                        gv_gasto.Visible = true;

                        Mensaje("Rubro no encontrado.");
                    }
                    else
                    {
                        gv_gasto.DataSource = inf_user;
                        gv_gasto.DataBind();
                        gv_gasto.Visible = true;
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
                    string d_rub = txt_buscar_gasto.Text.Trim();
                    String[] de_rub = d_rub.Trim().Split(char_s);

                    n_rub = de_rub[1].Trim();

                    using (dd_liecEntities edm_nclte = new dd_liecEntities())
                    {
                        var i_nclte = (from c in edm_nclte.inf_gastos
                                       where c.codigo_gasto == n_rub
                                       select c).FirstOrDefault();

                        guid_fclte = i_nclte.id_gasto;
                    }

                    using (dd_liecEntities data_user = new dd_liecEntities())
                    {
                        var inf_user = (from i_g in data_user.inf_gastos
                                        join t_r in data_user.fact_tipo_rubro on i_g.id_tipo_rubro equals t_r.id_tipo_rubro
                                        join i_r in data_user.inf_rubro on i_g.id_rubro equals i_r.id_rubro
                                        where i_g.id_gasto == guid_fclte
                                        select new
                                        {
                                            i_g.codigo_gasto,
                                            t_r.tipo_rubro,
                                            i_r.etiqueta_rubro,
                                            i_g.desc_gasto,
                                            i_g.fecha_registro
                                        }).OrderBy(x => x.codigo_gasto).ToList();

                        if (inf_user.Count == 0)
                        {
                            gv_gasto.DataSource = inf_user;
                            gv_gasto.DataBind();
                            gv_gasto.Visible = true;

                            Mensaje("Rubro no encontrado.");
                        }
                        else
                        {
                            gv_gasto.DataSource = inf_user;
                            gv_gasto.DataBind();
                            gv_gasto.Visible = true;
                        }
                    }
                }
                catch
                {
                    limpia_txt_gasto();
                    Mensaje("Rubro no encontrado.");
                }
            }
        }

        protected void gv_gasto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string str_clteID = e.Row.Cells[1].Text;
                int int_estatusID;

                using (dd_liecEntities data_clte = new dd_liecEntities())
                {
                    var i_clte = (from t_clte in data_clte.inf_gastos
                                  where t_clte.codigo_gasto == str_clteID
                                  select new
                                  {
                                      t_clte.id_est_gast,
                                  }).FirstOrDefault();

                    int_estatusID = int.Parse(i_clte.id_est_gast.ToString());
                }

                DropDownList DropDownList1 = (e.Row.FindControl("ddl_gasto_est") as DropDownList);

                using (dd_liecEntities db_sepomex = new dd_liecEntities())
                {
                    var tbl_sepomex = (from c in db_sepomex.fact_est_rub
                                       select c).ToList();

                    DropDownList1.DataSource = tbl_sepomex;

                    DropDownList1.DataTextField = "desc_est_rub";
                    DropDownList1.DataValueField = "id_est_rub";
                    DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
                    DropDownList1.SelectedValue = int_estatusID.ToString();
                }
            }
        }

        protected void chk_gasto_CheckedChanged(object sender, EventArgs e)
        {
            Guid guid_rub;
            string str_rub;

            foreach (GridViewRow row in gv_gasto.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chk_gasto") as CheckBox);
                    if (chkRow.Checked)
                    {
                        row.BackColor = Color.FromArgb(227, 76, 14);
                        str_rub = row.Cells[1].Text;

                        using (dd_liecEntities edm_rub = new dd_liecEntities())
                        {
                            var i_rub = (from c in edm_rub.inf_gastos
                                         where c.codigo_gasto == str_rub
                                         select c).FirstOrDefault();

                            guid_rub = i_rub.id_gasto;

                            var f_rub = (from r in edm_rub.inf_gastos
                                         where r.id_gasto == guid_rub
                                         select new
                                         {
                                             r.id_tipo_rubro,
                                             r.id_rubro,
                                             r.desc_gasto,
                                             r.monto
                                         }).FirstOrDefault();

                            ddl_tipo_gasto.SelectedValue = f_rub.id_tipo_rubro.ToString();
                            //eti_rub.Text = f_rub.etiqueta_gasto;
                            desc_gasto.Text = f_rub.desc_gasto;
                            decimal moneyvalue = decimal.Parse(f_rub.monto.ToString());
                            string monto_rub = String.Format("{0:C}", moneyvalue);
                            mont_gasto.Text = monto_rub;
                        }
                    }
                    else
                    {
                        row.BackColor = Color.White;
                    }
                }
            }
        }

        protected void ddl_gasto_est_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void ddl_eti_gasto_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid guid_r = Guid.Parse(ddl_eti_gasto.SelectedValue);
            double monto_rr, monto_gg;
            string monto_r, monto_g;
            using (dd_liecEntities m_r = new dd_liecEntities())
            {
                var i_rr = (from i_g in m_r.inf_rubro_mes
                            where i_g.id_rubro == guid_r
                            where i_g.id_est_rubm == 1
                            select i_g).FirstOrDefault();

                monto_rr = double.Parse(i_rr.monto_fijo.ToString());
                monto_r = string.Format("{0:C}", (Math.Truncate(Convert.ToDouble(monto_rr) * 100.0) / 100.0)); ;

                var i_r = (from i_g in m_r.inf_gastos
                           where i_g.id_rubro == guid_r
                           where i_g.fecha_registro.Value.Month == DateTime.Now.Month
                           where i_g.fecha_registro.Value.Year == DateTime.Now.Year
                           select new
                           {
                               i_g.monto
                           }).ToList();

                if (i_r.Count == 0)
                {
                    monto_gg = i_r.Count;
                }
                else
                {
                    monto_gg = double.Parse(i_r.Sum(x => x.monto).ToString());
                }
            }
            monto_g = string.Format("{0:C}", (Math.Truncate(Convert.ToDouble(monto_gg) * 100.0) / 100.0)); ;

            lbl_tgast.Text = "Monto Rubro: " + monto_r + " - Monto Gastado: " + monto_g;
        }

        protected void ddl_tipo_gasto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int int_tg = int.Parse(ddl_tipo_gasto.SelectedValue);
            ddl_eti_gasto.Items.Clear();
            using (dd_liecEntities m_genero = new dd_liecEntities())
            {
                var i_genero = (from f_tr in m_genero.inf_rubro
                                where f_tr.id_tipo_rubro == int_tg
                                select f_tr).ToList();

                ddl_eti_gasto.DataSource = i_genero;
                ddl_eti_gasto.DataTextField = "etiqueta_rubro";
                ddl_eti_gasto.DataValueField = "id_rubro";
                ddl_eti_gasto.DataBind();
            }
            ddl_eti_gasto.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
        }

        protected void mont_gasto_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(mont_gasto.Text))
            {
                rfv_mont_gasto.Enabled = true;
            }
            else
            {
                string stringTest = mont_gasto.Text.Trim().Replace("$", "").Replace(",", "");

                try
                {
                    decimal moneyvalue = decimal.Parse(stringTest);
                    string html = String.Format("{0:C}", moneyvalue);

                    mont_gasto.Text = html;
                    mont_gasto.Focus();
                }
                catch
                {
                    mont_gasto.Text = null;
                }
            }
        }

        protected void btn_guardar_gasto_Click(object sender, EventArgs e)
        {
            int t_gasto, min_gasto = 0, max_gasto = 0;
            string eti_gasto, s_desc_gasto, str_cod_gasto;
            double d_mont_fijo, monto_f;
            Guid guid_nrubro, guid_nrubrom, guid_rubf, guid_emp;
            DateTime f_filtro = DateTime.Now;

            if (acc_gasto == 0)
            {
                Mensaje("Favor de seleccionar una acción");
            }
            else
            {
                guid_nrubro = Guid.NewGuid();
                guid_nrubrom = Guid.NewGuid();
                guid_emp = Guid.Parse("D8A03556-6791-45F3-BABE-ECF267B865F1");
                t_gasto = int.Parse(ddl_tipo_gasto.SelectedValue);
                guid_rubf = Guid.Parse(ddl_eti_gasto.SelectedValue);
                s_desc_gasto = desc_gasto.Text.Trim().ToUpper();

                d_mont_fijo = double.Parse(mont_gasto.Text.Replace("$", ""));
                string usr_reg = lbl_usr_oper.Text;
                if (acc_gasto == 2)
                {
                    guid_nrubro = Guid.NewGuid();
                    Guid guid_idrubro;
                    int int_ddl, int_f_gasto = 0;
                    int int_estatusID = 0;
                    string f_gasto = null;
                    foreach (GridViewRow row in gv_gasto.Rows)
                    {
                        // int key = (int)GridView1.DataKeys[row.RowIndex].Value;
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[0].FindControl("chk_gasto") as CheckBox);
                            if (chkRow.Checked)
                            {
                                int_f_gasto = int_f_gasto + 1;
                                f_gasto = row.Cells[1].Text;

                                DropDownList dl = (DropDownList)row.FindControl("ddl_gasto_est");

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
                        gv_gasto.Visible = false;
                        limpia_txt_gasto();
                        Mensaje("Favor de seleccionar un gasto");
                    }
                    else
                    {
                        using (dd_liecEntities data_user = new dd_liecEntities())
                        {
                            var items_user = (from c in data_user.inf_gastos
                                              where c.codigo_gasto == f_gasto
                                              select c).FirstOrDefault();

                            guid_idrubro = items_user.id_gasto;
                        }

                        using (var m_nrubro = new dd_liecEntities())
                        {
                            var i_nrubro = (from c in m_nrubro.inf_gastos
                                            where c.id_gasto == guid_idrubro
                                            select c).FirstOrDefault();

                            i_nrubro.id_est_gast = int_estatusID;
                            i_nrubro.id_tipo_rubro = t_gasto;
                            i_nrubro.id_rubro = guid_rubf;
                            i_nrubro.desc_gasto = s_desc_gasto;
                            i_nrubro.monto = decimal.Parse(d_mont_fijo.ToString());

                            m_nrubro.SaveChanges();

                            limpia_txt_gasto();

                            rfv_eti_gasto.Enabled = false;
                            rfv_desc_gasto.Enabled = false;
                            rfv_tipo_gasto.Enabled = false;
                            rfv_mont_gasto.Enabled = false;

                            gv_gasto.Visible = false;
                            Mensaje("Datos actualizados con éxito.");
                        }
                    }
                }
                else
                {
                    using (dd_liecEntities edm_gasto = new dd_liecEntities())
                    {
                        var c_gasto = (from c in edm_gasto.inf_gastos
                                       select c).ToList();
                        if (c_gasto.Count == 0)
                        {
                            str_cod_gasto = "LIEC-G" + string.Format("{0:000}", c_gasto.Count + 1);

                            var i_nrub = new inf_gastos
                            {
                                id_gasto = guid_nrubro,
                                id_rubro = guid_rubf,
                                id_est_gast = 1,
                                codigo_gasto = str_cod_gasto,
                                id_tipo_rubro = t_gasto,
                                monto = decimal.Parse(d_mont_fijo.ToString()),
                                desc_gasto = s_desc_gasto,
                                fecha_registro = DateTime.Now,
                                id_emp = guid_emp,
                                id_usuario = guid_idusr
                            };

                            edm_gasto.inf_gastos.Add(i_nrub);
                            edm_gasto.SaveChanges();

                            var i_gastos = (from i_g in edm_gasto.inf_gastos
                                            where i_g.id_tipo_rubro == t_gasto
                                            where i_g.id_rubro == guid_rubf
                                            select new
                                            {
                                                i_g.monto,
                                            }).ToList();

                            if (i_gastos.Count == 0)
                            {
                                monto_f = i_gastos.Count;
                            }
                            else
                            {
                                monto_f = double.Parse(i_gastos.Sum(x => x.monto).ToString());
                            }
                            var i_rubm = (from i_g in edm_gasto.inf_rubro_mes
                                          where i_g.id_rubro == guid_rubf
                                          where i_g.id_est_rubm == 1
                                          where i_g.fecha_registro.Value.Month == DateTime.Now.Month
                                          where i_g.fecha_registro.Value.Year == DateTime.Now.Year
                                          select new
                                          {
                                              i_g.monto_fijo,
                                              i_g.minimo,
                                              i_g.min,
                                              i_g.maximo,
                                              i_g.max,
                                          }).FirstOrDefault();

                            double m_min = (Math.Truncate(Convert.ToDouble(i_rubm.monto_fijo) * 100.0) / 100.0) * ((Math.Truncate(Convert.ToDouble(i_rubm.minimo) * 100.0) / 100.0) / 100) + 1;
                            string monto_e = string.Format("{0:C}", (Math.Truncate(Convert.ToDouble(m_min) * 100.0) / 100.0));
                            if (monto_f >= m_min && i_rubm.min == 0)
                            {
                                using (dd_liecEntities data_user = new dd_liecEntities())
                                {
                                    var igf = (from c in data_user.inf_rubro_mes
                                               where c.id_rubro == guid_rubf
                                               where c.id_est_rubm == 1
                                               select c).FirstOrDefault();

                                    string monto_ff = string.Format("{0:C}", (Math.Truncate(Convert.ToDouble(igf.monto_fijo) * 100.0) / 100.0));
                                    string monto_g = string.Format("{0:C}", (Math.Truncate(Convert.ToDouble(d_mont_fijo) * 100.0) / 100.0));

                                    var i_trub = (from c in data_user.fact_tipo_rubro
                                                  where c.id_tipo_rubro == t_gasto
                                                  select c).FirstOrDefault();

                                    string ntrubro = i_trub.desc_tipo_rubro;

                                    var i_rub = (from c in data_user.inf_rubro
                                                 where c.id_rubro == guid_rubf
                                                 select c).FirstOrDefault();
                                    string nrubro = i_rub.rubro;

                                    var i_ev = (from c in data_user.inf_email_envio

                                                select c).FirstOrDefault();
                                    string f_clave = i_ev.clave.ToString();
                                    string detalle_e = "MONTO DE GASTO IGUAL O MAYOR AL 25 % DEL PRESUPUESTO FIJO";

                                    enviarcorreo(i_ev.email, i_ev.email, f_clave, i_ev.asunto, detalle_e, i_ev.servidor_smtp, int.Parse(i_ev.puerto.ToString()), DateTime.Now, "liec.sdk@outlook.com", ntrubro, nrubro, monto_ff, monto_g, monto_e, usr_reg);

                                    igf.min = 1;
                                    data_user.SaveChanges();
                                }
                            }

                            limpia_txt_gasto();
                            Mensaje("Datos agregados con éxito.");
                        }
                        else
                        {
                            str_cod_gasto = "LIEC-G" + string.Format("{0:000}", c_gasto.Count + 1);

                            var i_nrub = new inf_gastos
                            {
                                id_gasto = guid_nrubro,
                                id_rubro = guid_rubf,
                                id_est_gast = 1,
                                codigo_gasto = str_cod_gasto,
                                id_tipo_rubro = t_gasto,
                                monto = decimal.Parse(d_mont_fijo.ToString()),
                                desc_gasto = s_desc_gasto,
                                fecha_registro = DateTime.Now,
                                id_emp = guid_emp,
                                id_usuario = guid_idusr
                            };

                            edm_gasto.inf_gastos.Add(i_nrub);
                            edm_gasto.SaveChanges();

                            var i_gastos = (from i_g in edm_gasto.inf_gastos
                                            where i_g.id_tipo_rubro == t_gasto
                                            where i_g.id_rubro == guid_rubf
                                            where i_g.fecha_registro.Value.Month == DateTime.Now.Month
                                            where i_g.fecha_registro.Value.Year == DateTime.Now.Year
                                            select new
                                            {
                                                i_g.monto,
                                            }).Distinct().ToList();

                            if (i_gastos.Count == 0)
                            {
                                monto_f = i_gastos.Count;
                            }
                            else
                            {
                                monto_f = double.Parse(i_gastos.Sum(x => x.monto).ToString());
                            }
                            var i_rubm = (from i_g in edm_gasto.inf_rubro_mes
                                          where i_g.id_rubro == guid_rubf
                                          where i_g.id_est_rubm == 1
                                          select new
                                          {
                                              i_g.monto_fijo,
                                              i_g.minimo,
                                              i_g.min,
                                              i_g.maximo,
                                              i_g.max,
                                          }).FirstOrDefault();

                            double m_min = (Math.Truncate(Convert.ToDouble(i_rubm.monto_fijo) * 100.0) / 100.0) * ((Math.Truncate(Convert.ToDouble(i_rubm.minimo) * 100.0) / 100.0) / 100) + 1;
                            double m_max = (Math.Truncate(Convert.ToDouble(i_rubm.monto_fijo) * 100.0) / 100.0) * ((Math.Truncate(Convert.ToDouble(i_rubm.maximo) * 100.0) / 100.0) / 100) + 1;
                            string monto_ff = string.Format("{0:C}", (Math.Truncate(Convert.ToDouble(i_rubm.monto_fijo) * 100.0) / 100.0));
                            string monto_g = string.Format("{0:C}", (Math.Truncate(Convert.ToDouble(monto_f) * 100.0) / 100.0));
                            string monto_e = string.Format("{0:C}", (Math.Truncate(Convert.ToDouble(m_min) * 100.0) / 100.0));
                            string monto_em = string.Format("{0:C}", (Math.Truncate(Convert.ToDouble(m_max) * 100.0) / 100.0));

                            if (monto_f >= m_min && i_rubm.min == 0)
                            {
                                using (dd_liecEntities data_user = new dd_liecEntities())
                                {
                                    var igf = (from c in data_user.inf_rubro_mes
                                               where c.id_rubro == guid_rubf
                                               where c.id_est_rubm == 1
                                               select c).FirstOrDefault();

                                    var i_trub = (from c in data_user.fact_tipo_rubro
                                                  where c.id_tipo_rubro == t_gasto
                                                  select c).FirstOrDefault();

                                    string ntrubro = i_trub.desc_tipo_rubro;

                                    var i_rub = (from c in data_user.inf_rubro
                                                 where c.id_rubro == guid_rubf
                                                 select c).FirstOrDefault();

                                    string nrubro = i_rub.rubro;

                                    var i_ev = (from c in data_user.inf_email_envio

                                                select c).FirstOrDefault();
                                    string f_clave = i_ev.clave.ToString();
                                    string detalle_e = "MONTO DE GASTO IGUAL O MAYOR AL 25 % DEL PRESUPUESTO FIJO";

                                    enviarcorreo(i_ev.email, i_ev.email, f_clave, i_ev.asunto, detalle_e, i_ev.servidor_smtp, int.Parse(i_ev.puerto.ToString()), DateTime.Now, "liec.sdk@outlook.com", ntrubro, nrubro, monto_ff, monto_g, monto_e, usr_reg);

                                    igf.min = 1;
                                    data_user.SaveChanges();
                                }
                            }
                            else if (monto_f >= m_max && i_rubm.min == 1 && i_rubm.max == 0)
                            {
                                using (dd_liecEntities data_user = new dd_liecEntities())
                                {
                                    var igf = (from c in data_user.inf_rubro_mes
                                               where c.id_rubro == guid_rubf
                                               where c.id_est_rubm == 1
                                               select c).FirstOrDefault();

                                    var i_trub = (from c in data_user.fact_tipo_rubro
                                                  where c.id_tipo_rubro == t_gasto
                                                  select c).FirstOrDefault();

                                    string ntrubro = i_trub.desc_tipo_rubro;

                                    var i_rub = (from c in data_user.inf_rubro
                                                 where c.id_rubro == guid_rubf
                                                 select c).FirstOrDefault();

                                    string nrubro = i_rub.rubro;

                                    var i_ev = (from c in data_user.inf_email_envio

                                                select c).FirstOrDefault();
                                    string f_clave = i_ev.clave.ToString();
                                    string detalle_e = "MONTO DE GASTO IGUAL O MAYOR AL 75 % DEL PRESUPUESTO FIJO";

                                    enviarcorreo(i_ev.email, i_ev.email, f_clave, i_ev.asunto, detalle_e, i_ev.servidor_smtp, int.Parse(i_ev.puerto.ToString()), DateTime.Now, "liec.sdk@outlook.com", ntrubro, nrubro, monto_ff, monto_g, monto_em, usr_reg);

                                    igf.max = 1;
                                    data_user.SaveChanges();
                                }
                            }

                            limpia_txt_gasto();
                            Mensaje("Datos agregados con éxito.");
                        }
                    }
                }
            }
        }

        #endregion gastos

        #region caja

        protected void lkb_cont_caja_Click(object sender, EventArgs e)
        {
            acc_caja = 0;
            int_pnlID = 4;

            pnl_rubro.Visible = false;
            up_rubro.Update();

            pnl_gasto.Visible = false;
            up_gasto.Update();

            pnl_caja.Visible = true;
            div_busc_rub.Visible = false;
            i_agr_caja.Attributes["style"] = "color:white";
            i_edit_caja.Attributes["style"] = "color:white";
            gv_caja.Visible = false;
            limpia_txt_caja();
            up_caja.Update();
        }

        private void limpia_txt_caja()
        {
            ddl_tipo_caja.Items.Clear();
            using (dd_liecEntities m_genero = new dd_liecEntities())
            {
                var i_genero = (from f_tr in m_genero.fact_tipo_rubro
                                where f_tr.id_tipo_rubro != 4
                                select f_tr).ToList();

                ddl_tipo_caja.DataSource = i_genero;
                ddl_tipo_caja.DataTextField = "tipo_rubro";
                ddl_tipo_caja.DataValueField = "id_tipo_rubro";
                ddl_tipo_caja.DataBind();
            }
            ddl_tipo_caja.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
            ddl_eti_caja.Items.Clear();
            ddl_eti_caja.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
            eti_rub.Text = null;
            desc_caja.Text = null;
            mont_caja.Text = null;
        }

        protected void btn_buscar_caja_Click(object sender, EventArgs e)
        {
            string str_rub = txt_buscar_caja.Text.ToUpper().Trim();

            if (str_rub == "TODO")
            {
                using (dd_liecEntities data_user = new dd_liecEntities())
                {
                    var inf_user = (from i_g in data_user.inf_caja
                                    join t_r in data_user.fact_tipo_rubro on i_g.id_tipo_rubro equals t_r.id_tipo_rubro
                                    join i_r in data_user.inf_rubro on i_g.id_rubro equals i_r.id_rubro
                                    select new
                                    {
                                        i_g.codigo_caja,
                                        t_r.tipo_rubro,
                                        i_r.etiqueta_rubro,
                                        i_g.desc_caja,
                                        i_g.fecha_registro
                                    }).OrderBy(x => x.codigo_caja).ToList();

                    if (inf_user.Count == 0)
                    {
                        gv_caja.DataSource = inf_user;
                        gv_caja.DataBind();
                        gv_caja.Visible = true;

                        Mensaje("Rubro no encontrado.");
                    }
                    else
                    {
                        gv_caja.DataSource = inf_user;
                        gv_caja.DataBind();
                        gv_caja.Visible = true;
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
                    string d_rub = txt_buscar_caja.Text.Trim();
                    String[] de_rub = d_rub.Trim().Split(char_s);

                    n_rub = de_rub[1].Trim();

                    using (dd_liecEntities edm_nclte = new dd_liecEntities())
                    {
                        var i_nclte = (from c in edm_nclte.inf_caja
                                       where c.codigo_caja == n_rub
                                       select c).FirstOrDefault();

                        guid_fclte = i_nclte.id_caja;
                    }

                    using (dd_liecEntities data_user = new dd_liecEntities())
                    {
                        var inf_user = (from i_g in data_user.inf_caja
                                        join t_r in data_user.fact_tipo_rubro on i_g.id_tipo_rubro equals t_r.id_tipo_rubro
                                        join i_r in data_user.inf_rubro on i_g.id_rubro equals i_r.id_rubro
                                        where i_g.id_caja == guid_fclte

                                        select new
                                        {
                                            i_g.codigo_caja,
                                            t_r.tipo_rubro,
                                            i_r.etiqueta_rubro,
                                            i_g.desc_caja,
                                            i_g.fecha_registro
                                        }).OrderBy(x => x.codigo_caja).ToList();

                        if (inf_user.Count == 0)
                        {
                            gv_caja.DataSource = inf_user;
                            gv_caja.DataBind();
                            gv_caja.Visible = true;

                            Mensaje("Rubro no encontrado.");
                        }
                        else
                        {
                            gv_caja.DataSource = inf_user;
                            gv_caja.DataBind();
                            gv_caja.Visible = true;
                        }
                    }
                }
                catch
                {
                    limpia_txt_caja();
                    Mensaje("Rubro no encontrado.");
                }
            }
        }

        protected void btn_agr_caja_Click(object sender, EventArgs e)
        {
            acc_caja = 1;

            div_busc_rub.Visible = false;

            gv_caja.Visible = false;

            monto_extra.Enabled = false;

            limpia_txt_caja();

            i_agr_caja.Attributes["style"] = "color:#E34C0E";
            i_edit_caja.Attributes["style"] = "color:white";

            rfv_eti_caja.Enabled = true;
            rfv_desc_caja.Enabled = true;
            rfv_tipo_caja.Enabled = true;
            rfv_mont_caja.Enabled = true;
        }

        protected void btn_edit_caja_Click(object sender, EventArgs e)
        {
            acc_caja = 2;

            div_busc_caja.Visible = true;
            rfv_buscar_caja.Enabled = true;

            monto_extra.Enabled = false;

            limpia_txt_caja();

            i_agr_caja.Attributes["style"] = "color:white";
            i_edit_caja.Attributes["style"] = "color:#E34C0E";
        }

        protected void chkb_des_caja_CheckedChanged(object sender, EventArgs e)
        {
            acc_caja = 0;
            rfv_eti_caja.Enabled = false;
            rfv_desc_caja.Enabled = false;
            rfv_tipo_caja.Enabled = false;
            rfv_mont_caja.Enabled = false;

            rfv_buscar_caja.Enabled = false;
        }

        protected void gv_caja_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_caja.PageIndex = e.NewPageIndex;
            string str_rub = txt_buscar_caja.Text.ToUpper().Trim();

            if (str_rub == "TODO")
            {
                using (dd_liecEntities data_user = new dd_liecEntities())
                {
                    var inf_user = (from i_g in data_user.inf_caja
                                    join t_r in data_user.fact_tipo_rubro on i_g.id_tipo_rubro equals t_r.id_tipo_rubro
                                    join i_r in data_user.inf_rubro on i_g.id_rubro equals i_r.id_rubro
                                    select new
                                    {
                                        i_g.codigo_caja,
                                        t_r.tipo_rubro,
                                        i_r.etiqueta_rubro,
                                        i_g.desc_caja,
                                        i_r.fecha_registro
                                    }).OrderBy(x => x.codigo_caja).ToList();

                    if (inf_user.Count == 0)
                    {
                        gv_caja.DataSource = inf_user;
                        gv_caja.DataBind();
                        gv_caja.Visible = true;

                        Mensaje("Rubro no encontrado.");
                    }
                    else
                    {
                        gv_caja.DataSource = inf_user;
                        gv_caja.DataBind();
                        gv_caja.Visible = true;
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
                    string d_rub = txt_buscar_caja.Text.Trim();
                    String[] de_rub = d_rub.Trim().Split(char_s);

                    n_rub = de_rub[1].Trim();

                    using (dd_liecEntities edm_nclte = new dd_liecEntities())
                    {
                        var i_nclte = (from c in edm_nclte.inf_caja
                                       where c.codigo_caja == n_rub
                                       select c).FirstOrDefault();

                        guid_fclte = i_nclte.id_caja;
                    }

                    using (dd_liecEntities data_user = new dd_liecEntities())
                    {
                        var inf_user = (from i_g in data_user.inf_caja
                                        join t_r in data_user.fact_tipo_rubro on i_g.id_tipo_rubro equals t_r.id_tipo_rubro
                                        join i_r in data_user.inf_rubro on i_g.id_rubro equals i_r.id_rubro
                                        where i_g.id_caja == guid_fclte
                                        select new
                                        {
                                            i_g.codigo_caja,
                                            t_r.tipo_rubro,
                                            i_r.etiqueta_rubro,
                                            i_g.desc_caja,
                                            i_r.fecha_registro
                                        }).OrderBy(x => x.codigo_caja).ToList();

                        if (inf_user.Count == 0)
                        {
                            gv_caja.DataSource = inf_user;
                            gv_caja.DataBind();
                            gv_caja.Visible = true;

                            Mensaje("Rubro no encontrado.");
                        }
                        else
                        {
                            gv_caja.DataSource = inf_user;
                            gv_caja.DataBind();
                            gv_caja.Visible = true;
                        }
                    }
                }
                catch
                {
                    limpia_txt_caja();
                    Mensaje("Rubro no encontrado.");
                }
            }
        }

        protected void gv_caja_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string str_clteID = e.Row.Cells[1].Text;
                int int_estatusID;

                using (dd_liecEntities data_clte = new dd_liecEntities())
                {
                    var i_clte = (from t_clte in data_clte.inf_caja
                                  where t_clte.codigo_caja == str_clteID
                                  select new
                                  {
                                      t_clte.id_est_caja,
                                  }).FirstOrDefault();

                    int_estatusID = int.Parse(i_clte.id_est_caja.ToString());
                }

                DropDownList DropDownList1 = (e.Row.FindControl("ddl_caja_est") as DropDownList);

                using (dd_liecEntities db_sepomex = new dd_liecEntities())
                {
                    var tbl_sepomex = (from c in db_sepomex.fact_est_rub
                                       select c).ToList();

                    DropDownList1.DataSource = tbl_sepomex;

                    DropDownList1.DataTextField = "desc_est_rub";
                    DropDownList1.DataValueField = "id_est_rub";
                    DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
                    DropDownList1.SelectedValue = int_estatusID.ToString();
                }
            }
        }

        protected void chk_caja_CheckedChanged(object sender, EventArgs e)
        {
            Guid guid_rub;
            string str_rub;

            foreach (GridViewRow row in gv_caja.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chk_caja") as CheckBox);
                    if (chkRow.Checked)
                    {
                        row.BackColor = Color.FromArgb(227, 76, 14);
                        str_rub = row.Cells[1].Text;

                        using (dd_liecEntities edm_rub = new dd_liecEntities())
                        {
                            var i_rub = (from c in edm_rub.inf_caja
                                         where c.codigo_caja == str_rub
                                         select c).FirstOrDefault();

                            guid_rub = i_rub.id_caja;

                            var f_rub = (from r in edm_rub.inf_caja
                                         where r.id_caja == guid_rub
                                         select new
                                         {
                                             r.id_tipo_rubro,
                                             r.id_rubro,
                                             r.desc_caja,
                                             r.monto
                                         }).FirstOrDefault();

                            ddl_tipo_caja.SelectedValue = f_rub.id_tipo_rubro.ToString();
                            //eti_rub.Text = f_rub.etiqueta_caja;
                            desc_caja.Text = f_rub.desc_caja;
                            decimal moneyvalue = decimal.Parse(f_rub.monto.ToString());
                            string monto_rub = String.Format("{0:C}", moneyvalue);
                            mont_caja.Text = monto_rub;
                        }
                    }
                    else
                    {
                        row.BackColor = Color.White;
                    }
                }
            }
        }

        protected void ddl_caja_est_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void ddl_tipo_caja_SelectedIndexChanged(object sender, EventArgs e)
        {
            int int_tg = int.Parse(ddl_tipo_caja.SelectedValue);
            ddl_eti_caja.Items.Clear();
            using (dd_liecEntities m_genero = new dd_liecEntities())
            {
                var i_genero = (from f_tr in m_genero.inf_rubro
                                where f_tr.id_tipo_rubro == int_tg
                                select f_tr).ToList();

                ddl_eti_caja.DataSource = i_genero;
                ddl_eti_caja.DataTextField = "etiqueta_rubro";
                ddl_eti_caja.DataValueField = "id_rubro";
                ddl_eti_caja.DataBind();
            }
            ddl_eti_caja.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
        }

        protected void mont_caja_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(mont_caja.Text))
            {
                rfv_mont_caja.Enabled = true;
            }
            else
            {
                string stringTest = mont_caja.Text.Trim().Replace("$", "").Replace(",", "");

                try
                {
                    decimal moneyvalue = decimal.Parse(stringTest);
                    string html = String.Format("{0:C}", moneyvalue);

                    mont_caja.Text = html;
                    mont_caja.Focus();
                }
                catch
                {
                    mont_caja.Text = null;
                }
            }
        }

        protected void btn_guardar_caja_Click(object sender, EventArgs e)
        {
            int t_caja, min_caja = 0, max_caja = 0;
            string eti_caja, s_desc_caja, str_cod_caja;
            double d_mont_fijo, monto_f;
            Guid guid_nrubro, guid_nrubrom, guid_rubf, guid_emp;
            DateTime f_filtro = DateTime.Now;

            if (acc_caja == 0)
            {
                Mensaje("Favor de seleccionar una acción");
            }
            else
            {
                guid_nrubro = Guid.NewGuid();
                guid_nrubrom = Guid.NewGuid();
                guid_emp = Guid.Parse("D8A03556-6791-45F3-BABE-ECF267B865F1");
                t_caja = int.Parse(ddl_tipo_caja.SelectedValue);
                guid_rubf = Guid.Parse(ddl_eti_caja.SelectedValue);
                s_desc_caja = desc_caja.Text.Trim().ToUpper();

                d_mont_fijo = double.Parse(mont_caja.Text.Replace("$", ""));
                string usr_reg = lbl_usr_oper.Text;
                if (acc_caja == 2)
                {
                    guid_nrubro = Guid.NewGuid();
                    Guid guid_idrubro;
                    int int_ddl, int_f_caja = 0;
                    int int_estatusID = 0;
                    string f_caja = null;
                    foreach (GridViewRow row in gv_caja.Rows)
                    {
                        // int key = (int)GridView1.DataKeys[row.RowIndex].Value;
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[0].FindControl("chk_caja") as CheckBox);
                            if (chkRow.Checked)
                            {
                                int_f_caja = int_f_caja + 1;
                                f_caja = row.Cells[1].Text;

                                DropDownList dl = (DropDownList)row.FindControl("ddl_caja_est");

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
                        gv_caja.Visible = false;
                        limpia_txt_caja();
                        Mensaje("Favor de seleccionar un gasto");
                    }
                    else
                    {
                        using (dd_liecEntities data_user = new dd_liecEntities())
                        {
                            var items_user = (from c in data_user.inf_caja
                                              where c.codigo_caja == f_caja
                                              select c).FirstOrDefault();

                            guid_idrubro = items_user.id_caja;
                        }

                        using (var m_nrubro = new dd_liecEntities())
                        {
                            var i_nrubro = (from c in m_nrubro.inf_caja
                                            where c.id_caja == guid_idrubro
                                            select c).FirstOrDefault();

                            i_nrubro.id_est_caja = int_estatusID;
                            i_nrubro.id_tipo_rubro = t_caja;
                            i_nrubro.id_rubro = guid_rubf;
                            i_nrubro.desc_caja = s_desc_caja;
                            i_nrubro.monto = decimal.Parse(d_mont_fijo.ToString());

                            m_nrubro.SaveChanges();

                            limpia_txt_caja();

                            rfv_eti_caja.Enabled = false;
                            rfv_desc_caja.Enabled = false;
                            rfv_tipo_caja.Enabled = false;
                            rfv_mont_caja.Enabled = false;

                            gv_caja.Visible = false;
                            Mensaje("Datos actualizados con éxito.");
                        }
                    }
                }
                else
                {
                    using (dd_liecEntities edm_caja = new dd_liecEntities())
                    {
                        var c_caja = (from c in edm_caja.inf_caja
                                      select c).ToList();
                        if (c_caja.Count == 0)
                        {
                            str_cod_caja = "LIEC-G" + string.Format("{0:000}", c_caja.Count + 1);

                            var i_nrub = new inf_caja
                            {
                                id_caja = guid_nrubro,
                                id_rubro = guid_rubf,
                                id_est_caja = 1,
                                codigo_caja = str_cod_caja,
                                id_tipo_rubro = t_caja,
                                monto = decimal.Parse(d_mont_fijo.ToString()),
                                desc_caja = s_desc_caja,
                                fecha_registro = DateTime.Now,
                                id_emp = guid_emp,
                                id_usuario = guid_idusr
                            };

                            edm_caja.inf_caja.Add(i_nrub);
                            edm_caja.SaveChanges();

                            limpia_txt_caja();
                            Mensaje("Datos agregados con éxito.");
                        }
                        else
                        {
                            str_cod_caja = "LIEC-G" + string.Format("{0:000}", c_caja.Count + 1);

                            var i_nrub = new inf_caja
                            {
                                id_caja = guid_nrubro,
                                id_rubro = guid_rubf,
                                id_est_caja = 1,
                                codigo_caja = str_cod_caja,
                                id_tipo_rubro = t_caja,
                                monto = decimal.Parse(d_mont_fijo.ToString()),
                                desc_caja = s_desc_caja,
                                fecha_registro = DateTime.Now,
                                id_emp = guid_emp,
                                id_usuario = guid_idusr
                            };

                            edm_caja.inf_caja.Add(i_nrub);
                            edm_caja.SaveChanges();

                            limpia_txt_caja();
                            Mensaje("Datos agregados con éxito.");
                        }
                    }
                }
            }
        }

        #endregion caja
    }
}