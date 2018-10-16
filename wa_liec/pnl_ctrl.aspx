<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pnl_ctrl.aspx.cs" Inherits="wa_liec.pnl_ctrl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0" />
    <!-- Bootstrap -->

    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="styles/estilos_liec.css" rel="stylesheet" />

    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.3.1/css/all.css" integrity="sha384-mzrmE5qonljUremFsqc01SB46JvROS7bZs3IO2EmfFsd15uHvIt+Y8vEf7N7fWAU" crossorigin="anonymous">

    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <script src="Scripts/dist/jquery.maskMoney.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>

    <link rel="shortcut icon" href="img/ico_liec.png" type="image/png" />
    <title>/ ÁREAS </title>
</head>
<body>
    <script>
        function CheckOne(obj) {
            var grid = obj.parentNode.parentNode.parentNode;
            var inputs = grid.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox") {
                    if (obj.checked && inputs[i] != obj && inputs[i].checked) {
                        inputs[i].checked = false;
                    }
                }
            }
        }
    </script>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container">
            <asp:UpdatePanel ID="up_gastos_bienvenida" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <br />
                    <div class="row">

                        <div class="col-lg-6">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/img/ico_liec.png" Width="64" Height="64" CssClass="img-thumbnail" />
                        </div>

                        <div class="col-lg-6">
                            <div>
                               <p class="text-right">

                                    <label class="control-label fuente_css02">BIENVENID@:</label>
                                    <asp:LinkButton CssClass="buttonClass" ID="lkb_usr_oper" runat="server">
                                        <asp:Label CssClass="buttonClass" ID="lbl_usr_oper" runat="server" Text=""></asp:Label>&nbsp;<i class="fas fa-user-cog" id="i_usr_oper" runat="server"></i>
                                    </asp:LinkButton>

                                    <br />

                                    <label class="control-label fuente_css02">PERFIL:</label>
                                    <asp:Label CssClass="fuente_css02" ID="lbl_tusr" runat="server" Text=""></asp:Label>&nbsp;<i class="fas fa-user-shield fuente_css02" id="i1" runat="server"></i> 

                                    <br />

                                    <label class="control-label fuente_css02">EMPRESA:</label>
                                    <asp:LinkButton CssClass="buttonClass" ID="lkb_emp_oper" runat="server">
                                        <asp:Label CssClass="buttonClass" ID="lbl_emp_oper" runat="server" Text=""></asp:Label>&nbsp;<i class="fas fa-building" id="i_emp_oper" runat="server"></i>
                                    </asp:LinkButton>
                                </p>
                            </div>
                        </div>
                    </div>
                    <hr />
                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>

            <div class="row">
                <asp:UpdatePanel ID="up_gastos_menu" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="col-lg-4">
                            <div class="sidebar-nav">
                                <div class="navbar-default" role="navigation">
                                    <div class="navbar-header">
                                        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".sidebar-navbar-collapse"><span class="sr-only">Toggle navigation</span> <span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></button>
                                        <span class="visible-xs navbar-brand">Menú</span>
                                    </div>
                                    <div class="navbar-collapse collapse sidebar-navbar-collapse">
                                        <br />
                                        <div class="sidebar" style="display: block;">
                                            <ul class="nav">

                                                <li>
                                                    <asp:LinkButton CssClass="fuente_css02" ID="lkb_arqu" runat="server" OnClick="lkb_arqu_Click">
                                                        <i class="fas fa-pencil-alt" id="i_arqu" runat="server"></i>
                                                        <asp:Label CssClass="buttonClass" ID="lbl_arqu" runat="server" Text="ARQUITECTURA"></asp:Label>
                                                    </asp:LinkButton>
                                                </li>
                                                <li>
                                                    <asp:LinkButton CssClass="fuente_css02" ID="lkb_cali" runat="server" OnClick="lkb_cali_Click">
                                                        <i class="fas fa-check-double" id="i_cali" runat="server"></i>
                                                        <asp:Label CssClass="buttonClass" ID="lbl_cali" runat="server" Text="CALIDAD"></asp:Label>
                                                    </asp:LinkButton>
                                                </li>
                                                <li>
                                                    <asp:LinkButton CssClass="fuente_css02" ID="lkb_capt" runat="server" OnClick="lkb_capt_Click">
                                                        <i class="fas fa-file-signature" id="i_capt" runat="server"></i>
                                                        <asp:Label CssClass="buttonClass" ID="lbl_capt" runat="server" Text="CAPTURA"></asp:Label>
                                                    </asp:LinkButton>
                                                </li>
                                                <li>
                                                    <asp:LinkButton CssClass="fuente_css02" ID="lkb_cobr" runat="server" OnClick="lkb_cobr_Click">
                                                        <i class="fas fa-hand-holding-usd" id="i_cobr" runat="server"></i>
                                                        <asp:Label CssClass="buttonClass" ID="lbl_cobr" runat="server" Text="COBRANZA"></asp:Label>
                                                    </asp:LinkButton>
                                                </li>
                                                <li>
                                                    <asp:LinkButton CssClass="fuente_css02" ID="lkb_cont" runat="server" OnClick="lkb_cont_Click">
                                                        <i class="fas fa-dollar-sign" id="i_cont" runat="server"></i>
                                                        <asp:Label CssClass="buttonClass" ID="lbl_cont" runat="server" Text="CONTABILIDAD"></asp:Label>
                                                    </asp:LinkButton>
                                                </li>
                                                <li>
                                                    <asp:LinkButton CssClass="fuente_css02" ID="lkb_coor_labcent" runat="server" OnClick="lkb_coor_labcent_Click">
                                                        <i class="fas fa-vials" id="i_coor_labcent" runat="server"></i>
                                                        <asp:Label CssClass="buttonClass" ID="lbl_coor_labcent" runat="server" Text="COORD LAB CENTRAL"></asp:Label>
                                                    </asp:LinkButton>
                                                </li>
                                                <li>
                                                    <asp:LinkButton CssClass="fuente_css02" ID="lkb_desa_tec" runat="server" OnClick="lkb_desa_tec_Click">
                                                        <i class="fas fa-qrcode" id="i_desa_tec" runat="server"></i>
                                                        <asp:Label CssClass="buttonClass" ID="lbl_desa_tec" runat="server" Text="DESARROLLO DE TECNOLOGÍA"></asp:Label>
                                                    </asp:LinkButton>
                                                </li>
                                                <li>
                                                    <asp:LinkButton CssClass="fuente_css02" ID="lkb_desa_neg" runat="server" OnClick="lkb_desa_neg_Click">
                                                        <i class="fas fa-briefcase" id="i_desa_neg" runat="server"></i>
                                                        <asp:Label CssClass="buttonClass" ID="lbl_desa_neg" runat="server" Text="DESARROLLO NEGOCIOS"></asp:Label>
                                                    </asp:LinkButton>
                                                </li>
                                                <li>
                                                    <asp:LinkButton CssClass="fuente_css02" ID="lkb_estr" runat="server" OnClick="lkb_estr_Click">
                                                        <i class="fas fa-building" id="i_estr" runat="server"></i>
                                                        <asp:Label CssClass="buttonClass" ID="lbl_estr" runat="server" Text="ESTRUCTURAS"></asp:Label>
                                                    </asp:LinkButton>
                                                </li>
                                                <li>
                                                    <asp:LinkButton CssClass="fuente_css02" ID="lkb_gere_adm" runat="server" OnClick="lkb_gere_adm_Click">
                                                        <i class="fas fa-user-friends" id="i_gere_adm" runat="server"></i>
                                                        <asp:Label CssClass="buttonClass" ID="lbl_gere_adm" runat="server" Text="GERENCIA ADMINISTRATIVA"></asp:Label>
                                                    </asp:LinkButton>
                                                </li>
                                                <li>
                                                    <asp:LinkButton CssClass="fuente_css02" ID="lkb_gere_cal" runat="server" OnClick="lkb_gere_cal_Click">
                                                        <i class="fas fa-user-check" id="i_gere_cal" runat="server"></i>
                                                        <asp:Label CssClass="buttonClass" ID="lbl_gere_cal" runat="server" Text="GERENCIA CALIDAD"></asp:Label>
                                                    </asp:LinkButton>
                                                </li>
                                                <li>
                                                    <asp:LinkButton CssClass="fuente_css02" ID="lkb_gere_gen" runat="server" OnClick="lkb_gere_gen_Click">
                                                        <i class="fas fa-user-tie" id="i_gere_gen" runat="server"></i>
                                                        <asp:Label CssClass="buttonClass" ID="lbl_gere_gen" runat="server" Text="GERENCIA GENERAL"></asp:Label>
                                                    </asp:LinkButton>
                                                </li>
                                                <li>
                                                    <asp:LinkButton CssClass="fuente_css02" ID="lkb_gere_tec" runat="server" OnClick="lkb_gere_tec_Click">
                                                        <i class="fas fa-user-astronaut" id="i_gere_tec" runat="server"></i>
                                                        <asp:Label CssClass="buttonClass" ID="lbl_gere_tec" runat="server" Text="GERENCIA TÉCNICA"></asp:Label>
                                                    </asp:LinkButton>
                                                </li>
                                                <li>
                                                    <asp:LinkButton CssClass="fuente_css02" ID="lkb_mant" runat="server" OnClick="lkb_mant_Click">
                                                        <i class="fas fa-wrench" id="i_mant" runat="server"></i>
                                                        <asp:Label CssClass="buttonClass" ID="lbl_mant" runat="server" Text="MANTENIMIENTO"></asp:Label>
                                                    </asp:LinkButton>
                                                </li>
                                                <li>
                                                    <asp:LinkButton CssClass="fuente_css02" ID="lkb_meca_sue" runat="server" OnClick="lkb_meca_sue_Click">
                                                        <i class="fas fa-globe" id="i_meca_sue" runat="server"></i>
                                                        <asp:Label CssClass="buttonClass" ID="lbl_meca_sue" runat="server" Text="MECÁNICA DE SUELOS"></asp:Label>
                                                    </asp:LinkButton>
                                                </li>
                                                <li>
                                                    <asp:LinkButton CssClass="fuente_css02" ID="lkb_pers_obra" runat="server" OnClick="lkb_pers_obra_Click">
                                                        <i class="fas fa-warehouse" id="i_pers_obra" runat="server"></i>
                                                        <asp:Label CssClass="buttonClass" ID="lbl_pers_obra" runat="server" Text="PERSONAL EN OBRA"></asp:Label>
                                                    </asp:LinkButton>
                                                </li>
                                                <li>
                                                    <asp:LinkButton CssClass="fuente_css02" ID="lkb_recep" runat="server" OnClick="lkb_recep_Click">
                                                        <i class="fas fa-fax" id="i_recep" runat="server"></i>
                                                        <asp:Label CssClass="buttonClass" ID="lbl_recep" runat="server" Text="RECEPCIÓN"></asp:Label>
                                                    </asp:LinkButton>
                                                </li>
                                                <li>
                                                    <asp:LinkButton CssClass="fuente_css02" ID="lkb_recu_hum" runat="server" OnClick="lkb_recu_hum_Click">
                                                        <i class="fas fa-users" id="i_recu_hum" runat="server"></i>
                                                        <asp:Label CssClass="buttonClass" ID="lbl_recu_hum" runat="server" Text="RECURSOS HUMANOS"></asp:Label>
                                                    </asp:LinkButton>
                                                </li>
                                                <li>
                                                    <asp:LinkButton CssClass="fuente_css02" ID="lkb_supe" runat="server" OnClick="lkb_supe_Click">
                                                        <i class="fas fa-user-shield" id="i_supe" runat="server"></i>
                                                        <asp:Label CssClass="buttonClass" ID="lbl_supe" runat="server" Text="SUPERVISIÓN"></asp:Label>
                                                    </asp:LinkButton>
                                                </li>
                                                <br />
                                                <li>
                                                    <asp:LinkButton CssClass="fuente_css02" ID="lkb_salir" runat="server" OnClick="lkb_salir_Click">
                                                        <i class="fas fa-power-off" id="i_salir" runat="server"></i>
                                                        <asp:Label CssClass="buttonClass" ID="lbl_salir" runat="server" Text="SALIR"></asp:Label>
                                                    </asp:LinkButton>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <!--/.nav-collapse -->
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <asp:UpdatePanel ID="up_recu_hum" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="col-lg-8">
                            <div class="col-lg-12 ">
                                <div class="panel panel-default" id="pnl_recu_hum" runat="server" visible="false">
                                    <div class="panel-heading">
                                        <p class="text-right">
                                            <asp:LinkButton   ID="lkb_recu_hum_i" runat="server" OnClick="lkb_recu_hum_i_Click" ForeColor="white">
                                                <i class="fas fa-users" id="i_recu_hum_i" runat="server"></i>
                                                <asp:Label ID="lbl_recu_hum_i" runat="server" Text="RECURSOS HUMANOS"></asp:Label>
                                            </asp:LinkButton>
                                        </p>
                                    </div>
                                    <div class="panel-body">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="up_capt" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="col-lg-8">
                            <div class="col-lg-12 ">
                                <div class="panel panel-default" id="pnl_capt" runat="server" visible="false">
                                    <div class="panel-heading">
                                        <p class="text-right">
                                            <asp:LinkButton   ID="lkb_capt_i" runat="server" OnClick="lkb_capt_i_Click" ForeColor="white">
                                                <i class="fas fa-2x fa-file-signature" id="i_capt_i" runat="server"></i>
                                                <%--<asp:Label  ID="lbl_capt_i" runat="server" Text="CAPTURA"></asp:Label>--%>
                                            </asp:LinkButton>
                                        </p>
                                    </div>
                                    <div class="panel-body">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="up_cont" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="col-lg-8">
                            <div class="col-lg-12 ">
                                <div class="panel panel-default" id="pnl_cont" runat="server" visible="false">
                                    <div class="panel-heading">
                                        <p class="text-right">
                                            <asp:LinkButton   ID="lkb_cont_i" runat="server" OnClick="lkb_cont_i_Click" ForeColor="white">
                                                <i class="fas fa-2x fa-dollar-sign" id="i_cont_i" runat="server"></i>
                                                <%--<asp:Label  ID="lbl_cont_i" runat="server" Text="CONTABILIDAD"></asp:Label>--%>
                                            </asp:LinkButton>
                                        </p>
                                    </div>
                                    <div class="panel-body">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="up_desa_tec" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                         <div class="col-lg-8">
                            <div class="col-lg-12 ">
                                <div class="panel panel-default" id="pnl_desa_tec" runat="server" visible="false">
                                    <div class="panel-heading">
                                        <p class="text-right">
                                            <asp:LinkButton   ID="lkb_desa_tec_i" runat="server" OnClick="lkb_desa_tec_i_Click" ForeColor="white">
                                                <i class="fas fa-qrcode" id="i_desa_tec_i" runat="server"></i>
                                                <asp:Label  ID="lbl_desa_tec_i" runat="server" Text="DESARROLLO DE TECNOLOGÍA"></asp:Label>
                                            </asp:LinkButton>
                                        </p>
                                    </div>
                                    <div class="panel-body">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                     
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel"
            aria-hidden="true">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header encabezado001">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><i class="fa fa-window-close-o"></i></button>
                                <h4 class="modal-title">
                                    <asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label>
                                </h4>
                            </div>
                            <div class="modal-body">
                                <asp:Label CssClass="fuente_css02" ID="lblModalBody" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="modal-footer">
                                <button class="btn btn01" data-dismiss="modal" aria-hidden="true">OK <i class="fa fa-check-circle-o"></i></button>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
</body>
</html>
