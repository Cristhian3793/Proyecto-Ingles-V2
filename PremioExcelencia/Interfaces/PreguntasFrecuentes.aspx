<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreguntasFrecuentes.aspx.cs" Inherits="PremioExcelencia.Interfaces.PreguntasFrecuentes" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <!-- JavaScript Bundle with Popper -->
    <!-- CSS only -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <link href="../Styles/MyStyle.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" />
    <style type="text/css">
        body {
            font-size: 10.5px;
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


        @import url(https://fonts.googleapis.com/css?family=Open+Sans:300,800);






        .content {
            width: 80%;
            padding: 20px;
            margin: 0 auto;
            padding: 0 60px 0 0;
        }

        .centerplease {
            margin: 0 auto;
            max-width: 270px;
            font-size: 20px;
        }

        .question {
            position: relative;
            background: #085394;
            margin: 0;
            padding: 10px 10px 10px 50px;
            display: block;
            width: 100%;
            cursor: pointer;
            color: white;
        }

        .answers {
            background: #EEEEEE;
            padding: 0px 15px;
            margin: 5px 0;
            height: 0;
            overflow: hidden;
            z-index: -1;
            position: relative;
            opacity: 0;
            -webkit-transition: .7s ease;
            -moz-transition: .7s ease;
            -o-transition: .7s ease;
            transition: .7s ease;
        }

        .questions:checked ~ .answers {
            height: auto;
            opacity: 1;
            padding: 15px;
        }

        .plus {
            position: absolute;
            margin-left: 10px;
            z-index: 5;
            font-size: 2em;
            line-height: 100%;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            -o-user-select: none;
            user-select: none;
            -webkit-transition: .3s ease;
            -moz-transition: .3s ease;
            -o-transition: .3s ease;
            transition: .3s ease;
        }

        .questions:checked ~ .plus {
            -webkit-transform: rotate(45deg);
            -moz-transform: rotate(45deg);
            -o-transform: rotate(45deg);
            transform: rotate(45deg);
        }

        .questions {
            display: none;
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
                <a href="../Login/formLogin.aspx">
                    <asp:Image src="../interfaces/images/logo-sek-2.png" alt="Dont exist image" runat="server" Style="width: 100%; height: 100%" />
                </a>
            </div>
            <p style="float: right">
                PREGUNTAS FRECUENTES
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
        <div class="footer-content" style="padding: 0.5%;">
            <footer>
                <p style="color: white; font-size: 14px"><span class="glyphicon glyphicon-copyright-mark"></span>2021 - Recursos Tecnológicos </p>
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
</script>
