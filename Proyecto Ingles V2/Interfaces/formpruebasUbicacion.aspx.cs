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
                    cargarGridPruebas();
                }

            }
    
        }
        #region Invocacion Servicios
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
        public async void ServicioEliiminarInscrito(int idInscrito)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.DeleteAsync("api/InscritoAutonomo/" + idInscrito);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                }
                await ServicioExtraerInscrito();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
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
        public async void ActualizarNivelInscrito(long idInscrito,long IdNivel) {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ClInscritoAutonomo ins = new ClInscritoAutonomo();
                ins.IdInscrito = idInscrito;
                ins.IdNivel = IdNivel;
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
        #endregion

        #region Metodos
        public async void cargarGridPruebas()
        {
            List<ClInscritoAutonomo> inscrito = new List<ClInscritoAutonomo>();
            List<ClTipoEstudiante> tipoEstudiante = new List<ClTipoEstudiante>();
            List<ClPeriodoInscripcion> periodo = new List<ClPeriodoInscripcion>();
            List<ClPrueba> prueba = new List<ClPrueba>();
            List<ClNivel> nivel = new List<ClNivel>();
            tipoEstudiante = await ServicioExtraerTipoEstudiante();
            inscrito = await ServicioExtraerInscrito();
            periodo = await ServicioExtraerPeriodo();
            prueba = await ServicioExtraerPrueba();
            nivel = await ServicioExtraerNIvel();
            var query = from a in inscrito
                        join e in nivel on a.IdNivel equals e.idNivel into s
                        from x in s.DefaultIfEmpty()
                        join b in tipoEstudiante on a.IdTipoEstudiante equals b.IdTipoEstudiante
                        join c in periodo on a.idPerInscripcion equals c.IdPeriodoInscripcion
                        join d in prueba on a.IdInscrito equals d.IdInscrito
                        orderby a.NombreInscrito ascending
                        where a.EstadoPrueba == 1 || a.EstadoPrueba == 0
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
                            IdPrueba = d.IdPrueba,
                            NomNivel = s.Select(x=>x.nomNivel).FirstOrDefault(),
                            Calificacion = d.PunjatePrueba,
                        };
            dgvNotasPruebas.DataSource = query;
            dgvNotasPruebas.DataBind();
        }
        public async void cargarGridPruebasXInscrito(string cedula)
        {
            List<ClInscritoAutonomo> inscrito = new List<ClInscritoAutonomo>();
            List<ClTipoEstudiante> tipoEstudiante = new List<ClTipoEstudiante>();
            List<ClPeriodoInscripcion> periodo = new List<ClPeriodoInscripcion>();
            List<ClPrueba> prueba = new List<ClPrueba>();
            List<ClNivel> nivel = new List<ClNivel>();
            tipoEstudiante = await ServicioExtraerTipoEstudiante();
            inscrito = await ServicioExtraerInscrito();
            periodo = await ServicioExtraerPeriodo();
            prueba = await ServicioExtraerPrueba();
            nivel = await ServicioExtraerNIvel();
            var query = from a in inscrito
                        join e in nivel on a.IdNivel equals e.idNivel into s
                        from x in s.DefaultIfEmpty()
                        join b in tipoEstudiante on a.IdTipoEstudiante equals b.IdTipoEstudiante
                        join c in periodo on a.idPerInscripcion equals c.IdPeriodoInscripcion
                        join d in prueba on a.IdInscrito equals d.IdInscrito
                        orderby a.NombreInscrito ascending
                        where a.NumDocInscrito.Trim() == cedula.Trim()
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
                            IdPrueba = d.IdPrueba,
                            NomNivel = s.Select(x => x.nomNivel).FirstOrDefault(),
                            Calificacion = d.PunjatePrueba,
                        };
            dgvNotasPruebas.DataSource = query;
            dgvNotasPruebas.DataBind();
           
        }
        public async Task<long> extraerIdNivel(double? calificacion) {
            long nivel=0;
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
        public async void actualizarNotaPruebaUbicacion(ClPrueba pru)
        {
            try { 
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
                long idNIvel =await extraerIdNivel(pru.PunjatePrueba);
                //llamar servicio actualizar nivel de tabla inscritos
                ActualizarNivelInscrito(pru.IdInscrito,idNIvel);
                cargarGridPruebas();
            }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

        }
        #endregion


        #region Metodos forms


        protected void dgvNotasPruebas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            dgvNotasPruebas.EditIndex = e.NewEditIndex;
            //cargarGridPruebasXInscrito(txtCedula.Text.Trim().ToString());
            //cargarGridPruebas();
        }

        protected void dgvNotasPruebas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dgvNotasPruebas.EditIndex = -1;
            cargarGridPruebas();
        }
        public async Task<List<ClCalificacionNivel>> ExtraerCalificacion(){
            List<ClCalificacionNivel> clif = new List<ClCalificacionNivel>();
            clif = await ServicioExtraerCalificacion();
            return clif;
        }
        protected void dgvNotasPruebas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ClPrueba pru = new ClPrueba();

            List<ClCalificacionNivel> calificNivel = new List<ClCalificacionNivel>();

            int id = Convert.ToInt32(dgvNotasPruebas.DataKeys[e.RowIndex]["idPrueba"].ToString());
            long idInscrito = Convert.ToInt64(dgvNotasPruebas.DataKeys[e.RowIndex]["IdInscrito"].ToString());
            double? calificacion;
            //capturar el valor de un edittamplate
            TextBox txtCalificacion = (dgvNotasPruebas.Rows[e.RowIndex].Cells[4].FindControl("EditCalificacion") as TextBox);
            if (txtCalificacion.Text!="")
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

        protected void btnExccel_Click(object sender, EventArgs e)
        {
            DateTime fecha = DateTime.Now;
            string fechaDocument = Convert.ToString(fecha);
            string nombreArchivo = "CalificacionPruebasUbicacion" + "_" + fechaDocument;
            DataTable dt = new DataTable("calificacion");
            foreach (TableCell cell in dgvNotasPruebas.HeaderRow.Cells)
            {
                dt.Columns.Add(cell.Text);
            }
            foreach (GridViewRow row in dgvNotasPruebas.Rows)
            {
                dt.Rows.Add();
                for (int i = 0; i < row.Cells.Count; i++)
                {

                    dt.Rows[dt.Rows.Count - 1][i] = row.Cells[i].Text;
                    dt.Rows[dt.Rows.Count - 1][i] = row.Cells[i].Text.Replace("&nbsp;", "N.A");
                }
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);

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
        public override void VerifyRenderingInServerForm(Control control)
        {

        }
    }
}