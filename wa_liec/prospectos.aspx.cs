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
    public partial class prospectos : System.Web.UI.Page
    {
        public static int int_prospecto, int_pnlID;

        protected void Page_Load(object sender, EventArgs e)
        {
            ddl_colonia_prospecto.Items.Clear();
            ddl_colonia_prospecto.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
            ddl_acc_prospecto.Items.Clear();
            ddl_acc_prospecto.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
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

        protected void btn_agregar_prospecto_Click(object sender, EventArgs e)
        {
            int_prospecto = 1;

            i_agregar_prospecto.Attributes["style"] = "color:#E34C0E";
            i_editar_prospecto.Attributes["style"] = "color:white";
            rfv_buscar_prospecto.Enabled = false;
            rfv_est_prospecto.Enabled = false;
            rfv_emp_prospecto.Enabled = true;
            rfv_cont_prospecto.Enabled = true;
            rfv_callenum_prospecto.Enabled = false;
            rfv_colonia_prospecto.Enabled = false;
            rfv_acc_prospecto.Enabled = false;
            rfv_prospecto_coment.Enabled = false;
        }

        protected void btn_editar_prospecto_Click(object sender, EventArgs e)
        {
            int_pnlID = 1;
            int_prospecto = 2;
            div_busc_clt.Visible = true;
            i_agregar_prospecto.Attributes["style"] = "color:white";
            i_editar_prospecto.Attributes["style"] = "color:#E34C0E";
            rfv_buscar_prospecto.Enabled = true;
            rfv_est_prospecto.Enabled = false;
            rfv_emp_prospecto.Enabled = false;
            rfv_cont_prospecto.Enabled = false;
            rfv_callenum_prospecto.Enabled = false;
            rfv_colonia_prospecto.Enabled = false;
            rfv_acc_prospecto.Enabled = false;
            rfv_prospecto_coment.Enabled = false;
        }

        protected void chkb_desactivar_prospecto_CheckedChanged(object sender, EventArgs e)
        {
            int_prospecto = 0;
            i_agregar_prospecto.Attributes["style"] = "color:white";
            i_editar_prospecto.Attributes["style"] = "color:white";
            rfv_buscar_prospecto.Enabled = false;
            rfv_est_prospecto.Enabled = false;
            rfv_emp_prospecto.Enabled = false;
            rfv_cont_prospecto.Enabled = false;
            rfv_callenum_prospecto.Enabled = false;
            rfv_colonia_prospecto.Enabled = false;
            rfv_acc_prospecto.Enabled = false;
            rfv_prospecto_coment.Enabled = false;
        }

        protected void btn_guardar_prospecto_Click(object sender, EventArgs e)
        {
            if (int_prospecto == 0)
            {
                Mensaje("Favor de seleccionr una acción.");
                rfv_colonia_prospecto.Enabled = false;
            }
            else
            {
                guarda_prospecto();
            }
        }

        private void guarda_prospecto()
        {
            Guid guid_prospecto = Guid.NewGuid();
            Guid guid_sprospecto = Guid.NewGuid();
            Guid guid_emp = Guid.Parse("D8A03556-6791-45F3-BABE-ECF267B865F1");
            Guid guid_idusr = Guid.Parse("0298DF9A-4DBF-4EE5-84F7-1E7E519BE4F9");
            string str_cod_prospecto, str_nom_prospecto;

            string str_emp = txt_emp_prospecto.Text.Trim().ToUpper();
            string str_contacto = txt_cont_prospecto.Text.ToUpper().Trim();
            string str_telefono = txt_telefono_prospecto.Text;
            string str_email = txt_email_prospecto.Text.Trim();
            string str_callenum = txt_callenum_prospecto.Text.ToUpper().Trim();
            string str_cp = txt_cp_prospecto.Text;
            int int_colonia = Convert.ToInt32(ddl_colonia_prospecto.SelectedValue);
            int int_accion = Convert.ToInt32(ddl_acc_prospecto.SelectedValue);
            string str_coment = txt_prospecto_coment.Text.Trim().ToUpper();

            if (int_prospecto == 2)
            {
                int int_ddl, int_f_prospecto = 0;
                int int_estatusID = 0;
                string str_fclte = null;
                foreach (GridViewRow row in gv_prospecto.Rows)
                {
                    // int key = (int)GridView1.DataKeys[row.RowIndex].Value;
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chk_prospecto") as CheckBox);
                        if (chkRow.Checked)
                        {
                            int_f_prospecto = int_f_prospecto + 1;
                            str_fclte = row.Cells[1].Text;
                            str_nom_prospecto = row.Cells[2].Text;
                            DropDownList dl = (DropDownList)row.FindControl("ddl_prospecto_estatus");

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
                    limp_txt_prospecto();
                    Mensaje("Favor de seleccionar un cliente.");
                }
                else
                {
                    str_coment = txt_prospecto_coment.Text;

                    //using (var m_prospecto = new dd_liecEntities())
                    //{
                    //    var i_prospecto = (from c in m_prospecto.inf_prospectos
                    //                  where c.cod_prospecto == str_fclte
                    //                  select c).FirstOrDefault();

                    //    i_prospecto.id_tipo_rfc = int_trfc;
                    //    i_prospecto.rfc = str_rfc;
                    //    i_prospecto.id_est_prospecto = int_estatusID;
                    //    i_prospecto.razon_social = str_razon_social;
                    //    i_prospecto.telefono = str_telefono;
                    //    i_prospecto.email = str_email;
                    //    i_prospecto.callenum = str_callenum;
                    //    i_prospecto.d_codigo = str_cp;
                    //    i_prospecto.id_asenta_cpcons = int_colony;
                    //    i_prospecto.comentarios = str_coment;
                    //    i_prospecto.id_usuario = guid_idusr;

                    //    m_prospecto.SaveChanges();
                    //}

                    //rfv_rs.Enabled = false;
                    rfv_callenum_prospecto.Enabled = false;
                    rfv_cp_prospecto.Enabled = false;
                    rfv_colonia_prospecto.Enabled = false;
                    rfv_prospecto_coment.Enabled = false;
                    int_prospecto = 0;
                    limp_txt_prospecto();
                    gv_prospecto.Visible = false;
                    i_agregar_prospecto.Attributes["style"] = "color:white";
                    i_editar_prospecto.Attributes["style"] = "color:white";
                    Mensaje("Datos de cliente actualizados con éxito.");
                }
            }
            else if (int_prospecto == 1)
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
                                    id_est_prospecto = 1,
                                    empresa = str_emp,

                                    cod_prospecto = str_cod_prospecto,
                                    contacto = str_contacto,
                                    telefono = str_telefono,
                                    email = str_email,
                                    callenum = str_callenum,
                                    d_codigo = str_cp,
                                    id_asenta_cpcons = int_colonia,
                                    fecha_registro = DateTime.Now,
                                    id_emp = guid_emp,
                                    id_usuario = guid_idusr
                                };

                                m_prospecto.inf_prospectos.Add(i_prospecto);
                                m_prospecto.SaveChanges();
                            }

                            using (var m_prospecto = new dd_liecEntities())
                            {
                                var i_prospecto_s = new inf_seg_prospecto

                                {
                                    id_seg_prospecto = guid_sprospecto,
                                    id_tipo_accion = int_accion,
                                    comentarios = str_coment,
                                    fecha_registro = DateTime.Now,
                                    id_prospecto = guid_prospecto,
                                    id_usuario = guid_idusr
                                };

                                m_prospecto.inf_seg_prospecto.Add(i_prospecto_s);
                                m_prospecto.SaveChanges();
                            }
                            rfv_colonia_prospecto.Enabled = false;
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
                                    id_est_prospecto = 1,
                                    empresa = str_emp,

                                    cod_prospecto = str_cod_prospecto,
                                    contacto = str_contacto,
                                    telefono = str_telefono,
                                    email = str_email,
                                    callenum = str_callenum,
                                    d_codigo = str_cp,
                                    id_asenta_cpcons = int_colonia,
                                    fecha_registro = DateTime.Now,
                                    id_emp = guid_emp,
                                    id_usuario = guid_idusr
                                };

                                m_prospecto.inf_prospectos.Add(i_prospecto);
                                m_prospecto.SaveChanges();
                            }

                            using (var m_prospecto = new dd_liecEntities())
                            {
                                var i_prospecto_s = new inf_seg_prospecto

                                {
                                    id_seg_prospecto = guid_sprospecto,
                                    id_tipo_accion = int_accion,
                                    comentarios = str_coment,
                                    fecha_registro = DateTime.Now,
                                    id_prospecto = guid_prospecto,
                                    id_usuario = guid_idusr
                                };

                                m_prospecto.inf_seg_prospecto.Add(i_prospecto_s);
                                m_prospecto.SaveChanges();
                            }
                            rfv_colonia_prospecto.Enabled = false;
                            limp_txt_prospecto();
                            Mensaje("Datos de prospecto agregados con éxito.");
                        }
                    }
                    else
                    {
                        limp_txt_prospecto();
                        rfv_emp_prospecto.Enabled = false;
                        rfv_cont_prospecto.Enabled = false;
                        rfv_callenum_prospecto.Enabled = false;
                        rfv_cp_prospecto.Enabled = false;
                        rfv_colonia_prospecto.Enabled = false;
                        Mensaje("Cliente existe en la base de datos, favor de revisar.");
                    }
                }
            }
        }

        private void limp_txt_prospecto()
        {
            txt_emp_prospecto.Text = null;
            txt_cont_prospecto.Text = null;
            txt_telefono_prospecto.Text = null;
            txt_email_prospecto.Text = null;
            txt_callenum_prospecto.Text = null;
            txt_cp_prospecto.Text = null;
            txt_municipio_prospecto.Text = null;
            txt_estado_prospecto.Text = null;
            ddl_colonia_prospecto.Items.Clear();
            ddl_colonia_prospecto.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
            ddl_acc_prospecto.Items.Clear();
            ddl_acc_prospecto.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
            txt_prospecto_coment.Text = null;
        }

        private void Mensaje(string contenido)
        {
            lblModalTitle.Text = "LIEC";
            lblModalBody.Text = contenido;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();
        }

        protected void btn_buscar_prospecto_Click(object sender, EventArgs e)
        {
            limp_txt_prospecto();
            string str_rub = txt_buscar_prospecto.Text.ToUpper().Trim();

            if (str_rub == "TODO")
            {
                using (dd_liecEntities data_user = new dd_liecEntities())
                {
                    var inf_user = (from i_r in data_user.inf_prospectos
                                    select new
                                    {
                                        i_r.id_prospecto,
                                        i_r.cod_prospecto,
                                        i_r.empresa,
                                        i_r.fecha_registro
                                    }).OrderBy(x => x.cod_prospecto).ToList();

                    if (inf_user.Count == 0)
                    {
                        gv_prospecto.DataSource = inf_user;
                        gv_prospecto.DataBind();
                        gv_prospecto.Visible = true;

                        Mensaje("Empresa no encontrado.");
                    }
                    else
                    {
                        gv_prospecto.DataSource = inf_user;
                        gv_prospecto.DataBind();
                        gv_prospecto.Visible = true;
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

                                        where i_r.id_prospecto == guid_fclte

                                        select new
                                        {
                                            i_r.id_prospecto,
                                            i_r.cod_prospecto,
                                            i_r.empresa,
                                            i_r.fecha_registro
                                        }).OrderBy(x => x.cod_prospecto).ToList();

                        if (inf_user.Count == 0)
                        {
                            gv_prospecto.DataSource = inf_user;
                            gv_prospecto.DataBind();
                            gv_prospecto.Visible = true;

                            Mensaje("Empresa no encontrado.");
                        }
                        else
                        {
                            gv_prospecto.DataSource = inf_user;
                            gv_prospecto.DataBind();
                            gv_prospecto.Visible = true;
                        }
                    }
                }
                catch
                {
                    limp_txt_prospecto();
                    Mensaje("Rubro no encontrado.");
                }
            }
        }

        protected void chk_prospecto_CheckedChanged(object sender, EventArgs e)
        {
            Guid guid_prospecto;
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
                                                   //join i_tu in edm_prospecto.est_prospecto on t_prospecto.no_muesra equals i_tu.no_muesra
                                               where t_prospecto.id_prospecto == guid_prospecto
                                               select new
                                               {
                                                   t_prospecto.empresa,
                                                   t_prospecto.contacto,

                                                   t_prospecto.telefono,
                                                   t_prospecto.email,
                                                   t_prospecto.callenum,
                                                   t_prospecto.d_codigo,
                                                   t_prospecto.id_asenta_cpcons,
                                               }).FirstOrDefault();

                            txt_emp_prospecto.Text = i_prospecto.empresa;
                            txt_cont_prospecto.Text = i_prospecto.contacto;
                            txt_telefono_prospecto.Text = i_prospecto.telefono;
                            txt_email_prospecto.Text = i_prospecto.email;
                            txt_callenum_prospecto.Text = i_prospecto.callenum;
                            txt_cp_prospecto.Text = i_prospecto.d_codigo;

                            try
                            {
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


                            }

                            var inf_user = (from i_r in edm_prospecto.inf_seg_prospecto
                                            where i_r.id_prospecto == guid_prospecto

                                            select new
                                            {
                                                i_r.id_seg_prospecto,
                                                i_r.id_tipo_accion,
                                                i_r.comentarios,
                                                i_r.fecha_registro
                                            }).ToList();

                            gv_seg_prosp.DataSource = inf_user;
                            gv_seg_prosp.DataBind();
                            gv_seg_prosp.Visible = true;

                            rfv_emp_prospecto.Enabled = true;
                            rfv_cont_prospecto.Enabled = true;
                            rfv_callenum_prospecto.Enabled = true;
                            rfv_cp_prospecto.Enabled = true;
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
    }
}