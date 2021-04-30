using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica.Modelos;
using Logica;
using Logica.Servicios;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using Logica.ConexionServicios;
namespace Proyecto_Ingles_V2.Interfaces
{
    public partial class formEquivalenciaNivel : System.Web.UI.Page
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
                cargarComboNivelProgramado();
                cargarComboNivelAutonomo();
                }
            }
        }
        #region Invocacion Servicios
        public async Task<List<ClNivelesProgramado>> ServicioGetNivelProgramado()
        {
            List<ClNivelesProgramado> nivelProgramado = new List<ClNivelesProgramado>();
            try
            {               
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/NivelProgramado");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    nivelProgramado = JsonConvert.DeserializeObject<List<ClNivelesProgramado>>(empResponse);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return nivelProgramado;
        }

        public async Task<List<ClNivelesAutonomos>> ServicioGetNivelAutonomo()
        {
            List<ClNivelesAutonomos> nivelAutonomo = new List<ClNivelesAutonomos>();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/NivelAutonomo");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    nivelAutonomo = JsonConvert.DeserializeObject<List<ClNivelesAutonomos>>(empResponse);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return nivelAutonomo;
        }
        public async Task<List<ClNivel>> ServicioGetNiveles()
        {
            List<ClNivel> niveles = new List<ClNivel>();
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
                    niveles = JsonConvert.DeserializeObject<List<ClNivel>>(empResponse);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return niveles;
        }
        public async void ServiocInsertNivelEquivalente(ClEquivalenciaNivel nivequi) {
            try
            {
                string uri = "api/EquivalenciaNivel";
                var myContent = JsonConvert.SerializeObject(nivequi);
                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.PostAsync(uri, stringContent);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    string script = "confirm();";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
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
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Metodos
        public async void cargarComboNivelProgramado()
        {
            List<ClNivel> nivel = new List<ClNivel>();
            List<ClNivelesProgramado> nivelProgramado = new List<ClNivelesProgramado>();
            nivel = await ServicioGetNiveles();
            nivelProgramado = await ServicioGetNivelProgramado();
            var query = from a in nivelProgramado join b in nivel on a.idNivel equals b.idNivel select new {
                IdNivelProgramdo = a.idNIvelProgramado,
                descNivel=b.nomNivel,
            };
            cbxNivelProgramado.DataSource = query;
            cbxNivelProgramado.DataValueField = "IdNivelProgramdo";
            cbxNivelProgramado.DataTextField = "descNivel";
            cbxNivelProgramado.DataBind();
        }
        public async void cargarComboNivelAutonomo()
        {
            List<ClNivel> nivel = new List<ClNivel>();
            List<ClNivelesAutonomos> nivelAutonomo = new List<ClNivelesAutonomos>();
            nivel = await ServicioGetNiveles();
            nivelAutonomo = await ServicioGetNivelAutonomo();
            var query = from a in nivelAutonomo
                        join b in nivel on a.idNivel equals b.idNivel
                        select new
                        {
                            IdNivelAutonomo = a.idNIvelAutonomo,
                            descNivel = b.nomNivel,
                        };
            cbxNivelAutonomo.DataSource = query;
            cbxNivelAutonomo.DataValueField = "IdNivelAutonomo";
            cbxNivelAutonomo.DataTextField = "descNivel";
            cbxNivelAutonomo.DataBind();
        }

        #endregion

        #region Metodos Forms
        protected  void btnGuardar_Click(object sender, EventArgs e)
        {
            ClEquivalenciaNivel nivequi = new ClEquivalenciaNivel();
            long idAutonomo = Convert.ToInt64(cbxNivelAutonomo.SelectedValue.ToString());
            long idProgramado = Convert.ToInt64(cbxNivelProgramado.SelectedValue.ToString());
            nivequi.idNivelAut = idAutonomo;
            nivequi.idNivelPro = idProgramado;
            ServiocInsertNivelEquivalente(nivequi);

        }
        #endregion

    }
}