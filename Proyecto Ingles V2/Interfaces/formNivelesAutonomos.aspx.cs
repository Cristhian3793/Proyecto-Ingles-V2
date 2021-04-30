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
    public partial class formNivelesAutonomos : System.Web.UI.Page
    {
        static conexionServidor cs = new conexionServidor();
        string url = cs.url.ToString();
        protected void Page_Load(object sender, EventArgs e)
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
                    cargarComboCurso();
                }

            }

        }
        #region invocacion servicios
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

        public async void ServicioInsertarNivelProgramado()
        {
            try
            {
                List<ClEstadoNivel> estado = new List<ClEstadoNivel>();
                estado = await ServicioGetEstadoNivel();
                DateTime fecha = DateTime.Now;
                ClNivel nivel = new ClNivel();
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
                nivel.idTipoNivel = null;
                nivel.idCurso = Convert.ToInt32(cbxCursos.SelectedValue.ToString());
                nivel.descNivel = ddlNivel.SelectedValue.ToString();
                nivel.codNivel = txtcodNivel.Text.ToUpper();
                nivel.nomNivel = txtNomNivel.Text.ToUpper();
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
                    string script = @"<script type='text/javascript'>
                                alert('Añadido Correctamente');
                                </script>";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                }
                else
                {
                    string script = @"<script type='text/javascript'>
                                alert('No se pudo guardar');
                                </script>";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Metodos
        public async void cargarComboCurso()
        {
            List<ClCurso> curso = new List<ClCurso>();
            curso = await ServicioGetCursos();
            cbxCursos.DataSource = curso;
            cbxCursos.DataValueField = "IdCurso";
            cbxCursos.DataTextField = "DescCurso";
            cbxCursos.DataBind();

        }
        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
            ServicioInsertarNivelProgramado();
        }
    }
}