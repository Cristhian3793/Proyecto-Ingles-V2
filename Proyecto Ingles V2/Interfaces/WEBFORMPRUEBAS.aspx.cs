using Logica.ConexionServicios;
using Logica.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using ClosedXML.Excel;
using System.Data;
using System.Reflection;
using System.Text;

namespace Proyecto_Ingles_V2.Interfaces
{
    public partial class WEBFORMPRUEBAS : System.Web.UI.Page
    {
        static conexionServidor cs = new conexionServidor();
        string url = cs.url.ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {

                updateMatricula();


            }
        }
        public async Task<List<ClMatricula>> ServicioGetEstadoEstudiante(string numdoc, string codProducto)
        {
            List<ClMatricula> compInf = new List<ClMatricula>();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/ActualizarMatriculas?numdoc="+numdoc+"&codProducto="+codProducto);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClMatricula>>(empResponse);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return compInf;
        }

        public async void ActualizarEstadoMatricula(long idNivelEstudiante,int idEstado)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ClNivelesInscrito nivelesIns = new ClNivelesInscrito();
                nivelesIns.IDESTADONIVEL = idEstado;
                string uri = "api/ActualizarMatriculas?idNivel="+idNivelEstudiante;
                var myContent = JsonConvert.SerializeObject(nivelesIns);
                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                var method = new HttpMethod("PATCH");
                var request = new HttpRequestMessage(method, uri)
                {
                    Content = stringContent
                };
                response = await client.SendAsync(request);

            }
            catch (TaskCanceledException e)
            {
            }
        }
        public async void updateMatricula()//mandar a llamar servicios el primero para buscar el nivel que debe matricular y el segundo para actualizar el estado de nivel
        {

            int estado = 0;
            List<ClMatricula> matricula = await ServicioGetEstadoEstudiante("1721441895", "SEK1335");//recupero registro a pagar añadir periodo
            long IDNIVELESTUDIANTE = matricula.Select(x => x.IDNIVELESTUDIANTE).FirstOrDefault();//este es el que se envia para actualizar 
            long IDINSCRITO = matricula.Select(x => x.IDINSCRITO).FirstOrDefault();
            string NUMDOCINSCRITO = matricula.Select(x => x.NUMDOCINSCRITO).FirstOrDefault();
            long IDNIVEL = matricula.Select(x => x.IDNIVEL).FirstOrDefault();
            int IDESTADONIVEL = matricula.Select(x => x.IDESTADONIVEL).FirstOrDefault();
            string NOMNIVEL = matricula.Select(x => x.NOMNIVEL).FirstOrDefault();
            int PRUEBA = matricula.Select(x => x.PRUEBA).FirstOrDefault();
            //llamar servicio actualizar Estado Nivel

            if (PRUEBA == 1)//es prueba
            {
                estado = 2;//significa prueba pagada
                ActualizarEstadoMatricula(IDNIVELESTUDIANTE,estado);

            }
            else//es nivel
            {
                estado = 1;//significa matriculado
                ActualizarEstadoMatricula(IDNIVELESTUDIANTE,estado);
            }
        }
        protected void btnExccel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable("GridView_Data");
            foreach (TableCell cell in GridView1.HeaderRow.Cells)
            {
                dt.Columns.Add(cell.Text);
            }
            foreach (GridViewRow row in GridView1.Rows)
            {
                dt.Rows.Add();
                for (int i = 0; i < row.Cells.Count; i++)
                {
                   
                    dt.Rows[dt.Rows.Count - 1][i] = row.Cells[i].Text;
                    dt.Rows[dt.Rows.Count - 1][i] = row.Cells[i].Text.Replace("&nbsp;", "N.A");
                }
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=GV.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            //Session["pp"] = "Hola";
            //Response.Redirect("Default.aspx");
        }
    }
}