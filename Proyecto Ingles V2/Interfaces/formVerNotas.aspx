<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Site.Master" AutoEventWireup="true" CodeBehind="formVerNotas.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.formVerNotas" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-3.4.1.js" type="text/javascript"></script>
    <script src="../Scripts/DataTables/jquery.dataTables.js" type="text/javascript"></script>
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jeditable.js/2.0.19/jquery.jeditable.min.js" type="text/javascript"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10.16.5/dist/sweetalert2.all.min.js" type="text/javascript"></script>
    <link href="../Content/sweetalert.css" rel="stylesheet" />
    <style>
        .swal2-container {
            z-index: 15000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="width: 85%">
        <h4 class="well" style="background-color: rgb(8, 83, 148); color: white; text-align: center">Ingreso Notas</h4>

        <div class="col-lg-12 well">
            <div class="row">
                <div class="col-sm-12">
                    <div class="container" style="background: #EAE8E8; padding: 5px; width: 100%; border: solid 1px #DBDADA; border-radius: 5px">
                        <div class="row">
                            <div class="col-sm-2">
                                <asp:Label ID="Label1" runat="server" Text=" Número Documento:"></asp:Label>
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
                                <asp:DropDownList ID="cbxPeriodo" AppendDataBoundItems="true" runat="server" AutoPostBack="true" class="form-control">
                                    <asp:ListItem Text="-Todos-" Value="0" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-2">
                                <asp:Label ID="Label2" runat="server" Text="Nivel"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="cbxNiveles" AppendDataBoundItems="true" runat="server" AutoPostBack="true" class="form-control">
                                    <asp:ListItem Text="-Todos-" Value="0" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" class="btn btn-success" OnClick="btnConsultar_Click"/>

                        <span class="float-right" style="float: right">
                            <asp:LinkButton runat="server" ID="btnSubmit" class="btn" OnClick="btnSubmit_Click" Style="background: #24772E; color: white">
                                Exportar a Excel <i class="fas fa-file-excel"></i>
                            </asp:LinkButton>
                        </span>
                    </div>


                    <div class="row">
                        <br />
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="dgvInscrito" runat="server" AutoGenerateColumns="False" DataKeyNames="IdInscrito,IdNivelInscrito,IdNivel" CssClass="table table-bordered table-striped" EnablePersistedSelection="True" OnRowCommand="dgvInscrito_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="true" PageSize="10" OnPageIndexChanging="dgvInscrito_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdNivelInscrito" HeaderText="IdNivelInscrito" SortExpression="IdNivelInscrito" Visible="false" />
                                        <asp:BoundField DataField="IdNivel" HeaderText="IdNivel" SortExpression="IdNivel" Visible="false" />
                                        <asp:BoundField DataField="NumDocInscrito" HeaderText="N° Identificación" SortExpression="NumDocInscrito" ReadOnly="True" />
                                        <asp:BoundField DataField="IdInscrito" HeaderText="Cod Estudiante" SortExpression="IdInscrito" ReadOnly="True" />
                                        <asp:BoundField DataField="TipoEstudiante" HeaderText="Tipo Estudiante" SortExpression="TipoEstudiante" ReadOnly="True" />
                                        <asp:BoundField DataField="NombreInscrito" HeaderText="Nombres" SortExpression="NombreInscrito" ReadOnly="True" />
                                        <asp:BoundField DataField="ApellidoInscrito" HeaderText="Apellidos" SortExpression="ApellidoInscrito" ReadOnly="True" />
                                        <asp:BoundField DataField="Periodo" HeaderText="Periodo" ReadOnly="True" SortExpression="Periodo" />
                                        <%--<asp:BoundField DataField="IdPrueba" HeaderText="IdPrueba"  SortExpression="IdPrueba" Visible="False"/>--%>

                                        <asp:BoundField DataField="NomNivel" HeaderText="Nivel" ReadOnly="True" SortExpression="NomNivel" />
                                        <%--<asp:BoundField DataField="Estado" HeaderText="Estado" ReadOnly="True" SortExpression="Estado" />--%>
                                        <%--                              <asp:TemplateField HeaderText="Calificacion" SortExpression="Calificacion">
                                  <EditItemTemplate>
                                  <asp:TextBox ID="EditCalificacion" runat="server" Text='<%# Bind("Calificacion") %>' onkeypress="return NumCheck(event, this)"></asp:TextBox>
                               </EditItemTemplate>
                            <ItemTemplate>               
                           <asp:Label ID="Label1" runat="server" Text='<%# Bind("Calificacion") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>--%>
                                        <%--<asp:CommandField ButtonType="Button" ShowEditButton="True" />--%>
                                        <asp:ButtonField ButtonType="Button" Text="Crear Notas" CommandName="CrearNotas" AccessibleHeaderText="CrearNotas">
                                            <ControlStyle CssClass="btn btn-info" BorderColor="Black" BorderStyle="Outset" BackColor="#085394" ForeColor="White" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Button" Text="ver Notas" CommandName="VerNotas">

                                            <ControlStyle CssClass="btn btn-info" BorderColor="Black" BorderStyle="Outset" BackColor="#085394" ForeColor="White" />
                                        </asp:ButtonField>

                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                    <HeaderStyle BackColor="#085394" Font-Bold="True" ForeColor="#FFFFFF" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="FALSE" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
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
    <ajaxToolkit:ModalPopupExtender ID="btnPopUp_ModalPopupExtender2" runat="server" Enabled="True" TargetControlID="Button2"
        BackgroundCssClass="modalBackground" PopupControlID="PanelModal2">
    </ajaxToolkit:ModalPopupExtender>
    <!--fin boton abrir modal-->
    <asp:Panel ID="PanelModal2" runat="server" Style="display: none; background: white; width: 75%; height: auto; border: solid 1px black; z-index: -1;">
        <div class="modal-header">
        </div>
        <div class="modal-body">
            <div class="container-fluid well">
                <div class="row-fluid">
                    <div class="span4">
                        <div class="form-group col-sm-12">
                            <div class="col-sm-12">
                                <label class="control-label"><strong>Nivel</strong></label>
                                <asp:TextBox runat="server" ID="txtNivel" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>

                            <div class="col-sm-6">

                                <label class="control-label"><strong>Alumno</strong></label>
                                <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control"
                                    placeholder="Nombres" ReadOnly="true"></asp:TextBox>

                                <br />
                            </div>
                            <div class="col-sm-6">
                                <label class="control-label"><strong>Cédula</strong></label>
                                <asp:TextBox ID="txtCed" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                            </div>
                            <asp:HiddenField ID="HiddenIdIns" runat="server" />
                            <asp:HiddenField ID="HiddenNivel" runat="server" />
                            <asp:HiddenField ID="HiddenNivelEstudiante" runat="server" />
                        </div>
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <div class="form-group col-sm-12">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>


                                            <asp:Panel ID="Panel1" runat="server">
                                                <asp:GridView ID="dgvNotas" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered table-striped" DataKeyNames="IDNota" OnRowUpdating="dgvNotas_RowUpdating" OnRowCancelingEdit="dgvNotas_RowCancelingEdit" OnRowEditing="dgvNotas_RowEditing">
                                                    <Columns>
                                                        <asp:BoundField DataField="IDNota" HeaderText="id" SortExpression="IDNota" ReadOnly="true" />
                                                        <asp:BoundField DataField="NomUnidad" HeaderText="Tema de Unidad" SortExpression="NomUnidad" ReadOnly="true" />

                                                        <asp:TemplateField HeaderText="Calificacion" SortExpression="Calificacion">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="EditUnit1" runat="server" Text='<%# Bind("Calificacion") %>' onkeypress="return NumCheck(event, this)" onpaste="return false"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Calificacion") %>'></asp:Label>
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
                <div class="form-group col-sm-12">

                    <div class="col-xs-2">

                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <label class="control-label"><strong>Promedio: </strong></label>
                                <asp:TextBox ID="txtPromedio" runat="server" ReadOnly="true" onkeypress="return NumCheck(event, this)"  CssClass="form-control"></asp:TextBox>
                                <br />
                                <asp:Button ID="btnCalcPromedio" runat="server" Text="Cerrar Nivel" OnClick="btnCalcPromedio_Click" OnClientClick="return  confirmar_cierreNivel(this);" class="btn btn-success" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal-footer">

            <button class="btn" data-dismiss="modal" aria-hidden="true">Cancelar</button>
        </div>
    </asp:Panel>
    <asp:Button ID="Button2" runat="server" Height="47px" Text="MOSTRAR POPUP"
        Width="258px" hidden="hidden" />
    <script>
        function NoActualizar() {
            Swal.fire({
                icon: 'error',
                title: 'error',
                text: 'No se puede actualizar las notas el nivel ya fue cerrado',
                footer: '<a href></a>'
            })
        }
        function rechazado() {
            Swal.fire({
                icon: 'error',
                title: 'error',
                text: 'No existe Temas de Unidad creados para el Nivel',
                footer: '<a href></a>'
            })
        }
        function rechazado_notas() {
            Swal.fire({
                icon: 'error',
                title: 'error',
                text: 'Ya fueron creadas las notas para este nivel',
                footer: '<a href></a>'
            })
        }
        function confirm() {

            Swal.fire({
                icon: 'success',
                title: 'OK',
                text: 'Notas Creadas Correctamente!',
                footer: '<a href></a>'
            })
        }

        var object = { status: false, ele: null };
        function confirmar_cierreNivel(ev) {

            if (object.status) { return true; }
            Swal.fire({
                title: 'Está Seguro de Cerrar el Nivel?',
                text: "Usted no sera capaz de volver a editar las notas!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#29CB12',
                cancelButtonColor: '#d33',
                confirmButtonText: 'SI',
                cancelButtonText: 'NO',

            }).then((result) => {
                if (result.isConfirmed) {
                    object.status = true;
                    object.ele = ev;
                    object.ele.click();
                }
            })
            return false;
        }


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
            if (key == 46) {
                if (field.value == "") return false
                regexp = /^[0-9]+$/
                return regexp.test(field.value)
            }
            // other key
            return false
        }

    </script>
</asp:Content>
