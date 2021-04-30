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
    public partial class formConsultaNivelEquivalencia : System.Web.UI.Page
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
                    cargarGridNIvelesEquivalentes();
                }
                
            }           
        }
        #region Invocacion Servicios
        public async Task<List<ClNivelesAutonomos>> ServicioGetNivelAutonomo()//cargar niveles autonomos
        {
            List<ClNivelesAutonomos> compInf = new List<ClNivelesAutonomos>();
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
                    compInf = JsonConvert.DeserializeObject<List<ClNivelesAutonomos>>(empResponse);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async Task<List<ClNivelesProgramado>> ServicioGetNivelProgramado() {
            List<ClNivelesProgramado> compInf = new List<ClNivelesProgramado>();
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
                    compInf = JsonConvert.DeserializeObject<List<ClNivelesProgramado>>(empResponse);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async Task<List<ClNivel>> ServicioGetNivel()
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
        public async Task<List<ClEquivalenciaNivel>> ServicioGetNivelEquivalente()
        {
            List<ClEquivalenciaNivel> compInf = new List<ClEquivalenciaNivel>();
            try
            {
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
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async void ServicioActualizarNivelEquivalente(ClEquivalenciaNivel nivequi)
        {

            try
            {
                string uri = "api/EquivalenciaNivel?codEquivNivel=" + nivequi.IdEquivalenciaNivel;
                var myContent = JsonConvert.SerializeObject(nivequi);
                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.PutAsync(uri, stringContent);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    cargarGridNIvelesEquivalentes();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async void ServicioEliminarNivelEquivalente(long idNivelEquivalente)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.DeleteAsync("api/EquivalenciaNivel?codEquivNivel=" + idNivelEquivalente);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                }
                cargarGridNIvelesEquivalentes();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Metodos
        public async void CargarComboNivelesProgramados() {
            List<ClNivelesProgramado> nivPro =new List<ClNivelesProgramado>();
            List<ClNivel> nivel = new List<ClNivel>();
            nivPro =await ServicioGetNivelProgramado();
            nivel = await ServicioGetNivel();
            var query = from a in nivPro
                        join b in nivel on a.idNivel equals b.idNivel
                        select new
                        {
                            IdNivel=a.idNIvelProgramado,
                            DescNivel=b.nomNivel,
                        };
            cbxNivelesProgramados.DataSource = query;
            cbxNivelesProgramados.DataValueField = "IdNivel";
            cbxNivelesProgramados.DataTextField = "DescNivel";
            cbxNivelesProgramados.DataBind();  
        }
        public async void CargarComboNivelesAutonomos()
        {
            List<ClNivelesAutonomos> nivAut = new List<ClNivelesAutonomos>();
            List<ClNivel> nivel = new List<ClNivel>();
            nivAut = await ServicioGetNivelAutonomo();
            nivel = await ServicioGetNivel();
            var query = from a in nivAut
                        join b in nivel on a.idNivel equals b.idNivel
                        select new
                        {
                            IdNivel = a.idNIvelAutonomo,
                            DescNivel = b.nomNivel,
                        };
            cbxNivelesAutonomos.DataSource = query;
            cbxNivelesAutonomos.DataValueField = "IdNivel";
            cbxNivelesAutonomos.DataTextField = "DescNivel";
            cbxNivelesAutonomos.DataBind();
        }
        public  async void CargarModal() {
            List<ClEquivalenciaNivel> nivEquivalente = new List<ClEquivalenciaNivel>();
            nivEquivalente = await ServicioGetNivelEquivalente();
            CargarComboNivelesProgramados();
            CargarComboNivelesAutonomos();
        }
        public async void cargarGridNIvelesEquivalentes() {
            List<ClNivel> nivel = new List<ClNivel>();
            List<ClNivelesProgramado> nivelProgra = new List<ClNivelesProgramado>();
            List<ClNivelesAutonomos> nivelAutonomo = new List<ClNivelesAutonomos>();
            List<ClEquivalenciaNivel> equivalencia = new List<ClEquivalenciaNivel>();
            nivel =await ServicioGetNivel();
            nivelProgra = await ServicioGetNivelProgramado();
            nivelAutonomo = await ServicioGetNivelAutonomo();
            equivalencia = await ServicioGetNivelEquivalente();
            var listNivelAutonomo = from a in nivelAutonomo
                                    join b in nivel on a.idNivel equals b.idNivel
                                    select new
                                    {
                                        IdNivelAutonomo = a.idNIvelAutonomo,
                                        IdNivel=a.idNivel,
                                        NomNivelAutonomo=b.nomNivel,
                                    };
            var listNivelProgramado= from a in nivelProgra
                                    join b in nivel on a.idNivel equals b.idNivel
                                    select new
                                    {
                                        IdNivelProgramado = a.idNIvelProgramado,
                                        IdNivel = a.idNivel,
                                        NomNivelProgramado = b.nomNivel,
                                    };
            var query = from a in listNivelAutonomo 
                        join b in equivalencia on a.IdNivelAutonomo equals b.idNivelAut 
                        join c in listNivelProgramado on b.idNivelPro equals c.IdNivelProgramado 
                        select new {
                                IdEquivalenciaNivel=b.IdEquivalenciaNivel,
                                IdNivelAutonomo=a.IdNivelAutonomo,
                                IdNIvelProgramado=c.IdNivelProgramado,
                                NomNivelProgramado=c.NomNivelProgramado,
                                NomNivelAutonomo=a.NomNivelAutonomo,
                            };
            dgvNivelEquivalencia.DataSource = query;
            dgvNivelEquivalencia.DataBind();
        }

        #endregion

        #region Metodos Forms
        protected  void btnConsultar_Click(object sender, EventArgs e)
        {

        }
        #endregion

        protected void dgvNivelEquivalencia_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                //llenar Modal
                int fila = Convert.ToInt32(e.CommandArgument);              
                long idregistro = Convert.ToInt64(dgvNivelEquivalencia.DataKeys[fila]["IdEquivalenciaNivel"].ToString());
                long idNivProgramado = Convert.ToInt64(dgvNivelEquivalencia.DataKeys[fila]["IdNIvelProgramado"].ToString());
                long idNivAutonomo = Convert.ToInt64(dgvNivelEquivalencia.DataKeys[fila]["IdNivelAutonomo"].ToString());
                CargarComboNivelesProgramados();
                cbxNivelesProgramados.SelectedValue = idNivProgramado.ToString();
                CargarComboNivelesAutonomos();
                cbxNivelesAutonomos.SelectedValue = idNivAutonomo.ToString();
                idNivEqui.Value = idregistro.ToString();
                btnPopUp_ModalPopupExtender.Show();
            }
        }
        protected void btnActualizar_Click(object sender, EventArgs e)
        {

            ClEquivalenciaNivel equivalencia = new ClEquivalenciaNivel();
            equivalencia.IdEquivalenciaNivel = Convert.ToInt32(idNivEqui.Value);
            equivalencia.idNivelPro = Convert.ToInt64(cbxNivelesProgramados.SelectedValue);
            equivalencia.idNivelAut= Convert.ToInt64(cbxNivelesAutonomos.SelectedValue);
            ServicioActualizarNivelEquivalente(equivalencia);

        }

        protected void dgvNivelEquivalencia_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(dgvNivelEquivalencia.DataKeys[e.RowIndex]["IdEquivalenciaNivel"].ToString());
            ServicioEliminarNivelEquivalente(id);
        }
    }
}