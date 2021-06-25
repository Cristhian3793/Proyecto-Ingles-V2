<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inscripcion.aspx.cs" Inherits="PremioExcelencia.InscripcionExterna.Inscripcion" Async="true" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Inscripcion Premio a la Excelencia</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />

    <script src="https://code.jquery.com/jquery-1.12.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10.16.5/dist/sweetalert2.all.min.js" type="text/javascript"></script>
    <link href="../Content/sweetalert.css" rel="stylesheet" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="../Styles/MyStyle.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" />

    <style>
        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('../Interfaces/images/loader.gif') 50% 50% no-repeat rgb(249,249,249);
            opacity: .8;
        }
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
    <form runat="server">
        <div class="loader"></div>
        <!--header-->
        <nav class="navbar-head">
            <div style="float: left; width: 15%; height: 100%;" class="nomAplicacion">
                <a href="../Login/formLogin.aspx">
                    <asp:Image src="../interfaces/images/logo-sek-2.png" alt="Dont exist image" runat="server" Style="width: 100%; height: 100%" />
                </a>
            </div>
            <p style="float: right">
                PREMIO A LA EXCELENCIA
                 <span >
                    <asp:HyperLink ID="Modal" runat="server" style="cursor:pointer"><i class="far fa-comments" style="color: white;border:1px solid white;margin:3px;padding:2px;border-radius:5px"></i></asp:HyperLink>
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
            <div class="container">
                <h1 class="well" style="text-align: center">INSCRIPCIÓN</h1>
                <div class="col-lg-12 well">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" Text="Período de Inscripción" Style="font-weight: bold"></asp:Label>
                                <asp:DropDownList ID="cbxPeriodoLectivo" runat="server" AutoPostBack="true" class="form-control">
                                </asp:DropDownList>
                            </div>


                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" Text="Estudiante UISEK:" Style="font-weight: bold"></asp:Label>
                                <label class="switch">
                                    <input type="checkbox" runat="server" id="RabTipoEstudiante" onclick="this.checked=!this.checked;" />
                                    <span class="slider round"></span>
                                </label>
                            </div>

                            <div class="form-group">
                                <label>Tipo Documento :</label>
                                <asp:RadioButton runat="server" name="rb" GroupName="RdgDocuemnto" Text="CÉDULA" ID="RabCedula" Checked="true" OnCheckedChanged="RabCedula_CheckedChanged" AutoPostBack="true"></asp:RadioButton>
                                <asp:RadioButton runat="server" name="rb" GroupName="RdgDocuemnto" Text="PASAPORTE " ID="RabPasaporte" AutoPostBack="true" OnCheckedChanged="RabPasaporte_CheckedChanged"></asp:RadioButton>
                            </div>
                            <div class="row">
                                <div class="form-group  col-sm-6">
                                    <label>Identificación</label>


                                    <asp:TextBox type="text" placeholder="Ingrese su número de identificación" class="form-control" runat="server" ID="txtCed"></asp:TextBox>

                                    <asp:Label ID="lblCedula" Style="width: 100%; color: red; border: hidden" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblCedula_v" Style="width: 100%; color: red; border: hidden" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label>Correo Electrónico</label>
                                    <asp:TextBox type="text" placeholder="Ingrese su correo electrónico" class="form-control" runat="server" ID="txtEmail"></asp:TextBox>
                                    <asp:Label ID="lblemail_v" Style="width: 100%; color: red; border: hidden" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6 form-group">
                                    <label>Nombres</label>
                                    <asp:TextBox placeholder="Ingrese sus Nombres" class="form-control" runat="server" ID="txtNombres"></asp:TextBox>
                                    <asp:Label ID="lblnombres_v" Style="width: 100%; color: red; border: hidden" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="col-sm-6 form-group">
                                    <label>Apellidos</label>
                                    <asp:TextBox type="text" placeholder="Ingrese sus Apellidos" class="form-control" runat="server" ID="txtApellidos"></asp:TextBox>
                                    <asp:Label ID="lblApellidos_v" Style="width: 100%; color: red; border: hidden" runat="server" Text=""></asp:Label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-6 form-group">
                                    <label>Celular</label>
                                    <asp:TextBox type="text" placeholder="Ingrese su celular" class="form-control" runat="server" ID="txtCelular" MaxLength="10" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                    <asp:Label ID="lblCelular_v" Style="width: 100%; color: red; border: hidden" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="col-sm-6 form-group">
                                    <label>Teléfono Convencional</label>
                                    <asp:TextBox type="text" placeholder="Ingrese su teléfono Convencional" class="form-control" runat="server" ID="txtTelefono" MaxLength="9" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                    <asp:Label ID="lblTelefono_v" Style="width: 100%; color: red; border: hidden" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-sm-6">
                                    <label>Dirección Domiciliaria</label>
                                    <asp:TextBox placeholder="Ingrese su Dirección" class="form-control" runat="server" ID="txtDireccion" TextMode="MultiLine"></asp:TextBox>
                                    <asp:Label ID="lblDireccion_v" Style="width: 100%; color: red; border: hidden" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label>Solicitar Información</label>
                                    <asp:TextBox type="text" placeholder="Ingrese sus preguntas aqui" class="form-control" runat="server" ID="txtInformacion" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4 form-group">
                                    <label>Rendir Exámen de Ubicación</label>
                                    <label class="switch">
                                        <input type="checkbox" runat="server" id="RabExamen" />
                                        <span class="slider round"></span>
                                    </label>
                                </div>
                            </div>
                            <asp:HiddenField ID="correcto" runat="server" />
                            <asp:Button type="button" class="btn btn-lg btn-success" runat="server" Text="Enviar" ID="btnGuardar" OnClick="btnGuardarInscrito_Click"></asp:Button>
                        </div>
                    </div>
                </div>
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
                <p style="color: white"><span class="glyphicon glyphicon-copyright-mark"></span>2021 - Dirección de Tecnológia </p>
            </footer>
        </div>
    </form>

