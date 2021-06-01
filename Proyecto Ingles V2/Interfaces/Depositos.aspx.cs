using Logica.ConexionServicios;
using Proyecto_Ingles_V2.LoginDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Logica.Modelos;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace Proyecto_Ingles_V2.Interfaces
{
    public partial class Depositos : System.Web.UI.Page
    {
        List<Producto> listaCompras = new List<Producto>();
        static conexionServidor cs = new conexionServidor();
        string url = cs.url.ToString();
        protected String exception = "";
        private string OnServerPath()
        {
            //VERSION DE PRUEBA
            string host = HttpContext.Current.Request.Url.Host.ToLower();
            string path;
            if (host == "localhost")
                path = Server.MapPath("/pdf/");
            else
            {
                path = Server.MapPath("/");
                path = path.Replace("pagoonline_uisek_ec\\", "PortalAplicativo\\PagosDepositosTransferencias\\pdf\\");
                //verificar linea 37 cuando vaya a produccion es la carga de documentos

            }
            return path;
        }

        protected async void Page_Load(object sender, EventArgs e)
        {

            if (Session["usuario"] == null || (string)Session["usuario"] == "")
            {
                Response.Redirect("../Login/formLogin.aspx");
            }
            if ((int)Session["TipoUser"] == 1)
            { 
            try
                {
                    if (!IsPostBack)
                    {
                    //List<Producto> listaCompras = new List<Producto>();

                    //consulta los banco a los q se les hace transferencia.
                    DataSet dsBanco = Conexion.BuscarNAV_ds("[dbo].[DTA_Banco]", "*", "");
                        ddl_origen_deposito.DataSource = dsBanco.Tables[0];
                        ddl_origen_deposito.DataTextField = "nombre";
                        ddl_origen_deposito.DataValueField = "id";
                        ddl_origen_deposito.DataBind();
                        ddl_origen_deposito.Items.Insert(0, new ListItem("- Seleccione una opción -", "0"));
                        //consulta los tipos de concepto para la transacción
                        DataSet ds_concepto=null; //bloquer no escoja una opcion diferente de las de ingles
                        string usuario = (string)Session["usuario"].ToString();
                        cargarInformacionFactura();
                        listaCompras = await CargarInformacion(usuario);
                        string codProducto = listaCompras.Select(x => x.codConcepto).FirstOrDefault();
                        
                        if (codProducto == "SEK1333" || codProducto == "SEK1335" || codProducto == "SEK1337" || codProducto == "SEK1339" || codProducto == "SEK1341" || codProducto == "SEK1343" || codProducto == "SEK1345" || codProducto == "SEK1347")//niveles
                        {
                            ds_concepto= Conexion.BuscarNAV_ds("[dbo].[DTA_Concepto]", "*", "where nombre like 'Inglés Autónomo Nivel%'");
                        }
                        else if (codProducto == "SEK1203")//prueba de ubicacion
                        {
                            ds_concepto = Conexion.BuscarNAV_ds("[dbo].[DTA_Concepto]", "*", "where nombre like 'Inglés Autónomo Prueba Ubicación%'");

                        }
                        else if(codProducto== "SEK1074")//examen suficiencia
                        {
                            ds_concepto = Conexion.BuscarNAV_ds("[dbo].[DTA_Concepto]", "*", "Inglés Autónomo Prueba Suficiencia%'");
                        }
                        else if (codProducto == "SEK1205")//reconocimiento ingles
                        {
                            ds_concepto = Conexion.BuscarNAV_ds("[dbo].[DTA_Concepto]", "*", "Inglés Autónomo Reconocimiento%'");
                        }

                        ddl_concepto.DataSource = ds_concepto.Tables[0];
                        ddl_concepto.DataTextField = "nombre";
                        ddl_concepto.DataValueField = "id";
                        ddl_concepto.DataBind();                     
                        //ddl_concepto.SelectedValue = "";
                    Previsualizar();
                        //si hay facturas cone se codigo cliente podemos sacar los datos para facturacion sino queda en blanco

                    }     
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", @"alert('Error al ingresar su transacción.');console.log('" + ex.Message.Replace("'", "**") + "'); window.location.href='default.aspx';  ", true);
            }
            
            }

        }
        public async void ActualizarEstadoMatricula(long idNivelEstudiante, int idEstado)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ClNivelesInscrito nivelesIns = new ClNivelesInscrito();
                nivelesIns.IDESTADONIVEL = idEstado;
                string uri = "api/ActualizarMatriculas?idNivel=" + idNivelEstudiante;
                var myContent = JsonConvert.SerializeObject(nivelesIns);
                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                var method = new HttpMethod("PATCH");
                var request = new HttpRequestMessage(method, uri)
                {
                    Content = stringContent
                };
                response = await client.SendAsync(request);

            }
            catch (TaskCanceledException e)
            {
            }
        }




        protected async void enviar_Click(object sender, EventArgs e)
        {

            List<Producto> listaCompras = await CargarInformacion((string)Session["usuario"]);
            try
            {
                string cedula =(string)Session["usuario"];
                //comienza en valor cero 
                decimal valor = 0;
                //valida q el valor ingresado no sea vacío
                if (txt_valor_depositado.Text != "")
                {
                    //si no es así convierte el valor
                    valor = Convert.ToDecimal(txt_valor_depositado.Text);
                }
                //se carga
                //se busca el semestre activo para matricular
                DataSet ds_semestre = Conexion.BuscarUMAS_ds("UT_Semestre_Activo", "*", "where activo='1'");
                //string anio_periodo = await getPeriodo(cedula);//cambiar por periodo que tiene sistema ingles autonomo
                string anio_periodo = hiddenPeriodo.Value;//cambiar por periodo que tiene sistema ingles autonomo
                DataSet ds_comprobantevalidador = Conexion.BuscarNAV_ds("[dbo].[DTA_Info]", "*", "where no_comprobante='" + txt_comprobante.Text + "' and semestre='" + anio_periodo + "'");
                if (ds_comprobantevalidador.Tables[0].Rows.Count != 0 || txt_comprobante.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", @"alert('Ya existe ese número de comprobante o no esta ingresando el número de comprobante'); ", true);
                }
                else if (valor == 0)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", @"alert('El valor debe ser mayor a 0'); ", true);
                }
                else
                {
                    EnviarInfo();
                    


                    //EnviaCorreo_tesoreria();
                    //EnviaCorreo_Alumno();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", @"alert('Error al ingresar los datos del depósito, vuelva a intentarlo más tarde.');console.log('" + ex.Message.Replace("'", "**") + "');  ", true);
            }

        }
        //llamar servicio para buscar id 


        public async void EnviarInfo()
        {
            long idNivelEstudiante = (long)Session["idNivel"];
            ActualizarEstadoMatricula(idNivelEstudiante,5);//estado 5 significa en proceso
            List<ClInscritoAutonomo> cl = await getDatosInscrito();
            string cedula =(string)Session["usuario"];//cargar cedula de usuario


            string anio_periodo = hiddenPeriodo.Value;// cargar periodo de ingles
            //Actualizar estado en nivel a estado en proceso con valor 5


            string ipuser = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipuser))
                ipuser = Request.ServerVariables["REMOTE_ADDR"];



            string host = HttpContext.Current.Request.Url.Host.ToLower();
            if (host == "localhost")
                ipuser = "186.5.25.90";

            //fin ip 

            string id_trans;
            string secuencial_alterno;
            if (Session["SEC_ALTER"] != null)
                secuencial_alterno = Session["SEC_ALTER"].ToString();
            else
                secuencial_alterno = "Sec-II-" + ddl_concepto.SelectedValue + "-" + txt_comprobante.Text + "-" + cedula + "-" + DateTime.Now.ToShortDateString().Replace("/", "");
            

                id_trans = Conexion.insertarNAV_ID("[dbo].[DTA_Info]", "semestre,fecha_tramite,fecha_creacion,id_banco,no_comprobante,valor_deposito,id_concepto,tipo,estado,otro_concepto,identificacion,secuencial_alt,ip_user",
                            "'" + anio_periodo + "','" + txt_fecha_deposito.Text + "','" + DateTime.Now + "','" + ddl_origen_deposito.SelectedValue + "','" + txt_comprobante.Text +
                            "','" + txt_valor_depositado.Text + "','" + ddl_concepto.SelectedValue + "','" + ddl_tipo_pago.SelectedValue + "','1','" + txt_concepto.Text + "','" + cedula + "','" + secuencial_alterno + "','" + ipuser + "'");

            //se carga los datos de la lista de compras en el maestro
            List<Producto> listaCompras =await CargarInformacion((string)Session["usuario"]);
            Conexion.GuardarMaestroDeposito(id_trans, listaCompras);
            //nombre cargar del ingles

            if (id_trans != "-1")
            {
                string exito = SubirArchivos(cedula, id_trans,anio_periodo);

                DataSet ds_Valores = Conexion.BuscarNAV_ds("Maestrodeposito_online", "*", "where secuencial='" + id_trans + "'");

                string menDetalle = "1";
                for (int i = 0; i < ds_Valores.Tables[0].Rows.Count; i++)
                {
                    menDetalle = Conexion.insertarNAV_ID("DTA_Detalle", "id_info,codconcepto,cedula,nombre,semestre,valor,cantidad,descripcion",
                               "'" + id_trans + "','" + ds_Valores.Tables[0].Rows[i]["codconcepto"].ToString() + "','" + cedula + "','" + "CRISTHIAN " + "','"+anio_periodo+"','" + ds_Valores.Tables[0].Rows[i]["importe"].ToString()+"','"+ ds_Valores.Tables[0].Rows[i]["cantidad"].ToString()+"','"+ ds_Valores.Tables[0].Rows[i]["concepto"].ToString()+"'");//CAMBIAR NOMBRE
                    if (menDetalle == "-1")
                        break;
                }
                if (exito == "-1" || menDetalle == "-1")
                {
                    Conexion.DeleteNAV("[dbo].[DTA_Detalle]", "where id_info='" + id_trans + "'");
                    Conexion.DeleteNAV("[dbo].[DTA_Info]", "where id='" + id_trans + "'");     
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", @"alert('No se pudo ingresar la información enviada. Intentelo más tarde o comuníquese con el Administrador'); window.location.href = 'default.aspx';   ", true);
                }
            }
        }

        private string SubirArchivos(string codcli, string idtracking,string anioperiodo)
        {
            string exito = "-1";
            List<string> documentos = new List<string>();
            DataSet solicitud = Conexion.BuscarNAV_ds("[dbo].[DTA_Info]", "*", "where id='" + idtracking + "'");
            string anio = anioperiodo.Substring(0, 4);
            FileUpload file = fu_archivo;
            try
            {
                string confirmValue = Request.Form["confirm_value"];

                if (confirmValue == "Yes")
                {
                    string strExtensionName = string.Empty;
                    if (file.HasFile && file.Enabled)
                    {
                        if (file.PostedFile.ContentType == "application/pdf" || file.PostedFile.ContentType == "image/jpeg" || file.PostedFile.ContentType == "image/jpg" || file.PostedFile.ContentType == "image/png")
                        {
                            if (file.FileBytes.Length <= 2048 * 1024)
                            {
                                string archivo = string.Empty;
                                try
                                {
                                    string strFileNameWithPath = file.PostedFile.FileName;
                                    // get the extension name of the file
                                    strExtensionName = System.IO.Path.GetExtension(strFileNameWithPath);

                                    string path = OnServerPath() + anio + "\\";
                                    //aqui se arma el path del archivo
                                    archivo = path + "DTA_" + anio + "_" + txt_cedula.Text + "_" + idtracking + strExtensionName;


                                    documentos.Add(archivo);
                                    if (!Directory.Exists(path))
                                        Directory.CreateDirectory(path);

                                    if (File.Exists(archivo))
                                        System.IO.File.Delete(archivo);

                                    file.PostedFile.SaveAs(archivo);

                                    if (Conexion.ActualizarNAV("[dbo].[DTA_Info]", "url_comprobante='DTA_" + anio + "_" + txt_cedula.Text + "_" + idtracking + strExtensionName + "'", "where id='" + idtracking + "'") == "1")
                                    {
                                        exito = "1";
                                        string id_trans = Conexion.InsertarNAV("DTA_DatosFactura", "id_info,nombre_factura,[TipoIdentificacion],identificacion,direccion,telefono,email,codCli",
                                              "'" + idtracking + "','" + txt_nombre.Text + "','" + ddlTipo.SelectedValue + "','" + txt_cedula.Text + "','" + txt_direccion.Text + "','" + txt_telefono.Text + "','" + txt_correo.Text + "','" + codcli + "'");
                                        if (id_trans == "-1")
                                            exito = "-1";
                                        else
                                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", @"alert('Su información ha sido ingresada correctamente'); window.location.href = 'Default.aspx';   ", true);//revisar pagina de redireccion
                                    }
                                    else
                                    {

                                        BorrarDocumentos(documentos);
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", @"alert('No se pudo ingresar la información enviada. Intentelo más tarde o comuníquese con el Administrador'); window.location.href = 'Default.aspx';   ", true);//revisar pagina de redireccion
                                        exito = "-1";
                                        return exito;
                                    }

                                }
                                catch (Exception ex)
                                {
                                    BorrarDocumentos(documentos);
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", @"alert('No se pudo ingresar la información enviada. Intentelo más tarde o comuníquese con el Administrador**" + ex.Message + "'); ", true);
                                    return "-1";
                                }
                            }
                            else
                            {
                                BorrarDocumentos(documentos);
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", @"alert('Solo se aceptan archivos tipo PDF, jpg, png y jpeg de hasta 2 mb.'); ", true);
                                return "-1";
                            }
                        }
                        else
                        {
                            BorrarDocumentos(documentos);
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", @"alert('Solo se aceptan archivos tipo PDF, jpg, png y jpeg de hasta 2 mb.'); ", true);
                            return "-1";
                        }
                    }
                    else
                    {
                        exito = "1";
                    }
                }
                else
                {
                    BorrarDocumentos(documentos);
                    return "-1";
                }

            }
            catch (Exception ex)
            {
                BorrarDocumentos(documentos);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", @"alert('No se pudo ingresar la información enviada. Inténtelo más tarde o comuníquese con el Administrador'); console.log('" + ex.Message.Replace("'", "**") + "');", true);
                exito = "-1";
            }
            return exito;
        }
        private void BorrarDocumentos(List<string> lista)
        {
            if (lista.Count > 0)
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    if (File.Exists(lista[i]))
                        System.IO.File.Delete(lista[i]);

                }
            }
        }

        public async void EnviaCorreo_tesoreria()
        {
            try
            {
                List<ClInscritoAutonomo> inscrito=await  getDatosInscrito();
                var datos = from a in inscrito where a.NumDocInscrito.Trim().ToString() == (string)Session["usuario"] select new {
                    Nombre=a.NombreInscrito,
                    Apellido=a.ApellidoInscrito,
                };
                string nombre, apellido;
                nombre= datos.Select(x => x.Nombre).FirstOrDefault().ToString();
                apellido= datos.Select(x => x.Apellido).FirstOrDefault().ToString();
                string NombreUsuario = nombre + " " + apellido;


                MailMessage correo = new MailMessage();
                correo.To.Add("tesoreria@uisek.edu.ec");
                correo.From = new MailAddress("no.reply@uisek.edu.ec", "Correo notificación Depósitos Inglés Autónomo", System.Text.Encoding.UTF8);
                correo.Subject = "Correo autogenerado de  notificación Depósitos Inglés Autónomo";
                correo.SubjectEncoding = System.Text.Encoding.UTF8;
                correo.Body = "El/La alumn@ " + NombreUsuario + ", \n Ha enviado su solicitud de ingreso de " + ddl_tipo_pago.SelectedItem.Text + ",\n con el concepto de pago " + ddl_concepto.SelectedItem.Text + ",\n " +
                    "desde " + ddl_origen_deposito.SelectedItem.Text + " por la cantidad de " + txt_valor_depositado.Text + "$" +
                  "  NO RESPONDA A ESTE EMAIL.\n  \n En caso de dudas contacte al departamento técnico.  \n sistemas@uisek.edu.ec ";
                correo.BodyEncoding = System.Text.Encoding.UTF8;
                string body = "<html><head> " +
                 "<style type=\"text/css\">.style3 { width:30%;  } .style2 {color:red;}.style4 {border:0;}</style>" +
                "</head>" +
                "<body class=\"style4\">" +
                "<form id=\"form1\" runat=\"server\">" +
                "<tr><td colspan=\"2\" style=\"text-align:center; font-size:25px\"><img src='cid:logo' width='100px'></img></td>" +
                "<div><table class=\"style4\" style=\"width:100%;\">" +
                "<tr><td class=\"style3\"></td><td>El/La alumn@  " + NombreUsuario + ",</td> </tr>" +
                "<tr><td class=\"style3\"></td><td>Ha enviado su solicitud de ingreso de " + ddl_tipo_pago.SelectedItem.Text + ",</td>  " +
                "<tr><td class=\"style3\"></td><td>con el concepto de pago " + ddl_concepto.SelectedItem.Text + ",</td>  " +
                "<tr><td class=\"style3\"></td><td>desde " + ddl_origen_deposito.SelectedItem.Text + " por la cantidad de " + txt_valor_depositado.Text + "$</td>  " +
                "<td class=\"style3\"></td></tr><tr><td class=\"style3\">&nbsp;</td><td class=\"style2\"></td>" +//esta linea fue cambiada y hay que revisar ya que no debe decir localhost

                "<td class=\"style3\"></td></tr><tr><td class=\"style3\">&nbsp;</td><td class=\"style2\">NO RESPONDA A ESTE CORREO. </td>" +
                "<td class=\"style3\">&nbsp;</td></tr><tr><td class=\"style3\">&nbsp;</td><td>En caso de dudas contacte al departamento de Idiomas de la Universidad Internacional SEK.</td>" +
                "<td class=\"style3\">&nbsp;</td></tr> </table></div></form></body></html>";

                System.Net.Mime.ContentType mimeType = new System.Net.Mime.ContentType("text/html");
                // Add the alternate body to the message.

                AlternateView alternate = AlternateView.CreateAlternateViewFromString(body, mimeType);
                LinkedResource logo = new LinkedResource(Server.MapPath("~/Images/") + "logo.png")
                {
                    ContentId = "logo"
                };

                // Lo incrustamos en la vista HTML...
                alternate.LinkedResources.Add(logo);
                correo.AlternateViews.Add(alternate);
                correo.IsBodyHtml = false;
                SmtpClient client = new SmtpClient
                {
                    Credentials = new System.Net.NetworkCredential("no.reply@uisek.edu.ec", Funciones.ConectarMail()),
                    Port = 587,
                    Host = "smtp.gmail.com",
                    EnableSsl = true //Esto es para que vaya a través de SSL que es obligatorio con GMail 
                };
                client.Send(correo);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", @"alert('No se pudo enviar el correo electrónico'); console.log('" + ex.Message.Replace("'", "**") + "');", true);
            }



        }
        public async void EnviaCorreo_Alumno()
        {
            try
            {
                List<ClInscritoAutonomo> inscrito = await getDatosInscrito();
                var datos = from a in inscrito
                            where a.NumDocInscrito.Trim().ToString() == (string)Session["usuario"]
                            select new
                            {
                                Nombre = a.NombreInscrito,
                                Apellido = a.ApellidoInscrito,
                                Email=a.EmailInscrito,
                            };
                string nombre, apellido,email_;
                nombre = datos.Select(x => x.Nombre).FirstOrDefault().ToString();
                apellido = datos.Select(x => x.Apellido).FirstOrDefault().ToString();
                email_= datos.Select(x => x.Email).FirstOrDefault().ToString();

                string email = email_;//cambiar
                string NombreUsuario = nombre + " " + apellido; ;//cambiar
                MailMessage correo = new MailMessage();
                correo.To.Add(email);

                correo.From = new MailAddress("no.reply@uisek.edu.ec", "Correo Depósitos Inglés Autónomo", System.Text.Encoding.UTF8);
                correo.Subject = "Correo notificación Depósitos Inglés Autónomo";
                correo.SubjectEncoding = System.Text.Encoding.UTF8;
                correo.Body = "Estimad@ " + NombreUsuario + ", \n Ha enviado su solicitud de ingreso de " + ddl_tipo_pago.SelectedItem.Text + ",\n con el concepto de pago " + ddl_concepto.SelectedItem.Text + ",\n " +
                    "desde " + ddl_origen_deposito.SelectedItem.Text + " por la cantidad de " + txt_valor_depositado.Text + "$ Exitosamente" +
                  "  NO RESPONDA A ESTE EMAIL.\n  \n En caso de dudas contacte al departamento técnico.  \n helpdesk@uisek.edu.ec ";
                correo.BodyEncoding = System.Text.Encoding.UTF8;
                string body = "<html><head> " +
                 "<style type=\"text/css\">.style3 { width:30%;  } .style2 {color:red;}.style4 {border:0;}</style>" +
                "</head>" +
                "<body class=\"style4\">" +
                "<form id=\"form1\" runat=\"server\">" +
                "<tr><td colspan=\"2\" style=\"text-align:center; font-size:25px\"><img src='cid:logo' width='100px'></img></td>" +
                "<div><table class=\"style4\" style=\"width:100%;\">" +
                "<tr><td class=\"style3\"></td><td>Estimad@  " + NombreUsuario + ",</td> </tr>" +
                "<tr><td class=\"style3\"></td><td>Ha enviado su solicitud de ingreso de " + ddl_tipo_pago.SelectedItem.Text + ",</td>  " +
                "<tr><td class=\"style3\"></td><td>con el concepto de pago " + ddl_concepto.SelectedItem.Text + ",</td>  " +
                "<tr><td class=\"style3\"></td><td>desde " + ddl_origen_deposito.SelectedItem.Text + " por la cantidad de " + txt_valor_depositado.Text + "$</td>  " +
                "<tr><td class=\"style3\"></td><td>, las personas de tesoreria lo van a revisar,</td>  " +
                "<td class=\"style3\"></td></tr><tr><td class=\"style3\">&nbsp;</td><td class=\"style2\"></td>" +//esta linea fue cambiada y hay que revisar ya que no debe decir localhost

                "<td class=\"style3\"></td></tr><tr><td class=\"style3\">&nbsp;</td><td class=\"style2\">NO RESPONDA A ESTE CORREO. </td>" +
                "<td class=\"style3\">&nbsp;</td></tr><tr><td class=\"style3\">&nbsp;</td><td>En caso de dudas contacte al departamento de Idiomas de la Universidad Internacional SEK.</td>" +

                "<td class=\"style3\">&nbsp;</td></tr> </table></div></form></body></html>";

                System.Net.Mime.ContentType mimeType = new System.Net.Mime.ContentType("text/html");
                // Add the alternate body to the message.

                AlternateView alternate = AlternateView.CreateAlternateViewFromString(body, mimeType);
                LinkedResource logo = new LinkedResource(Server.MapPath("~/Images/") + "logo.png")
                {
                    ContentId = "logo"
                };

                // Lo incrustamos en la vista HTML...
                alternate.LinkedResources.Add(logo);
                correo.AlternateViews.Add(alternate);
                correo.IsBodyHtml = false;
                SmtpClient client = new SmtpClient
                {
                    Credentials = new System.Net.NetworkCredential("no.reply@uisek.edu.ec", Funciones.ConectarMail()),
                    Port = 587,
                    Host = "smtp.gmail.com",
                    EnableSsl = true //Esto es para que vaya a través de SSL que es obligatorio con GMail 
                };
                client.Send(correo);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", @"alert('No se pudo enviar el correo electrónico al alumno'); console.log('" + ex.Message.Replace("'", "**") + "');", true);
            }
        }



        //llenar Tabla de desgloce
        public  void Previsualizar()
        {

            try
            {
                //detalle , creditos , costo 
                //listaCompras = (List<Producto>)Session["listaCompras"];
                //string usuario = (string)Session["usuario"];
                //listaCompras = await CargarInformacion(usuario);
                //asignaturas 
                decimal valorPago = 0;

                //inicia grid valores 

                //llena la cabecera
                TableHeaderRow RowTableHead = new TableHeaderRow();
                RowTableHead.TableSection = TableRowSection.TableHeader;
                string[] encabezados = new string[4] { "Cantidad", "Descripción", "Precio Unitario", "Precio" };
                foreach (var y in encabezados)
                {
                    TableHeaderCell CellHead = new TableHeaderCell
                    {
                        HorizontalAlign = HorizontalAlign.Center,
                        BackColor = Color.LightGray,
                        Text = y
                    };
                    RowTableHead.Cells.Add(CellHead);
                }
                grd_Valores.Rows.Add(RowTableHead);
                //llena el detalle de lo que va pagar
                for (int i = 0; i < listaCompras.Count; i++)
                {
                    TableRow RowTable = new TableRow();

                    TableCell Cell_cant = new TableCell
                    {
                        Text = listaCompras[i].cantidad.ToString()
                    };
                    TableCell Cell_desc = new TableCell
                    {
                        Text = listaCompras[i].concepto.ToString()
                    };
                    TableCell Cell_unit = new TableCell
                    {
                        Text = listaCompras[i].importe.ToString(),
                        CssClass = "dolar"
                    };
                    TableCell Cell_precio = new TableCell
                    {
                        Text = Math.Round(Convert.ToDecimal(listaCompras[i].importe) * Convert.ToDecimal(listaCompras[i].cantidad), 2, MidpointRounding.AwayFromZero).ToString(),
                        CssClass = "dolar"
                    };
                    RowTable.Cells.Add(Cell_cant);
                    RowTable.Cells.Add(Cell_desc);
                    RowTable.Cells.Add(Cell_unit);
                    RowTable.Cells.Add(Cell_precio);

                    grd_Valores.Rows.Add(RowTable);

                }
                //termina de llenando los valores totales y subtotales

                decimal totalsinIVA = 0;
                decimal totalconIVA = 0;
                decimal IVA = 0;
                decimal total = 0;

                bool aux = true;
                for (int j = 0; j < listaCompras.Count; j++)
                {
                    DataSet ds = Conexion.BuscarNAV_ds("[UISEK_ECUADOR$Concepto Facturación] AS A INNER JOIN[UISEK_ECUADOR$Tarifa de concepto] AS B ON  A.[Cód_ concepto] = B.[Cód_ concepto] and getdate() between[Fecha inicial] and  dateadd(minute, 1439,[fecha final])", "[Grupo IVA neg_ producto]",
                        "where a.[cód_ concepto]='" + listaCompras[j].codConcepto + "'");
                    switch (ds.Tables[0].Rows[0][0].ToString())
                    {
                        case "IVA12":
                        case "IVA14":
                            totalconIVA += Math.Round(Convert.ToDecimal(listaCompras[j].importe) * Convert.ToDecimal(listaCompras[j].cantidad), 2, MidpointRounding.AwayFromZero);
                            aux = false;
                            break;
                        default:
                            totalsinIVA += Math.Round(Convert.ToDecimal(listaCompras[j].importe) * Convert.ToDecimal(listaCompras[j].cantidad), 2, MidpointRounding.AwayFromZero);
                            aux = false;
                            break;
                    }
                }
                IVA = Math.Round(totalconIVA * Convert.ToDecimal(0.12), 2, MidpointRounding.AwayFromZero);
                total = (totalconIVA + IVA) + totalsinIVA;
                if (aux)
                    valorPago = 0;
                else
                    valorPago = total;


                TableRow RowTable_IvaCero = new TableRow();
                //ingresa el valor base (sin IVA)
                //texto
                TableCell Cell_IvaCero = new TableCell
                {
                    Text = "Total grabado con IVA tarifa 0% =",
                    ColumnSpan = 3,
                    BackColor = Color.LightGray,
                    HorizontalAlign = HorizontalAlign.Right
                };
                RowTable_IvaCero.Cells.Add(Cell_IvaCero);
                //valor
                TableCell Cell_IvaCero_t = new TableCell
                {
                    Text = "" + totalsinIVA,
                    BackColor = Color.LightGray,
                    HorizontalAlign = HorizontalAlign.Right,
                    CssClass = "dolar"
                };
                RowTable_IvaCero.Cells.Add(Cell_IvaCero_t);

                grd_Valores.Rows.Add(RowTable_IvaCero);
                //ingresa el valor base (con IVA)
                //Texto
                TableRow RowTable_Iva12 = new TableRow();
                TableCell Cell_Iva12 = new TableCell
                {
                    Text = "Total grabado con IVA 12% =",
                    ColumnSpan = 3,
                    BackColor = Color.LightGray,
                    HorizontalAlign = HorizontalAlign.Right
                };
                RowTable_Iva12.Cells.Add(Cell_Iva12);
                //Valor
                TableCell Cell_Iva12_t = new TableCell
                {
                    Text = "" + totalconIVA,
                    BackColor = Color.LightGray,
                    HorizontalAlign = HorizontalAlign.Right,
                    CssClass = "dolar"
                };
                RowTable_Iva12.Cells.Add(Cell_Iva12_t);

                grd_Valores.Rows.Add(RowTable_Iva12);
                //IVA
                TableRow RowTable_Iva = new TableRow();
                //Texto
                TableCell Cell_Iva = new TableCell
                {
                    Text = "Importe del IVA =",
                    ColumnSpan = 3,
                    BackColor = Color.LightGray,
                    HorizontalAlign = HorizontalAlign.Right
                };
                RowTable_Iva.Cells.Add(Cell_Iva);
                //Valor
                TableCell Cell_Iva_t = new TableCell
                {
                    Text = "" + IVA,
                    BackColor = Color.LightGray,
                    HorizontalAlign = HorizontalAlign.Right,
                    CssClass = "dolar"
                };
                RowTable_Iva.Cells.Add(Cell_Iva_t);

                grd_Valores.Rows.Add(RowTable_Iva);
                //inseta el total
                TableRow RowTable_Total = new TableRow();
                //texto
                TableCell Cell_Total = new TableCell
                {
                    Text = "Importe Total = ",
                    ColumnSpan = 3,
                    BackColor = Color.LightGray,
                    HorizontalAlign = HorizontalAlign.Right
                };
                RowTable_Total.Cells.Add(Cell_Total);
                //Valor
                TableCell Cell_Total_t = new TableCell
                {
                    Text = "" + total,
                    BackColor = Color.LightGray,
                    HorizontalAlign = HorizontalAlign.Right,
                    CssClass = "dolar"
                };
                RowTable_Total.Cells.Add(Cell_Total_t);

                grd_Valores.Rows.Add(RowTable_Total);

            }
            catch (Exception ex)
            {
                //envía un mesaje de que sucedió un error
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "ventana", @"alert('Ventana no disponible'); window.close();", true);
                //registra el error en la tabla de errores del pago de matrículas 
                Conexion.Log(ex.Message.Replace("'", "\""), "checkout.aspx");
            }
        }


        #region METODOS
        public async Task<List<Producto>> CargarInformacion(string usuario)
        {
            Producto pro = new Producto();
            //List<Producto> listaCompras = new List<Producto>();
            List<ClInscritoAutonomo> ins = await ServicioExtraerInscritos();
            List<ClPeriodoInscripcion> per = await ServicioExtraerPeriodos();
            List<ClNivel> nivel = await ServicioGetNiveles();
            List<ClCurso> curso = await ServicioCargarCursos();
            List<ClNivelesInscrito> nivIns = await ServicioGetNivelesInscritos();
            List<ClPrueba> prueba = await ServicioExtraerPrueba();
            List<ClTipoNivel> tiponivel = await ServicioCargarTipoNivel();
            List<ClEstadoEstudiante> estadoEstu = await ServicioGetEstadoEstudiante();
            var infoNivelPago = from a in ins
                               
                                join c in nivIns on a.IdInscrito equals c.IDINSCRITO
                                join b in per on c.IDPERIODOINSCRIPCION equals b.IdPeriodoInscripcion
                                join h in prueba on c.IDPRUEBAUBICACION equals h.IdPrueba into NivelesPrueba
                                from i in NivelesPrueba.DefaultIfEmpty()
                                join d in nivel on c.IDNIVEL equals d.idNivel
                                join e in curso on d.idCurso equals e.IdCurso
                                join f in tiponivel on d.idTipoNivel equals f.idtipoNivel
                                join g in estadoEstu on c.IDESTADONIVEL equals g.CodEstadoEstu

                                where a.NumDocInscrito.Trim() == usuario && c.IDESTADONIVEL == 0
                                select new
                                {
                                    IDINSCRITO = a.IdInscrito,
                                    IDNIVEL = c.IDNIVELESTUDIANTE,
                                    NUMDOCINSCRITO = a.NumDocInscrito,
                                    NOMBINSCRITO = a.NombreInscrito,
                                    APELLIINSCRITO = a.ApellidoInscrito,
                                    PERIODO = b.Periodo,
                                    PUNTAJEPRUEBA = NivelesPrueba.Select(x => x.PunjatePrueba).FirstOrDefault(),
                                    Email = a.EmailInscrito,
                                    DESCCURSO = e.DescCurso,
                                    DESCTIPONIVEL = d.descNivel,
                                    NOMNIVEL = d.nomNivel,
                                    DESCNIVEL = d.descNivel,
                                    COSTONIVEL = d.costoNIvel,
                                    CODCURSO = d.codNivel,
                                    IDNIVELESTUDIANTE = c.IDNIVELESTUDIANTE,
                                    IDESTADONIVEL = c.IDESTADONIVEL,
                                    DESCESTADOESTUDIANTE = g.DescEstEstudiante,

                                };



            if (Convert.ToInt32(infoNivelPago.Count()) > 0)
            {
                hiddenPeriodo.Value= infoNivelPago.Select(x => x.PERIODO).FirstOrDefault().Trim().ToString();
                double valorDepositado= infoNivelPago.Select(x => x.COSTONIVEL).FirstOrDefault();
                txt_valor_depositado.Text = valorDepositado.ToString("#.00");
                Session["idNivel"] = infoNivelPago.Select(x => x.IDNIVELESTUDIANTE).FirstOrDefault();
                Session["codNivel"] = infoNivelPago.Select(x => x.CODCURSO).FirstOrDefault().Trim().ToString();
                pro.codConcepto = infoNivelPago.Select(x => x.CODCURSO).FirstOrDefault().ToString().Trim();
                pro.concepto = infoNivelPago.Select(x => x.NOMNIVEL).FirstOrDefault();
                pro.importe = Convert.ToDecimal(infoNivelPago.Select(x => x.COSTONIVEL).FirstOrDefault());
                pro.cantidad = 1;
                pro.iva = 0;
                listaCompras.Add(pro);
            }
            else
            {
                Session["idNivel"] = "";
                Session["codNivel"] = "";
                pro.codConcepto = "";
                pro.concepto = "";
                pro.importe = 0;
                pro.cantidad = 0;
                pro.iva = 0;
            }

                return listaCompras;
        }
        #endregion
        #region Servicios
        public async Task<List<ClNivelesInscrito>> ServicioGetNivelesInscritos()
        {
            List<ClNivelesInscrito> compInf = new List<ClNivelesInscrito>();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/NivelesInscrito");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClNivelesInscrito>>(empResponse);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async Task<List<ClEstadoEstudiante>> ServicioGetEstadoEstudiante()
        {
            List<ClEstadoEstudiante> compInf = new List<ClEstadoEstudiante>();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/EstadoEstudiante");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClEstadoEstudiante>>(empResponse);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async Task<string> getPeriodo(string usuario) {
            List<ClInscritoAutonomo> ins = await ServicioExtraerInscritos();
            List<ClPeriodoInscripcion> periodo = await ServicioExtraerPeriodos();
            List<ClNivelesInscrito> nivelesIns = await ServicioGetNivelesInscritos();
            var query = from a in ins 
                        join c in nivelesIns on a.IdInscrito equals c.IDINSCRITO
                        join b in periodo on c.IDPERIODOINSCRIPCION equals b.IdPeriodoInscripcion
                        where a.NumDocInscrito.Trim().ToString() == usuario.ToString().Trim()
                        select new {
                            Periodo = b.Periodo,
                        };
            return query.Select(x => x.Periodo).FirstOrDefault().ToString().Trim();
        }
        public async Task<List<ClInscritoAutonomo>> getDatosInscrito()
        {
            List<ClInscritoAutonomo> ins = await ServicioExtraerInscritos();


            return ins;
        }

        public async Task<List<ClInscritoAutonomo>> ServicioExtraerInscritos()//cargar todos inscritos
        {
            List<ClInscritoAutonomo> compInf = new List<ClInscritoAutonomo>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/InscritoAutonomo");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClInscritoAutonomo>>(empResponse);
                    return compInf;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async Task<List<ClPeriodoInscripcion>> ServicioExtraerPeriodos()//cargar todos inscritos
        {
            List<ClPeriodoInscripcion> compInf = new List<ClPeriodoInscripcion>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/PeriodoInscripcion");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClPeriodoInscripcion>>(empResponse);

                }

                return compInf;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async Task<List<ClTipoEstudiante>> ServicioGetTipoEstudiante()//cargar todos inscritos
        {
            List<ClTipoEstudiante> compInf = new List<ClTipoEstudiante>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/TipoEstudiante");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClTipoEstudiante>>(empResponse);

                }

                return compInf;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async Task<List<ClPrueba>> ServicioExtraerPrueba()
        {
            List<ClPrueba> compInf = new List<ClPrueba>();
            try
            {
                //string url = "http://servicioinglesuisek/";               
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/Prueba");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClPrueba>>(empResponse);
                    return compInf;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async Task<List<ClNivel>> ServicioGetNiveles()//cargar todos niveles 
        {
            List<ClNivel> compInf = new List<ClNivel>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/Niveles");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClNivel>>(empResponse);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return compInf;
        }

        public async Task<List<ClCurso>> ServicioCargarCursos()//cargar todos inscritos
        {
            List<ClCurso> cur = new List<ClCurso>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/Curso");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    cur = JsonConvert.DeserializeObject<List<ClCurso>>(empResponse);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return cur;
        }

        public async Task<List<ClTipoNivel>> ServicioCargarTipoNivel()
        {
            List<ClTipoNivel> cur = new List<ClTipoNivel>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/TipoNivel");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    cur = JsonConvert.DeserializeObject<List<ClTipoNivel>>(empResponse);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return cur;
        }
        public async void cargarInformacionFactura()
        {

            Producto pro = new Producto();
            //Inscritos
            List<ClInscritoAutonomo> ins = await ServicioExtraerInscritos();
            var queryInscritos = from a in ins

                                 where a.NumDocInscrito.Trim().ToString() == (string)Session["usuario"]
                                 select new
                                 {
                                     IDINSCRITO = a.IdInscrito,
                                     NUMDOCINSCRITO = a.NumDocInscrito,
                                     NOMBINSCRITO = a.NombreInscrito,
                                     APELLIINSCRITO = a.ApellidoInscrito,
                                     Email = a.EmailInscrito,
                                     Direccion = a.DirecInscrito,
                                     Telefono = a.TelefInscrito,
                                     Celular = a.CeluInscrito,
                                     TIPODOCUMENTO = a.IdTipoDocumento,//1 cedula 2 pasaporte


                                 };


            string nombre = queryInscritos.Select(x => x.NOMBINSCRITO).FirstOrDefault().ToString().Trim();
            string apellido = queryInscritos.Select(x => x.APELLIINSCRITO).FirstOrDefault().ToString().Trim();
            string nombreCompleto = nombre + " " + apellido;
            txt_nombre.Text = nombreCompleto;
            ddlTipo.SelectedValue = queryInscritos.Select(x => x.TIPODOCUMENTO).FirstOrDefault().ToString();
            txt_direccion.Text = queryInscritos.Select(x => x.Direccion).FirstOrDefault().ToString().Trim();
            txt_correo.Text = queryInscritos.Select(x => x.Email).FirstOrDefault().ToString().Trim();
            txt_telefono.Text = queryInscritos.Select(x => x.Celular).FirstOrDefault().ToString().Trim();
            txt_cedula.Text = queryInscritos.Select(x => x.NUMDOCINSCRITO).FirstOrDefault().ToString().Trim();
        }

        #endregion

    }
}