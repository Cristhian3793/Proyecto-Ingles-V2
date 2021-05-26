using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Conexion
{
    public class conexionBaseExterna
    {
        public static SqlConnection CreateConnection()
        {
            string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PortalEstudiantesUISEK_NV"].ConnectionString;
            return new SqlConnection(_connectionString);
        }
        public static SqlConnection CreateConnectionBBDDINGLES()
        {
            string _connectionString = System.Configuration.ConfigurationManager.AppSettings["connection_string"];
            return new SqlConnection(_connectionString);
        }
        public static DataSet GetDatosAlumno(string numcedula)//obtiene datos de alumno uisek
        {
            SqlConnection myConnection = CreateConnection();
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();

            string sql;
            sql = "select t2.Nombre,t2.[Apellido 1],t2.[Apellido 2],t2.[Home Page],t1.Cedula,t2.DireccionPadre,t2.DireccionMadre,t2.DireccionTutor,t2.[Movil alumno],t2.[Telefono Emergencias] from [Curso escolar alumno] t1 inner join Customer t2 on t1.Cedula = t2.[VAT Registration No_] where CodCentro in (45, 130, 100) and t1.Estado = 2 and cedula=" + "'" + numcedula + "' and [Home Page]<>''";
            SqlCommand comando = new SqlCommand(sql, myConnection);
            SqlDataAdapter lector = default(SqlDataAdapter);
            comando.Connection = myConnection;
            comando.CommandType = CommandType.Text;
            comando.CommandTimeout = 99999999;

            DataSet ds = new DataSet();
            try
            {
                lector = new SqlDataAdapter(comando);

                lector.Fill(ds);

            }
            catch (Exception ex)
            {
                lector = null;
            }
            finally
            {
                if (myConnection.State == ConnectionState.Open)
                    myConnection.Close();
            }
            return (ds);
        }
        public static DataSet GetPruebas()
        {
            SqlConnection myConnection = CreateConnectionBBDDINGLES();
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();

            string sql;
            sql = "select t1.IDINSCRITO,t1.NUMDOCINSCRITO,t5.DESCRTIPOESTUDIANTE,t6.PERIODO,t1.FECHAREGISTRO,t8.DESCESTADOPRUEBA,t1.NOMBINSCRITO,t1.APELLIINSCRITO,t1.CELUINSCRITO,t1.TELEFINSCRITO,t1.EMAILINSCRITO,t7.DESCESTESTUDIANTE,t3.PUNTAJEPRUEBA from INSCRITO_AUTONOMO t1 inner join NIVELESINSCRITO t2 on t1.IDINSCRITO = t2.IDINSCRITO inner join PRUEBA t3 on t2.IDINSCRITO = t3.IDINSCRITO inner join NIVEL t4 on t2.IDNIVEL = t4.IDNIVEL inner join TIPO_ESTUDIANTE t5 on t1.IDTIPOESTUDIANTE = t5.IDTIPOESTUDIANTE inner join PERIODO_INSCRIPCION t6 on t1.IDPERIODOINSCRIPCION = t6.IDPERIODOINSCRIPCION inner join ESTADO_ESTUDIANTE t7 on t2.IDESTADONIVEL = t7.CODESTADOESTU inner join ESTADO_PRUEBA t8 on t1.ESTADOPRUEBA = t8.CODESTADOPRUEBA where t2.PRUEBA = 1";
            SqlCommand comando = new SqlCommand(sql, myConnection);
            SqlDataAdapter lector = default(SqlDataAdapter);
            comando.Connection = myConnection;
            comando.CommandType = CommandType.Text;
            comando.CommandTimeout = 99999999;

            DataSet ds = new DataSet();
            try
            {
                lector = new SqlDataAdapter(comando);
                lector.Fill(ds);
            }
            catch (Exception ex)
            {
                lector = null;
            }
            finally
            {
                if (myConnection.State == ConnectionState.Open)
                    myConnection.Close();
            }
            return (ds);
        }


    }
}
