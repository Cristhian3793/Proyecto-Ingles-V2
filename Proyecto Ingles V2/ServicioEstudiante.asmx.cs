using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using Logica.Conexion;
namespace Proyecto_Ingles_V2
{
    /// <summary>
    /// Descripción breve de ServicioEstudiante
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class ServicioEstudiante : System.Web.Services.WebService
    {

        //[WebMethod]
        //public string HelloWorld()
        //{
        //    return "Hola a todos";
        //}
        [WebMethod]
        public string[] BuscaEstudiante(string cedula)
        {
            string[] resultado = new string[6];
            string apellidos;
            DataSet ds = conexionBaseExterna.GetDatosAlumno(cedula);
            if (ds.Tables[0].Rows.Count > 0)
            {
                apellidos= ds.Tables[0].Rows[0]["Apellido 1"].ToString()+" "+ ds.Tables[0].Rows[0]["Apellido 2"].ToString();
                resultado[0] = ds.Tables[0].Rows[0]["Nombre"].ToString();
                resultado[1] = apellidos;
                resultado[2] = ds.Tables[0].Rows[0]["DireccionPadre"].ToString(); 
                resultado[3] = ds.Tables[0].Rows[0]["Movil alumno"].ToString();
                resultado[4] = ds.Tables[0].Rows[0]["Telefono Emergencias"].ToString();
                resultado[5] = ds.Tables[0].Rows[0]["Home Page"].ToString();
            }
            else
            {
                resultado[0] = "No Existe";
            }
            return resultado;
        }
    }
}
