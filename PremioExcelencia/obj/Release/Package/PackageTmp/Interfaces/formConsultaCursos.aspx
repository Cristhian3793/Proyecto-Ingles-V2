<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Interfaces/Site.Master" CodeBehind="formConsultaCursos.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.formConsultaCursos" async="true"%>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
                 <div class="container" style=" padding-left:10%;padding-right:10%;">
                     <h4 class="well" style="background-color:rgb(8, 83, 148);color:white;text-align:center">Consulta Cursos</h4>
	<div class="col-lg-12 well">
	<div class="row">
        <div class="col-sm-12">

                                <div class="form-group" >
                                <asp:Label ID="Label2" runat="server" Text="Cod Curso"></asp:Label>
                                    <asp:TextBox ID="txtCodCurso" runat="server"  Width="179px"></asp:TextBox>
                                    <asp:Button ID="btnConsultar" runat="server" OnClick="btnConsultar_Click" Text="Consultar"  class="btn btn-success" />
                                </div>
             <div class="container" style="padding-left:20%">
                <div class="row" >
                    <div class="col-md-12" >
            <div class="table-responsive" >
                                    <asp:GridView ID="dgvCursos" runat="server" AutoGenerateColumns="False" OnPageIndexChanged="dgvCursos_PageIndexChanged" OnPageIndexChanging="dgvCursos_PageIndexChanging" CellPadding="4" BackColor="White" BorderColor="#3366CC" BorderStyle="None">
                                        <Columns>
                                            <asp:BoundField DataField="IDCURSO" HeaderText="Id Curso" SortExpression="IDCURSO" />
                                            <asp:BoundField DataField="CODCURSO" HeaderText="Codigo" SortExpression="CODCURSO" />
                                            <asp:BoundField DataField="DESCCURSO" HeaderText="Curso" SortExpression="DESCCURSO" />
                                            <asp:BoundField DataField="FECHACREACIONCURSO" HeaderText="Fecha Creacion" SortExpression="FECHACREACIONCURSO" />
                                        </Columns>
                                                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
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
        </div>
        </div>
                     </div>
    
</asp:Content>

