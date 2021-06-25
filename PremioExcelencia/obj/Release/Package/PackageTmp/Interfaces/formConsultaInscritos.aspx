<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Interfaces/Site.Master" CodeBehind="formConsultaInscritos.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.ConsultaInscritos" Async="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title></title>
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10.16.5/dist/sweetalert2.all.min.js" type="text/javascript"></script>
    <link href="../Content/sweetalert.css" rel="stylesheet" />

    <style>
        td {
            padding: 10px;
            color: black;
            
        }

        .buttonExcel {
            background-image: url(../Interfaces/images/iconos/excel.png);
        }

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
    <div class="container" style="width: 85%">
        <h4 class="well" style="background-color: rgb(8, 83, 148); color: white; text-align: center">Consulta Inscritos</h4>

        <div class="col-lg-12 well">
            <div class="row">
                <div class="col-sm-12">
                    <div class="container" style="background: #EAE8E8; padding: 5px; width: 100%; border: solid 1px #DBDADA; border-radius: 5px">
                        <div class="row">
                            <div class="col-sm-2">
                                <asp:Label ID="Label1" runat="server" Text="N° Identificación:"></asp:Label>
                            </div>
                            <div class="row">
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtCedula" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-2">
                                <asp:Label ID="Label3" runat="server" Text="Período:"></asp:Label>
                            </div>
                            <div class="row">
                                <div class="col-sm-2">
                                    <asp:DropDownList ID="cbxPeriodo" AppendDataBoundItems="true" runat="server" AutoPostBack="true" class="form-control">
                                        <asp:ListItem Text="-Todos-" Value="0" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-2">
                                <asp:Label ID="Label4" runat="server" Text="Niveles"></asp:Label>
                            </div>
                            <div class="col-sm-2">
                                <asp:DropDownList ID="cbxNiveles" AppendDataBoundItems="true" runat="server" AutoPostBack="true" class="form-control">
                                    <asp:ListItem Text="-Todos-" Value="0" />
                                </asp:DropDownList>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col-sm-2">
                                <asp:Button ID="btnConsultar" runat="server" OnClick="btnConsultar_Click" Text="Consultar" class="btn btn-success" />
                            </div>
                        </div>
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
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnactualizar" />
                                    </Triggers>
                                    <ContentTemplate>


                                        <asp:GridView ID="dgvInscrito" runat="server" AutoGenerateColumns="False" DataKeyNames="IdInscrito" CssClass="table table-bordered table-striped" EnablePersistedSelection="True" OnRowDeleting="dgvInscrito_RowDeleting" OnRowEditing="dgvInscrito_RowEditing" OnRowCommand="dgvInscrito_RowCommand" HorizontalAlign="Center" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="dgvInscrito_PageIndexChanging" AllowPaging="True">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <Columns>
                                                <asp:BoundField DataField="IdInscrito" HeaderText="Id Estudiante" SortExpression="IdInscrito" />
                                                <asp:BoundField DataField="NumDocInscrito" HeaderText="N° Identificación" SortExpression="NumDocInscrito" />
                                                <asp:BoundField DataField="PeriodoLectivo" HeaderText="Período" SortExpression="PeriodoLectivo" />
                                                <asp:BoundField DataField="IdNivel" HeaderText="IdNivel" SortExpression="IdNivel" Visible="false" />
                                                <asp:BoundField DataField="NombreInscrito" HeaderText="Nombres" SortExpression="NombreInscrito" />
                                                <asp:BoundField DataField="ApellidoInscrito" HeaderText="Apellidos" SortExpression="ApellidoInscrito" />
                                                <asp:BoundField DataField="CeluInscrito" HeaderText="Celular" SortExpression="CeluInscrito" />
                                                <asp:BoundField DataField="TelefInscrito" HeaderText="Teléfono" SortExpression="TelefInscrito" />
                                                <asp:BoundField DataField="DirecInscrito" HeaderText="Direccción" SortExpression="DirecInscrito" />
                                                <asp:BoundField DataField="EmailInscrito" HeaderText="Email" SortExpression="EmailInscrito" />
                                                <asp:BoundField DataField="EstadoPrueba" HeaderText="EstadoPrueba" SortExpression="EstadoPrueba" Visible="false" />
                                                <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" SortExpression="FechaRegistro" />
                                                <asp:BoundField DataField="TipoEstudiante" HeaderText="Tipo Estudiante" SortExpression="TipoEstudiante" />
                                                <asp:BoundField DataField="Prueba" HeaderText="Prueba" SortExpression="Prueba" />
                                                <asp:BoundField DataField="Info" HeaderText="Info" SortExpression="Info" />
                                                <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                                                <asp:BoundField DataField="NomNivel" HeaderText="Nivel" SortExpression="NomNivel" />
                                                <asp:ButtonField ButtonType="Button" Text="Editar" CommandName="Editar" AccessibleHeaderText="Edicion">
                                                    <ControlStyle BackColor="#2568A3" BorderColor="Black" BorderStyle="Outset" CssClass="btn btn-sm" ForeColor="White" />
                                                </asp:ButtonField>
                                                <asp:TemplateField HeaderText="acciones">
                                                    <ItemTemplate>
                                                        <asp:Button CommandName="Delete" runat="server" Text="Eliminar" OnClientClick="return  confirmdelete(this);" />
                                                    </ItemTemplate>
                                                    <ControlStyle BackColor="#CC0000" BorderColor="Black" BorderStyle="Outset" CssClass="btn btn-sm" ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:ButtonField ButtonType="Button" Text="Info" CommandName="Info" AccessibleHeaderText="Info">

                                                    <ControlStyle BackColor="#2568A3" BorderColor="Black" BorderStyle="Outset" CssClass="btn btn-sm" ForeColor="White" />
                                                </asp:ButtonField>

                                            </Columns>
                                            <EditRowStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                            <HeaderStyle BackColor="#085394" Font-Bold="True" ForeColor="#FFFFFF" />
                                            <PagerStyle BackColor="#085394" ForeColor="White" HorizontalAlign="Center" CssClass="stilo-paginacion"/>
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="FALSE" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        </asp:GridView>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!--inicio modal para actualizaciones-->
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!---botton abir modal-->
            <ajaxToolkit:ModalPopupExtender ID="btnPopUp_ModalPopupExtender" runat="server" Enabled="True" TargetControlID="btnPopUp"
                BackgroundCssClass="modalBackground" PopupControlID="PanelModal">
            </ajaxToolkit:ModalPopupExtender>
            <!--fin boton abrir modal-->
            <asp:Panel ID="PanelModal" runat="server" Style="display: none; background: white; width: 40%; height: auto; border: solid 1px black">
                <div class="modal-header">
                    <%--<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>--%>
                </div>
                <div class="modal-body">
                    <div class="container-fluid well">
                        <div class="row-fluid">
                            <div class="span4">
                                <div class="row ">
                                    <div class="form-group col-sm-6">
                                        <label class="control-label"><strong>Estudiante UISEK:</strong></label>
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="RabTipoEstudiante" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label class="control-label"><strong>Id Inscrito :</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtId" runat="server" CssClass="form-control"
                                            TabIndex="4" ValidationGroup="val4" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label class="control-label"><strong>Identificacion :</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="form-control"
                                            placeholder="Identificacion" TabIndex="4" ValidationGroup="val4" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group col-sm-6" hidden="hidden">
                                    <label class="control-label"><strong>Periodo de Inscripción:</strong></label>
                                    <asp:DropDownList ID="cbxPeriodoLectivo" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group col-sm-6">
                                    <label class="control-label"><strong>Email :</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"
                                            placeholder="Email" TabIndex="4" ValidationGroup="val4"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label class="control-label"><strong>Nombres :</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control"
                                            placeholder="Nombres del Cliente" TabIndex="4" ValidationGroup="val4"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label class="control-label"><strong>Apellidos :</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control"
                                            placeholder="Apellidos" TabIndex="4" ValidationGroup="val4"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label class="control-label"><strong>Celular :</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtCelular" runat="server" CssClass="form-control"
                                            placeholder="Apellidos" TabIndex="4" ValidationGroup="val4" onkeypress="javascript:return solonumeros(event)" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label class="control-label"><strong>Telefono Convencional:</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"
                                            placeholder="Telefono" TabIndex="4" ValidationGroup="val4" onkeypress="javascript:return solonumeros(event)" MaxLength="9"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group col-sm-12">
                                    <label class="control-label"><strong>Direccion:</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"
                                            placeholder="Direccion" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group col-sm-12" hidden="hidden">
                                    <label class="control-label"><strong>Informacion sobre Curso:</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtInformacion" runat="server" CssClass="form-control"
                                            placeholder="Información" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group col-sm-6" hidden="hidden">
                                    <asp:Label ID="Label2" runat="server" Text="Prueba Ubicación :" Style="font-weight: bold"></asp:Label>
                                    <label class="switch">
                                        <input type="checkbox" runat="server" id="RabPruebaUbicacion" />
                                        <span class="slider round"></span>
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                    <asp:Button class="btn btn-primary" ID="btnactualizar" OnClick="btnActualizar_Click" runat="server" Text="Guardar Cambios" ispostback="true" />
                </div>
            </asp:Panel>
            <asp:Button ID="btnPopUp" runat="server" Height="47px" Text="MOSTRAR POPUP"
                Width="258px" hidden="hidden" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <!--fin modal-->
    <!--inicio modal para info-->
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <!---botton abir modal-->
            <ajaxToolkit:ModalPopupExtender ID="btnPopUp_ModalPopupExtender2" runat="server" Enabled="True" TargetControlID="Button2"
                BackgroundCssClass="modalBackground" PopupControlID="PanelModal2">
            </ajaxToolkit:ModalPopupExtender>
            <!--fin boton abrir modal-->
            <asp:Panel ID="PanelModal2" runat="server" Style="display: none; background: white; width: 40%; height: auto; border: solid 1px black">
                <div class="modal-header">
                    <%--<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>--%>
                </div>
                <div class="modal-body">
                    <div class="container-fluid well">
                        <div class="row-fluid">
                            <div class="span4">

                                <div class="form-group col-sm-6">
                                    <label class="control-label"><strong>Email :</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtUsuEmail" runat="server" CssClass="form-control"
                                            placeholder="Email" TabIndex="4" ValidationGroup="val4"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group col-sm-12">
                                    <label class="control-label"><strong>Solicitud de Información sobre Curso:</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtsinfo" runat="server" CssClass="form-control"
                                            placeholder="Información Sobre Curso" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group col-sm-12">
                                    <label class="control-label"><strong>Respuestas sobre Informacion Curso:</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtInfo" runat="server" CssClass="form-control"
                                            placeholder="Información Sobre Curso" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                    <asp:Button class="btn btn-primary" ID="Button1" runat="server" Text="Enviar" ispostback="true" OnClick="Button1_Click" />
                </div>
            </asp:Panel>
            <asp:Button ID="Button2" runat="server" Height="47px" Text="MOSTRAR POPUP"
                Width="258px" hidden="hidden" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <!--fin modal-->
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
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
        function confirm() {

            Swal.fire({
                icon: 'success',
                title: 'OK',
                text: 'Enviado!',
                footer: '<a href></a>'
            })
        }
        function rechazado() {

            Swal.fire({
                icon: 'error',
                title: 'error',
                text: 'No se pudo Enviar',
                footer: '<a href></a>'
            })
        }

        function solonumeros(e) {

            var key;

            if (window.event) // IE
            {
                key = e.keyCode;
            }
            else if (e.which) // Netscape/Firefox/Opera
            {
                key = e.which;
            }

            if (key < 48 || key > 57) {
                return false;
            }

            return true;
        }
    </script>
</asp:Content>



