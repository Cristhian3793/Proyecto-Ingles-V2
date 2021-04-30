<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Site.Master" AutoEventWireup="true" CodeBehind="formNivelesAutonomos.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.formNivelesAutonomos" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style>
                .switch {
  position: relative;
  display: inline-block;
  width: 50px;
  height: 20px;
}

.switch input { 
  opacity: 0;
  width: 0;
  height: 0;
}

.slider {
  position: absolute;
  cursor: pointer;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: #ccc;
  -webkit-transition: .4s;
  transition: .4s;
}

.slider:before {
  position: absolute;
  content: "";
  height: 12px;
  width: 12px;
  left: 4px;
  bottom: 4px;
  background-color: white;
  -webkit-transition: .4s;
  transition: .4s;
}

input:checked + .slider {
  background-color: #2196F3;
}

input:focus + .slider {
  box-shadow: 0 0 1px #2196F3;
}

input:checked + .slider:before {
  -webkit-transform: translateX(26px);
  -ms-transform: translateX(26px);
  transform: translateX(26px);
}

/* Rounded sliders */
.slider.round {
  border-radius: 34px;
}

.slider.round:before {
  border-radius: 50%;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
                     <div class="container" style="padding-left:10%;padding-right:10%;">
                     <h4 class="well" style="background-color:rgb(8, 83, 148);color:white;text-align:center">Ingreso Niveles Ingles Autonomo</h4>
	                <div class="col-lg-12 well">
	                <div class="row">
                        <div class="col-sm-12">
                                <div class="row">
                                <div class="form-group col-sm-6">
                                 <asp:Label ID="Label5" runat="server" Text="Codigo Nivel"></asp:Label>
                                <asp:TextBox ID="txtcodNivel" runat="server" class="form-control"></asp:TextBox>
                                </div>

                                 <div class="form-group col-sm-6">
                                <asp:Label ID="Label4" runat="server" Text="Curso"></asp:Label>
                                <asp:DropDownList ID="cbxCursos" AppendDataBoundItems="true" runat="server" AutoPostBack="true" class="form-control">
                                <asp:ListItem Text="--seleccionar--" Value="0" />
                                </asp:DropDownList>
                                </div>
                                </div>
                                <div class="row">

                                <div class="form-group col-sm-6">
                                 <asp:Label ID="Label6" runat="server" Text="Nombre Nivel"></asp:Label>
                                <asp:TextBox ID="txtNomNivel" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group col-sm-6">
                                <asp:Label ID="Label3" runat="server" Text="Nivel"></asp:Label>
                                <%--<asp:TextBox ID="txtNivel" runat="server" TextMode="Number" class="form-control"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlNivel" AppendDataBoundItems="true" runat="server" AutoPostBack="true" class="form-control">
                                <asp:ListItem Text="1" Value="1" />
                                <asp:ListItem Text="2" Value="2" />
                                <asp:ListItem Text="3" Value="3" />
                                <asp:ListItem Text="4" Value="4" />
                                <asp:ListItem Text="5" Value="5" />
                                <asp:ListItem Text="6" Value="6" />
                                <asp:ListItem Text="7" Value="7" />
                                <asp:ListItem Text="8" Value="8" />
                                <asp:ListItem Text="9" Value="9" />
                                </asp:DropDownList>
                                </div>
                                </div>
                                <div class="row">
                                <div class="form-group col-sm-6">
                                 <asp:Label ID="Label1" runat="server" Text="Estado Nivel"></asp:Label>
                                   <label class="switch">
                                   <input type="checkbox" runat="server" id="RabActivo"/>
                                   <span class="slider round"></span>
                                  </label>    
                                </div>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="Button1" runat="server" Text="Guardar Nivel"  class="btn btn-success" OnClick="Button1_Click" />
                                </div>
                          </div>
                    </div>
                </div>
            </div>
</asp:Content>
