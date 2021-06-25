<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Site.Master" AutoEventWireup="true" CodeBehind="formUnidades.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.Unidades" async="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10.16.5/dist/sweetalert2.all.min.js" type="text/javascript"></script>
    <link href="../Content/sweetalert.css" rel="stylesheet"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
             <div class="container" style="width:40%;padding-left:10%;padding-right:10%;">
                     <h4 class="well" style="background-color:rgb(8, 83, 148);color:white;text-align:center">Ingreso Unidades por Nivel</h4>
	                    <div class=" well">
	                        <div class="row">
                                
                                <div class="form-group ">
                                <asp:Label ID="Label1" runat="server" Text="Codigo Unidad"></asp:Label> 
                                <asp:TextBox ID="txtCodUnidad" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group col-sm-6">
                                <asp:Label ID="Label3" runat="server" Text="Numero de Unidades Inicio"></asp:Label> 
                                <asp:TextBox ID="txtInicio" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group col-sm-6">
                                <asp:Label ID="Label2" runat="server" Text="Numero de Unidades Fin"></asp:Label> 
                                <asp:TextBox ID="txtFin" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                <center>
                               <div class="form-group">
                                  <asp:Button ID="Button1" runat="server" Text="Guardar Unidad" class="btn btn-success" OnClick="Button1_Click"/>
                              </div>
                                    </center>
                           </div>
                                 
                     </div>
                 </div>
              </div>
          </ContentTemplate>
       </asp:UpdatePanel>
           <script type="text/javascript">
       function confirm() {
           Swal.fire({
               icon: 'success',
               title: 'OK',
               text: 'El Registro se Guardo Corectamente!',
               footer: '<a href></a>'
           })
       }
           </script>
</asp:Content>
