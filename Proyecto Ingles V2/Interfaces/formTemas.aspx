<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Site.Master" AutoEventWireup="true" CodeBehind="formTemas.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.Temas" async="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10.16.5/dist/sweetalert2.all.min.js" type="text/javascript"></script>
    <link href="../Content/sweetalert.css" rel="stylesheet"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
             <div class="container" style=" padding-left:10%;padding-right:10%;">
                     <h4 class="well" style="background-color:rgb(8, 83, 148);color:white;text-align:center">Ingreso Unidades por Nivel</h4>
	                    <div class="col-lg-12 well">
	                        <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                    <asp:Label ID="lblTipoNivel" runat="server" Text="Tipo Nivel"></asp:Label>
                                    <asp:DropDownList ID="cbxTipoNivel"  runat="server" AppendDataBoundItems="true" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="cbxTipoNivel_SelectedIndexChanged">
                                        <asp:ListItem Value="0">-Seleccionar-</asp:ListItem>
                                    </asp:DropDownList>  
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" Text="Nivel"></asp:Label>
                                    <asp:DropDownList ID="cbxNivel" runat="server" AutoPostBack="true" class="form-control">                               
                                    </asp:DropDownList>        
                                    </div>
                                </div>
                                <div class="col-sm-12">      
                                 <div class="form-group">
                                <asp:Label ID="Label3" runat="server" Text="Unidad"></asp:Label> 
                                <asp:TextBox ID="txtUnidad" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="Button1" runat="server" Text="Guardar Unidad" class="btn btn-success" OnClick="Button1_Click"/>
                               </div>
                         </div>             
                     </div>
                 </div>
              </div>
          </ContentTemplate>
            </asp:UpdatePanel>
    <script>
        var object = { status: false, ele: null };
        function confirm() {

            Swal.fire({
                icon: 'success',
                title: 'OK',
                text: 'Registro Correcto Revise su correo!',
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


    </script>
</asp:Content>
