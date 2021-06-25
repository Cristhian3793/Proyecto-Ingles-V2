
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PremioExcelencia.Interfaces
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    if (Session["usuario"] == null || (string)Session["usuario"] == "")
            //    {
            //        Response.Redirect("../Login/formLogin.aspx");
            //    }
            //}
        }

        protected void btnSendSuggestion_Click(object sender, EventArgs e)
        {
          
        }
  
    }
}