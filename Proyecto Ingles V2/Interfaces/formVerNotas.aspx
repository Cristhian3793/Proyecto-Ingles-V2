<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Site.Master" AutoEventWireup="true" CodeBehind="formVerNotas.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.formVerNotas" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-3.4.1.js" type="text/javascript"></script>
    <script src="../Scripts/DataTables/jquery.dataTables.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jeditable.js/2.0.19/jquery.jeditable.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="width:85%">
<h4 class="well" style="background-color:rgb(8, 83, 148);color:white;text-align:center">Consulta Notas</h4>
        
	<div class="col-lg-12 well">
	<div class="row">
        <div class="col-sm-12">

            <span class="float-left">
             <asp:Label ID="Label1" runat="server" Text=" Número Documento:"></asp:Label>
            <asp:TextBox ID="txtCedula" runat="server" Width="179px" ></asp:TextBox>
            <asp:Button ID="btnConsultar" runat="server"  Text="Consultar" class="btn btn-success"/>          
            </span>
            <br />
            <br />
             <div class="row">
            <div class="col-xs-1">
                <div class="form-group">
                    <asp:Label ID="Label3" runat="server" Text="Período"></asp:Label>
                 </div>
            </div>
                <div class="col-xs-2">
                    <div class="form-group">
                    <asp:DropDownList ID="cbxPeriodo" AppendDataBoundItems="true" runat="server" AutoPostBack="true" class="form-control"  >
                    <asp:ListItem Text="-Todos-" Value="0" />
                    </asp:DropDownList>
                    </div>
                </div>
             </div>
            <span class="float-right" style="float:right">
            <asp:Button runat="server" Text="Exportar a Excel" ID="btnExccel" />
                </span>
              
         <div class="row">
            <br />
           <div class="col-md-12">
            <div class="table-responsive">
            <asp:GridView ID="dgvInscrito" runat="server"  BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False" DataKeyNames="IdInscrito"  CssClass="table table-bordered table-striped"  EnablePersistedSelection="True" OnRowCommand="dgvInscrito_RowCommand" >
                <Columns>
                                  <asp:BoundField DataField="NumDocInscrito" HeaderText="Identificacion" SortExpression="NumDocInscrito" ReadOnly="True" />
                                  <asp:BoundField DataField="IdInscrito" HeaderText="ID" SortExpression="IdInscrito" ReadOnly="True" />
                                  <asp:BoundField DataField="TipoEstudiante" HeaderText="Tipo Estudiante" SortExpression="TipoEstudiante" ReadOnly="True" />
                                  <asp:BoundField DataField="NombreInscrito" HeaderText="Nombres" SortExpression="NombreInscrito" ReadOnly="True" />
                                  <asp:BoundField DataField="ApellidoInscrito" HeaderText="Apellidos" SortExpression="ApellidoInscrito" ReadOnly="True" />
                                  <asp:BoundField DataField="CeluInscrito" HeaderText="Celular" SortExpression="CeluInscrito" ReadOnly="True" />
                                  <asp:BoundField DataField="TelefInscrito" HeaderText="Telefono" SortExpression="TelefInscrito" ReadOnly="True" />
                                  <asp:BoundField DataField="EmailInscrito" HeaderText="Email" SortExpression="EmailInscrito" ReadOnly="True" />
                                  <asp:BoundField DataField="PeriodoLectivo" HeaderText="PeriodoInscripcion" ReadOnly="True" SortExpression="PeriodoLectivo"/>
                                  <%--<asp:BoundField DataField="IdPrueba" HeaderText="IdPrueba"  SortExpression="IdPrueba" Visible="False"/>--%>
                                  <asp:BoundField DataField="IdNivel" HeaderText="idNivel"  SortExpression="IdNivel" />
                                  <asp:BoundField DataField="NomNivel" HeaderText="Nivel" ReadOnly="True" SortExpression="NomNivel" />
                                  <asp:BoundField DataField="Estado" HeaderText="Estado" ReadOnly="True" SortExpression="Estado" />
