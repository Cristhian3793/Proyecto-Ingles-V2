<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Site.Master" AutoEventWireup="true" CodeBehind="formPreguntasRespuestas.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.formPreguntasRespuestas" async="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8">
             <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10.16.5/dist/sweetalert2.all.min.js" type="text/javascript"></script>
    <link href="../Content/sweetalert.css" rel="stylesheet"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
                     <div class="container" style=" padding-left:10%;padding-right:10%;">
                     <h4 class="well" style="background-color:rgb(8, 83, 148);color:white;text-align:center">Preguntas y Respuestas</h4>
	                    <div class="col-lg-12 well">
	                        <div class="row">
                                <div class="col-sm-12 ">

                                    <div class="row">
                                    <div class="form-group col-sm-6">
                                        <asp:Label  Text="Pregunta" runat="server"></asp:Label>
                                        <asp:TextBox ID="txtpregunta" runat="server" class="form-control"></asp:TextBox>
                                     </div>
                                    <div class="form-group col-sm-6">
                                        <asp:Label  Text="Respuesta" runat="server"></asp:Label>
                                        <asp:TextBox ID="txtRespuesta" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    </div>
                                    <asp:Button id="btnGuardar" runat="server" text="Guardar" OnClick="btnGuardar_Click" CssClass="btn btn-success" />
                                    </div>                                   
                                </div>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="IDPREGUNTA" CssClass="table table-bordered table-striped" OnRowCommand="GridView1_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="IDPREGUNTA" HeaderText="Id" SortExpression="IDPREGUNTA"/>
                                    <asp:BoundField DataField="PREGUNTA" HeaderText="PREGUNTA" SortExpression="PREGUNTA"/>
                                    <asp:BoundField DataField="RESPUESTA" HeaderText="RESPUESTA" SortExpression="RESPUESTA"/>
                                    <asp:ButtonField ButtonType="Button" Text="Editar" CommandName="Editar" AccessibleHeaderText="Editar" />
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
    <!--inicio modal para info-->
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
        <!---botton abir modal-->
    <ajaxToolkit:ModalPopupExtender ID="btnPopUp_ModalPopupExtender2" runat="server"  Enabled="True" TargetControlID="Button2" 
                BackgroundCssClass="modalBackground" PopupControlID="PanelModal2">
    </ajaxToolkit:ModalPopupExtender>
        <!--fin boton abrir modal-->
            <asp:Panel ID="PanelModal2" runat="server" style="display:none; background:white; width:40%; height:auto;border:solid 1px black" >
                <div class="modal-header">
                <%--<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>--%>                
              </div>
              <div class="modal-body">
                    <div class="container-fluid well">
                        <div class="row-fluid">
                            <div class="span4"> 

                                 <div class="form-group col-sm-12">   
                                    <asp:HiddenField ID="idPre" runat="server" />
                                    <label class="control-label" ><strong>Pregunta:</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtPre" runat="server" CssClass="form-control" 
                                            placeholder="Pregunta" TextMode="MultiLine" ></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group col-sm-12">                                            
                                    <label class="control-label" ><strong>Respuesta:</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtResp" runat="server" CssClass="form-control" 
                                            placeholder="Respuesta" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>                
              </div>
              <div class="modal-footer">
                <button class="btn" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                <asp:Button class="btn btn-primary" ID="Button1"  runat="server" Text="Guardar"  OnClick="Button1_Click" OnClientClick="refreshGrid();"/>
              </div>
            </asp:Panel>
             <asp:Button ID="Button2" runat="server" Height="47px" Text="MOSTRAR POPUP" 
                Width="258px" hidden="hidden"/>           
            </ContentTemplate>
        
</asp:UpdatePanel>
    <!--fin modal--> 





      <script type="text/javascript">

          function refreshGrid() {
              opener.document.location.reload(true);
          }
          


       function confirm() {
           Swal.fire({
               icon: 'success',
               title: 'OK',
               text: 'El Registro se Guardo Corectamente!',
               footer: '<a href></a>'
           })
       }
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
