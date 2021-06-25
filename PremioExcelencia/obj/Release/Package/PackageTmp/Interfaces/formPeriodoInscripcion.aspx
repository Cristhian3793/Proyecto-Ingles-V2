<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Interfaces/Site.Master" CodeBehind="formPeriodoInscripcion.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.formPeriodoInscripcion" Async="true" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                 <div class="container" style=" padding-left:10%;padding-right:10%;">
                     <h4 class="well" style="background-color:rgb(8, 83, 148);color:white;text-align:center">Periodos Pruebas de Ubicación</h4>
	                    <div class="col-lg-12 well">
	                        <div class="row">
                                <div class="col-sm-12 ">

                                    <div class="row">
                                    <div class="form-group col-sm-6">
                                        <asp:Label  Text="Período" runat="server"></asp:Label>
                                        <asp:TextBox ID="txtPeriodo" runat="server" class="form-control"></asp:TextBox>
                                     </div>
                                    <div class="form-group col-sm-6">
                                        <asp:Label  Text="Año Lectivo" runat="server"></asp:Label>
                                        <asp:TextBox ID="txtAnoLectivo" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    </div>
                                    <div class="row">
                                    <div class="form-group col-sm-6">
                                        <asp:Label  Text="Fecha Inicio" runat="server"></asp:Label>
                                        <asp:TextBox ID="txtFechaInicio" runat="server" type="date" class="form-control"></asp:TextBox>
                                    </div>                                   
                                    <div class="form-group col-sm-6">
                                        <asp:Label  Text="Fecha Fin" runat="server" ></asp:Label>
                                        <asp:TextBox ID="txtFechaFin" runat="server" type="date" class="form-control"></asp:TextBox>
                                    </div>

                                    </div>
                                      <div class="row">
                                        <div class="form-group col-sm-6">
                                          <asp:Label ID="Label1" runat="server"  Text="Activado :" Style="font-weight:bold"></asp:Label>
                                           <label class="switch">
                                           <input type="checkbox" runat="server" id="RabActivo"/>
                                          <span class="slider round"></span>
                                          </label>    
                                        </div>
                                    </div>
                                    <div class="form-group">
                                          <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Guardar" class="btn btn-success"/>
                                    </div>         
                                </div>
                             </div>
                        </div>
                 </div>
  <script type="text/javascript">
      $(document).ready(function () {
          var d = new Date();
          var fecha = d.format("yyyy-MM-dd");
            $("#<%=txtFechaInicio.ClientID%>").val(fecha);
            $("#<%=txtFechaFin.ClientID%>").val(fecha);
      });


      

       function confirm() {
           Swal.fire({
               icon: 'success',
               title: 'OK',
               text: 'El Registro se Guardo Corectamente!',
               footer: '<a href></a>'
           })
       }
      function ExistPeriodoActivo() {
          Swal.fire({
              icon: 'error',

              text: 'Ya existe un periodo activo!',
              footer: '<a href></a>'
          })
      }
  </script>
</asp:Content>


