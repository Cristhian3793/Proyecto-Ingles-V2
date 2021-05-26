using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica.Modelos;
using Logica;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
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
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using ClosedXML.Excel;
using System.Net.Mail;
using System.Net.Mime;

namespace Proyecto_Ingles_V2.Interfaces
{
    public partial class ConsultaInscritos : System.Web.UI.Page
    {
        static conexionServidor cs = new conexionServidor();
        string url = cs.url.ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                if (Session["usuario"] == null || (string)Session["usuario"] == "")
                {
                    Response.Redirect("../Login/formLogin.aspx");
                }
                if ((int)Session["TipoUser"] == 1)
                {
                    Response.Redirect("../Interfaces/Default.aspx");
                }
                else
                {
                    CargarPeriodos();
                    joinTodosInscritos();
                }

            }
            
        }
        #region Servicios
        public async Task<List<ClEstadoPrueba>> ServicioExtraerEstadoPrueba()//cargar todos inscritos
        {
            List<ClEstadoPrueba> compInf = new List<ClEstadoPrueba>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/EstadoPrueba");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClEstadoPrueba>>(empResponse);
                    return compInf;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
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
                HttpResponseMessage res =await client.GetAsync("api/InscritoAutonomo");
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
        public async Task<List<ClTipoEstudiante>> ServicioExtraerTipoEstudiante()
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
                    return compInf;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }

        public async Task<List<ClPeriodoInscripcion>> ServicioExtraerPeriodos()
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
                    return compInf;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }

        public async void ServicioActualizarInscrito(ClInscritoAutonomo inscrito) {

            try { 
            string uri = "api/InscritoAutonomo?idInscrito=" + inscrito.IdInscrito;
            var myContent = JsonConvert.SerializeObject(inscrito);
            var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
            HttpResponseMessage res = await client.PutAsync(uri, stringContent);
            if (res.IsSuccessStatusCode)
            {
                var empResponse = res.Content.ReadAsStringAsync().Result;
                joinTodosInscritos();
            }
            
            
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public async void ServicioEliminarRegistro(int idInscrito)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.DeleteAsync("api/InscritoAutonomo/" + idInscrito);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                }
                joinTodosInscritos();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
        #endregion

        #region Metodos
        public void limpiardgv()
        {
            dgvInscrito.DataSource = null;
            dgvInscrito.DataBind();
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
        public async void joinTodosInscritos()
        {
            List<ClInscritoAutonomo> inscrito = new List<ClInscritoAutonomo>();
            List<ClTipoEstudiante> tipoEstudiante = new List<ClTipoEstudiante>();
            List<ClPeriodoInscripcion> periodos = new List<ClPeriodoInscripcion>();
            List<ClEstadoPrueba> estpru = new List<ClEstadoPrueba>();
            List<ClNivelesInscrito> nivelesIns = new List<ClNivelesInscrito>();
            List<ClEstadoEstudiante> estado = await ServicioGetEstadoEstudiante();
            tipoEstudiante = await ServicioExtraerTipoEstudiante();
            inscrito = await ServicioExtraerInscritos();
            periodos = await ServicioExtraerPeriodos();
            estpru = await ServicioExtraerEstadoPrueba();
            nivelesIns = await ServicioGetNivelesInscritos();

                   var query = from a in inscrito
                        join f in nivelesIns on a.IdInscrito equals f.IDINSCRITO
                        join b in periodos on f.IDPERIODOINSCRIPCION equals b.IdPeriodoInscripcion
                        join c in tipoEstudiante on a.IdTipoEstudiante equals c.IdTipoEstudiante
                        join d in estado on a.IdEstadoEstudiante equals d.IdEstadoEstudiante
                        join e in estpru on a.EstadoPrueba equals e.CodEstadoPrueba
                        orderby a.ApellidoInscrito ascending
                        select new
                        {
                            IdInscrito = a.IdInscrito,
                            IdTipoDocumento = a.IdTipoDocumento,
                            TipoEstudiante = c.DescTipoEstudiante,
                            NombreInscrito = a.NombreInscrito,
                            ApellidoInscrito = a.ApellidoInscrito,
                            NumDocInscrito = a.NumDocInscrito,
                            CeluInscrito = a.CeluInscrito,
                            TelefInscrito = a.TelefInscrito,
                            DirecInscrito = a.DirecInscrito,
                            EmailInscrito = a.EmailInscrito,
                            FechaRegistro = a.FechaRegistro,
                            EstadoPrueba = a.EstadoPrueba,
                            PeriodoLectivo = b.Periodo,
                            Prueba=e.DescEstadoPrueba,
                            Estado=d.DescEstEstudiante,
                            Info=a.InformacionCurso,
                        };
            dgvInscrito.DataSource = query.ToList();
            dgvInscrito.DataBind();
        }
        public async void joinTodosInscritosxPeriodo(int periodo)
        {
            List<ClInscritoAutonomo> inscrito = new List<ClInscritoAutonomo>();
            List<ClTipoEstudiante> tipoEstudiante = new List<ClTipoEstudiante>();
            List<ClPeriodoInscripcion> periodos = new List<ClPeriodoInscripcion>();
            List<ClEstadoPrueba> estpru = new List<ClEstadoPrueba>();
            List<ClNivelesInscrito> nivelesIns = new List<ClNivelesInscrito>();
            List<ClEstadoEstudiante> estado = await ServicioGetEstadoEstudiante();
            tipoEstudiante = await ServicioExtraerTipoEstudiante();
            inscrito = await ServicioExtraerInscritos();
            periodos = await ServicioExtraerPeriodos();
            estpru = await ServicioExtraerEstadoPrueba();
            nivelesIns = await ServicioGetNivelesInscritos();
            var query = from a in inscrito
                        join f in nivelesIns on a.IdInscrito equals f.IDINSCRITO
                        join b in periodos on f.IDPERIODOINSCRIPCION equals b.IdPeriodoInscripcion
                        join c in tipoEstudiante on a.IdTipoEstudiante equals c.IdTipoEstudiante
                        join d in estado on a.IdEstadoEstudiante equals d.IdEstadoEstudiante
                        join e in estpru on a.EstadoPrueba equals e.CodEstadoPrueba
                        where b.IdPeriodoInscripcion==periodo
                        orderby a.ApellidoInscrito ascending
                        select new
                        {
                            IdInscrito = a.IdInscrito,
                            IdTipoDocumento = a.IdTipoDocumento,
                            TipoEstudiante = c.DescTipoEstudiante,
                            NombreInscrito = a.NombreInscrito,
                            ApellidoInscrito = a.ApellidoInscrito,
                            NumDocInscrito = a.NumDocInscrito,
                            CeluInscrito = a.CeluInscrito,
                            TelefInscrito = a.TelefInscrito,
                            DirecInscrito = a.DirecInscrito,
                            EmailInscrito = a.EmailInscrito,
                            FechaRegistro = a.FechaRegistro,
                            EstadoPrueba = a.EstadoPrueba,
                            PeriodoLectivo = b.Periodo,
                            Prueba = e.DescEstadoPrueba,
                            Estado = d.DescEstEstudiante,
                            Info = a.InformacionCurso,
                        };
            dgvInscrito.DataSource = query.ToList();
            dgvInscrito.DataBind();
        }

        public async void buscarInscritoxIdentificacion(string identificacion)
        {
            List<ClInscritoAutonomo> inscrito = new List<ClInscritoAutonomo>();
            List<ClTipoEstudiante> tipoEstudiante = new List<ClTipoEstudiante>();
            List<ClPeriodoInscripcion> periodos = new List<ClPeriodoInscripcion>();
            List<ClEstadoEstudiante> estado = await ServicioGetEstadoEstudiante();
            List<ClEstadoPrueba> estpru = new List<ClEstadoPrueba>();
            List<ClNivelesInscrito> nivelesIns = new List<ClNivelesInscrito>();
            tipoEstudiante = await ServicioExtraerTipoEstudiante();
            inscrito = await ServicioExtraerInscritos();
            periodos = await ServicioExtraerPeriodos();
            estpru = await ServicioExtraerEstadoPrueba();
            nivelesIns = await ServicioGetNivelesInscritos();
            var query = from a in inscrito
                        join f in nivelesIns on a.IdInscrito equals f.IDINSCRITO
                        join b in periodos on f.IDPERIODOINSCRIPCION equals b.IdPeriodoInscripcion
                        join c in tipoEstudiante on a.IdTipoEstudiante equals c.IdTipoEstudiante
                        join d in estado on a.IdEstadoEstudiante equals d.IdEstadoEstudiante
                        join e in estpru on a.EstadoPrueba equals e.CodEstadoPrueba
                        where a.NumDocInscrito.Trim() == identificacion
                        select new
                        {
                            IdInscrito = a.IdInscrito,
                            IdTipoDocumento = a.IdTipoDocumento,
                            TipoEstudiante = c.DescTipoEstudiante,
                            NombreInscrito = a.NombreInscrito,
                            ApellidoInscrito = a.ApellidoInscrito,
                            NumDocInscrito = a.NumDocInscrito,
                            CeluInscrito = a.CeluInscrito,
                            TelefInscrito = a.TelefInscrito,
                            DirecInscrito = a.DirecInscrito,
                            EmailInscrito = a.EmailInscrito,
                            FechaRegistro = a.FechaRegistro,
                            EstadoPrueba = a.EstadoPrueba,
                            PeriodoLectivo = b.Periodo,
                            Prueba = e.DescEstadoPrueba,
                            Estado = d.DescEstEstudiante,
                            Info = a.InformacionCurso,
                        };
            dgvInscrito.DataSource = query.ToList();
            dgvInscrito.DataBind();
        }
        public async void llenarmodal(long idinscrito)
        {
            
            List<ClInscritoAutonomo> inscrito = new List<ClInscritoAutonomo>();
            List<ClTipoEstudiante> tipoEstudiante = new List<ClTipoEstudiante>();
            List<ClPeriodoInscripcion> periodos = new List<ClPeriodoInscripcion>();
            List<ClNivelesInscrito> nivelesIns = new List<ClNivelesInscrito>();
            List<ClEstadoEstudiante> estado = await ServicioGetEstadoEstudiante();
            tipoEstudiante = await ServicioExtraerTipoEstudiante();
            inscrito = await ServicioExtraerInscritos();
            periodos = await ServicioExtraerPeriodos();
            nivelesIns = await ServicioGetNivelesInscritos();
            var query = from a in inscrito
                        join f in nivelesIns on a.IdInscrito equals f.IDINSCRITO
                        join b in periodos on f.IDPERIODOINSCRIPCION equals b.IdPeriodoInscripcion
                        join c in tipoEstudiante on a.IdTipoEstudiante equals c.IdTipoEstudiante
                        join d in estado on a.IdEstadoEstudiante equals d.IdEstadoEstudiante
                        where a.IdInscrito== idinscrito
                        select new
                        {
                            IdInscrito = a.IdInscrito,
    
                            IdTipoDocumento = a.IdTipoDocumento,
                            IdTipoestudiante=a.IdTipoEstudiante,
                            TipoEstudiante = c.DescTipoEstudiante,
                            NombreInscrito = a.NombreInscrito,
                            ApellidoInscrito = a.ApellidoInscrito,
                            NumDocInscrito = a.NumDocInscrito,
                            CeluInscrito = a.CeluInscrito,
                            TelefInscrito = a.TelefInscrito,
                            DirecInscrito = a.DirecInscrito,
                            EmailInscrito = a.EmailInscrito,
                            FechaRegistro = a.FechaRegistro,
                            EstadoPrueba = a.EstadoPrueba,
                            IdPeriodo=f.IDPERIODOINSCRIPCION,
                            PeriodoLectivo = b.Periodo,
                            InformacionCurso =a.InformacionCurso,
                        };
            if (query.Select(x => x.IdTipoestudiante).FirstOrDefault() == 1)
            {

                RabTipoEstudiante.Checked = false;
            }
            else if(query.Select(x => x.IdTipoestudiante).FirstOrDefault() == 2)
                RabTipoEstudiante.Checked = true;
            if (query.Select(x => x.EstadoPrueba).FirstOrDefault() == 0) {
                RabPruebaUbicacion.Checked = false;
            }
            else if (query.Select(x => x.EstadoPrueba).FirstOrDefault() == 1) {
                RabPruebaUbicacion.Checked = true;
            }
            cbxPeriodoLectivo.DataSource = periodos;
            cbxPeriodoLectivo.DataValueField = "IdPeriodoInscripcion";
            cbxPeriodoLectivo.DataTextField = "Periodo";
            cbxPeriodoLectivo.DataBind();
            cbxPeriodoLectivo.SelectedValue = query.Select(x => x.IdPeriodo).FirstOrDefault().ToString();
            txtId.Text = idinscrito.ToString();
            txtIdentificacion.Text = query.Select(x => x.NumDocInscrito).FirstOrDefault().Trim();
            txtNombres.Text = query.Select(x => x.NombreInscrito).FirstOrDefault().Trim();
            txtApellidos.Text = query.Select(x => x.ApellidoInscrito).FirstOrDefault().Trim();
            txtCelular.Text = query.Select(x => x.CeluInscrito).FirstOrDefault().Trim();
            txtDireccion.Text = query.Select(x => x.DirecInscrito).FirstOrDefault().Trim();
            txtEmail.Text = query.Select(x => x.EmailInscrito).FirstOrDefault().Trim().ToLower();
            txtTelefono.Text = query.Select(x => x.TelefInscrito).FirstOrDefault().Trim();
            txtInformacion.Text = query.Select(x => x.InformacionCurso).FirstOrDefault().Trim();
        }

        public async void CargarPeriodos()
        {
            List<ClPeriodoInscripcion> periodos = new List<ClPeriodoInscripcion>();
            periodos = await ServicioExtraerPeriodos();
            cbxPeriodo.DataSource = periodos;
            cbxPeriodo.DataValueField = "IdPeriodoInscripcion";
            cbxPeriodo.DataTextField = "Periodo";
            cbxPeriodo.DataBind();
        }

            public Task EnviarCorreo(string email, string subject, string message)//cambiar por nuevo email
            {
                string text = message;

                AlternateView plainView =
                AlternateView.CreateAlternateViewFromString(text,
                                Encoding.UTF8,
                                MediaTypeNames.Text.Plain);

            string html = "<h2>" + message + "</h2>";

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
                string script = "confirm();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "xcript", script, true);
            }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(ex.Message);
                }
                return Task.CompletedTask;
            }

        #endregion

        #region MetodosForms
        protected void btnConsultar_Click(object sender, EventArgs e)
        {

            string identificacion = txtCedula.Text.Trim().ToString();
            if (txtCedula.Text.ToString().Trim() != "")
            {
                buscarInscritoxIdentificacion(identificacion);
            }
            else
            {
                joinTodosInscritos();
            }

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string mensaje = txtInfo.Text;
            string email = txtUsuEmail.Text.ToString();
            EnviarCorreo(email, "Respuesta a solicitud de Informacion",mensaje);

        }
        protected void btnActualizar_Click(object sender, EventArgs e) {
            int estadoprueba;
            int tipoEstudiante;
            ClInscritoAutonomo inscrito = new ClInscritoAutonomo();
            inscrito.IdInscrito = Convert.ToInt64(txtId.Text);
            inscrito.NombreInscrito = txtNombres.Text.Trim().ToUpper();
            inscrito.ApellidoInscrito = txtApellidos.Text.Trim().ToUpper();
            inscrito.EmailInscrito = txtEmail.Text.Trim();
            inscrito.DirecInscrito = txtDireccion.Text.Trim().ToUpper();
            inscrito.CeluInscrito = txtCelular.Text.Trim();
            inscrito.TelefInscrito = txtTelefono.Text.Trim();
            inscrito.NumDocInscrito = txtIdentificacion.Text.Trim();
            inscrito.InformacionCurso = txtInformacion.Text.Trim().ToUpper();
            if (RabPruebaUbicacion.Checked == true)
                estadoprueba = 1;
            else
                estadoprueba = 0;
            if (RabTipoEstudiante.Checked == true) {
                tipoEstudiante = 2;
            }
            else
            {
                tipoEstudiante = 1;
            }
            inscrito.EstadoPrueba = estadoprueba;
            //inscrito.idPerInscripcion = Convert.ToInt64(cbxPeriodoLectivo.SelectedValue.ToString());
            inscrito.IdTipoEstudiante = tipoEstudiante;
            //inscrito.IdNivel = null;

            //inscrito.IdTipoDocumento = 1;

            ServicioActualizarInscrito(inscrito);

        }

        protected void dgvInscrito_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(dgvInscrito.DataKeys[e.RowIndex].Values[0].ToString());
            ServicioEliminarRegistro(id);
        }

        protected void dgvInscrito_RowEditing(object sender, GridViewEditEventArgs e)
        {


        }
        protected void dgvInscrito_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            if (e.CommandName == "Editar")
            {
                int fila = Convert.ToInt32(e.CommandArgument);
                long idregistro = Convert.ToInt64(dgvInscrito.DataKeys[fila].Value);
                llenarmodal(idregistro);
                btnPopUp_ModalPopupExtender.Show();
                
            }
            if (e.CommandName == "Info")
            {
                int fila = Convert.ToInt32(e.CommandArgument);
                string email = dgvInscrito.Rows[fila].Cells[9].Text;
                string info = dgvInscrito.Rows[fila].Cells[14].Text;
                txtUsuEmail.Text = email.Trim();
                txtsinfo.Text = info;
                btnPopUp_ModalPopupExtender2.Show();
            }

        }
        #endregion
        public override void VerifyRenderingInServerForm(Control control)
        {
          
        }

        protected void cbxPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idPeriodo = Convert.ToInt32(cbxPeriodo.SelectedValue.ToString());
            if (idPeriodo == 0)
            {
                joinTodosInscritos();
            }
            else
            {
                joinTodosInscritosxPeriodo(idPeriodo);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DateTime fecha = DateTime.Now;
            string fechaDocument = Convert.ToString(fecha);
            string nombreArchivo = "Inscritos" + "_" + fechaDocument;
            DataTable dt = new DataTable("Inscritos");
            foreach (TableCell cell in dgvInscrito.HeaderRow.Cells)
            {
                if (cell.Text.Trim() != "&nbsp;" || cell.Text.Trim() == "acciones")
                    dt.Columns.Add(cell.Text);
            }
            foreach (GridViewRow row in dgvInscrito.Rows)
            {

                dt.Rows.Add();
                for (int i = 0; i < row.Cells.Count - 3; i++)
                {
                    string ss = dgvInscrito.HeaderRow.Cells[i].Text;
                    if (ss != "&nbsp;" || ss == "acciones")
                    {
                        dt.Rows[dt.Rows.Count - 1][i] = row.Cells[i].Text;
                        dt.Rows[dt.Rows.Count - 1][i] = row.Cells[i].Text.Replace("&nbsp;", "N.A");
                    }

                }
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + nombreArchivo + ".xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        protected void dgvInscrito_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvInscrito.PageIndex = e.NewPageIndex;
            joinTodosInscritos();
        }
    }
}