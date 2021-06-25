<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="formLogin.aspx.cs" Inherits="Proyecto_Ingles_V2.Login.formLogin" Async="true" %>

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
                REGISTRO INGLES AUTÓNOMO
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
                <body background="/Interfaces/Images/fondoIngles.jpg" bgcolor="FFCECB">
                    <h4 class="well" style="text-align: center; background: rgba(0, 0, 0, 0.60); color: white">INICIO DE SESIÓN</h4>
                    <div class="col-lg-12 well" style="background: rgba(0, 0, 0, 0.60);">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group ">
                                    <label style="color: white">Usuario</label>
                                    <asp:TextBox type="text" placeholder="usuario" class="form-control" runat="server" ID="txtUser" required="required"></asp:TextBox>

                                </div>
                                <div class="form-group">
                                    <label style="color: white">Password</label>
                                    <asp:TextBox type="password" placeholder="password" class="form-control" runat="server" ID="txtPassword" required="required"></asp:TextBox>
                                </div>

                                <center>
					    <asp:button type="button" class="btn btn-lg btn-success" runat="server" Text="Login" id="btnGuardar" OnClick="btnGuardar_Click" ></asp:button>
                        <br />
                        <br />
                        <div class="form-group">
                        <a href="../InscripcionExterna/InscripcionIngles.aspx" style="text-decoration:underline;color:white">Registrate</a>
                        </div>
                        <div class="form-group">
                        <a href="../Interfaces/PreguntasFrecuentes.aspx" style="text-decoration:underline;color:white">Preguntas Frecuentes</a>
                        </div>
                         </center>

                            </div>
                        </div>
                    </div>
                </body>
            </div>
        </div>
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
