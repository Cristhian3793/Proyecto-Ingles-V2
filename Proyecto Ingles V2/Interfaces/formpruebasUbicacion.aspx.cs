using System;
using System.Collections.Generic;
using System.Collections;
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
using NHibernate;
using Logica.Conexion;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using ClosedXML.Excel;
using System.Data;

namespace Proyecto_Ingles_V2.Interfaces
{
    public partial class pruebasUbicacion : System.Web.UI.Page
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
                    cargarGridPruebas();
                }

            }

        }
        #region Invocacion Servicios

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
        //public async void ActualizarNivelInscrito(long idInscrito, long IdNivel, int estado)
        //{
        //    HttpResponseMessage response = new HttpResponseMessage();
        //    try
        //    {
        //        ClInscritoAutonomo ins = new ClInscritoAutonomo();
        //        ins.IdInscrito = idInscrito;
        //        ins.IdEstadoEstudiante = estado;
        //        string uri = "api/InscritoAutonomo?idInscrito=" + ins.IdInscrito;
        //        var myContent = JsonConvert.SerializeObject(ins);
        //        var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
        //        var client = new HttpClient();
        //        client.BaseAddress = new Uri(url);
        //        client.DefaultRequestHeaders.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
        //        var method = new HttpMethod("PATCH");
        //        var request = new HttpRequestMessage(method, uri)
        //        {
        //            Content = stringContent
        //        };
        //        response = await client.SendAsync(request);

        //    }
        //    catch (TaskCanceledException e)
        //    {
        //    }
        //}

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
        #endregion

        #region Metodos
     


        public async void cargarGridPruebas()
        {
            List<ClInscritoAutonomo> inscrito = new List<ClInscritoAutonomo>();
            List<ClTipoEstudiante> tipoEstudiante = new List<ClTipoEstudiante>();
            List<ClPeriodoInscripcion> periodo = new List<ClPeriodoInscripcion>();
            List<ClPrueba> prueba = new List<ClPrueba>();
            List<ClNivel> nivel = new List<ClNivel>();
            List<ClNivelesInscrito> nivelesIns = new List<ClNivelesInscrito>();
            List<ClEstadoEstudiante> estadoEstudiante = await ServicioGetEstadoEstudiante();
            nivelesIns = await ServicioGetNivelesInscritos();
            tipoEstudiante = await ServicioExtraerTipoEstudiante();
            inscrito = await ServicioExtraerInscrito();
            periodo = await ServicioExtraerPeriodo();
            prueba = await ServicioExtraerPrueba();
            nivel = await ServicioExtraerNIvel();
            var query = from a in inscrito
                        join f in nivelesIns on a.IdInscrito equals f.IDINSCRITO
                        join d in prueba on f.IDINSCRITO equals d.IdInscrito// into s
                       // from d in s.DefaultIfEmpty()
                        join e in nivel on f.IDNIVEL equals e.idNivel
                        join b in tipoEstudiante on a.IdTipoEstudiante equals b.IdTipoEstudiante
                        join c in periodo on f.IDPERIODOINSCRIPCION equals c.IdPeriodoInscripcion
                        join g in estadoEstudiante on f.IDESTADONIVEL equals g.CodEstadoEstu
                        orderby a.NombreInscrito ascending
                        where f.PRUEBA==1 
                        select new
                        {
                            IdInscrito = a.IdInscrito,
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
                            IdPrueba = d.IdPrueba,//s.Select(x => x.IdPrueba).FirstOrDefault(),
                            NomNivel = e.nomNivel,
                            Estado = g.DescEstEstudiante,
                            Calificacion = d.PunjatePrueba//s.Select(x => x.PunjatePrueba).FirstOrDefault(),
                        };
            dgvNotasPruebas.DataSource = query.ToList();
            dgvNotasPruebas.DataBind();
        }
        public async void cargarGridPruebasXInscrito(string cedula)
        {
            List<ClInscritoAutonomo> inscrito = new List<ClInscritoAutonomo>();
            List<ClTipoEstudiante> tipoEstudiante = new List<ClTipoEstudiante>();
            List<ClPeriodoInscripcion> periodo = new List<ClPeriodoInscripcion>();
            List<ClPrueba> prueba = new List<ClPrueba>();
            List<ClNivel> nivel = new List<ClNivel>();
            List<ClNivelesInscrito> nivelesIns = new List<ClNivelesInscrito>();
            List<ClEstadoEstudiante> estadoEstudiante =await ServicioGetEstadoEstudiante();
            nivelesIns = await ServicioGetNivelesInscritos();
            tipoEstudiante = await ServicioExtraerTipoEstudiante();
            inscrito = await ServicioExtraerInscrito();
            periodo = await ServicioExtraerPeriodo();
            prueba = await ServicioExtraerPrueba();
            nivel = await ServicioExtraerNIvel();
            var query = from a in inscrito
                        join f in nivelesIns on a.IdInscrito equals f.IDINSCRITO
                        join d in prueba on f.IDNIVELESTUDIANTE equals d.IDNIVELESTUDIANTE //into s
                        //from x in s.DefaultIfEmpty()
                        join e in nivel on f.IDNIVEL equals e.idNivel
                        join b in tipoEstudiante on a.IdTipoEstudiante equals b.IdTipoEstudiante
                        join c in periodo on f.IDPERIODOINSCRIPCION equals c.IdPeriodoInscripcion
                        join g in estadoEstudiante on f.IDESTADONIVEL equals g.CodEstadoEstu
                        orderby a.NombreInscrito ascending
                        where a.NumDocInscrito.Trim() == cedula.Trim() && f.PRUEBA == 1
                        select new
                        {
                            IdInscrito = a.IdInscrito,
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
                            IdPrueba = d.IdPrueba,
                            NomNivel = e.nomNivel,
                            Estado=g.DescEstEstudiante,
                            Calificacion = d.PunjatePrueba,
                        };
            dgvNotasPruebas.DataSource = query.ToList();
            dgvNotasPruebas.DataBind();

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
            bool resp;
            long idnivelestudiante = pru.IDNIVELESTUDIANTE;
            resp = await VerificarPagos(pru.IdInscrito, idnivelestudiante);
            if (resp == false)
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
                            nivIns.IDESTADONIVEL = 0;//llamar funcion estado pagado o no pagado
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
                            niv.IDPERIODOINSCRIPCION =await  extrarIdPeridoActivo();
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
            else
            {
                string script = "rechazado();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "xcript", script, true);
            }

          
        }
        #endregion
        #region Metodos forms
        public async Task<long> extrarIdPeridoActivo()
        {
            List<ClPeriodoInscripcion> periodoActivo = await ServicioExtraerPeriodo();
            long idPeriodo;
            var periodo = from a in periodoActivo
                          where a.EstadoPeriodo == 1
                          select new
                          {
                              IDPERIODO = a.IdPeriodoInscripcion,
                          };
            idPeriodo = Convert.ToInt64(periodo.Select(x => x.IDPERIODO).FirstOrDefault());
            return idPeriodo;

        }

        protected void dgvNotasPruebas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            dgvNotasPruebas.EditIndex = e.NewEditIndex;

        }

        protected void dgvNotasPruebas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dgvNotasPruebas.EditIndex = -1;
            cargarGridPruebas();
        }
        public async Task<List<ClCalificacionNivel>> ExtraerCalificacion()
        {
            List<ClCalificacionNivel> clif = new List<ClCalificacionNivel>();
            clif = await ServicioExtraerCalificacion();
            return clif;
        }
        public async Task<bool> VerificarPagos(long idEstu,long idNivelEstu) {
            List<ClNivelesInscrito> compInf = new List<ClNivelesInscrito>();
            int contador=0;
            bool resp = false;
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
                    var query = from a in compInf
                                where a.IDINSCRITO == idEstu && a.IDNIVELESTUDIANTE==idNivelEstu
                                select new
                                {
                                    EstadoNivel=a.IDESTADONIVEL,
                                };
                    foreach (var a in query) {
                        if (a.EstadoNivel == 0)
                        {
                            contador++;
                            resp = true;
                        }
                    }
                    return resp;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return resp;
        }

        protected void dgvNotasPruebas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ClPrueba pru = new ClPrueba();
            bool resp;
            List<ClCalificacionNivel> calificNivel = new List<ClCalificacionNivel>();

            int id = Convert.ToInt32(dgvNotasPruebas.DataKeys[e.RowIndex]["idPrueba"].ToString());
            
            long idInscrito = Convert.ToInt64(dgvNotasPruebas.DataKeys[e.RowIndex]["IdInscrito"].ToString());

                double? calificacion;
                //capturar el valor de un edittamplate
                TextBox txtCalificacion = (dgvNotasPruebas.Rows[e.RowIndex].Cells[4].FindControl("EditCalificacion") as TextBox);
                if (txtCalificacion.Text != "")
                {
                    calificacion = Convert.ToDouble(txtCalificacion.Text.ToString());
                }
                else
                {
                    calificacion = null;
                }
                pru.IdPrueba = id;
                pru.PunjatePrueba = calificacion;
                pru.IdInscrito = idInscrito;
                //traer servicio
                actualizarNotaPruebaUbicacion(pru);
                dgvNotasPruebas.EditIndex = -1;
                cargarGridPruebas();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (txtCedula.Text.Trim().ToString() != "")
            {
                cargarGridPruebasXInscrito(txtCedula.Text);
            }
            else
            {
                cargarGridPruebas();
            }
        }
        #endregion

         public override void VerifyRenderingInServerForm(Control control)
        {

        }
        public DataSet cargarDatosExcel()
        {
            DataSet lista = conexionBaseExterna.GetPruebas();
            return lista;


        }
        protected  void LinkButton1_Click(object sender, EventArgs e)//excel
        {
            string[] cabecera = { "Cod Estudiante,Identificacion","Tipo Estudiante", "Periodo Inscripcion","Fecha Registro","Estado Prueba","Nombres", "Apellidos", "Celular", "Telefono", "Email","Estado","Calificacion" };
            DataSet lista = cargarDatosExcel();
            DataTable dt2 = lista.Tables[0];
            DateTime fecha = DateTime.Now;
            string fechaDocument = Convert.ToString(fecha);
            string nombreArchivo = "CalificacionPruebasUbicacion" + "_" + fechaDocument;
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt2);

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + nombreArchivo + ".xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        protected void dgvNotasPruebas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvNotasPruebas.PageIndex = e.NewPageIndex;
            cargarGridPruebas();
        }
    }
}