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
    public class EstadoEstudianteController : ApiController
    {
        ServiciosEstadoEstudiante serv = new ServiciosEstadoEstudiante();
        [HttpGet]
        public IList<ClEstadoEstudiante> GetEstadoEstudiante()//obtener todos los inscritos autonomos
        {
            return serv.getEstadoEstudiante();
        }
    }
}
