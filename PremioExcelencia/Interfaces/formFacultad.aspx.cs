using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Cfg;
using Logica.Modelos;
using Logica.Conexion;
using Logica;
using System.Data.SqlClient;
using System.Data;
using Logica.Servicios;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using Logica.ConexionServicios;

namespace PremioExcelencia.Interfaces
{
    public partial class formFacultad : System.Web.UI.Page
    {
        static conexionServidor cs = new conexionServidor();
        string url = cs.url.ToString();
        protected void Page_Load(object sender, EventArgs e)
        {

            //    if (Session["usuario"] == null || (string)Session["usuario"] == "")
            //    {
            //        Response.Redirect("/Login/formLogin.aspx");
            //    }
            //    if ((int)Session["TipoUser"] == 1)
            //    {
            //        Response.Redirect("/Interfaces/Default.aspx");
            //    }
            //    else
            //    {
            //    if (!IsPostBack)
            //    {
            //    }
            //}
        }
        protected  void Button1_Click(object sender, EventArgs e)
        {
           
        }
     

      



    }
}