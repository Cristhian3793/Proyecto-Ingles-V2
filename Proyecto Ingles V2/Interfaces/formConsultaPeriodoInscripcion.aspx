<%@ Page Language="C#" MasterPageFile="~/Interfaces/Site.Master" AutoEventWireup="true" CodeBehind="formConsultaPeriodoInscripcion.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.ConsultaPeriodoInscripcion" Async="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Consulta Periodos</title>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10.16.5/dist/sweetalert2.all.min.js" type="text/javascript"></script>
    <link href="../Content/sweetalert.css" rel="stylesheet" />
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
    <div class="container" style="padding-left: 10%; padding-right: 10%; width: 100%">
        <h4 class="well" style="background-color: rgb(8, 83, 148); color: white; text-align: center">Consulta Períodos</h4>
        <div class="col-lg-12 well">
            <div class="row">
                <div class="col-sm-12">

                    <div class="container" style="background: #EAE8E8; padding: 5px; width: 100%; border: solid 1px #DBDADA; border-radius: 5px">
                        <div class="row">
                            <div class="col-sm-2">
                                <asp:Label ID="Label2" runat="server" Text="Período"></asp:Label>
                                <asp:TextBox ID="txtPeriodo" runat="server" CssClass="form-control"></asp:TextBox>
                               
                            </div>

                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-2">
                             <asp:Button ID="btnConsulta" runat="server" OnClick="btnConsulta_Click" Text="Buscar" class="btn btn-success" />
                            </div>
                         </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="dgvPeriodo" runat="server" AutoGenerateColumns="False" AllowSorting="True" CaptionAlign="Bottom" OnRowCommand="dgvPeriodo_RowCommand" DataKeyNames="IDPERIODOINSCRIPCION" OnRowEditing="dgvPeriodo_RowEditing" OnRowDeleting="dgvPeriodo_RowDeleting" HorizontalAlign="Center" CssClass="table table-bordered table-striped" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IDPERIODOINSCRIPCION" HeaderText="ID" SortExpression="IDPERIODOINSCRIPCION" />
                                        <asp:BoundField DataField="PERIODO" HeaderText="PERIODO" SortExpression="PERIODO" />
                                        <asp:BoundField DataField="ANOLECTIVO" HeaderText="AÑO LECTIVO" SortExpression="ANOLECTIVO" />

                                        <asp:BoundField DataField="FechaInicio" HeaderText="FECHA INICIO" SortExpression="FechaInicio" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="FechaFin" HeaderText="FECHA FIN" SortExpression="FechaFin" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="EstadoPeriodo" HeaderText="ESTADO" SortExpression="EstadoPeriodo" />
                                        <asp:ButtonField ButtonType="Button" Text="Editar" CommandName="Editar" >
                                        <ControlStyle BackColor="#085394" BorderColor="Black" BorderStyle="Outset" CssClass="btn btn-success" />
                                        </asp:ButtonField>
                                        <asp:TemplateField HeaderText="acciones">
                                            <ItemTemplate>
                                                <asp:Button CommandName="Delete" runat="server" Text="Eliminar" OnClientClick="return  confirmdelete(this);" />
                                            </ItemTemplate>
                                            <ControlStyle BackColor="#CC0000" BorderColor="Black" BorderStyle="Outset" CssClass="btn btn-success" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                    <HeaderStyle BackColor="#085394" Font-Bold="True" ForeColor="#FFFFFF" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
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
                            <div class="span4 ">
                                <div class="row">

                                    <div class="form-group col-sm-3 ">
                                        <label class="control-label"><strong>Id Periodo</strong></label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtId" runat="server" CssClass="form-control"
                                                TabIndex="4" ValidationGroup="val4" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label class="control-label"><strong>Periodo :</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtPeriodoM" runat="server" CssClass="form-control"
                                            placeholder="Periodo" TabIndex="4" ValidationGroup="val4"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label class="control-label"><strong>Año Lectivo:</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtAnoLectivo" runat="server" CssClass="form-control"
                                            placeholder="Año Lectivo" TabIndex="4" ValidationGroup="val4"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group col-sm-6">
                                    <label class="control-label"><strong>Fecha Inicio :</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="form-control" type="Date"
                                            placeholder="Fecha Inicio Periodo" TabIndex="4" ValidationGroup="val4"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label class="control-label"><strong>Fecha Final :</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtFechaFin" runat="server" CssClass="form-control" type="Date"
                                            placeholder="Fecha Final Periodo" TabIndex="4" ValidationGroup="val4"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group col-sm-6">
                                    <div class="controls">
                                        <asp:Label ID="Label1" runat="server" Text="Estado :" Style="font-weight: bold"></asp:Label>
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="RabActivo" />
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
                    <asp:Button class="btn btn-primary" ID="btnactualizar" runat="server" Text="Guardar Cambios" ispostback="true" OnClick="btnActualizar_Click" />
                </div>
            </asp:Panel>
            <asp:Button ID="btnPopUp" runat="server" Height="47px" Text="MOSTRAR POPUP"
                Width="258px" hidden="hidden" />
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
        function ExistPeriodoActivo() {
            Swal.fire({
                icon: 'error',

                text: 'Ya existe un periodo activo!',
                footer: '<a href></a>'
            })
        }
        function confirm() {
            Swal.fire({
                icon: 'success',
                title: 'OK',
                text: 'El Registro se Actualizo Corectamente!',
                footer: '<a href></a>'
            })
        }
    </script>

</asp:Content>


