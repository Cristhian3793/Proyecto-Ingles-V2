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
namespace Proyecto_Ingles_V2.Login
{
    public partial class formLogin : System.Web.UI.Page
    {
        static conexionServidor cs = new conexionServidor();
        string url = cs.url.ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                Session["usuario"] = "";
                Session["Password"] = "";
                Session["TipoUser"] = "";
            }
        }
        #region Servicios
        public async Task<List<ClUsuarios>>  ServicioGetUsuarios() {
            List<ClUsuarios> compInf = new List<ClUsuarios>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/Usuarios");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClUsuarios>>(empResponse);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async void login(string usuario,string password) {
            string user, pass,nombres,apellidos;
            int tipoUser = 0;
            List<ClUsuarios> users = new List<ClUsuarios>();
            users = await ServicioGetUsuarios();
            var query = from a in users
                        where a.Usuario == usuario
                        select new { 
                        Usuario_=a.Usuario,
                        Password=a.Password,
                        Nombres=a.Nombres,
                        Apellidos=a.Apellidos,
                        TipoUser=a.tipoUser,
                        };

            if (query.Count() >= 1)
            {
                user = query.Select(x => x.Usuario_).FirstOrDefault().Trim().ToString();
                pass = query.Select(x => x.Password).FirstOrDefault().Trim().ToString();
                pass = DesencriptarClave(pass);
                nombres= query.Select(x => x.Nombres).FirstOrDefault().Trim().ToString();
                apellidos = query.Select(x => x.Apellidos).FirstOrDefault().Trim().ToString();
                tipoUser = Convert.ToInt32(query.Select(x => x.TipoUser).FirstOrDefault().ToString());
                if (user.Trim() != null && user.Trim() != "" && pass.Trim() != "")
                {
                    if (pass.Trim().ToString() == password.Trim().ToString())
                    {

                        Session["usuario"] = user;
                        Session["Password"] = pass;
                        Session["Nombres"] = nombres;
                        Session["Apellidos"] = apellidos;
                        Session["TipoUser"] = tipoUser;
                        Response.Redirect("../Interfaces/Default.aspx");
                    }
                    else
                    {
                        string script = "<script type='text/javascript'>alert('Contraseña Incorrecta');</script>";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "script", script, false);

                    }

                }
                else
                {
                    string script = "<script type='text/javascript'>alert('Usuario no Existe');</script>";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "script", script, false);
                }
            }
        }
        #endregion
        public static string DesencriptarClave(string password)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(password);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text.Trim().ToString();
            string password = txtPassword.Text.Trim().ToString();
            login(user, password);
        }
    }
}