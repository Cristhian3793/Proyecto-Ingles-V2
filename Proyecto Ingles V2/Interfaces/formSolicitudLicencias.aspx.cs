using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Text;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;

namespace Proyecto_Ingles_V2.Interfaces
{
    public partial class formSolicitudLicencias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
        }

        #region Metodos
        public Task EnviarCorreo(string email, string subject, string message)
        {
            string text = message;

            AlternateView plainView =
            AlternateView.CreateAlternateViewFromString(text,
                            Encoding.UTF8,
                            MediaTypeNames.Text.Plain);

            string html = "<h2>" + message + "</h2>";
            AlternateView htmlView =
            AlternateView.CreateAlternateViewFromString(html,
            Encoding.UTF8,
            MediaTypeNames.Text.Html);
            LinkedResource img =
                         new LinkedResource(@"C:\Users\UISEK\source\repos\Proyecto Ingles V2\Proyecto Ingles V2\images\uisek-mail.png",
                         MediaTypeNames.Image.Jpeg);
            img.ContentId = "imagen";
            htmlView.LinkedResources.Add(img);
            try
            {
                // Credenciales
                var credentials = new NetworkCredential("cristhian.tupiza@uisek.edu.ec", "Alexiscrow3793");
                // Mail message
                var mail = new MailMessage()
                {
                    From = new MailAddress("cristhian.tupiza@uisek.edu.ec", "Cristhian Tupiza"),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };
                mail.AlternateViews.Add(plainView);
                mail.AlternateViews.Add(htmlView);
                mail.To.Add(new MailAddress(email));
                // Smtp client
                var client = new SmtpClient()
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Credentials = credentials
                };
                // Send it...         
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            return Task.CompletedTask;
        }

        #endregion
        public void limpiarCampos()
        {
            txtInformacion.Text = "";
            txtEmail.Text = "";
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string subject = "Solicitud Licencias";
            string message = txtInformacion.Text;
            EnviarCorreo(email, subject, message);
            string script = "confirm();";      
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
            limpiarCampos();
        }
    }
}