<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Site.Master" AutoEventWireup="true" CodeBehind="formCalificacionNivel.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.formCalificacionNivel" async="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10.16.5/dist/sweetalert2.all.min.js" type="text/javascript"></script>
    <link href="../Content/sweetalert.css" rel="stylesheet"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
               <div class="container" style="padding-left:10%;padding-right:10%;">
                 <h4 class="well" style="background-color:rgb(8, 83, 148);color:white;text-align:center">Ingreso Calificación Niveles</h4>
	               <div class="col-lg-12 well">
	                <div class="row">
                        <div class="col-sm-12">
                                <div class="row">
                                    <div class="form-group col-sm-6">
                                        <b><asp:Label ID="label1" runat="server" Text="Tipo Nivel" ></asp:Label></b>
                                         <asp:DropDownList ID="cbxTipoNivel" runat="server" AppendDataBoundItems="true" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="cbxTipoNivel_SelectedIndexChanged" >                                   
                                         <asp:ListItem Text="-Seleccionar-" Value="0"></asp:ListItem>
                                         </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-sm-6">
                                        <b><asp:Label ID="Label7" runat="server" Text="Nivel" ></asp:Label></b>
                                         <asp:DropDownList ID="cbxNivel" runat="server" AutoPostBack="true" class="form-control" >                                   
                                         </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                  <div class="form-group col-sm-6">
                                    <asp:Label ID="Label6" runat="server" Text="Calificación Desde"></asp:Label>
                                    <asp:TextBox ID="txtCalificacionDesde" runat="server" class="form-control" required="true"></asp:TextBox>
                                  </div>
                                   <div class="form-group col-sm-6">
                                    <asp:Label ID="Label2" runat="server" Text="Calificación Hasta"></asp:Label>
                                    <asp:TextBox ID="txtCalificacionHasta" runat="server" class="form-control" required="true"></asp:TextBox>
                                  </div>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="Button1" runat="server" Text="Guardar Calificación"  class="btn btn-success" OnClick="Button1_Click" />
                                </div>
                         </div>
                    </div>
                </div>
            </div>
    <script>
        function re() {

            Swal.fire({
                icon: 'error',
                title: 'error',
                text: 'Registro No se pudo Guardar,Ya existe un nivel con las mismas calificaciones',
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
        function confirm() {

            Swal.fire({
                icon: 'success',
                title: 'OK',
                text: 'Registro Correcto!',
                footer: '<a href></a>'
            })
        }
    </script>
</asp:Content>
