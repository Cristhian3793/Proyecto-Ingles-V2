<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Site.Master" AutoEventWireup="true" CodeBehind="formLibros.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.formLibros" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
                    <div class="container" style=" padding-left:10%;padding-right:10%;">
                     <h4 class="well" style="background-color:rgb(8, 83, 148);color:white;text-align:center">Libros</h4>
	                    <div class="col-lg-12 well">
	                        <div class="row">
                                <div class="col-sm-12 ">
                                    <div class="row">
                                    <div class="form-group col-sm-6">
                                        
                                       <div class="form-group">
                                                <asp:Label ID="Label3" runat="server"  Text="Codigo Libro" Style="font-weight:bold"></asp:Label>
                                                <asp:TextBox ID="txtCodLibro" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                     </div>                                                                 
                                    <div class="form-group col-sm-6">
                                        <asp:Label  Text="Descripcion Libro" runat="server" Style="font-weight:bold"></asp:Label>
                                        <asp:TextBox ID="txtLibro" runat="server" class="form-control"></asp:TextBox>
                                    </div> 
                                    </div>   
                                    <div class="form-group">
                                          <asp:Button ID="btnGuardar" runat="server"  Text="Guardar" class="btn btn-success" OnClick="btnGuardar_Click"/>
                                    </div>                                   
                                </div>
                             </div>
                        </div>                      
                 </div>
</asp:Content>
