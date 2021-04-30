<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WEBFORMPRUEBAS.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.WEBFORMPRUEBAS" async="true"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10.16.5/dist/sweetalert2.all.min.js" type="text/javascript"></script>
   <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.min.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button Text="val" ID="btn" runat="server" OnClientClick="return confirmdelete();" />

            <input type="button" id="btn2" value="valor" onclick="confirmdelete()" />
        </div>
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        <asp:Button runat="server" Text="Descargar .xcel" ID="btnExccel" OnClick="btnExccel_Click"/>
        <asp:Button runat="server" Text="enviar Sesion" OnClick="Unnamed1_Click" />
    </form>
</body>
</html>


