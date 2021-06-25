<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Interfaces/Site.Master" CodeBehind="formCursos.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.formCursos" async="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>INGRESO CURSOS</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
                 <div class="container" style=" padding-left:10%;padding-right:10%;">
                     <h4 class="well" style="background-color:rgb(8, 83, 148);color:white;text-align:center">Ingreso Cursos</h4>
	<div class="col-lg-12 well">
	<div class="row">
        <div class="col-sm-12">

                                <div class="form-group">
                                <asp:Label ID="Label2" runat="server" Text="Nombre Curso"></asp:Label>
                                <asp:TextBox ID="txtDescCurso" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                <asp:Label ID="Label1" runat="server" Text="Cod Curso"></asp:Label>
                                <asp:TextBox ID="txtCodCurso" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="Button1" runat="server" Text="Guardar Curso" class="btn btn-success" OnClick="Button1_Click"/>
                                </div>
            </div>
        </div>
        </div>
                     </div>
                                

</asp:Content>





