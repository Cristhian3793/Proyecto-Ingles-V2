<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Site.Master" AutoEventWireup="true" CodeBehind="formpruebasUbicacion.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.pruebasUbicacion" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10.16.5/dist/sweetalert2.all.min.js" type="text/javascript"></script>
    <link href="../Content/sweetalert.css" rel="stylesheet" />
    <style>
        td {
            padding: 10px;
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="width: 85%">
        <h4 class="well" style="background-color: rgb(8, 83, 148); color: white; text-align: center">Ingreso Calificación Prueba de Ubicación
        </h4>
        <div class="col-lg-12 well">
            <div class="row">
                <div class="col-sm-12">
                    <div class="container" style="background: #EAE8E8; padding: 5px; width: 100%; border: solid 1px #DBDADA; border-radius: 5px">
                        <div class="row">
                            <div class="col-sm-2">
                                <asp:Label ID="Label2" runat="server" Text="N° Identificación"></asp:Label>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtCedula" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-sm-2">
                                <asp:Label ID="Label3" runat="server" Text="Período"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="cbxPeriodos" AppendDataBoundItems="true" runat="server" AutoPostBack="true" class="form-control">
                                    <asp:ListItem Text="-Todos-" Value="0" />
                                </asp:DropDownList>
                            </div>
                        </div>

                        <asp:Button ID="Button1" runat="server" Text="Buscar" class="btn btn-success" OnClick="Button1_Click" />
                        <span class="float-right" style="float: right">
                            <asp:LinkButton runat="server" ID="LinkButton1" class="btn" OnClick="LinkButton1_Click" Style="background: #24772E; color: white">
                                Exportar a Excel <i class="fas fa-file-excel"></i>
                            </asp:LinkButton>
                        </span>
                    </div>

                  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <br />
                                <br />
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="dgvNotasPruebas" runat="server" AutoGenerateColumns="False" DataKeyNames="idPrueba,IdInscrito,NivelEstudiante,NotaEnviada" OnRowCancelingEdit="dgvNotasPruebas_RowCancelingEdit" OnRowEditing="dgvNotasPruebas_RowEditing" OnRowUpdating="dgvNotasPruebas_RowUpdating" HorizontalAlign="Center" CssClass="table table-bordered table-striped" OnPageIndexChanging="dgvNotasPruebas_PageIndexChanging" AllowPaging="True" OnRowDataBound="dgvNotasPruebas_RowDataBound" OnRowCommand="dgvNotasPruebas_RowCommand">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <Columns>
                                                <asp:BoundField DataField="NumDocInscrito" HeaderText="N° Identificación" SortExpression="NumDocInscrito" ReadOnly="True" />
                                                <asp:BoundField DataField="IdInscrito" HeaderText="ID" SortExpression="IdInscrito" ReadOnly="True" />
                                                <asp:BoundField DataField="TipoEstudiante" HeaderText="Tipo Estudiante" SortExpression="TipoEstudiante" ReadOnly="True" />
                                                
                                                <asp:BoundField DataField="NombreInscrito" HeaderText="Nombres" SortExpression="NombreInscrito" ReadOnly="True" />
                                                <asp:BoundField DataField="ApellidoInscrito" HeaderText="Apellidos" SortExpression="ApellidoInscrito" ReadOnly="True" />
                                                <asp:BoundField DataField="CeluInscrito" HeaderText="Celular" SortExpression="CeluInscrito" ReadOnly="True" />
                                                <asp:BoundField DataField="TelefInscrito" HeaderText="Telefono" SortExpression="TelefInscrito" ReadOnly="True" />
                                                <asp:BoundField DataField="EmailInscrito" HeaderText="Email" SortExpression="EmailInscrito" ReadOnly="True" />
                                                <asp:BoundField DataField="PeriodoLectivo" HeaderText="Periodo" ReadOnly="True" SortExpression="PeriodoLectivo" />
                                                <asp:BoundField DataField="IdPrueba" HeaderText="IdPrueba" SortExpression="IdPrueba" Visible="False" />
                                                <asp:BoundField DataField="IdNivel" HeaderText="idNivel" ReadOnly="True" SortExpression="IdNivel" Visible="False" />
                                                <asp:BoundField DataField="NomNivel" HeaderText="Nivel" ReadOnly="True" SortExpression="NomNivel" />
                                                <asp:BoundField DataField="Estado" HeaderText="Estado" ReadOnly="True" SortExpression="Estado" />
                                                <asp:BoundField DataField="NivelEstudiante" HeaderText="NivelEstudiante" ReadOnly="True" SortExpression="NivelEstudiante" Visible="false" />
                                                <asp:TemplateField HeaderText="Calificacion" SortExpression="Calificacion">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="EditCalificacion" runat="server" Text='<%# Bind("Calificacion") %>' onkeypress="return NumCheck(event, this)" onpaste="return false"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Calificacion") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="IdTipoEstudiante" HeaderText="IdTipoEstudiante" SortExpression="IdTipoEstudiante" visible="false"/>
                                                <asp:BoundField DataField="NotaEnviada" HeaderText="NotaEnviada" SortExpression="NotaEnviada" visible="false"/>
                                                
                                                <asp:CommandField ButtonType="Button" ShowEditButton="True">
                                                    <ControlStyle BackColor="#085394" BorderColor="Black" BorderStyle="Outset" CssClass="btn btn-sm" ForeColor="White" />
                                                </asp:CommandField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnEnviarNota" runat="server" Text="Enviar" CommandName="EnviarNota"/>
                                                    </ItemTemplate>
                                                    <ControlStyle BackColor="#085394" BorderColor="Black" BorderStyle="Outset" CssClass="btn btn-sm" ForeColor="White" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                            <HeaderStyle BackColor="#085394" Font-Bold="True" ForeColor="#FFFFFF" />
                                            <PagerStyle BackColor="#085394" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                       </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
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
                if (key == 44) {
                    if (field.value == "") return false
                    regexp = /^[0-9]+$/
                    return regexp.test(field.value)
                }
                // other key
                return false
            }
            function enviadonota() {

                Swal.fire({
                    icon: 'success',
                    title: 'correcto',
                    text: 'Nota Enviada Correctamente',
                    footer: '<a href></a>'
                })
            }
            function rechazadonota() {

                Swal.fire({
                    icon: 'error',
                    title: 'error',
                    text: 'No se puedo enviar nota',
                    footer: '<a href></a>'
                })
            }


            function rechazado() {

                Swal.fire({
                    icon: 'error',
                    title: 'error',
                    text: 'No se puede registrar nota, la prueba de ubicación aun no ha sido pagada',
                    footer: '<a href></a>'
                })
            }
            function errorcalificacion() {

                Swal.fire({
                    icon: 'error',
                    title: 'error',
                    text: 'No se puede registrar nota, la nota de ubicacion debe ser entre 0 y 10',
                    footer: '<a href></a>'
                })
            }
            function pruabaPagada() {

                Swal.fire({
                    icon: 'error',
                    title: 'error',
                    text: 'El nivel generado por la la prueba de ubicación ya fue pagado, no se puede modificar la prueba de ubicación',
                    footer: '<a href></a>'
                })
            }
        </script>
    </div>
</asp:Content>
