<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="formDiasHorarios.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.formDiasHorarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <!--MODAL-->
         <asp:Panel ID="ss" runat="server" style="background:white; " Height="346px" Width="390px">
                        <div class="row-fluid">
                                <div class="control-group">                                            
                                    <label class="control-label" ><strong>Periodo :</strong></label>
                                    <div class="controls">
                                        <asp:DropDownList ID="ddlTipoCliente0" CssClass="span11" runat="server" 
                                            TabIndex="1" 
                                            AutoPostBack="True" ValidationGroup="val4" Font-Bold="True">
                                            <asp:ListItem>Lunes</asp:ListItem>
                                            <asp:ListItem>Martes</asp:ListItem>
                                            <asp:ListItem>Miercoles</asp:ListItem>
                                            <asp:ListItem>Jueves</asp:ListItem>
                                            <asp:ListItem>Viernes</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="control-group">                                            
                                    <label class="control-label" ><strong>Nombres :</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtNombres" runat="server" CssClass="span11" 
                                            placeholder="Nombres" TabIndex="4" ValidationGroup="val4"></asp:TextBox>
                                    </div>
                                    <label class="control-label" ><strong>Apellidos :</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtApellidos" runat="server" CssClass="span11" 
                                            placeholder="Apellidos" TabIndex="4" ValidationGroup="val4"></asp:TextBox>
                                    </div>
                                    <label class="control-label" ><strong>Cedula :</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtCedula1" runat="server" CssClass="span11" 
                                            placeholder="Cedula" TabIndex="4" ValidationGroup="val4"></asp:TextBox>
                                    </div>
                                    <label class="control-label" ><strong>Celular :</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtCelular" runat="server" CssClass="span11" 
                                            placeholder="Celular" TabIndex="4" ValidationGroup="val4"></asp:TextBox>
                                    </div>
                                    <label class="control-label" ><strong>Telefono :</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="span11" 
                                            placeholder="Telefono" TabIndex="4" ValidationGroup="val4"></asp:TextBox>
                                    </div>
                                     <label class="control-label" ><strong>Direccion :</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="span11" 
                                            placeholder="Direccion" TabIndex="4" ValidationGroup="val4"></asp:TextBox>
                                    </div>
                                    <label class="control-label" ><strong>Email :</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="span11" 
                                            placeholder="Email" TabIndex="4" ValidationGroup="val4"></asp:TextBox>
                                    </div>
                                </div>
                        </div>
            </asp:Panel>
        <!--FIN MODAL-->
        </div>
    </form>
</body>
</html>
