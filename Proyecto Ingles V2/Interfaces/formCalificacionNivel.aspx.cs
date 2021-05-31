using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica.Modelos;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using Logica.ConexionServicios;

namespace Proyecto_Ingles_V2.Interfaces
{
    public partial class formCalificacionNivel : System.Web.UI.Page
    {
        static conexionServidor cs = new conexionServidor();
        string url = cs.url.ToString();
        protected void Page_Load(object sender, EventArgs e)
        {


                if (Session["usuario"] == null || (string)Session["usuario"] == "")
                {
                    Response.Redirect("../Login/formLogin.aspx");
                }
                if ((int)Session["TipoUser"] == 1)
                {
                    Response.Redirect("../Interfaces/Default.aspx");
                }
                else {
                    if (!IsPostBack)
                    {
                        CargarComboTipoNiveles();
                        CargarComboNiveles();

                    }

                }
        }
        #region Invocacion Servicios
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
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async void ServicioInsertarCalificacionNivel(ClCalificacionNivel ca) {
            DateTime fecha = DateTime.Now;
            bool resp= await ValidarCalificaciones(ca.idNivel, ca.calificacionNivelDesde, ca.calificacionNivelHasta);
            if (resp == false)
            {
                try
                {
                    string uri = "api/CalificacionNivel";
                    var myContent = JsonConvert.SerializeObject(ca);
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
                        limpiarCampos();
                    }
                    else
                    {
                        string script = "rechazado();";
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
                string script = "re();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
            }

        }
        #endregion

        #region Metodos
        public void limpiarCampos() {
            txtCalificacionDesde.Text = "";
            txtCalificacionHasta.Text = "";
        }
        public async void CargarComboNiveles()
        {
            List<ClNivel> niveles = await ServicioGetNiveles();

            var query = from a in niveles
                        select new
                        {
                            idNivel = a.idNivel,
                            nomNivel = a.nomNivel,
                        };

            cbxNivel.DataSource = query;
            cbxNivel.DataValueField = "idNivel";
            cbxNivel.DataTextField = "nomNivel";
            cbxNivel.DataBind();
        }
        public async void CargarComboNivelesxTipo(int idTipoNivel) {
            List<ClNivel> niveles= await ServicioGetNiveles();

            var query = from a in niveles where a.idTipoNivel == idTipoNivel 
                        select new {
                            idNivel = a.idNivel,
                            nomNivel=a.nomNivel,
                        };
                      
            cbxNivel.DataSource = query;
            cbxNivel.DataValueField = "idNivel";
            cbxNivel.DataTextField = "nomNivel";
            cbxNivel.DataBind();
        }
        public async void CargarComboTipoNiveles() {
            List<ClTipoNivel> tiponiveles = await ServicioGetTipoNivel();
            cbxTipoNivel.DataSource = tiponiveles;
            cbxTipoNivel.DataValueField = "idtipoNivel";
            cbxTipoNivel.DataTextField = "descTipoNivel";
            cbxTipoNivel.DataBind();
        }
        #endregion
        #region MetodosForms
        protected void cbxTipoNivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idTipoNivel= Convert.ToInt32(cbxTipoNivel.SelectedValue.ToString());
            if (idTipoNivel != 0)
            {
                CargarComboNivelesxTipo(idTipoNivel);
            }
            else {
                CargarComboNiveles();
            }          
        }
        public async Task<List<ClCalificacionNivel>> ServicioGetCalificacion()
        {
            List<ClCalificacionNivel> compInf = new List<ClCalificacionNivel>();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/CalificacionNivel");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClCalificacionNivel>>(empResponse);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        #endregion
        public async Task<bool>  ValidarCalificaciones(long idNivel,double calificaciondesde,double calificacionHasta) {
            List<ClNivel> nivel =await  ServicioGetNiveles();
            List<ClCalificacionNivel> calif = await ServicioGetCalificacion();
            bool resp = false;
            var query = from a in nivel
                        join b in calif on a.idNivel equals b.idNivel
                        where b.idNivel == idNivel && b.calificacionNivelDesde == calificaciondesde && b.calificacionNivelHasta == calificacionHasta
                        select new
                        {
                            IdnivelC=b.idNivel,
                            CalDesde=b.calificacionNivelDesde,
                            CalHasta=b.calificacionNivelHasta,
                        };

            //segunda validacion mismas calificaciones diferente nivel
            var query2 = from c in nivel
                        join d in calif on c.idNivel equals d.idNivel
                        where d.calificacionNivelDesde == calificaciondesde && d.calificacionNivelHasta == calificacionHasta
                        select new
                        {
                            IdnivelC = d.idNivel,
                            CalDesde = d.calificacionNivelDesde,
                            CalHasta = d.calificacionNivelHasta,
                        };
            //1era validacion
            var query3 = from a in nivel
                        join b in calif on a.idNivel equals b.idNivel
                        where b.idNivel == idNivel 
                        select new
                        {
                            IdnivelC = b.idNivel,
                            CalDesde = b.calificacionNivelDesde,
                            CalHasta = b.calificacionNivelHasta,
                        };
            if (query.Count() > 0)//verifica si se registra un mismo nivel con los mismos valores no debe dejar guardar otro
            {
                resp = true;
                return resp;
            }
            else if (query2.Count() > 0)
            {
                resp = true;
                return resp;

            }
            else if (query3.Count()>0) {
                resp = true;
                return resp;
            }
            else
            {
                resp = false;
                return false;

            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ClCalificacionNivel ca = new ClCalificacionNivel();
            
            long idNivel = Convert.ToInt64(cbxNivel.SelectedValue.ToString());
            double calificacionDesde = Convert.ToDouble(txtCalificacionDesde.Text);
            double calificacionHasta= Convert.ToDouble(txtCalificacionHasta.Text);
            ca.idNivel = idNivel;
            ca.calificacionNivelDesde = calificacionDesde;
            ca.calificacionNivelHasta = calificacionHasta;

            ServicioInsertarCalificacionNivel(ca);
        }
    }
}