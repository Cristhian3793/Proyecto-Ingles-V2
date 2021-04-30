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

        public async void ServicioInsertrInscrito(ClInscritoAutonomo insA)
        {
            ClUsuarios user = new ClUsuarios();
            DateTime fecha = DateTime.Now;
            bool resp = await ValidarInscrito(insA.NumDocInscrito, insA.idPerInscripcion);
            if (resp == false)
            {
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

                        /*EXTRAER ID INSCRITO REGISTRADO SI USUARIO ELIGE OPCION DAR PRUEBA*/
                        long lastid = await ServicioExtraerUltimoIDInscrito();
                        /*INSERTAR PRUEBA SI USUARIO ELIGE OPCION DAR PRUEBA*/
                        ClPrueba pru = new ClPrueba();
                        pru.IdInscrito = Convert.ToInt64(lastid);
                        pru.PunjatePrueba = null;
                        pru.FechaPrueba = Convert.ToString(fecha);
                        ServicioIngresarPrueba(pru);

                        user.Usuario = insA.NumDocInscrito;
                        user.Password = "Uisek*";
                        user.Nombres = "";
                        user.Apellidos = "";
                        user.tipoUser = 1;
                        user.Nombres = insA.NombreInscrito;
                        user.Apellidos = insA.ApellidoInscrito;
                        ServicioInsertarUser(user);
                        await EnviarCorreo(txtEmail.Text.ToString(), "Registro Ingles Autónomo UISEK", "Registro Exitoso, sus credenciales para acceder al sistema son:", user.Usuario, user.Password);
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
            else {
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
            bool resp = false;
            var query = from a in inscrito
                        where a.NumDocInscrito.Trim().ToString() == cedula.Trim().ToString() && a.idPerInscripcion == idPeriodo
                        select new
                        {
                            numDoc = a.NumDocInscrito,
                            idPer = a.idPerInscripcion,
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
        public Task EnviarCorreo(string email, string subject, string message,string user,string pass)
        {
            string text = message;

            AlternateView plainView =
            AlternateView.CreateAlternateViewFromString(text,
                            Encoding.UTF8,
                            MediaTypeNames.Text.Plain);

            string html = "<h2>" + message + "</h2>" +
               "<p> Usuario :" + user + "</p>" +
               "<p> Contraseña :" + pass + "</p>";
            AlternateView htmlView =
            AlternateView.CreateAlternateViewFromString(html,
            Encoding.UTF8,
            MediaTypeNames.Text.Html);
            LinkedResource img =
                         new LinkedResource(@"C:\Users\UISEK\source\repos\Proyecto Ingles V2\Proyecto Ingles V2\images\uisek-mail.png",
                         MediaTypeNames.Image.Jpeg);
            img.ContentId = "imagen";
            htmlView.LinkedResources.Add(img);
            try
            {
                // Credenciales
                var credentials = new NetworkCredential("cristhian.tupiza@uisek.edu.ec", "Alexiscrow3793");
                // Mail message
                var mail = new MailMessage()
                {
                    From = new MailAddress("cristhian.tupiza@uisek.edu.ec", "Cristhian Tupiza"),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };
                mail.AlternateViews.Add(plainView);
                mail.AlternateViews.Add(htmlView);
                mail.To.Add(new MailAddress(email));
                // Smtp client
                var client = new SmtpClient()
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Credentials = credentials
                };
                // Send it...         
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            return Task.CompletedTask;
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
        public  void guardarDatos()
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

                    insA.idPerInscripcion = Convert.ToInt64(cbxPeriodoLectivo.SelectedValue.ToString());
                    insA.IdNivel = null;
                    insA.IdTipoDocumento = tipoDocumento;
                    insA.IdTipoEstudiante = tipoEstudiante;
                    insA.NombreInscrito = txtNombres.Text.ToUpper().Trim();
                    insA.ApellidoInscrito = txtApellidos.Text.ToUpper().Trim();
                    insA.NumDocInscrito = txtCed.Text.ToUpper().Trim();
                    insA.CeluInscrito = txtCelular.Text.ToUpper().Trim();
                    insA.TelefInscrito = txtTelefono.Text.ToUpper().Trim();
                    insA.DirecInscrito = txtDireccion.Text.ToUpper().Trim();
                    insA.EmailInscrito = txtEmail.Text.ToUpper().Trim();
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
                    insA.InformacionCurso = txtInformacion.Text.ToUpper().Trim();
                if (Convert.ToInt32(correcto.Value) == 0)
                {
                    lblCedula.Visible = true;
                    lblCedula.Text = "La Cedula es Incorrecta";

                }
                else
                {
                    lblCedula.Text = "";
                    ServicioInsertrInscrito(insA);
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

        #endregion

    }
}