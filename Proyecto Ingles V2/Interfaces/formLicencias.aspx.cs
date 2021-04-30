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

namespace Proyecto_Ingles_V2.Interfaces
{
    public partial class formLicencias : System.Web.UI.Page
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
                    CargarLibros();
                }

            }

        }
        #region Invocacion Servicios
        public async void ServicioInsertarLicencia(ClLicenciaCambridge licencia) {

            string uri = "api/LicenciasCambridge";
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
                limpiarCampos();
                string script = "confirm();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
                
            }
            else
            {
                string script = @"<script type='text/javascript'>
                                alert('No se pudo procesar la solicitud');
                                </script>";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                limpiarCampos();
            }
        }
        public async Task<List<CLLibros>> ServicioExtraerLibros() {
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
        #endregion

        #region Metodos
        public void limpiarCampos() {
            txtLicencia.Text = "";
        }

        public  async void CargarLibros() {
            List<CLLibros> libros = new List<CLLibros>();
            libros = await ServicioExtraerLibros();
            cbxLibros.DataSource =libros;
            cbxLibros.DataValueField = "idLibro";
            cbxLibros.DataTextField = "descLibro";
            cbxLibros.DataBind();

        }

        
        #endregion
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ClLicenciaCambridge licencia = new ClLicenciaCambridge();
            int activo = 0;
            DateTime fecha = DateTime.Now;
            //licencia.codLibro = txtCodLibro.Text;
            licencia.Licencia = txtLicencia.Text;
            if (RabActivo.Checked == true)
            {
                activo = 1;
            }
            else
            {
                activo = 0;
            }
            licencia.IdLibro = Convert.ToInt64(cbxLibros.SelectedValue.ToString());
            licencia.EstadoLicencia = activo;
            licencia.FechaEmision = fecha;

            ServicioInsertarLicencia(licencia);

        }
    }
}