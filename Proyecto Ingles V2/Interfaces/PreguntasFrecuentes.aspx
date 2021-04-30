<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreguntasFrecuentes.aspx.cs" Inherits="Proyecto_Ingles_V2.Interfaces.PreguntasFrecuentes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
<!-- JavaScript Bundle with Popper -->
<!-- CSS only -->
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <link href="../Styles/MyStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">


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


 @import url(https://fonts.googleapis.com/css?family=Open+Sans:300,800);






.content {
  width: 80%;
  padding: 20px;
  margin: 0 auto;
  padding: 0 60px 0 0;
}

.centerplease {
  margin: 0 auto;
  max-width: 270px;
  font-size: 20px;
}

.question {
  position: relative;
  background: #085394;
  margin: 0;
  padding: 10px 10px 10px 50px;
  display: block;
  width:100%;
  cursor: pointer;
  color:white;
}

.answers {
  background: #EEEEEE;
  padding: 0px 15px;
  margin: 5px 0;
  height: 0;
  overflow: hidden;
  z-index: -1;
  position: relative;
  opacity: 0;
  -webkit-transition: .7s ease;
  -moz-transition: .7s ease;
  -o-transition: .7s ease;
  transition: .7s ease;
}

.questions:checked ~ .answers{
  height: auto;
  opacity: 1;
  padding: 15px;
}

.plus {
  position: absolute;
  margin-left: 10px;
  z-index: 5;
  font-size: 2em;
  line-height: 100%;
  -webkit-user-select: none;    
  -moz-user-select: none;
  -ms-user-select: none;
  -o-user-select: none;
  user-select: none;
  -webkit-transition: .3s ease;
  -moz-transition: .3s ease;
  -o-transition: .3s ease;
  transition: .3s ease;
}

.questions:checked ~ .plus {
  -webkit-transform: rotate(45deg);
  -moz-transform: rotate(45deg);
  -o-transform: rotate(45deg);
  transform: rotate(45deg);
}

.questions {
  display: none;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
                   <!--header-->
          <nav class="navbar-head">
            <div style="float:left;width:15%;height:100%;" class="nomAplicacion">
           <a href="../Login/formLogin.aspx">
               <asp:Image src="../images/logo-sek-2.png" alt="Dont exist image" runat="server" style="width:100%;height:100%"/>
            </a>
            </div>
              <p style="float:right">
               REGISTRO INGLES AUTÓNOMO
              </p>
               <br />
              <br />
          </nav>
        <nav class="subnav">
        <div style="height:50px">
        </div>
        </nav>
        <!--fin header-->

<div class="content">
        <div>

<div class="container" style="padding-top:100px">
  <div class='centerplease' >
  <p>PREGUNTAS FRECUENTES</p>
</div>
<br>


<div>
  <input type="checkbox" id="question1" name="q"  class="questions">
  <div class="plus">+</div>
  <label for="question1" class="question">
    Costos de niveles
  </label>
  <div class="answers">
    $100
  </div>
</div>

<div>
  <input type="checkbox" id="question2" name="q" class="questions">
  <div class="plus">+</div>
  <label for="question2" class="question">
Fechas de Matricula
  </label>
  <div class="answers">
    20-21-22
  </div>
</div>
  
<div>
  <input type="checkbox" id="question3" name="q" class="questions">
  <div class="plus">+</div>
  <label for="question3" class="question">
   Periodos de Inscripcion
  </label>
  <div class="answers">
    This is the answer!
  </div>
</div>
</div>
        </div>
            </div>
             <div class="footer-content" style="padding:0.5%;">
    <footer >
        <p style="color:white;font-size:14px"><span class="glyphicon glyphicon-copyright-mark"></span> 2021 - Recursos Tecnológicos </p>
    </footer>
    </div> 
    </form>
</body>
</html>