</body>

</html>

<script type="text/javascript">
    /*validaciones*/
    /*fin Validaciones*/
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






    $(window).load(function () {
        $(".loader").fadeOut("slow");
    });


    var tipodoc = -1;
    var correcto = -1;

    function confirm() {

        Swal.fire({
            icon: 'success',
            title: 'OK',
            text: 'Registro Correcto Revise su correo!',
            footer: '<a href></a>'
        })
    }
    function rechazado() {

        Swal.fire({
            icon: 'error',
            title: 'error',
            text: 'Registro No se pudo Guardar',
            footer: '<a href></a>'
        })
    }
    $("document").ready(function () {
        if ($('#<%=txtCed.ClientID%>').val().trim() != "") {
            $('#<%=lblCedula_v.ClientID%>').hide();
        }
        if ($('#<%=txtEmail.ClientID%>').val().trim() != "") {
            $('#<%=lblemail_v.ClientID%>').hide();
        }
        if ($('#<%=txtCelular.ClientID%>').val().trim() != "") {
            $('#<%=lblCelular_v.ClientID%>').hide();
        }
        if ($('#<%=txtTelefono.ClientID%>').val().trim() != "") {
            $('#<%=lblTelefono_v.ClientID%>').hide();
        }
        if ($('#<%=txtDireccion.ClientID%>').val().trim() != "") {
            $('#<%=lblDireccion_v.ClientID%>').hide();
        }
        if ($('#<%=txtNombres.ClientID%>').val().trim() != "") {
            $('#<%=lblnombres_v.ClientID%>').hide();
        }
        if ($('#<%=txtApellidos.ClientID%>').val().trim() != "") {
            $('#<%=lblApellidos_v.ClientID%>').hide();
        }




        /*si es cedula*/
        if ($("#<%=RabCedula.ClientID%>").is(':checked')) {
            tipodoc = 1;
        }
        /*si es pasaporte*/
        else if ($("#<%=RabPasaporte.ClientID%>").is(':checked')) {
            tipodoc = 2;
        } else {
            $('#<%=RabCedula.ClientID%>').prop('checked', true);
            tipodoc = 1;
        }



    });
    $("#txtCed").blur(function () {
        /**
           * Algoritmo para validar cedulas de Ecuador
           * @Pasos  del algoritmo
           * 1.- Se debe validar que tenga 10 numeros
           * 2.- Se extrae los dos primero digitos de la izquierda y compruebo que existan las regiones
           * 3.- Extraigo el ultimo digito de la cedula
           * 4.- Extraigo Todos los pares y los sumo
           * 5.- Extraigo Los impares los multiplico x 2 si el numero resultante es mayor a 9 le restamos 9 al resultante
           * 6.- Extraigo el primer Digito de la suma (sumaPares + sumaImpares)
           * 7.- Conseguimos la decena inmediata del digito extraido del paso 6 (digito + 1) * 10
           * 8.- restamos la decena inmediata - suma / si la suma nos resulta 10, el decimo digito es cero
           * 9.- Paso 9 Comparamos el digito resultante con el ultimo digito de la cedula si son iguales todo OK sino existe error.     
        */
        if (tipodoc == 1) {
            var cedula = $("#txtCed").val();
            //Preguntamos si la cedula consta de 10 digitos
            if (cedula.length == 10) {

                //Obtenemos el digito de la region que sonlos dos primeros digitos
                var digito_region = cedula.substring(0, 2);

                //Pregunto si la region existe ecuador se divide en 24 regiones
                if (digito_region >= 1 && digito_region <= 24) {

                    // Extraigo el ultimo digito
                    var ultimo_digito = cedula.substring(9, 10);

                    //Agrupo todos los pares y los sumo
                    var pares = parseInt(cedula.substring(1, 2)) + parseInt(cedula.substring(3, 4)) + parseInt(cedula.substring(5, 6)) + parseInt(cedula.substring(7, 8));

                    //Agrupo los impares, los multiplico por un factor de 2, si la resultante es > que 9 le restamos el 9 a la resultante
                    var numero1 = cedula.substring(0, 1);
                    var numero1 = (numero1 * 2);
                    if (numero1 > 9) { var numero1 = (numero1 - 9); }

                    var numero3 = cedula.substring(2, 3);
                    var numero3 = (numero3 * 2);
                    if (numero3 > 9) { var numero3 = (numero3 - 9); }

                    var numero5 = cedula.substring(4, 5);
                    var numero5 = (numero5 * 2);
                    if (numero5 > 9) { var numero5 = (numero5 - 9); }

                    var numero7 = cedula.substring(6, 7);
                    var numero7 = (numero7 * 2);
                    if (numero7 > 9) { var numero7 = (numero7 - 9); }

                    var numero9 = cedula.substring(8, 9);
                    var numero9 = (numero9 * 2);
                    if (numero9 > 9) { var numero9 = (numero9 - 9); }

                    var impares = numero1 + numero3 + numero5 + numero7 + numero9;

                    //Suma total
                    var suma_total = (pares + impares);

                    //extraemos el primero digito
                    var primer_digito_suma = String(suma_total).substring(0, 1);

                    //Obtenemos la decena inmediata
                    var decena = (parseInt(primer_digito_suma) + 1) * 10;

                    //Obtenemos la resta de la decena inmediata - la suma_total esto nos da el digito validador
                    var digito_validador = decena - suma_total;

                    //Si el digito validador es = a 10 toma el valor de 0
                    if (digito_validador == 10)
                        var digito_validador = 0;

                    //Validamos que el digito validador sea igual al de la cedula
                    if (digito_validador == ultimo_digito) {
                        $("#lblCedula").hide();
                        correcto = 1;
                        $("#<%=correcto.ClientID%>").val(correcto);
                    } else {
                        $("#<%=lblCedula.ClientID%>").text("");
                        $("#<%=lblCedula.ClientID%>").text("La cédula es incorrecta");
                        $("#<%=lblCedula.ClientID%>").show();
                        correcto = 0;
                        $("#<%=correcto.ClientID%>").val(correcto);
                    }

                } else {
                    // imprimimos en consola si la region no pertenece
                    $("#<%=lblCedula.ClientID%>").text("");
                    $("#<%=lblCedula.ClientID%>").text("Esta cedula no pertenece a ninguna region");
                    $("#<%=lblCedula.ClientID%>").show();
                    correcto = 0;
                    $("#<%=correcto.ClientID%>").val(correcto);

                }
            } else {
                //imprimimos en consola si la cedula tiene mas o menos de 10 digitos
                $("#<%=lblCedula.ClientID%>").text("");
                $("#<%=lblCedula.ClientID%>").text("Esta cedula tiene menos de 10 Digitos");
                $("#<%=lblCedula.ClientID%>").show();
                correcto = 0;
                $("#<%=correcto.ClientID%>").val(correcto);
            }
        } else {
            correcto = -1;
            $("#<%=correcto.ClientID%>").val(correcto);
        }
    });


    function solonumeros(e) {

        var key;

        if (window.event) // IE
        {
            key = e.keyCode;
        }
        else if (e.which) // Netscape/Firefox/Opera
        {
            key = e.which;
        }

        if (key < 48 || key > 57) {
            return false;
        }

        return true;
    }

