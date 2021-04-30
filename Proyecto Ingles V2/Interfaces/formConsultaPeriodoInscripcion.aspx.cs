using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Logica.Modelos;
using Logica;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Cfg;
using Logica.Conexion;
using System.Data.SqlClient;
using System.Data;
using Logica.Servicios;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using Logica.ConexionServicios;
namespace Proyecto_Ingles_V2.Interfaces
{
    public partial class ConsultaPeriodoInscripcion : System.Web.UI.Page
    {
        static conexionServidor cs = new conexionServidor();
        string url = cs.url.ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
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
                    CargarPeriodos();
                }
            }
        }
        #region Invocacion Servicios
        public async void ServicioExtraerPeriodosXid()
        {
            List<ClPeriodoInscripcion> compInf = new List<ClPeriodoInscripcion>();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/PeriodoInscripcion?idper=" + txtPeriodo.Text.ToString().Trim());
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClPeriodoInscripcion>>(empResponse);
                }
                dgvPeriodo.DataSource = compInf;
                dgvPeriodo.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
        public async void ServicioActualizarPeriodo(ClPeriodoInscripcion periodo) {
            try
            {
                string uri = "api/PeriodoInscripcion?idper=" + periodo.IdPeriodoInscripcion;
                var myContent = JsonConvert.SerializeObject(periodo);
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
            catch (Exception ex) {
                throw ex;           
            }

        }
        public async void ServicioEliminarPeriodo(int idPeriodo) {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.DeleteAsync("api/PeriodoInscripcion?idper=" + idPeriodo);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    CargarPeriodos();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Metodos
        public async void CargarPeriodos() {
            List<ClPeriodoInscripcion> periodos = new List<ClPeriodoInscripcion>();
            periodos=await ServicioExtraerPeriodos();
            dgvPeriodo.DataSource = periodos;
            dgvPeriodo.DataBind();
        }
        public async void llenarmodal(long idperiodoRegistro) {
            List<ClPeriodoInscripcion> periodos = new List<ClPeriodoInscripcion>();
            periodos = await ServicioExtraerPeriodos();
            DateTime fechaInicio = new DateTime();
            DateTime fechaFin = new DateTime();
            var query = from a in periodos
                        where a.IdPeriodoInscripcion == idperiodoRegistro
                        select new {
                            IDPERIODOINSCRIPCION = a.IdPeriodoInscripcion,
                            PERIODO = a.Periodo,
                            ANOLECTIVO = a.AnoLectivo,
                            CODPERIODOINSCRIPCION = a.CodPeriodoInscripcion,
                            FECHAINICIO = a.FechaInicio,
                            FECHAFIN = a.FechaFin,
                            ESTADOPERIODO = a.EstadoPeriodo,
                        };
            txtId.Text = query.Select(x => x.IDPERIODOINSCRIPCION).FirstOrDefault().ToString();
            txtPeriodoM.Text = query.Select(x => x.PERIODO).FirstOrDefault().Trim().ToString();
            txtAnoLectivo.Text = query.Select(x => x.ANOLECTIVO).FirstOrDefault().ToString();
            fechaInicio = query.Select(x => x.FECHAINICIO).FirstOrDefault();
            fechaInicio = fechaInicio.Date;
            fechaFin= query.Select(x => x.FECHAFIN).FirstOrDefault();
            fechaFin = fechaFin.Date;
            txtFechaInicio.Text = fechaInicio.ToString("yyyy-MM-dd");
            txtFechaFin.Text = fechaFin.ToString("yyyy-MM-dd");
            if (query.Select(x => x.ESTADOPERIODO).FirstOrDefault() == 1)
            {
                RabActivo.Checked = true;
            }
            else
                RabActivo.Checked = false;
        }
        #endregion

        #region Metodos Forms
        protected void btnConsulta_Click(object sender, EventArgs e)
        {
            if (txtPeriodo.Text.ToString().Trim() != "")
            {
                long identificacion = Convert.ToInt64(txtPeriodo.Text.Trim().ToString());
                ServicioExtraerPeriodosXid();
            }
            else
            {
                CargarPeriodos();
            }
        }
        protected void dgvPeriodo_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int fila = Convert.ToInt32(e.CommandArgument);
                long idregistro = Convert.ToInt64(dgvPeriodo.DataKeys[fila].Value);
                llenarmodal(idregistro);
                btnPopUp_ModalPopupExtender.Show();
            }
        }
        protected void dgvPeriodo_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            
        }
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            ClPeriodoInscripcion periodo = new ClPeriodoInscripcion();
            int activo = 0;
            periodo.IdPeriodoInscripcion = Convert.ToInt32(txtId.Text);
            periodo.Periodo = txtPeriodoM.Text.ToUpper().Trim();
            periodo.AnoLectivo = Convert.ToInt32(txtAnoLectivo.Text);
            periodo.CodPeriodoInscripcion = "";
            periodo.FechaInicio = Convert.ToDateTime(txtFechaInicio.Text);
            periodo.FechaFin = Convert.ToDateTime(txtFechaFin.Text);
            if (RabActivo.Checked == true)
            {
                activo = 1;
            }
            else {
                activo = 0;
            }
            periodo.EstadoPeriodo = activo;
            ServicioActualizarPeriodo(periodo);
        }
        protected void dgvPeriodo_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int idPeriodo = Convert.ToInt32(dgvPeriodo.DataKeys[e.RowIndex].Values[0].ToString());
            ServicioEliminarPeriodo(idPeriodo);
        }
        #endregion


    }
}