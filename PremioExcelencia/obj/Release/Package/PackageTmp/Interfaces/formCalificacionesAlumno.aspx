<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Site.Master" AutoEventWireup="true" CodeBehind="formCalificacionesAlumno.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.formCalificacionesAlumno" async="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                    <div class="row">
                        <br />
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="dgvInscrito" runat="server" AutoGenerateColumns="False" DataKeyNames="IdInscrito,IdNivelInscrito,NumDocInscrito" CssClass="table table-bordered table-striped" EnablePersistedSelection="True" OnRowCommand="dgvInscrito_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="true" OnPageIndexChanging="dgvInscrito_PageIndexChanging" PageSize="10">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>

                                        <asp:BoundField DataField="NumDocInscrito" HeaderText="Identificacion" SortExpression="NumDocInscrito" ReadOnly="True" visible="false"/>
                                        <asp:BoundField DataField="IdInscrito" HeaderText="ID" SortExpression="IdInscrito" ReadOnly="True" visible="false"/>
                                        <asp:BoundField DataField="TipoEstudiante" HeaderText="Tipo Estudiante" SortExpression="TipoEstudiante" ReadOnly="True" visible="false"/>
                                        <asp:BoundField DataField="NombreInscrito" HeaderText="Nombres" SortExpression="NombreInscrito" ReadOnly="True" visible="false"/>
                                        <asp:BoundField DataField="ApellidoInscrito" HeaderText="Apellidos" SortExpression="ApellidoInscrito" ReadOnly="True" visible="false"/>
                                        <asp:BoundField DataField="NomNivel" HeaderText="Nivel" SortExpression="NomNivel" ReadOnly="True" />
                                        <%--<asp:BoundField DataField="Promedio" HeaderText="Promedio" SortExpression="Promedio" ReadOnly="True" />--%>
                                        <asp:BoundField DataField="IdNivelInscrito" HeaderText="IdNivelInscrito" SortExpression="IdNivelInscrito" Visible="false" />
                                        <asp:BoundField DataField="Periodo" HeaderText="Periodo" SortExpression="Periodo" ReadOnly="True" />
                                        <asp:ButtonField ButtonType="Button" Text="Ver Detalle" CommandName="VerNotas">
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
                                                        <asp:BoundField DataField="IDNota" HeaderText="id" SortExpression="IDNota" />
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
</asp:Content>
