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
using Logica.Conexion;
using ClosedXML.Excel;
using System.IO;

namespace Proyecto_Ingles_V2.Interfaces
{
    public partial class formHistoricoNotas : System.Web.UI.Page
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

        protected void dgvInscrito_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerNotas")
            {
                dgvNotas.DataSource = "";
                dgvNotas.DataBind();
                txtPromedio.Text = "";
                string nombre, apellidos;
                int fila = Convert.ToInt32(e.CommandArgument);//captura el indice de la fila del gridview
                long idInscito = Convert.ToInt64(dgvInscrito.DataKeys[fila]["IdInscrito"].ToString());
                string numDoc = dgvInscrito.Rows[fila].Cells[0].Text.ToString().Trim();//captura la cedula o pasaporte
                long idNivelInscrito = Convert.ToInt64(dgvInscrito.DataKeys[fila]["IdNivelInscrito"].ToString());
                string nomNivel = dgvInscrito.Rows[fila].Cells[5].Text.ToString().Trim();
                nombre = dgvInscrito.Rows[fila].Cells[3].Text.ToString().Trim();
                apellidos= dgvInscrito.Rows[fila].Cells[4].Text.ToString().Trim();
                txtNombres.Text = nombre + " " + apellidos;
                txtCed.Text = numDoc;
                txtNivel.Text = nomNivel;
                cargargridNotas(idInscito,idNivelInscrito);//cargar en modal notas niveles cerrados
                HiddenIdIns.Value = idInscito.ToString();

                btnPopUp_ModalPopupExtender2.Show();
            }
        }
        public async void cargarGridEstudiante()
        {
            int[] estadoNivel = { 1, 2 };
            List<ClInscritoAutonomo> inscrito = new List<ClInscritoAutonomo>();
            List<ClTipoEstudiante> tipoEstudiante = new List<ClTipoEstudiante>();
            List<ClPeriodoInscripcion> periodo = new List<ClPeriodoInscripcion>();
            List<ClPrueba> prueba = new List<ClPrueba>();
            List<ClNivel> nivel = new List<ClNivel>();
            List<ClNivelesInscrito> nivelesIns = new List<ClNivelesInscrito>();
            List<ClEstadoEstudiante> estadoEstu = new List<ClEstadoEstudiante>();
            List<ClNota> notas = new List<ClNota>();
            nivelesIns = await ServicioGetNivelesInscritos();
            tipoEstudiante = await ServicioExtraerTipoEstudiante();
            inscrito = await ServicioExtraerInscrito();
            periodo = await ServicioExtraerPeriodo();
            prueba = await ServicioExtraerPrueba();
            nivel = await ServicioExtraerNIvel();
            estadoEstu = await ServicioGetEstadoEstudiante();
            notas = await ServicioGetAllNotas();
            var query = from a in inscrito
                        join b in nivelesIns on a.IdInscrito equals b.IDINSCRITO
                        join c in nivel on b.IDNIVEL equals c.idNivel
                        join d in tipoEstudiante on a.IdTipoEstudiante equals d.IdTipoEstudiante
                        join e in periodo on b.IDPERIODOINSCRIPCION equals e.IdPeriodoInscripcion
                        join f in estadoEstu on b.IDESTADONIVEL equals f.CodEstadoEstu
                        join g in notas on b.IDNIVELESTUDIANTE equals g.IDNIVELESTUDINTE
                        where b.PRUEBA == 0 && g.ESTADO==3 && g.IDNIVEL==b.IDNIVEL && estadoNivel.Contains(b.ESTADONIVEL) && b.IDESTADONIVEL==1
                        //0 son solo niveles,3 significa promedio,1 significa nivel cerrado,1 significa pagado
                        //group new { a, d } by new { a.IdInscrito, d.DescTipoEstudiante, a.NombreInscrito, a.ApellidoInscrito, a.NumDocInscrito } into ad
                        select new
                        {
                            IdNivelInscrito=b.IDNIVELESTUDIANTE,
                            IdInscrito = a.IdInscrito,
                            IdTipoDocumento = a.IdTipoDocumento,
                            TipoEstudiante = d.DescTipoEstudiante,
                            NombreInscrito = a.NombreInscrito,
                            ApellidoInscrito = a.ApellidoInscrito,
                            NumDocInscrito = a.NumDocInscrito,
                            NomNivel=c.nomNivel,
                            IdPeriodo = b.IDPERIODOINSCRIPCION,
                            Periodo = e.Periodo,
                            Promedio =g.CALIFICACION
                        };
            dgvInscrito.DataSource = query.ToList();
            dgvInscrito.DataBind();
        }
        #region Servicios
        public DataSet cargarDatosExcel()
        {
            DataSet lista = conexionBaseExterna.GetHistoricoNotas();
            return lista;


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
        public async Task<List<ClPrueba>> ServicioExtraerPrueba()
        {
            List<ClPrueba> compInf = new List<ClPrueba>();
            try
            {           
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
        public async Task<List<ClNivelesInscrito>> ServicioGetNivelInscrito(long idInscrito)
        {
            List<ClNivelesInscrito> compInf = new List<ClNivelesInscrito>();
            try
            {


                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/NivelesInscrito/" + idInscrito);
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
        
        //crear servicio para buscar nivel y extraer estatus 
        public async void ExtraerEstatus(long idNivel)
        {
            List<ClNivelesInscrito> niveles = await ServicioGetNivelInscrito(idNivel);
            List<ClEstadoNota> estadoNota= await ServicioGetEstadoNota();
            var query = from a in niveles
                        join b in estadoNota on a.ESTADONIVEL equals b.CODESTADONOTA
                        select new { 
                        ESTADONIVEL=a.ESTADONIVEL,
                        DESESTADO=b.DESESTADONOTA
                        };
            lblEstadoNivel.Text = query.Select(x => x.DESESTADO).FirstOrDefault().ToString();


        }

        public async Task<List<ClNota>> ServicioGetAllNotas()
        {
            List<ClNota> compInf = new List<ClNota>();
            try
            {


                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/Notas");
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
        public async Task<List<ClUnidad>> ServicioGetTemas()
        {
            List<ClUnidad> compInf = new List<ClUnidad>();
            try
            {


                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/Unidades");
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
        public async Task<List<ClEstadoNota>> ServicioGetEstadoNota()
        {
            List<ClEstadoNota> compInf = new List<ClEstadoNota>();
            try
            {


                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/EstadoNota");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClEstadoNota>>(empResponse);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return compInf;

        }
        #endregion
        public async void CargarPromedio(long idInscrito, long idNivelEstudiante)
        {
            List<ClNota> notas = await ServicioGetNotas(idInscrito);
            List<ClEstadoNota> estadoNota = await ServicioGetEstadoNota();
            var queryPromedio = from a in notas
                                where a.IDINSCRITO == idInscrito && a.IDNIVELESTUDINTE == idNivelEstudiante && a.ESTADO == 3
                                select new
                                {
                                    Promedio = a.CALIFICACION,
                                };
            txtPromedio.Text = queryPromedio.Select(x => x.Promedio).FirstOrDefault().ToString();
        }
        public async void cargargridNotas(long idInscrito,long idNivelEstudiante) {
            List<ClNota> notas = await ServicioGetNotas(idInscrito);
            List<ClUnidad> temas = await ServicioGetTemas();
            
            var queryNotas = from a in notas 
                             join b in temas on a.IDTEMA equals b.idNomUnidad 
                             
                             where a.IDINSCRITO == idInscrito && a.IDNIVELESTUDINTE==idNivelEstudiante && a.ESTADO!=0
                             //diferente de 0 significa solo notas con estado 1 que es cerrado
                             select new {
                                 IdNota = a.IDNOTA,
                                 IdInscrito = a.IDINSCRITO,
                                 idNivel = a.IDNIVEL,
                                 idTema = a.IDTEMA,
                                 NomUnidad=b.NomUnidad,
                                 calificacion = a.CALIFICACION,
                                 Estado=a.ESTADO

                             };

            CargarPromedio(idInscrito, idNivelEstudiante);
            ExtraerEstatus(idNivelEstudiante);
            dgvNotas.DataSource = queryNotas;
            dgvNotas.DataBind();

            //lblPromedio.Visible=true;

        }

        protected void dgvInscrito_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvInscrito.PageIndex = e.NewPageIndex;
            cargarGridEstudiante();
        }

        public async void cargarComboPeriodo()
        {
            List<ClPeriodoInscripcion> periodos = await ServicioExtraerPeriodo();
            cbxPeriodo.DataSource = periodos;
            cbxPeriodo.DataValueField = "IdPeriodoInscripcion";
            cbxPeriodo.DataTextField = "Periodo";
            cbxPeriodo.DataBind();
        }

        public async void cargarComboNiveles()
        {
            List<ClNivel> niveles = await ServicioExtraerNIvel();
            var niveles_ = from a in niveles
                           where a.codNivel.Trim() != "SEK1203"
                           orderby a.descNivel ascending
                           select new
                           {
                               idNivel=a.idNivel,
                               nomNivel=a.nomNivel
                           };
            cbxNiveles.DataSource = niveles_;
            cbxNiveles.DataValueField = "idNivel";
            cbxNiveles.DataTextField = "nomNivel";
            cbxNiveles.DataBind();
        }



        protected void btnExccel_Click(object sender, EventArgs e)
        {
            DataSet lista = cargarDatosExcel();
            DataTable dt2 = lista.Tables[0];
            DateTime fecha = DateTime.Now;
            string fechaDocument = Convert.ToString(fecha);
            string nombreArchivo = "HistoricoNotasAlumnos" + "_" + fechaDocument;
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
        public async void BuscarEstudiante(string identificacion)
        {
            List<ClInscritoAutonomo> inscrito = new List<ClInscritoAutonomo>();
            List<ClTipoEstudiante> tipoEstudiante = new List<ClTipoEstudiante>();
            List<ClPeriodoInscripcion> periodo = new List<ClPeriodoInscripcion>();
            List<ClPrueba> prueba = new List<ClPrueba>();
            List<ClNivel> nivel = new List<ClNivel>();
            List<ClNivelesInscrito> nivelesIns = new List<ClNivelesInscrito>();
            List<ClEstadoEstudiante> estadoEstu = new List<ClEstadoEstudiante>();
            List<ClNota> notas = new List<ClNota>();
            nivelesIns = await ServicioGetNivelesInscritos();
            tipoEstudiante = await ServicioExtraerTipoEstudiante();
            inscrito = await ServicioExtraerInscrito();
            periodo = await ServicioExtraerPeriodo();
            prueba = await ServicioExtraerPrueba();
            nivel = await ServicioExtraerNIvel();
            estadoEstu = await ServicioGetEstadoEstudiante();
            notas = await ServicioGetAllNotas();
            var query = from a in inscrito
                        join b in nivelesIns on a.IdInscrito equals b.IDINSCRITO
                        join c in nivel on b.IDNIVEL equals c.idNivel
                        join d in tipoEstudiante on a.IdTipoEstudiante equals d.IdTipoEstudiante
                        join e in periodo on b.IDPERIODOINSCRIPCION equals e.IdPeriodoInscripcion
                        join f in estadoEstu on b.IDESTADONIVEL equals f.CodEstadoEstu
                        join g in notas on b.IDNIVELESTUDIANTE equals g.IDNIVELESTUDINTE
                        where b.PRUEBA == 0 && g.ESTADO == 3 && g.IDNIVEL == b.IDNIVEL && b.ESTADONIVEL == 1 && b.IDESTADONIVEL == 1 && a.NumDocInscrito.Trim()==identificacion
                        //0 son solo niveles,3 significa promedio,1 significa nivel cerrado,1 significa pagado
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
                            Promedio = g.CALIFICACION
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
            List<ClNota> notas = new List<ClNota>();
            nivelesIns = await ServicioGetNivelesInscritos();
            tipoEstudiante = await ServicioExtraerTipoEstudiante();
            inscrito = await ServicioExtraerInscrito();
            periodo = await ServicioExtraerPeriodo();
            prueba = await ServicioExtraerPrueba();
            nivel = await ServicioExtraerNIvel();
            estadoEstu = await ServicioGetEstadoEstudiante();
            notas = await ServicioGetAllNotas();
            var query = from a in inscrito
                        join b in nivelesIns on a.IdInscrito equals b.IDINSCRITO
                        join c in nivel on b.IDNIVEL equals c.idNivel
                        join d in tipoEstudiante on a.IdTipoEstudiante equals d.IdTipoEstudiante
                        join e in periodo on b.IDPERIODOINSCRIPCION equals e.IdPeriodoInscripcion
                        join f in estadoEstu on b.IDESTADONIVEL equals f.CodEstadoEstu
                        join g in notas on b.IDNIVELESTUDIANTE equals g.IDNIVELESTUDINTE
                        where b.PRUEBA == 0 && g.ESTADO == 3 && g.IDNIVEL == b.IDNIVEL && b.ESTADONIVEL == 1 && b.IDESTADONIVEL == 1 && b.IDPERIODOINSCRIPCION==idPeriodo
                        //0 son solo niveles,3 significa promedio,1 significa nivel cerrado,1 significa pagado
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
                            IdPeriodo= b.IDPERIODOINSCRIPCION,
                            Periodo=e.Periodo,
                            Promedio = g.CALIFICACION
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
            List<ClNota> notas = new List<ClNota>();
            nivelesIns = await ServicioGetNivelesInscritos();
            tipoEstudiante = await ServicioExtraerTipoEstudiante();
            inscrito = await ServicioExtraerInscrito();
            periodo = await ServicioExtraerPeriodo();
            prueba = await ServicioExtraerPrueba();
            nivel = await ServicioExtraerNIvel();
            estadoEstu = await ServicioGetEstadoEstudiante();
            notas = await ServicioGetAllNotas();
            var query = from a in inscrito
                        join b in nivelesIns on a.IdInscrito equals b.IDINSCRITO
                        join c in nivel on b.IDNIVEL equals c.idNivel
                        join d in tipoEstudiante on a.IdTipoEstudiante equals d.IdTipoEstudiante
                        join e in periodo on b.IDPERIODOINSCRIPCION equals e.IdPeriodoInscripcion
                        join f in estadoEstu on b.IDESTADONIVEL equals f.CodEstadoEstu
                        join g in notas on b.IDNIVELESTUDIANTE equals g.IDNIVELESTUDINTE
                        where b.PRUEBA == 0 && g.ESTADO == 3 && g.IDNIVEL == b.IDNIVEL && b.ESTADONIVEL == 1 && b.IDESTADONIVEL == 1 && b.IDNIVEL==idNivel
                        //0 son solo niveles,3 significa promedio,1 significa nivel cerrado,1 significa pagado
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
                            Promedio = g.CALIFICACION
                        };
            dgvInscrito.DataSource = query.ToList();
            dgvInscrito.DataBind();

        }
        public async void BuscarEstudiantexTodosParametros(long idNivel,long idperiodo,string identificacion)
        {
            List<ClInscritoAutonomo> inscrito = new List<ClInscritoAutonomo>();
            List<ClTipoEstudiante> tipoEstudiante = new List<ClTipoEstudiante>();
            List<ClPeriodoInscripcion> periodo = new List<ClPeriodoInscripcion>();
            List<ClPrueba> prueba = new List<ClPrueba>();
            List<ClNivel> nivel = new List<ClNivel>();
            List<ClNivelesInscrito> nivelesIns = new List<ClNivelesInscrito>();
            List<ClEstadoEstudiante> estadoEstu = new List<ClEstadoEstudiante>();
            List<ClNota> notas = new List<ClNota>();
            nivelesIns = await ServicioGetNivelesInscritos();
            tipoEstudiante = await ServicioExtraerTipoEstudiante();
            inscrito = await ServicioExtraerInscrito();
            periodo = await ServicioExtraerPeriodo();
            prueba = await ServicioExtraerPrueba();
            nivel = await ServicioExtraerNIvel();
            estadoEstu = await ServicioGetEstadoEstudiante();
            notas = await ServicioGetAllNotas();
            var query = from a in inscrito
                        join b in nivelesIns on a.IdInscrito equals b.IDINSCRITO
                        join c in nivel on b.IDNIVEL equals c.idNivel
                        join d in tipoEstudiante on a.IdTipoEstudiante equals d.IdTipoEstudiante
                        join e in periodo on b.IDPERIODOINSCRIPCION equals e.IdPeriodoInscripcion
                        join f in estadoEstu on b.IDESTADONIVEL equals f.CodEstadoEstu
                        join g in notas on b.IDNIVELESTUDIANTE equals g.IDNIVELESTUDINTE
                        where b.PRUEBA == 0 && g.ESTADO == 3 && g.IDNIVEL == b.IDNIVEL && b.ESTADONIVEL == 1 && b.IDESTADONIVEL == 1 && b.IDNIVEL == idNivel && b.IDPERIODOINSCRIPCION == idperiodo && a.NumDocInscrito.Trim()==identificacion
                        //0 son solo niveles,3 significa promedio,1 significa nivel cerrado,1 significa pagado
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
                            Promedio = g.CALIFICACION
                        };
            dgvInscrito.DataSource = query.ToList();
            dgvInscrito.DataBind();

        }
        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            string identificacion = txtCedula.Text.Trim();
            long idNivel = Convert.ToInt64(cbxNiveles.SelectedValue.ToString());
            long idPeriodo= Convert.ToInt64(cbxPeriodo.SelectedValue.ToString());
            if (txtCedula.Text.Trim() != "" && idPeriodo == 0 && idNivel == 0)//busca solo por identificacion
            {
                BuscarEstudiante(identificacion);
            }
            else if(idNivel==0 && identificacion.Trim()=="" && idPeriodo==0)//busca todos
            {
                cargarGridEstudiante();
            }
            else if (idNivel != 0 && idPeriodo==0)//busca solo por nivel
            {

                BuscarEstudianteXNivel(idNivel);
            }
            else if (idPeriodo != 0 && idNivel==0)//busca solo por periodo
            {
                BuscarEstudiantexPeriodo(idPeriodo);
            }
            else if(idPeriodo!=0 && idNivel!=0 && identificacion != "")//busca por todos las opciones
            {
                BuscarEstudiantexTodosParametros(idNivel,idPeriodo,identificacion);
            }
            
        }
    }
}