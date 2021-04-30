using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Logica.Modelos;
using Logica.Servicios;
namespace Services.Controllers
{
    public class TipoEstudianteController : ApiController
    {
        ServiciosTipoEstudiante serv = new ServiciosTipoEstudiante();
        [HttpGet]
        public IList<ClTipoEstudiante> getTipoEstudiante() {
            return serv.getTipoEstudiante();
        }

    }
}
