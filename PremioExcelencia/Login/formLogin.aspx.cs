using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica.Modelos;
using Logica.Servicios;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Net.Mail;


namespace PremioExcelencia.Login
{
    public partial class formLogin : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["usuario"] = "";
                Session["codUsuario"] = "";
                Session["Password"] = "";
                Session["TipoUser"] = "";
            }
        }
        #region Servicios

        public void limpiarcampos()
        {
            txtUser.Text = "";
            txtPassword.Text = "";
        }
        #endregion
        public static string DesencriptarClave(string password)
        {
            return "";
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnSendSuggestion_Click(object sender, EventArgs e)
        {
            
        }
        public void EnviaCorreo_HelpDesk()
        {
            
        }
    }
}