﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pnl_prospectos.aspx.cs" Inherits="wa_liec.pnl_prospectos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="es-mx">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0" />

    <link href="Content/fontawesome/all.css" rel="stylesheet" />

    <link href="Content/bootstrap.css" rel="stylesheet" />

    <link href="styles/estilos_liec.css" rel="stylesheet" />

    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <script src="Scripts/bootstrap.js"></script>

    <link rel="shortcut icon" href="img/ico_liec.png" type="image/png" />

    <title>/ PROSPECTO</title>
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
            <asp:UpdatePanel ID="up_prospecto_bienvenida" runat="server" UpdateMode="Conditional">
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
                <asp:UpdatePanel ID="up_body" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <asp:UpdatePanel ID="up_prospecto" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <div class="col-lg-12">
                                    <div class="col-lg-12 ">
                                        <div class="row">
                                            <ul class="nav nav-tabs">

                                                <li>
                                                    <asp:LinkButton CssClass="buttonClass" ID="lkb_prosp_prosp" runat="server" OnClick="lkb_prosp_prosp_Click">
                                                        <asp:Label CssClass="buttonClass" ID="lbl_prosp_prosp" runat="server" Text="Prospecto"></asp:Label>&nbsp;<i class="fas fa-user-tag" id="i_prosp" runat="server"></i>
                                                    </asp:LinkButton></li>
                                                <li>
                                                    <asp:LinkButton CssClass="lkb_react" ID="lkb_adat_prosp" runat="server" OnClick="lkb_adat_prosp_Click">
                                                        <asp:Label CssClass="buttonClass" ID="lbl_adat_prosp" runat="server" Text="Anexo de Datos"></asp:Label>&nbsp;<i class="fas fa-user-tie" id="i_adat" runat="server"></i>
                                                    </asp:LinkButton></li>
                                                <li>
                                                    <asp:LinkButton CssClass="lkb_react" ID="lkb_seg_prosp" runat="server" OnClick="lkb_seg_prosp_Click">
                                                        <asp:Label CssClass="buttonClass" ID="lbl_seg_prosp" runat="server" Text="Seguimiento"></asp:Label>&nbsp;<i class="fas fa-user-tie" id="i_seg_prosp" runat="server"></i>
                                                    </asp:LinkButton></li>
                                                <li>
                                                    <asp:LinkButton CssClass="lkb_react" ID="lkb_salir" runat="server" OnClick="lkb_salir_Click">
                                                        <asp:Label CssClass="buttonClass" ID="lbl_salir" runat="server" Text="Salir"></asp:Label>&nbsp;<i class="fas fa-power-off" id="i_salir" runat="server"></i>
                                                    </asp:LinkButton></li>
                                            </ul>
                                            <div class="panel panel-default" id="pnl_prospecto" runat="server" visible="false">
                                                <div class="panel-heading">
                                                    <div class="row">
                                                        <div class="col-md-8 col-sm-8">
                                                            <div class="input-group" id="div_busc_clt" runat="server" visible="false">
                                                                <span class="input-group-addon">
                                                                    <asp:Label CssClass="control-label fuente_css02" ID="lbl_buscar_prospecto" runat="server" Text="*BUSCAR CLIENTE:"></asp:Label>
                                                                </span>
                                                                <asp:TextBox CssClass="form-control input-box" ID="txt_buscar_prospecto" runat="server" placeholder="letras/números" TextMode="Search" TabIndex="1"></asp:TextBox>
                                                                <span class="input-group-btn">
                                                                    <asp:Button CssClass="btn btn01" ID="btn_buscar_prospecto" runat="server" Text="ACEPTAR" TabIndex="2" OnClick="btn_buscar_prospecto_Click" />
                                                                </span>
                                                            </div>
                                                            <div class="text-right">
                                                                <ajaxToolkit:AutoCompleteExtender ID="ace_buscar_prospecto" runat="server" ServiceMethod="SearchCustomers" MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="true" CompletionSetCount="10" TargetControlID="txt_buscar_prospecto" FirstRowSelected="false"></ajaxToolkit:AutoCompleteExtender>
                                                                <asp:RequiredFieldValidator ID="rfv_buscar_prospecto" runat="server" ErrorMessage="*Obligatorio" ControlToValidate="txt_buscar_prospecto" ForeColor="white" Enabled="false"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4 col-sm-4">
                                                            <p class="text-right">
                                                                PROSPECTOS
                                                        <span>
                                                            <asp:LinkButton CssClass="btn btn02" ID="btn_agregar_prospecto" runat="server" ToolTip="AGREGAR CLIENTE" TabIndex="3" OnClick="btn_agregar_prospecto_Click">
                                                                <i class="fas fa-plus" id="i_agregar_prospecto" runat="server"></i>
                                                            </asp:LinkButton>
                                                            <asp:LinkButton CssClass="btn btn02" ID="btn_editar_prospecto" runat="server" ToolTip="EDITAR CLIENTE" TabIndex="4" OnClick="btn_editar_prospecto_Click">
                                                                <i class="far fa-edit" id="i_editar_prospecto" runat="server"></i>
                                                            </asp:LinkButton>
                                                        </span>
                                                                <br />
                                                                <asp:CheckBox ID="chkb_desactivar_prospecto" runat="server" AutoPostBack="true" Text="Desactivar validaciones" TabIndex="5" OnCheckedChanged="chkb_desactivar_prospecto_CheckedChanged" />
                                                            </p>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">

                                                        <div class="col-lg-12" runat="server" id="div_prospecto" visible="false">
                                                            <asp:GridView CssClass="table" ID="gv_prospecto" runat="server" AutoGenerateColumns="False" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" TabIndex="6" PageSize="5" OnPageIndexChanging="gv_prospecto_PageIndexChanging">
                                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chk_prospecto" runat="server" onclick="CheckOne(this)" AutoPostBack="true" OnCheckedChanged="chk_prospecto_CheckedChanged" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="id_prospecto" HeaderText="ID" SortExpression="id_prospecto" Visible="true" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                                                    <asp:BoundField DataField="cod_prospecto" HeaderText="ID" SortExpression="cod_prospecto" Visible="true" />
                                                                    <asp:BoundField DataField="empresa" HeaderText="Empresa" SortExpression="empresa" />
                                                                    <asp:BoundField DataField="nom_usr" HeaderText="Usuario" SortExpression="nom_usr" />
                                                                    <asp:BoundField DataField="fecha_registro" HeaderText="Fecha" SortExpression="fecha_registro" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="false" />
                                                                </Columns>
                                                                <EditRowStyle BackColor="#999999" />
                                                                <FooterStyle BackColor="#5D7B9D" ForeColor="White" />
                                                                <HeaderStyle BackColor="#104D8d" ForeColor="White" />

                                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-2">
                                                            <div class="form-group text-left">

                                                                <asp:Label CssClass="control-label fuente_css02" ID="lbl_tipocont_prospecto" runat="server" Text="*Tipo de Contacto"></asp:Label>

                                                                <asp:DropDownList CssClass="form-control input-box" ID="ddl_tipocont_prospecto" runat="server" TabIndex="15" BackColor="LightGray" ForeColor="#104D8D"></asp:DropDownList>
                                                                <div class="text-right">
                                                                    <asp:RequiredFieldValidator ID="rfv_tipocont_prospecto" runat="server" ErrorMessage="*Obligatorio" ControlToValidate="ddl_tipocont_prospecto" InitialValue="0" ForeColor="DarkRed" Enabled="false" TabIndex="14"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-2">
                                                            <div class="form-group text-left">

                                                                <asp:Label CssClass="control-label fuente_css02" ID="lbl_giro_prospecto" runat="server" Text="Giro Empresa"></asp:Label>

                                                                <asp:DropDownList CssClass="form-control input-box" ID="ddl_giro_prospecto" runat="server" TabIndex="15" BackColor="LightGray" ForeColor="#104D8D"></asp:DropDownList>
                                                                <div class="text-right">
                                                                    <asp:RequiredFieldValidator ID="rfv_giro_prospecto" runat="server" ErrorMessage="*Obligatorio" ControlToValidate="ddl_giro_prospecto" InitialValue="0" ForeColor="DarkRed" Enabled="false" TabIndex="14"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <div class="form-group text-left">

                                                                <asp:Label CssClass="control-label fuente_css02" ID="lbl_emp_prospecto" runat="server" Text="*Empresa"></asp:Label>

                                                                <asp:TextBox CssClass="form-control input-box" ID="txt_emp_prospecto" runat="server" placeholder="letras/números" ToolTip="letras/números" TabIndex="9"></asp:TextBox>
                                                                <div class="text-right">
                                                                    <asp:RequiredFieldValidator ID="rfv_emp_prospecto" runat="server" ErrorMessage="*Obligatorio" ControlToValidate="txt_emp_prospecto" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-lg-2">
                                                            <div class="form-group text-left">

                                                                <asp:Label CssClass="control-label fuente_css02" ID="lbl_serv_prospecto" runat="server" Text="Servicio LIEC"></asp:Label>

                                                                <asp:DropDownList CssClass="form-control input-box" ID="ddl_serv_prospecto" runat="server" TabIndex="15" BackColor="LightGray" ForeColor="#104D8D"></asp:DropDownList>
                                                                <div class="text-right">
                                                                    <asp:RequiredFieldValidator ID="rfv_serv_prospecto" runat="server" ErrorMessage="*Obligatorio" ControlToValidate="ddl_serv_prospecto" InitialValue="0" ForeColor="DarkRed" Enabled="false" TabIndex="14"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-4">
                                                            <div class="form-group text-left">
                                                                <asp:Label CssClass="control-label fuente_css02" ID="lbl_callenum_prospecto" runat="server" Text="Calle ý número"></asp:Label>
                                                                <asp:TextBox CssClass="form-control input-box" ID="txt_callenum_prospecto" runat="server" placeholder="letras/números" ToolTip="letras/números" TextMode="MultiLine" BackColor="LightGray" ForeColor="#104D8D" TabIndex="12"></asp:TextBox>
                                                                <div class="text-right">
                                                                    <asp:RequiredFieldValidator ID="rfv_callenum_prospecto" runat="server" ErrorMessage="*Obligatorio" ControlToValidate="txt_callenum_prospecto" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-2">
                                                            <div class="form-group text-left">

                                                                <asp:Label CssClass="control-label fuente_css02" ID="lbl_cp_prospecto" runat="server" Text="Código Postal"></asp:Label>

                                                                <div class="input-group">
                                                                    <asp:TextBox CssClass="form-control input-box" ID="txt_cp_prospecto" runat="server" placeholder="5 (0-9)" MaxLength="5" ToolTip="Un código postal valido consiste en 5 numeros con valores del 0-9" TabIndex="13"></asp:TextBox>
                                                                    <ajaxToolkit:MaskedEditExtender ID="mee_cp_prospecto" runat="server" TargetControlID="txt_cp_prospecto" Mask="99999" />
                                                                    <span class="input-group-btn">
                                                                        <asp:Button CssClass="btn btn02" ID="btn_cp_prospecto" runat="server" Text="VALIDAR" TabIndex="14" OnClick="btn_cp_prospecto_Click" />
                                                                    </span>
                                                                </div>
                                                                <div class="text-right">
                                                                    <asp:RequiredFieldValidator ID="rfv_cp_prospecto" runat="server" ErrorMessage="*Obligatorio" ControlToValidate="txt_cp_prospecto" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-2">
                                                            <div class="form-group text-left">

                                                                <asp:Label CssClass="control-label fuente_css02" ID="lbl_colonia_prospecto" runat="server" Text="Colonia"></asp:Label>

                                                                <asp:DropDownList CssClass="form-control input-box" ID="ddl_colonia_prospecto" runat="server" TabIndex="15" BackColor="LightGray" ForeColor="#104D8D"></asp:DropDownList>
                                                                <div class="text-right">
                                                                    <asp:RequiredFieldValidator ID="rfv_colonia_prospecto" runat="server" ErrorMessage="*Obligatorio" ControlToValidate="ddl_colonia_prospecto" InitialValue="0" ForeColor="DarkRed" Enabled="false" TabIndex="14"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-2">

                                                            <div class="form-group text-left">

                                                                <asp:Label CssClass="control-label fuente_css02" ID="lbl_municipio_prospecto" runat="server" Text="Municipio"></asp:Label>

                                                                <asp:TextBox CssClass="form-control input-box" ID="txt_municipio_prospecto" runat="server" placeholder="letras/números" Enabled="false" BackColor="LightGray" TabIndex="16"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-2">
                                                            <div class="form-group text-left">

                                                                <asp:Label CssClass="control-label fuente_css02" ID="lbl_estado_prospecto" runat="server" Text="Estado"></asp:Label>

                                                                <asp:TextBox CssClass="form-control input-box" ID="txt_estado_prospecto" runat="server" placeholder="letras/números" Enabled="false" BackColor="LightGray" TabIndex="17"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="text-right">

                                                            <asp:Button CssClass="btn btn02" ID="btn_gprosp_alt" runat="server" Text="GUARDAR" TabIndex="18" Visible="false" OnClick="btn_gprosp_alt_Click" />
                                                        </div>
                                                    </div>
                                                    <div runat="server" id="div_cont_prosp" visible="false">
                                                        <div class="row">
                                                            <div class="col-lg-3">
                                                                <div class="form-group text-left">

                                                                    <asp:Label CssClass="control-label fuente_css02" ID="lbl_dpto_prosp" runat="server" Text="Departamento contacto"></asp:Label>

                                                                    <asp:TextBox CssClass="form-control input-box" ID="txt_dpto_prosp" runat="server" placeholder="letras/números" ToolTip="letras/números" TabIndex="9"></asp:TextBox>
                                                                    <div class="text-right">
                                                                        <asp:RequiredFieldValidator ID="rfv_dpto_prosp" runat="server" ErrorMessage="*Obligatorio" ControlToValidate="txt_dpto_prosp" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-3">
                                                                <div class="form-group text-left">

                                                                    <asp:Label CssClass="control-label fuente_css02" ID="lbl_cont_prospecto" runat="server" Text="Contacto"></asp:Label>

                                                                    <asp:TextBox CssClass="form-control input-box" ID="txt_cont_prospecto" runat="server" placeholder="letras/números" ToolTip="letras/números" TabIndex="9"></asp:TextBox>
                                                                    <div class="text-right">
                                                                        <asp:RequiredFieldValidator ID="rfv_cont_prospecto" runat="server" ErrorMessage="*Obligatorio" ControlToValidate="txt_cont_prospecto" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-3">
                                                                <div class="form-group text-left">
                                                                    <asp:Label CssClass="control-label fuente_css02" ID="lbl_tel1_prospecto" runat="server" Text="Teléfono"></asp:Label>
                                                                    <asp:TextBox CssClass="form-control input-box" ID="txt_tel1_prospecto" runat="server" MaxLength="30" placeholder="000-000-00000000x00000/00000" TextMode="Phone" ToolTip="Un número de teléfono válido consiste en un código de lada de 3 dígitos, un guión (-),un código de área de 3 dígitos, un guión (-) y el número telefónico con valores del 0 al 9" TabIndex="10"></asp:TextBox>
                                                                    <div class="text-right">
                                                                        <asp:RegularExpressionValidator ID="rev_tel1_prospecto" runat="server"
                                                                            ErrorMessage="Formato Invalido" ControlToValidate="txt_tel1_prospecto"
                                                                            ValidationExpression="[0-9]{3}[-][0-9]{3}[-][0-9]{8}[x][0-9]{5}[/][0-9]{5}" ForeColor="DarkRed">
                                                                        </asp:RegularExpressionValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-3">
                                                                <div class="form-group text-left">
                                                                    <asp:Label CssClass="control-label fuente_css02" ID="lbl_tel2_prospecto" runat="server" Text="Teléfono"></asp:Label>
                                                                    <asp:TextBox CssClass="form-control input-box" ID="txt_tel2_prospecto" runat="server" MaxLength="30" placeholder="000-000-00000000x00000/00000" TextMode="Phone" ToolTip="Un número de teléfono válido consiste en un código de lada de 3 dígitos, un guión (-),un código de área de 3 dígitos, un guión (-) y el número telefónico con valores del 0 al 9" TabIndex="10"></asp:TextBox>
                                                                    <div class="text-right">
                                                                        <asp:RegularExpressionValidator ID="rev_tel2_prospecto" runat="server"
                                                                            ErrorMessage="Formato Invalido" ControlToValidate="txt_tel2_prospecto"
                                                                            ValidationExpression="[0-9]{3}[-][0-9]{3}[-][0-9]{8}[x][0-9]{5}[/][0-9]{5}" ForeColor="DarkRed">
                                                                        </asp:RegularExpressionValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-4">
                                                                <div class="form-group text-left">
                                                                    <asp:Label CssClass="control-label fuente_css02" ID="lbl_email1_prospecto" runat="server" Text="e-mail"></asp:Label>
                                                                    <asp:TextBox CssClass="form-control input-box" ID="txt_email1_prospecto" runat="server" placeholder="xxxx@xxxx.xxx" TextMode="Email" ToolTip="xxxx@xxxx.xxx" TabIndex="11"></asp:TextBox>
                                                                    <br />
                                                                </div>

                                                            </div>

                                                            <div class="col-lg-4">
                                                                <div class="form-group text-left">
                                                                    <asp:Label CssClass="control-label fuente_css02" ID="lbl_email2_prospecto" runat="server" Text="e-mail"></asp:Label>
                                                                    <asp:TextBox CssClass="form-control input-box" ID="txt_email2_prospecto" runat="server" placeholder="xxxx@xxxx.xxx" TextMode="Email" ToolTip="xxxx@xxxx.xxx" TabIndex="11"></asp:TextBox>
                                                                    <br />
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4">
                                                                <div class="form-group text-left">
                                                                    <asp:Label CssClass="control-label fuente_css02" ID="Label1" runat="server" Text="Web"></asp:Label>
                                                                    <asp:TextBox CssClass="form-control input-box" ID="TextBox1" runat="server" placeholder="letras/números" BackColor="LightGray" ForeColor="#104D8D" TabIndex="17"></asp:TextBox>
                                                                </div>

                                                            </div>

                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-3">
                                                                <div class="form-group text-left">

                                                                    <asp:Label CssClass="control-label fuente_css02" ID="lbl_acc_prospecto" runat="server" Text="Acción"></asp:Label>

                                                                    <asp:DropDownList CssClass="form-control input-box" ID="ddl_acc_prospecto" runat="server" TabIndex="16" BackColor="LightGray" ForeColor="#104D8D"></asp:DropDownList>
                                                                    <div class="text-right">
                                                                        <asp:RequiredFieldValidator ID="rfv_acc_prospecto" runat="server" ErrorMessage="*Obligatorio" ControlToValidate="ddl_acc_prospecto" InitialValue="0" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6">
                                                                <div class="form-group text-left">
                                                                    <asp:Label CssClass="control-label fuente_css02" ID="lbl_prospecto_coment" runat="server" Text="Comentarios"></asp:Label>

                                                                    <asp:TextBox CssClass="form-control input-box" ID="txt_prospecto_coment" runat="server" placeholder="letras/números" TextMode="MultiLine" BackColor="LightGray" ForeColor="#104D8D" TabIndex="17"></asp:TextBox>
                                                                    <div class="text-right">
                                                                        <asp:RequiredFieldValidator ID="rfv_prospecto_coment" runat="server" ErrorMessage="*Obligatorio" ControlToValidate="txt_prospecto_coment" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-3">
                                                                <div class="text-right">

                                                                    <br />
                                                                    <asp:Button CssClass="btn btn02" ID="btn_guardar_prospecto" runat="server" Text="GUARDAR" TabIndex="18" OnClick="btn_guardar_prospecto_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel panel-default" id="pnl_prospcontf" runat="server" visible="false">
                                                <div class="panel-heading">
                                                    <div class="row">
                                                        <div class="col-md-8 col-sm-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon">
                                                                    <asp:Label CssClass="control-label fuente_css02" ID="lbl_buscar_prospcontf" runat="server" Text="*BUSCAR Cliente:"></asp:Label>
                                                                </span>
                                                                <asp:TextBox CssClass="form-control input-box" ID="txt_buscar_prospcontf" runat="server" placeholder="letras/números" TextMode="Search" TabIndex="1"></asp:TextBox>
                                                                <span class="input-group-btn">
                                                                    <asp:Button CssClass="btn btn01" ID="btn_buscar_prospcontf" runat="server" Text="ACEPTAR" TabIndex="2" OnClick="btn_buscar_prospcontf_Click" />
                                                                </span>
                                                            </div>
                                                            <div class="text-right">
                                                                <ajaxToolkit:AutoCompleteExtender ID="ace_buscar_prospcontf" runat="server" ServiceMethod="SearchCustomers" MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="true" CompletionSetCount="10" TargetControlID="txt_buscar_prospcontf" FirstRowSelected="false"></ajaxToolkit:AutoCompleteExtender>
                                                                <asp:RequiredFieldValidator ID="rfv_buscar_prospcontf" runat="server" ErrorMessage="*Obligatorio" ControlToValidate="txt_buscar_prospcontf" ForeColor="white" Enabled="false"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4 col-sm-4">
                                                            <p class="text-right">
                                                                CONTACTOS PROSPECTOS
            <span>
                <asp:LinkButton CssClass="btn btn02" ID="btn_agregar_prospcontf" runat="server" ToolTip="AGREGAR CONTACTO" TabIndex="3" OnClick="btn_agregar_prospcontf_Click">
                    <i class="fas fa-plus" id="i_agregar_prospcontf" runat="server"></i>
                </asp:LinkButton>
                <asp:LinkButton CssClass="btn btn02" ID="btn_editar_prospcontf" runat="server" ToolTip="EDITAR CONTACTO" TabIndex="4" OnClick="btn_editar_prospcontf_Click">
                    <i class="far fa-edit" id="i_editar_prospcontf" runat="server"></i>
                </asp:LinkButton>
            </span>
                                                                <br />
                                                                <asp:CheckBox ID="chkb_desactivar_prospcontf" runat="server" AutoPostBack="true" Text="Desactivar validaciones" TabIndex="5" OnCheckedChanged="chkb_desactivar_prospcontf_CheckedChanged" />
                                                            </p>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <asp:GridView CssClass="table" ID="gv_cont_prosp" runat="server" AutoGenerateColumns="False" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" TabIndex="6" PageSize="5">
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chk_cont_prosp" runat="server" onclick="CheckOne(this)" AutoPostBack="true" OnCheckedChanged="chk_cont_prosp_CheckedChanged" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="id_cont_prosp" HeaderText="ID" SortExpression="id_cont_prosp" Visible="true" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                                                <asp:BoundField DataField="dpto" HeaderText="Departamento" SortExpression="dpto" />
                                                                <asp:BoundField DataField="contacto" HeaderText="Contacto" SortExpression="contacto" />
                                                                <asp:BoundField DataField="fecha_registro" HeaderText="REGISTRO" SortExpression="fecha_registro" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="false" />
                                                            </Columns>
                                                            <EditRowStyle BackColor="#999999" />
                                                            <FooterStyle BackColor="#5D7B9D" ForeColor="White" />
                                                            <HeaderStyle BackColor="#104D8d" ForeColor="White" />
                                                            <PagerSettings Mode="NumericFirstLast" FirstPageText="Inicio" LastPageText="Final" />
                                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                        </asp:GridView>
                                                    </div>

                                                    <div runat="server" id="div_cont_prospf" visible="true">
                                                        <div class="row">
                                                            <div class="col-lg-2">
                                                                <div class="form-group text-left">

                                                                    <asp:Label CssClass="control-label fuente_css02" ID="lbl_dpto_prospcontf" runat="server" Text="Departamento contacto"></asp:Label>

                                                                    <asp:TextBox CssClass="form-control input-box" ID="txt_dpto_prospcontf" runat="server" placeholder="letras/números" ToolTip="letras/números" TabIndex="9"></asp:TextBox>
                                                                    <div class="text-right">
                                                                        <asp:RequiredFieldValidator ID="rfv_dpto_prospcontf" runat="server" ErrorMessage="*Obligatorio" ControlToValidate="txt_dpto_prospcontf" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4">
                                                                <div class="form-group text-left">

                                                                    <asp:Label CssClass="control-label fuente_css02" ID="lbl_cont_prospcontf" runat="server" Text="*Contacto"></asp:Label>

                                                                    <asp:TextBox CssClass="form-control input-box" ID="txt_cont_prospcontf" runat="server" placeholder="letras/números" ToolTip="letras/números" TabIndex="9"></asp:TextBox>
                                                                    <div class="text-right">
                                                                        <asp:RequiredFieldValidator ID="rfv_cont_prospcontf" runat="server" ErrorMessage="*Obligatorio" ControlToValidate="txt_cont_prospcontf" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-3">
                                                                <div class="form-group text-left">
                                                                    <asp:Label CssClass="control-label fuente_css02" ID="lbl_tel1_prospcontf" runat="server" Text="Teléfono"></asp:Label>
                                                                    <asp:TextBox CssClass="form-control input-box" ID="txt_tel1_prospcontf" runat="server" MaxLength="30" placeholder="000-000-00000000x00000/00000" TextMode="Phone" ToolTip="Un número de teléfono válido consiste en un código de lada de 3 dígitos, un guión (-),un código de área de 3 dígitos, un guión (-) y el número telefónico con valores del 0 al 9" TabIndex="10"></asp:TextBox>
                                                                    <div class="text-right">
                                                                        <asp:RegularExpressionValidator ID="rev_tel1_prospcontf" runat="server"
                                                                            ErrorMessage="Formato Invalido" ControlToValidate="txt_tel1_prospcontf"
                                                                            ValidationExpression="[0-9]{3}[-][0-9]{3}[-][0-9]{8}[x][0-9]{5}[/][0-9]{5}" ForeColor="DarkRed">
                                                                        </asp:RegularExpressionValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-3">
                                                                <div class="form-group text-left">
                                                                    <asp:Label CssClass="control-label fuente_css02" ID="lbl_tel2_prospcontf" runat="server" Text="Teléfono"></asp:Label>
                                                                    <asp:TextBox CssClass="form-control input-box" ID="txt_tel2_prospcontf" runat="server" MaxLength="30" placeholder="000-000-00000000x00000/00000" TextMode="Phone" ToolTip="Un número de teléfono válido consiste en un código de lada de 3 dígitos, un guión (-),un código de área de 3 dígitos, un guión (-) y el número telefónico con valores del 0 al 9" TabIndex="10"></asp:TextBox>
                                                                    <div class="text-right">
                                                                        <asp:RegularExpressionValidator ID="rev_tel2_prospcontf" runat="server"
                                                                            ErrorMessage="Formato Invalido" ControlToValidate="txt_tel2_prospcontf"
                                                                            ValidationExpression="[0-9]{3}[-][0-9]{3}[-][0-9]{8}[x][0-9]{5}[/][0-9]{5}" ForeColor="DarkRed">
                                                                        </asp:RegularExpressionValidator>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-4">
                                                                <div class="form-group text-left">
                                                                    <asp:Label CssClass="control-label fuente_css02" ID="lbl_email1_prospcontf" runat="server" Text="e-mail"></asp:Label>
                                                                    <asp:TextBox CssClass="form-control input-box" ID="txt_email1_prospcontf" runat="server" placeholder="xxxx@xxxx.xxx" TextMode="Email" ToolTip="xxxx@xxxx.xxx" TabIndex="11"></asp:TextBox>
                                                                    <br />
                                                                </div>

                                                            </div>

                                                            <div class="col-lg-4">
                                                                <div class="form-group text-left">
                                                                    <asp:Label CssClass="control-label fuente_css02" ID="lbl_email2_prospcontf" runat="server" Text="e-mail"></asp:Label>
                                                                    <asp:TextBox CssClass="form-control input-box" ID="txt_email2_prospcontf" runat="server" placeholder="xxxx@xxxx.xxx" TextMode="Email" ToolTip="xxxx@xxxx.xxx" TabIndex="11"></asp:TextBox>
                                                                    <br />
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4">
                                                                <div class="form-group text-left">
                                                                <div class="text-right">
                                                                    <br />
                                                                    <asp:Button CssClass="btn btn02" ID="btn_guardar_prospcontf" runat="server" Text="GUARDAR" TabIndex="18" OnClick="btn_guardar_prospcontf_Click" />
                                                                </div>  
                                                                </div>
                                                                
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lkb_salir" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </ContentTemplate>
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
