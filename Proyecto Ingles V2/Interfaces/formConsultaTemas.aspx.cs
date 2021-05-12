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
    public partial class ConsultaTemas : System.Web.UI.Page
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
                           
                            cargarTipoNivel();
                            cargarGridNiveles();
                         } 
                    }
        }
        public async Task<List<ClNivel>> ServicioGetNiveles()//cargar todos niveles 
        {
            List<ClNivel> compInf = new List<ClNivel>();
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
                    compInf = JsonConvert.DeserializeObject<List<ClNivel>>(empResponse);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async Task<List<ClTipoNivel>> ServicioGetTipoNivel()//cargar todos niveles 
        {
            List<ClTipoNivel> compInf = new List<ClTipoNivel>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/TipoNivel");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClTipoNivel>>(empResponse);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async Task<List<ClUnidad>> ServicioGetTemaNivel()//cargar todos niveles 
        {
            List<ClUnidad> compInf = new List<ClUnidad>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/Unidades");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClUnidad>>(empResponse);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async void llenarModal(int idNivel,string nomnivel)
        {


            List<ClUnidad> temaN = await ServicioGetTemaNivel();
            var query = from a in temaN
                        where a.idNivel == idNivel
                        select new
                        {
                            idNomUnidad=a.idNomUnidad,
                            idNivel=a.idNivel,
                            nomUnidad=a.NomUnidad,
                            codNomUnidad=a.codNomUnidad,
                            descNomUnidad=a.desNomUnidad,
                        };
            txtnivel.Text = nomnivel;
            dgvTemaUnidad.DataSource = query;
            dgvTemaUnidad.DataBind();
          //  cargarUnidades();

        }


        public async void cargarTipoNivel() {

            List<ClTipoNivel> tipoNivel = await ServicioGetTipoNivel();
            ddlTipoNIvel.DataSource = tipoNivel;
            ddlTipoNIvel.DataValueField = "idtipoNivel";
            ddlTipoNIvel.DataTextField = "descTipoNivel";
            ddlTipoNIvel.DataBind();
        }
        //public async void cargarNiveles() {

        //    List<ClNivel> niveles = await ServicioGetNiveles();
        //    ddlNivel.DataSource = niveles;
        //    ddlNivel.DataValueField = "idNivel";
        //    ddlNivel.DataTextField = "nomNivel";
        //    ddlNivel.DataBind();
        //}
        //public async void cargarNivelxTipo(int idTipoNivel)
        //{

        //    List<ClNivel> niveles = await ServicioGetNiveles();
        //    var query = from a in niveles
        //                where a.idTipoNivel == idTipoNivel
        //                select new {
        //                    idNivel=a.idNivel,
        //                    nomNivel=a.nomNivel,
        //                };
        //    ddlNivel.DataSource = query;
        //    ddlNivel.DataValueField = "idNivel";
        //    ddlNivel.DataTextField = "nomNivel";
        //    ddlNivel.DataBind();
        //}
        public async Task<List<ClCurso>> ServicioGetCurso()
        {
            List<ClCurso> compInf = new List<ClCurso>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/Curso");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClCurso>>(empResponse);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async Task<List<ClEstadoNivel>> ServicioGetEstadoNivel()
        {
            List<ClEstadoNivel> compInf = new List<ClEstadoNivel>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/EstadoNivel");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClEstadoNivel>>(empResponse);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return compInf;

        }
        public async Task<List<CLLibros>> ServicioGetLibros()//cargar niveles 
        {
            List<CLLibros> compInf = new List<CLLibros>();
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
                    compInf = JsonConvert.DeserializeObject<List<CLLibros>>(empResponse);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async void cargarGridNiveles()
        {
            List<ClNivel> niveles = new List<ClNivel>();
            List<ClCurso> curso = new List<ClCurso>();
            List<ClEstadoNivel> estNivel = new List<ClEstadoNivel>();
            List<ClTipoNivel> tipoNivel = new List<ClTipoNivel>();
            List<CLLibros> libros = new List<CLLibros>();
            niveles = await ServicioGetNiveles();
            curso = await ServicioGetCurso();
            estNivel = await ServicioGetEstadoNivel();
            tipoNivel = await ServicioGetTipoNivel();
            libros = await ServicioGetLibros();
            var query = from a in niveles
                        join b in curso on a.idCurso equals b.IdCurso
                        join c in estNivel on a.idEstadoNivel equals c.IdEstadoNivel
                        join d in tipoNivel on a.idTipoNivel equals d.idtipoNivel
                        join e in libros on a.idLibro equals e.idLibro
                        where a.codNivel.Trim().ToString()!="SEK1203"
                        orderby a.descNivel ascending
                        select new
                        {
                            idNivel = a.idNivel,
                            idEstadoNivel = a.idEstadoNivel,
                            estadoNivel = c.DescEstadoNivel,
                            idTipoNivel = a.idTipoNivel,
                            idCurso = a.idCurso,
                            nomCurso = b.DescCurso,
                            codNivel = a.codNivel.Trim(),
                            nomNivel = a.nomNivel,
                            descNivel = a.descNivel,
                            tipoNivel = d.descTipoNivel,
                            libro = e.descLibro,
                            CostoNivel = a.costoNIvel,
                        };
            dgvNivel.DataSource = query;
            dgvNivel.DataBind();
        }
        public async Task<List<ClTemaUnidad>> ServicioGetUnidades()
        {
            List<ClTemaUnidad> compInf = new List<ClTemaUnidad>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/TemasNiveles");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClTemaUnidad>>(empResponse);
                    return compInf;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        //public async void cargarUnidades(DropDownList list)
        //{
        //    List<ClTemaUnidad> temaUnidad = new List<ClTemaUnidad>();
        //    temaUnidad = await ServicioGetUnidades();
        //    var query = from a in temaUnidad
        //                group a by a.codTemaUnidad into b
        //                select new
        //                {
        //                    codNivel = b.Select(x => x.codTemaUnidad).FirstOrDefault(),
        //                };
        //    list.DataSource = query;
        //    list.DataValueField = "codNivel";
        //    list.DataTextField = "codNivel";
        //    list.DataBind();

        //}
        //public async void CargarTemas() {

        //    try
        //    {

        //        List<ClUnidad> compInf = new List<ClUnidad>();
        //        var client = new HttpClient();
        //        client.BaseAddress = new Uri(url);
        //        client.DefaultRequestHeaders.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
        //        HttpResponseMessage res = await client.GetAsync("api/Unidades/" + Convert.ToInt32(ddlNivel.SelectedValue.ToString()));
        //        if (res.IsSuccessStatusCode)
        //        {
        //            var empResponse = res.Content.ReadAsStringAsync().Result;
        //            compInf = JsonConvert.DeserializeObject<List<ClUnidad>>(empResponse);

        //        }
              
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }

        //}


        protected void ddlTipoNIvel_SelectedIndexChanged(object sender, EventArgs e)
        {

            //cargarNivelxTipo(Convert.ToInt32(ddlTipoNIvel.SelectedValue.ToString()));

        }

        protected void dgvTema_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar") {
                int fila = Convert.ToInt32(e.CommandArgument);

                int idregistro = Convert.ToInt32(dgvNivel.DataKeys[fila].Value);
                string nomnivel = dgvNivel.Rows[fila].Cells[4].Text.ToString();
                llenarModal(idregistro, nomnivel);
                btnPopUp_ModalPopupExtender2.Show();
            }
        }

        protected void dgvTemaUnidad_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    DropDownList ddlUnidades = (e.Row.FindControl("ddlUnidades") as DropDownList);
            //    cargarUnidades(ddlUnidades);

            //}


        }

        protected void dgvTemaUnidad_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "Guardar")
            //{
            //    int fila = Convert.ToInt32(e.CommandArgument);
            //    long idregistro = Convert.ToInt64(dgvTemaUnidad.DataKeys[fila].Value);
            //    DropDownList ddl = (DropDownList)dgvTemaUnidad.Rows[fila].Cells[0].FindControl("ddlUnidades");
            //    string usado = ddl.SelectedValue;
            //    string nivel=ddlUnidades.SelectedValue.ToString();

            //}
        }
    }
}