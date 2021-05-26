<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Site.Master" AutoEventWireup="true" CodeBehind="formInfoEstudiante.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.formEstudiante" async="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <title>Inscripcion</title>
       <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0">
         <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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
/*inicio update progress*/
        .overlay  
        {
          position: fixed;
          z-index: 98;
          top: 0px;
          left: 0px;
          right: 0px;
          bottom: 0px;
            background-color: #aaa; 
            filter: alpha(opacity=80); 
            opacity: 0.8; 
        }
        .overlayContent
        {
          z-index: 99;
          margin: 250px auto;
          width: 80px;
          height: 80px;
        }
        .overlayContent h2
        {
            font-size: 18px;
            font-weight: bold;
            color: #000;
        }
        .overlayContent img
        {
          width: 80px;
          height: 80px;
        }
/*fin*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container" style="width:80%" >
         <h4 class="well" style="background-color:rgb(8, 83, 148);color:white;text-align:center">Información Alumno</h4>
	    <div class="col-lg-12 well">
	    <div class="row">
            <div class="col-sm-12">   

                     <div class="row">
						<div class="form-group col-sm-6">
								<label>Identificación</label>
                                <asp:TextBox type="text" placeholder="Número de identificación" class="form-control" runat="server" ID="txtCed" readonly="true"></asp:TextBox>
						</div>
                     <div class="form-group col-sm-6">
						<label>Correo Electrónico</label>
						<asp:TextBox type="text" placeholder="Correo electrónico" class="form-control" runat="server" ID="txtEmail" readonly="true"></asp:TextBox>	
					</div>
                    </div>
						<div class="row">
							<div class="col-sm-6 form-group">
								<label>Nombres</label>
								<asp:TextBox placeholder="Nombres" class="form-control" required="required" runat="server" ID="txtNombres"  readonly="true"></asp:TextBox>
							</div>
							<div class="col-sm-6 form-group">
								<label>Apellidos</label>
								<asp:TextBox type="text" placeholder="Apellidos" class="form-control" runat="server" ID="txtApellidos" readonly="true"></asp:TextBox>
							</div>
						</div>

                         <div class="row">
            <br />
           <div class="col-md-12">
            <div class="table-responsive">
              <asp:HiddenField ID="hiidenIdNivelEstudiante" runat="server" />
            <asp:GridView ID="dgvEstudiante" runat="server"  BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False" DataKeyNames="IDINSCRITO"  CaptionAlign="Bottom"  EnablePersistedSelection="True" HorizontalAlign="Center" CssClass="table table-bordered table-striped" >
                <Columns>
                    
                    <asp:BoundField DataField="IDINSCRITO" HeaderText="ID" SortExpression="IDINSCRITO" />
                    <asp:BoundField DataField="PERIODO" HeaderText="PERIODO" SortExpression="PERIODO" />
                    <asp:BoundField DataField="DESCCURSO" HeaderText="Curso" SortExpression="DESCCURSO" />
                    <asp:BoundField DataField="CODCURSO" HeaderText="CODIGO" SortExpression="CODIGO" />
                    <asp:BoundField DataField="NOMNIVEL" HeaderText="Descripciòn Nivel" SortExpression="NOMNIVEL" />
                    <asp:BoundField DataField="DESCTIPONIVEL" HeaderText="Nivel" SortExpression="DESCTIPONIVEL"/>
                    <asp:BoundField DataField="COSTONIVEL" HeaderText="Costo Nivel" SortExpression="COSTONIVEL" dataformatstring="{0:0.00}"/>
                    <asp:BoundField DataField="PUNTAJEPRUEBA" HeaderText="Puntaje Prueba" SortExpression="PUNTAJEPRUEBA" />
                <%--<asp:BoundField DataField="IDNIVELESTUDIANTE" HeaderText="IDNivelEstudiante" SortExpression="IDNIVELESTUDIANTE" />--%>
                <asp:BoundField DataField="DESCESTADOESTUDIANTE" HeaderText="ESTADO PAGADO" SortExpression="DESCESTADOESTUDIANTE" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#085394" Font-Bold="True" ForeColor="#FFFFFF" HorizontalAlign=Center/>
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

                
          <asp:HiddenField ID="correcto" runat="server" />



   </div>    
</div>
</div>
        <!--crerar un update prpgress-->
</div> 
</asp:Content>
