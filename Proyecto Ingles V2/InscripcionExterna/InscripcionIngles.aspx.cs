using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Cfg;
using Logica.Conexion;
using System.Data.SqlClient;
using System.Data;
using Logica.Servicios;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using Logica.ConexionServicios;
using Logica.Modelos;
using Logica;
using System.Web.UI.HtmlControls;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

using System.Net.Mail;
using System.Net.Mime;
using Proyecto_Ingles_V2.LoginDb;

namespace Proyecto_Ingles_V2.InscripcionExterna
{
    public partial class InscripcionIngles : System.Web.UI.Page
    {
        static conexionServidor cs = new conexionServidor();
        string url = cs.url.ToString();
        ISession mySesion = SessionFactory.OpenSession;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                cargarComboPeriodo();

            }
            if (RabCedula.Checked == true)
            {
                txtCed.MaxLength = 10;
            }


        }
        #region Invocacion Servicios
        public async Task<List<ClInscritoAutonomo>> ServicioGetInscritos()
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
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async Task<List<ClPrueba>> ServicioGetPruebas()
        {
            List<ClPrueba> compInf = new List<ClPrueba>();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/Prueba");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClPrueba>>(empResponse);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;

        }
        public async Task<bool> validarExistePrueba(string numdoc)
        {
            bool resp = false;
            List<ClInscritoAutonomo> inscrito = await ServicioGetInscritos();
            List<ClPrueba> prueba = await ServicioGetPruebas();
            var pruebas = from a in inscrito
                          join b in prueba on a.IdInscrito equals b.IdInscrito
                          where a.NumDocInscrito.Trim() == numdoc.Trim()
                          select new
                          {
                              IdInscrito = a.IdInscrito,
                              NumDocumento = a.NumDocInscrito,

                          };
            if (pruebas.Count() > 0)//ya existe una prueba
            {
                resp = true;
            }
            return resp;
        }
        public async Task<List<ClPeriodoInscripcion>> ServicioGetPeriodosInscripcion()
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
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;

        }
        public async void ServicioInsertarUser(ClUsuarios usu)
        {

            try
            {
                string uri = "api/Usuarios";
                var myContent = JsonConvert.SerializeObject(usu);
                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.PostAsync(uri, stringContent);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public async void ServicioIngresarPrueba(ClPrueba pru)
        {
            try
            {
                string uri = "api/Prueba";
                var myContent = JsonConvert.SerializeObject(pru);
                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.PostAsync(uri, stringContent);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task<long> ServicioExtraerUltimoIDInscrito()
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
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            var lastid = compInf.OrderByDescending(x => x.IdInscrito).Select(x => x.IdInscrito).FirstOrDefault();
            return Convert.ToInt64(lastid);
        }
        public async Task<long> extraerIdNivel()
        {
            List<ClNivel> nivel = await ServicioGetNiveles();
            var query = from x in nivel
                        where x.nomNivel.Trim().ToString() == "HIGH BEGINNER 1"
                        select new
                        {
                            IdNivel = x.idNivel,
                        };
            return query.Select(x => x.IdNivel).FirstOrDefault();

        }
        public async Task<int> extraerIdInscrito()
        {
            List<ClEstadoEstudiante> estadoEstu = await ServicioGetEstadoEstudiante();
            int id = estadoEstu.Where(x => x.IdEstadoEstudiante == 1).Select(x => x.IdEstadoEstudiante).FirstOrDefault();
            return id;
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
        public async Task<List<ClNivel>> ServicioGetNiveles()
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
        public async Task<long> ServicioExtraerNivelBasico()
        {
            //SEK1203
            List<ClNivel> nivel = await ServicioGetNiveles();
            var query = nivel.Where(x => x.codNivel.Trim() == "SEK1333").Select(x => x.idNivel).FirstOrDefault();
            return query;

        }
        public async Task<long> ServicioExtraerNivelprueba()
        {
            //SEK1203
            List<ClNivel> nivel = await ServicioGetNiveles();
            var query = nivel.Where(x => x.codNivel.Trim() == "SEK1203").Select(x => x.idNivel).FirstOrDefault();
            return query;

        }
        public async Task<long> IdPruebaUbicacion(long idInscrito)
        {
            List<ClNivelesInscrito> niveles = new List<ClNivelesInscrito>();
            niveles = await ServicioGetNivelesInscritos();
            var lastid = niveles.OrderByDescending(x => x.IDNIVELESTUDIANTE).Select(x => x.IDNIVELESTUDIANTE).FirstOrDefault();
            return lastid;
        }
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
        public async void ServicioInsertarNivelIns(ClNivelesInscrito pru, long lastid)
        {
            try
            {
                string uri = "api/NivelesInscrito";
                var myContent = JsonConvert.SerializeObject(pru);
                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.PostAsync(uri, stringContent);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    //extraer id prueba

                    //insertar en prueba
                    if (pru.PRUEBA == 1)
                    {
                        long idPUbicacion = await IdPruebaUbicacion(lastid);
                        ClPrueba pruU = new ClPrueba();
                        pruU.IdInscrito = lastid;
                        pruU.PunjatePrueba = null;
                        pruU.FechaPrueba = Convert.ToString(DateTime.Now);
                        pruU.IDNIVELESTUDIANTE = idPUbicacion;
                        ServicioIngresarPrueba(pruU);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async void ServicioInsertrInscrito(ClInscritoAutonomo insA,long idperiodo)
        {
            DateTime fecha = DateTime.Now;
            bool resp = await ValidarInscrito(insA.NumDocInscrito, idperiodo);
            bool respPrueba = await validarExistePrueba(insA.NumDocInscrito.Trim());
            long lastid;

            if (resp == false)
            {
                if (respPrueba == false)
                {
                    ClUsuarios user = new ClUsuarios();
                    insA.IdEstadoEstudiante = await extraerIdInscrito();
                    try
                    {
                        string uri = "api/InscritoAutonomo";
                        var myContent = JsonConvert.SerializeObject(insA);
                        var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
                        var client = new HttpClient();
                        client.BaseAddress = new Uri(url);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                        HttpResponseMessage res = await client.PostAsync(uri, stringContent);
                        if (res.IsSuccessStatusCode)
                        {
                            var empResponse = res.Content.ReadAsStringAsync().Result;
                            if (insA.EstadoPrueba == 1)
                            {
                                /*EXTRAER ID INSCRITO REGISTRADO SI USUARIO ELIGE OPCION DAR PRUEBA*/
                                List<ClInscritoAutonomo> listaInscritos = new List<ClInscritoAutonomo>();
                                listaInscritos = await ServicioGetInscritos();
                                lastid = listaInscritos.OrderByDescending(x => x.IdInscrito).Select(x => x.IdInscrito).FirstOrDefault();
                                /*INSERTAR PRUEBA SI USUARIO ELIGE OPCION DAR PRUEBA*/
                                ClNivelesInscrito niveles = new ClNivelesInscrito();
                                niveles.IDINSCRITO = lastid;
                                niveles.IDNIVEL = await ServicioExtraerNivelprueba();
                                niveles.IDESTADONIVEL = 0;
                                niveles.FECHAREGISTRO = Convert.ToString(DateTime.Now);
                                niveles.PRUEBA = 1;
                                niveles.ESTADONIVEL = 0;
                                ServicioInsertarNivelIns(niveles, lastid);


                            }
                            else
                            {
                                /*EXTRAER ID INSCRITO REGISTRADO SI USUARIO ELIGE OPCION NO DAR PRUEBA*/
                                List<ClInscritoAutonomo> listaInscritos = new List<ClInscritoAutonomo>();
                                listaInscritos = await ServicioGetInscritos();
                                lastid = listaInscritos.OrderByDescending(x => x.IdInscrito).Where(x => x.NumDocInscrito.Trim() == insA.NumDocInscrito.Trim()).Select(x => x.IdInscrito).FirstOrDefault();
                                /*INSERTAR PRUEBA SI USUARIO ELIGE OPCION DAR PRUEBA*/
                                ClNivelesInscrito niveles = new ClNivelesInscrito();
                                niveles.IDINSCRITO = lastid;
                                niveles.IDNIVEL = await ServicioExtraerNivelBasico();
                                niveles.IDESTADONIVEL = 0;
                                niveles.PRUEBA = 0;
                                niveles.ESTADONIVEL = 0;
                                niveles.FECHAREGISTRO = Convert.ToString(DateTime.Now);
                                ServicioInsertarNivelIns(niveles, lastid);
                            }
                            string password = encriptarClave("Uisek*");
                            user.Usuario = insA.NumDocInscrito;
                            user.Password = password;
                            user.Nombres = "";
                            user.Apellidos = "";
                            user.tipoUser = 1;
                            user.Nombres = insA.NombreInscrito;
                            user.Apellidos = insA.ApellidoInscrito;
                            user.idInscrito = lastid;
                            ServicioInsertarUser(user);
                            EnviaCorreo_Alumno();
                            string script = @"Swal.fire({
                            icon: 'success',
                            title: 'OK',
                            text: 'Registro Correcto Revise su correo!',
                            footer: '<a href></a>'
                        })";
                            ClientScript.RegisterStartupScript(GetType(), "script", script, true);
                            limpiarCampos();
                        }
                        else
                        {
                            string script = @"Swal.fire({
                            icon: 'error',
                            title: 'error',
                            text: 'Registro No se pudo Guardar',
                            footer: '<a href></a>'
                            })";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
                            limpiarCampos();
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    string script = @"Swal.fire({
                    icon: 'error',
                    title: 'error',
                    text: 'Usted ya se encuentra registrado para rendir prueba de ubicación',
                    footer: '<a href></a>'
                    })";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
                }
            }
            else
            {
                string script = @"Swal.fire({
                    icon: 'error',
                    title: 'error',
                    text: 'Usted ya se encuentra Registrado',
                    footer: '<a href></a>'
                    })";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
                limpiarCampos();
            }

        }

        #endregion

        #region Metodos
        public async Task<bool> ValidarInscrito(string cedula, long idPeriodo)
        {
            List<ClInscritoAutonomo> inscrito = await ServicioGetInscritos();
            List<ClPeriodoInscripcion> periodo = await ServicioGetPeriodosInscripcion();
            List<ClNivelesInscrito> nivelesInscrito = await ServicioGetNivelesInscritos();
            bool resp = false;
            var query = from a in inscrito
                        join b in nivelesInscrito on a.IdInscrito equals b.IDINSCRITO
                        join c in periodo on b.IDPERIODOINSCRIPCION equals c.IdPeriodoInscripcion
                        where a.NumDocInscrito.Trim().ToString() == cedula.Trim().ToString() && b.IDPERIODOINSCRIPCION == idPeriodo
                        select new
                        {
                            numDoc = a.NumDocInscrito,
                            idPer = b.IDPERIODOINSCRIPCION,
                        };
            if (query.Count() > 0)
            {
                resp = true;
                return resp;

            }
            else
            {
                resp = false;
                return resp;
            }
        }
        public void traerDatosAlumnoUISEK()
        {
            string cedula = txtCed.Text.Trim();
            //construir servicio y llamar a servicio web que se conecte con base donde estan alumnos UISEK

        }
        public async void cargarComboPeriodo()
        {
            List<ClPeriodoInscripcion> periodos = new List<ClPeriodoInscripcion>();
            periodos = await ServicioGetPeriodosInscripcion();
            var query = periodos.Where(x => x.EstadoPeriodo == 1);
            cbxPeriodoLectivo.DataSource = query;
            cbxPeriodoLectivo.DataValueField = "IdPeriodoInscripcion";
            cbxPeriodoLectivo.DataTextField = "Periodo";
            cbxPeriodoLectivo.DataBind();
        }
        public void EnviaCorreo_Alumno()
        {
            try
            {
                string email = txtEmail.Text.Trim();
                string NombreUsuario = txtNombres.Text.Trim().ToUpper() + " " + txtApellidos.Text.Trim().ToUpper();//cambiar
                MailMessage correo = new MailMessage();
                correo.To.Add(email);

                correo.From = new MailAddress("no.reply@uisek.edu.ec", "Correo Registro Inglés Autónomo", System.Text.Encoding.UTF8);
                correo.Subject = "Correo notificación Registro Inglés Autónomo";
                correo.SubjectEncoding = System.Text.Encoding.UTF8;
                correo.Body = "Estimad@ " + NombreUsuario + ", \n usted Ha sido registrado correctamente en el curso de Íngles Autónomo" + ",\n " +

                  "  NO RESPONDA A ESTE EMAIL.\n  \n En caso de dudas contacte al departamento de Íngles.  \n helpdesk@uisek.edu.ec ";
                correo.BodyEncoding = System.Text.Encoding.UTF8;
                string body = "<html><head> " +
                 "<style type=\"text/css\">.style3 { width:20%;  } .style2 {color:red;}.style4 {border:0;}</style>" +
                "</head>" +
                "<body class=\"style4\">" +
                "<form id=\"form1\" runat=\"server\">" +
                "<tr><td colspan=\"2\" style=\"text-align:center; font-size:25px\"><img src='cid:logo' width='100px'></img></td>" +
                "<div><table class=\"style4\" style=\"width:100%;\">" +
                "<tr><td class=\"style3\"></td><td>Estimad@  " + NombreUsuario + ",</td> </tr>" +
                "<tr><td class=\"style3\"></td><td>Ha sido Registrado correctamente en el sistema de Íngles Autónomo </td>" +
                "<tr><td class=\"style3\"></td><td>Sus Credenciales para acceder al sistema son:" + "</td>" +
                "<tr><td class=\"style3\"></td><td><b>Usuario:</b>" + txtCed.Text.Trim() + "</td>" +
                "<tr><td class=\"style3\"></td><td><b>Contraseña: </b>" + "Uisek*" + "</td>" +
                "<tr><td class=\"style3\"></td><td><b>Dirección de Acceso al Sistema: </b>" + "https://localhost:44306/Login/formlogin" + "</td>  " +
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
        public void limpiarCampos()
        {
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtCelular.Text = "";
            txtDireccion.Text = "";
            txtEmail.Text = "";
            txtTelefono.Text = "";
            txtCed.Text = "";
            txtInformacion.Text = "";
            RabExamen.Checked = false;
            RabCedula.Checked = false;
            RabTipoEstudiante.Checked = false;


        }
        public void guardarDatos()
        {
            int tipoDocumento = 0;
            int tipoEstudiante = 0;
            short varexamen = 0;
            DateTime fecha = DateTime.Now;
            ClInscritoAutonomo insA = new ClInscritoAutonomo();
            try
            {

                if (RabCedula.Checked == true)
                {
                    tipoDocumento = 1;
                }
                else if (RabPasaporte.Checked == true)
                {
                    tipoDocumento = 2;
                }

                if (RabTipoEstudiante.Checked == true)
                {

                    tipoEstudiante = 2;
                }
                else
                {
                    tipoEstudiante = 1;
                }
                long Idperiodo = Convert.ToInt64(cbxPeriodoLectivo.SelectedValue.ToString().Trim());
                insA.IdTipoDocumento = tipoDocumento;
                insA.IdTipoEstudiante = tipoEstudiante;
                insA.NombreInscrito = txtNombres.Text.ToUpper().Trim();
                insA.ApellidoInscrito = txtApellidos.Text.ToUpper().Trim();
                insA.NumDocInscrito = txtCed.Text.Trim();
                insA.CeluInscrito = txtCelular.Text.Trim();
                insA.TelefInscrito = txtTelefono.Text.Trim();
                insA.DirecInscrito = txtDireccion.Text.ToUpper().Trim();
                insA.EmailInscrito = txtEmail.Text.Trim();
                insA.FechaRegistro = Convert.ToString(fecha);

                if (RabExamen.Checked == true)
                {
                    varexamen = 1;
                }
                else
                {
                    varexamen = 0;
                }
                insA.EstadoPrueba = varexamen;
                insA.InformacionCurso = txtInformacion.Text.ToUpper();
                if (Convert.ToInt32(correcto.Value) == 0)
                {
                    lblCedula.Visible = true;
                    lblCedula.Text = "La Cedula es Incorrecta";

                }
                else
                {
                    lblCedula.Text = "";
                    if (insA.NumDocInscrito.Trim().ToString() != "")
                        ServicioInsertrInscrito(insA, Idperiodo);
                    else
                    {
                        string script = "rechazado();";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        #endregion

        #region Metodos Forms
        protected void btnGuardarInscrito_Click(object sender, EventArgs e)
        {
            guardarDatos();
   
        }
        protected void RabCedula_CheckedChanged(object sender, EventArgs e)
        {
            txtCed.Text = "";
            txtCed.MaxLength = 10;

        }


        protected void RabPasaporte_CheckedChanged(object sender, EventArgs e)
        {
            txtCed.Text = "";
            txtCed.MaxLength = 12;
        }

        public string encriptarClave(string clave) {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(clave);
            result = Convert.ToBase64String(encryted);
            return result;
        }
        #endregion
    }
}