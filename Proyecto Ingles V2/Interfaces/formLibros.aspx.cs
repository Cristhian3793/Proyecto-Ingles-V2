using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
using Logica.Modelos;
namespace Proyecto_Ingles_V2.Interfaces
{
    public partial class formLibros : System.Web.UI.Page
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
                    
                }

            }
        }
        #region Invocacion Servicios
        public async void ServicioInsertarLibro(CLLibros libros) {
            string uri = "api/Libros";
            var myContent = JsonConvert.SerializeObject(libros);
            var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
            HttpResponseMessage res = await client.PostAsync(uri, stringContent);
            if (res.IsSuccessStatusCode)
            {
                var empResponse = res.Content.ReadAsStringAsync().Result;
                string script = @"<script type='text/javascript'>
                                alert('Añadido Correctamente');
                                </script>";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                limpiarCampos();
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
        #endregion

        #region Metodos
        public void limpiarCampos()
        {
            txtCodLibro.Text = "";
            txtLibro.Text = "";
        }
        #endregion


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            CLLibros libros = new CLLibros();
            libros.codLibro = txtCodLibro.Text;
            libros.descLibro = txtLibro.Text;
            ServicioInsertarLibro(libros);
        }
    }
}