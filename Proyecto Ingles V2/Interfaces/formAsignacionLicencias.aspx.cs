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
    public partial class formAsignacionLicencias : System.Web.UI.Page
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
                    cargarComboPeriodo();
                    cargarComboNiveles();
                    cargarGridEstudiante();
                }

            }
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
        public async Task<List<ClTipoEstudiante>> ServicioExtraerTipoEstudiante()
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
                    return compInf;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async Task<List<ClInscritoAutonomo>> ServicioExtraerInscrito()//cargar todos inscritos
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
        public async Task<List<CLLibros>> ServicioExtraerLibros()
        {
            List<CLLibros> lib = new List<CLLibros>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/Libros");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    lib = JsonConvert.DeserializeObject<List<CLLibros>>(empResponse);

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return lib;
        }
        public async Task<List<ClLicenciaCambridge>> ServicioExtraerLicencias()
        {
            List<ClLicenciaCambridge> li = new List<ClLicenciaCambridge>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/LicenciasCambridge");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    li = JsonConvert.DeserializeObject<List<ClLicenciaCambridge>>(empResponse);

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return li;

        }
        public async void llenarModal(long idLibro, string nivel, string libro,string nombreAlumno,string cedula)
        {
            List<ClLicenciaCambridge> licencias = await ServicioExtraerLicencias();
            var licencias_ = from a in licencias
                             where a.IdLibro == idLibro && a.EstadoLicencia==1
                             select new
                             {
                                 IDLICENCIA = a.IdLicencia,
                                 LICENCIA = a.Licencia,
                                 ESTADO = a.EstadoLicencia,

                             };
            txtNivel.Text = nivel;
            txtLibro.Text = libro;
            txtNombres.Text = nombreAlumno;
            txtCed.Text = cedula;
            dgvLicencias.DataSource = licencias_.ToList();
            dgvLicencias.DataBind();

        }
        public async void cargarGridEstudiantexced(string numdoc)
        {
            List<ClInscritoAutonomo> inscrito = new List<ClInscritoAutonomo>();
            List<ClTipoEstudiante> tipoEstudiante = new List<ClTipoEstudiante>();
            List<ClPeriodoInscripcion> periodo = new List<ClPeriodoInscripcion>();
            List<ClPrueba> prueba = new List<ClPrueba>();
            List<ClNivel> nivel = new List<ClNivel>();
            List<ClNivelesInscrito> nivelesIns = new List<ClNivelesInscrito>();
            List<ClEstadoEstudiante> estadoEstu = new List<ClEstadoEstudiante>();
            List<CLLibros> libros = new List<CLLibros>();
            List<ClNota> notas = new List<ClNota>();
            List<ClLicenciasEstudiante> licenciasEstudiante = new List<ClLicenciasEstudiante>();
            List<ClLicenciaCambridge> licenciasCambridge = new List<ClLicenciaCambridge>();
            licenciasCambridge = await ServicioExtraerLicencias();
            nivelesIns = await ServicioGetNivelesInscritos();
            tipoEstudiante = await ServicioExtraerTipoEstudiante();
            inscrito = await ServicioExtraerInscrito();
            periodo = await ServicioExtraerPeriodo();
            nivel = await ServicioExtraerNIvel();
            estadoEstu = await ServicioGetEstadoEstudiante();
            libros = await ServicioExtraerLibros();
            licenciasEstudiante = await ServicioExtraerLicenciasEstudiante();
            var query = from a in inscrito
                        join b in nivelesIns on a.IdInscrito equals b.IDINSCRITO
                        join h in licenciasEstudiante on b.IDNIVELESTUDIANTE equals h.IDNIVELESTUDIANTE into nivelLicencia
                        from bh in nivelLicencia.DefaultIfEmpty()
                        join i in licenciasCambridge on (nivelLicencia.Select(x => x.IDLICENCIA).FirstOrDefault()) equals i.IdLicencia into licEs
                        from hi in licEs.DefaultIfEmpty()
                        join c in nivel on b.IDNIVEL equals c.idNivel
                        join d in tipoEstudiante on a.IdTipoEstudiante equals d.IdTipoEstudiante
                        join e in periodo on b.IDPERIODOINSCRIPCION equals e.IdPeriodoInscripcion
                        join f in estadoEstu on b.IDESTADONIVEL equals f.CodEstadoEstu
                        join g in libros on c.idLibro equals g.idLibro

                        where b.PRUEBA == 0 && b.IDESTADONIVEL == 1 && a.NumDocInscrito.Trim()==numdoc.Trim()

                        select new
                        {
                            IdNivelInscrito = b.IDNIVELESTUDIANTE,
                            IdInscrito = a.IdInscrito,
                            IdTipoDocumento = a.IdTipoDocumento,
                            TipoEstudiante = d.DescTipoEstudiante,
                            NombreInscrito = a.NombreInscrito,
                            ApellidoInscrito = a.ApellidoInscrito,
                            NumDocInscrito = a.NumDocInscrito,
                            NomNivel = c.nomNivel,
                            IdPeriodo = b.IDPERIODOINSCRIPCION,
                            Periodo = e.Periodo,
                            IdLibro = g.idLibro,
                            Libro = g.descLibro,
                            LicenciaEstudiante = licEs.Select(x => x.Licencia).FirstOrDefault(),
                        };
            dgvInscrito.DataSource = query.ToList();
            dgvInscrito.DataBind();
        }
        public async void cargarGridEstudiante()
        {
            List<ClInscritoAutonomo> inscrito = new List<ClInscritoAutonomo>();
            List<ClTipoEstudiante> tipoEstudiante = new List<ClTipoEstudiante>();
            List<ClPeriodoInscripcion> periodo = new List<ClPeriodoInscripcion>();
            List<ClPrueba> prueba = new List<ClPrueba>();
            List<ClNivel> nivel = new List<ClNivel>();
            List<ClNivelesInscrito> nivelesIns = new List<ClNivelesInscrito>();
            List<ClEstadoEstudiante> estadoEstu = new List<ClEstadoEstudiante>();
            List<CLLibros> libros = new List<CLLibros>();
            List<ClNota> notas = new List<ClNota>();
            List<ClLicenciasEstudiante> licenciasEstudiante =new List<ClLicenciasEstudiante>();
            List<ClLicenciaCambridge> licenciasCambridge = new List<ClLicenciaCambridge>();
            licenciasCambridge = await ServicioExtraerLicencias();
            nivelesIns = await ServicioGetNivelesInscritos();
            tipoEstudiante = await ServicioExtraerTipoEstudiante();
            inscrito = await ServicioExtraerInscrito();
            periodo = await ServicioExtraerPeriodo();
            nivel = await ServicioExtraerNIvel();
            estadoEstu = await ServicioGetEstadoEstudiante();
            libros = await ServicioExtraerLibros();
            licenciasEstudiante = await ServicioExtraerLicenciasEstudiante();
            var query = from a in inscrito
                        join b in nivelesIns on a.IdInscrito equals b.IDINSCRITO 
                        join h in licenciasEstudiante on b.IDNIVELESTUDIANTE equals h.IDNIVELESTUDIANTE into nivelLicencia from bh in nivelLicencia.DefaultIfEmpty()
                        join i in licenciasCambridge on (nivelLicencia.Select(x=>x.IDLICENCIA).FirstOrDefault()) equals i.IdLicencia into licEs from hi in licEs.DefaultIfEmpty()
                        join c in nivel on b.IDNIVEL equals c.idNivel
                        join d in tipoEstudiante on a.IdTipoEstudiante equals d.IdTipoEstudiante
                        join e in periodo on b.IDPERIODOINSCRIPCION equals e.IdPeriodoInscripcion
                        join f in estadoEstu on b.IDESTADONIVEL equals f.CodEstadoEstu
                        join g in libros on c.idLibro equals g.idLibro

                        where b.PRUEBA == 0  && b.IDESTADONIVEL == 1

                        select new
                        {
                            IdNivelInscrito = b.IDNIVELESTUDIANTE,
                            IdInscrito = a.IdInscrito,
                            IdTipoDocumento = a.IdTipoDocumento,
                            TipoEstudiante = d.DescTipoEstudiante,
                            NombreInscrito = a.NombreInscrito,
                            ApellidoInscrito = a.ApellidoInscrito,
                            NumDocInscrito = a.NumDocInscrito,
                            NomNivel = c.nomNivel,
                            IdPeriodo = b.IDPERIODOINSCRIPCION,
                            Periodo = e.Periodo,
                            IdLibro=g.idLibro,
                            Libro=g.descLibro,
                            LicenciaEstudiante= licEs.Select(x=>x.Licencia).FirstOrDefault(),
                        };
            dgvInscrito.DataSource = query.ToList();
            dgvInscrito.DataBind();
        }
        public async void cargarComboNiveles()
        {
            List<ClNivel> niveles = await ServicioExtraerNIvel();
            var niveles_ = from a in niveles
                           where a.codNivel.Trim() != "SEK1203"
                           orderby a.descNivel ascending
                           select new
                           {
                               idNivel = a.idNivel,
                               nomNivel = a.nomNivel
                           };
            cbxNiveles.DataSource = niveles_;
            cbxNiveles.DataValueField = "idNivel";
            cbxNiveles.DataTextField = "nomNivel";
            cbxNiveles.DataBind();
        }
        public async void cargarComboPeriodo()
        {
            List<ClPeriodoInscripcion> periodos = await ServicioExtraerPeriodo();
            cbxPeriodo.DataSource = periodos;
            cbxPeriodo.DataValueField = "IdPeriodoInscripcion";
            cbxPeriodo.DataTextField = "Periodo";
            cbxPeriodo.DataBind();
        }
        protected void dgvInscrito_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerLicencias") {
                int fila = Convert.ToInt32(e.CommandArgument);//captura el indice de la fila del gridview
                long idLibro = Convert.ToInt64(dgvInscrito.DataKeys[fila]["IdLibro"].ToString());
                HiddenIdLibro.Value = idLibro.ToString();
                long idNivelEstudiante= Convert.ToInt64(dgvInscrito.DataKeys[fila]["IdNivelInscrito"].ToString());//nivelEstudiante
                HiddenNivelEstudiante.Value = idNivelEstudiante.ToString();
                long idInscrito = Convert.ToInt64(dgvInscrito.DataKeys[fila]["IdInscrito"].ToString());//IdInscrito
                HiddenIdIns.Value = idInscrito.ToString();
                string nivel = dgvInscrito.Rows[fila].Cells[5].Text;
                string libro = dgvInscrito.Rows[fila].Cells[9].Text;
                string nombreAlumno= dgvInscrito.Rows[fila].Cells[3].Text.Trim()+" "+ dgvInscrito.Rows[fila].Cells[4].Text.Trim();
                string cedula= dgvInscrito.Rows[fila].Cells[0].Text;
                llenarModal(idLibro, nivel, libro,nombreAlumno,cedula);
                btnPopUp_ModalPopupExtender2.Show();
            }
        }
        public async void ServicioInsertarLicenciaEstudiante(ClLicenciasEstudiante licencia)
        {

            try
            {
                string uri = "api/LicenciasEstudiante";
                var myContent = JsonConvert.SerializeObject(licencia);
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
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "xcript", script, true);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        protected void dgvInscrito_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvInscrito.PageIndex = e.NewPageIndex;
            cargarGridEstudiante();
        }

        protected void btnExccel_Click(object sender, EventArgs e)
        {

        }
        public async Task<List<ClLicenciasEstudiante>> ServicioExtraerLicenciasEstudiante()
        {
            List<ClLicenciasEstudiante> li = new List<ClLicenciasEstudiante>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/LicenciasEstudiante");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    li = JsonConvert.DeserializeObject<List<ClLicenciasEstudiante>>(empResponse);

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return li;

        }
        public async Task<bool> validarLicencia(long idNivelestudiante,long idinscrito)
        {
            bool resp = false;
            List<ClLicenciasEstudiante> licencias = new List<ClLicenciasEstudiante>();
            licencias = await ServicioExtraerLicenciasEstudiante();
            var licencias_ = from a in licencias
                             where a.IDINSCRITO == idinscrito && a.IDNIVELESTUDIANTE == idNivelestudiante
                             select new { a };
            if (licencias_.Count() > 0)//existe ya una licencias asignada a ese nivel
            {
                resp = true;
            }
            return resp;
         
        }
        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            string identificacion = txtCedula.Text.Trim();
            long idNivel = Convert.ToInt64(cbxNiveles.SelectedValue.ToString());
            long idPeriodo = Convert.ToInt64(cbxPeriodo.SelectedValue.ToString());
            if (txtCedula.Text.Trim() != "" && idPeriodo == 0 && idNivel == 0)//busca solo por identificacion
            {
                cargarGridEstudiantexced(identificacion);
            }
            else if (idNivel == 0 && identificacion.Trim() == "" && idPeriodo == 0)//busca todos
            {
                cargarGridEstudiante();
            }
            else if (idNivel != 0 && idPeriodo == 0)//busca solo por nivel
            {

                BuscarEstudianteXNivel(idNivel);
            }
            else if (idPeriodo != 0 && idNivel == 0)//busca solo por periodo
            {
                BuscarEstudiantexPeriodo(idPeriodo);
            }
            else if (idPeriodo != 0 && idNivel != 0 && identificacion != "")//busca por todos las opciones
            {
                BuscarEstudiantexTodosParametros(idNivel, idPeriodo, identificacion);
            }

        }
        public async void BuscarEstudiantexTodosParametros(long idNivel, long idperiodo, string identificacion)
        {

            List<ClInscritoAutonomo> inscrito = new List<ClInscritoAutonomo>();
            List<ClTipoEstudiante> tipoEstudiante = new List<ClTipoEstudiante>();
            List<ClPeriodoInscripcion> periodo = new List<ClPeriodoInscripcion>();
            List<ClPrueba> prueba = new List<ClPrueba>();
            List<ClNivel> nivel = new List<ClNivel>();
            List<ClNivelesInscrito> nivelesIns = new List<ClNivelesInscrito>();
            List<ClEstadoEstudiante> estadoEstu = new List<ClEstadoEstudiante>();
            List<CLLibros> libros = new List<CLLibros>();
            List<ClNota> notas = new List<ClNota>();
            List<ClLicenciasEstudiante> licenciasEstudiante = new List<ClLicenciasEstudiante>();
            List<ClLicenciaCambridge> licenciasCambridge = new List<ClLicenciaCambridge>();
            licenciasCambridge = await ServicioExtraerLicencias();
            nivelesIns = await ServicioGetNivelesInscritos();
            tipoEstudiante = await ServicioExtraerTipoEstudiante();
            inscrito = await ServicioExtraerInscrito();
            periodo = await ServicioExtraerPeriodo();
            nivel = await ServicioExtraerNIvel();
            estadoEstu = await ServicioGetEstadoEstudiante();
            libros = await ServicioExtraerLibros();
            licenciasEstudiante = await ServicioExtraerLicenciasEstudiante();
            var query = from a in inscrito
                        join b in nivelesIns on a.IdInscrito equals b.IDINSCRITO
                        join h in licenciasEstudiante on b.IDNIVELESTUDIANTE equals h.IDNIVELESTUDIANTE into nivelLicencia
                        from bh in nivelLicencia.DefaultIfEmpty()
                        join i in licenciasCambridge on (nivelLicencia.Select(x => x.IDLICENCIA).FirstOrDefault()) equals i.IdLicencia into licEs
                        from hi in licEs.DefaultIfEmpty()
                        join c in nivel on b.IDNIVEL equals c.idNivel
                        join d in tipoEstudiante on a.IdTipoEstudiante equals d.IdTipoEstudiante
                        join e in periodo on b.IDPERIODOINSCRIPCION equals e.IdPeriodoInscripcion
                        join f in estadoEstu on b.IDESTADONIVEL equals f.CodEstadoEstu
                        join g in libros on c.idLibro equals g.idLibro

                        where b.PRUEBA == 0 && b.IDESTADONIVEL == 1 && b.IDPERIODOINSCRIPCION == idperiodo && b.IDNIVEL==idNivel && a.NumDocInscrito.Trim()==identificacion.Trim()

                        select new
                        {
                            IdNivelInscrito = b.IDNIVELESTUDIANTE,
                            IdInscrito = a.IdInscrito,
                            IdTipoDocumento = a.IdTipoDocumento,
                            TipoEstudiante = d.DescTipoEstudiante,
                            NombreInscrito = a.NombreInscrito,
                            ApellidoInscrito = a.ApellidoInscrito,
                            NumDocInscrito = a.NumDocInscrito,
                            NomNivel = c.nomNivel,
                            IdPeriodo = b.IDPERIODOINSCRIPCION,
                            Periodo = e.Periodo,
                            IdLibro = g.idLibro,
                            Libro = g.descLibro,
                            LicenciaEstudiante = licEs.Select(x => x.Licencia).FirstOrDefault(),
                        };
            dgvInscrito.DataSource = query.ToList();
            dgvInscrito.DataBind();

        }
            public async void BuscarEstudiantexPeriodo(long idPeriodo)
        {
            List<ClInscritoAutonomo> inscrito = new List<ClInscritoAutonomo>();
            List<ClTipoEstudiante> tipoEstudiante = new List<ClTipoEstudiante>();
            List<ClPeriodoInscripcion> periodo = new List<ClPeriodoInscripcion>();
            List<ClPrueba> prueba = new List<ClPrueba>();
            List<ClNivel> nivel = new List<ClNivel>();
            List<ClNivelesInscrito> nivelesIns = new List<ClNivelesInscrito>();
            List<ClEstadoEstudiante> estadoEstu = new List<ClEstadoEstudiante>();
            List<CLLibros> libros = new List<CLLibros>();
            List<ClNota> notas = new List<ClNota>();
            List<ClLicenciasEstudiante> licenciasEstudiante = new List<ClLicenciasEstudiante>();
            List<ClLicenciaCambridge> licenciasCambridge = new List<ClLicenciaCambridge>();
            licenciasCambridge = await ServicioExtraerLicencias();
            nivelesIns = await ServicioGetNivelesInscritos();
            tipoEstudiante = await ServicioExtraerTipoEstudiante();
            inscrito = await ServicioExtraerInscrito();
            periodo = await ServicioExtraerPeriodo();
            nivel = await ServicioExtraerNIvel();
            estadoEstu = await ServicioGetEstadoEstudiante();
            libros = await ServicioExtraerLibros();
            licenciasEstudiante = await ServicioExtraerLicenciasEstudiante();
            var query = from a in inscrito
                        join b in nivelesIns on a.IdInscrito equals b.IDINSCRITO
                        join h in licenciasEstudiante on b.IDNIVELESTUDIANTE equals h.IDNIVELESTUDIANTE into nivelLicencia
                        from bh in nivelLicencia.DefaultIfEmpty()
                        join i in licenciasCambridge on (nivelLicencia.Select(x => x.IDLICENCIA).FirstOrDefault()) equals i.IdLicencia into licEs
                        from hi in licEs.DefaultIfEmpty()
                        join c in nivel on b.IDNIVEL equals c.idNivel
                        join d in tipoEstudiante on a.IdTipoEstudiante equals d.IdTipoEstudiante
                        join e in periodo on b.IDPERIODOINSCRIPCION equals e.IdPeriodoInscripcion
                        join f in estadoEstu on b.IDESTADONIVEL equals f.CodEstadoEstu
                        join g in libros on c.idLibro equals g.idLibro

                        where b.PRUEBA == 0 && b.IDESTADONIVEL == 1 && b.IDPERIODOINSCRIPCION==idPeriodo

                        select new
                        {
                            IdNivelInscrito = b.IDNIVELESTUDIANTE,
                            IdInscrito = a.IdInscrito,
                            IdTipoDocumento = a.IdTipoDocumento,
                            TipoEstudiante = d.DescTipoEstudiante,
                            NombreInscrito = a.NombreInscrito,
                            ApellidoInscrito = a.ApellidoInscrito,
                            NumDocInscrito = a.NumDocInscrito,
                            NomNivel = c.nomNivel,
                            IdPeriodo = b.IDPERIODOINSCRIPCION,
                            Periodo = e.Periodo,
                            IdLibro = g.idLibro,
                            Libro = g.descLibro,
                            LicenciaEstudiante = licEs.Select(x => x.Licencia).FirstOrDefault(),
                        };
            dgvInscrito.DataSource = query.ToList();
            dgvInscrito.DataBind();
        }
            public async void BuscarEstudianteXNivel(long idNivel)
        {
            List<ClInscritoAutonomo> inscrito = new List<ClInscritoAutonomo>();
            List<ClTipoEstudiante> tipoEstudiante = new List<ClTipoEstudiante>();
            List<ClPeriodoInscripcion> periodo = new List<ClPeriodoInscripcion>();
            List<ClPrueba> prueba = new List<ClPrueba>();
            List<ClNivel> nivel = new List<ClNivel>();
            List<ClNivelesInscrito> nivelesIns = new List<ClNivelesInscrito>();
            List<ClEstadoEstudiante> estadoEstu = new List<ClEstadoEstudiante>();
            List<CLLibros> libros = new List<CLLibros>();
            List<ClNota> notas = new List<ClNota>();
            List<ClLicenciasEstudiante> licenciasEstudiante = new List<ClLicenciasEstudiante>();
            List<ClLicenciaCambridge> licenciasCambridge = new List<ClLicenciaCambridge>();
            licenciasCambridge = await ServicioExtraerLicencias();
            nivelesIns = await ServicioGetNivelesInscritos();
            tipoEstudiante = await ServicioExtraerTipoEstudiante();
            inscrito = await ServicioExtraerInscrito();
            periodo = await ServicioExtraerPeriodo();
            nivel = await ServicioExtraerNIvel();
            estadoEstu = await ServicioGetEstadoEstudiante();
            libros = await ServicioExtraerLibros();
            licenciasEstudiante = await ServicioExtraerLicenciasEstudiante();
            var query = from a in inscrito
                        join b in nivelesIns on a.IdInscrito equals b.IDINSCRITO
                        join h in licenciasEstudiante on b.IDNIVELESTUDIANTE equals h.IDNIVELESTUDIANTE into nivelLicencia
                        from bh in nivelLicencia.DefaultIfEmpty()
                        join i in licenciasCambridge on (nivelLicencia.Select(x => x.IDLICENCIA).FirstOrDefault()) equals i.IdLicencia into licEs
                        from hi in licEs.DefaultIfEmpty()
                        join c in nivel on b.IDNIVEL equals c.idNivel
                        join d in tipoEstudiante on a.IdTipoEstudiante equals d.IdTipoEstudiante
                        join e in periodo on b.IDPERIODOINSCRIPCION equals e.IdPeriodoInscripcion
                        join f in estadoEstu on b.IDESTADONIVEL equals f.CodEstadoEstu
                        join g in libros on c.idLibro equals g.idLibro

                        where b.PRUEBA == 0 && b.IDESTADONIVEL == 1 && b.IDNIVEL==idNivel

                        select new
                        {
                            IdNivelInscrito = b.IDNIVELESTUDIANTE,
                            IdInscrito = a.IdInscrito,
                            IdTipoDocumento = a.IdTipoDocumento,
                            TipoEstudiante = d.DescTipoEstudiante,
                            NombreInscrito = a.NombreInscrito,
                            ApellidoInscrito = a.ApellidoInscrito,
                            NumDocInscrito = a.NumDocInscrito,
                            NomNivel = c.nomNivel,
                            IdPeriodo = b.IDPERIODOINSCRIPCION,
                            Periodo = e.Periodo,
                            IdLibro = g.idLibro,
                            Libro = g.descLibro,
                            LicenciaEstudiante = licEs.Select(x => x.Licencia).FirstOrDefault(),
                        };
            dgvInscrito.DataSource = query.ToList();
            dgvInscrito.DataBind();

        }
            public async void asignarLicenciaEstudiante(ClLicenciasEstudiante licencias)
        {
            bool resp =await validarLicencia(licencias.IDNIVELESTUDIANTE,licencias.IDINSCRITO);
            if (resp)//ya existe una licencia asignada para ese nivel
            {
                string script = "existeLicenciaAsignada();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "xcript", script, true);
            }
            else
            ServicioInsertarLicenciaEstudiante(licencias);
        }
        protected void dgvLicencias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ClLicenciasEstudiante licencias = new ClLicenciasEstudiante();
            if (e.CommandName== "asignarLicencia") {
                int fila = Convert.ToInt32(e.CommandArgument);//captura el indice de la fila del gridview
                long idNivelEstudiante = Convert.ToInt64(HiddenNivelEstudiante.Value);
                long idInscrito= Convert.ToInt64(HiddenIdIns.Value);
                long idLicencia= Convert.ToInt64(dgvLicencias.DataKeys[fila]["IDLICENCIA"].ToString());//IdInscrito
                licencias.IDLICENCIA = idLicencia;
                licencias.IDNIVELESTUDIANTE = idNivelEstudiante;
                licencias.IDINSCRITO = idInscrito;
                asignarLicenciaEstudiante(licencias);
                //agregar servcio para insertar licencia 
            }
        }

        protected void dgvLicencias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            long idLibro = Convert.ToInt64(HiddenIdLibro.Value);
            string nivel = txtNivel.Text.Trim();
            string alumno = txtNombres.Text.Trim();
            string cedula = txtCed.Text.Trim();
            string libro = txtLibro.Text.Trim();
            dgvLicencias.PageIndex = e.NewPageIndex;
            llenarModal(idLibro,nivel,libro,alumno,cedula);
        }
    }
}