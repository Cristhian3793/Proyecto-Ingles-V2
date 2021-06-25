<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Site.Master" AutoEventWireup="true" CodeBehind="formConsultaNiveles.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.formConsultaNivelesProgramado" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0">
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
    <div class="container">
        <h4 class="well" style="background-color: rgb(8, 83, 148); color: white; text-align: center">Consulta Niveles</h4>
        <div class="col-lg-12 well">
            <div class="row">
                <div class="col-sm-12">
                    <div class="container" style="background: #EAE8E8; padding: 5px; width: 100%; border: solid 1px #DBDADA; border-radius: 5px">
                        <div class="row">
                            <div class="col-sm-2">
                                <asp:Label ID="Label2" runat="server" Text="Cod Producto"></asp:Label>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtCodCurso" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-2">
                                <asp:Label ID="Label1" runat="server" Text="Tipo Nivel"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="cbxSearchNiveles" AppendDataBoundItems="true" runat="server" AutoPostBack="true" class="form-control">
                                    <asp:ListItem Text="-Todos-" Value="0" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" class="btn btn-success" OnClick="btnConsultar_Click" />
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnactualizar" />
                                    </Triggers>
                                    <ContentTemplate>


                                        <asp:GridView ID="dgvNiveles" runat="server" CellPadding="4" BackColor="White" BorderColor="#3366CC" BorderStyle="None" AutoGenerateColumns="False" CellSpacing="2" HorizontalAlign="Center" OnRowCommand="dgvNiveles_RowCommand" DataKeyNames="idNivel" OnRowDeleting="dgvNiveles_RowDeleting" CssClass="table table-bordered table-striped" AllowPaging="true" PageSize="9" OnPageIndexChanging="dgvNiveles_PageIndexChanging">
                                            <Columns>
                                                <asp:BoundField DataField="idNivel" HeaderText="idNivel" SortExpression="idNivel" />
                                                <%--<asp:BoundField DataField="idEstadoNivel" HeaderText="ESTADO NIVEL" SortExpression="idEstadoNivel" />--%>
                                                <asp:BoundField DataField="estadoNivel" HeaderText="ESTADO NIVEL" SortExpression="estadoNivel" />
                                                <%--<asp:BoundField DataField="idCurso" HeaderText="Curso" SortExpression="idCurso" />--%>
                                                <asp:BoundField DataField="nomCurso" HeaderText="Curso" SortExpression="nomCurso" />
                                                <asp:BoundField DataField="codNivel" HeaderText="Codigo Producto" SortExpression="codNivel" />
                                                <asp:BoundField DataField="nomNivel" HeaderText="Nombre" SortExpression="nomNivel" />
                                                <asp:BoundField DataField="descNivel" HeaderText="Nivel" SortExpression="descNivel" />
                                                <asp:BoundField DataField="tipoNivel" HeaderText="Tipo Nivel" SortExpression="tipoNivel" />
                                                <asp:BoundField DataField="libro" HeaderText="Libro" SortExpression="libro" />
                                                <asp:BoundField DataField="CostoNivel" HeaderText="Costo Nivel" SortExpression="CostoNivel" />
                                                <asp:ButtonField ButtonType="Button" Text="Editar" CommandName="Editar" />
                                                <asp:TemplateField HeaderText="acciones">
                                                    <ItemTemplate>
                                                        <asp:Button CommandName="Delete" runat="server" Text="Eliminar" OnClientClick="return  confirmdelete(this);" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                            <HeaderStyle BackColor="#085394" Font-Bold="True" ForeColor="#CCCCFF" />
                                            <PagerStyle BackColor="#085394" ForeColor="White" HorizontalAlign="Center" CssClass="stilo-paginacion" />
                                            <RowStyle BackColor="White" ForeColor="#003399" />
                                            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                            <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                            <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                            <SortedDescendingHeaderStyle BackColor="#002876" />
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
                                <asp:HiddenField ID="idTipoNivel" runat="server" />
                                <div class="form-group col-sm-6">
                                    <label class="control-label"><strong>Curso:</strong></label>
                                    <div class="controls">
                                        <asp:DropDownList ID="cbxCursos" runat="server" class="form-control" Enabled="false">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label class="control-label"><strong>Id Nivel :</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtIdNivel" runat="server" CssClass="form-control"
                                            TabIndex="4" ValidationGroup="val4" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label class="control-label"><strong>Cod Producto :</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtCodNivel" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label class="control-label"><strong>Nombre Nivel :</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtNomNivel" runat="server" CssClass="form-control"
                                            TabIndex="4" ValidationGroup="val4"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label class="control-label"><strong>Nivel :</strong></label>
                                    <div class="controls">
                                        <asp:DropDownList ID="cbxNivel" AppendDataBoundItems="true" runat="server" class="form-control">
                                            <asp:ListItem Text="0" Value="0" />
                                            <asp:ListItem Text="1" Value="1" />
                                            <asp:ListItem Text="2" Value="2" />
                                            <asp:ListItem Text="3" Value="3" />
                                            <asp:ListItem Text="4" Value="4" />
                                            <asp:ListItem Text="5" Value="5" />
                                            <asp:ListItem Text="6" Value="6" />
                                            <asp:ListItem Text="7" Value="7" />
                                            <asp:ListItem Text="8" Value="8" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label class="control-label"><strong>Estado Nivel :</strong></label>
                                    <div class="controls">
                                        <asp:DropDownList ID="cbxEstadoNivel" runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label class="control-label"><strong>Libro :</strong></label>
                                    <div class="controls">
                                        <asp:DropDownList ID="cbxLibros" runat="server" class="form-control" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="--Seleccionar--"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label class="control-label"><strong>Costo Nivel :</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtCostoNivel" runat="server" CssClass="form-control"
                                            TabIndex="4" ValidationGroup="val4" onkeypress="return NumCheck(event, this)" onpaste="return false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                    <asp:Button class="btn btn-primary" ID="btnactualizar" OnClick="btnActualizar_Click" runat="server" Text="Guardar Cambios" />
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
    </script>
</asp:Content>
