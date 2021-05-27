<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Site.Master" AutoEventWireup="true" CodeBehind="Depositos.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.Depositos" Async="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--head--%>
    <script src="../Scripts/Funciones.js?ver=0.4"></script>

    <script src="../Scripts/jquery-ui-1.12.1.js"></script>

    <script>
        $(document).ready(function () {
            revisar_concepto();
        });

        function Confirm() {

            var size;

            var filePath;


            var allowedExtensions = /(\.pdf)$/i;
            var confirm_value;
            //alert("tipo: " + tipo);
            Page_ClientValidate("ingreso");

            if (Page_IsValid) {

                //alert("entro tipo: " + tipo);

                if (document.getElementById('fu_archivo').files.length > 0) {
                    size = document.getElementById('fu_archivo').files[0].size;
                    if (size > 2465792) {
                        $("#fu_archivo").val("");
                        alert('Solo se aceptan archivos tipo PDF,JPG,PNG de hasta 2 mb.');
                        return false;
                    }
                    filePath = document.getElementById('fu_archivo').value;
                    allowedExtensions = /(\.pdf|\.jpg|\.jpeg|\.png)$/i;
                    if (!allowedExtensions.exec(filePath)) {
                        alert("Solo admite archivos de tipo PDF,JPG,PNG");
                        $("#fu_archivo").val("");
                        return false;
                    }
                } else {
                    alert("El Documento es Obligatorio");
                    return false;
                }


                confirm_value = document.createElement('INPUT');
                confirm_value.type = 'hidden';
                confirm_value.name = 'confirm_value';
                if (confirm('¿Está seguro/a que desea guardar los datos?\n Una vez guardados no se podrá realizar cambios.')) {
                    confirm_value.value = 'Yes';
                    document.forms[0].appendChild(confirm_value);
                    return true;
                }
                else {
                  return false;
                }

            }
        };
        function revisar_concepto() {
            var e = document.getElementById("ddl_concepto");
            var result = e.options[e.selectedIndex].text;
            if (result == "Otros") {
                $("#txt_concepto").removeAttr("disabled");
            } else {

                $("#txt_concepto").attr("disabled", "disabled");
            }
        };

    </script>

    <style>
        body {
            font-family: "Helvetica Neue",Helvetica,Arial,sans-serif;
            font-size: 14px;
            line-height: 1.42857143;
            color: #333;
            background-color: #fff;
        }

        .ui-widget-content {
            border: 1px solid #dddddd;
            background: #ffffff;
            color: #333333;
        }

                    .dolar {
                text-align: right;
            }

                .dolar::before {
                    content: "$ ";
                }



    </style>

    <%--</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">--%>