<%--     function traerDatosAlumno() {
        var cedula = $("#<%=txtCed.ClientID%>").val();


        if (cedula != "") {
            $.ajax({
                type: "POST",
                url: '../ServicioEstudiante.asmx/BuscaEstudiante',
                contentType: "application/json; charset=utf-8",
                headers: {
                    'X-CSRF-TOKEN': "{{ csrf_token() }}",
                },
                data: JSON.stringify({ cedula: cedula }),
                dataType: "json",
                beforeSend: function () {
                    $(".loader").fadeIn("slow");
                },
                success: function (response, status, data) {

                    var resp = response.d;
                    $(".loader").fadeOut("slow");
                    if (resp != "" && !resp.includes("No Existe")) {


                        $('#<%=RabTipoEstudiante.ClientID%>').prop('checked', true);
                        $('#<%=txtNombres.ClientID%>').val(resp[0]);
                        $('#<%=txtApellidos.ClientID%>').val(resp[1]);
                        $('#<%=txtDireccion.ClientID%>').val(resp[2]);
                        $('#<%=txtCelular.ClientID%>').val(resp[3]);
                        $('#<%=txtTelefono.ClientID%>').val(resp[4]);
                        $('#<%=txtEmail.ClientID%>').val(resp[5]);
                    }
                    else {

                        $('#<%=RabTipoEstudiante.ClientID%>').prop('checked', false);
                        $('#<%=txtNombres.ClientID%>').val("");
                        $('#<%=txtApellidos.ClientID%>').val("");
                        $('#<%=txtDireccion.ClientID%>').val("");
                        $('#<%=txtCelular.ClientID%>').val("");
                        $('#<%=txtTelefono.ClientID%>').val("");
                        $('#<%=txtEmail.ClientID%>').val("");

                    }
                }, statusCode: {

                    404: function (content) { alert('cannot find resource'); },
                    500: function (content) { alert('internal server error'); }
                },
                error: function (xhr, status, error) {
                    $(".loader").fadeOut("slow");
                    alert("Error ");
                }
            });
        }
    }--%>






</script>


