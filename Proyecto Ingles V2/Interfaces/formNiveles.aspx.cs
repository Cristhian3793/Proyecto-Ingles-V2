using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica.ConexionServicios;
using Logica.Modelos;
using Logica.Servicios;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
namespace Proyecto_Ingles_V2.Interfaces
{
    public partial class formNivelesProgramado : System.Web.UI.Page
    {
        static conexionServidor cs = new conexionServidor();
        string url = cs.url.ToString();
        protected  void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
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
                cargarComboTipoNivel();
                cargarComboCurso();
                cargarComboLibros();
                }

            }
        }
        #region invocacion servicios
        public async Task<List<ClTipoNivel>> ServicioGetTipoNivel() {
            List<ClTipoNivel> tipnivel = new List<ClTipoNivel>();
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
                    tipnivel = JsonConvert.DeserializeObject<List<ClTipoNivel>>(empResponse);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return tipnivel;
        }
        public async void ServicioInsertarNivelAutonomo() {
            try
            {
                ClNivelesAutonomos nivel = new ClNivelesAutonomos();
                string uri = "api/NivelAutonomo";
                var myContent = JsonConvert.SerializeObject(nivel);
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
                throw ex;
            }
        }

        public async void ServicioInsertarNivelProgramado()
        {
            try
            {
                ClNivelesProgramado nivel = new ClNivelesProgramado();
                string uri = "api/NivelProgramado";
                var myContent = JsonConvert.SerializeObject(nivel);
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
                throw ex;
            }
        }
        public async Task<List<CLLibros>> ServicioGetLibros() {
            List<CLLibros> lib = new List<CLLibros>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/Libros");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    lib = JsonConvert.DeserializeObject<List<CLLibros>>(empResponse);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return lib;
        }
        public async Task<List<ClEstadoNivel>> ServicioGetEstadoNivel()
        {
            List<ClEstadoNivel> cur = new List<ClEstadoNivel>();
            try
            {              
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/EstadoNivel");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    cur = JsonConvert.DeserializeObject<List<ClEstadoNivel>>(empResponse);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return cur;
        }
        public async Task<List<ClCurso>> ServicioGetCursos()
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

        public async  Task<List<ClNivel>> ServicioGetNiveles() {
            List<ClNivel> nivel = new List<ClNivel>();
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
                    nivel = JsonConvert.DeserializeObject<List<ClNivel>>(empResponse);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return nivel;
        }
        public async void ServicioInsertarNivelProgramado(ClNivelesProgramado nivelpro) {
            try
            {
                string uri = "api/NivelProgramado";
                var myContent = JsonConvert.SerializeObject(nivelpro);
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
        public async void ServicioInsertarNivelAutonomo(ClNivelesAutonomos nivelaut) {
            try
            {
                string uri = "api/NivelAutonomo";
                var myContent = JsonConvert.SerializeObject(nivelaut);
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
        public async void ServicioInsertarNivel() {
            try
            {
                List<ClEstadoNivel> estado = new List<ClEstadoNivel>();
                estado = await ServicioGetEstadoNivel();
                DateTime fecha = DateTime.Now;
                ClNivel nivel = new ClNivel();
                long idNivel;
                ClNivelesProgramado nivelpro = new ClNivelesProgramado();
                ClNivelesAutonomos nivelAuto = new ClNivelesAutonomos();
                int activo = 0;
                var active = estado.Where(a => a.IdEstadoNivel == 1).Select(a => a.IdEstadoNivel).FirstOrDefault();
                var inactive = estado.Where(a => a.IdEstadoNivel == 2).Select(a => a.IdEstadoNivel).FirstOrDefault();
                if (RabActivo.Checked == true)
                {
                    activo = Convert.ToInt32(active);
                }
                else
                {
                    activo = Convert.ToInt32(inactive);
                }
                nivel.idEstadoNivel = activo;
                if (Convert.ToInt64(ddlLibros.SelectedValue.ToString()) != 0)
                {
                    nivel.idLibro = Convert.ToInt64(ddlLibros.SelectedValue.ToString());
                }
                else {
                    nivel.idLibro = null;
                }
                
                if (Convert.ToInt64(cbxTipoNivel.SelectedValue.ToString()) != 0)
                {
                    nivel.idTipoNivel = Convert.ToInt64(cbxTipoNivel.SelectedValue.ToString());
                }else
                {
                    nivel.idTipoNivel = null;
                }
                nivel.idCurso = Convert.ToInt32(cbxCursos.SelectedValue.ToString());              
                nivel.descNivel = ddlNivel.SelectedValue.ToString();
                nivel.codNivel = txtcodNivel.Text.ToUpper();
                nivel.nomNivel = txtNomNivel.Text.ToUpper();
                nivel.costoNIvel =Convert.ToDouble(txtCostoNivel.Text);
                string uri = "api/Niveles";
                var myContent = JsonConvert.SerializeObject(nivel);
                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.PostAsync(uri, stringContent);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;

                    //Invocar servicio extraer ultimo id
                    List<ClNivel> niveles = new List<ClNivel>();
                    niveles = await ServicioGetNiveles();
                    var id=niveles.OrderByDescending(x => x.idNivel).Select(x => x.idNivel).FirstOrDefault();
                    //Guardar Nivel Programado 
                    idNivel = Convert.ToInt64(id);
                    if (Convert.ToInt32(cbxTipoNivel.SelectedValue.ToString()) == 1)
                    {
                        nivelpro.idNivel = idNivel;
                        ServicioInsertarNivelProgramado(nivelpro);
                        string script = "confirm();";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "script", script, true);

                    }//guardar nivel autonomo
                    else if (Convert.ToInt32(cbxTipoNivel.SelectedValue.ToString()) == 2)
                    {
                        nivelAuto.idNivel = idNivel;
                        ServicioInsertarNivelAutonomo(nivelAuto);
                        string script = "confirm();";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "script", script, true);
                    }
                }
                else
                {
                    string script = @"<script type='text/javascript'>
                                alert('No se pudo guardar');
                                </script>";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                }
                limpiarCampos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Metodos
        public async void cargarComboTipoNivel() {
            List<ClTipoNivel> tipnivel = new List<ClTipoNivel>();
            tipnivel = await ServicioGetTipoNivel();
            cbxTipoNivel.DataSource = tipnivel;
            cbxTipoNivel.DataValueField = "idtipoNivel";
            cbxTipoNivel.DataTextField = "descTipoNivel";
            cbxTipoNivel.DataBind();
        }
        public async void cargarComboLibros() {
            List<CLLibros> lib = new List<CLLibros>();
            lib = await ServicioGetLibros();
            ddlLibros.DataSource = lib;
            ddlLibros.DataValueField = "idLibro";
            ddlLibros.DataTextField = "descLibro";
            ddlLibros.DataBind();
        }
        public async void cargarComboCurso() {
            List<ClCurso> curso = new List<ClCurso>();
            curso = await ServicioGetCursos();
            cbxCursos.DataSource = curso;
            cbxCursos.DataValueField = "IdCurso";
            cbxCursos.DataTextField = "DescCurso";
            cbxCursos.DataBind();
        }
        public void limpiarCampos()
        {
            txtcodNivel.Text = "";
            txtNomNivel.Text = "";
            txtCostoNivel.Text = "";

        }

        public async Task<bool> validarExisteNivel(long idNivel, int tipoNivel, string codNivel)
        {
            bool resp = false;
            List<ClNivel> niveles = await ServicioGetNiveles();
            foreach (ClNivel a in niveles) {
                if (a.idNivel == idNivel && a.codNivel.Trim() == codNivel && a.idTipoNivel == tipoNivel)
                {
                    resp = true;
                    break;
                }
                else if (a.idNivel == idNivel && a.codNivel.Trim() == codNivel)
                {
                    resp = true;
                    break;
                }
                else if (a.codNivel.Trim() == codNivel) {
                    resp = true;
                    break;
                }          
            }
            return resp;
        }

        #endregion

        #region Metodos Forms
        protected async  void Button1_Click(object sender, EventArgs e)
        {
            long idNivel =Convert.ToInt64(ddlNivel.SelectedValue);
            string codNivel = txtcodNivel.Text.Trim().ToString();
            int idTipoNivel= Convert.ToInt32(cbxTipoNivel.SelectedValue);
            bool resp = false;
            resp = await validarExisteNivel(idNivel,idTipoNivel,codNivel);
            if (resp == false)
            {
                ServicioInsertarNivel();
            }
            else {
                string script = "existe();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "script", script, true);
                limpiarCampos();
            }
                        
           
                  
        }
        #endregion

    }
}