<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="acceso.aspx.cs" Inherits="wa_liec.acceso" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
	<title> / Acceso</title>


	<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="acceso/vendor/bootstrap/css/bootstrap.min.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="acceso/fonts/font-awesome-4.7.0/css/font-awesome.min.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="acceso/fonts/Linearicons-Free-v1.0.0/icon-font.min.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="acceso/vendor/animate/animate.css">
<!--===============================================================================================-->	
	<link rel="stylesheet" type="text/css" href="acceso/vendor/css-hamburgers/hamburgers.min.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="acceso/vendor/animsition/css/animsition.min.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="acceso/vendor/select2/select2.min.css">
<!--===============================================================================================-->	
	<link rel="stylesheet" type="text/css" href="acceso/vendor/daterangepicker/daterangepicker.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="acceso/css/util.css">
	<link rel="stylesheet" type="text/css" href="acceso/css/main.css">
<!--===============================================================================================-->
	<!--===============================================================================================-->
	<script src="acceso/vendor/jquery/jquery-3.2.1.min.js"></script>
<!--===============================================================================================-->
	<script src="acceso/vendor/animsition/js/animsition.min.js"></script>
<!--===============================================================================================-->
	<script src="acceso/vendor/bootstrap/js/popper.js"></script>
	<script src="acceso/vendor/bootstrap/js/bootstrap.min.js"></script>
<!--===============================================================================================-->
	<script src="acceso/vendor/select2/select2.min.js"></script>
<!--===============================================================================================-->
	<script src="acceso/vendor/daterangepicker/moment.min.js"></script>
	<script src="acceso/vendor/daterangepicker/daterangepicker.js"></script>
<!--===============================================================================================-->
	<script src="acceso/vendor/countdowntime/countdowntime.js"></script>
<!--===============================================================================================-->
	<script src="acceso/js/main.js"></script>
</head>
<body>
	<form id="form1" runat="server">
			<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
	<div class="limiter">
		<div class="container-login100">
			<div class="wrap-login100 p-l-85 p-r-85 p-t-55 p-b-55">
				<span class="login100-form-title p-b-32">
					<asp:Image CssClass="center-block img-responsive img-thumbnail" ID="Image2" runat="server" ImageUrl="~/img/ico_liec.png" Width="64" Height="64" />
					/ Acceso
				</span>
				<div>
					<asp:Label CssClass="control-label" ID="lbl_usuario" runat="server" Text="*Usuario"></asp:Label>
					<br />
					<asp:TextBox CssClass="form-control" ID="txt_usuario" runat="server" TabIndex="1" placeholder="Capturar usuario"></asp:TextBox>
					<div class="text-right">
						<asp:RequiredFieldValidator ID="rfv_usuario" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_usuario" ForeColor="#e24c0e"></asp:RequiredFieldValidator>
					</div>
				</div>
				<div>
					<asp:Label CssClass="control-label" ID="lbl_clave" runat="server" Text="*Contraseña"></asp:Label>
					<br />
					<asp:TextBox CssClass="form-control" ID="txt_clave" runat="server" TabIndex="2" placeholder="Capturar contraseña" TextMode="Password"></asp:TextBox>
					<div class="text-right">
						<asp:RequiredFieldValidator ID="rfv_clave" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_clave" ForeColor="#e24c0e"></asp:RequiredFieldValidator>
					</div>
					<br />
				</div>
				<div class="text-right">
					<asp:Button CssClass="btn btn-secondary" ID="btn_acceso" runat="server" Text="Iniciar sesión" TabIndex="3" OnClick="btn_acceso_Click" />
				</div>
			</div>
		</div>
	</div>
	<div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
		<div class="modal-dialog">
			<asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
				<ContentTemplate>
					<div class="modal-content">
						<div class="modal-header">
							<button type="button" class="close login100-form-title" data-dismiss="modal" aria-hidden="true">x</button>
							<h4 class="modal-title">
								<asp:Label ID="lblModalTitle" CssClass="login100-form-title" runat="server" Text=""></asp:Label>
							</h4>
						</div>
						<div class="modal-body">
							<asp:Label ID="lblModalBody" CssClass="login100-form-title" runat="server" Text=""></asp:Label>
						</div>
						<div class="modal-footer">
							<button class="btn btn-secondary" data-dismiss="modal" aria-hidden="true">Ok </button>
						</div>
					</div>
				</ContentTemplate>
			</asp:UpdatePanel>
		</div>
	</div>
	</form>
</body>
</html>
