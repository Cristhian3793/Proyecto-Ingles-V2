using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Cfg;
using Logica.Conexion;
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
using Logica.Modelos;
using Logica;
using System.Web.UI.HtmlControls;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

using System.Net.Mail;
using System.Net.Mime;


namespace PremioExcelencia.InscripcionExterna
{
    public partial class Inscripcion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        #region Metodos



      
        public void limpiarCampos()
        {
            
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtCelular.Text = "";
            txtDireccion.Text = "";
            txtEmail.Text = "";
            txtTelefono.Text = "";
            txtCed.Text = "";
            txtInformacion.Text = "";
            RabExamen.Checked = false;
            RabCedula.Checked = false;
            RabTipoEstudiante.Checked = false;


        }

        #endregion

        #region Metodos Forms
        protected void btnGuardarInscrito_Click(object sender, EventArgs e)
        {
          
   
        }
        protected void RabCedula_CheckedChanged(object sender, EventArgs e)
        {
            txtCed.Text = "";
            txtCed.MaxLength = 10;
        }
        protected void RabPasaporte_CheckedChanged(object sender, EventArgs e)
        {
            txtCed.Text = "";
            txtCed.MaxLength = 12;
        }


        #endregion

        protected void btnSendSuggestion_Click(object sender, EventArgs e)
        {
            
        }
       

    }
}