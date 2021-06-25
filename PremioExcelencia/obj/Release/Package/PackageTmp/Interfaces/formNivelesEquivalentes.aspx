<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Interfaces/Site.Master" CodeBehind="formNivelesEquivalentes.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.formEquivalenciaNivel" async="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Niveles Autonomos</title>
         <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10.16.5/dist/sweetalert2.all.min.js" type="text/javascript"></script>
    <link href="../Content/sweetalert.css" rel="stylesheet"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="container" >
                     <h4 class="well" style="background-color:rgb(8, 83, 148);color:white;text-align:center">Ingreso Niveles Equivalentes</h4>
	<div class="col-lg-12 well">
	<div class="row">
        <div class="col-sm-12">
                               <div class="form-group">
                                <asp:Label ID="Label4" runat="server" Text="Nivel Programado"></asp:Label>
                                <asp:DropDownList ID="cbxNivelProgramado" AppendDataBoundItems="true" runat="server" AutoPostBack="true" class="form-control" >
                                <asp:ListItem Text="-seleccionar-" Value="0" />
                                </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                <asp:Label ID="Label5" runat="server" Text="Nivel Autonomo"></asp:Label>
                                <asp:DropDownList ID="cbxNivelAutonomo" AppendDataBoundItems="true" runat="server" AutoPostBack="true" class="form-control" >
                                <asp:ListItem Text="-seleccionar-" Value="0" />
                                </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                     <asp:Button ID="btnGuardar" runat="server" Text="Guardar" class="btn btn-success" OnClick="btnGuardar_Click"/>                                                               
                                </div>
            </div>
        </div>
        </div>
       </div>
       <script type="text/javascript">
       function confirm() {
           Swal.fire({
               icon: 'success',
               title: 'OK',
               text: 'El Registro se Guardo Corectamente!',
               footer: '<a href></a>'
           })
       }
           function existe() {
               Swal.fire({
                   icon: 'error',
                   text: 'El Registro ya existe',
                   footer: '<a href></a>'
               })
           }
           function select() {
               Swal.fire({
                   icon: 'error',
                   text: 'Debe seleccionar un nivel autonomo y un nivel programamdo',
                   footer: '<a href></a>'
               })
           }
       </script>
</asp:Content>




