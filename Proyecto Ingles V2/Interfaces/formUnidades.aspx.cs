using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using Logica.ConexionServicios;
using Logica.Modelos;
using Logica;
using Logica.Servicios;
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
              
                }

            }

        }
        #region Invocacion Servicios
        public async void ServicioInsertarUnidad(ClTemaUnidad unidad)
        {

            string uri = "api/TemasNiveles";
            var myContent = JsonConvert.SerializeObject(unidad);
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
            else
            {
                string script = @"<script type='text/javascript'>
                                alert('No se pudo procesar la solicitud');
                                </script>";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
            }
        }
        public async Task<List<ClTemaUnidad>> ServicioGetEstadoNivel()
        {
            List<ClTemaUnidad> compInf = new List<ClTemaUnidad>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/TemasNiveles");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClTemaUnidad>>(empResponse);

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
        public void crearUnidades() {
            string codigo = txtCodUnidad.Text.Trim().ToUpper();
            string prefijo = "UNIT ";
            int inicio = Convert.ToInt32(txtInicio.Text);
            string codGroupUnidad;
            int fin = Convert.ToInt32(txtFin.Text);
            ClTemaUnidad unidad = new ClTemaUnidad();
            for (int i = inicio; i <= fin; i++) {

                codGroupUnidad = prefijo + i;
                unidad.codTemaUnidad = codigo;
                unidad.descTemaUnidad = codGroupUnidad;
                ServicioInsertarUnidad(unidad);
            }
            limpiarCampos();
            string script = "confirm();";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);

        }

        #endregion
        public void limpiarCampos() {
            txtCodUnidad.Text = "";
            txtFin.Text = "";
            txtInicio.Text = "";        
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            crearUnidades();
        }
    }
}