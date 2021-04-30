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
    public partial class Unidades : System.Web.UI.Page
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
                   llenarComboNiveles();
                }

            }

        }
        #region Invocacion Servicios
        public async void ServicioGetTemasUnidades()
        {
            try
            {

                List<ClUnidad> uni = new List<ClUnidad>();
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/Unidades/" + Convert.ToInt32(ddlNivel.SelectedValue.ToString()));
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    uni = JsonConvert.DeserializeObject<List<ClUnidad>>(empResponse);

                }
                ddlTema.DataSource = uni;
                ddlTema.DataValueField = "IdNomUnidad";
                ddlTema.DataTextField = "NomUnidad";
                ddlTema.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
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

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async void ServicioInsertTemaUnidad()
        {
            ClTemaUnidad pru = new ClTemaUnidad();
            pru.idNomUnidad = Convert.ToInt32(ddlTema.SelectedValue.ToString());
            pru.codTemaUnidad = "";
            pru.descTemaUnidad = txtDescUnidad.Text;
            string uri = "api/TemasNiveles";
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
                string script = @"<script type='text/javascript'>
                                alert('Añadido Correctamente');
                                </script>";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);

            }
            else
            {
                string script = @"<script type='text/javascript'>
                                alert('No se pudo procesar la solicitud');
                                </script>";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
            }
        }
        #endregion
        #region Metodos
        public async void llenarComboNiveles() {
            List<ClNivel> niveles = new List<ClNivel>();
            niveles = await ServicioGetNiveles();
            ddlNivel.DataSource = niveles;
            ddlNivel.DataValueField = "idNivel";
            ddlNivel.DataTextField = "nomNivel";
            ddlNivel.DataBind();

        }
        public void limpiarCampos()
        {
            txtDescUnidad.Text = "";
        }
        #endregion
        #region MetodosForms
        protected void Button1_Click(object sender, EventArgs e)
        {
            ServicioInsertTemaUnidad();
        }
        protected void ddlNivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlTema.Items.Clear();
            ServicioGetTemasUnidades();
        }
        #endregion
    }
}