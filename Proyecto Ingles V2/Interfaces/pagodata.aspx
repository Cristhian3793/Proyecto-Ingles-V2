<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pagodata.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.pagodata" %>

<!DOCTYPE html>

<html>
<head>
    <!-- jQuery -->
    <script src="../Scripts/jquery-3.4.1.min.js"></script>
    <%--testlink--%>
    <script src="https://test.oppwa.com/v1/paymentWidgets.js?checkoutId=<%Response.Write(Request.QueryString["id"]); %>">
    <%--produccion--%>
    <%--<script src="https://oppwa.com/v1/paymentWidgets.js?checkoutId=<%Response.Write(Request.QueryString["id"]); %>">--%>
    </script>
    <style>
        body {
            background-color: #f6f6f5;
        }

        .button {
            width: 100%
        }

        @media (min-width: 480px) {
            /* this rule applies only to devices with a minimum screen width of 480px */
            .button {
                width: 50%;
            }
        }
    </style>
    <meta name="viewport" content="width=device-width, initial-scale=1">
</head>
<body>
    <%--testlink--%>
   <form id="wpwl-form-card" action="https://pruebas.stupendo.ec/v1/payment/process" class="paymentWidgets" data-brands="VISA MASTER DINERS DISCOVER AMEX"></form>
    <%--produccion--%>
    <%--<form id="wpwl-form-card" action="https://app.stupendo.ec/v1/payment/process" class="paymentWidgets" data-brands="<%Response.Write(Request.QueryString["brand"]); %>"></form>--%>
</body>
<%-- customization https://datafast.docs.oppwa.com/tutorials/integration-guide/widget-api--%>
<script language="JavaScript" type="text/javascript" src="https://www.datafast.com.ec/js/dfAdditionalValidations1.js"></script>
<script>
    var wpwlOptions = {
        onReady: function () {
            var dif = <%Response.Write(Request.QueryString["dif"]); %>;
            var tipo =<%Response.Write(Request.QueryString["tipo"]); %>;
            var tipocredito =
                '<div class="wpwl-wrapper wpwl-wrapper-custom" style="display:inlineblock">' +
                'Tipo de crédito: <select name="customParameters[SHOPPER_TIPOCREDITO]" >';
            console.log(tipo);
            switch (tipo) {
                case "00"://corriente
                    tipocredito += '<option selected value="00">Corriente</option > ';
                    break;
                case "01":
                    //dif corriente
                    tipocredito += '<option selected value="01">Dif corriente</option>';
                    break;
                case "03":
                    //dif sin int                    
                    tipocredito += '<option selected value="03">Dif sin int</option>';
                    break;
                case "02":
                    //dif con int
                    tipocredito += '<option selected value="02">Dif con int</option>';
                    break;
            }

            tipocredito += '</div>';
            $('form.wpwl-form-card').find('.wpwl-button').before(tipocredito);

            //Inicio Diferidos
            var numberOfInstallmentsHtml = '<div class="wpwl-label wpwl-label-custom" style="display:inline-block">Meses de Diferido: </div> ' +
                '<div class="wpwl-wrapper wpwl-wrapper-custom" style="display:inline-block">' +
                '<select name="recurring.numberOfInstallments">'
            
            numberOfInstallmentsHtml += '<option selected value="' + dif +'">'+dif+'</option>';

            numberOfInstallmentsHtml += '</select>' +
                '</div>';
            $('form.wpwl-form-card').find('.wpwl-button').before(numberOfInstallmentsHtml);
            //total a pagar
            var total = '<br/><br/><div class="wpwl-label wpwl-label-custom" style="display:inline-block">Valor a Pagar: $ <%Response.Write(Request.QueryString["total"]); %></div>' +
                '<div class="wpwl-wrapper wpwl-wrapper-custom" style="display:inline-block">' +
                '</div>';
            $('form.wpwl-form-card').find('.wpwl-button').before(total);
            //fin total a pagar
            var datafast = '<br/><br/><img src=' + '"https://www.datafast.com.ec/images/verified.png" style=' + '"display:block;margin:0 auto; width:100%;">';
            $('form.wpwl-form-card').find('.wpwl-button').before(datafast);
        },
        locale: "es",
        style: "card",
        brandDetection: true,
        maskCvv: true,
        allowEmptyCvv: false,
        showCVVHint: true,
        labels: { cvv: "CVV", cardHolder: "Nombre (Igual que en la tarjeta)" }
    }


</script>

</html>
