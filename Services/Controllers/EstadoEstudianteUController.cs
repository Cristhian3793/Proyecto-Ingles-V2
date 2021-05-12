using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Logica.Servicios;
using Logica.Modelos;
namespace Services.Controllers
{
    public class EstadoEstudianteUController : ApiController
    {
        ServicioUpdateEstadoEstudiante serv = new ServicioUpdateEstadoEstudiante();

        [HttpPatch]
        public bool UpdateEstadoEstudiante(ClInscritoAutonomo insA, long idInscrito)//actualizar nivel
        {
            return serv.actualizarEstadoEstudiante(insA, idInscrito);
        }

    }
}
