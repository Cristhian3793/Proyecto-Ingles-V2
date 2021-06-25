<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Site.Master" AutoEventWireup="true" CodeBehind="formSeleccionNivel.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.formSeleccionNivel" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10.16.5/dist/sweetalert2.all.min.js" type="text/javascript"></script>
    <link href="../Content/sweetalert.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="width: 85%">
        <h4 class="well" style="background-color: rgb(8, 83, 148); color: white; text-align: center">Nivel Disponible Estudiante</h4>

        <div class="col-lg-12 well">
            <div class="row">
                <div class="col-sm-12">
                    <div class="container" style="background: #EAE8E8; padding: 5px; width: 100%; border: solid 1px #DBDADA; border-radius: 5px">
                        <b>
                            <asp:Label ID="Label1" runat="server" Text="Período" Visible="false"></asp:Label></b>
                        <div class="row">
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtPeriodo" runat="server" ReadOnly="true" CssClass="form-control" Visible="false"></asp:TextBox>
                            </div>
                        </div>

                        <asp:HiddenField ID="HiddenIdPeriodo" runat="server" />
                        <asp:HiddenField ID="HiddenPeriodo" runat="server" />
                        <asp:HiddenField ID="HiddenIdNivelEstudiante" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <br />
            <div class="col-md-12">
                <div class="table-responsive">
                    <asp:GridView ID="dgvNivel" runat="server" DataKeyNames="CODALUMNO,IDNIVEL" EnablePersistedSelection="True" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" OnRowCommand="dgvNivel_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="CODALUMNO" HeaderText="COD ALUMNO" SortExpression="CODALUMNO" Visible="false" />
                            <%--<asp:BoundField DataField="PERIODO" HeaderText="PERIODO" SortExpression="PERIODO" />--%>
                            <%--<asp:BoundField DataField="IDPERIODO" HeaderText="IDPERIODO" SortExpression="IDPERIODO" />--%>
                            <asp:BoundField DataField="IDNIVEL" HeaderText="IDNIVEL" SortExpression="IDNIVEL" Visible="false" />
                            <asp:BoundField DataField="CODNIVEL" HeaderText="CODNIVEL" SortExpression="CODNIVEL" />
                            <asp:BoundField DataField="NIVEL" HeaderText="NIVEL" SortExpression="NIVEL" />
                            <asp:BoundField DataField="DESCNIVEL" HeaderText="DESCNIVEL" SortExpression="DESCNIVEL" />
                            <asp:BoundField DataField="PRECIO" HeaderText="PRECIO" SortExpression="PRECIO" />
                            <asp:ButtonField ButtonType="Button" Text="Inscribirse" CommandName="Inscribirse" AccessibleHeaderText="Inscribirse">
                                <ControlStyle BackColor="#2568A3" BorderColor="Black" BorderStyle="Outset" CssClass="btn btn-sm" />
                            </asp:ButtonField>


                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#085394" Font-Bold="True" ForeColor="#FFFFFF" />
                        <PagerStyle BackColor="#085394" ForeColor="White" HorizontalAlign="Center" CssClass="stilo-paginacion" />
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



    <script>
        var object = { status: false, ele: null };
        function confirmInscripcion(ev) {

            if (object.status) { return true; }
            Swal.fire({
                title: 'Esta seguro de inscribirse',
                text: "Sus datos seran enviados ",
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

