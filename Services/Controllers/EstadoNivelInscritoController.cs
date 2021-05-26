using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Logica.Modelos;
using Logica.Servicios;
using NHibernate;
namespace Services.Controllers
{
    public class EstadoNivelInscritoController : ApiController
    {
        ServicioUpdateEstadoNivel serv = new ServicioUpdateEstadoNivel();
        [HttpPatch]
        public bool UpdateEstadoNivel(ClNivelesInscrito niv, long id)//actualizar nivel
        {
            return serv.actualizarEstadoNivel(niv, id);
        }
    }
}