<%--                              <asp:TemplateField HeaderText="Calificacion" SortExpression="Calificacion">
                                  <EditItemTemplate>
                                  <asp:TextBox ID="EditCalificacion" runat="server" Text='<%# Bind("Calificacion") %>' onkeypress="return NumCheck(event, this)"></asp:TextBox>
                               </EditItemTemplate>
                            <ItemTemplate>               
                           <asp:Label ID="Label1" runat="server" Text='<%# Bind("Calificacion") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>--%>
                       <%--<asp:CommandField ButtonType="Button" ShowEditButton="True" />--%>
                    <asp:ButtonField ButtonType="Button" Text="CrearNotas" CommandName="CrearNotas" AccessibleHeaderText="CrearNotas" />
                    <asp:ButtonField ButtonType="Button" Text="Notas" CommandName="VerNotas"/>
                    
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#085394" Font-Bold="True" ForeColor="#FFFFFF" />
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

     <!--inicio modal para info-->
        <!---botton abir modal-->
    <ajaxToolkit:ModalPopupExtender ID="btnPopUp_ModalPopupExtender2" runat="server"  Enabled="True" TargetControlID="Button2" 
                BackgroundCssClass="modalBackground" PopupControlID="PanelModal2">
    </ajaxToolkit:ModalPopupExtender>
        <!--fin boton abrir modal-->
         <asp:Panel ID="PanelModal2" runat="server" style="display:none; background:white; width:75%; height:auto;border:solid 1px black" >
                <div class="modal-header">       
              </div>
              <div class="modal-body">
                    <div class="container-fluid well">
                        <div class="row-fluid">
                            <div class="span4"> 

                                 <div class="form-group col-sm-12">
                                     <label class="control-label"><strong>Alumno</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control" 
                                            placeholder="Nombres" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <label class="control-label" ><strong>Nivel</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtnivel" runat="server" CssClass="form-control" 
                                            placeholder="nivel" ReadOnly="true"></asp:TextBox>
                                    </div>
                                     <asp:HiddenField ID="HiddenIdIns" runat="server" />
                                     <asp:HiddenField ID="HiddenNivel" runat="server" />
                                </div>
               <div class="col-md-12">
                   <div class="table-responsive">
                       <div class="form-group col-sm-12">
                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                         <ContentTemplate>             
                           <asp:Panel ID="Panel1" runat="server">

                                <asp:GridView ID="dgvNotas" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered table-striped"  DataKeyNames="IDNota" OnRowUpdating="dgvNotas_RowUpdating" OnRowCancelingEdit="dgvNotas_RowCancelingEdit" OnRowEditing="dgvNotas_RowEditing">
                                    <Columns>
                                            <asp:BoundField DataField="IDNota" HeaderText="id" SortExpression="IDNota" readonly="true"/>                                         
                                            <asp:BoundField DataField="NomUnidad" HeaderText="Tema de Unidad" SortExpression="NomUnidad" readonly="true"/>

                                            <asp:TemplateField HeaderText="Unit1" SortExpression="Unit1">
                                               <EditItemTemplate>
                                                <asp:TextBox ID="EditUnit1" runat="server" Text='<%# Bind("Unit1") %>' onkeypress="return NumCheck(event, this)"></asp:TextBox>
                                               </EditItemTemplate>
                                               <ItemTemplate>               
                                                   <asp:Label ID="Label1" runat="server" Text='<%# Bind("Unit1") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Done1" SortExpression="Done1">
                                               <EditItemTemplate>
                                                <asp:TextBox ID="EditDone1" runat="server" Text='<%# Bind("Done1") %>' onkeypress="return NumCheck(event, this)"></asp:TextBox>
                                               </EditItemTemplate>
                                               <ItemTemplate>               
                                                   <asp:Label ID="Label1" runat="server" Text='<%# Bind("Done1") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>

                                           <asp:TemplateField HeaderText="Unit2" SortExpression="Unit2">
                                               <EditItemTemplate>
                                                <asp:TextBox ID="EditUnit2" runat="server" Text='<%# Bind("Unit2") %>' onkeypress="return NumCheck(event, this)"></asp:TextBox>
                                               </EditItemTemplate>
                                               <ItemTemplate>               
                                                   <asp:Label ID="Label1" runat="server" Text='<%# Bind("Unit2") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Done2" SortExpression="Done2">
                                               <EditItemTemplate>
                                                <asp:TextBox ID="EditDone2" runat="server" Text='<%# Bind("Done2") %>' onkeypress="return NumCheck(event, this)"></asp:TextBox>
                                               </EditItemTemplate>
                                               <ItemTemplate>               
                                                   <asp:Label ID="Label1" runat="server" Text='<%# Bind("Done2") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Unit3" SortExpression="Unit4">
                                               <EditItemTemplate>
                                                <asp:TextBox ID="EditUnit3" runat="server" Text='<%# Bind("Unit3") %>' onkeypress="return NumCheck(event, this)"></asp:TextBox>
                                               </EditItemTemplate>
                                               <ItemTemplate>               
                                                   <asp:Label ID="Label1" runat="server" Text='<%# Bind("Unit3") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Done3" SortExpression="Done3">
                                               <EditItemTemplate>
                                                <asp:TextBox ID="EditDone3" runat="server" Text='<%# Bind("Done3") %>' onkeypress="return NumCheck(event, this)"></asp:TextBox>
                                               </EditItemTemplate>
                                               <ItemTemplate>               
                                                   <asp:Label ID="Label1" runat="server" Text='<%# Bind("Done3") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Ckeckpoint" SortExpression="Ckeckpoint">
                                               <EditItemTemplate>
                                                <asp:TextBox ID="EditCkeckpoint" runat="server" Text='<%# Bind("Ckeckpoint") %>' onkeypress="return NumCheck(event, this)"></asp:TextBox>
                                               </EditItemTemplate>
                                               <ItemTemplate>               
                                                   <asp:Label ID="Label1" runat="server" Text='<%# Bind("Ckeckpoint") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Unit4" SortExpression="Unit4">
                                               <EditItemTemplate>
                                                <asp:TextBox ID="EditUnit4" runat="server" Text='<%# Bind("Unit4") %>' onkeypress="return NumCheck(event, this)"></asp:TextBox>
                                               </EditItemTemplate>
                                               <ItemTemplate>               
                                                   <asp:Label ID="Label1" runat="server" Text='<%# Bind("Unit4") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Done4" SortExpression="Done4">
                                               <EditItemTemplate>
                                                <asp:TextBox ID="EditDone4" runat="server" Text='<%# Bind("Done4") %>' onkeypress="return NumCheck(event, this)"></asp:TextBox>
                                               </EditItemTemplate>
                                               <ItemTemplate>               
                                                   <asp:Label ID="Label1" runat="server" Text='<%# Bind("Done4") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Unit5" SortExpression="Unit5">
                                               <EditItemTemplate>
                                                <asp:TextBox ID="EditUnit5" runat="server" Text='<%# Bind("Unit5") %>' onkeypress="return NumCheck(event, this)"></asp:TextBox>
                                               </EditItemTemplate>
                                               <ItemTemplate>               
                                                   <asp:Label ID="Label1" runat="server" Text='<%# Bind("Unit5") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Done5" SortExpression="Done6">
                                               <EditItemTemplate>
                                                <asp:TextBox ID="EditDone5" runat="server" Text='<%# Bind("Done5") %>' onkeypress="return NumCheck(event, this)"></asp:TextBox>
                                               </EditItemTemplate>
                                               <ItemTemplate>               
                                                   <asp:Label ID="Label1" runat="server" Text='<%# Bind("Done5") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Unit6" SortExpression="Unit6">
                                               <EditItemTemplate>
                                                <asp:TextBox ID="EditUnit6" runat="server" Text='<%# Bind("Unit6") %>' onkeypress="return NumCheck(event, this)"></asp:TextBox>
                                               </EditItemTemplate>
                                               <ItemTemplate>               
                                                   <asp:Label ID="Label1" runat="server" Text='<%# Bind("Unit6") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Done6" SortExpression="Done6">
                                               <EditItemTemplate>
                                                <asp:TextBox ID="EditDone6" runat="server" Text='<%# Bind("Done6") %>' onkeypress="return NumCheck(event, this)"></asp:TextBox>
                                               </EditItemTemplate>
                                               <ItemTemplate>               
                                                   <asp:Label ID="Label1" runat="server" Text='<%# Bind("Done6") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Estado" SortExpression="Estado">
                                               <EditItemTemplate>
                                                <asp:TextBox ID="EditEstado" runat="server" Text='<%# Bind("Estado") %>' onkeypress="return NumCheck(event, this)"></asp:TextBox>
                                               </EditItemTemplate>
                                               <ItemTemplate>               
                                                   <asp:Label ID="Label1" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>   
                                            <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                                    </Columns>
                                         <FooterStyle BackColor="#99CCCC" 
                                         ForeColor="#003399" />
                                        <HeaderStyle BackColor="#085394" Font-Bold="True" ForeColor="#CCCCFF" />
                                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                        <RowStyle BackColor="White" ForeColor="#003399" />
                                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                        <SortedDescendingHeaderStyle BackColor="#002876" />
                                </asp:GridView>
                                </asp:Panel>
                                       </ContentTemplate>
                                </asp:UpdatePanel>
     
                                </div>
                                </div>
                            </div>
                            </div>
                        </div>                   
                    </div>                
              </div>
              <div class="modal-footer">
                <button class="btn" data-dismiss="modal" aria-hidden="true" >Cancelar</button>
              </div>
             
            </asp:Panel>
             <asp:Button ID="Button2" runat="server" Height="47px" Text="MOSTRAR POPUP" 
                Width="258px" hidden="hidden"/>           
</asp:Content>
