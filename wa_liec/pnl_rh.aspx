<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pnl_rh.aspx.cs" Inherits="wa_liec.pnl_rh" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />

    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0" />
    <!-- Bootstrap -->

    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="styles/estilos_liec.css" rel="stylesheet" />

    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.3.1/css/all.css" integrity="sha384-mzrmE5qonljUremFsqc01SB46JvROS7bZs3IO2EmfFsd15uHvIt+Y8vEf7N7fWAU" crossorigin="anonymous">

    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <script src="Scripts/dist/jquery.maskMoney.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>

    <link rel="shortcut icon" href="img/ico_liec.png" type="image/png" />
    <title>/ RH </title>
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
                        <div class="col-lg-3">
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
                                                    <asp:LinkButton CssClass="fuente_css02" ID="lkb_usr" runat="server" OnClick="lkb_usr_Click">
                                                        <i class="fas fa-user-cog" id="i_usr" runat="server"></i>
                                                        <asp:Label CssClass="buttonClass" ID="lbl_usr" runat="server" Text="USUARIOS"></asp:Label>
                                                    </asp:LinkButton>
                                                </li>
                                                <li>
                                                    <asp:LinkButton CssClass="fuente_css02" ID="lkb_med" runat="server">
                                                        <i class="fas fa-user-md" id="i_med" runat="server"></i>
                                                        <asp:Label CssClass="buttonClass" ID="lbl_med" runat="server" Text="MÉDICOS"></asp:Label>
                                                    </asp:LinkButton>
                                                </li>
                                                <li>
                                                    <asp:LinkButton CssClass="fuente_css02" ID="lkb_fin" runat="server">
                                                        <i class="fas fa-money-check-alt" id="i_fin" runat="server"></i>
                                                        <asp:Label CssClass="buttonClass" ID="lbl_fin" runat="server" Text="BANCARIOS"></asp:Label>
                                                    </asp:LinkButton>
                                                </li>
                                                <li>
                                                    <asp:LinkButton CssClass="fuente_css02" ID="lkb_cap" runat="server">
                                                        <i class="fas fa-user-graduate" id="i_cap" runat="server"></i>
                                                        <asp:Label CssClass="buttonClass" ID="lbl_cap" runat="server" Text="CAPACITACIÓN"></asp:Label>
                                                    </asp:LinkButton>
                                                </li>
                                                <br />
                                                <li>
                                                    <asp:LinkButton CssClass="fuente_css02" ID="lkb_salir" runat="server">
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

                <asp:UpdatePanel ID="up_usrs" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="col-lg-9">
                            <div class="col-lg-12 ">
                                <div class="panel panel-default" id="pnl_usrs" runat="server" visible="false">
                                    <div class="panel-heading">
                                        <p class="text-left">
                                            <div class="input-group" id="div_busc_usr" runat="server" visible="false">
                                                <span class="input-group-addon">
                                                    <asp:Label CssClass="control-label fuente_css02" ID="lbl_buscar_usrs" runat="server" Text="*BUSCAR USUARIO:"></asp:Label>
                                                </span>
                                                <asp:TextBox CssClass="form-control input-box" ID="txt_buscar_usrs" runat="server" placeholder="letras/números" TextMode="Search" TabIndex="1"></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <asp:Button CssClass="btn btn01" ID="btn_buscar_usrs" runat="server" Text="ACEPTAR" TabIndex="2" OnClick="btn_buscar_usrs_Click" />
                                                </span>
                                            </div>
                                            <div class="text-right">
                                                <ajaxToolkit:AutoCompleteExtender ID="ace_buscar_usrs" runat="server" ServiceMethod="SearchCustomers" MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="true" CompletionSetCount="10" TargetControlID="txt_buscar_usrs" FirstRowSelected="false"></ajaxToolkit:AutoCompleteExtender>
                                                <asp:RequiredFieldValidator ID="rfv_buscar_usrs" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_buscar_usrs" ForeColor="white" Enabled="false"></asp:RequiredFieldValidator>
                                            </div>
                                            <p>
                                            </p>
                                            <p class="text-right">
                                                REGISTRO DE USUARIOS <span>
                                                    <asp:LinkButton ID="btn_agregar_usrs" runat="server" CssClass="btn btn02" TabIndex="3" ToolTip="AGREGAR USUARIO" OnClick="btn_agregar_usrs_Click">
                                                        <i class="fas fa-plus" id="i_agregar_usrs" runat="server"></i>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btn_editar_usrs" runat="server" CssClass="btn btn02" TabIndex="4" ToolTip="EDITAR USUARIO" OnClick="btn_editar_usrs_Click">
                                                        <i class="far fa-edit" id="i_editar_usrs" runat="server"></i>
                                                    </asp:LinkButton>
                                                </span>
                                                <br />
                                                <asp:CheckBox ID="chkb_desactivar_usrs" runat="server" AutoPostBack="true" Text="Desactivar validaciones" TabIndex="5" OnCheckedChanged="chkb_desactivar_usrs_CheckedChanged" />
                                            </p>
                                        </p>
                                    </div>
                                    <div class="panel-body">
                                        <div class="col-lg-12">
                                            <asp:GridView CssClass="table" ID="gv_usrs" runat="server" AutoGenerateColumns="False" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" TabIndex="5" OnRowDataBound="gv_usrs_RowDataBound">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk_usrs" runat="server" onclick="CheckOne(this)" AutoPostBack="true" OnCheckedChanged="chk_usrs_CheckedChanged" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="cod_usr" HeaderText="ID" SortExpression="cod_usr" Visible="true" />

                                                    <asp:BoundField DataField="nom_cmpleto" HeaderText="NOMBRE COMPLETO" SortExpression="nom_cmpleto" />

                                                    <asp:BoundField DataField="fecha_registro" HeaderText="REGISTRO" SortExpression="fecha_registro" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="false" />
                                                    <asp:TemplateField HeaderText="ESTATUS">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddl_usrs_estatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_usrs_estatus_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
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
                                        <div class="col-lg-4">
                                            <div class="form-group text-left">

                                                <asp:Label CssClass="control-label fuente_css02" ID="lbl_area_usr" runat="server" Text="*Área"></asp:Label>

                                                <asp:DropDownList CssClass="form-control input-box" ID="ddl_area_usr" runat="server" TabIndex="6" Style="color: #104D8D; background-color: #D3D3D3;"></asp:DropDownList>
                                                <div class="text-right">
                                                    <asp:RequiredFieldValidator ID="rfv_area_usr" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="ddl_area_usr" InitialValue="0" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group text-left">

                                                <asp:Label CssClass="control-label fuente_css02" ID="lbl_perfil_usr" runat="server" Text="*Perfil"></asp:Label>

                                                <asp:DropDownList CssClass="form-control input-box" ID="ddl_perfil_usr" runat="server" TabIndex="7" Style="color: #104D8D; background-color: #D3D3D3;"></asp:DropDownList>
                                                <div class="text-right">
                                                    <asp:RequiredFieldValidator ID="rfv_perfil_usr" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="ddl_perfil_usr" InitialValue="0" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group text-left">

                                                <asp:Label CssClass="control-label fuente_css02" ID="lbl_fnac_usr" runat="server" Text="Cumpleaños"></asp:Label>

                                                <asp:TextBox CssClass="form-control input-box" ID="txt_fnac_usr" runat="server" placeholder="letras/números" ToolTip="letras/números" TextMode="Date" BackColor="LightGray" ForeColor="#104D8D" TabIndex="8"></asp:TextBox>
                                                <div class="text-right">
                                                    <asp:RequiredFieldValidator ID="rfv_fnac_usr" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_fnac_usr" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">

                                            <div class="form-group text-left">

                                                <asp:Label CssClass="control-label fuente_css02" ID="lbl_nombre_usr" runat="server" Text="*Nombre"></asp:Label>

                                                <asp:TextBox CssClass="form-control input-box" ID="txt_nombre_usr" runat="server" placeholder="letras/números" ToolTip="letras/números" TabIndex="9"></asp:TextBox>
                                                <div class="text-right">
                                                    <asp:RequiredFieldValidator ID="rfv_nombre_usr" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_nombre_usr" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group text-left">

                                                <asp:Label CssClass="control-label fuente_css02" ID="lbl_apaterno_usr" runat="server" Text="*Apellido Materno"></asp:Label>

                                                <asp:TextBox CssClass="form-control input-box" ID="txt_apaterno_usr" runat="server" placeholder="letras/números" ToolTip="letras/números" BackColor="LightGray" ForeColor="#104D8D" TabIndex="10"></asp:TextBox>
                                                <div class="text-right">
                                                    <asp:RequiredFieldValidator ID="rfv_apaterno_usr" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_apaterno_usr" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="form-group text-left">

                                                <asp:Label CssClass="control-label fuente_css02" ID="lbl_amaterno_usr" runat="server" Text="*Apellido Materno"></asp:Label>

                                                <asp:TextBox CssClass="form-control input-box" ID="txt_amaterno_usr" runat="server" placeholder="letras/números" ToolTip="letras/números" BackColor="LightGray" ForeColor="#104D8D" TabIndex="11"></asp:TextBox>
                                                <div class="text-right">
                                                    <asp:RequiredFieldValidator ID="rfv_amaterno_usr" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_amaterno_usr" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group text-left">

                                                <asp:Label CssClass="control-label fuente_css02" ID="lbl_tel_usr" runat="server" Text="Teléfono"></asp:Label>

                                                <asp:TextBox CssClass="form-control input-box" ID="txt_tel_usr" runat="server" MaxLength="16" placeholder="000-000-00000000" TextMode="Phone" ToolTip="Un número de teléfono válido consiste en un código de lada de 3 dígitos, un guión (-),un código de área de 3 dígitos, un guión (-) y el número telefónico con valores del 0 al 9" TabIndex="12"></asp:TextBox>
                                                <div class="text-right">
                                                    <asp:RegularExpressionValidator ID="revPhone" runat="server"
                                                        ErrorMessage="Formato Invalido" ControlToValidate="txt_tel_usr"
                                                        ValidationExpression="[0-9]{3}[-][0-9]{3}[-][0-9]{8}" ForeColor="DarkRed">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                            <div class="form-group text-left">

                                                <asp:Label CssClass="control-label fuente_css02" ID="lbl_email_usr" runat="server" Text="Correo electrónico"></asp:Label>

                                                <asp:TextBox CssClass="form-control input-box" ID="txt_email_usr" runat="server" placeholder="xxxx@xxxx.xxx" TextMode="Email" ToolTip="xxxx@xxxx.xxx" TabIndex="13"></asp:TextBox>
                                                <br />
                                            </div>
                                        </div>
                                        <div class="col-lg-12">
                                            <div class="form-group text-left">

                                                <asp:Label CssClass="control-label fuente_css02" ID="lbl_callenum_usr" runat="server" Text="*Calle ý número"></asp:Label>

                                                <asp:TextBox CssClass="form-control input-box" ID="txt_callenum_usr" runat="server" placeholder="letras/numeros" ToolTip="letras/numeros" TextMode="MultiLine" BackColor="LightGray" ForeColor="#104D8D" TabIndex="14"></asp:TextBox>
                                                <div class="text-right">
                                                    <asp:RequiredFieldValidator ID="rfv_callenum_usr" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_callenum_usr" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <div class="form-group text-left">

                                                        <asp:Label CssClass="control-label fuente_css02" ID="lbl_cp_usr" runat="server" Text="*Código Postal"></asp:Label>

                                                        <div class="input-group">
                                                            <asp:TextBox CssClass="form-control input-box" ID="txt_cp_usr" runat="server" placeholder="5[0-9]" MaxLength="5" ToolTip="Un código postal valido consiste en 5 numeros con valores del 0-9" TabIndex="15"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="mee_cp_usr" runat="server" TargetControlID="txt_cp_usr" Mask="99999" />
                                                            <span class="input-group-btn">
                                                                <asp:Button CssClass="btn btn02" ID="btn_cp_usr" runat="server" Text="VALIDAR" TabIndex="16" OnClick="btn_cp_usr_Click" />
                                                            </span>
                                                        </div>
                                                        <div class="text-right">
                                                            <asp:RequiredFieldValidator ID="rfv_cp_usr" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_cp_usr" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="form-group text-left">

                                                        <asp:Label CssClass="control-label fuente_css02" ID="lbl_col_usr" runat="server" Text="*Colonia"></asp:Label>

                                                        <asp:DropDownList CssClass="form-control input-box" ID="ddl_col_usr" runat="server" TabIndex="17" BackColor="LightGray" ForeColor="#104D8D"></asp:DropDownList>
                                                        <div class="text-right">
                                                            <asp:RequiredFieldValidator ID="rfv_col_usr" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="ddl_col_usr" InitialValue="0" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <div class="form-group text-left">

                                                        <asp:Label CssClass="control-label fuente_css02" ID="lbl_municipio_usr" runat="server" Text="Municipio"></asp:Label>

                                                        <asp:TextBox CssClass="form-control input-box" ID="txt_municipio_usr" runat="server" placeholder="letras/numeros" Enabled="false" BackColor="LightGray"></asp:TextBox>
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="form-group text-left">

                                                        <asp:Label CssClass="control-label fuente_css02" ID="lbl_estado_usr" runat="server" Text="Estado"></asp:Label>

                                                        <asp:TextBox CssClass="form-control input-box" ID="txt_estado_usr" runat="server" placeholder="letras/numeros" Enabled="false" BackColor="LightGray"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <asp:Label CssClass="control-label fuente_css02" ID="lbl_usr_coment" runat="server" Text="Comentarios"></asp:Label>

                                            <asp:TextBox CssClass="form-control input-box" ID="txt_usr_coment" runat="server" placeholder="letras/números" TextMode="MultiLine" Enabled="false" BackColor="LightGray" ForeColor="#104D8D" TabIndex="18"></asp:TextBox>
                                            <div class="text-right">
                                                <asp:RequiredFieldValidator ID="rfv_usr_coment" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_usr_coment" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="text-right">
                                                <asp:Button CssClass="btn btn02" ID="btn_guardar_usrs" runat="server" Text="GUARDAR" TabIndex="19" OnClick="btn_guardar_usrs_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="up_caja" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="up_correo" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="col-lg-10">
                            <div class="col-lg-12 ">
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="col-lg-10">
                            <div class="col-lg-12 ">
                            </div>
                        </div>
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