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
using NHibernate;
using FluentNHibernate;
using Logica.Conexion;

namespace Proyecto_Ingles_V2.Interfaces
{
    public partial class formConsultaNivelesProgramado : System.Web.UI.Page
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
                if (!IsPostBack) {
                    cargarGridNiveles();
                    cargarCbxSearcTipoNiveles();
                }
            }
                
        }

        #region Invocar Servicios
        public async void ServicioActualizarNivel(ClNivel nivel)
        {

            try
            {
                string uri = "api/Niveles?idnivel=" + nivel.idNivel;
                var myContent = JsonConvert.SerializeObject(nivel);
                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.PutAsync(uri, stringContent);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    cargarGridNiveles();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<ClTipoNivel>> ServicioGetTipoNivel()
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
        public async void cargarCbxSearcTipoNiveles() {
            List<ClTipoNivel> niveles = await ServicioGetTipoNivel();
            cbxSearchNiveles.DataSource = niveles;
            cbxSearchNiveles.DataValueField = "idtipoNivel";
            cbxSearchNiveles.DataTextField = "descTipoNivel";
            cbxSearchNiveles.DataBind();
        }
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
        public async Task<List<ClNivel>> ServicioGetNivelesXCod()//cargar niveles 
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
       

        public async void CargarNivelesx(long idNivel) {

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
                        where a.idNivel==idNivel
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
            dgvNiveles.DataSource = query.ToList();
            dgvNiveles.DataBind();

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
        public async void ServicioEliminarNivel(long id) {

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.DeleteAsync("api/Niveles/" + id);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                }
                cargarGridNiveles();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        #endregion

        #region Metodos
        public async void llenarmodal(long idNivel)
        {
            List<ClNivel> niveles = new List<ClNivel>();
            List<ClCurso> curso = new List<ClCurso>();
            List<ClEstadoNivel> estNivel = new List<ClEstadoNivel>();
            List<ClTipoNivel> tipoNivel = new List<ClTipoNivel>();
            List<CLLibros> libro = new List<CLLibros>();
            int nivel;
            int estadoNivel;
            int idlibro;
            int idCurso;
            int idTipNiv;
            niveles = await ServicioGetNiveles();
            niveles = niveles.Where(x => x.idNivel == idNivel).ToList();
            curso = await ServicioGetCurso();
            estNivel = await ServicioGetEstadoNivel();
            tipoNivel = await ServicioGetTipoNivel();
            libro = await ServicioGetLibros();
            var query = from a in niveles
                        join b in curso on a.idCurso equals b.IdCurso
                        join c in estNivel on a.idEstadoNivel equals c.IdEstadoNivel
                        join d in tipoNivel on a.idTipoNivel equals d.idtipoNivel
                        join e in libro on a.idLibro equals e.idLibro
                        where a.idNivel == idNivel
                        select new
                        {
                            IdNIvel = a.idNivel,
                            CodNivel = a.codNivel,
                            NomNIvel = a.nomNivel,
                            DescNivel = a.descNivel,
                            IdEstadoNivel = a.idEstadoNivel,
                            IdLIbro = a.idLibro,
                            IdCurso=a.idCurso,
                            IdtTipoNivel=a.idTipoNivel,
                            CostoNivel=a.costoNIvel,
                        };
            txtIdNivel.Text = query.Select(x => x.IdNIvel).FirstOrDefault().ToString();
            txtCodNivel.Text = query.Select(x => x.CodNivel).FirstOrDefault().ToString();
            txtNomNivel.Text = query.Select(x => x.NomNIvel).FirstOrDefault().ToString();
            txtCostoNivel.Text = query.Select(x => x.CostoNivel).FirstOrDefault().ToString();
            nivel = Convert.ToInt32(query.Select(x => x.DescNivel).FirstOrDefault().ToString());
            estadoNivel = Convert.ToInt32(query.Select(x => x.IdEstadoNivel).FirstOrDefault().ToString());
            idlibro = Convert.ToInt32(query.Select(x => x.IdLIbro).FirstOrDefault().ToString());
            cbxNivel.SelectedValue = nivel.ToString();
            idCurso = Convert.ToInt32(query.Select(x => x.IdCurso).FirstOrDefault().ToString());
            idTipNiv= Convert.ToInt32(query.Select(x => x.IdtTipoNivel).FirstOrDefault().ToString());
            //cargar combo estado nivel
            cbxEstadoNivel.DataSource = estNivel;
            cbxEstadoNivel.DataValueField = "IdEstadoNivel";
            cbxEstadoNivel.DataTextField = "DescEstadoNivel";
            cbxEstadoNivel.DataBind();
            cbxEstadoNivel.SelectedValue = estadoNivel.ToString();
            cbxLibros.DataSource = libro;
            cbxLibros.DataValueField = "idLibro";
            cbxLibros.DataTextField = "descLibro";
            cbxLibros.DataBind();
            cbxLibros.SelectedValue = idlibro.ToString();
           
            cbxCursos.DataSource = curso;
            cbxCursos.DataValueField = "IdCurso";
            cbxCursos.DataTextField = "DescCurso";
            cbxCursos.DataBind();
            cbxCursos.SelectedValue = idCurso.ToString();
            idTipoNivel.Value = idTipNiv.ToString();

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
                        orderby a.descNivel ascending
                        select new
                        {
                            idNivel = a.idNivel,
                            idEstadoNivel = a.idEstadoNivel,
                            estadoNivel = c.DescEstadoNivel,
                            idTipoNivel = a.idTipoNivel,
                            idCurso = a.idCurso,
                            nomCurso = b.DescCurso,
                            codNivel = a.codNivel,
                            nomNivel = a.nomNivel,
                            descNivel = a.descNivel,
                            tipoNivel = d.descTipoNivel,
                            libro = e.descLibro,
                            CostoNivel = a.costoNIvel,
                        };
            dgvNiveles.DataSource = query.ToList();
            dgvNiveles.DataBind();
        }

        public async void cargarGridNivelesXTipo(long  tipoNIvel_)
        {
            List<ClNivel> niveles = new List<ClNivel>();
            List<ClCurso> curso = new List<ClCurso>();
            List<ClEstadoNivel> estNivel = new List<ClEstadoNivel>();
            List<ClTipoNivel> tipoNivel = new List<ClTipoNivel>();
            List<CLLibros> libros = new List<CLLibros>();
            niveles= await ServicioGetNiveles();
            curso = await ServicioGetCurso();
            estNivel = await ServicioGetEstadoNivel();
            tipoNivel = await ServicioGetTipoNivel();
            libros = await ServicioGetLibros();
            var query = from a in niveles
                        join b in curso on a.idCurso equals b.IdCurso
                        join c in estNivel on a.idEstadoNivel equals c.IdEstadoNivel
                        join d in tipoNivel on a.idTipoNivel equals d.idtipoNivel
                        join e in libros on a.idLibro equals e.idLibro
                        where a.idTipoNivel== tipoNIvel_
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
            dgvNiveles.DataSource = query.ToList();
            dgvNiveles.DataBind();
        }
        public async void cargarGridNivelesXcod(string codnivel)
        {
            List<ClNivel> nivelesxcod = new List<ClNivel>();
            List<ClCurso> curso = new List<ClCurso>();
            List<ClEstadoNivel> estNivel = new List<ClEstadoNivel>();
            List<ClTipoNivel> tipoNivel = new List<ClTipoNivel>();
            List<CLLibros> libros = new List<CLLibros>();
            nivelesxcod = await ServicioGetNivelesXCod();
            curso = await ServicioGetCurso();
            estNivel = await ServicioGetEstadoNivel();
            tipoNivel = await ServicioGetTipoNivel();
            libros = await ServicioGetLibros();

            var query = from a in nivelesxcod
                        join b in curso on a.idCurso equals b.IdCurso
                        join c in estNivel on a.idEstadoNivel equals c.IdEstadoNivel
                        join d in tipoNivel on a.idTipoNivel equals d.idtipoNivel
                        join e in libros on a.idLibro equals e.idLibro
                        where a.codNivel.Trim()==codnivel
                        select new
                        {
                            idNivel = a.idNivel,
                            idEstadoNivel = a.idEstadoNivel,
                            estadoNivel = c.DescEstadoNivel,
                            idTipoNivel = a.idTipoNivel,
                            idCurso = a.idCurso,
                            nomCurso = b.DescCurso,
                            codNivel = a.codNivel,
                            nomNivel = a.nomNivel,
                            descNivel = a.descNivel,
                            tipoNivel = d.descTipoNivel,
                            libro = e.descLibro,
                            CostoNivel = a.costoNIvel,
                        };
            dgvNiveles.DataSource = query.ToList();
            dgvNiveles.DataBind();
        }
        #endregion

        #region MetodosForms

        protected void dgvNiveles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                //llenar Modal
                int fila = Convert.ToInt32(e.CommandArgument);
                long idregistro = Convert.ToInt64(dgvNiveles.DataKeys[fila].Value);
                llenarmodal(idregistro);
                btnPopUp_ModalPopupExtender.Show();
            }
        }
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            ClNivel nivel = new ClNivel();
            nivel.idNivel = Convert.ToInt64(txtIdNivel.Text);
            nivel.idEstadoNivel = Convert.ToInt64(cbxEstadoNivel.SelectedValue.ToString());
            nivel.idLibro = Convert.ToInt64(cbxLibros.SelectedValue.ToString());
            nivel.idCurso = Convert.ToInt64(cbxCursos.SelectedValue.ToString());
            nivel.codNivel = txtCodNivel.Text.ToUpper().Trim();
            nivel.nomNivel = txtNomNivel.Text.ToUpper().Trim();
            nivel.descNivel = cbxNivel.SelectedValue.ToString();
            nivel.idTipoNivel = Convert.ToInt64(idTipoNivel.Value);
            nivel.costoNIvel = Convert.ToDouble(txtCostoNivel.Text);
            ServicioActualizarNivel(nivel);
        }
        protected void dgvNiveles_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(dgvNiveles.DataKeys[e.RowIndex].Values[0].ToString());
            ServicioEliminarNivel(id);
        }
        #endregion
        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            string codnivel = txtCodCurso.Text.Trim();
            int idTipoNivel = Convert.ToInt32(cbxSearchNiveles.SelectedValue);
            if (codnivel != "" && Convert.ToInt32(cbxSearchNiveles.SelectedValue) == 0)//POR CODIGO
            {
                cargarGridNivelesXcod(codnivel);
            }
            else if (Convert.ToInt32(cbxSearchNiveles.SelectedValue) == 0 && codnivel != "")//TODOS
            {
                cargarGridNiveles();
            }
            else if (Convert.ToInt32(cbxSearchNiveles.SelectedValue) != 0 && codnivel == "")// POR TIPO NIVEL 
            {
                cargarGridNivelesXTipo(idTipoNivel);
            }
            else if (Convert.ToInt32(cbxSearchNiveles.SelectedValue) == 0 && codnivel == "")// POR TIPO NIVEL 
            {
                cargarGridNiveles();
            }

        }

        protected void dgvNiveles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvNiveles.PageIndex = e.NewPageIndex;
            cargarGridNiveles();
        }
    }
}