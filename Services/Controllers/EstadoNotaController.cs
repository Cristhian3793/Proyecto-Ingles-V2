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
    public class EstadoNotaController : ApiController
    {
        ServiciosEstadoNota serv = new ServiciosEstadoNota();
        [HttpGet]
        public IList<ClEstadoNota> getEstadoNota()
        {
            return serv.getEstadoNota();
        }
    }
}
