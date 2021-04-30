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
    public partial class Temas : System.Web.UI.Page
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
                    CargarComboTipoNivel();
                    CargarComboNivel();
                }

            }
        }
        #region Servicios
        public async Task<List<ClNivel>>  ServicioGetNiveles()
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
                    return compInf;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async Task<List<ClTipoNivel>> ServicioGetTipoNivel()
        {
            List<ClTipoNivel> compInf = new List<ClTipoNivel>();
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
                    compInf = JsonConvert.DeserializeObject<List<ClTipoNivel>>(empResponse);
                    return compInf;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async void ServicioInsertarTemaUnidad(ClUnidad tema)
        {
            string uri = "api/Unidades";
            var myContent = JsonConvert.SerializeObject(tema);
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
                ScriptManager.RegisterStartupScript(this, typeof(Page), "script", script, true);

            }
            else
            {
                string script = "rechazado();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "script", script, true);
            }

        }
        #endregion

        #region Metodos
        public async void CargarComboNivel() {
            List<ClNivel> nivel = new List<ClNivel>();
            nivel = await ServicioGetNiveles();
            cbxNivel.DataSource = nivel.ToList();
            cbxNivel.DataValueField = "idNivel";
            cbxNivel.DataTextField = "nomNivel";
            cbxNivel.DataBind();
        }
        public async void CargarComboNivelxTipo(int idTipo)
        {
            List<ClNivel> nivel = new List<ClNivel>();
            nivel = await ServicioGetNiveles();
            var query = from a in nivel where a.idTipoNivel == idTipo select new { idNivel=a.idNivel, nomNivel=a.nomNivel };
            cbxNivel.DataSource = query;
            cbxNivel.DataValueField = "idNivel";
            cbxNivel.DataTextField = "nomNivel";
            cbxNivel.DataBind();
        }
        public async void CargarComboTipoNivel() {
            List<ClTipoNivel> tiponivel = new List<ClTipoNivel>();
            tiponivel = await ServicioGetTipoNivel();
            cbxTipoNivel.DataSource = tiponivel.ToList();
            cbxTipoNivel.DataValueField = "idtipoNivel";
            cbxTipoNivel.DataTextField = "descTipoNivel";
            cbxTipoNivel.DataBind();
        }

        #endregion

        #region MetodosForms
        #endregion



        protected void Button1_Click(object sender, EventArgs e)
        {
            insertarUnidad();
            limpiarCampos();
        }

        public  void insertarUnidad()
        {

            ClUnidad pru = new ClUnidad();
            pru.idNivel = Convert.ToInt64(cbxNivel.SelectedValue.ToString());
            pru.codNomUnidad = "";
            pru.NomUnidad = txtUnidad.Text.Trim().ToUpper().ToString();
            pru.desNomUnidad = "";
            ServicioInsertarTemaUnidad(pru);
        }
        public void limpiarCampos() {
            txtUnidad.Text = "";
        }

        protected void cbxTipoNivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbxTipoNivel.SelectedValue) == 0) {
                CargarComboNivel();
            }
            else
                CargarComboNivelxTipo(Convert.ToInt32(cbxTipoNivel.SelectedValue));
           
        }
    }
}