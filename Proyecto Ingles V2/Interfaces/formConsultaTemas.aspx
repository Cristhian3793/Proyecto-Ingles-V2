<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Site.Master" AutoEventWireup="true" CodeBehind="formConsultaTemas.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.ConsultaTemas" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
        td {
            padding: 10px;
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h4 class="well" style="background-color: rgb(8, 83, 148); color: white; text-align: center">Consulta Temas Nivel</h4>
        <div class="col-lg-12 well">
            <div class="row">
                <div class="col-sm-12">
                    <div class="container" style="background: #EAE8E8; padding: 5px; width: 100%; border: solid 1px #DBDADA; border-radius: 5px">
                        <div class="row">
                            <div class="col-sm-2">
                                <asp:Label ID="Label1" runat="server" Text="Nivel"></asp:Label>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlNivel" AppendDataBoundItems="true" runat="server" AutoPostBack="true" class="form-control">
                                        <asp:ListItem Text="--TODOS--" Value="0" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click"  CssClass="btn btn-success"/>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="dgvNivel" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="false" DataKeyNames="idNivel" CssClass="table table-bordered table-striped" OnRowCommand="dgvTema_RowCommand" PageSize="10" AllowPaging="true" OnPageIndexChanging="dgvNivel_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="idNivel" HeaderText="idNivel" SortExpression="idNivel" />
                                        <asp:BoundField DataField="estadoNivel" HeaderText="ESTADO NIVEL" SortExpression="estadoNivel" />
                                        <asp:BoundField DataField="nomCurso" HeaderText="Curso" SortExpression="nomCurso" />
                                        <asp:BoundField DataField="codNivel" HeaderText="Codigo Producto" SortExpression="codNivel" />
                                        <asp:BoundField DataField="nomNivel" HeaderText="Nombre" SortExpression="nomNivel" />
                                        <asp:BoundField DataField="descNivel" HeaderText="Nivel" SortExpression="descNivel" />
                                        <asp:BoundField DataField="tipoNivel" HeaderText="Tipo Nivel" SortExpression="tipoNivel" />
                                        <asp:BoundField DataField="libro" HeaderText="Libro" SortExpression="libro" />
                                        <asp:ButtonField ButtonType="Button" Text="Temas Unidad" CommandName="Editar" />
                                        <asp:TemplateField HeaderText="acciones">
                                            <ItemTemplate>
                                                <asp:Button CommandName="Delete" runat="server" Text="Eliminar" OnClientClick="return  confirmdelete(this);" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#99CCCC"
                                        ForeColor="#003399" />
                                    <HeaderStyle BackColor="#085394" Font-Bold="True" ForeColor="#CCCCFF" />
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
                </div>
                <div class="modal-body">
                    <div class="container-fluid well">
                        <div class="row-fluid">
                            <div class="span4">

                                <div class="form-group col-sm-12">
                                    <label class="control-label"><strong>Nivel</strong></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtnivel" runat="server" CssClass="form-control"
                                            placeholder="nivel" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group col-sm-12">
                                    <asp:GridView ID="dgvTemaUnidad" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered table-striped"  DataKeyNames="idNomUnidad">
                                        <Columns>
                                            <asp:BoundField DataField="idNomUnidad" HeaderText="id" SortExpression="idNomUnidad" />
                                            <asp:BoundField DataField="nomUnidad" HeaderText="Tema de Unidad" SortExpression="nomUnidad" />
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
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                    <asp:Button class="btn btn-primary" ID="Button1" runat="server" Text="Enviar" />
                </div>
            </asp:Panel>
            <asp:Button ID="Button2" runat="server" Height="47px" Text="MOSTRAR POPUP"
                Width="258px" hidden="hidden" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <!--fin modal-->
</asp:Content>
