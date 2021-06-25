<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="formLogin.aspx.cs" Inherits="PremioExcelencia.Login.formLogin" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <script src="https://code.jquery.com/jquery-1.12.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10.16.5/dist/sweetalert2.all.min.js" type="text/javascript"></script>
    <link href="../Content/sweetalert.css" rel="stylesheet" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="../Styles/MyStyle.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" />
    <style>
        body {
            background: "/Interfaces/Images/imgUni2.png";
            font-size: 11px;
        }

        .switch {
            position: relative;
            display: inline-block;
            width: 50px;
            height: 20px;
        }

            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 12px;
                width: 12px;
                left: 4px;
                bottom: 4px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(26px);
            -ms-transform: translateX(26px);
            transform: translateX(26px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 34px;
        }

            .slider.round:before {
                border-radius: 50%;
            }
        /*Modal*/
        .modalContainer {
            display: none;
            position: fixed;
            z-index: 1;
            padding-top: 100px;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            overflow: auto;
            background-color: rgb(0,0,0);
            background-color: rgba(0,0,0,0.4);
        }

            .modalContainer .modal-content {
                background-color: #fefefe;
                margin: auto;
                padding: 20px;
                border: 1px solid lightgray;
                border-top: 10px solid #ffff;
                width: 30%;
            }

            .modalContainer .close {
                color: #aaaaaa;
                float: right;
                font-size: 28px;
                font-weight: bold;
            }

                .modalContainer .close:hover,
                .modalContainer .close:focus {
                    color: #000;
                    text-decoration: none;
                    cursor: pointer;
                }
        /*fin Modal*/
    </style>
</head>
<body>

    <form id="form1" runat="server">
        <!--header-->
        <nav class="navbar-head">
            <div style="float: left; width: 15%; height: 100%;" class="nomAplicacion">
                <a href="#">
                    <asp:Image src="/interfaces/images/logo-sek-2.png" alt="Dont exist image" runat="server" Style="width: 100%; height: 100%" />
                </a>
            </div>
            <p style="float: right">
                PREMIO A LA EXCELENCIA
                <span>
                  <asp:HyperLink ID="Modal" runat="server" Style="cursor: pointer"><i class="far fa-comments" style="color: white;border:1px solid white;margin:3px;padding:2px;border-radius:5px"></i></asp:HyperLink>
                </span>
            </p>

            <br />
            <br />
        </nav>
        <nav class="subnav">
            <div style="height: 50px">
            </div>
        </nav>
        <!--fin header-->
        <br />
        <br />
        <div>
            <div class="container" style="width: 30%; padding-top: 150px;">
                <body background="/Interfaces/Images/fondoPremio.jpg" bgcolor="FFCECB" >
                    <h4 class="well" style="text-align: center; background: rgba(0, 0, 0, 0.60); color: white">INICIO DE SESIÓN</h4>
                    <div class="col-lg-12 well" style="background: rgba(0, 0, 0, 0.60);">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group ">
                                    <label style="color: white">Usuario</label>
                                    <asp:TextBox type="text" placeholder="usuario" class="form-control" runat="server" ID="txtUser" ></asp:TextBox>
                                    <asp:Label ID="lbluser_v" Style="width: 100%; color: red; border: hidden" runat="server" Text=""></asp:Label>

                                </div>
                                <div class="form-group">
                                    <label style="color: white">Password</label>
                                    <asp:TextBox type="password" placeholder="password" class="form-control" runat="server" ID="txtPassword" ></asp:TextBox>
                                    <asp:Label ID="lblpassword_v" Style="width: 100%; color: red; border: hidden" runat="server" Text=""></asp:Label>

                                </div>

                                <center>
                                    <asp:Button type="button" class="btn btn-lg btn-success" runat="server" Text="Login" ID="btnGuardar" OnClick="btnGuardar_Click"></asp:Button>
                                    <br />
                                    <br />
                                    <div class="form-group">
                                        <a href="../InscripcionExterna/Inscripcion.aspx" style="text-decoration: underline; color: white">Registrate</a>
                                    </div>
                                    <div class="form-group">
                                        <a href="../Interfaces/PreguntasFrecuentes.aspx" style="text-decoration: underline; color: white">Preguntas Frecuentes</a>
                                    </div>
                                </center>

                            </div>
                        </div>
                    </div>
                </body>
            </div>
        </div>
        <!--Modal-->


        <div id="tvesModal" class="modalContainer">
            <div class="modal-content">
                <span class="close">×</span>
                <div class="row">
                    <div class="col-sm-12">
                        <asp:TextBox ID="txtContactName" runat="server" placeholder="Nombres" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-12">
                        <asp:TextBox ID="txtContactEmail" runat="server" placeholder="Email" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-12">
                        <asp:TextBox ID="txtContactMessage" runat="server" TextMode="MultiLine" placeholder="Mensaje" CssClass="form-control"></asp:TextBox>
                    </div>

                </div>
                <br />
                <div class="row">
                    <center>
                        <asp:Button ID="btnSendSuggestion" runat="server" Text="Enviar" class="btn btn-sm btn-success" OnClick="btnSendSuggestion_Click" />
                    </center>
                </div>
            </div>
        </div>


        <!--fin Modal-->
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <div class="footer-content" style="padding: 0.5%;">
            <footer>
                <p style="color: white"><span class="glyphicon glyphicon-copyright-mark"></span>2021 - Recursos Tecnológicos </p>
            </footer>
        </div>
    </form>
</body>

</html>
<script type="text/javascript">
    if (document.getElementById("Modal")) {
        var modal = document.getElementById("tvesModal");
        var btn = document.getElementById("Modal");
        var span = document.getElementsByClassName("close")[0];
        var body = document.getElementsByTagName("body")[0];

        btn.onclick = function () {
            modal.style.display = "block";

            body.style.position = "static";
            body.style.height = "100%";
            body.style.overflow = "hidden";
        }

        span.onclick = function () {
            modal.style.display = "none";

            body.style.position = "inherit";
            body.style.height = "auto";
            body.style.overflow = "visible";
        }

        window.onclick = function (event) {
            if (event.target == modal) {
                modal.style.display = "none";

                body.style.position = "inherit";
                body.style.height = "auto";
                body.style.overflow = "visible";
            }
        }
    }
    $("document").ready(function () {
        if ($('#<%=txtUser.ClientID%>').val().trim() != "") {
            $('#<%=lbluser_v.ClientID%>').hide();
        }
        if ($('#<%=txtPassword.ClientID%>').val().trim() != "") {
            $('#<%=lblpassword_v.ClientID%>').hide();
        }
    });
</script>