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
             public static DataSet GetInscritos()
        {
            SqlConnection myConnection = CreateConnectionBBDDINGLES();
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();

            string sql;
            sql = "select t1.IDINSCRITO as CODESTUDIANTE,t1.NUMDOCINSCRITO IDENTIFICACION," +
                "T4.DESCRTIPOESTUDIANTE TIPOESTUDIANTE, T1.NOMBINSCRITO NOMBRES," +
                "T1.APELLIINSCRITO APELLIDOS, T1.CELUINSCRITO CELULAR," +
                "T1.TELEFINSCRITO TELEFONO, T1.EMAILINSCRITO EMAIL, T1.FECHAREGISTRO FECHAINSCRIPCION, T5.PERIODO PERIODO, T3.NOMNIVEL," +
                "T6.DESCESTESTUDIANTE ESTADONIVEL, T7.DESCESTADOPRUEBA PRUEBA " +
                "from INSCRITO_AUTONOMO t1 inner join NIVELESINSCRITO t2 on t1.IDINSCRITO = t2.IDINSCRITO " +
                "inner join NIVEL t3 on t2.IDNIVEL = t3.IDNIVEL " +
                "inner join TIPO_ESTUDIANTE t4 on t1.IDTIPOESTUDIANTE = t4.IDTIPOESTUDIANTE " +
                "inner join PERIODO_INSCRIPCION t5 on t2.IDPERIODOINSCRIPCION = t5.IDPERIODOINSCRIPCION " +
                "inner join ESTADO_ESTUDIANTE t6 on t2.IDESTADONIVEL = t6.CODESTADOESTU " +
                "inner join ESTADO_PRUEBA t7 on t2.PRUEBA = t7.CODESTADOPRUEBA " +
                "where t2.IDESTADONIVEL in (0, 5)"+
                " order by t5.PERIODO";
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
        public static DataSet GetMatriculas()
        {
            SqlConnection myConnection = CreateConnectionBBDDINGLES();
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();

            string sql;
            sql = "select t1.IDINSCRITO as CODESTUDIANTE,t1.NUMDOCINSCRITO IDENTIFICACION," +
                    "T4.DESCRTIPOESTUDIANTE TIPOESTUDIANTE, T1.NOMBINSCRITO NOMBRES," +
                    "T1.APELLIINSCRITO APELLIDOS, T1.CELUINSCRITO CELULAR," +
                    "T1.TELEFINSCRITO TELEFONO, T1.EMAILINSCRITO EMAIL, T1.FECHAREGISTRO FECHAINSCRIPCION, T5.PERIODO PERIODO, T3.NOMNIVEL," +
                    "T6.DESCESTESTUDIANTE ESTADONIVEL " +
                    "from INSCRITO_AUTONOMO t1 inner join NIVELESINSCRITO t2 on t1.IDINSCRITO = t2.IDINSCRITO " +
                    "inner join NIVEL t3 on t2.IDNIVEL = t3.IDNIVEL " +
                    "inner join TIPO_ESTUDIANTE t4 on t1.IDTIPOESTUDIANTE = t4.IDTIPOESTUDIANTE " +
                    "inner join PERIODO_INSCRIPCION t5 on t2.IDPERIODOINSCRIPCION = t5.IDPERIODOINSCRIPCION " +
                    "inner join ESTADO_ESTUDIANTE t6 on t2.IDESTADONIVEL = t6.CODESTADOESTU " +
                    "where t2.IDESTADONIVEL in (1,2,3,4) " +
                    "order by t5.PERIODO";
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
        public static DataSet GetHistoricoNotas()
        {
            SqlConnection myConnection = CreateConnectionBBDDINGLES();
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();

            string sql;
            sql = "select t1.IDINSCRITO as CODESTUDIANTE,t1.NUMDOCINSCRITO IDENTIFICACION," +
                    "T4.DESCRTIPOESTUDIANTE TIPOESTUDIANTE, T1.NOMBINSCRITO NOMBRES," +
                    "T1.APELLIINSCRITO APELLIDOS," +
                    "T2.FECHAREGISTRO FECHAREGISTRO, T5.PERIODO PERIODO, T3.NOMNIVEL," +
                    "T7.CALIFICACION PROMEDIO,t8.DESESTADONOTA " +
                    "from INSCRITO_AUTONOMO t1 inner join NIVELESINSCRITO t2 on t1.IDINSCRITO = t2.IDINSCRITO " +
                    "inner join NIVEL t3 on t2.IDNIVEL = t3.IDNIVEL " +
                    "inner join TIPO_ESTUDIANTE t4 on t1.IDTIPOESTUDIANTE = t4.IDTIPOESTUDIANTE " +
                    "inner join PERIODO_INSCRIPCION t5 on t2.IDPERIODOINSCRIPCION = t5.IDPERIODOINSCRIPCION " +
                    "inner join ESTADO_ESTUDIANTE t6 on t2.IDESTADONIVEL = t6.CODESTADOESTU " +
                    "inner join NOTAS t7 on t2.IDNIVELESTUDIANTE = t7.IDNIVELESESTUDIANTE "+
                    "inner join ESTADO_NOTA t8 on t2.ESTADONIVEL = t8.CODESTADONOTA "+
                    "where t2.ESTADONIVEL in (1,2) and t2.IDESTADONIVEL=1 and t7.IDNIVEL=t2.IDNIVEL and t7.ESTADO=3 and t2.PRUEBA=0" +
                    "order by t5.PERIODO";
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
            sql = "select t1.IDINSCRITO as CODESTUDIANTE,t1.NUMDOCINSCRITO IDENTIFICACION," +
                    "T4.DESCRTIPOESTUDIANTE TIPOESTUDIANTE, T1.NOMBINSCRITO NOMBRES," +
                    "T1.APELLIINSCRITO APELLIDOS, T1.CELUINSCRITO CELULAR," +
                    "T1.TELEFINSCRITO TELEFONO, T1.EMAILINSCRITO EMAIL, T1.FECHAREGISTRO FECHAINSCRIPCION, T5.PERIODO PERIODO, T3.NOMNIVEL," +
                    "T6.DESCESTESTUDIANTE ESTADONIVEL, T7.DESCESTADOPRUEBA PRUEBA,t8.PUNTAJEPRUEBA CALIFICACION " +
                    "from INSCRITO_AUTONOMO t1 inner join NIVELESINSCRITO t2 on t1.IDINSCRITO = t2.IDINSCRITO " +
                    "inner join PRUEBA t8 on t2.IDINSCRITO = t8.IDINSCRITO "+
                    "inner join NIVEL t3 on t2.IDNIVEL = t3.IDNIVEL " +
                    "inner join TIPO_ESTUDIANTE t4 on t1.IDTIPOESTUDIANTE = t4.IDTIPOESTUDIANTE " +
                    "inner join PERIODO_INSCRIPCION t5 on t2.IDPERIODOINSCRIPCION = t5.IDPERIODOINSCRIPCION " +
                    "inner join ESTADO_ESTUDIANTE t6 on t2.IDESTADONIVEL = t6.CODESTADOESTU " +
                    "inner join ESTADO_PRUEBA t7 on t2.PRUEBA = t7.CODESTADOPRUEBA " +
                    "where  t2.PRUEBA=1" +
                    " order by t5.PERIODO";
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
