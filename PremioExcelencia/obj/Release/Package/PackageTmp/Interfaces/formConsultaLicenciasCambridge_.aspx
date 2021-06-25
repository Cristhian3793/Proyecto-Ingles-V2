<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Site.Master" AutoEventWireup="true" CodeBehind="formConsultaLicenciasCambridge_.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.formConsultaLicenciasCambridge_" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        td {
            padding: 10px;
            color: black;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="width: 50%">
        <h4 class="well" style="background-color: rgb(8, 83, 148); color: white; text-align: center">Consulta de Licencias</h4>
        <div class="col-lg-12 well">
            <div class="row">
                <div class="col-sm-12">
                    <div class="container" style="background: #EAE8E8; padding: 5px; width: 100%; border: solid 1px #DBDADA; border-radius: 5px">
                        <div class="row">
                            <div class="col-sm-2">
                                <b><asp:Label ID="Label2" runat="server" Text="Nivel"></asp:Label></b>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:DropDownList ID="cbxNiveles" AppendDataBoundItems="true" runat="server" AutoPostBack="true" class="form-control">
                                        <asp:ListItem Text="-Todos-" Value="0" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click" CssClass="btn btn-success"/>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="dgvNiveles" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False" DataKeyNames="IDNIVEL,IDLIBRO" HorizontalAlign="Center" OnRowCommand="dgvNiveles_RowCommand" CssClass="table table-bordered table-striped" AllowPaging="true" PageSize="8" OnPageIndexChanging="dgvNiveles_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="IDNIVEL" HeaderText="ID NIVEL" SortExpression="IDNIVEL" ReadOnly="True" />
                                        <asp:BoundField DataField="IDLIBRO" HeaderText="IDLIBRO" SortExpression="IDLIBRO" ReadOnly="True" Visible="false" />
                                        <asp:BoundField DataField="NOMNIVEL" HeaderText="NIVEL" SortExpression="NOMNIVEL" ReadOnly="True" />
                                        <asp:BoundField DataField="CODNIVEL" HeaderText="COD NIVEL" SortExpression="CODNIVEL" ReadOnly="True" />
                                        <asp:BoundField DataField="NOMLIBRO" HeaderText="LIBRO" SortExpression="NOMLIBRO" ReadOnly="True" />
                                        <asp:ButtonField ButtonType="Button" Text="Ver Licencias" CommandName="VerLicencias" />
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
                </div>
            </div>
        </div>
    </div>
    <!--inicio modal para actualizaciones-->
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                                <div class="row-fluid">
                                    <div class="form-group col-sm-12">
                                        <div class="col-sm-6">
                                            <label class="control-label"><strong>Nivel</strong></label>
                                            <asp:TextBox ID="txtNivel" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-6">
                                            <label class="control-label"><strong>Libro</strong></label>
                                            <asp:TextBox ID="txtLibro_" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                       <!-- <div class="col-sm-6">
                                            <label class="control-label"><strong>Activas</strong></label>
                                            <asp:DropDownList ID="cbxActivas" runat="server" class="form-control" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>-->
                                        <asp:HiddenField ID="hiddidLIbro" runat="server" />
                                        <asp:HiddenField ID="hiddidNivel" runat="server" />
                                    </div>

                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <div class="form-group col-sm-12">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Panel ID="Panel1" runat="server">
                                                            <asp:GridView ID="dgvLicencias" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered table-striped" DataKeyNames="IDLICENCIA">
                                                                <Columns>
                                                                    <asp:BoundField DataField="IDLICENCIA" HeaderText="id" SortExpression="IDLICENCIA" />
                                                                    <asp:BoundField DataField="LICENCIA" HeaderText="LICENCIA" SortExpression="LICENCIA" />
                                                                    <%--<asp:BoundField DataField="ESTADO" HeaderText="ESTADO" SortExpression="ESTADO" />--%>
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
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                    <asp:Button class="btn btn-primary" ID="btnactualizar" runat="server" Text="Guardar Cambios" ispostback="true" />
                </div>
            </asp:Panel>
            <asp:Button ID="btnPopUp" runat="server" Height="47px" Text="MOSTRAR POPUP"
                Width="258px" hidden="hidden" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <!--fin modal-->
</asp:Content>
