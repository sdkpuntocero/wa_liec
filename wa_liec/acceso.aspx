﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="acceso.aspx.cs" Inherits="wa_liec.acceso" %>

<!DOCTYPE html>
<html>

<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.4.1/css/all.css" integrity="sha384-5sAR7xN1Nv6T6+dT2mhtzEpVJvfS3NScPQTrOxhwjIuvcA67KV2R5Jz6kr4abQsz" crossorigin="anonymous">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.7.1/css/bootstrap-datepicker.standalone.min.css">
    <link rel="stylesheet" href="acceso/now-ui-kit.css" type="text/css">
    <link rel="stylesheet" href="acceso/assets/css/nucleo-icons.css" type="text/css">
    <script src="acceso/assets/js/navbar-ontop.js"></script>
    <link rel="icon" href="assets/img/ico_liec.png">
    <title>/ Bienvenidos</title>
</head>

<body class="bg-info">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="py-5 bg-info" style="background-image: url(&quot;assets/img/bg11.jpg&quot;); background-size: cover; background-position: center top;">
            <div class="container">
                <div class="row my-4">
                    <div class="mx-auto col-md-6 col-10 col-xl-4 px-4 rounded">
                        <div class="card bg-info">
                            <div class="card-body text-center bg-secondary">
                                <div class="row mt-5">
                                    <div class="col-md-12">
                                        <h5 class="mb-4">
                                            <b>Acceso</b></h5>
                                        <img class="img-fluid d-block mx-auto img-thumbnail" src="acceso/assets/img/ico_liec.png" width="128">
                                    </div>
                                </div>
                                <div class="row mt-4 pt-2">
                                    <div class="col">
                                        <form>
                                            <div class="form-group mb-2">
                                                <div class="input-group border-0">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text" id="basic-addon1">
                                                           
                                                            <i class="fas fa-user-lock"></i>
                                                        </span>
                                                    </div>
                                                    <asp:TextBox CssClass="form-control" ID="txt_usuario" runat="server" TabIndex="1" placeholder="Usuario"></asp:TextBox>
                                                </div>
                                                <div class="text-right">
                                                    <asp:RequiredFieldValidator ID="rfv_usuario" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_usuario"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group mb-2">
                                                <div class="input-group border-0">
                                                    <div class="input-group-prepend ">
                                                        <span class="input-group-text" id="basic-addon1">
                                                            <i class="fas  fa-key"></i>
                                                        </span>
                                                    </div>
                                                    <asp:TextBox CssClass="form-control" ID="txt_clave" runat="server" TabIndex="2" placeholder="Contraseña" TextMode="Password"></asp:TextBox>
                                                </div>
                                                <div class="text-right">
                                                    <asp:RequiredFieldValidator ID="rfv_clave" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_clave"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <asp:Button CssClass="btn mt-4 mb-3 btn-light rounded btn-lg text-primary" ID="btn_acceso" runat="server" Text="Iniciar sesión" TabIndex="3" OnClick="btn_acceso_Click" />
                                        </form>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="py-3">
            <div class="container">
                <div class="row">
                    <div class="col-lg-4 col-6 p-3">
                        <h5><b>Dirección</b></h5>
                        <ul class="list-unstyled">
                            <li><a href="https://www.google.com/maps/place/LIEC,+S.A.+de+C.V./@19.2222902,-98.9933232,18z/data=!4m5!3m4!1s0x0:0x4730600a5d3c95a8!8m2!3d19.2221669!4d-98.9936292?hl=es-ES" class="text-white"><i class="fas fa-map-marker-alt"></i>Cda. Morelos 6, Tecaxtitla, 12100 San Antonio Tecómitl, CDMX</a> </li>
                        </ul>
                    </div>
                    <div class="col-lg-4 col-6 p-3 text-white">
                        <h5><b>Contacto</b></h5>
                        <ul class="list-unstyled">
                            <li class="text-white"><a href="mailto:contacto@liec.com.mx" class="text-white"><i class="far fa-envelope"></i>contacto@liec.com.mx</a> </li>
                            <li class="text-white"><a href="tel:015521614772" class="text-white"><i class="fas fa-phone"></i>01 (55) 21614772</a> </li>
                        </ul>
                    </div>
                    <div class="col-lg-3 col-md-4 p-3">
                        <h5>Síguenos</h5>
                        <div class="row">
                            <div class="col-md-12 d-flex align-items-center justify-content-between my-2 text-success">
                                <a href="#" title="Facebook"><i class="d-block fab fa-facebook-f fa-lg mr-2 text-white"></i></a>
                                <a href="#" title="Twitter"><i class="d-block fab fa-twitter fa-lg mx-2 text-white"></i></a>
                                <a href="#" title="Google"><i class="d-block fab fa-google-plus-g fa-lg mx-2 text-white"></i></a>
                                <a href="#" title="Youtube"><i class="d-block fab fa-youtube fa-lg mx-2 text-white"></i></a>
                                <a href="#" title="Linkedin"><i class="d-block fab fa-linkedin-in fa-lg mx-2 text-white"></i></a>
                                <a href="#" title="Github"><i class="d-block fab fa-github fa-lg ml-2 text-white"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 text-center">
                        <p class="mb-0 mt-2">© LIEC - 2018. Todos los derechos reservados</p>
                    </div>
                </div>
            </div>
        </div>
        <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
        <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js" integrity="sha384-ChfqqxuZUCnJSK3+MXmPNIyE6ZbWh2IMqE241rYiqJxyMiZ6OW/JmZQ5stwEULTy" crossorigin="anonymous"></script>
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