using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Proyecto_Ingles_V2.LoginDb
{
    public static class Conexion
    {
        /// <summary>
        /// Crea una cadena de conexión al NAV
        /// </summary>
        /// <returns></returns>
        public static SqlConnection CreateConnection()
        {
            string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PortalEstudiantesUISEK_NV"].ConnectionString;
            return new SqlConnection(_connectionString);
        }
        /// <summary>
        /// Crea una cadena de conexión al UMAS
        /// </summary>
        /// <returns></returns>
        private static SqlConnection CreateConnectionUMAS()
        {
            string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PortalEstudiantesUISEK_EC"].ConnectionString;
            return new SqlConnection(_connectionString);
        }

        public static string ValNulo(string var)
        {
            string functionReturnValue;
            if (var == null || string.IsNullOrEmpty(var))
                functionReturnValue = "0";
            else
                functionReturnValue = var;

            return functionReturnValue;
        }
        /// <summary>
        /// Devuelve los valores monetarios de las asignaturas/ pago matrícula
        /// </summary>
        /// <param name="CodCli"></param>
        /// <param name="semestre"></param>
        /// <returns></returns>
        public static DataSet ValoresImporte(string CodCli, string semestre)
        {
            string strSQL = "";
            DataSet ds = new DataSet();
            SqlDataAdapter da = default(SqlDataAdapter);
            strSQL = "exec PagoOnLine_RellenarPrecios '" + CodCli + "','" + semestre + "' select Uds,Descripcion,PrecioUd,total from PagosOnLine_Importes where codcli='" + CodCli + "' and semestre='" + semestre + "'";
            SqlConnection connection = Conexion.CreateConnection();
            try
            {
                da = new SqlDataAdapter(strSQL, connection);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ds = null;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();

            }
            return (ds);
        }

        /// <summary>
        /// Devuelve la sumatoria de créditos y por tipo de materias 
        /// </summary>
        /// <param name="CodCli"></param>
        /// <param name="semestre"></param>
        /// <returns></returns>
        public static DataSet ResumenAsignaturas(string CodCli, string semestre)
        {
            SqlConnection myConnection = CreateConnection();
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();

            string sql;
            sql = "SELECT" + "(select CAST(sum(creditos) AS DECIMAL(4,0)) from SEK_V_Maestro_M where tipo='Matricula 1' and codcli='" + CodCli + "' and semestre='" + semestre + "')as CMAT1," +

                "(select count(*) from SEK_V_Maestro_M where tipo='Matricula 1' and codcli='" + CodCli + "' and semestre='" + semestre + "') as MAT1," +
                "(select CAST(sum(creditos) AS DECIMAL(4,0)) from SEK_V_Maestro_M where tipo='Matricula 2' and codcli='" + CodCli + "' and semestre='" + semestre + "') as CMAT2," +
                "(select count(*) from SEK_V_Maestro_M where tipo='Matricula 2' and codcli='" + CodCli + "' and semestre='" + semestre + "') as MAT2," +
                "(select CAST(sum(creditos) AS DECIMAL(4,0)) from SEK_V_Maestro_M where tipo='Matricula 3' and codcli='" + CodCli + "' and semestre='" + semestre + "') as CMAT3," +

                "(select count(*) from SEK_V_Maestro_M where tipo='Matricula 3' and codcli='" + CodCli + "' and semestre='" + semestre + "') as MAT3," +
                "(select CAST(sum(creditos) AS DECIMAL(4,0)) from SEK_V_Maestro_M where tipo='Convalidada' and codcli='" + CodCli + "' and semestre='" + semestre + "') as CCONV," +

                "(select count(*) from SEK_V_Maestro_M where tipo='Convalidada' and codcli='" + CodCli + "' and semestre='" + semestre + "') as CONV," +
                "(select CAST(sum(creditos) AS DECIMAL(4,0)) from SEK_V_Maestro_M where tipo='Homologada' and codcli='" + CodCli + "' and semestre='" + semestre + "') as CHOM," +
            "(select count(*) from SEK_V_Maestro_M where tipo='Homologada' and codcli='" + CodCli + "' and semestre='" + semestre + "') as HOM";
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
        /// <summary>
        /// Busca en el NAV y devuelve un adaptador SQL
        /// </summary>
        /// <param name="tabla"></param>
        /// <param name="campos"></param>
        /// <param name="condicion"></param>
        /// <returns></returns>
        public static SqlDataAdapter BuscarNAV(string tabla, string campos, string condicion)
        {
            SqlConnection myConnection = CreateConnection();
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();

            string sql;
            sql = "SELECT " + campos + " FROM " + tabla + " " + condicion;
            SqlCommand comando = new SqlCommand(sql, myConnection);
            SqlDataAdapter lector = default(SqlDataAdapter);
            comando.Connection = myConnection;
            comando.CommandType = CommandType.Text;
            comando.CommandTimeout = 99999999;

            try
            {
                lector = new SqlDataAdapter(comando);
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
            return lector;
        }
        private static SqlDataAdapter BuscarNAV_simple(string campos)
        {
            SqlConnection myConnection = CreateConnection();
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();

            string sql;
            sql = "SELECT " + campos;
            SqlCommand comando = new SqlCommand(sql, myConnection);
            comando.CommandType = CommandType.Text;
            comando.CommandTimeout = 99999999;
            SqlDataAdapter lector = default(SqlDataAdapter);
            try
            {
                lector = new SqlDataAdapter(comando);
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
            return lector;

        }
        /// <summary>
        /// Funcion copia del original pero con campos reducidos, se creo apra poder realizar una consulta
        /// en UMAS que no contenia tabla ni condicion fue creada para revisar las asistencias del estudiante
        /// </summary>
        /// <param name="campos"></param>
        /// <returns></returns>
        public static DataSet BuscarNAV_ds_simple(string campos)
        {
            SqlDataAdapter da = BuscarNAV_simple(campos);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return (ds);
        }
        /// <summary>
        /// Usa la función de buscar en NAV y devuelve una Tabla 
        /// </summary>
        /// <param name="tabla"></param>
        /// <param name="campos"></param>
        /// <param name="condicion"></param>
        /// <returns></returns>
        public static DataSet BuscarNAV_ds(string tabla, string campos, string condicion)
        {
            SqlDataAdapter da = BuscarNAV(tabla, campos, condicion);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return (ds);
        }

        public static DataSet BuscarUMAS_ds_compleja(string tabla, string campos, string condicion, string tabla2, string campos2, string condicion2)
        {
            SqlDataAdapter da = BuscarUMAS_compleja(tabla, campos, condicion, tabla2, campos2, condicion2);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return (ds);
        }

        private static SqlDataAdapter BuscarUMAS_compleja(string tabla, string campos, string condicion, string tabla2, string campos2, string condicion2)
        {
            SqlConnection myConnection = CreateConnectionUMAS();
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();

            string sql;
            sql = "SELECT " + campos + " FROM " + tabla + " " + condicion + "union SELECT " + campos2 + " FROM " + tabla2 + " " + condicion2;
            SqlCommand comando = new SqlCommand(sql, myConnection);
            comando.CommandType = CommandType.Text;
            comando.CommandTimeout = 99999999;
            SqlDataAdapter lector = default(SqlDataAdapter);
            try
            {
                lector = new SqlDataAdapter(comando);
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
            return lector;

        }
        /// <summary>
        /// Busca en el UMAS y devuelve un adaptador SQL
        /// </summary>
        /// <param name="tabla"></param>
        /// <param name="campos"></param>
        /// <param name="condicion"></param>
        /// <returns></returns>
        public static SqlDataAdapter BuscarUMAS(string tabla, string campos, string condicion)
        {
            SqlConnection myConnection = CreateConnectionUMAS();
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();

            string sql;
            sql = "SELECT " + campos + " FROM " + tabla + " " + condicion;
            SqlCommand comando = new SqlCommand(sql, myConnection);
            SqlDataAdapter lector = default(SqlDataAdapter);
            comando.Connection = myConnection;
            comando.CommandType = CommandType.Text;
            comando.CommandTimeout = 99999999;
            try
            {
                lector = new SqlDataAdapter(comando);
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
            return lector;

        }

        /// <summary>
        /// Usa la función de buscar en UMAS y devuelve una Tabla 
        /// </summary>
        /// <param name="tabla"></param>
        /// <param name="campos"></param>
        /// <param name="condicion"></param>
        /// <returns></returns>
        public static DataSet BuscarUMAS_ds(string tabla, string campos, string condicion)
        {
            SqlDataAdapter da = BuscarUMAS(tabla, campos, condicion);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return (ds);
        }
        /// <summary>
        /// permite insertar una registro en una tabla del NAV
        /// </summary>
        /// <param name="tabla"></param>
        /// <param name="campos"></param>
        /// <param name="valores"></param>
        /// <returns></returns>
        public static string InsertarNAV(string tabla, string campos, string valores)
        {
            string messages = string.Empty;
            SqlCommand comando = new SqlCommand();
            SqlConnection myConnection = CreateConnection();
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();

            string sql = "INSERT INTO  " + tabla + " (   " + campos + ") values ( " + valores + ");";
            // crecion de excepcion de la consulta a la base de datos
            try
            {
                comando.Connection = myConnection;
                comando.CommandText = sql;
                // comando.Transaction = oTrans
                //comando.CommandTimeout = 14000
                comando.ExecuteNonQuery();

                messages = "1";

            }
            catch (SqlException ex)
            {
                messages = "-1";
            }
            finally
            {
                if (!tabla.Contains("logsErrorPagoOnline"))
                    Log(sql.Replace("'", "\""), tabla, messages);
                if (myConnection.State == ConnectionState.Open)
                    myConnection.Close();
            }
            return messages;

        }

        /// <summary>
        /// Función que permite insertar un valor en una tabla del UMAS 
        /// </summary>
        /// <param name="tabla"></param>
        /// <param name="campos"></param>
        /// <param name="valores"></param>
        /// <returns></returns>
        public static string InsertarUMAS(string tabla, string campos, string valores)
        {
            string sql = null;
            string messages = string.Empty;
            SqlCommand comando = new SqlCommand();
            SqlConnection myConnection = CreateConnectionUMAS();
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();

            sql = "INSERT INTO  " + tabla + " (   " + campos + ") values ( " + valores + ");";
            // crecion de excepcion de la consulta a la base de datos
            try
            {
                comando.Connection = myConnection;
                comando.CommandText = sql;
                // comando.Transaction = oTrans
                //comando.CommandTimeout = 14000
                comando.ExecuteNonQuery();

                messages = "1";

            }
            catch (SqlException ex)
            {
                messages = "-1";
            }
            finally
            {
                if (myConnection.State == ConnectionState.Open)
                    myConnection.Close();
            }
            return messages;

        }

        public static string semestreactivo(string strSQL)
        {
            SqlConnection sqlConnection = CreateConnection();
            sqlConnection.Open();

            SqlCommand sqlCommand1 = new SqlCommand();
            var _with1 = sqlCommand1;
            _with1.CommandText = strSQL;
            _with1.CommandType = CommandType.Text;
            _with1.Connection = sqlConnection;

            SqlDataReader sqlDataReader1 = default(SqlDataReader);
            sqlDataReader1 = sqlCommand1.ExecuteReader();
            sqlDataReader1.Read();
            string strRetVal = null;
            strRetVal = sqlDataReader1[0].ToString();
            sqlDataReader1.Close();
            if (sqlConnection.State == ConnectionState.Open)
                sqlConnection.Close();
            return strRetVal;
        }

        public static string ActualizarNAV(string tabla, string campos, string condicion)
        {
            string messages = string.Empty;
            SqlCommand comando = new SqlCommand();
            SqlConnection myConnection = CreateConnection();
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();

            string sql = "UPDATE " + tabla + " SET " + campos + " " + condicion + ";";
            // crecion de excepcion de la consulta a la base de datos
            try
            {
                comando.Connection = myConnection;
                comando.CommandText = sql;
                comando.ExecuteNonQuery();

                messages = "1";

            }
            catch (SqlException ex)
            {
                messages = "-1";
            }
            finally
            {
                Log(sql.Replace("'", "\""), tabla, messages);
                if (myConnection.State == ConnectionState.Open)
                    myConnection.Close();
            }
            return messages;

        }
        public static string ActualizarUMAS(string tabla, string campos, string condicion)
        {
            string sql = null;
            string messages = string.Empty;
            SqlCommand comando = new SqlCommand();
            SqlConnection myConnection = CreateConnectionUMAS();
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();

            sql = "UPDATE " + tabla + " SET " + campos + " " + condicion + ";";
            // crecion de excepcion de la consulta a la base de datos
            try
            {
                comando.Connection = myConnection;
                comando.CommandText = sql;
                // comando.Transaction = oTrans
                //comando.CommandTimeout = 14000
                comando.ExecuteNonQuery();

                messages = "1";

            }
            catch (SqlException ex)
            {
                messages = "-1";
            }
            finally
            {

                if (myConnection.State == ConnectionState.Open)
                    myConnection.Close();
            }
            return messages;

        }

        public static string insertarNAV_ID(string tabla, string campos, string valores)
        {
            string sql = null;
            string messages = string.Empty;
            SqlCommand comando = new SqlCommand();
            SqlConnection myConnection = CreateConnection();
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            SqlDataAdapter lector = default(SqlDataAdapter);
            sql = "INSERT INTO  " + tabla + " ( " + campos + ") values ( " + valores + "); SELECT @@IDENTITY AS 'Identity'; ";
            // crecion de excepcion de la consulta a la base de datos
            try
            {
                comando.Connection = myConnection;
                comando.CommandType = CommandType.Text;
                comando.CommandTimeout = 99999999;
                comando.CommandText = sql;
                lector = new SqlDataAdapter(comando);
                DataSet ds = new DataSet();
                lector.Fill(ds);
                messages = ds.Tables[0].Rows[0][0].ToString();

            }
            catch (SqlException ex)
            {
                messages = "-1";
                lector = null;
            }
            finally
            {

                if (myConnection.State == ConnectionState.Open)
                    myConnection.Close();
            }

            return messages;

        }

        public static string DeleteUMAS(string tabla, string condicion)
        {
            string sql = null;
            string messages = string.Empty;
            SqlCommand comando = new SqlCommand();
            SqlConnection myConnection = CreateConnectionUMAS();
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();

            sql = "DELETE " + tabla + " WHERE " + condicion + ";";
            // crecion de excepcion de la consulta a la base de datos
            try
            {
                comando.Connection = myConnection;
                comando.CommandText = sql;
                comando.ExecuteNonQuery();
                messages = "1";

            }
            catch (SqlException ex)
            {
                messages = "-1";
            }
            finally
            {
                if (myConnection.State == ConnectionState.Open)
                    myConnection.Close();
            }
            return messages;
        }

        public static string DeleteNAV(string tabla, string condicion)
        {
            string messages = string.Empty;
            SqlCommand comando = new SqlCommand();
            SqlConnection myConnection = CreateConnection();
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();

            string sql = "DELETE from " + tabla + " WHERE " + condicion + ";";
            // crecion de excepcion de la consulta a la base de datos
            try
            {
                comando.Connection = myConnection;
                comando.CommandText = sql;
                comando.ExecuteNonQuery();
                messages = "1";

            }
            catch (SqlException ex)
            {
                messages = "-1";
            }
            finally
            {
                Log(sql.Replace("'", "\""), tabla, messages);
                if (myConnection.State == ConnectionState.Open)
                    myConnection.Close();
            }
            return messages;
        }
        /// <summary>
        /// Funcion copia del original creada para realizar una consulta en UMAS sin la necesidad de campos ni condiciones
        /// fue creada para revisar las asistencias del estudiante
        /// </summary>
        /// <param name="campos"></param>
        /// <returns></returns>

        private static SqlDataAdapter BuscarUMAS_simple(string campos)
        {
            SqlConnection myConnection = CreateConnectionUMAS();
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();

            string sql;
            sql = "SELECT " + campos;
            SqlCommand comando = new SqlCommand(sql, myConnection);
            comando.CommandType = CommandType.Text;
            comando.CommandTimeout = 99999999;
            SqlDataAdapter lector = default(SqlDataAdapter);
            try
            {
                lector = new SqlDataAdapter(comando);
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
            return lector;

        }

        /// <summary>
        /// Funcion copia del original pero con campos reducidos, se creo apra poder realizar una consulta
        /// en UMAS que no contenia tabla ni condicion fue creada para revisar las asistencias del estudiante
        /// </summary>
        /// <param name="campos"></param>
        /// <returns></returns>
        public static DataSet BuscarUMAS_ds_simple(string campos)
        {
            SqlDataAdapter da = BuscarUMAS_simple(campos);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return (ds);
        }
        /// <summary>
        /// Función que permite el registro de los datos de facturación y guarda el intento de transaccion el log LOG_TransaccionesBotonPagos
        /// </summary>
        /// <param name="Semestre"></param>
        /// <param name="CodCli"></param>
        /// <param name="Importe"></param>
        /// <param name="Iva"></param>
        /// <param name="NombreFactura"></param>
        /// <param name="TipoIdenFactura"></param>
        /// <param name="IdentificacionFactura"></param>
        /// <param name="TelefonoFactura"></param>
        /// <param name="DireccionFactura"></param>
        /// <param name="CorreoFactura"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string GuardaTransaccion(string Cedula, string Importe, string Iva, string NombreFactura, string TipoIdenFactura, string IdentificacionFactura, string TelefonoFactura, string DireccionFactura, string CorreoFactura, string ip, int esAlumno)
        {
            string sqlValores = string.Empty;
            string sqlCampos = string.Empty;
            string sqlCamposLog = string.Empty;
            string sqlValoresLog = string.Empty;

            //obtiene si una transaccion de este usuario esta activa
            DataSet ds = BuscarNAV_ds("[TransaccionesBotonPagosOnline]", "*", " where Cedula='" + Cedula + "' and ( Estado='Pagado' or  Estado='Negado' or Estado='Autorizado')");
            //obtiene la cantidad de transacciones de usuario (cedula)
            DataSet ds3 = BuscarNAV_ds("[TransaccionesBotonPagosOnline]", "count(*)", " where cedula='" + Cedula + "'");
            //ontiene la cantidad total de intentos de transaccion para saber el secuencial 
            DataSet dsSecuencial = BuscarNAV_ds("LOG_TransaccionesBotonPagoOnline", "count (*)", "");
            SqlConnection myConnection = CreateConnection();
            string rID;
            string Secuencial = "002" + (int.Parse(dsSecuencial.Tables[0].Rows[0][0].ToString()) + 300).ToString();
            string fecha = DateTime.Now.ToShortDateString();
            string hora = (DateTime.Now.Hour.ToString().Length < 2 ? ("0" + DateTime.Now.Hour.ToString()) : DateTime.Now.Hour.ToString()) + ":" + (DateTime.Now.Minute.ToString().Length < 2 ? ("0" + DateTime.Now.Minute.ToString()) : DateTime.Now.Minute.ToString()) + ":" + (DateTime.Now.Second.ToString().Length < 2 ? ("0" + DateTime.Now.Second.ToString()) : DateTime.Now.Second.ToString());
            Int16 ProcesadaCaja = 0;
            Int16 Firmada = 0;
            string Estado = "En Proceso";
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            string mens = string.Empty;
            string mens2 = string.Empty;

            try
            {


                sqlCampos = "[Secuencial],[Cedula],[Fecha],[Hora],[Importe],[Estado],[ProcesadaCaja],[Firmada],[NombreFactura]" +
                ",[IdentificacionFactura],[TelefonoFactura],[DireccionFactura],[CorreoFactura],[TipoIdenFactura],[IVA]" +
                ",[D_datos],[D_aut],[D_Cre],[D_Mes],[D_ttar],[D_sub],[D_Iva],[D_Ice],[D_Int],[D_Tot],[D_tNo],[D_cDt],[D_Tipo],numtransaccion,EsAlumno,lote,Referencia,origen";

                sqlValores = "'" + Secuencial + "','" + Cedula + "','" + fecha + "','" + hora + "'," + Importe + ",'" + Estado + "','" +
                    +ProcesadaCaja + "','" + Firmada + "','" + NombreFactura + "','" + IdentificacionFactura + "','" + TelefonoFactura + "','" + DireccionFactura +
                            "','" + CorreoFactura + "'," + TipoIdenFactura + "," + Iva + ",'','','',0,'',0,0,0,0,0,'','','','" + (int.Parse(ds3.Tables[0].Rows[0][0].ToString()) + 1) + "','" + esAlumno + "','','',1";

                mens2 = InsertarNAV("TransaccionesBotonPagosOnline", sqlCampos, sqlValores);

                DataSet dsNum = BuscarNAV_ds("[TransaccionesBotonPagosOnline]", "*", " where Cedula='" + Cedula + "' ");

                DataSet dsLog = BuscarNAV_ds("LOG_TransaccionesBotonPagoOnline", "*", "where secuencial='" + Secuencial + "'");
                sqlCamposLog = "[Cedula],[Secuencial],[Fecha],[Hora],[Estado],[ip],revisado,numtransaccion";
                sqlValoresLog = "'" + Cedula + "','" + Secuencial + "','" + fecha + "','" + hora + "','" + Estado + "','" + ip + "','0','" + dsNum.Tables[0].Rows.Count + "'";

                mens = InsertarNAV("[LOG_TransaccionesBotonPagoOnline]", sqlCamposLog, sqlValoresLog);
                if (mens == "1" && mens2 == "1")
                    rID = Secuencial.ToString();
                else
                    rID = "0";

            }
            catch (Exception ex)
            {
                rID = "0";
                Log(ex.Message);
            }
            finally
            {
                if (myConnection.State == ConnectionState.Open)
                    myConnection.Close();
            }

            return rID;
        }

        public static string GuardarMaestro(string id, List<Producto> listaCompras)
        {
            string aux;
            if (listaCompras.Count > 0)
            {
                int cant = 0;
                DeleteNAV("Maestro_PagoOnline", "where secuencial='" + id + "'");
                foreach (Producto x in listaCompras)
                {
                    cant += Convert.ToInt32(InsertarNAV("Maestro_PagoOnline", "secuencial , codconcepto , concepto , importe , cantidad,iva ", "'" + id + "','" + x.codConcepto + "','" + x.concepto + "','" + x.importe.ToString().Replace(",", ".") + "','" + x.cantidad + "','" + x.iva.ToString().Replace(",", ".") + "'"));
                }
                if (cant != listaCompras.Count)
                    aux = "0";
                else
                    aux = "1";
            }
            else
                aux = "0";

            return aux;
        }

        public static string GuardarMaestroDeposito(string id, List<Producto> listaCompras)
        {
            string aux;
            if (listaCompras.Count > 0)
            {
                int cant = 0;
                DeleteNAV("Maestrodeposito_online", "secuencial='" + id + "'");
                foreach (Producto x in listaCompras)
                {
                    cant += Convert.ToInt32(InsertarNAV("Maestrodeposito_online",
                        "secuencial , codconcepto , concepto , importe , cantidad,iva ",
                        "'" + id + "','" + x.codConcepto + "','" + x.concepto + "','" + x.importe.ToString().Replace(",", ".") + "','" + x.cantidad + "','" + x.iva.ToString().Replace(",", ".") + "'"));
                }
                if (cant != listaCompras.Count)
                    aux = "0";
                else
                    aux = "1";
            }
            else
                aux = "0";

            return aux;
        }

        /// <summary>
        /// Función que permite el primer registro de los datos devueltos por Diners al realizar el pago
        /// Todo los valores con los devueltos por Diners
        /// </summary>
        /// <param name="secuencial"></param>
        /// <param name="D_datos"></param>
        /// <param name="D_aut"></param>
        /// <param name="D_Cre"></param>
        /// <param name="D_Mes"></param>
        /// <param name="D_ttar"></param>
        /// <param name="D_sub"></param>
        /// <param name="D_Iva"></param>
        /// <param name="D_Ice"></param>
        /// <param name="D_Int"></param>
        /// <param name="D_Tot"></param>
        /// <param name="D_cDt"></param>
        /// <param name="D_Tipo"></param>
        /// <returns></returns>
        public static bool RegistroPago(string secuencial, string D_datos, string D_aut, string D_Cre, string D_Mes, string D_ttar, string D_sub, string D_Iva, string D_Ice, string D_Int, string D_Tot, string D_cDt, string D_Tipo)
        {
            //Hora y fecha actual
            string fecha = DateTime.Now.ToShortDateString();
            string hora = (DateTime.Now.Hour.ToString().Length < 2 ? ("0" + DateTime.Now.Hour.ToString()) : DateTime.Now.Hour.ToString()) + ":" + (DateTime.Now.Minute.ToString().Length < 2 ? ("0" + DateTime.Now.Minute.ToString()) : DateTime.Now.Minute.ToString()) + ":" + (DateTime.Now.Second.ToString().Length < 2 ? ("0" + DateTime.Now.Second.ToString()) : DateTime.Now.Second.ToString());
            string mens = string.Empty;
            string mens2 = string.Empty;
            bool registro = false;
            string sqlValores = string.Empty;
            //se consulta el importe y el IVA guardados para comprobarlos
            DataSet ds = BuscarNAV_ds("[TransaccionesBotonPagosOnline]", "importe,iva", " where secuencial='" + secuencial + "'");
            //se transforma los valores 
            string importe = ds.Tables[0].Rows[0][0].ToString().Replace(",", ".");
            importe = importe.Substring(0, importe.LastIndexOf(".")) + importe.Substring(importe.LastIndexOf(".") + 1, 2);

            if (D_Int.Length >= 3)
                D_Int = D_Int.Substring(0, D_Int.Length - 2) + "." + D_Int.Substring(D_Int.Length - 2);
            else
                D_Int = "0.00";

            string iva = ds.Tables[0].Rows[0][1].ToString().Replace(",", ".");
            iva = iva.Substring(0, iva.LastIndexOf(".")) + iva.Substring(iva.LastIndexOf(".") + 1, 2);

            //se compara, si son correctos se actualizan el estado en la tabla de logs 
            //y se guardan los valores devueltos por Diners
            //y la función devuelve TRUE
            //en caso de que no sean correctos solo se pone Negado en el log
            //y la funcion devuelve FALSE

            if (importe == D_sub && iva == D_Iva)
            {
                try
                {
                    if (D_Tipo.Trim() == "P")
                        mens2 = ActualizarNAV("LOG_TransaccionesBotonPagoOnline", " estado='Autorizado', revisado=0 ", "where secuencial='" + secuencial + "'");
                    else
                        mens2 = ActualizarNAV("LOG_TransaccionesBotonPagoOnline", " estado='Reverso', revisado=0 ", "where secuencial='" + secuencial + "'");

                    sqlValores = "fecha='" + fecha + "',hora='" + hora + "', D_datos='" + D_datos + "',  D_aut='" + D_aut + "',  D_Cre='" + D_Cre + "', D_Mes=" + D_Mes +
                       " ,D_ttar='" + D_ttar + "', D_sub=" + D_sub.Substring(0, D_sub.Length - 2) + "." + D_sub.Substring(D_sub.Length - 2) +
                       " ,D_Iva=" + D_Iva.Substring(0, D_Iva.Length - 2) + "." + D_Iva.Substring(D_Iva.Length - 2) +
                       " ,D_Ice=" + D_Ice.Substring(0, D_Ice.Length - 2) + "." + D_Ice.Substring(D_Ice.Length - 2) +
                       " ,D_Int=" + D_Int +
                       " ,D_Tot=" + D_Tot.Substring(0, D_Tot.Length - 2) + "." + D_Tot.Substring(D_Tot.Length - 2) +
                       " ,D_tNo=" + secuencial +
                       " ,D_cDt='" + D_cDt + "' ,D_Tipo='" + D_Tipo + "' ";
                    mens = ActualizarNAV("TransaccionesBotonPagosOnline", sqlValores, " where secuencial='" + secuencial + "' ");
                    if (mens == "1" && mens2 == "1")
                        registro = true;
                }
                catch (Exception ex)
                {
                    Log(ex.Message.Replace("'", "\""));
                    registro = false;
                }
            }
            else
            {
                mens2 = ActualizarNAV("LOG_TransaccionesBotonPagoOnline", " estado='Negado' ", "where secuencial='" + secuencial + "'");
                registro = false;
            }
            return registro;

        }

        /// <summary>
        /// Se analiza el estado y se verifica si la transacción fue correcta o no, se actualiza el estado en la tabla de pagos y devuelve TRUE o FALSE 
        /// dependiendo cual sea el caso
        /// </summary>
        /// <param name="secuencial"></param>
        /// <param name="estado"></param>
        /// <param name="numAut"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorDetails"></param>
        /// <returns></returns>
        public static bool RegistroPagoFinal(string secuencial, string estado, string numAut, string errorCode, string errorDetails)
        {
            bool flag;
            if (estado == "Y")
            {
                ActualizarNAV("TransaccionesBotonPagosOnline", "Estado='Pagado'", " where secuencial='" + secuencial + "' ");
                ActualizarNAV("LOG_TransaccionesBotonPagoOnline", "Estado='Pagado'", " where secuencial='" + secuencial + "' ");
                flag = true;
            }
            else
            {
                ActualizarNAV("TransaccionesBotonPagosOnline", "Estado='Negado'", " where secuencial='" + secuencial + "' ");
                ActualizarNAV("LOG_TransaccionesBotonPagoOnline", "Estado='Negado'", " where secuencial='" + secuencial + "' ");
                flag = false;
            }
            return flag;
        }
        /// <summary>
        /// Log para revisión de errores al momento del pago
        /// </summary>
        /// <param name="logito"></param>
        public static void Log(string logito)
        {
            InsertarNAV("logsErrorPagoOnline", "error,fecha ", "'" + logito + "','" + DateTime.Now + "'");
        }
        /// <summary>
        /// Log para revisión de errores al momento del pago
        /// </summary>
        /// <param name="logito"></param>
        public static void Log(string logito, string table, string status)
        {
            InsertarNAV("logsErrorPagoOnline", "error,fecha,tabla,status ", "'" + logito + "','" + DateTime.Now + "','" + table + "','" + status + "'");
        }
        /// <summary>
        /// Log para revisión de errores al momento del pago
        /// </summary>
        /// <param name="logito"></param>
        public static void Log(string logito, string page)
        {
            InsertarNAV("logsErrorPagoOnline", "error,fecha,pagina ", "'" + logito + "','" + DateTime.Now + "','" + page + "'");
        }
    }
}