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
using System.Drawing;
using System.Data;

namespace Proyecto_Ingles_V2.Interfaces
{
    public partial class formVerNotas : System.Web.UI.Page
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
                else
                {
                if (!IsPostBack)
                {
                    cargarGridPruebas();
                }

            }
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
        public async Task<List<ClEstadoEstudiante>> ServicioGetEstadoEstudiante()
        {
            List<ClEstadoEstudiante> compInf = new List<ClEstadoEstudiante>();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/EstadoEstudiante");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClEstadoEstudiante>>(empResponse);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async void actualizarNotas(ClNota no,long idnota,long idIns,long idNiv)
        {
            try
            {
                string uri = "api/Notas?idNota=" + idnota;
                var myContent = JsonConvert.SerializeObject(no);
                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.PutAsync(uri, stringContent);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    llenarModal(idIns, idNiv);
                    cargarGridPruebas();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task<List<ClInscritoAutonomo>> ServicioExtraerInscrito()//cargar todos inscritos
        {
            List<ClInscritoAutonomo> compInf = new List<ClInscritoAutonomo>();
            try
            {
                //string url = "http://servicioinglesuisek/";               
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
        public async Task<List<ClTipoEstudiante>> ServicioExtraerTipoEstudiante()
        {
            List<ClTipoEstudiante> compInf = new List<ClTipoEstudiante>();
            try
            {
                //string url = "http://servicioinglesuisek/";

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/TipoEstudiante");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClTipoEstudiante>>(empResponse);
                    return compInf;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }

        public async Task<List<ClPeriodoInscripcion>> ServicioExtraerPeriodo()
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
                    return compInf;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;

        }
        public async Task<List<ClCalificacionNivel>> ServicioExtraerCalificacion()
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
        public async void ActualizarNivelInscrito(long idInscrito, long IdNivel, int estado)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ClInscritoAutonomo ins = new ClInscritoAutonomo();
                ins.IdInscrito = idInscrito;
                ins.IdNivel = IdNivel;
                ins.IdEstadoEstudiante = estado;
                string uri = "api/InscritoAutonomo?idInscrito=" + ins.IdInscrito;
                var myContent = JsonConvert.SerializeObject(ins);
                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                var method = new HttpMethod("PATCH");
                var request = new HttpRequestMessage(method, uri)
                {
                    Content = stringContent
                };
                response = await client.SendAsync(request);

            }
            catch (TaskCanceledException e)
            {

            }

        }

        public async Task<List<ClNivel>> ServicioExtraerNIvel()
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

        #region Metodos
        public async void cargarGridPruebas()
        {
            List<ClInscritoAutonomo> inscrito = new List<ClInscritoAutonomo>();
            List<ClTipoEstudiante> tipoEstudiante = new List<ClTipoEstudiante>();
            List<ClPeriodoInscripcion> periodo = new List<ClPeriodoInscripcion>();
            List<ClPrueba> prueba = new List<ClPrueba>();
            List<ClNivel> nivel = new List<ClNivel>();
            List<ClNivelesInscrito> nivelesIns = new List<ClNivelesInscrito>();
            List<ClEstadoEstudiante> estadoEstu = new List<ClEstadoEstudiante>();
            nivelesIns = await ServicioGetNivelesInscritos();
            tipoEstudiante = await ServicioExtraerTipoEstudiante();
            inscrito = await ServicioExtraerInscrito();
            periodo = await ServicioExtraerPeriodo();
            prueba = await ServicioExtraerPrueba();
            nivel = await ServicioExtraerNIvel();
            estadoEstu = await ServicioGetEstadoEstudiante();
            var query = from a in inscrito
                        join f in nivelesIns on a.IdInscrito equals f.IDINSCRITO
                        //join d in prueba on f.IDNIVELESTUDIANTE equals d.IDNIVELESTUDIANTE //into s
                        //from d in s.DefaultIfEmpty()
                        join e in nivel on f.IDNIVEL equals e.idNivel
                        join b in tipoEstudiante on a.IdTipoEstudiante equals b.IdTipoEstudiante
                        join c in periodo on a.idPerInscripcion equals c.IdPeriodoInscripcion
                        join d in estadoEstu on f.IDESTADONIVEL equals d.CodEstadoEstu
                        orderby a.NombreInscrito ascending
                        where f.PRUEBA == 0
                        select new
                        {
                            IdInscrito = a.IdInscrito,
                            IdNivel = a.IdNivel,
                            IdTipoDocumento = a.IdTipoDocumento,
                            TipoEstudiante = b.DescTipoEstudiante,
                            NombreInscrito = a.NombreInscrito,
                            ApellidoInscrito = a.ApellidoInscrito,
                            NumDocInscrito = a.NumDocInscrito,
                            CeluInscrito = a.CeluInscrito,
                            TelefInscrito = a.TelefInscrito,
                            DirecInscrito = a.DirecInscrito,
                            EmailInscrito = a.EmailInscrito,
                            FechaRegistro = a.FechaRegistro,
                            EstadoPrueba = a.EstadoPrueba,
                            PeriodoLectivo = c.Periodo,
                            //IdPrueba = d.IdPrueba,//s.Select(x => x.IdPrueba).FirstOrDefault(),
                            NomNivel = e.nomNivel,
                            Estado = d.DescEstEstudiante,
                            //Calificacion = d.PunjatePrueba//s.Select(x => x.PunjatePrueba).FirstOrDefault(),
                        };
            dgvInscrito.DataSource = query;
            dgvInscrito.DataBind();
        }
        public async void cargarGridPruebasXInscrito(string cedula)
        {
            List<ClInscritoAutonomo> inscrito = new List<ClInscritoAutonomo>();
            List<ClTipoEstudiante> tipoEstudiante = new List<ClTipoEstudiante>();
            List<ClPeriodoInscripcion> periodo = new List<ClPeriodoInscripcion>();
            List<ClPrueba> prueba = new List<ClPrueba>();
            List<ClNivel> nivel = new List<ClNivel>();
            List<ClNivelesInscrito> nivelesIns = new List<ClNivelesInscrito>();
            List<ClEstadoEstudiante> estadoEstu = new List<ClEstadoEstudiante>();
            nivelesIns = await ServicioGetNivelesInscritos();
            tipoEstudiante = await ServicioExtraerTipoEstudiante();
            inscrito = await ServicioExtraerInscrito();
            periodo = await ServicioExtraerPeriodo();
            prueba = await ServicioExtraerPrueba();
            nivel = await ServicioExtraerNIvel();
            estadoEstu = await ServicioGetEstadoEstudiante();
            var query = from a in inscrito
                        join f in nivelesIns on a.IdInscrito equals f.IDINSCRITO
                        join e in nivel on f.IDNIVEL equals e.idNivel
                        join b in tipoEstudiante on a.IdTipoEstudiante equals b.IdTipoEstudiante
                        join c in periodo on a.idPerInscripcion equals c.IdPeriodoInscripcion
                        join d in estadoEstu on f.IDESTADONIVEL equals d.CodEstadoEstu
                        orderby a.NombreInscrito ascending
                        orderby a.NombreInscrito ascending
                        where a.NumDocInscrito.Trim() == cedula.Trim() && f.PRUEBA == 0
                        select new
                        {
                            IdInscrito = a.IdInscrito,
                            IdNivel = a.IdNivel,
                            IdTipoDocumento = a.IdTipoDocumento,
                            TipoEstudiante = b.DescTipoEstudiante,
                            NombreInscrito = a.NombreInscrito,
                            ApellidoInscrito = a.ApellidoInscrito,
                            NumDocInscrito = a.NumDocInscrito,
                            CeluInscrito = a.CeluInscrito,
                            TelefInscrito = a.TelefInscrito,
                            DirecInscrito = a.DirecInscrito,
                            EmailInscrito = a.EmailInscrito,
                            FechaRegistro = a.FechaRegistro,
                            EstadoPrueba = a.EstadoPrueba,
                            PeriodoLectivo = c.Periodo,
                            NomNivel = e.nomNivel,
                            Estado = d.DescEstEstudiante,
                        };
            dgvInscrito.DataSource = query;
            dgvInscrito.DataBind();

        }
        public async Task<long> extraerIdNivel(double? calificacion)
        {
            long nivel = 0;
            List<ClCalificacionNivel> calif = new List<ClCalificacionNivel>();
            double califDesde;
            double califHasta;

            calif = await ServicioExtraerCalificacion();
            foreach (ClCalificacionNivel a in calif)
            {
                califDesde = Convert.ToDouble(a.calificacionNivelDesde);
                califHasta = Convert.ToDouble(a.calificacionNivelHasta);

                if (calificacion >= califDesde && calificacion <= califHasta)
                {
                    nivel = a.idNivel;
                }
            }
            return nivel;
        }
        public async void UpdateNivelInscrito(ClNivelesInscrito pru, long idNIvelInscrito)
        {
            string uri = "api/NivelesInscrito/" + idNIvelInscrito;
            var myContent = JsonConvert.SerializeObject(pru);
            var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
            HttpResponseMessage res = await client.PutAsync(uri, stringContent);
            if (res.IsSuccessStatusCode)
            {
                var empResponse = res.Content.ReadAsStringAsync().Result;
            }
        }
        public async void ServicioInsertarSoloNivel(ClNivelesInscrito pru)
        {
            try
            {
                string uri = "api/NivelesInscrito";
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

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async void actualizarNotaPruebaUbicacion(ClPrueba pru)
        {
            try
            {
                string uri = "api/Prueba?codigo=" + pru.IdPrueba;
                var myContent = JsonConvert.SerializeObject(pru);
                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.PutAsync(uri, stringContent);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    //obtener idnivel 
                    long IdNIvel = await extraerIdNivel(pru.PunjatePrueba);
                    //recupera id de nuevo nivel
                    List<ClNivelesInscrito> nivelesInscrito = new List<ClNivelesInscrito>();
                    nivelesInscrito = await ServicioGetNivelesInscritos();
                    long existePrueba = nivelesInscrito.Where(x => x.IDPRUEBAUBICACION == pru.IdPrueba).Count();
                    if (existePrueba > 0)
                    {
                        long IdnivelEstudiante = nivelesInscrito.Where(x => x.IDPRUEBAUBICACION == pru.IdPrueba).Select(x => x.IDNIVELESTUDIANTE).FirstOrDefault();
                        ClNivelesInscrito nivIns = new ClNivelesInscrito();
                        nivIns.IDNIVEL = IdNIvel;
                        nivIns.IDESTADONIVEL = 0;
                        nivIns.IDPRUEBAUBICACION = pru.IdPrueba;
                        UpdateNivelInscrito(nivIns, IdnivelEstudiante);
                    }
                    else
                    {
                        ClNivelesInscrito niv = new ClNivelesInscrito();
                        niv.IDNIVEL = IdNIvel;
                        niv.IDESTADONIVEL = 0;
                        niv.IDINSCRITO = pru.IdInscrito;
                        niv.IDPRUEBAUBICACION = pru.IdPrueba;
                        niv.FECHAREGISTRO = Convert.ToString(DateTime.Now);
                        ServicioInsertarSoloNivel(niv);

                    }
                    cargarGridPruebas();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<List<ClUnidad>> ServicioGetTemas(long idNIvel)
        {
            List<ClUnidad> compInf = new List<ClUnidad>();
            try
            {

              
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/Unidades/" + idNIvel);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClUnidad>>(empResponse);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return compInf;

        }
        public async void ServicioInsertarNotas(ClNota nota)
        {

            try
            {
                string uri = "api/Notas";
                var myContent = JsonConvert.SerializeObject(nota);
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

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public async void crearNotas(long idInscrito,long idnivel) {
            ClNota nota = new ClNota();

            //servicio extraer temas
            List<ClUnidad> unidades =await ServicioGetTemas(idnivel);
            foreach (ClUnidad a in unidades) {
                long idNomUnidad = a.idNomUnidad;
                nota.IDTEMA = idNomUnidad;
                nota.IDNIVEL = idnivel;
                nota.IDINSCRITO = idInscrito;
                ServicioInsertarNotas(nota);

            }
        }
        public async Task<List<ClNota>> ServicioGetNotas(long idInscrito)
        {
            List<ClNota> compInf = new List<ClNota>();
            try
            {


                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/Notas?idIns=" + idInscrito);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClNota>>(empResponse);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return compInf;

        }



        #endregion
        public async void llenarModal(long idInscrito, long idNivel) {
            List<ClNota> notas = new List<ClNota>();
            notas = await ServicioGetNotas(idInscrito);
            List<ClInscritoAutonomo> ins = await ServicioExtraerInscrito();
            List<ClNivel> niv = await ServicioExtraerNIvel();
            List<ClUnidad> uni = await ServicioGetTemas(idNivel);
            string nombresCompletos;
            var query = from a in notas 
                        join b in uni on a.IDTEMA equals b.idNomUnidad 
                        join c in niv on a.IDNIVEL equals c.idNivel 
                        join d in ins on a.IDINSCRITO equals d.IdInscrito 
                        select new {
                            NombreAlumno=d.NombreInscrito,
                            ApellidosAlumno=d.ApellidoInscrito,
                            NomNivel=c.nomNivel,
                            idNiv=a.IDNIVEL,
                            idIns=a.IDINSCRITO,

                            NomUnidad=b.NomUnidad,
                            IDNota=a.IDNOTA,
                            Unit1=a.UNIT_1,
                            Done1=a.DONE_1,
                            Unit2 = a.UNIT_2,
                            Done2 = a.DONE_2,
                            Unit3 = a.UNIT_3,
                            Done3 = a.DONE_3,
                            Ckeckpoint=a.CHECK_POINT,
                            Unit4 = a.UNIT_4,
                            Done4 = a.DONE_4,
                            Unit5 = a.UNIT_5,
                            Done5 = a.DONE_5,
                            Unit6 = a.UNIT_6,
                            Done6 = a.DONE_6,
                            Estado = a.ESTADO,
                        };
            nombresCompletos= query.Select(x => x.NombreAlumno).FirstOrDefault().Trim()+" "+query.Select(x => x.ApellidosAlumno).FirstOrDefault().Trim();
            txtNombres.Text = nombresCompletos;
            txtnivel.Text = query.Select(x => x.NomNivel).FirstOrDefault();
            string inscrito= query.Select(x => x.idIns).FirstOrDefault().ToString();
            string nivel = query.Select(x => x.idNiv).FirstOrDefault().ToString();
            HiddenIdIns.Value = inscrito;
            HiddenNivel.Value = nivel;
            dgvNotas.DataSource = query;
            dgvNotas.DataBind();
        }
        protected void dgvInscrito_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CrearNotas")
            {
                int fila = Convert.ToInt32(e.CommandArgument);
                long idNivel = Convert.ToInt64(dgvInscrito.Rows[fila].Cells[9].Text);
                long idInscito = Convert.ToInt64(dgvInscrito.DataKeys[fila].Value);
                crearNotas(idInscito,idNivel);
            }
            if (e.CommandName == "VerNotas") {
                
                int fila = Convert.ToInt32(e.CommandArgument);
                long idNivel = Convert.ToInt64(dgvInscrito.Rows[fila].Cells[9].Text);
                long idInscito = Convert.ToInt64(dgvInscrito.DataKeys[fila].Value);
                llenarModal(idInscito,idNivel);
                btnPopUp_ModalPopupExtender2.Show();
            }

        }

        protected void dgvNotas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {


                ClNota no = new ClNota();
                int id = Convert.ToInt32(dgvNotas.DataKeys[e.RowIndex]["IDNota"].ToString());
                TextBox txtUnit1 = (dgvNotas.Rows[e.RowIndex].Cells[1].FindControl("EditUnit1") as TextBox);
                TextBox txtDone1 = (dgvNotas.Rows[e.RowIndex].Cells[2].FindControl("EditDone1") as TextBox);
                TextBox txtUnit2 = (dgvNotas.Rows[e.RowIndex].Cells[3].FindControl("EditUnit2") as TextBox);
                TextBox txtDone2 = (dgvNotas.Rows[e.RowIndex].Cells[4].FindControl("EditDone2") as TextBox);
                TextBox txtUnit3 = (dgvNotas.Rows[e.RowIndex].Cells[5].FindControl("EditUnit3") as TextBox);
                TextBox txtDone3 = (dgvNotas.Rows[e.RowIndex].Cells[6].FindControl("EditDone3") as TextBox);
                TextBox txtCheck = (dgvNotas.Rows[e.RowIndex].Cells[7].FindControl("EditCkeckpoint") as TextBox);
                TextBox txtUnit4 = (dgvNotas.Rows[e.RowIndex].Cells[8].FindControl("EditUnit4") as TextBox);
                TextBox txtDone4 = (dgvNotas.Rows[e.RowIndex].Cells[9].FindControl("EditDone4") as TextBox);
                TextBox txtUnit5 = (dgvNotas.Rows[e.RowIndex].Cells[10].FindControl("EditUnit5") as TextBox);
                TextBox txtDone5 = (dgvNotas.Rows[e.RowIndex].Cells[11].FindControl("EditDone5") as TextBox);
                TextBox txtUnit6 = (dgvNotas.Rows[e.RowIndex].Cells[12].FindControl("EditUnit6") as TextBox);
                TextBox txtDone6 = (dgvNotas.Rows[e.RowIndex].Cells[13].FindControl("EditDone6") as TextBox);
                TextBox txtEstado = (dgvNotas.Rows[e.RowIndex].Cells[14].FindControl("EditEstado") as TextBox);
                no.UNIT_1 = txtUnit1.Text;
                no.DONE_1 = txtDone1.Text;
                no.UNIT_2 = txtUnit2.Text;
                no.DONE_2 = txtDone2.Text;
                no.UNIT_3 = txtUnit3.Text;
                no.DONE_3 = txtDone3.Text;
                no.UNIT_4 = txtUnit4.Text;
                no.DONE_4 = txtDone4.Text;
                no.UNIT_5 = txtUnit5.Text;
                no.DONE_5 = txtDone5.Text;
                no.UNIT_6 = txtUnit6.Text;
                no.DONE_6 = txtDone6.Text;
                no.ESTADO = txtEstado.Text;
                no.CHECK_POINT = txtCheck.Text;
                long idIns = Convert.ToInt64(HiddenIdIns.Value);
                long idNiv = Convert.ToInt64(HiddenNivel.Value);
                actualizarNotas(no, id,idIns,idNiv);
                dgvNotas.EditIndex = -1;
        }

        protected void dgvNotas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

                dgvNotas.EditIndex = -1;
                long idIns = Convert.ToInt64(HiddenIdIns.Value);
                long idNiv = Convert.ToInt64(HiddenNivel.Value);
                llenarModal(idIns, idNiv);
            
        }

        protected void dgvNotas_RowEditing(object sender, GridViewEditEventArgs e)
        {

                dgvNotas.EditIndex = e.NewEditIndex;
                long idIns = Convert.ToInt64(HiddenIdIns.Value);
                long idNiv = Convert.ToInt64(HiddenNivel.Value);
                llenarModal(idIns, idNiv);

        }
    }
}