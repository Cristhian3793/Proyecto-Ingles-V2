<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Site.Master" AutoEventWireup="true" CodeBehind="formAsignacionLicencias.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.formAsignacionLicencias" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10.16.5/dist/sweetalert2.all.min.js" type="text/javascript"></script>
    <link href="../Content/sweetalert.css" rel="stylesheet"/>
    <style>
                .swal2-container {
            z-index: 15000;
        }
                        td{
            padding:10px;
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container" style="width: 85%">
        <h4 class="well" style="background-color: rgb(8, 83, 148); color: white; text-align: center">Asignación de Licencias</h4>

        <div class="col-lg-12 well">
            <div class="row">
                <div class="col-sm-12">
                    <div class="container" style="background: #EAE8E8; padding: 5px; width: 100%; border: solid 1px #DBDADA; border-radius: 5px">
                        <div class="row">
                            <div class="col-sm-2">
                                <asp:Label ID="Label1" runat="server" Text=" Número Identificación:"></asp:Label>
                            </div>
                            <div class="row">
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtCedula" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-2">
                                <asp:Label ID="Label3" runat="server" Text="Período"></asp:Label>
                            </div>
                            <div class="col-sm-2">
                                <asp:DropDownList ID="cbxPeriodo" AppendDataBoundItems="true" runat="server" AutoPostBack="true" class="form-control">
                                    <asp:ListItem Text="-Todos-" Value="0" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-2">
                                <asp:Label ID="Label2" runat="server" Text="Nivel"></asp:Label>
                            </div>
                            <div class="col-sm-2">
                                <asp:DropDownList ID="cbxNiveles" AppendDataBoundItems="true" runat="server" AutoPostBack="true" class="form-control">
                                    <asp:ListItem Text="-Todos-" Value="0" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" class="btn btn-success" OnClick="btnConsultar_Click"/>

                        <span class="float-right" style="float: right">
                            <asp:LinkButton runat="server" ID="btnExccel" class="btn" OnClick="btnExccel_Click" Style="background: #24772E; color: white">
                                Exportar a Excel <i class="fas fa-file-excel"></i>
                            </asp:LinkButton>
                        </span>
                    </div>

                    <div class="row">
                        <br />
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="dgvInscrito" runat="server" AutoGenerateColumns="False" DataKeyNames="IdInscrito,IdNivelInscrito,IdLibro,Correo" CssClass="table table-bordered table-striped" EnablePersistedSelection="True" OnRowCommand="dgvInscrito_RowCommand" AllowPaging="True" PageSize="10" OnPageIndexChanging="dgvInscrito_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None"  >
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>

                                        <asp:BoundField DataField="NumDocInscrito" HeaderText="Identificacion" SortExpression="NumDocInscrito" ReadOnly="True" />
                                        <asp:BoundField DataField="IdInscrito" HeaderText="ID" SortExpression="IdInscrito" ReadOnly="True" />
                                        <asp:BoundField DataField="TipoEstudiante" HeaderText="Tipo Estudiante" SortExpression="TipoEstudiante" ReadOnly="True" />
                                        <asp:BoundField DataField="NombreInscrito" HeaderText="Nombres" SortExpression="NombreInscrito" ReadOnly="True" />
                                        <asp:BoundField DataField="ApellidoInscrito" HeaderText="Apellidos" SortExpression="ApellidoInscrito" ReadOnly="True" />
                                        <asp:BoundField DataField="NomNivel" HeaderText="Nivel" SortExpression="NomNivel" ReadOnly="True" />                                       
                                        <asp:BoundField DataField="IdNivelInscrito" HeaderText="IdNivelInscrito" SortExpression="IdNivelInscrito" Visible="false" />
                                        <asp:BoundField DataField="Periodo" HeaderText="Periodo" SortExpression="Periodo" ReadOnly="True" />
                                        <asp:BoundField DataField="IdLibro" HeaderText="IdLibro" SortExpression="IdLibro" ReadOnly="True" visible="false"/>
                                        <asp:BoundField DataField="Libro" HeaderText="Libro" SortExpression="Libro" ReadOnly="True" />
                                        <asp:BoundField DataField="IdNivelInscrito" HeaderText="IdNivelInscrito" SortExpression="IdNivelInscrito" ReadOnly="True" visible="false"/>
                                        <asp:BoundField DataField="LicenciaEstudiante" HeaderText="LicenciaEstudiante" SortExpression="LicenciaEstudiante" ReadOnly="True" />
                                        <asp:BoundField DataField="Correo" HeaderText="Correo" SortExpression="Correo" ReadOnly="True" />
                                        <asp:ButtonField ButtonType="Button" Text="Asignar Licencia" CommandName="VerLicencias" >
                                        <ControlStyle BackColor="#2568A3" BorderColor="Black" BorderStyle="Outset" CssClass="btn btn-success" />
                                        </asp:ButtonField>
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
    <asp:Panel ID="PanelModal2" runat="server" Style="display: none; background: white; width: 75%; height: auto; border: solid 1px black;">
        <div class="modal-header">
        </div>
        <div class="modal-body">
            <div class="container-fluid well">
                <div class="row-fluid">
                    <div class="span4">
                        <div class="form-group col-sm-12">

                            <div class="col-sm-6">
                                <label class="control-label"><strong>Alumno</strong></label>
                                <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control"
                                    placeholder="Nombres" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-sm-6">
                                <label class="control-label"><strong>Cédula</strong></label>
                                <asp:TextBox ID="txtCed" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-6">
                                <label class="control-label"><strong>Nivel</strong></label>
                                <asp:TextBox runat="server" ID="txtNivel" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                             <div class="col-sm-6">
                                <label class="control-label"><strong>Libro</strong></label>
                                <asp:TextBox runat="server" ID="txtLibro" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <asp:HiddenField ID="HiddenIdIns" runat="server" />
                            <asp:HiddenField ID="HiddenNivel" runat="server" />
                            <asp:HiddenField ID="HiddenNivelEstudiante" runat="server" />
                            <asp:HiddenField ID="HiddenIdLibro" runat="server" />
                            <asp:HiddenField ID="HiddenEmail" runat="server" />
                        </div>
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <div class="form-group col-sm-12">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel1" runat="server">
                                                <asp:GridView ID="dgvLicencias" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered table-striped" DataKeyNames="IDLICENCIA,LICENCIA" OnRowCommand="dgvLicencias_RowCommand" OnPageIndexChanging="dgvLicencias_PageIndexChanging" PageSize="5" AllowPaging="true">
                                                    <Columns>
                                                        <asp:BoundField DataField="IDLICENCIA" HeaderText="id" SortExpression="IDLICENCIA" />
                                                        <asp:BoundField DataField="LICENCIA" HeaderText="Licencia" SortExpression="LICENCIA" />
                                                        <asp:BoundField DataField="ESTADO" HeaderText="EstadoLicencia" SortExpression="ESTADO" visible="false"/>
                                                        <asp:ButtonField ButtonType="Button" Text="Asignar" CommandName="asignarLicencia" />
                                                    </Columns>
                                                    <FooterStyle BackColor="#99CCCC"
                                                        ForeColor="#003399" />
                                                    <HeaderStyle BackColor="#085394" Font-Bold="True" ForeColor="#CCCCFF" />
                                                    <PagerStyle BackColor="#085394" ForeColor="White" HorizontalAlign="Center" />
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
            <button class="btn" data-dismiss="modal" aria-hidden="true">Cancelar</button>
        </div>

    </asp:Panel>
    <asp:Button ID="Button2" runat="server" Height="47px" Text="MOSTRAR POPUP" Width="258px" hidden="hidden" />
    <script>

        function confirm() {

            Swal.fire({
                icon: 'success',
                title: 'OK',
                text: 'Licencia Asignada correctamente',
                footer: '<a href></a>'
            })
        }
        function existeLicenciaAsignada() {

            Swal.fire({
                icon: 'error',
                title: 'error',
                text: 'ya se ha asignado una licencia a este nivel',
                footer: '<a href></a>'
            })
        }
    </script>
</asp:Content>
