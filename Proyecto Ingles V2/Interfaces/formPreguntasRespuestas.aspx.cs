using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica.Modelos;
using Logica.ConexionServicios;
using System.Threading.Tasks;

namespace Proyecto_Ingles_V2.Interfaces
{
    public partial class formPreguntasRespuestas : System.Web.UI.Page
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
                    cargarGrid();
                }
            }


  
        }
        #region servicios
        public async void ServicioInsertarPreguntaRespuesta(ClPreguntasRespuestas cl)
        {

            string uri = "api/PreguntasRespuestas";
            try
            {
                var myContent = JsonConvert.SerializeObject(cl);
                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.PostAsync(uri, stringContent);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    string script = "confirm();";


                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
                    limpiarCampos();
                    cargarGrid();
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<ClPreguntasRespuestas>> ServicioGetPreguntasRespuestas()//cargar todos niveles 
        {
            List<ClPreguntasRespuestas> compInf = new List<ClPreguntasRespuestas>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/PreguntasRespuestas");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClPreguntasRespuestas>>(empResponse);
                   
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return compInf;
        }

        #endregion

        #region metodos
        public void limpiarCampos() {
            txtpregunta.Text = "";
            txtRespuesta.Text = "";
        
        }

        public  void InsertPreguntaResp() {
            ClPreguntasRespuestas cl = new ClPreguntasRespuestas();
            cl.PREGUNTA = txtpregunta.Text.Trim().ToString().ToUpper();
            cl.RESPUESTA = txtRespuesta.Text.Trim().ToString().ToUpper();
            ServicioInsertarPreguntaRespuesta(cl);

        }
        public async void cargarGrid() {
           GridView1.DataSource=await ServicioGetPreguntasRespuestas();
           GridView1.DataBind();
        }
        public async Task<List<ClPreguntasRespuestas>> ServicioGetPreguntasXid(int id)
        {
            List<ClPreguntasRespuestas> pregunta = new List<ClPreguntasRespuestas>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/PreguntasRespuestas/" + id);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    pregunta = JsonConvert.DeserializeObject<List<ClPreguntasRespuestas>>(empResponse);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return pregunta;
        }
        public async void cargarPreguntasxId(int id) {

            List<ClPreguntasRespuestas> preguntas=await ServicioGetPreguntasXid(id);
            var query = from a in preguntas select new {
                ID=a.IDPREGUNTA,
                Pregunta = a.PREGUNTA,
                Respuesta=a.RESPUESTA,
            
            };
            idPre.Value= query.Select(x => x.ID).FirstOrDefault().ToString();
            txtPre.Text = query.Select(x => x.Pregunta).FirstOrDefault().ToString();
            txtResp.Text = query.Select(x => x.Respuesta).FirstOrDefault().ToString();

        }
        #endregion

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            InsertPreguntaResp();
            
        }
        public  void updatePregutas() {

            ClPreguntasRespuestas pre = new ClPreguntasRespuestas();
            pre.IDPREGUNTA = Convert.ToInt32(idPre.Value);
            pre.PREGUNTA = txtPre.Text.Trim().ToUpper();
            pre.RESPUESTA = txtResp.Text.Trim().ToUpper();
            actualizarPreguntas(pre);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            updatePregutas();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            System.Text.Encoding utf_8 = System.Text.Encoding.UTF8;
            if (e.CommandName=="Editar"){
                int fila = Convert.ToInt32(e.CommandArgument);
                int id = Convert.ToInt32(GridView1.DataKeys[fila].Value);
                cargarPreguntasxId(id);
                btnPopUp_ModalPopupExtender2.Show();
            }
        }
        public async void ServicioEliminarRegistro(int id)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.DeleteAsync("api/PreguntasRespuestas/" + id);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    
                }
                cargarGrid();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

       public async void actualizarPreguntas(ClPreguntasRespuestas pru)
        {
            try
            {
                string uri = "api/PreguntasRespuestas/" + pru.IDPREGUNTA;
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
                    cargarGrid();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}