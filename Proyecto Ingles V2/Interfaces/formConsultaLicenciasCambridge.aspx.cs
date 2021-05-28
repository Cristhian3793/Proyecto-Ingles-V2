using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica.Modelos;
using Newtonsoft;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Cfg;
using Logica.Conexion;
using Logica;
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
using System.Drawing;

namespace Proyecto_Ingles_V2.Interfaces
{
    public partial class formConsultaLicenciasCambridge : System.Web.UI.Page
    {
        static conexionServidor cs = new conexionServidor();
        string url = cs.url.ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                if (Session["usuario"] == null || (string)Session["usuario"] == "" )
                {

                    Response.Redirect("../Login/formLogin.aspx");
                }
                if((int)Session["TipoUser"] == 1)
                {
                    Response.Redirect("../Interfaces/Default.aspx");
                }
                else
                {
                    cargarGridLicencias();
                }
            }
        }
        #region Invocacion Servicios
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
        public async void ServicioEliminarLicencias(long idlicencia) {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.DeleteAsync("api/LicenciasCambridge/" + idlicencia);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;

                    cargarGridLicencias();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<ClLicenciaCambridge>> ServicioGetLicenciaXid(long idLicencia) {
            List<ClLicenciaCambridge> licencia = new List<ClLicenciaCambridge>();
            try
            {
                
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/LicenciasCambridge?codigo=" + idLicencia);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    licencia = JsonConvert.DeserializeObject<List<ClLicenciaCambridge>>(empResponse);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return licencia;
        }
        //public async void ServicioActualizarLicencia() {
        //    ClLicenciaCambridge licencia = new ClLicenciaCambridge();
        //    //licencia.IdLicencia =
        //    //licencia.IdLibro =
        //    //licencia.Licencia=
        //    //licencia.EstadoLicencia=

        //}
        #endregion

        #region Metodos
        public async void cargarGridLicencias() {
            List<CLLibros> libros = new List<CLLibros>();
            List<ClLicenciaCambridge> licencias = new List<ClLicenciaCambridge>();
            libros = await ServicioExtraerLibros();
            licencias = await ServicioExtraerLicencias();
            var query = from a in libros
                        join b in licencias on a.idLibro equals b.IdLibro

                        select new
                        {
                            IdLicencia = b.IdLicencia,
                            codLibro=a.codLibro,
                            Licencia=b.Licencia,
                            EstadoLicencia=b.EstadoLicencia,
                            IdLibro=a.idLibro,
                            Libro=a.descLibro,
                        };
            dgvLicencias.DataSource = query;
            dgvLicencias.DataBind();       
        }
        public async void cargarGridLicenciasXLibro(string nomlibro) {
            List<CLLibros> libros = new List<CLLibros>();
            List<ClLicenciaCambridge> licencias = new List<ClLicenciaCambridge>();
            libros = await ServicioExtraerLibros();
            licencias = await ServicioExtraerLicencias();
            var query = from a in libros
                        join b in licencias on a.idLibro equals b.IdLibro
                        where a.descLibro.Trim().ToString()==nomlibro.Trim().ToString()
                        select new
                        {
                            IdLicencia = b.IdLicencia,
                            codLibro = a.codLibro,
                            Licencia = b.Licencia,
                            EstadoLicencia = b.EstadoLicencia,
                            IdLibro = a.idLibro,
                            Libro = a.descLibro,

                        };

            dgvLicencias.DataSource = query;
            dgvLicencias.DataBind();
        }
        public async void cargarComboLibros() {
            List<CLLibros> libros = new List<CLLibros>();
            libros = await ServicioExtraerLibros();
            cbxLibros.DataSource = libros;
            cbxLibros.DataValueField = "idLibro";
            cbxLibros.DataTextField = "descLibro";
            cbxLibros.DataBind();
        }
        public async void llenarModal(long idlicencia) {
            int idlib;
            int activo;
            List<ClLicenciaCambridge> licencia = new List<ClLicenciaCambridge>();
            licencia = await ServicioGetLicenciaXid(idlicencia);
            var query = from a in licencia select new { 
            Idlicencia=a.IdLicencia,
            Idlibro=a.IdLibro,
            licencia=a.Licencia,
            activo=a.EstadoLicencia,
            };
            idlib = Convert.ToInt32(query.Select(x => x.Idlibro).FirstOrDefault().ToString());
            cargarComboLibros();
            cbxLibros.SelectedValue = idlib.ToString();
            txtLicencia.Text = query.Select(x=>x.licencia).FirstOrDefault().ToString();
            activo= Convert.ToInt32(query.Select(x => x.activo).FirstOrDefault().ToString());
            if (activo== 1)
            {
                RabActivo.Checked = true;
            }
            else
                RabActivo.Checked = false;
                
        }
        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
            string nombreLibro = txtLibro.Text;
            if (txtLibro.Text.Trim().ToString() != "")
            {
                cargarGridLicenciasXLibro(nombreLibro);
            }
            else {
                cargarGridLicencias();
            }           
        }

        protected void dgvLicencias_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int _estado = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "EstadoLicencia").ToString());

                if (_estado == 0 )
                    e.Row.BackColor = System.Drawing.Color.FromArgb(234, 127, 87);
            }
        }

        protected void dgvLicencias_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int idLicencia = Convert.ToInt32(dgvLicencias.DataKeys[e.RowIndex].Values[0].ToString());
            ServicioEliminarLicencias(idLicencia);
        }

        protected void dgvLicencias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int fila = Convert.ToInt32(e.CommandArgument);
                long idlicencia = Convert.ToInt64(dgvLicencias.DataKeys[fila].Value);
                llenarModal(idlicencia);
                btnPopUp_ModalPopupExtender.Show();
            }
        }
    }
}