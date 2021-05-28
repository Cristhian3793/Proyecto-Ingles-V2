<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Site.Master" AutoEventWireup="true" CodeBehind="formConsultaLicenciasCambridge_.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.formConsultaLicenciasCambridge_" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="width: 50%">
        <h4 class="well" style="background-color: rgb(8, 83, 148); color: white; text-align: center">Consulta de Licencias</h4>
        <div class="col-lg-12 well">
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group ">
                        <asp:Label ID="Label2" runat="server" Text="Libro"></asp:Label>
                        <asp:TextBox ID="txtLibro" runat="server"></asp:TextBox>
                        <asp:Button ID="Button1" runat="server" Text="Buscar" class="btn btn-success" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:GridView ID="dgvNiveles" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False" DataKeyNames="IDNIVEL,IDLIBRO" HorizontalAlign="Center" OnRowCommand="dgvNiveles_RowCommand" CssClass="table table-bordered table-striped">
                                <Columns>
                                    <asp:BoundField DataField="IDNIVEL" HeaderText="IDNIVEL" SortExpression="IDNIVEL" ReadOnly="True" />
                                    <asp:BoundField DataField="IDLIBRO" HeaderText="IDLIBRO" SortExpression="IDLIBRO" ReadOnly="True" visible="false"/>
                                    <asp:BoundField DataField="NOMNIVEL" HeaderText="NOMNIVEL" SortExpression="NOMNIVEL" ReadOnly="True" />
                                    <asp:BoundField DataField="CODNIVEL" HeaderText="CODNIVEL" SortExpression="CODNIVEL" ReadOnly="True" />
                                    <asp:BoundField DataField="NOMLIBRO" HeaderText="NOMLIBRO" SortExpression="NOMLIBRO" ReadOnly="True" />
                                    <asp:ButtonField ButtonType="Button" Text="Ver Licencias" CommandName="VerLicencias" />
                                </Columns>
                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                <HeaderStyle BackColor="#085394" Font-Bold="True" ForeColor="#FFFFFF" HorizontalAlign="Center" />
                                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
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
                                        <div class="col-sm-6">
                                        <label class="control-label"><strong>Activas</strong></label>
                                        <asp:DropDownList ID="cbxActivas" runat="server" class="form-control" AutoPostBack="true">
                                        </asp:DropDownList>
                                        </div>
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
                                                                    <asp:BoundField DataField="ESTADO" HeaderText="ESTADO" SortExpression="ESTADO" />
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
