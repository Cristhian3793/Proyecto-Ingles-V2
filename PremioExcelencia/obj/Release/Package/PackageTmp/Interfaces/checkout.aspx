<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Site.Master" AutoEventWireup="true" CodeBehind="checkout.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.FORMPRUEBAMAESTRO" async="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <script type="text/javascript">
            const doblepago = () => {

                var r = confirm("Considere que mantiene una o más transacciones anteriores autorizada dentro de los últimos 7 días; con productos de éste carrito de compras. Desea continuar con este pago?");
                if (r == false) {
                    window.location.href = 'https://pagosonline.uisek.edu.ec';
                }
            };
            const doblepago_2 = (x) => {

                var r = confirm("Se registran " + x + " transacciones autorizadas dentro de los últimos 7 días. Desea continuar con este pago?");
                if (r == false) {
                    window.location.href = 'https://pagosonline.uisek.edu.ec';
                }
            };

            function AcceptTermsCheckBoxValidation(oSrouce, args) {

                var checkbox = document.getElementById('chkTerminos');
                var validador = checkbox.checked;

                if (validador === true) {
                    $("#ddlTipo").removeAttr("disabled");
                } else {
                    alert("Es necesario aceptar los términos y condiciones.");
                    return false;
                }

            }
            function bloquear() {
                if (typeof (Page_ClientValidate) == 'function') {

                    Page_ClientValidate();

                }


                //$('#chkTerminos').prop('checked', true);
                var checkbox = document.getElementById('chkTerminos');
                var validador = checkbox.checked;
                if (validador === true) {
                    //alert("entro1")
                    if (Page_IsValid) {
                        if ($("#ddl_Tarjeta")[0].selectedIndex == 0 || $("#ddl_Banco")[0].selectedIndex == 0 || $("#ddl_TipoCredito")[0].selectedIndex == 0 || $(":radio[name='rbl_meses']").index($(":radio[name='rbl_meses']:checked")) < 0) {
                            Page_ClientValidate("pagoTarjeta");
                            alert("Debe seleccionar una tarjeta, banco emisor y tipo de diferido");
                            $("#chkTerminos").prop('checked', false);
                            return false;
                        }

                        else {
                            $("#txtCedula").attr("readonly", "readonly");
                            $("#txtDireccion").attr("readonly", "readonly");
                            $("#txtEmail").attr("readonly", "readonly");
                            $("#txtNombre").attr("readonly", "readonly");
                            $("#txtTelefono").attr("readonly", "readonly");
                            $("#ddlTipo").attr("disabled", "disabled");
                            BloquearTarjeta();
                            checkbox.prop('checked', true);
                        }
                    }
                    else {
                        $('#chkTerminos').prop('checked', false);
                    }
                } else {
                    $("#txtCedula").removeAttr("readonly");
                    $("#txtDireccion").removeAttr("readonly");
                    $("#txtEmail").removeAttr("readonly");
                    $("#txtNombre").removeAttr("readonly");
                    $("#txtTelefono").removeAttr("readonly");
                    $("#ddlTipo").removeAttr("disabled");
                    DesbloquearTarjeta();
                }

            }
            const BloquearTarjeta = () => { $("#ddl_Tarjeta").prop("disabled", true); $("#ddl_Banco").prop("disabled", true); $("#ddl_TipoCredito").prop("disabled", true); };
            const DesbloquearTarjeta = () => { $("#ddl_Tarjeta").prop("disabled", false); $("#ddl_Banco").prop("disabled", false); $("#ddl_TipoCredito").prop("disabled", false); };

            //función que valida número permite utilizar numpad y evita el uso de shift
            function valid(e) {
                tecla = (document.all) ? e.keyCode : e.which;
                console.log(tecla);

                if (tecla == 8) return true;
                if (tecla == 44 || tecla == 46 || tecla == 27) {
                    return true;
                }
                if ((tecla >= 48 && e.keyCode <= 57) && !event.shiftKey) {
                    patron = /^\d*$/;
                    te = String.fromCharCode(tecla);
                    return patron.test(te);
                } else if (e.keyCode >= 96 && e.keyCode <= 105) {
                    return true;
                }
                else
                    return false;
            }
            function ValidarCed(source, arguments) {

                var txt = document.getElementById(source.controltovalidate);
                var digitos = txt.value.length;
                var suma = 0;
                var mul = 0;
                var dv = 0;
                if ($("#ddlTipo").val() == 0) {
                    if (digitos == 13) {
                        var flag = false;
                        var flag2 = false;
                        var flag3 = false;
                        for (i = 0; i < 9; i++) {
                            if (i % 2 == 0) {
                                mul = parseInt(txt.value.charAt(i)) + parseInt(txt.value.charAt(i));

                                if (mul <= 9)
                                    suma = suma + mul;
                                else
                                    suma = suma + (mul - 9);
                            }
                            else {
                                suma = suma + parseInt(txt.value.charAt(i));
                            }
                        }

                        dv = suma % 10;
                        if (dv != 0) {

                            dv = 10 - dv;

                        }
                        else {
                            dv = 0;
                        }
                        if (dv == txt.value.charAt(9) && txt.value.charAt(10) == "0" && txt.value.charAt(11) == "0" && txt.value.charAt(12) == "1") {
                            flag = true;
                        }
                        else
                            flag = false;
                        var modulo = 11;
                        var tercerDigito = 6;
                        var coeficientes = [3, 2, 7, 6, 5, 4, 3, 2];
                        var numeroProvincia = parseInt(txt.value.charAt(0) + txt.value.charAt(1));
                        var sociedadPublica = parseInt(txt.value.charAt(2));
                        var establecimiento = "0001"
                        var total = 0
                        if ((numeroProvincia > 0 && numeroProvincia < 25) && sociedadPublica == tercerDigito && txt.value.substring(9) == establecimiento) {
                            var digitoVerificadorRecibido = parseInt(txt.value.charAt(8));
                            for (var j = 0; j < coeficientes.length; j++)
                                total = total + coeficientes[j] * parseInt(txt.value.charAt(j));
                            var digitoVerificadorObtenido = (total % modulo) == 0 ? 0 : modulo - (total % modulo);
                            if (digitoVerificadorRecibido == digitoVerificadorObtenido)
                                flag2 = true;
                            else
                                flag2 = false;
                        }
                        else
                            flag2 = false
                        var tercerDigitoEsp = 9;
                        var totalEsp = 0;
                        var establecimientoEsp = "001";
                        var coeficientesEsp = [4, 3, 2, 7, 6, 5, 4, 3, 2];
                        var numeroProvinciaEsp = parseInt(txt.value.charAt(0) + txt.value.charAt(1));
                        var sociedadPublicaEsp = parseInt(txt.value.charAt(2));
                        if ((numeroProvinciaEsp > 0 && numeroProvinciaEsp < 25) && sociedadPublicaEsp == tercerDigitoEsp && txt.value.substring(10) == establecimientoEsp) {

                            var digitoVerificadorRecibidoEsp = parseInt(txt.value.charAt(9));

                            for (var j = 0; j < coeficientesEsp.length; j++)
                                totalEsp = totalEsp + coeficientesEsp[j] * parseInt(txt.value.charAt(j));

                            var digitoVerificadorObtenidoEsp = (totalEsp % modulo) == 0 ? 0 : modulo - (totalEsp % modulo);
                            if (digitoVerificadorRecibidoEsp == digitoVerificadorObtenidoEsp)
                                flag3 = true;
                            else
                                flag3 = false;
                        }
                        else
                            flag3 = false

                        if (flag || flag2 || flag3)
                            arguments.IsValid = true;
                        else
                            arguments.IsValid = false;
                    } else
                        arguments.IsValid = false;

                }
                else if ($("#ddlTipo").val() == 1) {
                    if (digitos == 10) {
                        for (i = 0; i < 9; i++) {
                            if (i % 2 == 0) {
                                mul = parseInt(txt.value.charAt(i)) + parseInt(txt.value.charAt(i));

                                if (mul <= 9)
                                    suma = suma + mul;
                                else
                                    suma = suma + (mul - 9);
                            }
                            else {
                                suma = suma + parseInt(txt.value.charAt(i));
                            }
                        }
                        dv = suma % 10;
                        if (dv != 0) {

                            dv = 10 - dv;

                        }
                        else {
                            dv = 0;
                        }
                        if (dv == txt.value.charAt(9)) {
                            source.errormessage = "";
                            arguments.IsValid = true;

                        }
                        else {
                            source.errormessage = "Identificación no válida";
                            arguments.IsValid = false;
                        }

                    } else {
                        source.errormessage = "Identificación no válida";
                        arguments.IsValid = false;
                    }
                }
                else if ($("#ddlTipo").val() == 2) {
                    if (digitos < 10) {
                        source.errormessage = "";
                        arguments.IsValid = true;
                    }
                    else {
                        source.errormessage = "Identificación no válida";
                        arguments.IsValid = false;
                    }
                }
            }
            function terminos() {
                document.getElementById("frm_Terminos").src = "../terminos/terminos.pdf";
                $('#ModalTerminos').modal('show');
            }
            function privacidad() {
                document.getElementById("frm_Terminos").src = "../terminos/privacidad.pdf";
                $('#ModalTerminos').modal('show');
            }

        </script>
        <style>
            .titulo {
                font-family: Verdana, Geneva, Tahoma, sans-serif;
                font-size: 1.1em;
                font-weight: bold;
                color: #2e89b6;
            }

            .titulo_adv {
                font-family: Verdana, Geneva, Tahoma, sans-serif;
                font-size: 1.3em;
                font-weight: bold;
                color: #2e89b6;
            }

            .modal iframe {
                width: 98%;
                height: 100%;
            }

            .la {
                width: 65%;
            }

            .modal-dialog.reg,
            .modal-content.reg, .modal-body.reg {
                /* 80% of window height */
                height: 70%;
            }

            .dolar {
                text-align: right;
            }

                .dolar::before {
                    content: "$ ";
                }

            .radio-pago {
                font-family: 'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;
                color: #808080;
            }
        </style>
        <!-- Modal terminos y privacidad -->
        <div class="container">
            <div class="modal fade " id="ModalTerminos" role="dialog">
                <div class="modal-dialog modal-lg reg">
                    <!-- Modal content-->
                    <div class="modal-content reg">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="modal-body reg">
                            <iframe id="frm_Terminos" style="border: none;"></iframe>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <!-- / Modal-->
        <div class="containt">
            <div class="content">
                <div class="row">
                    <div class="col-xs-8">
                        <div class="content">
                            <div class="row text-center">
                                <div class="col-xs-12">
                                    <img src="../interfaces/media/logo_sek.png" width="30%" />
                                </div>
                            </div>
                            <div class="row text-center">
                                <div class="col-xs-12">
                                    <h5>
                                        <label><b>¡Atenci&oacute;n! Este documento no tiene valor tributario.</b></label></h5>
                                </div>
                            </div>

                            <br />
                            <div class="row">
                                <div class="col-sm-offset-2 col-xs-12 col-md-8">
                                    <div class="row ">
                                        <div class="col-xs-12">
                                            <h4>
                                                <asp:Label ID="lbl_fecha" runat="server"></asp:Label></h4>
                                        </div>
                                        <div class="col-xs-12">
                                            <h4>
                                                <label>Desglose de productos</label></h4>
                                        </div>
                                    </div>
                                    <div class="row ">
                                        <div class="col-xs-12">
                                            <asp:Table runat="server" ID="grd_Valores" CssClass="table-responsive" Width="100%"></asp:Table>
                                        </div>
                                    </div>
                                    <br />


                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-4">
                        <asp:Panel runat="server" ID="pnl_boton">

                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    <asp:Label runat="server" ID="lblTitulo" Text="Datos de Factura" CssClass="titulo"></asp:Label></h3>
                            </div>
                            <div class="panel-body">
                                <fieldset>
                                    <div class="form-group">
                                        <label class="col-sm-3" for="txtNombre">Nombre:</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox class="form-control" runat="server" ID="txtNombre" placeholder="Nombre" MaxLength="200" autofocus></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombre" ForeColor="Red" ErrorMessage="Ingrese un nombre"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3" for="ddlTipo">Tipo Identificación:</label>
                                        <div class="col-sm-9">
                                            <asp:DropDownList class="form-control" runat="server" ID="ddlTipo" ClientIDMode="Static">
                                                <asp:ListItem Value="0" Text="RUC"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Cédula"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Pasaporte"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlTipo" ForeColor="Red" ErrorMessage="Seleccione una opción"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3" for="txtCedula">Identificación:</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox class="form-control" runat="server" ID="txtCedula" placeholder="Identificación" required="required" MaxLength="13" ClientIDMode="Static"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCedula" ForeColor="Red" ErrorMessage="Ingrese una identificación"></asp:RequiredFieldValidator><br />
                                            <asp:CustomValidator ID="csCedula" runat="server" ClientValidationFunction="ValidarCed" ControlToValidate="txtCedula" ForeColor="Red" ErrorMessage="Identificación no válida" ValidateEmptyText="True"> </asp:CustomValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3" for="txtDireccion">Dirección:</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox class="form-control" runat="server" ID="txtDireccion" placeholder="Dirección" required="required" MaxLength="80"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDireccion" ForeColor="Red" ErrorMessage="Ingrese una dirección"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3" for="txtTelefono">Teléfono:</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox class="form-control" runat="server" ID="txtTelefono" placeholder="Teléfono" required="required" MaxLength="15" onkeydown="return valid(event)"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTelefono" ForeColor="Red" ErrorMessage="Ingrese un teléfono"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3" for="txtEmail">Email:</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox class="form-control" runat="server" ID="txtEmail" placeholder="Email" required="required" MaxLength="150" TextMode="Email"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEmail" ForeColor="Red" ErrorMessage="Ingrese un correo electrónico"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                </fieldset>

                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                        <label class="titulo">Formas de Pago</label></h3>
                                </div>
                                <div class="panel-body">
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-xs-12" style="font-family: 'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;">Tarjetas aceptadas:&nbsp;</label>

                                            <div class="col-xs-12">
                                                <asp:Image runat="server" ID="img_tarjetas" ImageUrl="../interfaces/images/logos_tarjetascredito_2.png" Width="200px" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">

                                        <asp:UpdatePanel runat="server" ID="upnl_Tarjeta" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-xs-12 col-md-3">
                                                        <label class="radio-pago">Marca Tarjeta&nbsp;</label>
                                                    </div>
                                                    <div class="col-xs-12 col-md-9">
                                                        <asp:DropDownList runat="server" ID="ddl_Tarjeta" OnSelectedIndexChanged="ddl_Tarjeta_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xs-12">
                                                        <asp:RequiredFieldValidator runat="server" ID="rq_Tarjeta" ControlToValidate="ddl_Tarjeta" CssClass="alert-danger" ValidationGroup="pagoTarjeta" ErrorMessage="Debe seleccionar una tarjeta" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <asp:UpdatePanel runat="server" ID="upnl_Banco" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="row">
                                                            <div class="col-xs-12 col-md-3">
                                                                <label class="radio-pago">Banco Emisor</label>
                                                            </div>
                                                            <div class="col-xs-12 col-md-9">
                                                                <asp:DropDownList runat="server" ID="ddl_Banco" OnSelectedIndexChanged="ddl_Banco_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xs-12">
                                                                <asp:RequiredFieldValidator runat="server" ID="rq_Banco" ControlToValidate="ddl_Banco" CssClass="alert-danger" ValidationGroup="pagoTarjeta" ErrorMessage="Debe seleccionar un Banco Emisor" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>

                                                        <asp:UpdatePanel runat="server" ID="upnl_Diferido" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <div class="row">
                                                                    <div class="col-xs-12 col-md-3">
                                                                        <label class="radio-pago">Tipo Cr&eacute;dito</label>
                                                                    </div>
                                                                    <div class="col-xs-12 col-md-9">
                                                                        <asp:DropDownList runat="server" ID="ddl_TipoCredito" OnSelectedIndexChanged="ddl_TipoCredito_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-xs-12">
                                                                        <asp:RequiredFieldValidator runat="server" ID="rq_TipoCredito" ControlToValidate="ddl_TipoCredito" CssClass="alert-danger" ValidationGroup="pagoTarjeta" ErrorMessage="Debe seleccionar un Tipo de Crédito" InitialValue="0"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-xs-12 col-md-3">
                                                                        <label class="radio-pago">Meses Diferido</label>
                                                                    </div>
                                                                    <div class="col-xs-12 col-md-4">
                                                                        <asp:RadioButtonList runat="server" ID="rbl_meses" CssClass="radio-pago" AutoPostBack="false" RepeatDirection="Horizontal">
                                                                        </asp:RadioButtonList>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-xs-12">
                                                                        <asp:RequiredFieldValidator runat="server" ID="rq_meses" ControlToValidate="rbl_meses" CssClass="alert-danger" ValidationGroup="pagoTarjeta" ErrorMessage="Debe seleccionar los meses de plazo"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>

                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="ddl_TipoCredito" EventName="selectedindexchanged" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddl_Banco" EventName="selectedindexchanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddl_Tarjeta" EventName="selectedindexchanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>

                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="checkbox-inline">
                                                    <asp:CheckBox runat="server" ID="chkTerminos" ClientIDMode="Static" OnClick="bloquear();" />

                                                    <label style="color: red; font-weight: bold;">
                                                        Confirme que los datos ingresados sean correctos, estos saldr&aacute;n en su factura. Adem&aacute;s acepta los 
                                                        <a href="#" onclick="terminos();">t&eacute;rminos y condiciones</a>, <a href="#" onclick="privacidad();">pol&iacute;ticas de privacidad</a>.                                            
                                                    </label>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <asp:Button ID="btn_pagar" runat="server" Text="Pagar" CssClass="btn btn-success btn-block" OnClick="btn_pagar_Click" OnClientClick=" return AcceptTermsCheckBoxValidation();" CausesValidation="true" />

                            </div>

                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnl_pagos" Visible="false" Height="100%" Style="position: fixed;">
                            <iframe runat="server" id="if_pagos" height="100%" width="100%"></iframe>
                        </asp:Panel>
                                                 <br />
        <br />
        <br />
        <br />

                    </div>
                </div>
            </div>
        </div>
</asp:Content>
