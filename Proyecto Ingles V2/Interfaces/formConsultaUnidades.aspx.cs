using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using Logica.ConexionServicios;
using Logica.Modelos;
using Logica;
using Logica.Servicios;
using System.Threading.Tasks;
namespace Proyecto_Ingles_V2.Interfaces
{
    public partial class ConsultaUnidades : System.Web.UI.Page
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
                       cargarNivelesAutonomos();
                }
             
            }
        }
        public async void cargarNivelesAutonomos()
        {
            try
            {
                List<ClEquivalenciaNivel> compInf = new List<ClEquivalenciaNivel>();
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/EquivalenciaNivel");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClEquivalenciaNivel>>(empResponse);

                }

                ddlNivel.DataSource = compInf;
                ddlNivel.DataValueField = "IdEquivalenciaNivel";
                ddlNivel.DataTextField = "DescEquivalenciaNivel";
                ddlNivel.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}