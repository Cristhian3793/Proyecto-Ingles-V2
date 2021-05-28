using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
using Logica.Modelos;
namespace Proyecto_Ingles_V2.Interfaces
{
    public partial class formConsultaLicenciasCambridge_ : System.Web.UI.Page
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
                    cargarGridLicencias();
                }
            }
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
        public async Task<List<ClNivel>> ServicioExtraerNiveles()
        {
            List<ClNivel> lib = new List<ClNivel>();
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
                    lib = JsonConvert.DeserializeObject<List<ClNivel>>(empResponse);

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return lib;
        }
        public async void cargarGridLicencias() {
            List<CLLibros> libros = await ServicioExtraerLibros();
            List<ClNivel> niveles = await ServicioExtraerNiveles();
            string[] lista = { "SEK1203", "SEK1074", "SEK1205" };//codigos de productos que no son niveles
            var query = from a in libros
                         join b in niveles on a.idLibro equals b.idLibro
                         where !lista.Contains(b.codNivel.Trim())
                        select new
                         {

                             IDNIVEL=b.idNivel,
                             IDLIBRO = a.idLibro,
                             NOMNIVEL =b.nomNivel,
                             CODNIVEL=b.codNivel,
                             NOMLIBRO=a.descLibro

                         };
            dgvNiveles.DataSource = query;
            dgvNiveles.DataBind();
        }

        protected void dgvNiveles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerLicencias") {
                int fila = Convert.ToInt32(e.CommandArgument);//captura el indice de la fila del gridview
                long idLibro = Convert.ToInt64(dgvNiveles.DataKeys[fila]["IDLIBRO"].ToString());
                string nivel = dgvNiveles.Rows[fila].Cells[2].Text;
                string libro = dgvNiveles.Rows[fila].Cells[4].Text;
                llenarModal(idLibro,nivel,libro);
                btnPopUp_ModalPopupExtender.Show();
            
            }
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
        public async void llenarModal(long idLibro,string nivel,string libro) {
            List<ClLicenciaCambridge> licencias = await ServicioExtraerLicencias();
            var licencias_ = from a in licencias where a.IdLibro == idLibro 
             select new {
                IDLICENCIA=a.IdLicencia,
                LICENCIA=a.Licencia,
                ESTADO=a.EstadoLicencia,
            };
            txtLibro_.Text = libro;
            txtNivel.Text = nivel;
            dgvLicencias.DataSource = licencias_;
            dgvLicencias.DataBind();

        }
    }
}