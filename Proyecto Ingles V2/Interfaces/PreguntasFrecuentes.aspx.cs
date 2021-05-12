using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica.Servicios;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using Logica.ConexionServicios;
using Logica.Modelos;
using System.IO;
using System.Data.Sql;
using System.Data;

namespace Proyecto_Ingles_V2.Interfaces
{
    public partial class PreguntasFrecuentes : System.Web.UI.Page
    {
        static conexionServidor cs = new conexionServidor();
        string url = cs.url.ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet dataSet = new DataSet(); 
            LoadTextBox();
        }
        #region Servicios
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
        private async void LoadTextBox()
        {
            string idquestion = "idquestion";
            string contet = "<div class=\"content\"><div>";
            string content2 = "<div class=\"container\" style=\"padding-top: 100px\">";
            string content3 = "<div class=\"centerplease\"><p>PREGUNTAS FRECUENTES</p></div>";
            Response.Write(contet);
            Response.Write(content2);
            Response.Write(content3);
            List<ClPreguntasRespuestas> preguntas = await ServicioGetPreguntasRespuestas();
            int i = 0;
            foreach (ClPreguntasRespuestas cl in preguntas) {

                string Pregunta;
                string Respuesta;
                Pregunta = cl.PREGUNTA;
                Respuesta = cl.RESPUESTA;
                idquestion = idquestion + i;
                string html = "<div><input type=\"checkbox\" id=\"" + idquestion + "\"class=\"questions\"><div class=\"plus\">+</div><label for=\"" + idquestion + "\" class=\"question\">"+Pregunta+"</label><div class=\"answers\">"+Respuesta+"</div></div>";
                Response.Write(html);
                idquestion = "idquestion";
                i++;           
            }
            string fin = "</div></div></div>";
            Response.Write(fin);

            //for (int i = 0; i < 3; i++)
            //{
            //        idquestion = idquestion+i;
            //        string html= "<div><input type=\"checkbox\" id=\""+idquestion+"\"class=\"questions\"><div class=\"plus\">+</div><label for=\""+idquestion+"\" class=\"question\">Costos de niveles</label><div class=\"answers\">HIGH BEGGINER $100</div></div>";
            //        Response.Write(html);
            //        idquestion = "idquestion";
            //}

        }
        #region Metodos
        public async void crearElementosDinamicos() { 
         List<ClPreguntasRespuestas> preguntas= await ServicioGetPreguntasRespuestas();
            


        }


        #endregion

    }
}