<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Site.Master" AutoEventWireup="true" CodeBehind="formConsultaCalificacionNivel.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.formConsultaCalificacionNivel" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    <div class="container" style="width: 70%">
        <h4 class="well" style="background-color: rgb(8, 83, 148); color: white; text-align: center">Consulta Calificaciones </h4>
        <div class="col-lg-12 well">
            <div class="row">
                <div class="col-sm-12">
                    <div class="container" style="background: #EAE8E8; padding: 5px; width: 100%; border: solid 1px #DBDADA; border-radius: 5px">
                        <div class="row">
                            <div class="col-sm-2">
                                <asp:Label ID="Label2" runat="server" Text="Cod Nivel"></asp:Label>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtNivel" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" class="btn btn-success" OnClick="btnConsultar_Click" />
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="dgvCalificacionNivel" runat="server" BackColor="White" DataKeyNames="idCalificacionNivel,idNivel" GridLines="none" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="false" EnablePersistedSelection="True" HorizontalAlign="Center" OnRowCancelingEdit="dgvCalificacionNivel_RowCancelingEdit" OnRowEditing="dgvCalificacionNivel_RowEditing" OnRowUpdating="dgvCalificacionNivel_RowUpdating" OnRowDeleting="dgvCalificacionNivel_RowDeleting" CssClass="table table-bordered table-striped" AllowPaging="true" PageSize="10" OnPageIndexChanging="dgvCalificacionNivel_PageIndexChanging">
                                            <Columns>
                                                <asp:BoundField DataField="idCalificacionNivel" HeaderText="Id" SortExpression="idCalificacionNivel" ReadOnly="true" />
                                                <asp:BoundField DataField="idNivel" HeaderText="Id Nivel" SortExpression="idNivel" Visible="false" />
                                                <asp:BoundField DataField="nomNivel" HeaderText="Nivel" SortExpression="nomNivel" ReadOnly="true" />
                                                 <asp:BoundField DataField="codNivel" HeaderText="Cod Nivel" SortExpression="codNivel" ReadOnly="true" />
                                                <asp:BoundField DataField="tipoNivel" HeaderText="tipoNivel" SortExpression="tipoNivel" ReadOnly="true" />
                                                <asp:TemplateField HeaderText="Calificacion Desde" SortExpression="Calificacion">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="EditCalificacion" runat="server" Text='<%# Bind("Calificacion") %>' onpaste="return false" onkeypress="return NumCheck(event, this)"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Calificacion") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Calificacion Hasta" SortExpression="CalificacionHasta">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("CalificacionHasta") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtCalificacionHasta" Text='<%# Bind("CalificacionHasta") %>' onpaste="return false" onkeypress="return NumCheck(event, this)"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                                                <asp:TemplateField HeaderText="acciones">
                                                    <ItemTemplate>
                                                        <asp:Button CommandName="Delete" runat="server" Text="Eliminar" OnClientClick="return  confirmdelete(this);" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>

                                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                            <HeaderStyle BackColor="#085394" Font-Bold="True" ForeColor="#FFFFFF" HorizontalAlign="Center" />
                                            <PagerStyle BackColor="#085394" ForeColor="White" HorizontalAlign="Center" CssClass="stilo-paginacion" />
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <script>
        function re() {

            Swal.fire({
                icon: 'error',
                title: 'error',
                text: 'Registro No se pudo Guardar,Ya existe un nivel con las mismas calificaciones',
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
