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
using System.Drawing;
using System.Data;

namespace Proyecto_Ingles_V2.Interfaces
{
    public partial class formVerNotas : System.Web.UI.Page
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
                    cargarComboNiveles();
                    cargarComboPeriodo();
                    cargarGridEstudiante();
                }
            }
        }



        protected void dgvInscrito_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerNotas") {
                string nombre, apellidos;
                txtPromedio.Text = "";
                int fila = Convert.ToInt32(e.CommandArgument);//captura el indice de la fila del gridview
                long idInscito = Convert.ToInt64(dgvInscrito.DataKeys[fila]["IdInscrito"].ToString());
                string numDoc= dgvInscrito.Rows[fila].Cells[2].Text.ToString().Trim();//captura la cedula o pasaporte
                long idNivel = Convert.ToInt64(dgvInscrito.DataKeys[fila]["IdNivel"].ToString());
                long idNivelestudiante = Convert.ToInt64(dgvInscrito.DataKeys[fila]["IdNivelInscrito"].ToString());
                nombre = dgvInscrito.Rows[fila].Cells[5].Text.ToString().Trim();
                apellidos = dgvInscrito.Rows[fila].Cells[6].Text.ToString().Trim();
                txtNivel.Text= dgvInscrito.Rows[fila].Cells[8].Text.ToString().Trim();
                txtNombres.Text = nombre + " " + apellidos;
                txtCed.Text = numDoc;
                HiddenIdIns.Value = idInscito.ToString();
                HiddenNivel.Value = idNivel.ToString();
                HiddenNivelEstudiante.Value = idNivelestudiante.ToString();
                CargarGridNotasAlumno(idInscito,idNivel,idNivelestudiante);
                btnPopUp_ModalPopupExtender2.Show();

            }
            if(e.CommandName== "CrearNotas")
            {
                int fila = Convert.ToInt32(e.CommandArgument);
                long idInscrito = Convert.ToInt64(dgvInscrito.DataKeys[fila]["IdInscrito"].ToString());
                long idNivel = Convert.ToInt64(dgvInscrito.DataKeys[fila]["IdNivel"].ToString());
                long idNivelestudiante = Convert.ToInt64(dgvInscrito.DataKeys[fila]["IdNivelInscrito"].ToString());
                crearNotas(idInscrito, idNivel, idNivelestudiante);

            }
        }
        protected async void dgvNotas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {


                ClNota no = new ClNota();
                long id = Convert.ToInt64(dgvNotas.DataKeys[e.RowIndex]["IDNota"].ToString());
                TextBox txtUnit1 = (dgvNotas.Rows[e.RowIndex].Cells[1].FindControl("EditUnit1") as TextBox);
                no.CALIFICACION = Convert.ToDouble(txtUnit1.Text);
                long idIns = Convert.ToInt64(HiddenIdIns.Value);//idInscrito
                long idNiv = Convert.ToInt64(HiddenNivel.Value);//idnivel
                long idnivelestu= Convert.ToInt64(HiddenNivelEstudiante.Value);
                bool resp =await validarEditNotas(id,idIns);
                if (resp == false)
                {
                    actualizarNotas(no, id, idIns, idNiv,idnivelestu);
                }
                else
                {
                string script = "NoActualizar();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "xscript", script, true);
                }               
                dgvNotas.EditIndex = -1;
        }
        protected void dgvNotas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

                dgvNotas.EditIndex = -1;
                long idIns = Convert.ToInt64(HiddenIdIns.Value);
                long idNivel= Convert.ToInt64(HiddenNivel.Value);
                long idNivelEstudiante = Convert.ToInt64(HiddenNivelEstudiante.Value);
                CargarGridNotasAlumno(idIns, idNivel,idNivelEstudiante);
            
        }
        protected void dgvNotas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            dgvNotas.EditIndex = e.NewEditIndex;
            long idIns = Convert.ToInt64(HiddenIdIns.Value);
            long idNivel = Convert.ToInt64(HiddenNivel.Value);
            long idNivelEstudiante = Convert.ToInt64(HiddenNivelEstudiante.Value);
            CargarGridNotasAlumno(idIns, idNivel, idNivelEstudiante);

        }
        #region metodos
        public async void actualizarNotas(ClNota no, long idnota, long idIns, long idNiv,long idnivelestu)
        {
            try
            {
                string uri = "api/Notas?idNota=" + idnota;
                var myContent = JsonConvert.SerializeObject(no);
                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.PutAsync(uri, stringContent);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    long idNivelEstudiante = Convert.ToInt64(HiddenNivelEstudiante.Value);
                    CargarGridNotasAlumno(idIns, idNiv, idnivelestu);
                    cargarGridEstudiante();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async void ActualizarEstadoCalificacion(long idNota, int estado)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ClNota no = new ClNota();
                no.ESTADO = estado;
                string uri = "api/Notas?idNota=" + idNota;
                var myContent = JsonConvert.SerializeObject(no);
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
        public async void crearNotas(long idInscrito, long idnivel,long idNivelestudiante)
        {
            //verificar si las notas ya fueron creadas
            int contador_notas = 0;
            List<ClNota> notas = await ServicioGetNotas(idInscrito);
            foreach (ClNota a in notas) {
                if (a.IDNIVELESTUDINTE==idNivelestudiante) {
                    contador_notas++;
                }
            }

            if (contador_notas == 0)//crear notas
            {
                ClNota nota = new ClNota();
                //servicio extraer temas
                List<ClUnidad> unidades = await ServicioGetTemas(idnivel);
                foreach (ClUnidad a in unidades)
                {
                    long idNomUnidad = a.idNomUnidad;
                    nota.IDTEMA = idNomUnidad;
                    nota.IDNIVEL = idnivel;
                    nota.IDINSCRITO = idInscrito;
                    nota.IDNIVELESTUDINTE = idNivelestudiante;
                    ServicioInsertarNotas(nota);

                }
            }
            else//ya existen notas no debe volver a crear
            {
                string script = "rechazado_notas();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "xscript", script, true);

            }

        }

        //actualizar nivel inscrito
        public async void ActualizarEstadoNivelInscrito(long idNivelestu, int estado)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ClNivelesInscrito no = new ClNivelesInscrito();
                no.ESTADONIVEL = estado;
                string uri = "api/EstadoNivelInscrito/" + idNivelestu;
                var myContent = JsonConvert.SerializeObject(no);
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




        public async void CargarGridNotasAlumno(long idInscrito, long idNivel,long idNivelEstudiante)
        {
            try
            {
                List<ClNota> notas = await ServicioGetNotas(idInscrito);
                List<ClInscritoAutonomo> ins = await ServicioExtraerInscrito();
                List<ClNivel> niv = await ServicioExtraerNIvel();
                List<ClUnidad> uni = await ServicioGetAllTemas();
                var queryNotasEnProceso = from a in notas
                                          where a.ESTADO == 0 && a.IDNIVEL==idNivel && a.IDNIVELESTUDINTE==idNivelEstudiante
                                          select new
                                          {
                                              IdNota=a.IDNOTA,
                                              IdInscrito=a.IDINSCRITO,
                                              idNivel=a.IDNIVEL,
                                              idTema=a.IDTEMA,
                                              calificacion=a.CALIFICACION,
                                              a.ESTADO
                                          };


                if (uni.Count() > 0)
                {
                    if (queryNotasEnProceso.Count() > 0) { 
                    
                    var query = from a in notas
                                join b in uni on a.IDTEMA equals b.idNomUnidad
                                join c in niv on a.IDNIVEL equals c.idNivel
                                join d in ins on a.IDINSCRITO equals d.IdInscrito
                                where a.IDNIVELESTUDINTE==idNivelEstudiante && a.ESTADO == 0 && a.IDNIVEL == idNivel
                                select new
                                {
                                    NombreAlumno = d.NombreInscrito,
                                    ApellidosAlumno = d.ApellidoInscrito,
                                    NomNivel = c.nomNivel,
                                    idNiv = a.IDNIVEL,
                                    idIns = a.IDINSCRITO,
                                    NomUnidad = b.NomUnidad,
                                    IDNota = a.IDNOTA,
                                    Calificacion = a.CALIFICACION,
                                    Estado = a.ESTADO,
                                };
                    dgvNotas.DataSource = query;
                    dgvNotas.DataBind();              
                    }
                    else
                    {
                        dgvNotas.DataSource = "";
                        dgvNotas.DataBind();
                    }


                }
                else
                {
                    string script = "rechazado();";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "xscript", script, true);
                }

            }
            catch (Exception ex)
            {
                string script = "rechazado();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "xscript", script, true);

            }

        }
        //public async void CargarComboNivelesAlumno(string numdoc, long idInscrito)
        //{
        //    List<ClInscritoAutonomo> inscrito = new List<ClInscritoAutonomo>();
        //    List<ClPeriodoInscripcion> periodo = new List<ClPeriodoInscripcion>();
        //    List<ClNivel> nivel = new List<ClNivel>();
        //    List<ClNivelesInscrito> nivelesIns = new List<ClNivelesInscrito>();
        //    nivelesIns = await ServicioGetNivelesInscritos();
        //    inscrito = await ServicioExtraerInscrito();
        //    periodo = await ServicioExtraerPeriodo();
        //    nivel = await ServicioExtraerNIvel();
        //    var query = from a in inscrito
        //                join b in nivelesIns on a.IdInscrito equals b.IDINSCRITO
        //                join c in nivel on b.IDNIVEL equals c.idNivel
        //                join d in periodo on a.idPerInscripcion equals d.IdPeriodoInscripcion
        //                where b.PRUEBA == 0 && b.IDESTADONIVEL == 1 && a.NumDocInscrito.Trim() == numdoc && b.IDINSCRITO == idInscrito && b.ESTADONIVEL==0
        //                select new
        //                {
        //                    IdNivelInscrito=b.IDNIVELESTUDIANTE,
        //                    IdInscrito = a.IdInscrito,
        //                    IdTipoDocumento = a.IdTipoDocumento,
        //                    NombreInscrito = a.NombreInscrito,
        //                    ApellidoInscrito = a.ApellidoInscrito,
        //                    NumDocInscrito = a.NumDocInscrito,
        //                    IdNivel = b.IDNIVEL,
        //                    NomNivel = c.nomNivel
        //                };
        //    ddlNivel.DataSource = query;
        //    ddlNivel.DataValueField = "IdNivelInscrito";
        //    ddlNivel.DataTextField = "NomNivel";
        //    ddlNivel.DataBind();
        //    //CargarGridNotasAlumno(idInscrito, Convert.ToInt64(ddlNivel.SelectedValue.ToString()));

        //    btnPopUp_ModalPopupExtender2.Show();
        //}
        public async void cargarComboNiveles()
        {
            List<ClNivel> niveles = await ServicioExtraerNIvel();
            var niveles_ = from a in niveles
                           where a.codNivel.Trim() != "SEK1203"
                           orderby a.descNivel ascending
                           select new
                           {
                               idNivel = a.idNivel,
                               nomNivel = a.nomNivel
                           };
            cbxNiveles.DataSource = niveles_;
            cbxNiveles.DataValueField = "idNivel";
            cbxNiveles.DataTextField = "nomNivel";
            cbxNiveles.DataBind();
        }
        public async void cargarGridEstudiante()
        {
            List<ClInscritoAutonomo> inscrito = new List<ClInscritoAutonomo>();
            List<ClTipoEstudiante> tipoEstudiante = new List<ClTipoEstudiante>();
            List<ClPeriodoInscripcion> periodo = new List<ClPeriodoInscripcion>();
            List<ClPrueba> prueba = new List<ClPrueba>();
            List<ClNivel> nivel = new List<ClNivel>();
            List<ClNivelesInscrito> nivelesIns = new List<ClNivelesInscrito>();
            List<ClEstadoEstudiante> estadoEstu = new List<ClEstadoEstudiante>();
            List<ClNota> notas = new List<ClNota>();
            nivelesIns = await ServicioGetNivelesInscritos();
            tipoEstudiante = await ServicioExtraerTipoEstudiante();
            inscrito = await ServicioExtraerInscrito();
            periodo = await ServicioExtraerPeriodo();
            prueba = await ServicioExtraerPrueba();
            nivel = await ServicioExtraerNIvel();
            estadoEstu = await ServicioGetEstadoEstudiante();
            var query = from a in inscrito
                        join b in nivelesIns on a.IdInscrito equals b.IDINSCRITO
                        join c in nivel on b.IDNIVEL equals c.idNivel
                        join d in tipoEstudiante on a.IdTipoEstudiante equals d.IdTipoEstudiante
                        join e in periodo on b.IDPERIODOINSCRIPCION equals e.IdPeriodoInscripcion
                        join f in estadoEstu on b.IDESTADONIVEL equals f.CodEstadoEstu
                        where b.PRUEBA == 0 && b.ESTADONIVEL == 0 && b.IDESTADONIVEL == 1
                        //0 son solo niveles 1 significa nivel cerrado,1 significa pagado
                        select new
                        {
                            IdNivelInscrito = b.IDNIVELESTUDIANTE,
                            IdInscrito = a.IdInscrito,
                            IdTipoDocumento = a.IdTipoDocumento,
                            TipoEstudiante = d.DescTipoEstudiante,
                            NombreInscrito = a.NombreInscrito,
                            ApellidoInscrito = a.ApellidoInscrito,
                            NumDocInscrito = a.NumDocInscrito,
                            NomNivel = c.nomNivel,
                            IdPeriodo = b.IDPERIODOINSCRIPCION,
                            IdNivel=b.IDNIVEL,
                            Periodo = e.Periodo,
                            
                        };
            dgvInscrito.DataSource = query;
            dgvInscrito.DataBind();
        }
        public async Task<long> extrarIdPeridoActivo()
        {
            List<ClPeriodoInscripcion> periodoActivo = await ServicioExtraerPeriodo();
            long idPeriodo;
            var periodo = from a in periodoActivo
                          where a.EstadoPeriodo == 1
                          select new
                          {
                              IDPERIODO=a.IdPeriodoInscripcion,
                          };
            idPeriodo = Convert.ToInt64(periodo.Select(x => x.IDPERIODO).FirstOrDefault());
            return idPeriodo;

        }
        public async Task<long> extraerIdNivel(double? calificacion)
        {
            long nivel = 0;
            List<ClCalificacionNivel> calif = new List<ClCalificacionNivel>();
            double califDesde;
            double califHasta;

            calif = await ServicioExtraerCalificacion();
            foreach (ClCalificacionNivel a in calif)
            {
                califDesde = Convert.ToDouble(a.calificacionNivelDesde);
                califHasta = Convert.ToDouble(a.calificacionNivelHasta);

                if (calificacion >= califDesde && calificacion <= califHasta)
                {
                    nivel = a.idNivel;
                }
            }
            return nivel;
        }
        public async void UpdateNivelInscrito(ClNivelesInscrito pru, long idNIvelInscrito)
        {
            string uri = "api/NivelesInscrito/" + idNIvelInscrito;
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
            }
        }
        protected async void btnCalcPromedio_Click(object sender, EventArgs e)//calcular promedio y asignar nivel
        {
            long idperiodoActivo= await extrarIdPeridoActivo();
            int cantNiveles = 6;
            long idInscrito = 0;
            long idNIvelEstudiante = 0;
            long idNivel = 0;
            int contNivels = 0;
            idInscrito = Convert.ToInt64(HiddenIdIns.Value);
            idNivel = Convert.ToInt64(HiddenNivel.Value);
            idNIvelEstudiante= Convert.ToInt64(HiddenNivelEstudiante.Value);
            ActualizarEstadoNivelInscrito(idNIvelEstudiante, 1);//1 significa cerrdado
            double promedio = 0;
            List<ClNota> notas = await ServicioGetNotas(idInscrito);
            var query = from a in notas
                        where a.IDNIVELESTUDINTE == idNIvelEstudiante && a.IDINSCRITO==idInscrito
                        select new
                        {
                            IDCALIFICACION = a.IDNOTA,
                            IDNIVEL = a.IDNIVEL,
                            IDINSCRITO = a.IDINSCRITO,
                            Calificacion = a.CALIFICACION,
                        };

            foreach (var a in query)
            {
                if (a.Calificacion != 0)
                {
                    promedio = promedio + a.Calificacion;
                    contNivels++;
                }

            }
            if (contNivels == cantNiveles)
            {
                ClNota nota = new ClNota();

                promedio = promedio / cantNiveles;
                txtPromedio.Text = Convert.ToDouble(promedio).ToString();
                if (promedio >= 6)//aprueba el nivel y se agrega nuevo nivel a estudiante
                {
                    //estados
                    //0 en proceso
                    //1 aprobado
                    //2 reprobrado
                    //3 promedio
                    //actulizar notas con estatus aprobado
                    foreach (var a in query)
                    {
                        long idCalif = a.IDCALIFICACION;
                        int estado = 1;
                        ActualizarEstadoCalificacion(idCalif, estado);
                    }
                    //guardar linea con promedio
                    nota.IDINSCRITO = idInscrito;
                    nota.IDNIVEL = idNivel;
                    nota.IDTEMA = 0;
                    nota.CALIFICACION = promedio;
                    nota.ESTADO = 3;
                    nota.IDNIVELESTUDINTE = idNIvelEstudiante;
                    //nota.IDNIVELESTUDINTE = Convert.ToInt64(ddlNivel.SelectedValue.ToString());
                    ServicioInsertarPromedio(nota);
                    //llamar servicio para extraer id de nivel enviar parametros idnivel y recuperar el nivel, buscar el nivel y extraer el idnivel
                    List<ClNivel> nivel = await ServicioExtraerNIvel();
                    int nivel_Desc = -1;
                    foreach (ClNivel a in nivel)
                    {
                        if (a.idNivel == idNivel)
                        {
                            nivel_Desc = Convert.ToInt32(a.descNivel);//nivel_desc es el numero de nivel
                        }
                    }
                    int idNuevoNivel = -1;
                    foreach (ClNivel a in nivel)
                    {
                        if (Convert.ToInt32(a.descNivel) == (nivel_Desc + 1) && nivel_Desc<8)
                        {
                            idNuevoNivel = Convert.ToInt32(a.idNivel);
                        }
                    }
                    //guardar nivel nuevo con estatus en proceso
                    ClNivelesInscrito nivIns = new ClNivelesInscrito();
                    nivIns.IDNIVEL = idNuevoNivel;
                    nivIns.IDESTADONIVEL = 0;
                    nivIns.IDINSCRITO = idInscrito;
                    nivIns.FECHAREGISTRO = Convert.ToString(DateTime.Now);
                    nivIns.IDPRUEBAUBICACION = null;
                    nivIns.PRUEBA = 0;
                    nivIns.ESTADONIVEL = 0;
                    nivIns.IDPERIODOINSCRIPCION = idperiodoActivo;
                    ServicioInsertarNivelIns(nivIns);
                }
                else if (promedio < 6)//pierde nivel y se guarda nivel actual
                {
                    //actualizar estatus en proceso para niveles que aun no se hayan cerrado
                    //poner estatus nivel perdido
                    foreach (var a in query)
                    {
                        long idCalif = a.IDCALIFICACION;
                        int estado = 2;//reprobado
                        ActualizarEstadoCalificacion(idCalif, estado);
                    }
                    //guardar linea com promedio
                    nota.IDINSCRITO = idInscrito;
                    nota.IDNIVEL = idNivel;
                    nota.IDTEMA = 0;
                    nota.CALIFICACION = promedio;
                    nota.ESTADO = 3;
                    nota.IDNIVELESTUDINTE = idNIvelEstudiante;
                    //nota.IDNIVELESTUDINTE = Convert.ToInt64(ddlNivel.SelectedValue.ToString());
                    ServicioInsertarPromedio(nota);
                    //volver asignar nivel con estatus en proceso
                    ClNivelesInscrito nivIns = new ClNivelesInscrito();
                    nivIns.IDNIVEL = idNivel;
                    nivIns.IDESTADONIVEL = 0;
                    nivIns.IDINSCRITO = idInscrito;
                    nivIns.FECHAREGISTRO = Convert.ToString(DateTime.Now);
                    nivIns.IDPRUEBAUBICACION = null;
                    nivIns.PRUEBA = 0;
                    nivIns.ESTADONIVEL = 0;
                    nivIns.IDPERIODOINSCRIPCION = idperiodoActivo;
                    ServicioInsertarNivelIns(nivIns);
                }
            }
            else//no se inserto las 6 notas completas
            {
                txtPromedio.Text = Convert.ToInt32(-1).ToString();
            }
        }

        public async Task<bool> validarEditNotas(long idNota,long idInscrito) //no dejar editar notas
        {
            bool resp = false;
            int estado;
            List<ClNota> notas = await ServicioGetNotas(idInscrito);
            if (notas.Count() > 0) {
                foreach (ClNota a in notas) {
                    if (a.IDNOTA == idNota) {
                        estado = a.ESTADO;
                        if (estado != 0) {
                            resp = true;//si es verdadero ya no se puede modificar
                            break;
                        }
                    }
                }
            }
            return resp;
        }
        public async void cargarComboPeriodo()
        {
            List<ClPeriodoInscripcion> periodos = await ServicioExtraerPeriodo();
            cbxPeriodo.DataSource = periodos;
            cbxPeriodo.DataValueField = "IdPeriodoInscripcion";
            cbxPeriodo.DataTextField = "Periodo";
            cbxPeriodo.DataBind();
        }

        #endregion
        #region Servicios
        public async Task<List<ClInscritoAutonomo>> ServicioExtraerInscrito()//cargar todos inscritos
        {
            List<ClInscritoAutonomo> compInf = new List<ClInscritoAutonomo>();
            try
            {
                //string url = "http://servicioinglesuisek/";               
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/InscritoAutonomo");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClInscritoAutonomo>>(empResponse);
                    return compInf;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async Task<List<ClTipoEstudiante>> ServicioExtraerTipoEstudiante()
        {
            List<ClTipoEstudiante> compInf = new List<ClTipoEstudiante>();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/TipoEstudiante");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClTipoEstudiante>>(empResponse);
                    return compInf;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }

        public async Task<List<ClPeriodoInscripcion>> ServicioExtraerPeriodo()
        {
            List<ClPeriodoInscripcion> compInf = new List<ClPeriodoInscripcion>();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/PeriodoInscripcion");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClPeriodoInscripcion>>(empResponse);
                    return compInf;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;

        }
        public async Task<List<ClCalificacionNivel>> ServicioExtraerCalificacion()
        {
            List<ClCalificacionNivel> compInf = new List<ClCalificacionNivel>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/CalificacionNivel");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClCalificacionNivel>>(empResponse);
                    return compInf;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;

        }
        public async Task<List<ClNivel>> ServicioExtraerNIvel()
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
                    return compInf;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;

        }
        public async void ServicioInsertarSoloNivel(ClNivelesInscrito pru)
        {
            try
            {
                string uri = "api/NivelesInscrito";
                var myContent = JsonConvert.SerializeObject(pru);
                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.PostAsync(uri, stringContent);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task<List<ClUnidad>> ServicioGetTemas(long idNIvel)
        {
            List<ClUnidad> compInf = new List<ClUnidad>();
            try
            {


                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/Unidades/" + idNIvel);
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
        public async Task<List<ClUnidad>> ServicioGetAllTemas()
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

        public async void ServicioInsertarPromedio(ClNota nota)//crear promedio
        {

            try
            {
                string uri = "api/Notas";
                var myContent = JsonConvert.SerializeObject(nota);
                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.PostAsync(uri, stringContent);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public async void ServicioInsertarNotas(ClNota nota)//crear notas
        {

            try
            {
                string uri = "api/Notas";
                var myContent = JsonConvert.SerializeObject(nota);
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
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "xscript", script, true);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public async Task<List<ClNivelesInscrito>> ServicioGetNivelesInscritos()
        {
            List<ClNivelesInscrito> compInf = new List<ClNivelesInscrito>();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/NivelesInscrito");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClNivelesInscrito>>(empResponse);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async Task<List<ClPrueba>> ServicioExtraerPrueba()
        {
            List<ClPrueba> compInf = new List<ClPrueba>();
            try
            {
                //string url = "http://servicioinglesuisek/";               
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/Prueba");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClPrueba>>(empResponse);
                    return compInf;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async Task<List<ClEstadoEstudiante>> ServicioGetEstadoEstudiante()
        {
            List<ClEstadoEstudiante> compInf = new List<ClEstadoEstudiante>();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/EstadoEstudiante");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClEstadoEstudiante>>(empResponse);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async Task<List<ClNota>> ServicioGetNotas(long idInscrito)
        {
            List<ClNota> compInf = new List<ClNota>();
            try
            {


                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/Notas?idIns=" + idInscrito);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClNota>>(empResponse);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async void ServicioInsertarNivelIns(ClNivelesInscrito pru)
        {
            try
            {
                string uri = "api/NivelesInscrito";
                var myContent = JsonConvert.SerializeObject(pru);
                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.PostAsync(uri, stringContent);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task<List<ClNivelesInscrito>> ServicioGetNivelesInscritosxId(long id)
        {
            List<ClNivelesInscrito> compInf = new List<ClNivelesInscrito>();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/NivelesInscrito/"+id);
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClNivelesInscrito>>(empResponse);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        #endregion
        protected void btnConsultarNotas_Click(object sender, EventArgs e)
        {
            //long idNivel = Convert.ToInt64(ddlNivel.SelectedValue.ToString());
            long idInscrito = Convert.ToInt64(HiddenIdIns.Value);
            //CargarGridNotasAlumno(idInscrito,idNivel);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
    }
}