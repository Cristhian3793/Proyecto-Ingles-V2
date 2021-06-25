
//Validar el ingreso de notas
function valid(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    console.log(tecla);

    if (tecla == 8) return true;
    if (tecla == 44 || tecla == 46 || tecla == 27) {
        return true;
    }
    if ((tecla >= 48 && e.keyCode <= 57) && !event.shiftKey) {
        patron = /^\d*$/;
        te = String.fromCharCode(tecla);
        return patron.test(te);
    } else if (e.keyCode >= 96 && e.keyCode <= 105) {
        return true;
    }
    else
        return false;
}
//Valida que solo se ingrensen numeros, sin puntos ni comas ni caracteres especiales
function numeros(evt) {
    var theEvent = evt || window.event;
    var key = theEvent.keyCode || theEvent.which;
    if (key == 8 || key==46) return true;
    key = String.fromCharCode(key);
    var regex = /[0-9]/;
    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault) theEvent.preventDefault();
    }
}


function validar(e) {

    tecla = e.which || e.keyCode;
    if (tecla == 8) return false;
    patron = /\d &&/;

    te = String.fromCharCode(tecla);

    return (patron.test(te) || tecla == 9 || tecla == 8);
}

function confirmar_archivos() {

    var exito = -1;
    
    if ($("#arch_URL1").is(':visible') && $("#arch_URL1").val() != "") {
        var size1 = document.getElementById('arch_URL1').files[0].size;
        if (size1 > 2465792 ){
            $("#arch_URL1").val("");
            alert('Solo se aceptan archivos tipo PDF de hasta 2 mb.');
            return false;
        } else {
            exito = 1;
        }
    }
    if ($("#arch_URL1").is(':visible') && $("#arch_URL2").val() != "") {
        var size2 = document.getElementById('arch_URL2').files[0].size;
        if (size2 > 2465792) {
            $("#arch_URL2").val("");
            alert('Solo se aceptan archivos tipo PDF de hasta 2 mb.');
            return false;
        } else {
            exito = 1;
        }
    }
    if ($("#arch_URL1").is(':visible') && $("#arch_URL3").val() != "") {
        var size3 = document.getElementById('arch_URL3').files[0].size;
        if (size3 > 2465792) {
            $("#arch_URL3").val("");
            alert('Solo se aceptan archivos tipo PDF de hasta 2 mb.');
            return false;
        } else {
            exito = 1;
        }
    }
    if ($("#arch_URL1").is(':visible') && $("#arch_URL4").val() != "") {
        var size4 = document.getElementById('arch_URL4').files[0].size;
        if (size4 > 2465792) {
            $("#arch_URL4").val("");
            alert('Solo se aceptan archivos tipo PDF de hasta 2 mb.');
            return false;
        } else {
            exito = 1;
        }
    }
    if ($("#arch_URL1").is(':visible') && $("#arch_URL5").val() != "") {
        var size5 = document.getElementById('arch_URL5').files[0].size;
        if (size5 > 2465792) {
            $("#arch_URL5").val("");
            alert('Solo se aceptan archivos tipo PDF de hasta 2 mb.');
            return false;
        } else {
            exito = 1;
        }
    }
    if (exito = 1) {
        var confirm_value = document.createElement('INPUT');
        confirm_value.type = 'hidden';
        confirm_value.name = 'confirm_value';
        if (confirm('Está seguro que desea grabar los datos ingresados incluyendo los respaldos de su trámite.')) {
            confirm_value.value = 'Yes';
            document.forms[0].appendChild(confirm_value);
            Page_ClientValidate("Grupo_1");
            if (Page_IsValid)
                return true;
            else
                alert('El formulario está incompleto, revise los campos obligatorios.');
                return false;
        }
        else {
            confirm_value.value = 'No';
            return false;
        }
    }
   
}
//función que valida el ingreso de números para valores monetarios ( n unidades, 2 decimales)
function validateFloatKeyPress(el, evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    var number = el.value.split('.');
    if (charCode == 13)
        return false;

    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    //solo un punto
    if (number.length > 1 && charCode == 46) {
        return false;
    }
    //consigue la posicion de un caracter
    var caratPos = getSelectionStart(el);
    var dotPos = el.value.indexOf(".");
    if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
        return false;
    }
    return true;
}

function getSelectionStart(o) {
    if (o.createTextRange) {
        var r = document.selection.createRange().duplicate()
        r.moveEnd('character', o.value.length)
        if (r.text == '') return o.value.length
        return o.value.lastIndexOf(r.text)
    } else return o.selectionStart
}
//fin función