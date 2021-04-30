using Logica.Modelos;
using Logica.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Logica.ModelosDAO;
namespace Services.Controllers
{
    public class CalificacionNivelController : ApiController
    {
        ServiciosCalificacionNiveles serv = new ServiciosCalificacionNiveles();
        [HttpGet]
        public IList<ClCalificacionNivel> GetCalificacionNIvel()
        {
            return serv.getCalificacionNivel();
        }
        [HttpPost]
        public void PostinsertarCalificacionNivel(ClCalificacionNivel niv)
        {
            serv.InsertarCalificacionNivel(niv);
        }
        [HttpDelete]
        public string DeleteCalificacionNivel(long id)
        {
            return serv.eliminarCalificacionNivel(id);
        }
        [HttpGet]
        public IList<ClCalificacionNivel> GetCalificacionNivelXId(long id)
        {
            return serv.getCalificacionNivelXId(id);
        }
        [HttpPut]
        public bool UpdateCalificacionNivel(ClCalificacionNivel insA, long id)
        {
            return serv.actualizarCalificacionNivel(insA, id);
        }
    }
}
