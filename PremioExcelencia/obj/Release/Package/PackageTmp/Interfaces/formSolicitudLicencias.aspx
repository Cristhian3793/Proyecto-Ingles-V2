<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Site.Master" AutoEventWireup="true" CodeBehind="formSolicitudLicencias.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.formSolicitudLicencias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
         <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10.16.5/dist/sweetalert2.all.min.js" type="text/javascript"></script>
    <link href="../Content/sweetalert.css" rel="stylesheet"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
                   <div class="container" style=" padding-left:10%;padding-right:10%;">
                     <h4 class="well" style="background-color:rgb(8, 83, 148);color:white;text-align:center">Solicitud Licencias Cambridge</h4>
	                    <div class="col-lg-12 well">
	                        <div class="row">
                                <div class="col-sm-12 ">
                                    <div class="row">
                                    <div class="form-group col-sm-6">                                        
                                       <div class="form-group">
                                                <asp:Label ID="Label3" runat="server"  Text="Email" Style="font-weight:bold"></asp:Label>
                                                <asp:TextBox ID="txtEmail" runat="server" class="form-control" type="email"></asp:TextBox>
                                        </div>
                                     </div> 
                                     </div> 
                                    <div class="form-group">
                                        <asp:Label  Text="Informacion" runat="server" Style="font-weight:bold"></asp:Label>
                                        <asp:TextBox ID="txtInformacion" runat="server" class="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                    </div>      
                                    <div class="form-group">
                                          <asp:Button ID="btnGuardar" runat="server"  Text="Enviar" class="btn btn-success" OnClick="btnGuardar_Click" />
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
               text: 'Informacion Enviada Correctamente',
               footer: '<a href></a>'
           })
       }

      </script>
</asp:Content>
