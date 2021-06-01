<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Interfaces/Site.Master" CodeBehind="formNiveles.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.formNivelesProgramado" async="true"%>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Ingreso Nivel Programado</title>
     <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10.16.5/dist/sweetalert2.all.min.js" type="text/javascript"></script>
    <link href="../Content/sweetalert.css" rel="stylesheet"/>
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
                     <h4 class="well" style="background-color:rgb(8, 83, 148);color:white;text-align:center">Ingreso Niveles</h4>
	                <div class="col-lg-12 well">
	                <div class="row">
                        <div class="col-sm-12">
                                <div class="row">
                                    <div class="form-group col-sm-6">
                                        <b><asp:Label ID="Label7" runat="server" Text="Tipo Nivel" ></asp:Label></b>
                                         <asp:DropDownList ID="cbxTipoNivel" runat="server" class="form-control" AppendDataBoundItems="true">
                                             <%--<asp:ListItem Text="--seleccionar--" Value="0" />--%>
                                        </asp:DropDownList>
                                    </div>
                                         <div class="form-group col-sm-6">
                                         <asp:Label ID="Label5" runat="server" Text="Código Producto"></asp:Label>
                                        <asp:TextBox ID="txtcodNivel" runat="server" class="form-control" required="true"></asp:TextBox>
                                        </div>
                                </div>
                                <div class="row">
                                  <div class="form-group col-sm-6">
                                    <asp:Label ID="Label6" runat="server" Text="Nombre Nivel"></asp:Label>
                                    <asp:TextBox ID="txtNomNivel" runat="server" class="form-control"></asp:TextBox>
                                  </div>
                                <div class="form-group col-sm-6">
                                <asp:Label ID="Label4" runat="server" Text="Curso"></asp:Label>
                                <asp:DropDownList ID="cbxCursos"  runat="server" AutoPostBack="true" class="form-control">
                                <%--<asp:ListItem Text="--seleccionar--" Value="0" />--%>
                                </asp:DropDownList>
                                </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-sm-6">
                                    <asp:Label ID="Label3" runat="server" Text="Nivel"></asp:Label>
                                    <asp:DropDownList ID="ddlNivel" AppendDataBoundItems="true" runat="server" AutoPostBack="true" class="form-control">
                                    <asp:ListItem Text="1" Value="1" />
                                    <asp:ListItem Text="2" Value="2" />
                                    <asp:ListItem Text="3" Value="3" />
                                    <asp:ListItem Text="4" Value="4" />
                                    <asp:ListItem Text="5" Value="5" />
                                    <asp:ListItem Text="6" Value="6" />
                                    <asp:ListItem Text="7" Value="7" />
                                    <asp:ListItem Text="8" Value="8" />
                                    </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-sm-6">
                                    <asp:Label ID="Label2" runat="server" Text="Libro"></asp:Label>
                                     <asp:DropDownList ID="ddlLibros" runat="server" AutoPostBack="true" class="form-control">
                                        <%--<asp:ListItem Text="--Seleccionar--" Value="0" />--%>
                                    </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                 <div class="form-group col-sm-6">
                                    <asp:Label ID="Label8" runat="server" Text="Costo Nivel"></asp:Label>
                                    <asp:TextBox ID="txtCostoNivel" runat="server" class="form-control" required="true" onkeypress="return NumCheck(event, this)" onpaste="return false"></asp:TextBox>
                                </div>
                                <div class="form-group col-sm-6">
                                    <asp:Label ID="Label1" runat="server" Text="Estado Nivel"></asp:Label>
                                   <div class="row">
                                       <div class="col-md-4">
                                   <label class="switch">
                                   <input type="checkbox" runat="server" id="RabActivo"/>
                                   <span class="slider round"></span>
                                  </label>   
                                       </div>
                                </div>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <asp:Button ID="Button1" runat="server" Text="Guardar Nivel"  class="btn btn-success" OnClick="Button1_Click"/>
                                </div>
                                </div>
                    </div>
                </div>
            </div>
   <script type="text/javascript">
       function confirm() {
           Swal.fire({
               icon: 'success',
               title: 'OK',
               text: 'El Registro se Guardo Corectamente!',
               footer: '<a href></a>'
           })
       }
       function existe() {
           Swal.fire({
               icon: 'error',
               text: 'Ya exite un registro con los mismos datos',
               footer: '<a href></a>'
           })
       }
       function error() {
           Swal.fire({
               icon: 'error',
               text: 'No se pudo guardar',
               footer: '<a href></a>'
           })
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


