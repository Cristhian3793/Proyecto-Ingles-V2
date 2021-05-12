<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WEBFORMPRUEBAS.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.WEBFORMPRUEBAS" async="true"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     
    <%--<script src="../Scripts/Funciones.js?ver=0.4"></script>--%>
    <script src="../Scripts/jquery-3.1.1.slim.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txt_fecha_deposito" AutoCompleteType="Disabled" ClientIDMode="Static" placeholder="dd/mm/aaaa" CssClass=" form-control clDate"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rq_fecha" runat="server" ControlToValidate="txt_fecha_deposito" InitialValue="" ValidationGroup="ingreso" ForeColor="Red" ErrorMessage="Ingrese una fecha"></asp:RequiredFieldValidator>
            </div>
            <asp:Button Text="val" ID="btn" runat="server" OnClientClick="return confirmdelete();" />

            <input type="button" id="btn2" value="valor" onclick="confirmdelete()" />
        </div>
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        <asp:Button runat="server" Text="Descargar .xcel" ID="btnExccel" OnClick="btnExccel_Click"/>
        <asp:Button runat="server" Text="enviar Sesion" OnClick="Unnamed1_Click" />
    </form>
</body>
</html>
    <script type="text/javascript">

        $(document).ready(

            function () {
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


