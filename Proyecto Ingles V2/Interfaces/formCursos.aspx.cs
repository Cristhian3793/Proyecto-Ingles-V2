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

namespace Proyecto_Ingles_V2.Interfaces
{
   
    public partial class formCursos : System.Web.UI.Page
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
                
            }
        }
        protected async void Button1_Click(object sender, EventArgs e)
        {
            #region Insertar Curso
            DateTime fecha = DateTime.Now;
            ClCurso curso = new ClCurso();
            curso.CodCurso = txtCodCurso.Text;
            curso.DescCurso = txtDescCurso.Text;
            curso.FechaCreacionCurso = Convert.ToString(fecha);
            string uri = "api/Curso";
            var myContent = JsonConvert.SerializeObject(curso);
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

            }
            else {
                string script = @"<script type='text/javascript'>
                                alert('No se pudo procesar la solicitud');
                                </script>";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
            }
            limpiarCampos();
            #endregion
        }
        public void limpiarCampos() {
            txtCodCurso.Text = "";
            txtDescCurso.Text = "";
        }
        
    }
}