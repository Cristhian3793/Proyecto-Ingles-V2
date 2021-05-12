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
    public class EstadoPruebaController : ApiController
    {
        ServiciosEstadoPrueba serv = new ServiciosEstadoPrueba();
        [HttpGet]
        public IList<ClEstadoPrueba> GetEstadoPrueba()//obtener todos los Estados Niveles
        {
            return serv.getEstadoPrueba();
        }
    }
}
