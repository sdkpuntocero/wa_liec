<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pnl_cont.aspx.cs" Inherits="wa_liec.pnl_cont" %>

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
    <title>/ CONCRETO</title>
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
                                        <asp:Label CssClass="buttonClass" ID="lbl_usr_oper" runat="server" Text=""></asp:Label>&nbsp;<i class="fa fa-male" id="i_usr_oper" runat="server"></i>
                                    </asp:LinkButton>

                                    <br />

                                    <label class="control-label fuente_css02">PERFIL:</label>
                                    <asp:Label CssClass="buttonClass" ID="lbl_tusr" runat="server" Text=""></asp:Label>

                                    <br />

                                    <label class="control-label fuente_css02">EMPRESA:</label>
                                    <asp:LinkButton CssClass="buttonClass" ID="lkb_emp_oper" runat="server">
                                        <asp:Label CssClass="buttonClass" ID="lbl_emp_oper" runat="server" Text=""></asp:Label>&nbsp;<i class="fa fa-puzzle-piece" id="i_emp_oper" runat="server"></i>
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
                <asp:UpdatePanel ID="up_cont_menu" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="col-lg-2">
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
                                                    <asp:LinkButton CssClass="fuente_css02" ID="lkb_cont_rub" runat="server" OnClick="lkb_cont_rub_Click">
                                                        <i class="fas fa-boxes" id="i_cont_rub" runat="server"></i>
                                                        <asp:Label CssClass="buttonClass" ID="lbl_cont_rub" runat="server" Text="RUBROS"> </asp:Label>
                                                    </asp:LinkButton>
                                                </li>
                                                <li>
                                                    <asp:LinkButton CssClass="fuente_css02" ID="lkb_cont_gast" runat="server" OnClick="lkb_cont_gast_Click">
                                                        <i class="far fa-credit-card" id="i_cont_gast" runat="server"></i>
                                                        <asp:Label CssClass="buttonClass" ID="lbl_cont_gast" runat="server" Text="GASTOS"> </asp:Label>
                                                    </asp:LinkButton>
                                                </li>
                                                <li>
                                                    <asp:LinkButton CssClass="fuente_css02" ID="lkb_cont_caja" runat="server">
                                                        <i class="fab fa-stack-overflow" id="i_cont_caja" runat="server"></i>
                                                        <asp:Label CssClass="buttonClass" ID="lbl_cont_caja" runat="server" Text="CAJA"></asp:Label>
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
                <asp:UpdatePanel ID="up_rubro" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="col-lg-10">
                            <div class="col-lg-12 ">
                                <div class="row">
                                    <div class="panel panel-default" id="pnl_rubro" runat="server" visible="false">
                                        <div class="panel-heading">
                                            <p class="text-left">
                                                <div class="input-group" id="div_busc_rub" runat="server" visible="false">
                                                    <span class="input-group-addon">
                                                        <asp:Label CssClass="control-label fuente_css02" ID="lbl_buscar_rub" runat="server" Text="*Buscar Rubro:"></asp:Label>
                                                    </span>
                                                    <asp:TextBox CssClass="form-control input-box" ID="txt_buscar_rub" runat="server" placeholder="letras/numeros" TextMode="Search" TabIndex="3"></asp:TextBox>
                                                    <span class="input-group-btn">
                                                        <asp:Button CssClass="btn btn01" ID="btn_buscar_rub" runat="server" Text="ACEPTAR" OnClick="btn_buscar_rub_Click" TabIndex="4" />
                                                    </span>
                                                </div>
                                                <div class="text-right">
                                                    <ajaxToolkit:AutoCompleteExtender ID="ace_buscar_rub" runat="server" ServiceMethod="SearchCustomers" MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="true" CompletionSetCount="10" TargetControlID="txt_buscar_rub" FirstRowSelected="false"></ajaxToolkit:AutoCompleteExtender>
                                                    <asp:RequiredFieldValidator ID="rfv_buscar_rub" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_buscar_rub" ForeColor="white" Enabled="false"></asp:RequiredFieldValidator>
                                                </div>
                                            </p>
                                            <p class="text-right">
                                                REGISTRO DE RUBROS

                                        <span>
                                            <asp:LinkButton CssClass="btn btn02" ID="btn_agr_rubro" runat="server" ToolTip="AGREGAR RUBRO" TabIndex="1" OnClick="btn_agr_rubro_Click">
                                                <i class="fas fa-plus" id="i_agr_rubro" runat="server"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton CssClass="btn btn02" ID="btn_edit_rubro" runat="server" ToolTip="EDITAR RUBRO" TabIndex="2" OnClick="btn_edit_rubro_Click">
                                                <i class="far fa-edit" id="i_edit_rubro" runat="server"></i>
                                            </asp:LinkButton>
                                        </span>
                                                <br />
                                                <asp:CheckBox ID="chkb_des_rubro" runat="server" AutoPostBack="true" Text="Desactivar validaciones" OnCheckedChanged="chkb_des_rubro_CheckedChanged" />
                                            </p>
                                        </div>

                                        <div class="panel-body">
                                            <div class="row">

                                                <div class="col-lg-12">
                                                    <asp:GridView CssClass="table" ID="gv_rubro" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="10" OnPageIndexChanging="gv_rubro_PageIndexChanging" OnRowDataBound="gv_rubro_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chk_rubro" runat="server" onclick="CheckOne(this)" AutoPostBack="true" OnCheckedChanged="chk_rubro_CheckedChanged" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="codigo_rubro" HeaderText="ID" SortExpression="codigo_rubro" Visible="true" />
                                                            <asp:BoundField DataField="tipo_rubro" HeaderText="TIPO RUBRO" SortExpression="tipo_rubro" />
                                                            <asp:BoundField DataField="etiqueta_rubro" HeaderText="ETIQUETA" SortExpression="etiqueta_rubro" Visible="true" />
                                                            <asp:BoundField DataField="rubro" HeaderText="RUBRO" SortExpression="rubro" />
                                                            <asp:BoundField DataField="fecha_registro" HeaderText="FECHA DE REGISTRO" SortExpression="fecha_registro" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="false" />
                                                            <asp:TemplateField HeaderText="Estatus">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddl_rub_est" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_rub_est_SelectedIndexChanged">
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
                                                    <br />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-3">
                                                    <div class="form-group">
                                                        <label for="ddl_tipo_rubro" class="fuente_css02">*Tipo Rubro</label>
                                                        <asp:DropDownList CssClass="form-control input-box" ID="ddl_tipo_rubro" runat="server" TabIndex="4"></asp:DropDownList>
                                                        <div class="text-right">
                                                            <asp:RequiredFieldValidator ID="rfv_tipo_rubro" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="ddl_tipo_rubro" InitialValue="0" ForeColor="DarkRed " Enabled="false"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-2">
                                                    <div class="form-group">
                                                        <label for="eti_rub" class="fuente_css02">*Etiqueta Rubro</label>
                                                        <asp:TextBox CssClass="form-control input-box" ID="eti_rub" runat="server" placeholder="[a-z/0-9]" TabIndex="5"></asp:TextBox>
                                                        <div class="text-right">
                                                            <asp:RequiredFieldValidator ID="rfv_eti_rub" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="eti_rub" ForeColor="DarkRed " Enabled="false"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4">
                                                    <div class="form-group">
                                                        <label class="fuente_css02">*Descripción Rubro</label>
                                                        <asp:TextBox CssClass="form-control input-box" ID="desc_rubro" runat="server" TextMode="MultiLine" placeholder="[a-z/0-9]" TabIndex="6"></asp:TextBox>
                                                        <div class="text-right">
                                                            <asp:RequiredFieldValidator ID="rfv_desc_rubro" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="desc_rubro" ForeColor="DarkRed " Enabled="false"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3">
                                                    <div class="form-group text-left">
                                                        <label class="fuente_css02">*Monto fijo</label>
                                                        <div class="input-group">
                                                            <asp:TextBox CssClass="form-control input-box" ID="mont_rubro" runat="server" AutoPostBack="true" OnTextChanged="mont_rubro_TextChanged" placeholder="[0-9]" TabIndex="7"></asp:TextBox>
                                                            <div class="text-right">
                                                                <asp:RequiredFieldValidator ID="rfv_mont_rubro" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="mont_rubro" ForeColor="DarkRed " Enabled="false"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-lg-2">
                                                    <div class="form-group text-left">
                                                        <label class="fuente_css02">*Minimo: % 0</label>
                                                        <asp:TextBox CssClass="form-control input-box" ID="minimo_rubro" runat="server" TextMode="Number" placeholder="[0-9]" TabIndex="8"></asp:TextBox>
                                                        <div class="text-right">
                                                            <asp:RequiredFieldValidator ID="rfv_minimo_rubro" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="minimo_rubro" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-2">
                                                    <div class="form-group text-left">
                                                        <label class="fuente_css02">*Máximo: % 0</label>
                                                        <asp:TextBox CssClass="form-control input-box " ID="maximo_rubro" runat="server" TextMode="Number" placeholder="[0-9]" TabIndex="9"></asp:TextBox>
                                                        <div class="text-right">
                                                            <asp:RequiredFieldValidator ID="rfv_maximo_rubro" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="maximo_rubro" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-2">
                                                    <div class="form-group text-left">
                                                        <label class="fuente_css02">*Monto extra</label>
                                                        <div class="input-group">
                                                            <asp:TextBox CssClass="form-control input-box" ID="monto_extra" runat="server" AutoPostBack="true" OnTextChanged="mont_rubro_TextChanged" placeholder="[0-9]" Enabled="false" TabIndex="10"></asp:TextBox>
                                                            <div class="text-right">
                                                                <asp:RequiredFieldValidator ID="rfv_pextra_rubro" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="monto_extra" ForeColor="DarkRed" Enabled="false" placeholder="[0-9]"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-2">
                                                    <div class="form-group text-left">
                                                        <label class="fuente_css02">*Monto gastado</label>
                                                        <div class="input-group">
                                                            <asp:TextBox CssClass="form-control input-box" ID="monto_gastado" runat="server" AutoPostBack="true" OnTextChanged="mont_rubro_TextChanged" placeholder="[0-9]" Enabled="false" TabIndex="11"></asp:TextBox>
                                                            <div class="text-right">
                                                                <asp:RequiredFieldValidator ID="rfv_monto_gastado" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="monto_gastado" ForeColor="DarkRed" Enabled="false" placeholder="[0-9]"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-2 text-right">
                                                    <div class="form-group text-right">

                                                        <br />
                                                        <asp:Button CssClass="btn btn02" ID="btn_guardar_rubro" runat="server" Text="GUARDAR" OnClick="btn_guardar_rubro_Click" TabIndex="12" />
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
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="up_gasto" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="col-lg-10">
                            <div class="col-lg-12 ">
                                <div class="row">
                                    <div class="panel panel-default" id="pnl_gasto" runat="server" visible="false">
                                        <div class="panel-heading">
                                            <p class="text-left">
                                                <div class="input-group" id="div_busc_gasto" runat="server" visible="false">
                                                    <span class="input-group-addon">
                                                        <asp:Label CssClass="control-label fuente_css02" ID="lbl_buscar_gasto" runat="server" Text="*Buscar Rubro:"></asp:Label>
                                                    </span>
                                                    <asp:TextBox CssClass="form-control input-box" ID="txt_buscar_gasto" runat="server" placeholder="letras/numeros" TextMode="Search" TabIndex="3"></asp:TextBox>
                                                    <span class="input-group-btn">
                                                        <asp:Button CssClass="btn btn01" ID="btn_buscar_gasto" runat="server" Text="ACEPTAR" OnClick="btn_buscar_gasto_Click" TabIndex="4" />
                                                    </span>
                                                </div>
                                                <div class="text-right">
                                                    <ajaxToolkit:AutoCompleteExtender ID="ace_buscar_gasto" runat="server" ServiceMethod="SearchCustomers" MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="true" CompletionSetCount="10" TargetControlID="txt_buscar_gasto" FirstRowSelected="false"></ajaxToolkit:AutoCompleteExtender>
                                                    <asp:RequiredFieldValidator ID="rfv_buscar_gasto" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_buscar_gasto" ForeColor="white" Enabled="false"></asp:RequiredFieldValidator>
                                                </div>
                                            </p>
                                            <p class="text-right">
                                                REGISTRO DE RUBROS

                                        <span>
                                            <asp:LinkButton CssClass="btn btn02" ID="btn_agr_gasto" runat="server" ToolTip="AGREGAR RUBRO" TabIndex="1" OnClick="btn_agr_gasto_Click">
                                                <i class="fas fa-plus" id="i_agr_gasto" runat="server"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton CssClass="btn btn02" ID="btn_edit_gasto" runat="server" ToolTip="EDITAR RUBRO" TabIndex="2" OnClick="btn_edit_gasto_Click">
                                                <i class="far fa-edit" id="i_edit_gasto" runat="server"></i>
                                            </asp:LinkButton>
                                        </span>
                                                <br />
                                                <asp:CheckBox ID="chkb_des_gastoro" runat="server" AutoPostBack="true" Text="Desactivar validaciones" OnCheckedChanged="chkb_des_gasto_CheckedChanged" />
                                            </p>
                                        </div>

                                        <div class="panel-body">
                                            <div class="row">

                                                <div class="col-lg-12">
                                                    <asp:GridView CssClass="table" ID="gv_gasto" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="10" OnPageIndexChanging="gv_gasto_PageIndexChanging" OnRowDataBound="gv_gasto_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chk_gasto" runat="server" onclick="CheckOne(this)" AutoPostBack="true" OnCheckedChanged="chk_gasto_CheckedChanged" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="codigo_caja" HeaderText="ID" SortExpression="codigo_caja" Visible="true" />
                                                            <asp:BoundField DataField="tipo_rubro" HeaderText="TIPO RUBRO" SortExpression="tipo_rubro" />
                                                            <asp:BoundField DataField="rubro" HeaderText="RUBRO" SortExpression="rubro" />
                                                            <asp:BoundField DataField="desc_caja" HeaderText="DESCRIPCIÓN CAJA" SortExpression="desc_caja" />
                                                            <asp:BoundField DataField="fecha_registro" HeaderText="FECHA DE REGISTRO" SortExpression="fecha_registro" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="false" />
                                                            <asp:TemplateField HeaderText="Estatus">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddl_gasto_est" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_gasto_est_SelectedIndexChanged">
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
                                                    <br />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-2">
                                                    <div class="form-group">
                                                        <label for="ddl_tipo_gastoro" class="fuente_css02">*Tipo Rubro</label>
                                                        <asp:DropDownList CssClass="form-control input-box" ID="ddl_tipo_gasto" runat="server" TabIndex="4" OnSelectedIndexChanged="ddl_tipo_gasto_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        <div class="text-right">
                                                            <asp:RequiredFieldValidator ID="rfv_tipo_gasto" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="ddl_tipo_gasto" InitialValue="0" ForeColor="DarkRed " Enabled="false" ></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-2">
                                                    <div class="form-group">
                                                        <label for="eti_gasto" class="fuente_css02">*Etiqueta Rubro</label>
                                                        <asp:DropDownList CssClass="form-control input-box" ID="ddl_eti_gasto" runat="server" TabIndex="5" ></asp:DropDownList>
                                                        <div class="text-right">
                                                            <asp:RequiredFieldValidator ID="rfv_eti_gasto" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="ddl_eti_gasto" InitialValue="0" ForeColor="DarkRed " Enabled="false"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-5">
                                                    <div class="form-group">
                                                        <label class="fuente_css02">*Descripción Gasto</label>
                                                        <asp:TextBox CssClass="form-control input-box" ID="desc_gasto" runat="server" TextMode="MultiLine" placeholder="[a-z/0-9]" TabIndex="6"></asp:TextBox>
                                                        <div class="text-right">
                                                            <asp:RequiredFieldValidator ID="rfv_desc_gasto" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="desc_gasto" ForeColor="DarkRed " Enabled="false"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3">
                                                    <div class="form-group text-left">
                                                        <label class="fuente_css02">*Monto fijo</label>
                                                        <div class="form-group">
                                                            <asp:TextBox CssClass="form-control input-box" ID="mont_gasto" runat="server" AutoPostBack="true" OnTextChanged="mont_gasto_TextChanged" placeholder="[0-9]" TabIndex="7"></asp:TextBox>
                                                            <div class="text-right">
                                                                <asp:RequiredFieldValidator ID="rfv_mont_gasto" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="mont_gasto" ForeColor="DarkRed " Enabled="false"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                         <div class="form-group text-right">
                                                   
                                                            <asp:Button CssClass="btn btn02" ID="btn_guardar_gasto" runat="server" Text="GUARDAR" OnClick="btn_guardar_gasto_Click" TabIndex="12" />
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
