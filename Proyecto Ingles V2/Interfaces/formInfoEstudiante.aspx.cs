using Logica.ConexionServicios;
using Logica.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto_Ingles_V2.Interfaces
{
    public partial class formEstudiante : System.Web.UI.Page
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
                    if (!IsPostBack)
                    {
                        string usuario=(string)Session["usuario"];
                        string periodo = (string)Session["periodo"];
                        CargarInformacion(usuario);
                        cargarInformacionAlumno();
                    }

                }
        
        }
        #region Servicios
        public async Task<List<ClInscritoAutonomo>> ServicioExtraerInscritos()//cargar todos inscritos
        {
            List<ClInscritoAutonomo> compInf = new List<ClInscritoAutonomo>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/InscritoAutonomo");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClInscritoAutonomo>>(empResponse);
                    return compInf;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async Task<List<ClPeriodoInscripcion>> ServicioExtraerPeriodos()//cargar todos inscritos
        {
            List<ClPeriodoInscripcion> compInf = new List<ClPeriodoInscripcion>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/PeriodoInscripcion");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClPeriodoInscripcion>>(empResponse);

                }

                return compInf;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async Task<List<ClTipoEstudiante>> ServicioGetTipoEstudiante()//cargar todos inscritos
        {
            List<ClTipoEstudiante> compInf = new List<ClTipoEstudiante>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/TipoEstudiante");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClTipoEstudiante>>(empResponse);

                }

                return compInf;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async Task<List<ClPrueba>> ServicioExtraerPrueba()
        {
            List<ClPrueba> compInf = new List<ClPrueba>();
            try
            {
                //string url = "http://servicioinglesuisek/";               
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/Prueba");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClPrueba>>(empResponse);
                    return compInf;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;


        }

        public async Task<List<ClNivel>> ServicioGetNiveles()//cargar todos niveles 
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

        public async Task<List<ClCurso>> ServicioCargarCursos()//cargar todos inscritos
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
        public async Task<List<ClTipoNivel>> ServicioCargarTipoNivel()
        {
            List<ClTipoNivel> cur = new List<ClTipoNivel>();
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
                    cur = JsonConvert.DeserializeObject<List<ClTipoNivel>>(empResponse);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return cur;
        }
        public async Task<List<ClNivelesInscrito>> ServicioGetNivelesInscritos()
        {
            List<ClNivelesInscrito> compInf = new List<ClNivelesInscrito>();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/NivelesInscrito");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClNivelesInscrito>>(empResponse);
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


        public async void cargarInformacionAlumno()
        {


            List<ClInscritoAutonomo> ins = await ServicioExtraerInscritos();
            var queryInscritos = from a in ins

                                 where a.NumDocInscrito.Trim().ToString() == (string)Session["usuario"]
                                 select new
                                 {
                                     IDINSCRITO = a.IdInscrito,
                                     NUMDOCINSCRITO = a.NumDocInscrito,
                                     NOMBINSCRITO = a.NombreInscrito,
                                     APELLIINSCRITO = a.ApellidoInscrito,
                                     Email = a.EmailInscrito,
                                     Direccion = a.DirecInscrito,
                                     Telefono = a.TelefInscrito,
                                     Celular = a.CeluInscrito,
                                     TIPODOCUMENTO = a.IdTipoDocumento,//1 cedula 2 pasaporte


                                 };

            string nombre = queryInscritos.Select(x => x.NOMBINSCRITO).FirstOrDefault().ToString().Trim();
            string apellido = queryInscritos.Select(x => x.APELLIINSCRITO).FirstOrDefault().ToString().Trim();
            string nombreCompleto = nombre + " " + apellido;
            txtNombres.Text = nombre;
            txtApellidos.Text = apellido;
            txtEmail.Text = queryInscritos.Select(x => x.Email).FirstOrDefault().ToString().Trim();
            txtCed.Text= queryInscritos.Select(x => x.NUMDOCINSCRITO).FirstOrDefault().ToString().Trim();

        }
        public async void CargarInformacion(string usuario) {
            //Inscritos
            List<ClInscritoAutonomo> ins = await ServicioExtraerInscritos();
            List<ClPeriodoInscripcion> per = await ServicioExtraerPeriodos();
            List<ClTipoEstudiante> tipo = await ServicioGetTipoEstudiante();
            List<ClPrueba> prueba = await ServicioExtraerPrueba();
            //NIveles
            List<ClNivel> nivel = await ServicioGetNiveles();
            List<ClTipoNivel> tiponivel =await ServicioCargarTipoNivel();
            List<ClCurso> curso = await ServicioCargarCursos();
            List<ClNivelesInscrito> nivIns =await ServicioGetNivelesInscritos();

            var queryInscritos = from a in ins
                                 join b in per on a.idPerInscripcion equals b.IdPeriodoInscripcion
                                 join c in tipo on a.IdTipoEstudiante equals c.IdTipoEstudiante
                                 join d in nivIns on a.IdInscrito equals d.IDINSCRITO
                                 join e in prueba on d.IDINSCRITO equals e.IdInscrito
                                 where a.NumDocInscrito.Trim().ToString() == usuario.Trim().ToString() && d.IDESTADONIVEL == 0
                        select new {
                            IDINSCRITO=a.IdInscrito,
                            IDNIVEL=d.IDNIVEL,
                            NUMDOCINSCRITO=a.NumDocInscrito,
                            NOMBINSCRITO=a.NombreInscrito,
                            APELLIINSCRITO=a.ApellidoInscrito,
                            PERIODO=b.Periodo,
                            PUNTAJEPRUEBA=e.PunjatePrueba,
                            Email=a.EmailInscrito,

                            IDNIVELESTUDIANTE=d.IDNIVELESTUDIANTE,
                            IDESTADONIVEL=d.IDESTADONIVEL,
                        };


            var queryNiveles = from a in nivel
                               join b in tiponivel on a.idTipoNivel equals b.idtipoNivel
                               join c in curso on a.idCurso equals c.IdCurso
                               select new
                               {

                                   IDNIVEL=a.idNivel,
                                   DESCCURSO=c.DescCurso,
                                   DESCTIPONIVEL=b.descTipoNivel,
                                   NOMNIVEL=a.nomNivel,
                                   DESCNIVEL=a.nomNivel,
                                   COSTONIVEL=a.costoNIvel,
                                   CODCURSO=a.codNivel,

                               };

            var qeryfin = from a in queryInscritos join b in queryNiveles on a.IDNIVEL equals b.IDNIVEL                         
                        select new {
                            IDINSCRITO = a.IDINSCRITO,
                            IDNIVEL = a.IDNIVEL,
                            NUMDOCINSCRITO = a.NUMDOCINSCRITO,
                            NOMBINSCRITO = a.NOMBINSCRITO,
                            APELLIINSCRITO = a.APELLIINSCRITO,
                            PERIODO = a.PERIODO,
                            PUNTAJEPRUEBA = a.PUNTAJEPRUEBA,
                            Email=a.Email,
                            DESCCURSO = b.DESCCURSO,
                            DESCTIPONIVEL = b.DESCTIPONIVEL,
                            NOMNIVEL = b.NOMNIVEL,
                            DESCNIVEL = b.DESCNIVEL,
                            COSTONIVEL = b.COSTONIVEL,
                            CODCURSO = b.CODCURSO,

                            IDNIVELESTUDIANTE = a.IDNIVELESTUDIANTE,
                            IDESTADONIVEL = a.IDESTADONIVEL,

                        };
            //txtNombres.Text = qeryfin.Select(x => x.NOMBINSCRITO).FirstOrDefault();
            //txtApellidos.Text= qeryfin.Select(x => x.APELLIINSCRITO).FirstOrDefault();
            //txtCed.Text= qeryfin.Select(x => x.NUMDOCINSCRITO).FirstOrDefault();
            //txtEmail.Text = qeryfin.Select(x => x.Email).FirstOrDefault();
            //Session["periodo"] = qeryfin.Select(x => x.PERIODO).FirstOrDefault().ToString();
            dgvEstudiante.DataSource = qeryfin;
            dgvEstudiante.DataBind();

        }
        #endregion

    }
}