<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Interfaces/Site.Master" CodeBehind="formConsultaNivelEquivalencia.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.formConsultaNivelEquivalencia" async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Consulta Nivel Autonomo</title>
<script src="https://cdn.jsdelivr.net/npm/sweetalert2@10.16.5/dist/sweetalert2.all.min.js" type="text/javascript"></script>
<link href="../Content/sweetalert.css" rel="stylesheet"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
                 <div class="container" style=" padding-left:10%;padding-right:10%;">
                     <h4 class="well" style="background-color:rgb(8, 83, 148);color:white;text-align:center">Consulta Niveles Equivalentes</h4>
	                    <div class="col-lg-12 well">
	                    <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                <asp:Label ID="Label2" runat="server" Text="Cod Equivalencia"></asp:Label>
                                    <asp:TextBox ID="txtCodEquivalencia" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnConsultar" runat="server" OnClick="btnConsultar_Click" Text="Consultar"  class="btn btn-success"/>
                                </div>                       
                            <div class="row" >
                                <div class="col-md-12" >
                                    <div class="table-responsive" > 
                                    <asp:GridView ID="dgvNivelEquivalencia" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" OnRowCommand="dgvNivelEquivalencia_RowCommand" DataKeyNames="IdEquivalenciaNivel,IdNivelAutonomo,IdNIvelProgramado" OnRowDeleting="dgvNivelEquivalencia_RowDeleting" CssClass="table table-bordered table-striped">
                                        <Columns>
                                            <asp:BoundField DataField="IdEquivalenciaNivel" HeaderText="ID NIVEL EQUIVALENTE" SortExpression="IdEquivalenciaNivel" />
                                            <asp:BoundField DataField="IdNivelAutonomo" HeaderText="ID NIVEL AUTONOMO" SortExpression="IdNivelAutonomo" visible="false"/>
                                            <asp:BoundField DataField="IdNIvelProgramado" HeaderText="ID NIVEL PROGRAMADO" SortExpression="IdNIvelProgramado" visible="false"/>
                                            <asp:BoundField DataField="NomNivelProgramado" HeaderText="NIVEL PROGRAMADO" SortExpression="NomNivelProgramado" />
                                            <asp:BoundField DataField="NomNivelAutonomo" HeaderText="NIVEL AUTONOMO" SortExpression="NomNivelAutonomo" />
                                            <asp:ButtonField ButtonType="Button" Text="Editar" CommandName="Editar"/>
                                            <asp:TemplateField HeaderText="acciones">                                          
                                            <ItemTemplate>
                                            <asp:Button CommandName="Delete" runat="server" Text="Eliminar" OnClientClick="return  confirmdelete(this);"/>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                                        <HeaderStyle BackColor="#085394" Font-Bold="True" ForeColor="#CCCCFF" />
                                                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                                        <RowStyle BackColor="White" ForeColor="#003399" />
                                                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                        <SortedDescendingHeaderStyle BackColor="#002876" />
                                    </asp:GridView>
                                </div>
                        </div>
                    </div>
                </div>
          
        </div>
        </div>
     </div>
    <asp:HiddenField id="idNivEqui" runat="server"/>
    <!--inicio modal para actualizaciones-->
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
<!---botton abir modal-->
  <ajaxToolkit:ModalPopupExtender ID="btnPopUp_ModalPopupExtender" runat="server" Enabled="True" TargetControlID="btnPopUp" 
                BackgroundCssClass="modalBackground" PopupControlID="PanelModal">
    </ajaxToolkit:ModalPopupExtender>
<!--fin boton abrir modal-->
            <asp:Panel ID="PanelModal" runat="server" style="display:none; background:white; width:40%; height:auto;border:solid 1px black" >
              <div class="modal-header">
                <%--<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>--%>                
              </div>
              <div class="modal-body">
                    <div class="container-fluid well">
                        <div class="row-fluid">
                            <div class="row">
                            <div class="span4 ">
                                  <div class="form-group col-sm-6">
                                    <div class="controls">
                                    <label class="control-label" ><strong>Nivel Programado :</strong></label>
                                    <asp:DropDownList ID="cbxNivelesProgramados"  runat="server" class="form-control">
                                   </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group col-sm-6">                                            
                                    <label class="control-label" ><strong>Nivel Autonomo</strong></label>
                                    <div class="controls">
                                   <asp:DropDownList ID="cbxNivelesAutonomos"  runat="server" class="form-control">
                                   </asp:DropDownList>
                                    </div>
                                </div>

                            </div>
                                </div>
                        </div>
                    </div>                
              </div>
              <div class="modal-footer">
                <button class="btn" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                <asp:Button class="btn btn-primary" ID="btnactualizar"   runat="server" Text="Guardar Cambios" ispostback="true" OnClick="btnActualizar_Click"/>
              </div>
            </asp:Panel>
             <asp:Button ID="btnPopUp" runat="server" Height="47px" Text="MOSTRAR POPUP" 
                Width="258px" hidden="hidden"/>
            </ContentTemplate>
</asp:UpdatePanel>
    <!--fin modal--> 
  <script>
            var object = { status: false, ele: null };
            function confirmdelete(ev) {
                
                if (object.status) { return true;}
                Swal.fire({
                    title: 'Está Seguro de Eliminar el Registro?',
                    text: "Usted no sera capaz de revertir esta acción!",
                    icon: 'Precaucion',
                    showCancelButton: true,
                    confirmButtonColor: '#29CB12',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'SI',
                    cancelButtonText: 'NO'
                }).then((result) => {
                    if (result.isConfirmed) {
                        object.status = true;
                        object.ele = ev;
                        object.ele.click();
                    }
                })
                return false;
            }
 </script>
</asp:Content>

