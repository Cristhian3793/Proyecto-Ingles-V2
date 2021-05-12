<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Site.Master" AutoEventWireup="true" CodeBehind="formConsultaLicenciasCambridge.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.formConsultaLicenciasCambridge" async="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10.16.5/dist/sweetalert2.all.min.js" type="text/javascript"></script>
    <link href="../Content/sweetalert.css" rel="stylesheet"/>
    <style>
.switch {
  position: relative;
  display: inline-block;
  width: 50px;
  height: 20px;
}

.switch input { 
  opacity: 0;
  width: 0;
  height: 0;
}

.slider {
  position: absolute;
  cursor: pointer;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: #ccc;
  -webkit-transition: .4s;
  transition: .4s;
}
.slider:before {
  position: absolute;
  content: "";
  height: 12px;
  width: 12px;
  left: 4px;
  bottom: 4px;
  background-color: white;
  -webkit-transition: .4s;
  transition: .4s;
}

input:checked + .slider {
  background-color: #2196F3;
}

input:focus + .slider {
  box-shadow: 0 0 1px #2196F3;
}

input:checked + .slider:before {
  -webkit-transform: translateX(26px);
  -ms-transform: translateX(26px);
  transform: translateX(26px);
}
/* Rounded sliders */
.slider.round {
  border-radius: 34px;
}

.slider.round:before {
  border-radius: 50%;
}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

                 <div class="container" style="width:50%">
                     <h4 class="well" style="background-color:rgb(8, 83, 148);color:white;text-align:center">Consulta de Licencias</h4>
	                    <div class="col-lg-12 well">
	                        <div class="row">
                                <div class="col-sm-12">
                                <div class="form-group ">                                
                                <asp:Label ID="Label2" runat="server" Text="Libro"></asp:Label>                                                     
                                    <asp:TextBox ID="txtLibro" runat="server"></asp:TextBox>         

                                    <asp:Button ID="Button1" runat="server" Text="Buscar" class="btn btn-success" OnClick="Button1_Click"/>
                                     </div>
                                </div>                           
 
                            <div class="row" >
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                   <asp:GridView ID="dgvLicencias" runat="server"   BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False" DataKeyNames="IdLicencia" OnRowDataBound="dgvLicencias_RowDataBound" HorizontalAlign="Center" OnRowDeleting="dgvLicencias_RowDeleting" OnRowCommand="dgvLicencias_RowCommand" CssClass="table table-bordered table-striped">
                                       <Columns>
                                           <asp:BoundField DataField="IdLicencia" HeaderText="ID" SortExpression="IdLicencia" ReadOnly="True" />                                          
                                           <asp:BoundField DataField="Licencia" HeaderText="LICENCIA" SortExpression="Licencia" ReadOnly="True" />
                                           <asp:BoundField DataField="EstadoLicencia" HeaderText="ESTADO" SortExpression="EstadoLicencia" ReadOnly="True" />
                                           <asp:BoundField DataField="IdLibro" HeaderText="ID LIBRO" SortExpression="IdLibro" ReadOnly="True" visible="false"/>
                                           <asp:BoundField DataField="codLibro" HeaderText="COD LIBRO" SortExpression="codLibro" ReadOnly="True"/>
                                           <asp:BoundField DataField="Libro" HeaderText="LIBRO" SortExpression="Libro" ReadOnly="True" />                                                                                
                                           <asp:ButtonField ButtonType="Button" Text="Editar" CommandName="Editar"/>
                                             <asp:TemplateField HeaderText="acciones">
                                            <ItemTemplate>
                                            <asp:Button CommandName="Delete" runat="server" Text="Eliminar" OnClientClick="return  confirmdelete(this);"/>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                       </Columns>
                                                       <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                                        <HeaderStyle BackColor="#085394" Font-Bold="True" ForeColor="#FFFFFF" HorizontalAlign="Center"/>
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
    <!--inicio modal para actualizaciones-->
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                            <div class="span4 ">
                                <div class="row-fluid">
                                <div class="form-group col-sm-6">                                            
                                    <label class="control-label" ><strong>Libro</strong></label>
                                    
                                    <asp:DropDownList ID="cbxLibros"  runat="server"  class="form-control" autopostback="true">
                                    <%--<asp:ListItem Text="-seleccionar-" Value="0" />--%>
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="idLIbro" runat="server"/>
                                </div>
                                <div class="form-group col-sm-6">                                            
                                    <label class="control-label" ><strong>Licencia :</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtLicencia" runat="server" CssClass="form-control" 
                                            placeholder="licencia" TabIndex="4" ValidationGroup="val4"></asp:TextBox>
                                    </div>
                                </div>
                                </div>
                                <div class="form-group col-sm-6">                                            
                                           <div class="controls">
                                            <asp:Label ID="Label1" runat="server"  Text="Estado :" Style="font-weight:bold"></asp:Label>
                                           <label class="switch">
                                           <input type="checkbox" runat="server" id="RabActivo"/>
                                          <span class="slider round"></span>
                                          </label>  
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>                
              </div>
              <div class="modal-footer">
                <button class="btn" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                <asp:Button class="btn btn-primary" ID="btnactualizar"   runat="server" Text="Guardar Cambios" ispostback="true" />
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

                             if (object.status) { return true; }
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
