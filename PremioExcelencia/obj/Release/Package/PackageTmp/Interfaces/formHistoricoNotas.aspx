<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Site.Master" AutoEventWireup="true" CodeBehind="formHistoricoNotas.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.formHistoricoNotas" Async="true" %>

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
        <h4 class="well" style="background-color: rgb(8, 83, 148); color: white; text-align: center">Consulta Historico de  Notas</h4>

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
                        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" class="btn btn-success" OnClick="btnConsultar_Click" />

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
                                <asp:GridView ID="dgvInscrito" runat="server" AutoGenerateColumns="False" DataKeyNames="IdInscrito,IdNivelInscrito" CssClass="table table-bordered table-striped" EnablePersistedSelection="True" OnRowCommand="dgvInscrito_RowCommand" AllowPaging="True" PageSize="5" OnPageIndexChanging="dgvInscrito_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>

                                        <asp:BoundField DataField="NumDocInscrito" HeaderText="Identificacion" SortExpression="NumDocInscrito" ReadOnly="True" />
                                        <asp:BoundField DataField="IdInscrito" HeaderText="ID" SortExpression="IdInscrito" ReadOnly="True" />
                                        <asp:BoundField DataField="TipoEstudiante" HeaderText="Tipo Estudiante" SortExpression="TipoEstudiante" ReadOnly="True" />
                                        <asp:BoundField DataField="NombreInscrito" HeaderText="Nombres" SortExpression="NombreInscrito" ReadOnly="True" />
                                        <asp:BoundField DataField="ApellidoInscrito" HeaderText="Apellidos" SortExpression="ApellidoInscrito" ReadOnly="True" />
                                        <asp:BoundField DataField="NomNivel" HeaderText="Nivel" SortExpression="NomNivel" ReadOnly="True" />
                                        <asp:BoundField DataField="Promedio" HeaderText="Promedio" SortExpression="Promedio" ReadOnly="True" />
                                        <asp:BoundField DataField="IdNivelInscrito" HeaderText="IdNivelInscrito" SortExpression="IdNivelInscrito" Visible="false" />
                                        <asp:BoundField DataField="Periodo" HeaderText="Periodo" SortExpression="Periodo" ReadOnly="True" />
                                        <asp:ButtonField ButtonType="Button" Text="Ver Detalle" CommandName="VerNotas">
                                            <ControlStyle BackColor="#2568A3" BorderColor="Black" BorderStyle="Outset" CssClass="btn btn-sm" ForeColor="White" />
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
                                <label class="control-label"><strong>Período</strong></label>
                                <asp:TextBox runat="server" ID="txtPeriodo" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <asp:HiddenField ID="HiddenIdIns" runat="server" />
                            <asp:HiddenField ID="HiddenNivel" runat="server" />
                        </div>
                        <div class="col-md-12" id="cuerpo_pdf">
                            <div class="table-responsive">
                                <div class="form-group col-sm-12">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel1" runat="server">
                                                <asp:GridView ID="dgvNotas" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered table-striped" DataKeyNames="IDNota">
                                                    <Columns>
                                                        <asp:BoundField DataField="IDNota" HeaderText="id" SortExpression="IDNota" visible="false"/>
                                                        <asp:BoundField DataField="NomUnidad" HeaderText="Tema de Unidad" SortExpression="NomUnidad" />
                                                        <asp:BoundField DataField="Calificacion" HeaderText="Calificación" SortExpression="Calificacion" />
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

                <div class="col-md-12">
                    <div id="estado-promedio">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="col-xs-1">
                                    <label class="control-label"><strong>Promedio: </strong></label>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtPromedio" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <b>
                                        <asp:Label ID="lblEstadoNivel" runat="server"></asp:Label></b>
                                </div>
                                <div class=" col-md-offset-1" style="float: right">
                                    <asp:Button ID="btnPdfHistorico" runat="server" Text="Descargar Reporte" OnClientClick="pruebaDivAPdf();" class="btn btn-success" />
                                </div>

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
    <asp:Button ID="Button2" runat="server" Height="47px" Text="MOSTRAR POPUP" Width="258px" hidden="hidden" />
    <script>

        function getImgFromUrl(logo_url, callback) {
            var img = new Image();
            img.src = logo_url;
            img.onload = function () {
                callback(img);
            };
        }
        function generatePDF(img) {
            var fecImpresion =new  Date();
            var pdf = new jsPDF('p', 'pt', 'letter');
            pdf.addImage(img, 'JPEG', 360, 0, 250, 85);
            var Titulo = "HISTORICO DE NOTAS";
            var nombres = $("#<%=txtNombres.ClientID%>").val();
            var cedula = $("#<%=txtCed.ClientID%>").val();
            var Nivel = $("#<%=txtNivel.ClientID%>").val();
            var promedio = $("#<%=txtPromedio.ClientID%>").val();
            var estadoNivel = $('#<%=lblEstadoNivel.ClientID%>').html();
            var NotaFinal = "PROMEDIO: " + promedio + " " + estadoNivel;
            var periodo = $("#<%=txtPeriodo.ClientID%>").val();
            var elementHTML = $('#cuerpo_pdf')[0];
            var nombre_archivo = nombres + "_" + Nivel + "_" + periodo;
            var lineaSeparadora = "__________________________________________________________________";
            source = elementHTML;
            var fechaImp = String(fecImpresion.format("yyyy-MM-dd"));
            specialElementHandlers = {
                '#bypassme': function (element, renderer) {
                    return true
                }
            };
            pdf.text(10, 80, lineaSeparadora);
            pdf.text(10, 185, lineaSeparadora);
            //titulos
            pdf.setFontSize(12);
            pdf.setFontType('bold');
            pdf.text(250, 70, Titulo);
            pdf.setFontSize(10);
            pdf.text(30, 105, "CURSO: ");
            pdf.text(30, 120, "NIVEL: ");
            pdf.text(30, 135, "ESTUDIANTE: ");
            pdf.text(30, 150, "N° IDENTIFICACIÓN: ");
            pdf.text(30, 165, "PERIODO: ");
            pdf.text(390,165,"FECHA IMPRESIÓN: ")
            // contenido titulos
            pdf.setFontSize(10);
            pdf.setFontType('normal');
            pdf.text(135, 105, "INGLES AUTÓNOMO");           
            pdf.text(135, 120, Nivel);
            pdf.text(135, 135, nombres);
            pdf.text(135, 150, cedula);
            pdf.text(135, 165, periodo);
            pdf.text(500, 165, fechaImp);
            margins = {
                top: 215,
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
                    pdf.setFontSize(10);
                    pdf.setFontType('bold');
                    pdf.text(380, 400, NotaFinal);
                    pdf.save(nombre_archivo + '.pdf');
                }, margins
            );

        }

        function pruebaDivAPdf(){
            var logo_url = "/interfaces/images/uisek-mail.png";
            getImgFromUrl(logo_url, function (img) {
                generatePDF(img);
            });
        }


       

        

    </script>
</asp:Content>
