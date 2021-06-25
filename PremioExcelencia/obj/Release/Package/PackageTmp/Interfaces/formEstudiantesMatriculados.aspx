<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Site.Master" AutoEventWireup="true" CodeBehind="formEstudiantesMatriculados.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.formEstudiantesMatriculados" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" />
       <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.2/jspdf.min.js"></script>
    <style>
        td {
            padding: 10px;
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" style="width: 85%">
        <h4 class="well" style="background-color: rgb(8, 83, 148); color: white; text-align: center">Alumnos Matriculados
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
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtCedula" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-2">
                                <asp:Label ID="Label3" runat="server" Text="Niveles"></asp:Label>
                            </div>
                            <div class="col-sm-2">
                                <asp:DropDownList ID="cbxNiveles" AppendDataBoundItems="true" runat="server" AutoPostBack="true" class="form-control">
                                    <asp:ListItem Text="-Todos-" Value="0" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-2">
                                <asp:Label ID="Label1" runat="server" Text="Periodos"></asp:Label>
                            </div>
                            <div class="col-sm-2">
                                <asp:DropDownList ID="cbxPeriodos" AppendDataBoundItems="true" runat="server" AutoPostBack="true" class="form-control">
                                    <asp:ListItem Text="-Todos-" Value="0" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <asp:Button ID="Button1" runat="server" Text="Consultar" class="btn btn-success" OnClick="Button1_Click" />

                        <span class="float-right" style="float: right">

                            <asp:LinkButton runat="server" ID="btnSubmit" class="btn" OnClick="btnSubmit_Click" Style="background: #24772E; color: white">
                                Exportar a Excel <i class="fas fa-file-excel"></i>
                            </asp:LinkButton>
                            <asp:Button ID="btnPdf" runat="server" Text="Descargar Reporte" class="btn btn-info" OnClientClick="pruebaDivAPdf();" Visible="false"/>
                        </span>
                    </div>

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-12" id="cuerpo_pdf">

                                    <div class="table-responsive">
                                        <asp:GridView ID="dgvNotasPruebas" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False" DataKeyNames="IdInscrito" HorizontalAlign="Center" CssClass="table table-bordered table-striped" AllowPaging="true" PageSize="10" OnPageIndexChanging="dgvNotasPruebas_PageIndexChanging">
                                            <Columns>
                                                <asp:BoundField DataField="NumDocInscrito" HeaderText="N° Identificación" SortExpression="NumDocInscrito" ReadOnly="True" />
                                                <asp:BoundField DataField="IdInscrito" HeaderText="ID" SortExpression="IdInscrito" ReadOnly="True" />
                                                <asp:BoundField DataField="TipoEstudiante" HeaderText="Tipo Estudiante" SortExpression="TipoEstudiante" ReadOnly="True" />
                                                <asp:BoundField DataField="NombreInscrito" HeaderText="Nombres" SortExpression="NombreInscrito" ReadOnly="True" />
                                                <asp:BoundField DataField="ApellidoInscrito" HeaderText="Apellidos" SortExpression="ApellidoInscrito" ReadOnly="True" />
                                                <asp:BoundField DataField="CeluInscrito" HeaderText="Celular" SortExpression="CeluInscrito" ReadOnly="True" />
                                                <asp:BoundField DataField="TelefInscrito" HeaderText="Telefono" SortExpression="TelefInscrito" ReadOnly="True" />
                                                <asp:BoundField DataField="EmailInscrito" HeaderText="Email" SortExpression="EmailInscrito" ReadOnly="True" />
                                                <asp:BoundField DataField="PeriodoLectivo" HeaderText="Período" ReadOnly="True" SortExpression="PeriodoLectivo" />
                                                <asp:BoundField DataField="IdNivel" HeaderText="idNivel" ReadOnly="True" SortExpression="IdNivel" Visible="False" />
                                                <asp:BoundField DataField="NomNivel" HeaderText="Nivel" ReadOnly="True" SortExpression="NomNivel" />
                                                <asp:BoundField DataField="Estado" HeaderText="Estado" ReadOnly="True" SortExpression="Estado" />
                                            </Columns>
                                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                            <HeaderStyle BackColor="#085394" Font-Bold="True" ForeColor="#FFFFFF" />
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

                            <asp:GridView ID="GridView1" runat="server">
                            </asp:GridView>
                            <asp:GridView ID="GridView2" runat="server"></asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <script>

        function pruebaDivAPdf() {

            var pdf = new jsPDF('p', 'pt', 'letter');
            var d = new Date();
            var fecha = d.getDate();
            var nombre = "matriculados";
            var elementHTML = $('#cuerpo_pdf')[0];
            var nombre_archivo = nombre + "_" + fecha;
            source = elementHTML;

            specialElementHandlers = {
                '#bypassme': function (element, renderer) {
                    return true
                }
            };

            margins = {
                top: 115,
                bottom: 60,
                left: 70,
                width: 500,

            };

            pdf.fromHTML(
                source,
                //margins.bottom,
                margins.left,//x
                margins.top,//y

                { // y coord
                    'width': margins.width,
                    'elementHandlers': specialElementHandlers
                },

                function (dispose) {
                    pdf.save(nombre_archivo + '.pdf');
                }, margins
            );


        }
    </script>
</asp:Content>
