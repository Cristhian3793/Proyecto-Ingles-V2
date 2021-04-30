<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Site.Master" AutoEventWireup="true" CodeBehind="formUnidades.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.Unidades" async="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
             <div class="container" style=" padding-left:10%;padding-right:10%;">
                     <h4 class="well" style="background-color:rgb(8, 83, 148);color:white;text-align:center">Ingreso Unidades por Nivel</h4>
	                    <div class="col-lg-12 well">
	                        <div class="row">
                                <div class="col-sm-12">
                                <div class="form-group">                                
                                <asp:Label ID="Label1" runat="server" Text="Nivel"></asp:Label>
                                <asp:DropDownList ID="ddlNivel" AppendDataBoundItems="true" runat="server" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="ddlNivel_SelectedIndexChanged">                               
                                 <asp:ListItem Text="--seleccionar--" Value="0" />
                                </asp:DropDownList>                               
                                </div>  
                                <div class="form-group">                                
                                <asp:Label ID="Label2" runat="server" Text="Tema de Unidad"></asp:Label>
                                <asp:DropDownList ID="ddlTema" AppendDataBoundItems="true" runat="server" AutoPostBack="true" class="form-control">                               
                                 <asp:ListItem Text="--seleccionar--" Value="0" />
                                </asp:DropDownList>                               
                                </div>          
                                <div class="form-group">
                                <asp:Label ID="Label3" runat="server" Text="Numero Unidades"></asp:Label> 
                                <asp:TextBox ID="txtDescUnidad" runat="server" class="form-control"></asp:TextBox>
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
</asp:Content>
