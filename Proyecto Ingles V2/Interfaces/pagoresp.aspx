<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pagoresp.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.pagoresp" async="true"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <!-- jQuery -->
    <script src="../Scripts/jquery-3.4.1.min.js"></script>
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous" />

    <!-- Latest compiled and minified JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>

    <title>Pagos en L&iacute;nea</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="content">
                <table style="width: 400px; text-align: center; margin: 10px auto; border: 2px solid gray; font-size: 10px; font-family: Arial;">
                    <tr>
                        <td style="padding: 10px 0px 20px 0px; font-size: larger; font-weight: bold;">
                            <asp:Image runat="server" ID="imgLogo" ImageUrl="~/media/logo.png" Width="257px" Height="108px" />
                            <br />
                            <asp:Label runat="server" ID="lblTitulo" Text="Pago en Línea"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="font-size: 10px; font-family: Arial; margin-left: 30%; width: 40%; margin-right: 30%;">
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblNumeroOrden"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lnlNumeroAutorizacion"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblFecha"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblTarjeta"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblValor"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblEstado"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblError"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblMensaje"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnImprimir" OnClientClick="javascript:window.print();" Text="Imprimir" Visible="false" CssClass="btn" />
                            &nbsp;
                <asp:Button runat="server" ID="btnSalir" OnClick="btnSalir_Click" Text="Cerrar" CssClass="btn" />
                        </td>
                    </tr>

                </table>
            </div>
        </div>
    </form>
</body>
</html>
