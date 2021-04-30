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
    public partial class formConsultaCursos : System.Web.UI.Page
    {
        static conexionServidor cs = new conexionServidor();
        string url = cs.url.ToString();
        protected async void Page_Load(object sender, EventArgs e)
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
                    await CargarCursosAsync();
                }

            }

        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected async void btnConsultar_Click(object sender, EventArgs e)
        {
            #region Consultar Cursos
            if (txtCodCurso.Text.ToString().Trim() != "")
            {
                await CargarGridCursosXCodAsync();

            }
            else
            {
                await CargarCursosAsync();
            }          
            #endregion
        }
        public async Task CargarGridCursosXCodAsync()//cargar curso 
        {
            try
            {               
                List<ClCurso> cur = new List<ClCurso>();
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/Curso?codigo=" + txtCodCurso.Text.ToString().Trim());
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    cur = JsonConvert.DeserializeObject<List<ClCurso>>(empResponse);

                }
                dgvCursos.DataSource = cur;
                dgvCursos.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task CargarCursosAsync()//cargar todos inscritos
        {
            try
            {
                List<ClCurso> cur = new List<ClCurso>();
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/Curso");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    cur = JsonConvert.DeserializeObject<List<ClCurso>>(empResponse);

                }

                dgvCursos.DataSource = cur;
                dgvCursos.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void dgvCursos_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dgvCursos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCursos.PageIndex = e.NewPageIndex;
        }
    }
}