<!--INICIO DE DESGLOCE DE PRODUCTO-->
  <div class="containt">
            <div class="content">
                <div class="row">
                   <div class="col-xs-8">
                        <div class="content">
                            <div class="row text-center">
                                <div class="col-xs-12">
                                    <img src="/media/logo_sek.png" width="30%" />
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
                                <div class="col-sm-offset-2 col-xs-12 col-md-8" style="padding-right:50px">
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
                                           <div class="col-xs-4" >
                                               <!--INICIO INFORMACION-->
                        <div class="ContentPlaceHolder1_pnl_boton" style="padding-right:40px">
                            <div class="panel-body" >
        <div class="row">
            <div class="text-center titulo">
                <h2>
                    <asp:Label runat="server" ID="Label1">Ingreso Depósitos</asp:Label>

                </h2>
            </div>
        </div>
        <div class="row">
            <div class="text-center">
                <h3>Informaci&oacute;n de Facturaci&oacute;n</h3>
            </div>
        </div>
        <hr>
        <div class="row">
            <asp:HiddenField ID="hiddenPeriodo" runat="server" />
            <div class="col-sm-5">
                <asp:Label runat="server" Style="font-weight: bold;" Text="Tipo Identificación"></asp:Label>
            </div>
            <div class="col-sm-7">
                <asp:DropDownList class="form-control" runat="server" ID="ddlTipo" ClientIDMode="Static">
                    <asp:ListItem Value="0" Text="RUC"></asp:ListItem>
                    <asp:ListItem Value="1" Text="Cédula"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Pasaporte"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-5">
                <asp:Label runat="server" Style="font-weight: bold;" Text="Identificación"></asp:Label>
            </div>
            <div class="col-sm-7">
                <asp:TextBox runat="server" CssClass="form-control" ID="txt_cedula"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rq_cedula" runat="server" ControlToValidate="txt_cedula" InitialValue="" ValidationGroup="ingreso" ForeColor="Red" ErrorMessage="Ingrese una cédula para la factura"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-5">
                <asp:Label runat="server" Style="font-weight: bold;" Text="Nombre"></asp:Label>
            </div>
            <div class="col-sm-7">
                <asp:TextBox runat="server" CssClass="form-control" ID="txt_nombre"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rq_nombre" runat="server" ControlToValidate="txt_nombre" InitialValue="" ValidationGroup="ingreso" ForeColor="Red" ErrorMessage="Ingrese un nombre para la factura"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-5">
                <asp:Label runat="server" Style="font-weight: bold;" Text="Dirección"></asp:Label>
            </div>
            <div class="col-sm-7">
                <asp:TextBox runat="server" CssClass="form-control" ID="txt_direccion"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rq_direccion" runat="server" ControlToValidate="txt_direccion" InitialValue="" ValidationGroup="ingreso" ForeColor="Red" ErrorMessage="Ingrese una dirección para la factura"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-5">
                <asp:Label runat="server" Style="font-weight: bold;" Text="Correo electrónico:"></asp:Label>
            </div>
            <div class="col-sm-7">
                <asp:TextBox runat="server" CssClass="form-control" ID="txt_correo"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rq_correo" runat="server" ControlToValidate="txt_correo" InitialValue="" ValidationGroup="ingreso" ForeColor="Red" ErrorMessage="Ingrese un correo electrónico para la factura"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-5">
                <asp:Label runat="server" Style="font-weight: bold;" Text="Teléfono:"></asp:Label>
            </div>
            <div class="col-sm-7">
                <asp:TextBox runat="server" CssClass="form-control" ID="txt_telefono"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rq_telefono" runat="server" ControlToValidate="txt_telefono" InitialValue="" ValidationGroup="ingreso" ForeColor="Red" ErrorMessage="Ingrese un teléfono para la factura"></asp:RequiredFieldValidator>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="text-center">
                <h3>
                    <asp:Label runat="server" Text="Información Transacción"></asp:Label></h3>
            </div>
        </div>
        <br />
        <br />
        <div class="row">
            <div class="col-sm-5">
                <asp:Label runat="server" Text="Tipo de transacción:" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-sm-7">
                <asp:DropDownList CssClass="form-control" runat="server" ID="ddl_tipo_pago" ClientIDMode="Static">
                    <asp:ListItem Value="1" Text="Depósito"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Transferencia"></asp:ListItem>
                    <asp:ListItem Value="3" Text="Pago Anticipado"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-5">
                <asp:Label runat="server" Text="Depósito realizado en:" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-sm-7">
                <asp:DropDownList runat="server" CssClass="form-control" ClientIDMode="Static" ID="ddl_origen_deposito" AutoPostBack="false"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rq_banco" runat="server" ControlToValidate="ddl_origen_deposito" InitialValue="0" ValidationGroup="ingreso" ForeColor="Red" ErrorMessage="Seleccione una opción"></asp:RequiredFieldValidator>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-5">
                <asp:Label runat="server" Text="Fecha depósito" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-sm-7">
                <asp:TextBox runat="server" TextMode="Date" ID="txt_fecha_deposito" AutoCompleteType="Disabled" ClientIDMode="Static" placeholder="dd/mm/aaaa" CssClass=" form-control clDate"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rq_fecha" runat="server" ControlToValidate="txt_fecha_deposito" InitialValue="" ValidationGroup="ingreso" ForeColor="Red" ErrorMessage="Ingrese una fecha"></asp:RequiredFieldValidator>
            </div>
        </div>
        <br />
        <div class="row">            
            <div class="col-sm-5">
                <asp:Label runat="server" Text="Valor depositado" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-sm-7">
                <asp:TextBox runat="server" ID="txt_valor_depositado" onkeypress="return validateFloatKeyPress(this,event);" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rq_valor" runat="server" ControlToValidate="txt_valor_depositado" InitialValue="" ValidationGroup="ingreso" ForeColor="Red" ErrorMessage="Ingrese un valor"></asp:RequiredFieldValidator>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-5">
                <asp:Label runat="server" Text="No Comprobante" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-sm-7">
                <asp:TextBox runat="server" ID="txt_comprobante" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rq_comprobante" runat="server" ControlToValidate="txt_comprobante" InitialValue="" ValidationGroup="ingreso" ForeColor="Red" ErrorMessage="Ingrese un numero de comprobante"></asp:RequiredFieldValidator>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-5">
                <asp:Label runat="server" Text="Concepto de transacción" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-sm-7">
                <asp:DropDownList runat="server" ClientIDMode="Static" CssClass="form-control" ID="ddl_concepto" onchange="revisar_concepto();" AutoPostBack="false"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rq_concepto" runat="server" ControlToValidate="ddl_concepto" InitialValue="x" ValidationGroup="ingreso" ForeColor="Red" ErrorMessage="Seleccione una opción"></asp:RequiredFieldValidator>
            </div>
        </div>
        <br />
        <div class="row" hidden="hidden">
            <div class="col-sm-5">
            </div>
            <div class="col-sm-7">
                <asp:TextBox runat="server" ID="txt_concepto" ClientIDMode="Static" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-5">
                <asp:Label runat="server" Text="Seleccionar Archivo" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-sm-7">
                <asp:FileUpload runat="server" ClientIDMode="Static" ID="fu_archivo" accept="image/png, image/jpeg, .pdf" />
            </div>
        </div>
        <br />
        <div class="row">
            <%--<div class="col-sm-6text-right">
                <asp:HyperLink runat="server" Text="Regresar" NavigateUrl="confirmacionCarrito.aspx" CssClass="btn btn-info btn-xs"></asp:HyperLink>
            </div>--%>
            <div class="text-center">
                <asp:Button runat="server" Text="Enviar" CssClass="btn btn-success" ID="btn_enviar" ValidationGroup="ingreso" OnClientClick="return Confirm();" OnClick="enviar_Click"></asp:Button>
            </div>

        </div>
    </div>
                    <!--FIN INICIO INFORMACION-->

</div>


                       </div>
                  </div>

               </div>

            </div>
    <!--FIN DESGLOSE DE PRODUCTO-->

    <br />
    <br />
    <br />
    <br />
 
    <script>
        $(document).ready(
            function () {
                var d = new Date();
                var fecha = d.format("yyyy-MM-dd");
                $("#<%=txt_fecha_deposito.ClientID%>").val(fecha);


                $(".clDate").datepicker({ dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true, yearRange: '-2:+1', editable: false, maxDate: '0' });

                $.datepicker.regional['es'] = {
                    closeText: 'Cerrar',
                    prevText: '<Ant',
                    nextText: 'Sig>',
                    currentText: 'Hoy',
                    monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                    monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
                    dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
                    dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
                    dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
                    weekHeader: 'Sm',
                    dateFormat: 'dd/mm/yy',
                    firstDay: 1,
                    isRTL: false,
                    showMonthAfterYear: false,
                    yearSuffix: ''
                };
                $.datepicker.setDefaults($.datepicker.regional['es']);

            });

    </script>



</asp:Content>
