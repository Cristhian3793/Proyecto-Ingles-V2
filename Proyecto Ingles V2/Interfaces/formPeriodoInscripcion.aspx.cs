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
using Logica.Modelos;
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
    public partial class formPeriodoInscripcion : System.Web.UI.Page
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
        protected  void Button1_Click(object sender, EventArgs e)
        {
            InsertarPeriodo();
        }
        #region Invocacion Servicios
        public async void ServicioInsertarPeriodo(ClPeriodoInscripcion per) {
            string uri = "api/PeriodoInscripcion";
            try {
                var myContent = JsonConvert.SerializeObject(per);
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
            } catch (Exception ex) {           
                throw ex;
            }


        }
        #endregion

        #region Metodos
        public void InsertarPeriodo()
        {
            DateTime fecha = DateTime.Now;
            ClPeriodoInscripcion per = new ClPeriodoInscripcion();
            int activo = 0;
            //per.CodPeriodoInscripcion = txtCodigoPeriodo.Text.ToUpper().Trim();
            per.Periodo = txtPeriodo.Text.ToUpper().Trim();
            per.AnoLectivo = Convert.ToInt32(txtAnoLectivo.Text);
            per.FechaInicio = Convert.ToDateTime(txtFechaInicio.Text);
            per.FechaFin = Convert.ToDateTime(txtFechaFin.Text);
            if (RabActivo.Checked == true)
            {
                activo = 1;
            }
            else
                activo = 0;
            per.EstadoPeriodo = activo;
            if (Convert.ToDateTime(txtFechaFin.Text) < Convert.ToDateTime(txtFechaInicio.Text))
            {
                string script = @"<script type='text/javascript'>
                                alert('No se pudo procesar la solicitud la fecha no debe ser menor a la fecha inicial');
                                </script>";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
            }
            else {

                ServicioInsertarPeriodo(per);
                string script = "confirm();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
            }
        }
        public void limpiarCampos()
        {
            txtAnoLectivo.Text = "";
            txtPeriodo.Text = "";
            txtFechaInicio.Text = "";
            txtFechaFin.Text = "";
        }
        #endregion


    }
}