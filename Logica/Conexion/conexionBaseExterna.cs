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
        public static DataSet GetDatosAlumno(string numcedula)
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

    }
}
