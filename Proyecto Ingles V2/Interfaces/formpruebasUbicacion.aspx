<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Site.Master" AutoEventWireup="true" CodeBehind="formpruebasUbicacion.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.pruebasUbicacion" async="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
             <div class="container" style="width:85%">
                     <h4 class="well" style="background-color:rgb(8, 83, 148);color:white;text-align:center">Ingreso Calificación Prueba de Ubicación
                     </h4>
	                    <div class="col-lg-12 well">
	                        <div class="row">
                                <div class="col-sm-12">
                                   <span class="float-left">
                                    <asp:Label ID="Label2" runat="server" Text="Cédula Inscrito"></asp:Label>                                                   
                                    <asp:TextBox ID="txtCedula" runat="server"></asp:TextBox>         
                                    <asp:Button ID="Button1" runat="server" Text="Buscar" class="btn btn-success" OnClick="Button1_Click" />
                                    </span>
                                    <div class="col-xs-2">
             
                                    <div class="form-group">
                                     <asp:DropDownList ID="cbxSearchNiveles" AppendDataBoundItems="true" runat="server" AutoPostBack="true" class="form-control">
                                     <asp:ListItem Text="-Todos-" Value="0" />
                                     </asp:DropDownList>
                                    </div>
                                 </div>
                        <span class="float-right" style="float:right">
                        <asp:Button runat="server" Text="Exportar a Excel" ID="btnExccel" OnClick="btnExccel_Click"/>
                            </span>
                                </div>
                             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                            <div class="row" >
                                <br />
                                <br />
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                   <asp:GridView ID="dgvNotasPruebas" runat="server"   BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False" DataKeyNames="idPrueba,IdInscrito" OnRowCancelingEdit="dgvNotasPruebas_RowCancelingEdit" OnRowEditing="dgvNotasPruebas_RowEditing" OnRowUpdating="dgvNotasPruebas_RowUpdating" HorizontalAlign="Center" >
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
                                           <asp:BoundField DataField="IdPrueba" HeaderText="IdPrueba" ReadOnly="True" SortExpression="IdPrueba" Visible="False" />
                                           <asp:BoundField DataField="IdNivel" HeaderText="idNivel" ReadOnly="True" SortExpression="IdNivel" Visible="False"/>
                                           <asp:BoundField DataField="NomNivel" HeaderText="Nivel" ReadOnly="True" SortExpression="NomNivel" />
                                           <asp:TemplateField HeaderText="Calificacion" SortExpression="Calificacion">
                                               <EditItemTemplate>
                                                  <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="ErrorA" ControlToValidate="EditCalificacion" >ErrorB</asp:RequiredFieldValidator>--%>
                                                  <%--<asp:RegularExpressionValidator ID="RegexDecimal" runat="server" ValidationExpression="((\d+)((\.\d{1,2})?))$" ErrorMessage="Ingrese un monto decimal" ControlToValidate="EditCalificacion" />--%>
                                                   <asp:TextBox ID="EditCalificacion" runat="server" Text='<%# Bind("Calificacion") %>' onkeypress="return NumCheck(event, this)"></asp:TextBox>
                                               </EditItemTemplate>
                                               <ItemTemplate>               
                                                   <asp:Label ID="Label1" runat="server" Text='<%# Bind("Calificacion") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           <asp:CommandField ButtonType="Button" ShowEditButton="True" />
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

                                <asp:GridView ID="GridView1" runat="server">
                              </asp:GridView>

                                </ContentTemplate>
                               </asp:UpdatePanel>                       
                         </div>             
                 </div>
              </div>
<script type="text/javascript">
function NumCheck(e, field) {
  key = e.keyCode ? e.keyCode : e.which
  // backspace
  if (key == 8) return true
  // 0-9
  if (key > 47 && key < 58) {
    if (field.value == "") return true
      regexp = /.[0-9]{2}$/
    return !(regexp.test(field.value))
  }
  // .
  if (key ==46) {
    if (field.value == "") return false
    regexp = /^[0-9]+$/
    return regexp.test(field.value)
  }
  // other key
  return false
}
</script>
</asp:Content>
