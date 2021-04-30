using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto_Ingles_V2.Interfaces
{
    public partial class formDiasHorarios : System.Web.UI.Page
    {
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
                
            }
        }
    }
}