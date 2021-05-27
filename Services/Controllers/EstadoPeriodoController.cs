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
    public class EstadoPeriodoController : ApiController
    {
        ServiciosEstadoPeriodo serv = new ServiciosEstadoPeriodo();
        [HttpGet]
        public IList<ClEstadoPeriodo> GetEstadoPeriodo()//obtener todos los Estados Niveles
        {
            return serv.getEstadoPeriodo();
        }
    }
}
