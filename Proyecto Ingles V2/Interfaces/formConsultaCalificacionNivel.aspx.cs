using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica.ConexionServicios;
using Logica.Modelos;
using Newtonsoft.Json;

namespace Proyecto_Ingles_V2.Interfaces
{
    public partial class formConsultaCalificacionNivel : System.Web.UI.Page
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
                else {
                    cargarGridCalificacionNiveles();
                }

            }
            
        }
        #region Invocacion Servicios
        public async Task<List<ClCalificacionNivel>> ServicioGetCalificacionNivel()
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
                    return compInf;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
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
                    return compInf;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async Task<List<ClTipoNivel>> ServicioTipoNIvel()
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
        public async void actualizarCalificacionNivel(ClCalificacionNivel ca)
        {
            bool resp=await ValidarCalificaciones(ca.idNivel,ca.calificacionNivelDesde,ca.calificacionNivelHasta);
            if (resp == false)
            {
                string uri = "api/CalificacionNivel/" + ca.idCalificacionNivel;
                var myContent = JsonConvert.SerializeObject(ca);
                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.PutAsync(uri, stringContent);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    cargarGridCalificacionNiveles();
                }

            }
            else {
                string script = "re();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);

            }


        }
        #endregion
        #region Metodos
        public async void cargarGridCalificacionNiveles() {
            List<ClCalificacionNivel> calificacion = new List<ClCalificacionNivel>();
            List<ClNivel> niveles = new List<ClNivel>();
            List<ClTipoNivel> tiponivel = new List<ClTipoNivel>();
            tiponivel =await ServicioTipoNIvel();
            calificacion = await ServicioGetCalificacionNivel();
            niveles = await ServicioGetNiveles();
            var query = from a in calificacion
                        join b in niveles on a.idNivel equals b.idNivel
                        join c in tiponivel on b.idTipoNivel equals c.idtipoNivel
                        orderby a.calificacionNivelDesde ascending
                        select new {
                            idCalificacionNivel = a.idCalificacionNivel,
                            idNivel = a.idNivel,
                            nomNivel = b.nomNivel,
                            tipoNivel=c.descTipoNivel,
                            Calificacion = a.calificacionNivelDesde,
                            CalificacionHasta=a.calificacionNivelHasta,
                        };
            dgvCalificacionNivel.DataSource = query;
            dgvCalificacionNivel.DataBind();
        }
        #endregion

        #region Metodos Forms
        #endregion

        protected void dgvCalificacionNivel_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dgvCalificacionNivel.EditIndex = -1;
            cargarGridCalificacionNiveles();
        }

        protected void dgvCalificacionNivel_RowEditing(object sender, GridViewEditEventArgs e)
        {
            dgvCalificacionNivel.EditIndex = e.NewEditIndex;

        }

        protected void dgvCalificacionNivel_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ClCalificacionNivel ca = new ClCalificacionNivel();
            long id = Convert.ToInt64(dgvCalificacionNivel.DataKeys[e.RowIndex]["idCalificacionNivel"].ToString());
            double  calificacionDesde;
            double  calificacionHasta;
            TextBox txtCalificacion = (dgvCalificacionNivel.Rows[e.RowIndex].Cells[4].FindControl("EditCalificacion") as TextBox);
            TextBox txtCalificacionHasta = (dgvCalificacionNivel.Rows[e.RowIndex].Cells[5].FindControl("txtCalificacionHasta") as TextBox);
            long idNivel=Convert.ToInt64(dgvCalificacionNivel.DataKeys[e.RowIndex]["idNivel"].ToString());
            //long idNivel = Convert.ToInt64(dgvCalificacionNivel.Rows[e.RowIndex].Cells[1].Text);//se usa cuando la columna no esta oculta
            calificacionDesde = Convert.ToDouble(txtCalificacion.Text);
            calificacionHasta = Convert.ToDouble(txtCalificacionHasta.Text);
            ca.idCalificacionNivel = id;
            ca.idNivel = idNivel;
            ca.calificacionNivelDesde = calificacionDesde;
            ca.calificacionNivelHasta = calificacionHasta;
 
            actualizarCalificacionNivel(ca);
            dgvCalificacionNivel.EditIndex = -1;           
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
 
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
        public async Task<bool> ValidarCalificaciones(long idNivel, double calificaciondesde, double calificacionHasta)
        {
            List<ClNivel> nivel = await ServicioGetNiveles();
            List<ClCalificacionNivel> calif = await ServicioGetCalificacion();
            bool resp = false;
            var query = from a in nivel
                        join b in calif on a.idNivel equals b.idNivel
                        where b.idNivel == idNivel && b.calificacionNivelDesde == calificaciondesde && b.calificacionNivelHasta == calificacionHasta
                        select new
                        {
                            IdnivelC = b.idNivel,
                            CalDesde = b.calificacionNivelDesde,
                            CalHasta = b.calificacionNivelHasta,
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
            else if (query3.Count() > 0)
            {
                resp = true;
                return resp;
            }
            else
            {
                resp = false;
                return false;

            }

        }
        public async void ServicioEliminarRegistro(long idreg)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.DeleteAsync("api/CalificacionNivel/" + idreg);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                }
                cargarGridCalificacionNiveles();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void dgvCalificacionNivel_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            long id = Convert.ToInt64(dgvCalificacionNivel.DataKeys[e.RowIndex]["idCalificacionNivel"].ToString());
            ServicioEliminarRegistro(id);
        }
    }
}